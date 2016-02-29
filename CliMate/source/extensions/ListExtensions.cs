using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.Extensions {
	public static class ListExtensions {
		public static List<T2> FilterByType<T1,T2>(this List<T1> list ) where T2 : class {
			var filtered = list.Where(e => e is T2).Select(e => e as T2).ToList(); 
			return filtered;
		}
	}
}
