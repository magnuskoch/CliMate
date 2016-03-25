using System;
using System.Collections.Generic;
using CliMate.interfaces.cli;
using CliMate.interfaces.view;
using CliMate.source.extensions;

namespace CliMate.source.view {
	public class TerminalAutoCompleteSession : IAutoCompleteSession {

		private string completion;
		private IInputStream inputStream;

		public TerminalAutoCompleteSession(IInputStream inputStream) {
			this.inputStream = inputStream;
		}

		public void Enter(ICliCommand command,  Action<string> uiUpdate) {	

			IList<string> completions = command.GetAutoCompletion();
			string matched = GetMatchedPart(command);

			int i = 0;
			int l = completions.Count - 1;

			do {
				completion = matched + completions[i];
				uiUpdate(completion);
				i = l / (i+1); 
			} while (inputStream.ReadKey().Key == ConsoleKey.Tab);
		} 

		private string GetMatchedPart(ICliCommand command) {
			if(command.matched.IsNullOrEmpty()) {
				return string.Empty;
			}
			return string.Join(" ", command.matched);
		}

		public string GetSelectedCompletion() {
			return completion;
		}
	}	
}	
