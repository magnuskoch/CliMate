using CliMate.source.view;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				reader.Append(chars[i]);
			}
			string actual = reader.GetInput();

			// assert
			Assert.AreEqual(expected, actual);
		}
	}
}
