using CliMate.config;
using CliMate.Factories;
using CliMate.interfaces;
using CliMate.interfaces.tokens;
using CliMate.source;
using CliMate.source.tokens;
using CliMate.source.View;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.context {
	public class CliMateContainer : Container {
		public static Container Create() {
			var container = new Container();

			container.Register<IUI, CliMateUI>();
			container.Register<ICommandParser, CommandParser>();
			container.Register<Config>(() => new Config(), Lifestyle.Singleton);
			container.Register<IStringSplitter, StringSplitter>();
			container.Register<ITokenizer, Tokenizer>();
			container.Register<Factory>(() => new Factory(container));

			container.Verify();

			return container;
		}
	}
}
