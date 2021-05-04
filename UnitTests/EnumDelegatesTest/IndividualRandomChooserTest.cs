using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualRandomChooserTest : ABaseClassTest
    {

        [TestMethod]
        public void TestRandomChoose()
        {
            int FitnessSum = 0;

            for (int i = 0; i < Algorithm.Population.Count; i++)
            {
                Algorithm.Population[i].Fitness = i;
            }

            foreach (var randomChooseType in Enum.GetValues(typeof(RandomChooseType)))
            {
                var ChooseRandomIndividual = IndividualRandomChooser.ChooseChooserType((RandomChooseType)randomChooseType);

                for (int i = 0; i < 100; ++i)
                {
                    FitnessSum+= ChooseRandomIndividual().Fitness;
                }

                Assert.IsTrue(FitnessSum > 200);

            }
        }


    }
}
