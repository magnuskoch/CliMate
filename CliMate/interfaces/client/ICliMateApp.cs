using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces {
	public interface ICliMateApp : ICliMateModule {
		string GetWelcomeMessage();
		string GetGoodbyeMessage();
		void Main(string[] args);
	}
}
