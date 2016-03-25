using System;

namespace CliMate.interfaces.view {
	public interface IInputStream {
		ConsoleKeyInfo ReadKey();
		void UpdateLine(string line);	
	}	
}
