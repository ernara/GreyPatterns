using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class IndividualRandomChooserTest
    {
        int N1;
        int N2;
        int M;
        int PopulationSize;
        Algorithm Algorithm;
        private Func<int, Individual> ChooseRandomIndividual;

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
            PopulationSize = 2;
        }

        [TestMethod]
        public void TestRandomChooses()
        {
            foreach (var randomChooseType in Enum.GetValues(typeof(RandomChooseType)))
            {
                Algorithm = new Algorithm(N1, N2, M, PopulationSize);
                ChooseRandomIndividual = IndividualRandomChooser.ChooseChooserType((RandomChooseType)randomChooseType);

                for (int i = 0; i < 100; ++i)
                {
                    Assert.AreNotEqual(ChooseRandomIndividual(0).ToString(), Algorithm.ToString());
                    Assert.AreEqual(ChooseRandomIndividual(1).ToString(), Algorithm.ToString());
                }
            }
        }


    }
}
