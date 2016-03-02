using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.view {
	public interface IInputReader {
		void Append(char c);
		string GetInput();
	}
}
