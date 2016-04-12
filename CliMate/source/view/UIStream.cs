using System;
using System.Collections.Generic;
using CliMate.consts;
using CliMate.interfaces.view;
using CliMate.source.extensions;

namespace CliMate.source.view {
	public class UIStream : IUIStream {

		private Dictionary<ConsoleKey,Func<int>> actionMap;
		private Dictionary<ConsoleKey,Func<int>> actionMapShift;

		public UIStream() {
			actionMap = new Dictionary<ConsoleKey,Func<int>>();
			actionMapShift = new Dictionary<ConsoleKey,Func<int>>();
			 
			actionMap[ConsoleKey.Backspace] = () => KeyCodes.Backspace;
			actionMap[ConsoleKey.Tab] = () => KeyCodes.Tab;
			actionMap[ConsoleKey.LeftArrow] = () => KeyCodes.ArrowLeft;
			actionMap[ConsoleKey.LeftArrow] = () => KeyCodes.ArrowLeft;
			actionMap[ConsoleKey.UpArrow] = () => KeyCodes.ArrowUp;
			actionMap[ConsoleKey.DownArrow] = () => KeyCodes.ArrowDown;
			actionMap[ConsoleKey.Enter] = () => KeyCodes.Return;

			actionMapShift[ConsoleKey.Tab] = () => KeyCodes.TabShift;;
		}
		public int ReadKey() {
			ConsoleKeyInfo keyInfo = Console.ReadKey(intercept:true);
			Func<int> action = null;

			if((keyInfo.Modifiers & ConsoleModifiers.Shift) != 0) {
				action = actionMapShift.Get(keyInfo.Key, () => (int) keyInfo.KeyChar);
			} else {
				action = actionMap.Get(keyInfo.Key, () => (int) keyInfo.KeyChar);
			}
			

			
			return action();	
		}

		public void UpdateLine(string line, int caret) {
			Console.CursorLeft = 0;
			Console.Write( new String(' ', Console.WindowWidth-1));
			Console.CursorLeft = 0;
			Console.Write(line);
			Console.CursorLeft = caret;
		}

		public void UpdateLine(string line) {
			int caret = line.Length;
			UpdateLine(line, caret);
		}	

		public void WriteLine(string line) {
			Console.Write(Environment.NewLine);
			Console.WriteLine(line);
		}
	}	
}	
