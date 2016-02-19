using CliMate.source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces {
	public interface ICliMateModule : ICliMateObject {
		string name {
			get;
		}
		void Deregister();
	}
}
