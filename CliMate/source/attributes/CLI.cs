using CliMate.assets;
using CliMate.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate {
	public class CLI : Attribute, ICliMateObject {
		
		public string name { get; set; }
		private string manual;
	
		public CLI(string name, string manual = Text.NO_MANUAL) {
			this.name = name;
			this.manual = manual;
		}

		public string GetManual() {
			return manual;
		}
	}
}
