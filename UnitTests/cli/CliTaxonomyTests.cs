using CliMate.context;
using CliMate.enums;
using CliMate.Factories;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.source.cli;
using CliMate.source.tokenizer;
using Moq;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.cli.data;
using NUnit.Framework;

namespace Tests.cli {
	[TestFixture]
	public class CliTaxonomyTests {

		[Test ()]
		public void CanGenerateCommand() {

			// Arrange
			var objectProvider = new Mock<ICliObjectProvider>();

			var root = new CliObject();
			var _object = new CliObject();
			var method = new CliObject();
			var arg = new CliObject();

			_object.alias = new List<string> { "obj" };
			method.alias = new List<string> { "method" };
			arg.alias = new List<string> { "arg" };

			root.children.Add(_object);
			_object.children.Add(method);
			_object.type = CliObjectType.Object;
			method.children.Add(arg);
			method.type = CliObjectType.Method;
			arg.type = CliObjectType.Value;

			objectProvider.Setup(op => op.GetCliObject()).Returns(root);
				
			var factory = CliMateContainer.Create().GetInstance<Factory>();

			var taxonomy = new CliTaxonomy(objectProvider.Object, factory);
			var tokens = new List<IToken> {
				(new Token { type = TokenType.Object, value = "obj" }) as IToken,
				(new Token { type = TokenType.Method, value = "method" }) as IToken,
				(new Token { type = TokenType.Argument, value = "arg" }) as IToken,
				(new Token { type = TokenType.Value, value = "val" }) as IToken,
			};

			// Act
			ICliCommand command = taxonomy.GetCommand(tokens);

			// Assert	
			Assert.AreSame(_object, command.object_);
			Assert.AreEqual(method, command.method);
			Assert.AreEqual(arg, command.args[0]);
		}
	}
}
