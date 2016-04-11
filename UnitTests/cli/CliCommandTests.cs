using System.Collections.Generic;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.source.cli;
using CliMate.source.tokenizer;
using Moq;
using NUnit.Framework;
using Tests.cli.data;

namespace Tests.cli {

	[TestFixture]
	public class CliCommandTests {

		private CliCommand GetCommand() {
			var argsMethodSynchronizer = new Mock<IArgsMethodSynchronizer>();
			return new CliCommand(argsMethodSynchronizer.Object);
		}

		[Test]
		public void CanGenerateAutoCompletionOnNoData() {

			// Arrange
			var command = GetCommand();
			int expected = 0;

			// Act
			IList<string> autoCompletion = command.GetAutoCompletion();
			int actual = autoCompletion.Count;


			// Assert
			Assert.AreEqual(expected,actual);	
		}

		[Test]
		public void CanGenerateAutoCompletionOnObject() {

			// Arrange
			var command = GetCommand();
			command.object_ = new CliObject();
			command.object_.children = new List<ICliObject>();

			string method1Name = "method1";
			string method2Name = "method2";

			var method1 = new CliObject();
			method1.name = method1Name; 
			method1.alias = new List<string> { method1Name };

			var method2 = new CliObject();
			method2.name = method2Name; 
			method2.alias = new List<string> { method2Name };

			command.object_.children.Add(method1);
			command.object_.children.Add(method2);

			// Act
			IList<string> autoCompletion = command.GetAutoCompletion();

			// Assert
			Assert.AreEqual(method1Name,autoCompletion[0]);	
			Assert.AreEqual(method2Name,autoCompletion[1]);	
		}

		[Test]
		public void CanPruneAutoCompletionOnPartialInput() {

			// Arrange
			var command = GetCommand();
			command.object_ = new CliObject();
			command.object_.children = new List<ICliObject>();

			var trailing = new Token();
			trailing.value = "matched";
			command.trailing = new List<IToken>{ trailing };
			

			string matchedMethod = "matchedMethod";
			string nonMatchedMethod = "nonMatchedMethod";

			var method1 = new CliObject();
			method1.name = matchedMethod; 
			method1.alias = new List<string> { matchedMethod };

			var method2 = new CliObject();
			method2.name = nonMatchedMethod; 
			method2.alias = new List<string> { nonMatchedMethod };

			command.object_.children.Add(method1);
			command.object_.children.Add(method2);

			int expectedMatches = 1;
	
			// Act
			IList<string> autoCompletion = command.GetAutoCompletion();

			// Assert
			Assert.AreEqual(expectedMatches, autoCompletion.Count);
			Assert.AreEqual(matchedMethod,autoCompletion[0]);	
		}
		[Test]
		public void CanGenerateAutoCompletionOnMethod() {

			// Arrange
			var command = GetCommand();

			command.method = new CliObject();
			command.method.children = new List<ICliObject>();

			command.object_ = new CliObject();
			command.object_.children = new List<ICliObject>();
			command.object_.children.Add(command.method);

			string argument1Name = "method1";
			string argument2Name = "method2";

			var argument1 = new CliObject();
			argument1.name = argument1Name; 
			argument1.alias = new List<string> { argument1Name };

			var argument2 = new CliObject();
			argument2.name = argument2Name; 
			argument2.alias = new List<string> { argument2Name };

			command.method.children.Add(argument1);
			command.method.children.Add(argument2);

			// Act
			IList<string> autoCompletion = command.GetAutoCompletion();

			// Assert
			Assert.AreEqual(argument1Name,autoCompletion[0]);	
			Assert.AreEqual(argument2Name,autoCompletion[1]);	
		}

		[Test]
		public void CanGenerateAutoCompletionOnArgs() {

			// Arrange
			var command = GetCommand();

			command.method = new CliObject();
			command.method.children = new List<ICliObject>();

			string argument1Name = "method1";
			string argument2Name = "method2";

			var argument1 = new CliObject();
			argument1.name = argument1Name; 
			argument1.alias = new List<string> { argument1Name };

			var argument2 = new CliObject();
			argument2.name = argument2Name;
			argument2.alias = new List<string> { argument2Name };

			command.method.children.Add(argument1);
			command.method.children.Add(argument2);

			command.args = new List<ICliObject>();
			command.args.Add(argument1);
			
			// The method has two arguments (argument1 and argument2), but
			// only argument 1 has been registered with the command. So we expect
			// auto completion to suggest a single argument
			const int expected = 1;

			// Act
			IList<string> autoCompletion = command.GetAutoCompletion();

			// Assert
			Assert.AreEqual(argument2Name,autoCompletion[0]);	
			Assert.AreEqual(expected, autoCompletion.Count);
		}

		[Test]
		public void CanPrintManualOnMissingMethod() {
			// Arrange
			var command = GetCommand();	
			command.object_ = new CliObject();
			string expected = "manual";
			command.object_.manual = expected; 

			// Act
			string actual = command.Execute() as string;
				
			// Assert
			Assert.AreEqual(expected, actual);	
		}

		[Test]
		public void CanPrintManualOnInvalidArguments() {
			// Arrange
			var command = GetCommand();	

			command.object_ = new CliObject();
			command.object_.data = new TestObject();

			string expected = "manual";
			command.method = new CliObject();
			command.method.data = typeof(TestObject).GetMethod("_method");			
			command.method.manual = expected; 

			command.args = new List<ICliObject>();

			// Act
			string actual = command.Execute() as string;
				
			// Assert
			Assert.AreEqual(expected, actual);	
		}
	}
}
