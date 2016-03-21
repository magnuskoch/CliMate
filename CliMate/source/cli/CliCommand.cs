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

		public object Execute() {
			object obj = object_.data;
			MethodInfo methodInfo = method.data as MethodInfo;
			object[] arguments = args.Select(arg => arg.data).ToArray();
			return methodInfo.Invoke(obj, arguments);

		}

		public List<string> GetAutoCompletion() {

			if(false) {
			} else {
				return new List<string>();
			}
		}

		private List<string> ICliObject2AutoCompletionStrings(IList<ICliObject> cliObjects) {
			return null;
		}
	}
}
