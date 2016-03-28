using CliMate.consts;
using CliMate.source.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests.view {
	[TestFixture]
	public class InputReaderTests {

		[Test]
		public void CanReadMultipleLine() {
			// Arrange
			var reader = new InputReader();
			char[] chars = { 't', 'e', 's', 't' };
			string expected = string.Join(string.Empty, chars);


			// Act
			for(int i=0; i<chars.Length; i++) {
				reader.Insert(chars[i]);
			}
			reader.ClearLine();

			for(int i=0; i<chars.Length; i++) {
				reader.Insert(chars[i]);
			}
			string actual = reader.GetLine();

			// Assert
			Assert.AreEqual(expected, actual);	
		}


		[Test ()]
		public void CanReadInputWithArrowKeys() {
			
			// arrange
			int[] input = {
				'e',
				's',
				KeyCodes.ArrowLeft,
				KeyCodes.ArrowLeft,
				KeyCodes.ArrowLeft,
				KeyCodes.ArrowLeft,
				KeyCodes.ArrowLeft,
				KeyCodes.ArrowLeft,
				't',
				KeyCodes.ArrowRight,
				KeyCodes.ArrowRight,
				KeyCodes.ArrowLeft,
				KeyCodes.ArrowLeft,
				KeyCodes.ArrowRight,
				KeyCodes.ArrowRight,
				KeyCodes.ArrowRight,
				KeyCodes.ArrowRight,
				KeyCodes.ArrowRight,
				't'
			};

			var reader = new InputReader();
			string expected = "test";
				
			// act
			for(int i=0; i < input.Length; i++) {
				reader.Insert(input[i]);
			}

			string actual = reader.GetLine();

			// assert
			Assert.AreEqual(expected, actual);
		}

		[Test ()]
		public void CanReadInputWithBackSpace() {
			
			// arrange
			int[] input = {
				't','e','s','t', KeyCodes.Backspace, 't'
			};
			var reader = new InputReader();
			string expected = "test";
				
			// act
			for(int i=0; i < input.Length; i++) {
				reader.Insert(input[i]);
			}

			string actual = reader.GetLine();

			// assert
			Assert.AreEqual(expected, actual);
		}

		[Test ()]
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

		[Test]
		public void CarReadSpecialCharacterInput() {
			// arrange
			int[] input = {
				't','e','s','t', KeyCodes.Backspace
			};
			var reader = new InputReader();
			string expected = "tes";
				
			// act
			for(int i=0; i < input.Length; i++) {
				reader.Insert(input[i]);
			}
			string actual = reader.GetLine();

			// assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void CanDeleteAllCharactersWithBackspace() {
			// arrange
			int[] input = {
				't','e','s','t', 
				KeyCodes.Backspace,
				KeyCodes.Backspace,
				KeyCodes.Backspace, 
				KeyCodes.Backspace,
			};
			var reader = new InputReader();
			string expected = string.Empty;
				
			// act
			for(int i=0; i < input.Length; i++) {
				reader.Insert(input[i]);
			}
			string actual = reader.GetLine();

			// assert
			Assert.AreEqual(expected, actual);
		}
	}
}
