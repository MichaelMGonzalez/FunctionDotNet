using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFunctionParser
{
    [TestFixture]
    class ParserTester
    {
        Parser parser;
        Evaluator evaluator;
        [SetUp]
        public void Init()
        {
            parser = new Parser();
        }
        [TearDown]
        public void Dispose()
        {
            parser = null;
        }
        [TestCase]
        public void TestParsingSimpleConstant()
        {
            evaluator = parser.Parse("5");
            Assert.AreEqual(5, evaluator.Evaluate());
        }
        [TestCase]
        public void TestParsingSimpleFunction()
        {
            evaluator = parser.Parse("Sin(5)");
            Assert.AreEqual(Math.Sin(5), evaluator.Evaluate());
        }
        
    }
}
