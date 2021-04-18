using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SwapperTest
    {
        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            
        }

        [TestMethod]
        public void TestReturnRandomNumber()
        {
            List<int> list = new() { 0, 1, 2, 3, 4, 5 };

            Assert.AreEqual(3, list[3]);
            Swapper.Swap(list, 3, 5);
            Assert.AreEqual(5, list[3]);
        }
    }
}
