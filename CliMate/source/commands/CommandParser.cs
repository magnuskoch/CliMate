using CliMate.enums;
using CliMate.interfaces;
using CliMate.source.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source {
	public class CommandParser : ICommandParser {
		
		public CommandParser() {
		}

		public Func<CommandFeedback> GetCommand(string userInput, ICliMateModule module) {
			userInput = userInput.Trim();

			ICliMateObject lastRecognized = null;
			try {
				Func<CommandFeedback> action = GetAction(userInput, module, ref lastRecognized);
				return action;
			} catch(ArgumentException) {
				lastRecognized = lastRecognized ?? module;
                return () => GetInvalidUserInputFeedback(lastRecognized, userInput);
			}
		}

		private CommandFeedback GetInvalidUserInputFeedback(ICliMateObject lastRecognized, string userInput) {
			Debug.Assert(lastRecognized != null, "Last recognized should always default to at least the app instance");

			return new CommandFeedback {
				message = lastRecognized.GetManual(),
				type = FeedbackType.Message
			};

		}
		
		public Func<CommandFeedback> GetAction(string userInput, ICliMateModule module, ref ICliMateObject lastRecognized) {

			Queue<string> commandStack = GetCommandStack(userInput);
			object commandOwner = UnwindCommandStack(module, commandStack, ref lastRecognized);
			Debug.Assert(commandStack.Count == 1, string.Format(
				"Expected only the command method to reamin in the command stack. But stack has [{0}] entries",
				commandStack.Count));
			MethodInfo commandMethod;
			GetExposedCommand(commandOwner, commandStack.Dequeue(), out commandMethod, ref lastRecognized);
			List<KeyValuePair<string, string>> arguments = GetArguments(userInput);

			Func<CommandFeedback> action = GetAction(commandOwner, commandMethod, arguments);

			return action;
		}

		public Func<CommandFeedback> GetAction(object owner, MethodInfo method, List<KeyValuePair<string, string>> arguments) {

			string[] orderedArguments = GetOrderedArguments(method, arguments);
			Func<CommandFeedback> action = () => {
				string message = method.Invoke(owner, orderedArguments) as string;
				return new CommandFeedback { message = message, type = FeedbackType.Message };
			};
			return action;
		}

		public string[] GetOrderedArguments(MethodInfo method, List<KeyValuePair<string, string>> arguments) {
			ParameterInfo[] parameters = method.GetParameters();
			List<CliMateExposed> exposed = new List<CliMateExposed>(parameters.Length);

			for (int i=0; i<parameters.Length; i++) {
				ParameterInfo parameter = parameters[i];
				if(parameter.ParameterType != typeof(string)) {
					throw new ArgumentException(string.Format(
						"Only string arguments are support, {0} is {1}", parameter.Name, parameter.GetType()
					));
				}
				List<CliMateExposed> exposedOnParameter  = parameter.GetCustomAttributes<CliMateExposed>().ToList();
				if(exposedOnParameter.Count != 1 ) {
					string msg = string.Format(
						exposedOnParameter.Count == 0 ?
						"No exposed attribute found on {0}"
						: "Multiple exposed attribues found on {0}",
						parameter.Name	
					);
					throw new ArgumentException(msg);
				}
				Debug.Assert(exposedOnParameter.Count == 1);
				exposed.Insert(i, exposedOnParameter[0]);
			}

			foreach(KeyValuePair<string,string> argPair in arguments) {
				var matches = exposed.Where(e => e.name == argPair.Key).ToList();
                if (matches.Count == 0) {
					throw new ArgumentException(string.Format(
						"No paramater exposed as {0} on {1}", argPair.Value, method.Name
					));
				}
			}
			
			return arguments
				.OrderBy(a => exposed.IndexOf(exposed.Find(e => e.name == a.Key)))
				.Select(a => a.Value)
				.ToArray();

		}

		public object UnwindCommandStack(ICliMateModule module, Queue<string> commandStack, ref ICliMateObject lastRecognized) {

			object o = module;

			while (commandStack.Count > 0) {
				string name = commandStack.Peek();
				object child;
				if (TryGetExposedChild(name, o, out child)) {
					commandStack.Dequeue();				
					lastRecognized = child as ICliMateObject;
					o = child;
				} else { break; }
			} 

            if (commandStack.IsNullOrEmpty()) {
				throw new ArgumentException("Command stack was empty after unwinding for objects, but we need at least one methed defined !");
			}
			return o;
		}

		public CliMateExposed GetExposedCommand(object owner, string name, out MethodInfo method, ref ICliMateObject lastRecognized) {
			MethodInfo[] methodInfos = owner.GetType().GetMethods();
			method = null;

			var exposed = new List<CliMateExposed>();
			foreach (MethodInfo methodInfo in methodInfos) {
				var exposedOnMethod = GetExposed(methodInfo.GetCustomAttributes(), name);
				foreach (CliMateExposed e in exposedOnMethod) {
					method = methodInfo;
					if(method.ReturnType != typeof(string)) {
						throw new ArgumentException(string.Format(
							"Exposed method [{0}] does not return a string !", method.Name	
						));
					}
					lastRecognized = e as ICliMateObject;
                }
				exposed.AddRange(exposedOnMethod);
			}

			if (exposed.Count != 1) {
				string error = String.Format(exposed.Count == 0 ?
					"No public methods exposed as [{0}] on object"
					: "Multiple public methods exposed as [{0}] on object", name
				);
				throw new ArgumentException(error);
			}
			return exposed[0];
		}

		private List<CliMateExposed> GetExposed(IEnumerable<Attribute> attributes, string name = null) {
			var exposed = new List<CliMateExposed>();
			foreach (Attribute attribute in attributes) {
				CliMateExposed _exposed = attribute as CliMateExposed;
				if (_exposed == null) continue;
				if (name == null || _exposed.name == name) {
					exposed.Add(_exposed);
				}
			}
			return exposed;
		}

		public bool TryGetExposedChild(string name, object parent, out object child ) {
			Debug.Assert(!string.IsNullOrEmpty(name), "TryGetExposedChild recieved empty name");
			Debug.Assert(parent != null, "TryGetExposedChild recieve null parent");
			child = null;

			List<PropertyInfo> properties =
				parent.GetType()
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.ToList();

			var exposed = new List<CliMateExposed>();
			foreach(PropertyInfo propertyInfo in properties) {
				var exposedOnProperty = GetExposed(propertyInfo.GetCustomAttributes(), name);
				foreach(CliMateExposed e in exposedOnProperty) {
					child = propertyInfo.GetValue(parent);
				}
				exposed.AddRange(exposedOnProperty);
			}

			if(exposed.Count != 1) {
				string error = String.Format(exposed.Count == 0 ?
					"No public fields exposed as [{0}] on object"
					: "Multiple public fields exposed as [{0}] on object", name  
				);
				return false;
			}
			return true;
		}
	
		public List<KeyValuePair<string,string>> GetArguments(string userInput) {
			int firstArg = userInput.IndexOf(" -");
			string args = userInput.Substring(firstArg+1);
			// for now we only allow the pattern -argName argvalue
			string[] split = args.Split(' ');
			int l = split.Length;
			if (l%2!=0) {
				throw new ArgumentException(string.Format(
					"Input not valid. [{0}] does not match a -argName argvalue pattern", userInput	
				));
			}
			var argPairs = new List<KeyValuePair<string, string>>();
			for (int i = 0; i < l; i += 2) {
				var pair = new KeyValuePair<string, string>(
					split[i].Remove(0,1), 
					split[i + 1]
				);
				argPairs.Add(pair);
			}
			return argPairs;
		}

		public Queue<string> GetCommandStack(string userInput) {
			var queue = new Queue<string>();
			string[] raw = userInput.Split(' ');
			for (int i = 0; i < raw.Length; i++) {
				if (raw[i].StartsWith("-")) break;
				else queue.Enqueue(raw[i]);
			}
			if(queue.Count < 1) {
				throw new ArgumentException(String.Format(
					"Invalid user input {0}", userInput));
			}
			return queue;
		}

	}
}
