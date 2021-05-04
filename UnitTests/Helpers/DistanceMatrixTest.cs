using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DistanceMatrixTest :ABaseClassTest
    {
        
        [TestMethod]
        public void TestCalculateDistance()
        {
            DistancesMatrix = new(N2, N2);
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < N; ++j)
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
