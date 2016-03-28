using System.Collections.Generic;
using CliMate.interfaces.tokens;

namespace CliMate.interfaces.cli {
	// a cli command is the result of matching an IToken list to a cli
	// taxonomy consisting of ICliObjects
	public interface ICliCommand {
		ICliObject object_ { get; set; }
		ICliObject method { get; set; }
		IList<ICliObject> args { get; set; }
		IList<IToken> matched { get; set; }
		IList<IToken> trailing { get; set; }
		object Execute();
		IList<string> GetAutoCompletion();
	}
}
