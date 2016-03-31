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

		private List<ICliObject> _children;
		public List<ICliObject> children {
			get {
				_children = _children ?? new List<ICliObject>();
				return _children;
			}
			set {
				_children = value;
			}
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

		public void Reset() {
			// Value type are given data by the user through the command line in each session.
			// We want to reset this data.
			if (type == CliObjectType.Value) {
				data = null;
			}
		}
	}
}
