using System.Collections.Generic;
using CliMate.interfaces.cli;

namespace CliMate.interfaces {
	public interface IAutoCompletionProvider {
		IList<string> GetAutoCompletions(ICliCommand command);	
	}	
}
