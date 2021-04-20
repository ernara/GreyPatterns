using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualLocalSearcherTest
    {
        int N1;
        int N2;
        int M;
        int PopulationSize;
        Algorithm Algorithm;
        public Individual Individual;

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 32;
            N2 = 32;
            M = 16;
            PopulationSize = 1;

            Algorithm = new Algorithm(N1, N2, M, PopulationSize);
            Individual = new(Algorithm.BestIndividual);

        }

        [TestMethod]
        public void TestPlaceboLocalSearch()
        {
            Assert.AreEqual(Individual.ToString(), Algorithm.ToString());
        }

        [TestMethod]
        public void TestLocalSearchsWhenPopulationSize()
        {
            foreach (var localSearchType in Enum.GetValues(typeof(LocalSearchType)))
            {
                IndividualLocalSearcher.ChooseLocalSearcherType((LocalSearchType)localSearchType);
                Individual.LocalSearch(true);
                Assert.AreNotEqual(Individual.ToString(), Algorithm.ToString());
                Individual = new(Algorithm.BestIndividual);
            }

        }


    }
}
