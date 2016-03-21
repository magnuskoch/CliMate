using CliMate;
using CliMate.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.commandParser.classes {
	public class Root : ICliMateModule {

		[CliAvailable("child")]
		public Child child { get; set; }

		public string name {
			get {
				return "root";
			}
		}

		public string GetManual() {
			throw new NotImplementedException();
		}
	}
}
