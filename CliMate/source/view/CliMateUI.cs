using CliMate.interfaces;
using System;
using System.Collections.Generic;
using CliMate.enums;

namespace CliMate.source.View {
	public class CliMateUI : IUI {

		private static Dictionary<FeedbackType, ConsoleColor> colorMap;
		private static ConsoleColor DEFAULT_COLOR = ConsoleColor.White;

		static CliMateUI() {
			colorMap = new Dictionary<FeedbackType, ConsoleColor>();
			colorMap[FeedbackType.Message] = ConsoleColor.White;
			colorMap[FeedbackType.Warning] = ConsoleColor.Yellow;
			colorMap[FeedbackType.Error] = ConsoleColor.Red;
		}


		public void Write(FeedbackType type, string message) {
			Console.ForegroundColor = colorMap[type];
			Console.WriteLine(message);
			Console.ForegroundColor = DEFAULT_COLOR;
		}

		public void Write(FeedbackType type, string format, params object[] args) {
			Write(type, string.Format(format, args));
		}
	}
}
