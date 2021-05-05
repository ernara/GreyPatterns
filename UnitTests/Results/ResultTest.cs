using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    class ResultTest : ABaseClassTest
    {
        [TestMethod]
        public void TestResult()
        {
            Result result = new (Individual);
            Assert.AreEqual($"{N} {M} {Individual.Fitness}", result.ToString());
                
        }
    }
}

