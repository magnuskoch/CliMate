using CliMate.context;
using CliMate.enums;
using CliMate.interfaces;
using CliMate.interfaces.view;
using CliMate.source;
using CliMate.source.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate {
	public abstract class CliMateApp : ContextObject, ICliMateApp {


		public string name {
			get {
				throw new NotImplementedException();
			}
		}

		public abstract string GetGoodbyeMessage();
		public abstract string GetWelcomeMessage();
		protected object applicationRoot { get; } 


		public CliMateApp() {
			// The app is instantiated by the client, so we need to do
			// injections manually.
			//commandParser = container.GetInstance<ICommandParser>();
			//ui = container.GetInstance<IUI>();
		}


		public void Main(string[] args) {
			var localContainer = CliMateContainer.Create(applicationRoot);
			IInputView view = localContainer.GetInstance<IInputView>();
			view.Enter();
		}
		public abstract string GetManual();

		public void Deregister() {
			throw new NotImplementedException();
		}
	}
}
