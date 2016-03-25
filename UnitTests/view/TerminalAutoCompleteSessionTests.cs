using System;
using System.Collections.Generic;
using CliMate.interfaces.cli;
using CliMate.source.view;
using Moq;
using NUnit.Framework;
using CliMate.interfaces.view;

namespace Tests.view {
	[TestFixture]
	public class TerminalAutoCompleteSessionTests {

		[Test]
		public void CanRunSession() {
			// Arrange
			var uiStream = new Mock<IUIStream>();
			var autoCompleter = new TerminalAutoCompleteSession(null);
			var command = new Mock<ICliCommand>();
			var completion = new List<string>{ "suggestion1", "suggestion2" };
			command.Setup( c => c.GetAutoCompletion() ).Returns( completion ); 

			var actual = new List<string>();
			Action<string> updater = (s) => {
				actual.Add(s);
			};	

			// Act
			autoCompleter.Enter(command.Object, updater);

			// Assert
		}
	}
}
