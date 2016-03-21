using CliMate.config;
using CliMate.context;
using CliMate.source.tokens;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests.tokens {
	[TestFixture]
	public class StringSplitterTests {
		
		private static Container container = CliMateContainer.Create();

		[Test ()]
		public void CanSplitComplexInput() {
			// Arrange
			string objectStandard = "object";
			string methodStandard = "method";
			string methodDash = "method-with-dash";
			string arg = "-arg-";
			string value = "value part2OfValue";
			string input = string.Format("{0}    {1}   {2}   {3}  {4}     {5}   {6}       {7}",
				objectStandard, methodStandard, methodDash, methodStandard, arg, value, arg, value);
			var splitter = new StringSplitter(container.GetInstance<Config>());

			string[] methodStack;
			string[] argValuePairs;

			// Act
			splitter.Split(input, out methodStack, out argValuePairs);

			// Assert 
			Assert.AreEqual(objectStandard, methodStack[0]);
			Assert.AreEqual(methodStandard, methodStack[1]);
			Assert.AreEqual(methodDash, methodStack[2]);
			Assert.AreEqual(methodStandard, methodStack[3]);
			Assert.AreEqual(arg.Substring(1), argValuePairs[0]);
			Assert.AreEqual(value, argValuePairs[1]);
			Assert.AreEqual(arg.Substring(1), argValuePairs[2]);
			Assert.AreEqual(value, argValuePairs[3]);
		}
	}
}
