using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces {
	public interface ICommandParser {
		Func<CommandFeedback> GetCommand(string userInput, ICliMateModule module);	
	}
}
