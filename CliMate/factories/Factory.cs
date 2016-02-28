using CliMate.context;
using CliMate.interfaces;
using CliMate.source;
using CliMate.source.View;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.Factories {
	
	public class Factory {

		private Container container;

		public Factory(Container container) {
			this.container = container;
		}

		public T Create<T>() where T:class{
			return container.GetInstance<T>();
		}
		
	}
}
