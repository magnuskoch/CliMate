using CliMate.interfaces.cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliMate.interfaces.tokens;

namespace CliMate.source.cli {
	public class CliTaxonomy : ICliTaxonomy {

		public void Create(ICliObject root) {
		}

		public ICliCommand GetCommand(IList<IToken> tokens) {
			throw new NotImplementedException();
		}
	}
}
