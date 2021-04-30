using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualMutatorTest : ABaseClassTest
    {

        [TestMethod]
        public void TestPlaceboMutate()
        {
            foreach (var mutateType in Enum.GetValues(typeof(MutateType)))
            {
                IndividualMutator.ChooseMutatorType((MutateType)mutateType);
                Assert.AreEqual(Individual.ToString(), Algorithm.ToString());
            }
        }

        [TestMethod]
        public void TestMutatesWhenPopulationSizeIsOne()
        {
            foreach (var mutateType in Enum.GetValues(typeof(MutateType)))
            {
                _ = new MutateFlags(false, false, false, false, true);
                Individual = new(Algorithm.BestIndividual);
                IndividualMutator.ChooseMutatorType((MutateType)mutateType);
                Individual.Mutate(true);

                Assert.AreNotEqual(Individual.ToString(), Algorithm.ToString());
            }
        }


    }
}
