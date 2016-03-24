using System;
using CliMate.interfaces.cli;
using CliMate.interfaces.view;

namespace CliMate.source.view {
	public class TerminalAutoCompleteSession : IAutoCompleteSession {

		public void Enter(ICliCommand command, Action<string> uiUpdate) {	
			uiUpdate( "Auto complete !");
		} 

		public string GetSelectedCompletion() {
			return "Auto complete !";
		}
	}	
}	
