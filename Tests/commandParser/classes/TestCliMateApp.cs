﻿using CliMate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tests.commandParser.classes {
	public class TestCliMateApp : CliMateApp {

		[CliMateExposed("root")]
		public Root root { get; set; }

		public override string GetGoodbyeMessage() {
			throw new NotImplementedException();
		}

		public override string GetManual() {
			return "Test app manual";
		}

		public override string GetWelcomeMessage() {
			throw new NotImplementedException();
		}
	}
}