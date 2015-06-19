﻿using NUnit.Framework;
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
            evaluator = null;
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
        [TestCase]
        public void TestParsingSimplePower()
        {
            evaluator = parser.Parse("5^5");
            Assert.AreEqual(Math.Pow(5,5), evaluator.Evaluate());
        }
        [TestCase]
        public void TestParsingPowerAndFunction()
        {
            evaluator = parser.Parse("Sin(5)^Cos(5)");
            Assert.AreEqual(Math.Pow(Math.Sin(5), Math.Cos(5)), evaluator.Evaluate());
        }
        [TestCase]
        public void TestParsingComplexPower()
        {
            evaluator = parser.Parse("Sin(5)^(Cos(5)^Ln(5))");
            double actual = Math.Sin(5);
            actual = Math.Pow(actual, Math.Pow(Math.Cos(5), Math.Log(5)));
            Assert.AreEqual(actual, evaluator.Evaluate());
        }
        
        
    }
}
