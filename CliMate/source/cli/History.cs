using System.Collections.Generic;
using CliMate.interfaces;

namespace CliMate.source.cli {
	public class History : IHistory {
	
		private List<string> history;
		private int sessionIndex;

		public History() {
			history = new List<string>();
			sessionIndex = 0;
		}

		public void StartSession() {
			sessionIndex = history.Count;
		}

		public void Add(string item) {
			history.Add(item);
		}

		public string GetPrevious() {
			sessionIndex--;
			return GetCurrent();
		}

		public string GetNext() {
			sessionIndex++;
			return GetCurrent();
		}

		private string GetCurrent() {
			ClampSessionIndex();	
			return history[sessionIndex];	
		}

		private void ClampSessionIndex() {
			if(sessionIndex < 0) sessionIndex = 0;
			else if(sessionIndex >= history.Count) sessionIndex = history.Count -1;
		}

	}	
}	
