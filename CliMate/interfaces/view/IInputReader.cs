using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.view {
	public interface IInputReader {
		// We use integers to store an actual char int the [0,255] range
		// and cross platform special characters in the [256,max] range.
		void Insert(int i);
		void Insert(char c);
		void Insert(string s);
		int GetPosition();
		string ClearLine();
		string GetLine();
	}
}
