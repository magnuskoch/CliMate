using CliMate.interfaces.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.view {
	public class InputReader : IInputReader {

		private List<char> cache = new List<char>();

		public void Append(char c) {
			cache.Add(c);
		}

		private void Purge() {
			cache.Clear();
		}

		public string GetInput() {
			var sb = new StringBuilder();
			int l = cache.Count;
			for(int i=0; i<l; i++) {
				sb.Append(cache[i]);
			}

			Purge();
			return sb.ToString();
		}

	}
}
