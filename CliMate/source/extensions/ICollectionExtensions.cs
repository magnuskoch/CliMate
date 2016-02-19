using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.Extensions {
	public static class ICollectionExtensions {
		public static bool IsNullOrEmpty(this ICollection collection) {
			return collection == null || collection.Count == 0;
		}
	}
}
