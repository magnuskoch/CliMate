using CliMate.interfaces.cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliMate.enums;

namespace CliMate.source.cli {
	public class CliObject : ICliObject {
		public IList<string> alias {
			get; set;
		}

		public List<ICliObject> children {
			get; set;
		}

		public object data {
			get; set;
		}

		public string manual {
			get; set;
		}

		public string name {
			get; set;
		}

		public ICliObject parent {
			get; set;
		}

		public CliObjectType type {
			get; set;
		}
	}
}
