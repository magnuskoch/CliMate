using System.Collections.Generic;
using System.Reflection;
using CliMate.Factories;
using CliMate.interfaces.cli;
using CliMate.source.cli;
using Moq;
using NUnit.Framework;

namespace Tests.cli {
	[TestFixture]
	public class ArgsMethodSynchronizerTests {

		public void MultipleArgs(string a, string b) {
		}

		public void ArgAndOptionalArg(string a, string b = null) {
		}

		private CliObject GetArgument(string name, object data) {
			var co = new CliObject();
			co.name = name;
			co.data = data;
			return co;
		}

		private ArgsMethodSynchronizer GetSyncher() {
			var factory = new Factory(CliMateTestContainer.Create());
			var syncher = new ArgsMethodSynchronizer(factory);
			return syncher;
		}

		[Test]
		public void CanMatchOptionalArgs() {

			// Arrange
			var syncher = GetSyncher();
			MethodInfo methodInfo = typeof(ArgsMethodSynchronizerTests).GetMethod("ArgAndOptionalArg");

			object valueA = new object();
			
			var args = new List<ICliObject> { GetArgument("a", valueA) };
			object[] synchedArgs;
			int exptectedArgs = 2;

			// Act
			syncher.TrySync(methodInfo, args, out synchedArgs);

			// Assert
			Assert.AreEqual(exptectedArgs, synchedArgs.Length);
			Assert.AreSame(valueA, synchedArgs[0]);	
			Assert.AreEqual(null, synchedArgs[1]);
			
		}

		[Test]
		public void CanMatchWhenAllArgsArePresentButIncorrectlyOrdered() {

			// Arrange
			var syncher = GetSyncher();
			MethodInfo methodInfo = typeof(ArgsMethodSynchronizerTests).GetMethod("MultipleArgs");

			object valueA = new object();
			object valueB = new object();
			
			var args = new List<ICliObject> { GetArgument("b", valueB), GetArgument("a", valueA) };
			object[] synchedArgs;

			// Act
			syncher.TrySync(methodInfo, args, out synchedArgs);

			// Assert
			Assert.AreSame(valueA, synchedArgs[0]);	
			Assert.AreSame(valueB, synchedArgs[1]);	
		}

		[Test]
		public void CanNotMatchWhenArgsAreMissing() {
			// Arrange
			var syncher = GetSyncher();
			MethodInfo methodInfo = typeof(ArgsMethodSynchronizerTests).GetMethod("MultipleArgs");

			object valueA = new object();
			
			var args = new List<ICliObject> { GetArgument("a", valueA) };
			object[] synchedArgs;

			// Act
			bool canSync = syncher.TrySync(methodInfo, args, out synchedArgs);

			// Assert
			Assert.That(!canSync);	
		}
	}	
}	
