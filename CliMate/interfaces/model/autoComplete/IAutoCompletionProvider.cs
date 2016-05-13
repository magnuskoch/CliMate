using System.Collections.Generic;
using CliMate.interfaces.cli;

namespace CliMate.interfaces {
	public interface IAutoCompletionProvider<T> {
		IList<string> GetAutoCompletions(T t);	
	}	
}
