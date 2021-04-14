using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CoordinateTest
    {
        Coordinate coordinate;
        int N2;

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            N2 = 2;
        }

        [TestMethod]
        public void TestNewCoordinates()
        {
            coordinate = new Coordinate(0, N2);
            Assert.AreEqual(0, coordinate.X);
            Assert.AreEqual(0, coordinate.Y);

            coordinate = new Coordinate(1, N2);
            Assert.AreEqual(0, coordinate.X);
            Assert.AreEqual(1, coordinate.Y);

            coordinate = new Coordinate(2, N2);
            Assert.AreEqual(1, coordinate.X);
            Assert.AreEqual(0, coordinate.Y);

            coordinate = new Coordinate(3, N2);
            Assert.AreEqual(1, coordinate.X);
            Assert.AreEqual(1, coordinate.Y);

            coordinate = new Coordinate(4, N2);
            Assert.AreEqual(2, coordinate.X);
            Assert.AreEqual(0, coordinate.Y);

            coordinate = new Coordinate(5, N2);
            Assert.AreEqual(2, coordinate.X);
            Assert.AreEqual(1, coordinate.Y);
        }
    }
}
