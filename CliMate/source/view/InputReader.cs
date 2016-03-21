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
		private Dictionary<char, Action<char>> actionMap;

		public InputReader() {
			Purge();
			CreateActionMap();
		}

		public void Insert(char c) {
			var action = actionMap.Get(c, InsertChar);
			action(c);
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
			actionMap = new Dictionary<char, Action<char>>();
			actionMap[KeyCodes.Backspace] = DeleteChar;
			actionMap[KeyCodes.BackspaceOSX] = DeleteChar;
			actionMap[KeyCodes.ArrowLeft] = c => ChangePosition(-1);
			actionMap[KeyCodes.ArrowRight] = c => ChangePosition(1);
		}

		private void InsertChar(char c) {
			position = buffer.IsNullOrEmpty() || position == null  
				? buffer.AddFirst(c) 
				: buffer.AddAfter(position, c);
		}

		private void DeleteChar(char c) {
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
