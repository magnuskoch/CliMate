using System.Collections.Generic;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.source;
using CliMate.source.cli;
using CliMate.source.tokenizer;
using Moq;
using NUnit.Framework;

namespace Tests.autoComplete {


	[TestFixture]
	public class CliCommandAutoCompleterTests {

		private CliCommand GetCommand() {
			var argsMethodSynchronizer = new Mock<IArgsMethodSynchronizer>();
			return new CliCommand(argsMethodSynchronizer.Object);
		}


		private CliCommandAutoCompleter GetCliCommandAutoCompleter() {
			var stringCompleterMock = new Mock<IAutoCompletionProvider<string>>();
			var cliCommandAutoCompleter = new CliCommandAutoCompleter(stringCompleterMock.Object);
			return cliCommandAutoCompleter;
		}

		[Test]
		public void CanGenerateAutoCompletionOnNoData() {

			// Arrange
			var command = GetCommand();
			var autoCompleter = GetCliCommandAutoCompleter();
			int expected = 0;

			// Act
			IList<string> autoCompletion = autoCompleter.GetAutoCompletions(command);
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

			var autoCompleter = GetCliCommandAutoCompleter();

			// Act
			IList<string> autoCompletion = autoCompleter.GetAutoCompletions(command);

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
	
			var autoCompleter = GetCliCommandAutoCompleter();

			// Act
			IList<string> autoCompletion = autoCompleter.GetAutoCompletions(command);

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

			var autoCompleter = GetCliCommandAutoCompleter();

			// Act
			IList<string> autoCompletion = autoCompleter.GetAutoCompletions(command);

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

			var autoCompleter = GetCliCommandAutoCompleter();

			// Act
			IList<string> autoCompletion = autoCompleter.GetAutoCompletions(command);

			// Assert
			Assert.AreEqual(argument2Name,autoCompletion[0]);	
			Assert.AreEqual(expected, autoCompletion.Count);
		}
	}
}
