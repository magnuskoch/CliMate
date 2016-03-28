using System.Collections.Generic;
using CliMate.interfaces;
using CliMate.interfaces.cli;
using CliMate.interfaces.tokens;

namespace CliMate.source.cli {
	public class CliModule : ICliModule {

		public ITokenizer tokenizer; 
		public ICliTaxonomy taxonomy; 

		public CliModule(ITokenizer tokenizer, ICliTaxonomy taxonomy) {
			this.tokenizer = tokenizer;
			this.taxonomy = taxonomy;
		}

		public ICliCommand GetCommand(string input)	{
			List<IToken> tokens = tokenizer.GetTokens(input);
			ICliCommand command = taxonomy.GetCommand(tokens);
			return command;
		}
	}	
}	

