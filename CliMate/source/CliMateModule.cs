using CliMate.Factories;
using CliMate.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CliMate {
	public abstract class CliMateModule : ICliMateModule {

		public abstract string name { get; }

		
		public CliMateModule() {
			
		}
		
		public abstract string GetManual();
		
	}
}
