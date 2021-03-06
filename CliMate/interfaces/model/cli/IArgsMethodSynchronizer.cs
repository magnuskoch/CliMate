using System.Collections.Generic;
using System.Reflection;
using CliMate.interfaces.cli;

namespace CliMate.source.cli {
	public interface IArgsMethodSynchronizer {
		
		bool TrySync(MethodInfo methodInfo, IList<ICliObject> args, out object[] argsSynced); 
	}	
}	
