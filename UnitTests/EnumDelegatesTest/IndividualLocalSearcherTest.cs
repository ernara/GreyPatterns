using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class IndividualLocalSearcherTest : ABaseClassTest
    {
        [TestMethod]
        public void TestLocalSearch()
        {
            Individual = new Individual(Enumerable.Range(0, N).ToList(), 0);
            Individual.LocalSearch(true);
            Assert.IsTrue(Individual.Fitness > 0);

            Individual = new Individual(Enumerable.Range(0, N).ToList(), 0);
            Individual.LocalSearch(true);
            Assert.IsTrue(Individual.Fitness > 0);

            //TODO: find how to do exceptions
            //Individual = new Individual(Enumerable.Range(0, 1).ToList(), 0);
            //Assert.ThrowsException(   (List<int>) => Individual.LocalSearch(true)   );
        }
    }
}
