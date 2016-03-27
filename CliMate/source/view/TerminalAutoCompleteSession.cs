using System;
using System.Collections.Generic;
using CliMate.consts;
using CliMate.interfaces.cli;
using CliMate.interfaces.view;
using CliMate.source.extensions;
using System.Linq;

namespace CliMate.source.view {
	public class TerminalAutoCompleteSession : IAutoCompleteSession {

		private string completion;
		private IUIStream uiStream;

		public TerminalAutoCompleteSession(IUIStream uiStream) {
			this.uiStream = uiStream;
		}

		public void Enter(ICliCommand command, Action<string> uiUpdate) {	

			IList<string> completions = command.GetAutoCompletion();
			string matched = GetMatchedPart(command);

			int i = 0;
			int l = completions.Count - 1;

			do {
				completion = matched + completions[i];
				uiUpdate(completion);
				i = l / (i+1); 
			} while (uiStream.ReadKey() == KeyCodes.Tab);
			
			completion += " ";
		} 

		private string GetMatchedPart(ICliCommand command) {
			if(command.matched.IsNullOrEmpty()) {
				return string.Empty;
			}
			string matched = string.Join(" ", command.matched.Select( m => m.value) );
			if (matched.Length > 0) {
				matched += " ";
			}
			return matched;
		}

		public string GetSelectedCompletion() {
			return completion;
		}
	}	
}	
