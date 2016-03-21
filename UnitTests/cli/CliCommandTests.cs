using System.Collections.Generic;
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
	}
}
