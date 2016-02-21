using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces {
	public interface ICliMateApp : ICliMateModule {
		ICommandParser commandParser { set; }
		IUI ui { set; }
		string GetWelcomeMessage();
		string GetGoodbyeMessage();
		void Main(string[] args);
	}
}
