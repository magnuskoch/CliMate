using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.tokens {
	public interface IStringSplitter {
		void Split(string input, out string[] methodStack, out string[] argValuePairs, out bool hasEndDelimiter);
	}
}
