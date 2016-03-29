using System;
using CliMate.Factories;
using CliMate.consts;
using CliMate.enums;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.interfaces.view;

namespace CliMate.source.view {
	public class TerminalView :  IInputView {

		private IUIStream uiStream;
		private IInputReader inputReader;
		private ICliModule cliModule;
		private Factory factory;

		public TerminalView(ICliModule cliModule, UIStream uiStream, IInputReader inputReader, Factory factory) {
			this.inputReader = inputReader;
			this.uiStream = uiStream;
			this.cliModule = cliModule;
			this.factory = factory;
		}

		public void Enter() {
			Console.WriteLine("hi");
			bool quit = false;
			while(!quit) {
				int input = uiStream.ReadKey();
				if(input == KeyCodes.Return) {
					string line = inputReader.ClearLine();
					ICliCommand command = cliModule.GetCommand( line );
					uiStream.WriteLine( command.Execute().ToString() );
				} else if(input == KeyCodes.Tab) {
 					IAutoCompleteSession autoCompleteSession = factory.Create<IAutoCompleteSession>(); 
					ICliCommand command = cliModule.GetCommand( inputReader.GetLine() );
					autoCompleteSession.Enter(command, autoCompletion => {
						uiStream.UpdateLine(autoCompletion);
						inputReader.ClearLine();
						inputReader.Insert( autoCompleteSession.GetSelectedCompletion() );
					});
				} else {
					inputReader.Insert(input);
					uiStream.UpdateLine( inputReader.GetLine(), inputReader.GetPosition() + 1 );
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
