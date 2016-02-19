using CliMate.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces {
	public interface IUI {
		void Write(FeedbackType type, string message);
		void Write(FeedbackType type, string format, params object[] args);
	}
}
