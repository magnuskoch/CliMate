using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CliMate.Factories;
using CliMate.interfaces;

namespace Tests {
	[TestClass]
	public class ReflectionFacadeTest {

		private class Single { }

		private interface Multiple { }
		private class MultipleA : Multiple { }
		private class MultipleB : Multiple { }

		[TestMethod]
		public void CanGetSingleImplementor() {
			// Arrange
			var reflectionFacade = new ReflectionFacade();

			// Act
			Type t = reflectionFacade.GetSingleImplementor<Single>();

			// Assert
			Assert.IsNotNull(t);
			
		}

		[TestMethod]
		public void FailsOnMultipleIfRequestedSingle() {
			// Arrange
			var reflectionFacade = new ReflectionFacade();

			// Act
			bool failed = false;
			try {
				reflectionFacade.GetSingleImplementor<Multiple>();
			} catch {
				failed = true;
			}

			// Assert
			Assert.IsTrue(failed);
		}
	}
}
