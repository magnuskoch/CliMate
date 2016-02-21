using CliMate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.commandParser.classes {
	public class Root : CliMateModule {

		[CliMateExposed("child")]
		public Child child { get; set; }

		public override string name {
			get {
				return "root";
			}
		}

		public override string GetManual() {
			throw new NotImplementedException();
		}
	}
}
