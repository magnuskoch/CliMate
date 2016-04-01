using CliMate.interfaces.tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliMate.enums;

namespace CliMate.source.tokenizer {
	public class Token : IToken {
		public TokenType type {
			get; set;
		}

		public string value {
			get; set;
		}
	}
}
