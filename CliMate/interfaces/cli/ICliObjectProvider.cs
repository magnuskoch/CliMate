using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.cli {
	public interface ICliObjectProvider {
		void Analyze(object application);
		ICliObject GetCliObject();
	}
}
