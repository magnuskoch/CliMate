using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CliMate.interfaces.cli;

namespace CliMate.source.cli {
	public class CliCommand : ICliCommand {

		public IList<ICliObject> args {
			get; set;
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
			if(args != null) return ICliObject2AutoCompletionStrings(method.children.Except( args ).ToList() );
			if(method != null) return ICliObject2AutoCompletionStrings(method.children);
			if(object_ != null) return ICliObject2AutoCompletionStrings(object_.children);
			else return new List<string>();
		}

		private List<string> ICliObject2AutoCompletionStrings(IList<ICliObject> cliObjects) {
			return cliObjects.Select( co => co.name ).ToList();
		}
	}
}
