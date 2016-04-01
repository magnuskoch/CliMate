using CliMate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.cli.data {
	class TestObject {
		[CLI("method")]
		public void _method(
				[CLI("arg1")] string _arg1,
				[CLI("arg2")] string _arg2
			) {

		}
	}
}
