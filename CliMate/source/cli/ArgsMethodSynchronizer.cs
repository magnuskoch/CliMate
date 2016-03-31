using System.Collections.Generic;
using System.Reflection;
using CliMate.interfaces.cli;

namespace CliMate.source.cli {
	public class ArgsMethodSynchronizer : IArgsMethodSynchronizer {
		
		public bool TrySync(MethodInfo methodInfo, IList<ICliObject> args, out object[] argsSynced) {
			argsSynced = null;
			return true;
		}	

	}	
}	
