using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualMutatorTest : ABaseClassTest
    {
        [TestMethod]
        public void TestMutate()
        {
            foreach (var mutateType in Enum.GetValues(typeof(MutateType)))
            {
                Individual = new(Individual2);
                IndividualMutator.ChooseMutatorType((MutateType)mutateType);
                Individual.Mutate(true);

                Assert.AreNotEqual(Individual.ToString(), Individual2.ToString());
            }
        }
    }
}
