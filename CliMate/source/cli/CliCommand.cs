using CliMate.interfaces.cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CliMate.source.cli {
	public class CliCommand : ICliCommand {


		private IList<ICliObject> _args;
		public IList<ICliObject> args {
			get {
				_args = _args ?? new List<ICliObject>();
				return _args;
			}
			set {
				_args = value;
			}
		}

		public ICliObject method {
			get; set;
		}

		public ICliObject object_ {
			get; set;
		}

		public void GetAutoCompletion() {
			throw new NotImplementedException();
		}
	}
}
