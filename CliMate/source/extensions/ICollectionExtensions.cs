using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.extensions {
	public static class ICollectionExtensions {
		public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
			return collection == null || collection.Count == 0; 
		}
	}
}
