using CliMate.interfaces;
using CliMate.source;
using CliMate.source.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.Factories {
	// Doubles as a poor man's ioc
	public class Factory {

		private static IReflectionFacade reflectionFacade;
		private static ICliMateApp app;

		static Factory() {
			ResolveStaticInjections();
		}

		// Forces static injections
		public static void Touch() {
			
        }
		
		private static void ResolveStaticInjections() {
			reflectionFacade = new ReflectionFacade();
			CliMateCommand.reflectionFacade = reflectionFacade;
		}
		
		public static ICliMateApp GetClimateApp() {
			if(app != null) {
				throw new InvalidOperationException("Multiple CliMate app's not supported !");
			}
			Type appType = reflectionFacade.GetSingleImplementor<ICliMateApp>();
			app = reflectionFacade.Instantiate<ICliMateApp>(appType);
			app.commandParser = GetCommandParser();
			app.ui = new CliMateUI();
            return app;
        }

		public static ICommandParser GetCommandParser() {
			Debug.Assert(app != null, "Tried to get command parser, but no app was available !");
			ICommandParser commandParser = new CommandParser();
			commandParser.reflectionFacade = reflectionFacade;
			commandParser.app = app;
			return commandParser;
        }
	}
}
