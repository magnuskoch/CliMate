using System.Collections.Generic;
using CliMate.interfaces.cli;
using CliMate.source.cli;
using NUnit.Framework;

namespace Tests.cli {

	[TestFixture]
	public class CliCommandTests {

		[Test]
		public void CanGenerateAutoCompletionOnNoData() {

			// Arrange
			var command = new CliCommand();
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
			var command = new CliCommand();
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
		public void CanGenerateAutoCompletionOnMethod() {

			// Arrange
			var command = new CliCommand();

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
			var command = new CliCommand();

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
	}
}
