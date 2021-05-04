using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class IndividualCreatorTest : ABaseClassTest
    {
        
        [TestMethod]
        public void TestCreateIndividual()
        {
            foreach (var individualType in Enum.GetValues(typeof(IndividualType)))
            {
                IndividualCreator.ChooseCreatorType((IndividualType)individualType);
                Individual = new Individual();
                Assert.AreEqual(N, Individual.Genes.Count);
                Assert.AreEqual(N, Individual.Genes.Distinct().Count());
                Assert.IsTrue(Individual.Fitness>0);
            }

        }
    }
}
