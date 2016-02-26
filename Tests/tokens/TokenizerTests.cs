using CliMate.enums;
using CliMate.interfaces.tokens;
using CliMate.source.tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.tokens {
	[TestClass]
	public class TokenizerTests {

		[TestMethod]
		public void CanParseAllTypes() {
			// Arrange
			string _object = "object";
			string method = "metod";
			string argument = "-argument";
			string value = "value";

			string input = string.Format("{0} {1} {2} {3} {4} {5} {6}", 
				_object, _object, method, argument, value, argument, value);

			var tokenizer = new Tokenizer();

			// Act
			List<IToken> tokens = tokenizer.GetTokens(input);

			// Assert
			Assert.AreEqual(TokenType.Object, tokens[0]);
			Assert.AreEqual(TokenType.Method, tokens[1]);
			Assert.AreEqual(TokenType.Argument, tokens[2]);
			Assert.AreEqual(TokenType.Value, tokens[3]);
		}		
	}
}
