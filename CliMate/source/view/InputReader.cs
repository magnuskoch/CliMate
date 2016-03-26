using CliMate.consts;
using CliMate.interfaces.view;
using CliMate.source.extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.view {
	public class InputReader : IInputReader {

		private LinkedList<char> buffer = new LinkedList<char>();
		private LinkedListNode<char> position;
		private Dictionary<int, Action<int>> actionMap;

		public InputReader() {
			Purge();
			CreateActionMap();
		}

		public void Insert(string s) {
			foreach(char c in s) {
				Insert(c);
			}
		}

		public void Insert(char c) {
			Insert((int) c);
		}

		public void Insert(int i) {
			var action = actionMap.Get(i, InsertChar);
			action(i);
		}

		public int GetPosition() {
			return buffer.GetIndex<char>(position);
		}

		public string GetLine() {
			string line = string.Join(string.Empty, buffer);
			return line;
		}

		public string ClearLine() {
			string line = GetLine();
			Purge();
			return line;
		}

		private void CreateActionMap() {
			actionMap = new Dictionary<int, Action<int>>();

			actionMap[KeyCodes.Backspace] = i => DeleteChar();
			actionMap[KeyCodes.ArrowLeft] = i => ChangePosition(-1);
			actionMap[KeyCodes.ArrowRight] = i => ChangePosition(1);
		}

		private void InsertChar(int i) {
			Debug.Assert( i < 256, string.Format("Trying to insert integer {0} that would overflow the char cast !", i));
			char c = (char) i;

			position = buffer.IsNullOrEmpty() || position == null  
				? buffer.AddFirst(c) 
				: buffer.AddAfter(position, c);
		}

		private void DeleteChar() {
			if (buffer.Count == 1) {
				return;
			}
			LinkedListNode<char> prev = position.Previous;
			buffer.Remove(position);
			position = prev;
			Debug.Assert(position != null, "Position was null after deleting char !");
		}

		private void ChangePosition(int shift) {
			int target = GetPosition() + shift;

			if(shift == 0) {
				return;
			} else if(target < 0) {
				// Setting the position to null implies that is is before the first entry.
				position = null;
				return;
			} else if(target >= buffer.Count - 1) {
				position = buffer.Last;
				return;
			}

			int shift_abs = Math.Abs(shift);
			bool goingForward = (shift / shift_abs) > 0;

			for(int i=0; i < shift_abs; i++) {
				position = goingForward ? position.Next : position.Previous; 
			}
		}

		private void Purge() {
			buffer.Clear();
		}

	}
}
