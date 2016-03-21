using CliMate.interfaces.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliMate.enums;
using CliMate.consts;

namespace CliMate.source.view {
	public class TerminalView :  IInputView {

		private IInputReader inputReader;

		public TerminalView(IInputReader inputReader) {
			this.inputReader = inputReader;
		}

		public event EventHandler autoCompleteRequested;
		public event EventHandler executionRequested;

		public void Enter() {
			Console.WriteLine("hi");
			bool quit = false;
			while(!quit) {
				char input = Console.ReadKey().KeyChar;
				if(input == KeyCodes.Return || input == KeyCodes.ReturnOSX) {
					string line = inputReader.ClearLine();
					Console.WriteLine("Executing :" + line);
				} else {
					inputReader.Insert(input);
					int position = inputReader.GetPosition();
					Console.CursorLeft = 0;
					Console.Write(inputReader.GetLine());
					Console.CursorLeft = position + 1;
				}
			}
		}

		public string GetInput() {
			throw new NotImplementedException();
		}

		public void Update(string content, ViewContentFormat format) {
			throw new NotImplementedException();
		}
	}
}
