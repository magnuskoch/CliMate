using System;
using System.Collections.Generic;
using CliMate.interfaces.cli;
using CliMate.interfaces.view;

namespace CliMate.source.view {
	public class TerminalAutoCompleteSession : IAutoCompleteSession {

		private string completion;

		public void Enter(ICliCommand command, Action<string> uiUpdate) {	

			IList<string> completions = command.GetAutoCompletion();

			int i = 0;
			int l = completions.Count - 1;

			do {
				completion = completions[i];
				uiUpdate(completion);
				i = l / (i+1); 
			} while (Console.ReadKey().Key == ConsoleKey.Tab);
		} 

		public string GetSelectedCompletion() {
			return completion;
		}
	}	
}	
