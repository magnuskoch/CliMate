using CliMate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.cli.data {
	class TestApp {
		[CLI("obj")]
		public TestObject _obj = new TestObject(); 
	}
}
