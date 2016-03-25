using System;
using CliMate.interfaces.view;

namespace CliMate.source.view {
	public class UIStream : IUIStream {
		public UIStream() {
			
		}
		public ConsoleKeyInfo ReadKey() {
			return Console.ReadKey();
		}

		public void UpdateLine(string line) {
			Console.CursorLeft = 0;
			Console.Write(line);
			Console.CursorLeft = line.Length;
		}	
	}	
}	
