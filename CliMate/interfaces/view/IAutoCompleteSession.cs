using System;
using CliMate.interfaces.cli;

namespace CliMate.interfaces.view {
	public interface IAutoCompleteSession {
		void Enter(ICliCommand command, Action<string> uiUpdate);	
	}	
}
