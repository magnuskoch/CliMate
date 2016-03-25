using System;

namespace CliMate.interfaces.view {
	public interface IUIStream {
		ConsoleKeyInfo ReadKey();
		void UpdateLine(string line);	
	}	
}
