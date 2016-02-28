using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.cli {
	public interface ICliObject {
		string name { get; set; }
		IList<string> alias { get; set; }
		string manual { get; set; }
		ICliObject parent { get; set; }
		List<ICliObject> children { get; set; }
		object data { get; set; }
	}
}
