using System.Collections.Generic;
using CliMate.source;
using NUnit.Framework;

namespace Tests.autoComplete {


	[TestFixture]
	public class FileSystemAutoCompleterTests {

		private const string TEST_DATA_PATH = "../../test-data/directory-structure";

		[Test]
		public void CanAutoCompleteOnNoInput() {
			// Arrange
			string input = string.Empty;	
			string expected = "root";
			int expctedNumberOfCompletions = 1;

			var autoCompleter = new FileSystemAutoCompleter();

			// Act
			IList<string> completions = autoCompleter.GetAutoCompletions(input);
		
			// Assert
			Assert.AreEqual(expctedNumberOfCompletions, completions.Count);
			Assert.AreEqual(expected, completions[0]);	
		}
	}
}
