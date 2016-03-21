using CliMate.config;
using CliMate.interfaces.tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.tokens {
	public class StringSplitter : IStringSplitter {

		private Config config;

		public StringSplitter(Config config) {
			this.config = config;
		}

		public void Split(string input, out string[] methodStack, out string[] argValuePairs) {
			int firstArgument = input.IndexOf(config.ARGUMENT_DELIMITER);

			string objectMethodPart = input.Substring(0, firstArgument);
			string argumentPart = input.Substring(firstArgument);

			methodStack = GetObjectMethodSplit(objectMethodPart);
			argValuePairs = GetArgumentSplit(argumentPart);

		}

		private string[] GetArgumentSplit(string input) {

			var collapsed = new List<string>();
			string[] argValuePairs = input.Split(
				new string[] { config.ARGUMENT_DELIMITER },
				StringSplitOptions.RemoveEmptyEntries
			);
			Debug.Assert(argValuePairs.Length % 2 == 0, "Only arg value sequences are support");

			var result = new string[argValuePairs.Length * 2];

			int l = argValuePairs.Length;
			for(int i=0; i<l; i++) {
				string pair = argValuePairs[i];
				int argEnd = pair.IndexOf(' ');
				string arg = pair.Substring(0, argEnd);
				string value = pair.Substring(argEnd).Trim();
				int i_result = i * 2;
				result[i_result + 0] = arg;
				result[i_result + 1] = value;
			}
				
			return result;
		}

		private string[] GetObjectMethodSplit(string input) {
			string[] split = input.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
			return split;
		}
	}
}
