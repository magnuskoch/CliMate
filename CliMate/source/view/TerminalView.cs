using System;
using CliMate.Factories;
using CliMate.consts;
using CliMate.enums;
using CliMate.interfaces;
using CliMate.interfaces.view;

namespace CliMate.source.view {
	public class TerminalView :  IInputView {

		private IInputReader inputReader;
		private ICliModule cliModule;
		private Factory factory;

		public TerminalView(ICliModule cliModule, IInputReader inputReader, Factory factory) {
			this.inputReader = inputReader;
			this.cliModule = cliModule;
			this.factory = factory;
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
				} else if(input == KeyCodes.TabOSX) {
					IAutoCompleteSession autoCompleteSession = factory.Create<IAutoCompleteSession>(); 
					autoCompleteSession.Enter(null, autoCompletion => {
						Console.CursorLeft = 0;
						Console.Write(autoCompletion); 
						Console.CursorLeft = autoCompletion.Length;
					});
					inputReader.Insert( autoCompleteSession.GetSelectedCompletion() );
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
