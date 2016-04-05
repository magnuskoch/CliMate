using System;
using System.Collections.Generic;
using CliMate.config;
using CliMate.consts;
using CliMate.interfaces.cli;
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

			var autoCompleter = new TerminalAutoCompleteSession(uiStream.Object, config);
			var command = new Mock<ICliCommand>();
			var completion = new List<string>{ "suggestion1", "suggestion2" };
			command.Setup( c => c.GetAutoCompletion() ).Returns( completion ); 

			var actual = new List<string>();
			Action<string> updater = (s) => {
				actual.Add(s);
			};

			int expectedSuggestions = 3;	

			// Act
			autoCompleter.Enter(command.Object, updater);

			// Assert
			Assert.AreEqual(expectedElementsInQueueAfterSession, uiInput.Count);
			Assert.AreEqual(expectedSuggestions, actual.Count);
		}
	}
}
