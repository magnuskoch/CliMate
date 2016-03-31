using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CliMate.interfaces.cli;
using CliMate.source.cli;

namespace CliMate.source.cli {
	public class ArgsMethodSynchronizer : IArgsMethodSynchronizer {
		
		public bool TrySync(MethodInfo methodInfo, IList<ICliObject> args, out object[] argsSynced) {
			Dictionary<string,int> parameterNameOrderMap = GetParameterNameOrderMap( methodInfo.GetParameters() );
			return Sync(args, parameterNameOrderMap, out argsSynced);
		}	
		
		private bool Sync(IList<ICliObject> args, Dictionary<string,int> parameterOrderMap, out object[] argsSynced) {
			argsSynced = null;

			// Argument number mismatch
			if(args.Count != parameterOrderMap.Keys.Count) {
				return false;
			}

			// Argument name mismatch
			foreach(string name in parameterOrderMap.Keys) {
				if(args.Count( arg => arg.name == name) != 1) {
					return false;
				}	
			}

			argsSynced = args
				.OrderBy( arg => parameterOrderMap[arg.name])
				.Select( arg => arg.data)
				.ToArray();
					
			return true;
		}

		private static Dictionary<string,int> GetParameterNameOrderMap(ParameterInfo[] parameters) {
			var map = new Dictionary<string,int>();
			for(int i=0; i<parameters.Length; i++) {
				map[parameters[i].Name] = i;
			}
			return map;
		}

	}	
}	
