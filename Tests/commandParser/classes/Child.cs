using CliMate;

namespace Tests.commandParser.classes {
	public class Child {
		[CliMateExposed("child")]
		public Child child { get; set; }

		[CliMateExposed("action")]
		public string Method(
			[CliMateExposed("name")] string arg1,
			[CliMateExposed("email")] string arg2) {
			return string.Empty;
		}
	}
}