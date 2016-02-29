using CliMate.context;
using CliMate.enums;
using CliMate.Factories;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.source.cli;
using CliMate.source.tokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.cli.data;

namespace Tests.cli {
	[TestClass]
	public class CliTaxonomyTests {

		[TestMethod]
		public void CanGenerateCommand() {

			// Arrange
			var source = new TestApp();
			var _object = new TestObject();
			var method = _object.GetType().GetMethod("_method");

			var objectProvider = new Mock<ICliObjectProvider>();
			var factory = CliMateContainer.Create().GetInstance<Factory>();
			var taxonomy = new CliTaxonomy(objectProvider.Object, factory);
			var tokens = new List<IToken> {
				(new Token { type = TokenType.Object, value = "obj" }) as IToken,
				(new Token { type = TokenType.Method, value = "_method" }) as IToken,
				(new Token { type = TokenType.Argument, value = "_arg1" }) as IToken,
				(new Token { type = TokenType.Value, value = "val1" }) as IToken,
				(new Token { type = TokenType.Argument, value = "rg2" }) as IToken,
				(new Token { type = TokenType.Value, value = "val2" }) as IToken
			};

			// Act
			ICliCommand command = taxonomy.GetCommand(tokens);

			// Assert	
			Assert.AreSame(_object, command.object_.data);
			Assert.AreEqual(method.Name, command.method.name);
			Assert.AreEqual("_arg1", command.args[0].name);
			Assert.AreEqual("arg1", command.args[0].alias[0]);
			Assert.AreEqual("val1", command.args[0].data);
			Assert.AreEqual("_arg2", command.args[1].name);
			Assert.AreEqual("arg2", command.args[1].alias[0]);
			Assert.AreEqual("val2", command.args[1].data);

		}
	}
}
