using CliMate.Factories;
using CliMate.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate {
	public static class CliMateEntryPoint {
		public static void Start(string[] args) {
			ICliMateApp app = Factory.GetClimateApp();
			app.Main(args);
        }
	}
}
