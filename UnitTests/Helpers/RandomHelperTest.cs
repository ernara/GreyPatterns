using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RandomHelperTest
    {
        int ExceptNumber;
        int MinValue;
        int MaxValue;

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            ExceptNumber = 1;
            MinValue = 0;
            MaxValue = 2;
        }

        [TestMethod]
        public void TestReturnRandomNumber()
        {
            for (int i = 0; i < 100; ++i)
            {
                Assert.AreNotEqual(ExceptNumber, RandomHelper.ReturnRandomNumber(ExceptNumber, MaxValue));
                Assert.AreNotEqual(ExceptNumber, RandomHelper.ReturnRandomNumber(ExceptNumber, MinValue, MaxValue));
            }
        }
    }
}
