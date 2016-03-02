using CliMate.interfaces.view;
using CliMate.source.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CliMate.source.view {
	public class InputReader : IInputReader {

		// Implementation made to be easy to maintain rather than fast.
		private LinkedList<char> buffer = new LinkedList<char>();
		private LinkedListNode<char> position;
		private Dictionary<char, Action<char>> actionMap;

		public InputReader() {
			Purge();
			CreateActionMap();
		}

		private void CreateActionMap() {
			actionMap = new Dictionary<char, Action<char>>();
			actionMap[(char)8] = DeleteChar;
		}

		private void InsertChar(char c) {
			position.Value = c;
			position = buffer.AddAfter(position, ' ');
		}

		private void DeleteChar(char c) {

		}

		private void ChangePosition(int shift) {

		}

		public void Insert(char c) {
			var action = actionMap.Get(c, InsertChar);
			action(c);
		}

		private void Purge() {
			buffer.Clear();
			position = buffer.AddFirst(' ');
		}

		public int GetPosition() {
			return 0;
		}

		public string GetLine() {
			var sb = new StringBuilder();

			LinkedListNode<char> node = buffer.First;
			// The last node is always the empty char ' '
			while(node.Next != null) {
				sb.Append(node.Value);
				node = node.Next;
			}

			Purge();
			return sb.ToString();
		}

	}
}
