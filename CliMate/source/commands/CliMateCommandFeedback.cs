using CliMate.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate {
	public class CommandFeedback {
		public string message { get; set; }
		public FeedbackType type { get; set; }

		public static CommandFeedback Empty {
			get {
				return new CommandFeedback { type = FeedbackType.Message, message = string.Empty };
			}
		}
	}
}	
