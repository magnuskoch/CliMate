using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CliMate.config;
using CliMate.consts;
using CliMate.enums;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.interfaces.view;
using CliMate.source.extensions;

namespace CliMate.source.view {
	public class TerminalAutoCompleteSession : IAutoCompleteSession {

		private string completion;
		private IUIStream uiStream;
		private Config config;
		private IAutoCompletionProvider<ICliCommand> autoCompletionProvider;
		private ITokenizer tokenizer;

		public TerminalAutoCompleteSession(IUIStream uiStream, IAutoCompletionProvider<ICliCommand> autoCompletionProvider, Config config, ITokenizer tokenizer) {
			this.uiStream = uiStream;
			this.config = config;
			this.autoCompletionProvider = autoCompletionProvider;
			this.tokenizer = tokenizer;
		}

		public void Enter(ICliCommand command, Action<string> uiUpdate) {	

			IList<string> completions = autoCompletionProvider.GetAutoCompletions(command); 
			if (completions.Count == 0) {
				return;
			}
			IList<IToken> matchedTokens = command.matched;

			// It is bit tricky do determine when a value-type argument is complete, since we cannot know what constitutes a valid value,
			// which depending on the concrete use can be anything. To remedy this situation we have a ratcher complicated check.
			// First we get past the edge case where no input at all is recognised.
			if(!matchedTokens.IsNullOrEmpty()) {
				bool delimiterEncountered = !command.trailing.IsNullOrEmpty() && command.trailing[0].type == TokenType.Delimiter;

				if(matchedTokens.Last().type == TokenType.Value && !delimiterEncountered) {
					matchedTokens = matchedTokens.Take( matchedTokens.Count - 1 ).ToList();
				}

			}
			string matched = tokenizer.RebuildTokens(matchedTokens);

			int i = 0;
			int l = completions.Count;
			int currentKey = KeyCodes.Tab;
			int shift = 0;
			bool done = false;

			do {
				Debug.Assert( 0 <= i && i < l, String.Format("i must be in range [0.{0}], but was {1}", l-1,i));
 				completion = matched + completions[i];
				uiUpdate(completion);
				currentKey = uiStream.ReadKey();
				if(currentKey == KeyCodes.Tab) shift = 1;
				else if(currentKey == KeyCodes.TabShift) shift = -1;
				else done = true;
				// "+l" to ammend the case where i becomes negative because shift=-1
				i = (i+shift+l) % l; 

			} while (!done);
			
			//completion += " ";
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
