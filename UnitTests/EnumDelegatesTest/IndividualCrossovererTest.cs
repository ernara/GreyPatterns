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
        public void TestCrossovers()
        {
            foreach (var crossoverType in Enum.GetValues(typeof(CrossoverType)))
            {
                Individual = new();
                Individual2 = new(Individual);
                IndividualCrossoverer.ChooseCrossoverType((CrossoverType)crossoverType);
                Individual.Crossover(Individual2);
                Assert.AreEqual(Individual.Fitness, Individual2.Fitness);
                Individual = new ();
                Individual2 = new ();
                Individual.Crossover(Individual2);
                Assert.AreNotEqual(Individual.Fitness, Individual2.Fitness);
            }
        }

    }
}
