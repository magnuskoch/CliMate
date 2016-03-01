using CliMate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.integration.data {
	class TestObject {
		[CliAvailable("method")]
		public string _method(
				[CliAvailable("arg1")] string _arg1,
				[CliAvailable("arg2")] string _arg2
			) {
			return _arg1 + _arg2;
		}
	}
}
