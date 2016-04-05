using System;
using System.Collections.Generic;
using System.Linq;
using CliMate.config;
using CliMate.consts;
using CliMate.enums;
using CliMate.interfaces.cli;
using CliMate.interfaces.view;
using CliMate.source.extensions;

namespace CliMate.source.view {
	public class TerminalAutoCompleteSession : IAutoCompleteSession {

		private string completion;
		private IUIStream uiStream;
		private Config config;

		public TerminalAutoCompleteSession(IUIStream uiStream, Config config) {
			this.uiStream = uiStream;
			this.config = config;
		}

		public void Enter(ICliCommand command, Action<string> uiUpdate) {	

			IList<string> completions = command.GetAutoCompletion();
			if (completions.Count == 0) {
				return;
			}
			string matched = GetMatchedPart(command);

			int i = 0;
			int l = completions.Count;

			do {
 				completion = matched + completions[i];
				uiUpdate(completion);
				i = (i+1) % l; 
			} while (uiStream.ReadKey() == KeyCodes.Tab);
			
			completion += " ";
		} 

		private string GetMatchedPart(ICliCommand command) {
			if(command.matched.IsNullOrEmpty()) {
				return string.Empty;
			}
			string[] originalValues = command.matched.Select( token => {
						string prefix = token.type == TokenType.Argument ? 
							config.ARGUMENT_DELIMITER.Trim() 
							: string.Empty;
						return prefix + token.value;
					}).ToArray();
			string matched = string.Join(" ", originalValues);
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
