using System.Collections.Generic;
using System.Linq;
using CliMate.enums;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.source.extensions;

namespace CliMate.source {

	public class CliCommandAutoCompleter : IAutoCompletionProvider<ICliCommand> {

		private IAutoCompletionProvider<string> fileSystemAutoCompletionProvider;

		public CliCommandAutoCompleter(IAutoCompletionProvider<string> fileSystemAutoCompletionProvider) {
			this.fileSystemAutoCompletionProvider = fileSystemAutoCompletionProvider;
		}


		public IList<string> GetAutoCompletions(ICliCommand cliCommand) {
 			IList<string> autoCompletions = null;

			if(!cliCommand.args.IsNullOrEmpty()) {
				autoCompletions = ICliObject2AutoCompletionStrings(cliCommand, cliCommand.method.children.Except( cliCommand.args ).ToList() );
			} else if(cliCommand.method != null) {
				autoCompletions = ICliObject2AutoCompletionStrings(cliCommand, cliCommand.method.children);
			} else if(cliCommand.object_ != null) {
				autoCompletions = ICliObject2AutoCompletionStrings(cliCommand, cliCommand.object_.children);
			}

			if(autoCompletions.IsNullOrEmpty()) {
				string trailing = cliCommand.matched.IsNullOrEmpty() ? null : cliCommand.matched.Last().value;
				autoCompletions = fileSystemAutoCompletionProvider.GetAutoCompletions(trailing);		
			}
			return autoCompletions;
		}

		private IList<string> ICliObject2AutoCompletionStrings(ICliCommand cliCommand, IList<ICliObject> cliObjects) {
			var completions = new List<string>();
			string trailingValue = null;

			// If nothing is trailing, we have nothing to autocomplete on. This can be the case when autocompleting in
			// the middle of a "value" type token.
			if (cliCommand.trailing.IsNullOrEmpty()) {
				// However there is an edge case. If there are also no matched tokens, the input is empty, and in this
				// case we do want to auto complete on the command objects.
				if (!cliCommand.matched.IsNullOrEmpty()) {
					return completions;
				}
			}
			else {
				trailingValue = cliCommand.trailing[0].value;
			}

			// If we have more than one trailing token, it makes little sense to attempt autocompletion
			// and we just default to returning the empty list.
			if(cliCommand.trailing == null || cliCommand.trailing.Count <= 1) {
				foreach(ICliObject cliObject in cliObjects) {
					string prefix = 
						cliObject.type == CliObjectType.Value ? "-" : string.Empty;
					string completion = prefix + cliObject.alias[0];
					if(trailingValue == null || completion.Contains(trailingValue)) {
						completions.Add(completion);
					}
				}
			}
			return completions;
		}

	}	
}	
