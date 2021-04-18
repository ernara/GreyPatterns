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
        public Algorithm Algorithm;
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
                Algorithm.Population[1].Fitness = 10000;
                Algorithm.Population[0].Fitness = 25000;

                ChooseRandomIndividual = IndividualRandomChooser.ChooseChooserType((RandomChooseType)randomChooseType);

                for (int i = 0; i < 100; ++i)
                {
                    //if (ChooseRandomIndividual(0).ToString() == Algorithm.ToString())
                    Assert.AreNotEqual(ChooseRandomIndividual(0).ToString(), Algorithm.Population[0].ToString());
                    Assert.AreEqual(ChooseRandomIndividual(1).ToString(), Algorithm.Population[0].ToString());
                }
            }
        }


    }
}
