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
