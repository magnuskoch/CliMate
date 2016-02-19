using CliMate.enums;
using CliMate.interfaces;
using CliMate.source.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate {
	public abstract class CliMateApp : ICliMateApp {

		public abstract string GetGoodbyeMessage();
		public abstract string GetWelcomeMessage();

		public ICommandParser commandParser { private get; set; }
		public IUI ui { get; set; }

		private Dictionary<ConsoleKey, Action> keyDelegates;
		private bool quit;
		private StringBuilder userInput;
		private string[] args;

	
		private void BuildKeyDelegates() {
			keyDelegates = new Dictionary<ConsoleKey, Action>();
			keyDelegates[ConsoleKey.Tab] = AutoComplete;
			keyDelegates[ConsoleKey.Enter] = ExecuteCommand;
        }

		public void Main(string[] args) {
			BuildKeyDelegates();
			quit = false;
			userInput = new StringBuilder();
			this.args = args;
			ui.Write(FeedbackType.Message, GetWelcomeMessage());

			while (!quit) {
				userInput.AppendLine(Console.ReadLine());
				ExecuteCommand();
				/*
				TODO: Enable key-by-key functionality
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				
				Action action = keyDelegates.Get(
					keyInfo.Key, 
					defaultValue : () => AddToCommand(keyInfo.KeyChar)
				);
				action();
				*/
			}
		}

		private void AddToCommand(char c) {
			userInput.Append(c);
		}

		private void ExecuteCommand() {
			Func<CommandFeedback> command = null;
			string input_s = userInput.ToString();
			if(!TryGetSystemCommand(input_s, out command)) {
				command = commandParser.GetCommand(userInput.ToString());
			}
			CommandFeedback feedback = command();
			ui.Write(feedback.type, feedback.message);
			userInput.Clear();
		}

		private bool TryGetSystemCommand(string userInput, out Func<CommandFeedback> command) {
			command = null;
			userInput = userInput.Trim();
			if(userInput.ToLower().Equals("q") ) {
				command = () => {
					quit = true;
					return CommandFeedback.Empty;
				};
			}
			return command != null;
		}

		private void AutoComplete() {
			throw new NotImplementedException();
		}

		public abstract string GetManual();
	}
}
