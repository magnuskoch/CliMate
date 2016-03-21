using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.cli {
	// a cli command is the result of matching an IToken list to a cli
	// taxonomy consisting of ICliObjects
	public interface ICliCommand {
		ICliObject object_ { get; set; }
		ICliObject method { get; set; }
		IList<ICliObject> args { get; set; }
		object Execute();
		void GetAutoCompletion();
	}
}
