using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MathFunctionParser
{
    [TestFixture]
    class LexerTester
    {
        Lexer lexer;
        [SetUp]
        public void Init()
        {
            lexer = new Lexer(); 
        }
        [TearDown]
        public void Dispose()
        {
            lexer = null;
        }
        [TestCase]
        public void TestRemoveWhiteSpace1()
        {
            string str = lexer.RemoveAllWhiteSpace(" sin(xy ) ");
            Assert.AreEqual("sin(xy)", str);
        }
        [TestCase]
        public void TestRemoveWhiteSpace2()
        {
            string str = lexer.RemoveAllWhiteSpace(" sin(x * y + cos( y * ln( y * pi ) - 78 ) )    ");
            Assert.AreEqual("sin(x*y+cos(y*ln(y*pi)-78))", str);
        }
    }
}
