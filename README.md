# CliMate
A small framework for adding a command line interface to any .Net application. CliMate provides a simple way of exposing fields, properties, methods and arguments to the command line with auto completion, man-pages, and method feedback only using the CliExposed Attribute. 

# Example

public void MyApp {

	[CliExposed(alias:"MyCommand", manual:"Simply stuff")]
	public void MyMethod() {
	}
} 

