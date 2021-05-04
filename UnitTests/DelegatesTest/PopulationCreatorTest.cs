using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PopulationCreatorTest : ABaseClassTest
    {
        public void TestCreatePopulation()
        {
            List<Individual> Population = PopulationCreator.CreatePopulation();
            Assert.AreEqual(PopulationSize, Population.Count);
        }
    }
}
