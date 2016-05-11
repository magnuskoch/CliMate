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
			string objectMethodPart = firstArgument == -1 ? 
				input : input.Substring(0, firstArgument);
			string argumentPart = firstArgument == -1 ?
				string.Empty : input.Substring(firstArgument);

			methodStack = GetObjectMethodSplit(objectMethodPart);
			argValuePairs = GetArgumentSplit(argumentPart);

		}

		private string[] GetArgumentSplit(string input) {
			if(input == null) {
				throw new ArgumentException ("Tried to get argument split on null input !");
			}
			if (input.Length == 0) {
				return new string[0];
			}

			string[] argValuePairs = input.Split(
				new string[] { config.ARGUMENT_DELIMITER },
				StringSplitOptions.RemoveEmptyEntries
			);

			var result = new string[argValuePairs.Length * 2];

			int l = argValuePairs.Length;
			for(int i=0; i<l; i++) {
				string pair = argValuePairs[i].Trim();
				int argEnd = pair.IndexOf(' ');
				int i_result = i * 2;
				if (argEnd == -1) {
					// No value is present. Assign entire pair as argument name
					result [i_result + 0] = pair;
				} else {
					string arg = pair.Substring(0, argEnd);
					string value = pair.Substring(argEnd).Trim();

					result[i_result + 0] = arg;
					result[i_result + 1] = value;
				}
			}
				
			return result;
		}

		private string[] GetObjectMethodSplit(string input) {
			string[] split = input.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
			return split;
		}
	}
}
