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
        }

        [TestMethod]
        public void TestPopulationsize()
        {
            Algorithm = new Algorithm(N1, N2, M, -10);
            Assert.AreEqual(Algorithm.PopulationSize, Algorithm.Population.Count);

            Algorithm = new Algorithm(N1, N2, M, 10);
            Assert.AreEqual(Algorithm.PopulationSize, Algorithm.Population.Count);

            Algorithm = new Algorithm(N1, N2, M, 100);
            Assert.AreEqual(Algorithm.PopulationSize, Algorithm.Population.Count);
        }

        [TestMethod]
        public void TestSort()
        {
            Algorithm = new Algorithm(N1, N2, M, 10);

            for (int i = 1; i < 10; i++)
            {
                Assert.IsTrue(Algorithm.Population[i].Fitness >= Algorithm.Population[i - 1].Fitness);
            }
        }
    }
}
