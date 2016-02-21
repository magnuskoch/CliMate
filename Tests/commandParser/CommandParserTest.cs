using CliMate;
using CliMate.context;
using CliMate.Factories;
using CliMate.interfaces;
using CliMate.source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tests.commandParser.classes;

namespace Tests {
	[TestClass]
	public class CommandParserTest {

		private static Container container = CliMateContainer.Create();

		private CommandParser parser;

		private class DummyClimateObject : ICliMateObject {
			public string GetManual() {
				throw new NotImplementedException();
			}
		}

		private ICliMateObject dummy = new DummyClimateObject();
		
		[TestInitialize]
		public void TestInitialize() {
			parser = container.GetInstance<ICommandParser>() as CommandParser;
		}

		[TestMethod]
		public void CanUnwindIfLastIsObject() {
			// Arrange
			string input = "root child child";
			Queue<string> commandStack = parser.GetCommandStack(input);
			var app = new TestCliMateApp();
			app.root = new Root();
			app.root.child = new Child(1);
			app.root.child.child = new Child(2);
			ICliMateModule module = app;
			ICliMateObject actual = new DummyClimateObject();

			// Act
			try {
				parser.UnwindCommandStack(module, commandStack, ref actual);
			} catch(ArgumentException) {
				// We expect an execption
			}
			// Assert
			Assert.AreEqual(app.root.child.child, actual);

		}	

		[TestMethod]
		public void CanBuildCallTree() {
			// Arrange
			string input = "obj1 obj2 -file omg";
			int expectedLength = 2;

			// Act
			Queue<string> callTree = parser.GetCommandStack(input);

			// Assert
			Assert.AreEqual(expectedLength, callTree.Count);
		}

		[TestMethod]
		public void GetOrdererArgumentsInsertsBlankOnEmptyOptionals() {
			// Arrange
			var args = new List<KeyValuePair<string, string>>();
			args.Add(new KeyValuePair<string, string>("name", "John"));
            MethodInfo method = typeof(Child).GetMethod("Method");
			var expected = new string[] { "John", null };
			var child = new Child();

			// Act
			string[] actual = parser.GetOrderedArguments(method, args);
			method.Invoke(child, actual);

			// Assert
			Assert.AreEqual(expected[0], actual[0]);	
			Assert.AreEqual(expected[1], actual[1]);	
		}
		

		[TestMethod]
		public void ShowAppManualIfNoCommandObjectCanBeFound() {
			// Arrange
			string input = "nonsense";
			var app = new TestCliMateApp();
			string expected = app.GetManual();

			// Act
			Func< CommandFeedback> feedback = parser.GetCommand(input, app);
			string actual = feedback().message;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CanFindExposedRoot() {
			// Arrange
			string input = "root child action -argument myArg";
			Queue<string> callTree = parser.GetCommandStack(input);

			// Act
			var rootOwner = new Root();
			ICliMateObject dummy = new DummyClimateObject();
			ICliMateModule module = rootOwner;
			
			// Assert
			Assert.IsNotNull(module);
		}

		[TestMethod]
		public void CanFindExposedChild() {
			// Arrange
			string input = "root child action -argument myArg";
			Queue<string> commandStack = parser.GetCommandStack(input);
			var app = new TestCliMateApp();
            ICliMateModule module = app;

			// Act
			object actual;
			parser.TryGetExposedChild(commandStack.Dequeue(), module, out actual);
			
			// Assert
			Assert.AreEqual(app.root, actual);
			
		}


		[TestMethod]
		public void CanUnwindCommandStack() {
			// Arrange
			string input = "root child child action -argument myArg";
			Queue<string> commandStack = parser.GetCommandStack(input);
			var app = new TestCliMateApp();
			app.root = new Root();
			app.root.child = new Child();
			app.root.child.child = new Child();
			ICliMateModule module = app;

			// Act
			object actual = parser.UnwindCommandStack(module, commandStack, ref dummy);
		
			// Assert
			Assert.AreEqual(app.root.child.child, actual);

		}

		[TestMethod]
		public void CanGetArguments() {
			// Arrange
			var arg1 = new KeyValuePair<string, string>("arg1", "val1");
			var arg2 = new KeyValuePair<string, string>("arg2", "val2");
			string input = string.Format(
				"root child child action -{0} {1} -{2} {3}", arg1.Key, arg1.Value, arg2.Key, arg2.Value);
			
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
			
			MethodInfo actual;

			// Act
			parser.GetExposedCommand(child, exposedName, out actual, ref dummy);

			// Assert
			Assert.AreEqual(actualName, actual.Name);
		}

	}
}
