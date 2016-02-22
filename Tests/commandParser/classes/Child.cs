using System;
using CliMate;
using CliMate.interfaces;

namespace Tests.commandParser.classes {
	public class Child : ICliMateObject {
		[CliMateExposed("child")]
		public Child child { get; set; }

		public int id;

		public const string METHOD_FEEDBACK = "method feedback";

		public Child() {

		}

		public Child(int id) {
			this.id = id;
		}

		public string GetManual() {
			return "Child manual";
		}

		[CliMateExposed("noArgs")]
		public string Method2() {
			return METHOD_FEEDBACK;
		}

		[CliMateExposed("action")]
		public string Method(
			[CliMateExposed("name")] string arg1,
			[CliMateExposed("email")] string arg2 = null) {
			return METHOD_FEEDBACK;
		}
	}
}