using CliMate;
using CliMate.Factories;
using CliMate.interfaces;
using CliMate.source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests {
	[TestClass]
	public class CommandParserTest {

		private class DummyClimateObject : ICliMateObject {
			public string GetManual() {
				throw new NotImplementedException();
			}
		}

		private ICliMateObject dummy = new DummyClimateObject();
		private static ICliMateApp app = Factory.GetClimateApp();

		private class Decorated : CliMateCommand {

			[CliMateExposed("child")]
			public Child child { get; set; }

			public override string name {
				get {
					return "root";
				}
			}

			public override string GetManual() {
				throw new NotImplementedException();
			}
		}

		private class Child {

			[CliMateExposed("child")]
			public Child child { get; set; }

			[CliMateExposed("action")]
			public string Method(
				[CliMateExposed("name")] string arg1, 
				[CliMateExposed("email")] string arg2) {
				return string.Empty;
			}
		}

		[TestMethod]
		public void CanBuildCallTree() {
			// Arrange
			string input = "obj1 obj2 -file omg";
			var parser = new CommandParser();
			int expectedLength = 2;

			// Act
			Queue<string> callTree = parser.GetCommandStack(input);

			// Assert
			Assert.AreEqual(expectedLength, callTree.Count);
		}

		[TestMethod]
		public void ThrowsIfNoCommandObjectCanBeFound() {
			// Arrange
			string input = "root child action -argument myArg";
			var parser = new CommandParser();

			// Act
			bool gotException = false;
			try {
				parser.GetCommand(input);
			} catch {
				gotException = true;
			}

			// Assert
			Assert.IsTrue(gotException);
		}

		[TestMethod]
		public void CanFindExposedRoot() {
			// Arrange
			string input = "root child action -argument myArg";
			var parser = Factory.GetCommandParser() as CommandParser;
			Queue<string> callTree = parser.GetCommandStack(input);

			// Act
			var rootOwner = new Decorated();
			ICliMateObject dummy = new DummyClimateObject();
			ICliMateModule module = parser.GetModule(callTree, ref dummy);
			rootOwner.Deregister();

			// Assert
			Assert.IsNotNull(module);
		}

		[TestMethod]
		public void ThrowsIfConflictingRootsAreExposed() {
			// Arrange
			Factory.Touch();
			var root = new Decorated();
			
			// Act
			bool gotException = false;
			try {
				var conflict = new Decorated();
			} catch {
				gotException = true;
			}
			root.Deregister();

			// Assert
			Assert.IsTrue(gotException);
		}

		[TestMethod]
		public void CanFindExposedChild() {
			// Arrange
			string input = "root child action -argument myArg";
			var parser = Factory.GetCommandParser() as CommandParser;
			Queue<string> commandStack = parser.GetCommandStack(input);
			var rootOwner = new Decorated();
			rootOwner.child = new Child();
			ICliMateModule module = parser.GetModule(commandStack, ref dummy);

			// Act
			object actual;
			parser.GetExposedChild(commandStack.Dequeue(), rootOwner, out actual);
			rootOwner.Deregister();

			// Assert
			Assert.AreEqual(rootOwner.child, actual);
			
		}


		[TestMethod]
		public void CanUnwindCommandStack() {
			// Arrange
			string input = "root child child action -argument myArg";
			var parser = Factory.GetCommandParser() as CommandParser;
			Queue<string> commandStack = parser.GetCommandStack(input);
			var rootOwner = new Decorated();
			rootOwner.child = new Child();
			rootOwner.child.child = new Child();
            ICliMateModule module = parser.GetModule(commandStack,ref dummy);;

			// Act
			object actual = parser.UnwindCommandStack(module, commandStack, ref dummy);
			rootOwner.Deregister();

			// Assert
			Assert.AreEqual(rootOwner.child.child, actual);

		}

		[TestMethod]
		public void CanGetArguments() {
			// Arrange
			var arg1 = new KeyValuePair<string, string>("arg1", "val1");
			var arg2 = new KeyValuePair<string, string>("arg2", "val2");
			string input = string.Format(
				"root child child action -{0} {1} -{2} {3}", arg1.Key, arg1.Value, arg2.Key, arg2.Value);
			var parser = Factory.GetCommandParser() as CommandParser;


			// Act
			List<KeyValuePair<string,string>> actual = parser.GetArguments(input);

			// Assert
			Assert.AreEqual(arg1.Key, actual[0].Key);
			Assert.AreEqual(arg1.Value, actual[0].Value);
			Assert.AreEqual(arg2.Key, actual[1].Key);
			Assert.AreEqual(arg2.Value, actual[1].Value);

		}

		[TestMethod]
		public void CanGetOrdererArguments() {
			// Arrange
			var arg1 = new KeyValuePair<string, string>("email", "dv@darkside.com");
			var arg2 = new KeyValuePair<string, string>("name", "Darth");
			string input = string.Format(
				"root child child action -{0} {1} -{2} {3}", arg1.Key, arg1.Value, arg2.Key, arg2.Value);
			var parser = Factory.GetCommandParser() as CommandParser;
			List<KeyValuePair<string, string>> arguments = parser.GetArguments(input);
			MethodInfo method = typeof(Child).GetMethod("Method");

			// Act
			string[] actual = parser.GetOrderedArguments(method, arguments);

			// Assert
			Assert.AreEqual(arg2.Value, actual[0]);
			Assert.AreEqual(arg1.Value, actual[1]);
		}

		[TestMethod]
		public void CanGetExposedCommand(){
			// Arrange
			var child = new Child();
			string exposedName = "action";
			string actualName = "Method";
			var parser = Factory.GetCommandParser() as CommandParser;
			MethodInfo actual;

			// Act
			parser.GetExposedCommand(child, exposedName, out actual, ref dummy);

			// Assert
			Assert.AreEqual(actualName, actual.Name);
		}

	}
}
