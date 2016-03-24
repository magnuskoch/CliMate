using SimpleInjector;
using CliMate.Factories;
using CliMate.config;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;
using CliMate.interfaces.view;
using CliMate.source.cli;
using CliMate.source.tokens;
using CliMate.source.view;

namespace CliMate.context {
	public class CliMateContainer : Container {
		public static Container Create(object application) {
			var container = new Container();

			container.Register<Config>(() => new Config(), Lifestyle.Singleton);
			container.Register<IStringSplitter, StringSplitter>();
			container.Register<ITokenizer, Tokenizer>();
			container.Register<Factory>(() => new Factory(container), Lifestyle.Singleton);
			container.Register<ICliObject, CliObject>();
			container.Register<ICliCommand, CliCommand>();
			container.Register<ICliTaxonomy, CliTaxonomy>(Lifestyle.Singleton);
			container.Register<ICliObjectProvider, CliObjectProvider>(Lifestyle.Singleton);
			container.Register<IInputView, TerminalView>();
			container.Register<IInputReader, InputReader>();

			container.Verify();

			return container;
		}
	}
}
