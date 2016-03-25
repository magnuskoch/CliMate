using System;
using CliMate.interfaces.view;

namespace CliMate.source.view {
	public class UIStream : IUIStream {
		public UIStream() {
			
		}
		public ConsoleKeyInfo ReadKey() {
			return Console.ReadKey(intercept:true);
		}

		public void UpdateLine(string line) {
			Console.CursorLeft = 0;
			Console.Write(line);
			Console.CursorLeft = line.Length;
		}	

		public void WriteLine(string line) {
			Console.Write(Environment.NewLine);
			Console.WriteLine(line);
		}
	}	
}	
