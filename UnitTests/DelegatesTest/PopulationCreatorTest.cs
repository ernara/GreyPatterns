using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PopulationCreatorTest
    {
        int N1;
        int N2;
        int M;
        int PopulationSize;
        public Algorithm Algorithm;

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 5;
            N2 = 5;
            M = 3;
            PopulationSize = 10;
            Algorithm = new Algorithm(-10, N2, M, PopulationSize);
        }

        [TestMethod]
        public void TestPopulationsize()
        {
            Assert.AreEqual(PopulationSize, Algorithm.Population.Count);
        }

        [TestMethod]
        public void TestSort()
        {
            for (int i = 1; i < PopulationSize; i++)
            {
                Assert.IsTrue(Algorithm.Population[i].Fitness >= Algorithm.Population[i - 1].Fitness);
            }
        }
    }
}
