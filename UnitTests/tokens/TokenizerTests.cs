using System.Collections.Generic;
using System.Linq;
using CliMate.config;
using CliMate.enums;
using CliMate.interfaces.tokens;
using CliMate.source.tokens;
using NUnit.Framework;
using SimpleInjector;

namespace Tests.tokens {
	[TestFixture]
	public class TokenizerTests {

		private static Container container = CliMateTestContainer.Create();

		[Test]
		public void CanParseEndDelimiter() {
			// Arrange
			var config = container.GetInstance<Config>();
			string input = "Test" + config.END_DELIMITER;
			var tokenizer = new Tokenizer(container.GetInstance<IStringSplitter>(), config);
			var expectedTypeOfLastToken = TokenType.Delimiter;


			// Act
			List<IToken> tokens = tokenizer.GetTokens(input);	

			// Assert
			Assert.AreEqual(expectedTypeOfLastToken, tokens.Last().type);	
		}
	
		[Test]
		public void CanParseNoEndDelimiter() {
			// Arrange
			string input = "Test";
			var config = container.GetInstance<Config>();
			var tokenizer = new Tokenizer(container.GetInstance<IStringSplitter>(), config);
			var disallowedEndToken = TokenType.Delimiter;


			// Act
			List<IToken> tokens = tokenizer.GetTokens(input);	

			// Assert
			Assert.AreNotEqual(disallowedEndToken, tokens.Last().type);	
		}

		[Test]
		public void CanParseAllTypes() {
			// Arrange
			string _object = "object";
			string method = "metod";
			string argument = "argument";
			string value = "value";

			string input = string.Format("{0} {1} {2} -{3} {4} -{5} {6}", 
				_object, _object, method, argument, value, argument, value);
			
			var config = new Config();
			var tokenizer = new Tokenizer(container.GetInstance<IStringSplitter>(), config);
			const int expectedTokens = 7;

			// Act
			List<IToken> tokens = tokenizer.GetTokens(input);

			// Assert
			Assert.AreEqual(expectedTokens, tokens.Count);
			Assert.AreEqual(TokenType.Object, tokens[0].type);
			Assert.AreEqual(TokenType.Object, tokens[1].type);
			Assert.AreEqual(TokenType.Method, tokens[2].type);
			Assert.AreEqual(TokenType.Argument, tokens[3].type);
			Assert.AreEqual(TokenType.Value, tokens[4].type);
			Assert.AreEqual(TokenType.Argument, tokens[5].type);
			Assert.AreEqual(TokenType.Value, tokens[6].type);
		}		
	}
}
