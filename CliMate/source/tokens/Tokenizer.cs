using CliMate.enums;
using CliMate.interfaces.tokens;
using CliMate.source.tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.tokens {
	public class Tokenizer : ITokenizer {

		private IStringSplitter splitter;	

		public Tokenizer(IStringSplitter splitter) {
			this.splitter = splitter;
		}

		public List<IToken> GetTokens(string input) {
			if(input == null) {
				throw new ArgumentException("GetTokens recieved null input !");
			}
			if(input.Trim() == string.Empty) {
				return new List<IToken>();
			}
			string[] methodStack;
			string[] argValuePairs; 
			
            splitter.Split(input, out methodStack, out argValuePairs);

			var result = new List<IToken>();
			CreateMethodStackTokens(methodStack, result);
			CreateArgValuePairTokens(argValuePairs, result);

			return result;
		}

		private void CreateMethodStackTokens(string[] methodStack, List<IToken> target) {
			
			int l = methodStack.Length;	
			for(int i=0; i<l; i++) {
				// TODO: Create through factory
				var token = new Token();
				token.value = methodStack[i];
				bool isLastElement = i == l - 1;
				token.type = isLastElement ? TokenType.Method : TokenType.Object;
				target.Add(token);
			}
		}

		private void CreateArgValuePairTokens(string[] argValuePairs, List<IToken> target) {

			int l = argValuePairs.Length;	
			for(int i=0; i< l; i++) {
				// TODO: Create through factory
				var token = new Token();
				token.value = argValuePairs[i];
				// Alternate between argument and value
				token.type = i % 2 == 0 ? TokenType.Argument : TokenType.Value;
				target.Add(token);
			}
		}

	}
}
