using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class IndividualCrossovererTest : ABaseClassTest
    {
        
        [TestMethod]
        public void TestPlaceboCrossover()
        {
            foreach (var crossoverType in Enum.GetValues(typeof(CrossoverType)))
            {
                Individual = new Individual();
                Individual Individual2 = new Individual(Individual);
                IndividualCrossoverer.ChooseCrossoverType((CrossoverType)crossoverType);
                Individual.Crossover(Individual2);
                Assert.AreEqual(Individual.Fitness, Individual2.Fitness);
            }
        }

        [TestMethod]
        public void TestCrossovers()
        {
            foreach (var crossoverType in Enum.GetValues(typeof(CrossoverType)))
            {
                Individual = new Individual();
                Individual Individual2 = new ();
                IndividualCrossoverer.ChooseCrossoverType((CrossoverType)crossoverType);
                Individual.Crossover(Individual2);
                Assert.AreNotEqual(Individual.Fitness, Individual2.Fitness);
            }
        }

    }
}
