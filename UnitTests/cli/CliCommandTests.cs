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
			List<string> autoCompletion = command.GetAutoCompletion();
			int actual = autoCompletion.Count;


			// Assert
			Assert.AreEqual(expected,actual);	
		}

		[Test]
		public void CanGenerateAutoCompletionOnMethod() {

			// Arrange
			var command = new CliCommand();
			command.object_ = new CliObject();
			command.object_.children = new List<ICliObject>();

			string method1Name = "method1";
			string method2Name = "method2";

			var method1 = new CliObject();
			method1.name = method1Name; 

			var method2 = new CliObject();
			method2.name = method2Name; 

			command.object_.children.Add(method1);
			command.object_.children.Add(method2);

			int expected = 0;

			// Act
			List<string> autoCompletion = command.GetAutoCompletion();

			// Assert
			Assert.AreEqual(method1Name,autoCompletion[0]);	
			Assert.AreEqual(method2Name,autoCompletion[1]);	
		}
	}
}
