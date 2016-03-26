using System;

namespace CliMate.interfaces.view {
	public interface IUIStream {
		int ReadKey();
		void UpdateLine(string line);	
		void WriteLine(string line);	
	}	
}
