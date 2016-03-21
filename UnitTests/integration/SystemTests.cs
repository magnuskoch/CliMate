using CliMate.context;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.integration.data;
using NUnit.Framework;

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

			Container container = CliMateContainer.Create();
			var tokenizer = container.GetInstance<ITokenizer>();
			var taxonomy = container.GetInstance<ICliTaxonomy>();
			var cliObjectProvider = container.GetInstance<ICliObjectProvider>();
			cliObjectProvider.Analyze(app);

			// Act
			List<IToken> tokens = tokenizer.GetTokens(input);
			ICliCommand command = taxonomy.GetCommand(tokens);

			// Assert
			Assert.AreEqual(app._obj, command.object_.data);
			Assert.AreEqual(expectedReturn, command.Execute());
		}
	}
}
