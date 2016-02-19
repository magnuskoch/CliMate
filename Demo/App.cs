using CliMate;
using Demo.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo {

	class App : CliMateApp {

		[CliMateExposed("skynet")]
		public Skynet skynet = new Skynet();
		
		public override string GetGoodbyeMessage() {
			return "Goodbye !";
		}

		public override string GetManual() {
			return "available commands: skynet";
		}

		public override string GetWelcomeMessage() {
			return "Welcome to the future !";
		}
	}
}
