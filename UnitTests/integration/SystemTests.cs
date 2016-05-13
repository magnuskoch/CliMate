using System.Collections.Generic;
using CliMate.context;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using NUnit.Framework;
using SimpleInjector;
using Tests.integration.data;

namespace Tests.integration {
	[TestFixture]
	public class SystemTests {
		[Test ()]
		public void CanMatchCommandToObject() {
			// Arrange
			var app = new TestApp();
			app._obj = new TestObject();

			string expectedReturn = app._obj._method("hey","you");
			string input = "obj method -arg1 hey -arg2 you";

			Container container = CliMateContainer.Create(app);
			var tokenizer = container.GetInstance<ITokenizer>();
			var taxonomy = container.GetInstance<ICliTaxonomy>();

			// Act
			List<IToken> tokens = tokenizer.GetTokens(input);
			ICliCommand command = taxonomy.GetCommand(tokens);

			// Assert
			Assert.AreEqual(app._obj, command.object_.data);
			Assert.AreEqual(expectedReturn, command.Execute());
		}

		[Test]
		public void CanOfferAutoCompletionOnEmptyInput() {

			// Arrange
			var app = new TestApp();
			app._obj = new TestObject();

			Container container = CliMateContainer.Create(app);
			ICliModule cliModule = container.GetInstance<ICliModule>();
			IAutoCompletionProvider<ICliCommand> autoCompletionProvider = container.GetInstance<IAutoCompletionProvider<ICliCommand>>();

			int expectedAutoCompletions = 1;
			string expectedAutoCompletion = "obj";
				
			// Act
			ICliCommand command = cliModule.GetCommand( string.Empty );
			IList<string> autoCompletions = autoCompletionProvider.GetAutoCompletions( command );

			// Assert
			Assert.AreEqual(expectedAutoCompletions, autoCompletions.Count);
			Assert.AreEqual(expectedAutoCompletion, autoCompletions[0]);

		}
	}
}
