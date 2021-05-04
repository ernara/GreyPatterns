using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CoordinateTest : ABaseClassTest
    {
        [TestMethod]
        public void TestNewCoordinates()
        {
            Coordinate = new Coordinate(0, N2);
            Assert.AreEqual(0, Coordinate.X);
            Assert.AreEqual(0, Coordinate.Y);

            Coordinate = new Coordinate(1, N2);
            Assert.AreEqual(0, Coordinate.X);
            Assert.AreEqual(1, Coordinate.Y);

            Coordinate = new Coordinate(16, N2);
            Assert.AreEqual(1, Coordinate.X);
            Assert.AreEqual(0, Coordinate.Y);

            Coordinate = new Coordinate(17, N2);
            Assert.AreEqual(1, Coordinate.X);
            Assert.AreEqual(1, Coordinate.Y);

            Coordinate = new Coordinate(32, N2);
            Assert.AreEqual(2, Coordinate.X);
            Assert.AreEqual(0, Coordinate.Y);

            Coordinate = new Coordinate(33, N2);
            Assert.AreEqual(2, Coordinate.X);
            Assert.AreEqual(1, Coordinate.Y);

            Coordinate = new Coordinate(255, N2);
            Assert.AreEqual(15, Coordinate.X);
            Assert.AreEqual(15, Coordinate.Y);
        }
    }
}
