using System.Collections.Generic;
using CliMate.source;
using NUnit.Framework;
using System.IO;

namespace Tests.autoComplete {


	[TestFixture]
	public class FileSystemAutoCompleterTests {

		private const string TEST_DATA_PATH = "../../test-data/directory-structure";

		[Test]
		public void CanAutoCompleteOnRootInput() {
			// Arrange
			string input = TEST_DATA_PATH;	
			const string expected = "root";
			const int expctedNumberOfCompletions = 1;

			var autoCompleter = new FileSystemAutoCompleter();

			// Act
			IList<string> completions = autoCompleter.GetAutoCompletions(input);
		
			// Assert
			Assert.AreEqual(expctedNumberOfCompletions, completions.Count);
			Assert.AreEqual(expected, completions[0]);	
		}

		[Test]
		public void CanAutoCompleteOnPartialInput() {
			// Arrange
			string input = Path.Combine(TEST_DATA_PATH, "RO");
			const string expected = "root";
			const int expctedNumberOfCompletions = 1;

			var autoCompleter = new FileSystemAutoCompleter();

			// Act
			IList<string> completions = autoCompleter.GetAutoCompletions(input);

			// Assert
			Assert.AreEqual(expctedNumberOfCompletions, completions.Count);
			Assert.AreEqual(expected, completions[0]);	
		}
	}
}
