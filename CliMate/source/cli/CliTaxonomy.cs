using CliMate.interfaces.cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliMate.interfaces.tokens;
using CliMate.Factories;

namespace CliMate.source.cli {
	public class CliTaxonomy : ICliTaxonomy {

		//private static Dictionary<TokenType, >


		static CliTaxonomy() {

		}

		private ICliObjectProvider objectProvider;
		private Factory factory;

		public CliTaxonomy(ICliObjectProvider objectProvider, Factory factory) {
			this.objectProvider = objectProvider;
			this.factory = factory;
		}

		public ICliCommand GetCommand(IList<IToken> tokens) {
			ICliCommand command = factory.Create<ICliCommand>();
			int l = tokens.Count;
			ICliObject level = objectProvider.GetRoot();
			for(int i=0; i<l; i++) {
				IToken token = tokens[i];
			}
			return command;
		}

		private bool TryGetCliObject(IToken token, out ICliObject _object) {
			_object = null;
			return true;
		}

		private ICliObject GetObject(IToken token) {
			return null;	
		}
	}
}
