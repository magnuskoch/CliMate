using CliMate.interfaces.cli;

namespace CliMate.interfaces {
	public interface ICliModule {
		ICliCommand GetCommand(string input);	
	}	
}
