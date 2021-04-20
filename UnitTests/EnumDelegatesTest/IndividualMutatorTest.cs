using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualMutatorTest
    {
        int N1;
        int N2;
        int M;
        int PopulationSize;
        Algorithm Algorithm;
        public Individual Individual;

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 2;
            N2 = 2;
            M = 1;
            PopulationSize = 1;

            
        }

        [TestMethod]
        public void TestPlaceboMutate()
        {
            foreach (var mutateType in Enum.GetValues(typeof(MutateType)))
            {
                Algorithm = new Algorithm(N1, N2, M, PopulationSize);
                Individual = new(Algorithm.BestIndividual);
                IndividualMutator.ChooseMutatorType((MutateType)mutateType);
                Assert.AreEqual(Individual.ToString(), Algorithm.ToString());
            }
        }

        [TestMethod]
        public void TestMutatesWhenPopulationSizeIsOne()
        {
            foreach (var mutateType in Enum.GetValues(typeof(MutateType)))
            {
                Algorithm = new Algorithm(N1, N2, M, PopulationSize);
                _ = new MutateFlags(false, false, false, false, true);
                Individual = new(Algorithm.BestIndividual);
                IndividualMutator.ChooseMutatorType((MutateType)mutateType);
                Individual.Mutate(true);

                Assert.AreNotEqual(Individual.ToString(), Algorithm.ToString());
            }
        }


    }
}
