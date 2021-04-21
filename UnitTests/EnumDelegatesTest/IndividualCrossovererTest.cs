using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class IndividualCrossovererTest
    {
        public readonly Algorithm Algorithm = new(10, 10, 20, 1);
        Individual Individual;
        Individual Individual2;

        [TestCleanup]
        public void TestCleanup()
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            
        }

        [TestMethod]
        public void TestPlaceboCrossover()
        {
            foreach (var crossoverType in Enum.GetValues(typeof(CrossoverType)))
            {
                Individual = new Individual();
                Individual2 = new Individual(Individual);
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
                Individual2 = new Individual();
                IndividualCrossoverer.ChooseCrossoverType((CrossoverType)crossoverType);
                Individual.Crossover(Individual2);
                Assert.AreNotEqual(Individual.Fitness, Individual2.Fitness);
            }
        }

    }
}
