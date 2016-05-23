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
			string expected = Path.Combine(TEST_DATA_PATH,"root");
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
			string expected = Path.Combine(TEST_DATA_PATH, "root");
			const int expctedNumberOfCompletions = 1;

			var autoCompleter = new FileSystemAutoCompleter();

			// Act
			IList<string> completions = autoCompleter.GetAutoCompletions(input);

			// Assert
			Assert.AreEqual(expctedNumberOfCompletions, completions.Count);
			Assert.AreEqual(expected, completions[0]);	
		}

		[Test]
		public void CanAutoCompleteOnDirectoryTree() {
			// Arrange
			string input = Path.Combine( TEST_DATA_PATH, "root/" );	
			string expected = Path.Combine(TEST_DATA_PATH, "root/child");
			const int expctedNumberOfCompletions = 1;

			var autoCompleter = new FileSystemAutoCompleter();

			// Act
			IList<string> completions = autoCompleter.GetAutoCompletions(input);
		
			// Assert
			Assert.AreEqual(expctedNumberOfCompletions, completions.Count);
			Assert.AreEqual(expected, completions[0]);	
		
		}
		[Test]
		public void CanAutoCompleteOnNoInput() {
			// Arrange
			string input = string.Empty;
			const int expectedMinimumNumberOfCompletions = 1;

			var autoCompleter = new FileSystemAutoCompleter();

			// Act
			IList<string> completions = autoCompleter.GetAutoCompletions(input);

			// Assert
			Assert.That( completions.Count >= expectedMinimumNumberOfCompletions );
		}
	}
}