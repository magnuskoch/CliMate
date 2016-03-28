using CliMate.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.view {
	interface IInputView {
		void Update(string content, ViewContentFormat format );
		string GetInput();
		void Enter();
	}
}
