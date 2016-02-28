using CliMate.interfaces.cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CliMate.source.cli {
	public class CliCommand : ICliCommand {


		IList<ICliObject> ICliCommand.args {
			get {
				throw new NotImplementedException();
			}

			set {
				throw new NotImplementedException();
			}
		}

		ICliObject ICliCommand.method {
			get {
				throw new NotImplementedException();
			}

			set {
				throw new NotImplementedException();
			}
		}

		ICliObject ICliCommand.object_ {
			get {
				throw new NotImplementedException();
			}

			set {
				throw new NotImplementedException();
			}
		}

		public void GetAutoCompletion() {
			throw new NotImplementedException();
		}
	}
}
