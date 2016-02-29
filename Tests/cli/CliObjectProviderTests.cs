using CliMate.context;
using CliMate.Factories;
using CliMate.interfaces.cli;
using CliMate.source.cli;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.cli.data;

namespace Tests.cli {
	[TestClass]
	public class CliObjectProviderTests {
		[TestMethod]
		public void CanGenerateRootObject() {

			// Arrange
			var factory = CliMateContainer.Create().GetInstance<Factory>();
			var objectProvider = new CliObjectProvider(factory);
			var app = new TestApp();
			app._obj = new TestObject();
			int expectedAppChildren = 1;
			string expectedMethodAlias = "method";
			int intExpectedArgs = 2;

			// Act
			ICliObject rootObject = objectProvider.GetCliObject(app);

			// Assert
			Assert.AreEqual(expectedAppChildren, rootObject.children.Count);
			Assert.AreSame(app._obj, rootObject.children[0].data);
			Assert.AreEqual(expectedMethodAlias, rootObject.children[0].children[0].alias[0]);
			Assert.AreEqual(intExpectedArgs, rootObject.children[0].children[0].children.Count);
		}
	}
}
