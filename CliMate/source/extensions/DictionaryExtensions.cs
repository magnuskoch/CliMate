using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.extensions {
	internal static class DictionaryExtensions {
		public static T2 Get<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 defaultValue = default(T2)) {
			T2 dictionaryValue;
			if( dict.TryGetValue(key, out dictionaryValue)) {
				return dictionaryValue;
			}
			return defaultValue;
		}
	}
}
