using CliMate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tests.commandParser.classes {
	public class TestCliMateApp : CliMateApp {

		[CliAvailable("root")]
		public Root root { get; set; }

		public const string MANUAL = "Test app manual";

		public override string GetGoodbyeMessage() {
			throw new NotImplementedException();
		}

		public override string GetManual() {
			return MANUAL;
		}

		public override string GetWelcomeMessage() {
			throw new NotImplementedException();
		}
	}
}
