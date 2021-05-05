using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    class BestResultsTest : ABaseClassTest
    {
        [TestMethod]
        public void TestSetUpResults()
        {
            BestResults.SetUpResults();
            Assert.AreEqual(20, BestResults.BestFoundResults.Count);
            Assert.AreEqual(20, BestResults.BestKnownResults.Count);
        }
    }
}


