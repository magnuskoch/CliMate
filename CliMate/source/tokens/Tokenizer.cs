using System;
using System.Collections.Generic;
using System.Linq;
using CliMate.config;
using CliMate.enums;
using CliMate.interfaces.tokens;
using CliMate.source.extensions;
using CliMate.source.tokenizer;

namespace CliMate.source.tokens {
	public class Tokenizer : ITokenizer {

		private IStringSplitter splitter;	
		private Config config;

		public Tokenizer(IStringSplitter splitter, Config config) {
			this.splitter = splitter;
			this.config = config;
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
			bool hasEndDelimiter;
			
            splitter.Split(input, out methodStack, out argValuePairs, out hasEndDelimiter);

			var result = new List<IToken>();
			CreateMethodStackTokens(methodStack, result);
			CreateArgValuePairTokens(argValuePairs, result);

			return result;
		}

		public string RebuildTokens(IList<IToken> tokens) {
			if(tokens.IsNullOrEmpty()) {
				return string.Empty;
			}
			string[] originalValues = tokens.Select( token => {
						string prefix = token.type == TokenType.Argument ? 
							config.ARGUMENT_DELIMITER.Trim() 
							: string.Empty;
						return prefix + token.value;
					}).ToArray();
			string matched = string.Join(" ", originalValues);
			if (matched.Length > 0) {
				matched += " ";
			}
			return matched;
		}

		private void CreateEndDelimiterTokens(bool hasEndDelimiter, List<IToken> target) {
			if(hasEndDelimiter) {
				var token = new Token();
				token.type = TokenType.Delimiter;
				token.value = null;
				target.Add(token);
			}
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
