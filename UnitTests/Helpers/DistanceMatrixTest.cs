using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DistanceMatrixTest
    {
        int N1;
        int N2;
        DistancesMatrix DistancesMatrix;

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 4;
            N2 = 4;
            DistancesMatrix = new DistancesMatrix(N1, N2);
        }

        [TestMethod]
        public void TestDistanceMatrixGetLength()
        {
            Assert.AreEqual(N1*N2, DistancesMatrix.Array.GetLength(0));
            Assert.AreEqual(N1*N2, DistancesMatrix.Array.GetLength(1));
            DistancesMatrix = new DistancesMatrix(5, 10);
            Assert.AreEqual(50, DistancesMatrix.Array.GetLength(0));
            Assert.AreEqual(50, DistancesMatrix.Array.GetLength(1));
        }

        [TestMethod]
        public void TestDistanceMatrixValues()
        {
            for (int i = 0; i < DistancesMatrix.Array.GetLength(0); ++i)
            {
                for (int j = 0; j < DistancesMatrix.Array.GetLength(1); ++j)
                {
                    if (i==j)
                    {
                        Assert.AreEqual(0, DistancesMatrix[i, j]);
                    }
                    else
                    {
                        Assert.IsTrue(DistancesMatrix[i, j] > 0);
                    }
                }
            }
        }
    }
}
