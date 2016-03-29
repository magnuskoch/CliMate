using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using CliMate.enums;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.source.extensions;

namespace CliMate.source.cli {
	public class CliCommand : ICliCommand {

		public IList<ICliObject> args {
			get; set;
		}

		public ICliObject method {
			get; set;
		}

		public ICliObject object_ {
			get; set;
		}

		public IList<IToken> trailing {
			get; set;
		}

		public IList<IToken> matched {
			get; set;
		}
		
		public object Execute() {
			Debug.Assert(object_ != null, "Expected command to at least have an object.");

			if(method == null) {
				return object_.manual;
			}
			Debug.Assert(args != null, "Expected args list to be initialized.");

			object obj = object_.data;
			MethodInfo methodInfo = method.data as MethodInfo;
			object[] arguments = args.Select(arg => arg.data).ToArray();
			try {
				return methodInfo.Invoke(obj, arguments);
			} catch(Exception) {
				// We never want to show the user the content of an exception. Something went
				// irrevocably wrong and was not handled. Let's print the manual.
				return method.manual; 
			}
		}

		public IList<string> GetAutoCompletion() {
			if(!args.IsNullOrEmpty()) return ICliObject2AutoCompletionStrings(method.children.Except( args ).ToList() );
			if(method != null) return ICliObject2AutoCompletionStrings(method.children);
			if(object_ != null) return ICliObject2AutoCompletionStrings(object_.children);
			else return new List<string>();
		}

		private List<string> ICliObject2AutoCompletionStrings(IList<ICliObject> cliObjects) {
			var completions = new List<string>();
			foreach(ICliObject cliObject in cliObjects) {
				string prefix = 
					cliObject.type == CliObjectType.Value ? "-" : string.Empty;
				completions.Add(prefix + cliObject.alias[0]);
			}
			return completions;
		}
	}
}
