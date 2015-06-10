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
        [TestCase]
        public void TestSimpleFunctionCase()
        {
            LinkedList<Token> list = lexer.AnalyzeString(" sin(xy ) ");
            string str = "";
            foreach(Token t in list)
            {
                str += t.ToString();
            }
            Assert.AreEqual("Sin(xy)", str);

        }
        [TestCase]
        public void TestComplexCase()
        {
            LinkedList<Token> list = lexer.AnalyzeString(" sin(x * y + cos( y * ln( y * pi ) - 78 ) )    ");
            string str = "";
            foreach (Token t in list)
            {
                str += t.ToString();
            }
            Assert.AreEqual("Sin(x*y+Cos(y*Ln(y*3.14159)-78))", str);

        }
        [TestCase]
        public void TestFunctionsInSeries()
        {
            LinkedList<Token> list = lexer.AnalyzeString(" sin(x) - ln(5) + ln( sin( y) ^ cos(x))    ");
            string str = "";
            foreach (Token t in list)
            {
                str += t.ToString();
            }
            Assert.AreEqual("Sin(x)-Ln(5)+Ln(Sin(y)^Cos(x))", str);

        }
    }
}
