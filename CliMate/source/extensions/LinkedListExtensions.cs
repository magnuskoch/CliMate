using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.extensions {
	public static class LinkedListExtensions {
		public static int GetIndex<T>(this LinkedList<T> list, LinkedListNode<T> element) {

			LinkedListNode<T> node = list.First;
			int position_index = 0;
			while(node != null) {
				if(node == element) {
					return position_index;
				}
				position_index++;
				node = node.Next;
			}
			// No Match
			return -1;
		}
	}
}
