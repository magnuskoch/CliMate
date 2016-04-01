using CliMate.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces.tokens {
	public interface IToken {
		TokenType type { get; set; }
		string value { get; set; }
	}
}
