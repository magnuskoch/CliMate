using CliMate.Factories;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source{
    public class ContextObject {

		protected Container container {
			get { return Factory.container; }
		}

		public ContextObject() {
			Factory.Touch();
		}
	}
}
