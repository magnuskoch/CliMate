using System.Collections.Generic;
using System.Linq;
using CliMate.assets;
using CliMate.enums;
using CliMate.interfaces.cli;
using CliMate.source.extensions;

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

		private string _manual;

		public string manual {
			get {
				return _manual ?? GetDefaultManual();  
			} set {
				_manual = value;
			}
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

		private string GetDefaultManual() {
			if( children.IsNullOrEmpty()) {
				return Text.NO_MANUAL;
			}
			string[] availableChildren = children.Select( child => child.alias[0] ).ToArray();
			return string.Join(",", availableChildren); 
		}

	}
}
