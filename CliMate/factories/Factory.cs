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

		public static Container container {
			get; private set;
		}
		
		static Factory() {
			container = CliMateContainer.Create();
		}



		// Forces static injections
		public static void Touch() {
			
        }
		
		
	}
}
