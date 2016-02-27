using CliMate.interfaces.tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.cli {
	public interface ICliTaxonomy {
		void Create(ICliObject root);
		ICliCommand GetCommand(IList<IToken> tokens);
	}
}
