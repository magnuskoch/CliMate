using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.cli {
	// a cli command is the result of matching a IToken list to a clie
	// taxonomy consisting of ICliObjects
	interface ICliCommand {
		ICliObject theObject { get; set; }
		ICliObject method { get; set; }
		IList<ICliObject> args { get; set; }
		void GetAutoCompletion();
	}
}
