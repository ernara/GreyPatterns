using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualLocalSearcherTest : ABaseClassTest
    {

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
