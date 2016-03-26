using System;
using System.Collections.Generic;
using CliMate.consts;
using CliMate.interfaces.view;
using CliMate.source.extensions;

namespace CliMate.source.view {
	public class UIStream : IUIStream {

		private Dictionary<ConsoleKey,Func<int>> actionMap;

		public UIStream() {
			 actionMap = new Dictionary<ConsoleKey,Func<int>>();
			 
			 actionMap[ConsoleKey.Backspace] = () => KeyCodes.Backspace;
			 actionMap[ConsoleKey.Tab] = () => KeyCodes.Tab;
			 actionMap[ConsoleKey.LeftArrow] = () => KeyCodes.ArrowLeft;
			 actionMap[ConsoleKey.RightArrow] = () => KeyCodes.ArrowRight;
			 actionMap[ConsoleKey.Enter] = () => KeyCodes.Return;
		}
		public int ReadKey() {
			ConsoleKeyInfo keyInfo = Console.ReadKey(intercept:true);
			Func<int> action = actionMap.Get(keyInfo.Key, () => (int) keyInfo.KeyChar);
			
			return action();	
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
