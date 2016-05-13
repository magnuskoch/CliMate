using System.Collections.Generic;
using System.Linq;
using CliMate.enums;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.source.extensions;

namespace CliMate.source {

	public class CliCommandAutoCompleter : IAutoCompletionProvider<ICliCommand> {

		public IAutoCompletionProvider<string> fileSystemAutoCompletionProvider;

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
				autoCompletions = fileSystemAutoCompletionProvider.GetAutoCompletions( cliCommand.trailing[0].value);		
			}
			return autoCompletions;
		}

		private IList<string> ICliObject2AutoCompletionStrings(ICliCommand cliCommand, IList<ICliObject> cliObjects) {
			var completions = new List<string>();
			// If we have more than one trailing token, it makes little sense to attempt autocompletion
			// and we just default to returning the empty list.
			if(cliCommand.trailing == null || cliCommand.trailing.Count <= 1) {
				string trailingValue = cliCommand.trailing.IsNullOrEmpty() ? null : cliCommand.trailing[0].value;
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
