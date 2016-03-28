using System;

namespace CliMate.interfaces.view {
	public interface IUIStream {
		int ReadKey();
		void UpdateLine(string line);	
		void UpdateLine(string line, int caret);	
		void WriteLine(string line);	
	}	
}
