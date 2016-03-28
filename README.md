# CliMate
A small framework for adding a command line interface to any .Net application. CliMate provides a simple way of exposing fields, properties, methods and arguments to the command line by decorating code with custom attributes. 
CliMate suports out-of-the-box auto completion, man-pages, and string feedback when executing methods. 

# Example

	public void MyApp {

		[CliExposed(alias:"MyCommand", manual:"Simply stuff")]
		public void MyMethod() {
		}
	} 

