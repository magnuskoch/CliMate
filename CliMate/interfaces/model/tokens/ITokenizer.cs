using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.tokens {
	public interface ITokenizer {
		List<IToken> GetTokens(string input);
		string RebuildTokens(IList<IToken> tokens);
	}
}
