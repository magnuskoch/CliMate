using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			int currentKey = KeyCodes.Tab;
			int shift = 0;
			bool done = false;

			do {
				Debug.Assert( 0 >= i && i < l, String.Format("i must be in range [0.{0}], but was {1}", i,l));
 				completion = matched + completions[i];
				uiUpdate(completion);
				// "+l" to ammend the case where i becomes negative because shift=-1
				currentKey = uiStream.ReadKey();
				if(currentKey == KeyCodes.Tab) shift = 1;
				else if(currentKey == KeyCodes.TabShift) shift = -1;
				else done = true;
				i = (i+shift+l) % l; 

			} while (!done);
			
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
