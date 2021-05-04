using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualLocalSearcherTest : ABaseClassTest
    {
        [TestMethod]
        public void TestLocalSearch()
        {
            foreach (var localSearchType in Enum.GetValues(typeof(LocalSearchType)))
            {
                IndividualLocalSearcher.ChooseLocalSearcherType((LocalSearchType)localSearchType);
                Individual = new Individual(Enumerable.Range(0, N).ToList(), 0);
                Individual.LocalSearch(true);
                Assert.IsTrue(Individual.Fitness > 0);

                try
                {
                    Individual = new Individual(Enumerable.Range(0, Individual.N * 2).ToList(), 0);
                    Individual.LocalSearch(true);
                }

                catch (ArgumentException e)
                {
                    Assert.AreEqual(Exceptions.WrongGenesCountMessage, e.Message);
                }

            }
        }
    }
}
