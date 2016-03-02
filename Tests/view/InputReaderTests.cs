using CliMate.source.view;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests.view {
	[TestClass]
	public class InputReaderTests {

		[TestMethod]
		public void CanReadSimpleInput() {
			// arrange
			char[] chars = {
				't','e','s','t'
			};
			var reader = new InputReader();
			string expected = "test";
				
			// act
			for(int i=0; i < chars.Length; i++) {
				reader.Insert(chars[i]);
			}

			string actual = reader.GetLine();

			// assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CarReadSpecialCharacterInput() {
			// arrange
			char backspace = (char) Keys.Back;
			char[] chars = {
				't','e','s','t',
			};
			var reader = new InputReader();
			string expected = "test";
				
			// act
			for(int i=0; i < chars.Length; i++) {
				reader.Insert(chars[i]);
			}
			string actual = reader.GetLine();

			// assert
			Assert.AreEqual(expected, actual);
		}
	}
}
