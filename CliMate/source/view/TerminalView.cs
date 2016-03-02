using CliMate.interfaces.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliMate.enums;

namespace CliMate.source.view {
	public class TerminalView :  IInputView {

		private IInputReader inputReader;

		public TerminalView(IInputReader inputReader) {
			this.inputReader = inputReader;
		}

		public event EventHandler autoCompleteRequested;
		public event EventHandler executionRequested;

		public void Enter() {

		}

		public string GetInput() {
			throw new NotImplementedException();
		}

		public void Update(string content, ViewContentFormat format) {
			throw new NotImplementedException();
		}
	}
}
