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
			var arg1 = new CliObject();
			var arg2 = new CliObject();

			_object.alias = new List<string> { "obj" };
			method.alias = new List<string> { "method" };
			arg1.alias = new List<string> { "arg1" };
			arg2.alias = new List<string> { "arg2" };

			root.children.Add(_object);
			_object.children.Add(method);
			_object.type = CliObjectType.Object;
			method.children.Add(arg1);
			method.children.Add(arg2);
			method.type = CliObjectType.Method;
			arg1.type = CliObjectType.Value;
			arg2.type = CliObjectType.Value;

			objectProvider.Setup(op => op.GetCliObject()).Returns(root);
				
			var factory = CliMateContainer.Create().GetInstance<Factory>();

			var taxonomy = new CliTaxonomy(objectProvider.Object, factory);
			var tokens = new List<IToken> {
				(new Token { type = TokenType.Object, value = "obj" }) as IToken,
				(new Token { type = TokenType.Method, value = "method" }) as IToken,
				(new Token { type = TokenType.Argument, value = "arg1" }) as IToken,
				(new Token { type = TokenType.Value, value = "val1" }) as IToken,
				(new Token { type = TokenType.Argument, value = "arg2" }) as IToken,
				(new Token { type = TokenType.Value, value = "val2" }) as IToken,
			};

			// Act
			ICliCommand command = taxonomy.GetCommand(tokens);

			// Assert	
			Assert.AreSame(_object, command.object_);
			Assert.AreEqual(method, command.method);
			Assert.AreEqual(arg1, command.args[0]);
			Assert.AreEqual(arg2, command.args[1]);
		}
	}
}
