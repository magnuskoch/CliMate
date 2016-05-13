using System;
using System.Collections.Generic;
using CliMate.config;
using CliMate.consts;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.interfaces.view;
using CliMate.source.view;
using Moq;
using NUnit.Framework;

namespace Tests.view {
	[TestFixture]
	public class TerminalAutoCompleteSessionTests {

		[Test]
		public void CanRunSession() {
			// Arrange

			var config = new Config();
			var uiStream = new Mock<IUIStream>();
			var uiInput = new Queue<int>();
		  
			uiInput.Enqueue(KeyCodes.Tab);
			uiInput.Enqueue(KeyCodes.Tab);
			// We expect the tab session to end on this input
			uiInput.Enqueue(KeyCodes.Space);
			// So this last elemennt should remain in the queue
			uiInput.Enqueue('t');
			
			int expectedElementsInQueueAfterSession = 1;

			uiStream.Setup( u => u.ReadKey() ).Returns( () => uiInput.Dequeue() );

			var tokenizer = new Mock<ITokenizer>();
			var completion = new List<string>{ "suggestion1", "suggestion2" };
			var autoCompleter = new Mock<IAutoCompletionProvider<ICliCommand>>();
			autoCompleter.Setup( ac => ac.GetAutoCompletions(It.IsAny<ICliCommand>())).Returns( completion );
			var autoCompleteSession = new TerminalAutoCompleteSession(uiStream.Object, autoCompleter.Object, config, tokenizer.Object);
			var command = new Mock<ICliCommand>();

			var actual = new List<string>();
			Action<string> updater = (s) => {
				actual.Add(s);
			};

			int expectedSuggestions = 3;	

			// Act
			autoCompleteSession.Enter(command.Object, updater);

			// Assert
			Assert.AreEqual(expectedElementsInQueueAfterSession, uiInput.Count);
			Assert.AreEqual(expectedSuggestions, actual.Count);
		}
	}
}
