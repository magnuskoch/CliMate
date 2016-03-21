using CliMate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.integration.data {
	class TestApp {
		[CliAvailable("obj")]
		public TestObject _obj = new TestObject(); 
	}
}
