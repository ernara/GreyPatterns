using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class LocalSearchFlagsTest
    {
        public int N1;
        public int N2;
        public int M;
        public Algorithm Algorithm;
        public Individual Individual;
        public Individual Individual2;


        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 16;
            N2 = 16;
            M = 16;
            Algorithm = new Algorithm(16, 16, 16,1);
            Individual = new(Algorithm.BestIndividual);
        }
        

        [TestMethod]
        public void TestLocalSearchFlags()
        {
            Algorithm = new Algorithm(16, 16, 16, 1, new MutateFlags(), new LocalSearchFlags(true));
            Individual2 = new Individual(Algorithm.BestIndividual);

            Assert.IsTrue(Individual.Fitness > Individual2.Fitness);

            _ = new LocalSearchFlags(false, true);
            Individual2 = new Individual(Individual);
            Individual2.MakeMirrored();
            Assert.IsTrue(Individual.Fitness > Individual2.Fitness);


            _ = new LocalSearchFlags(false, false, true);
            Individual2 = new Individual(Individual);
            Individual2.MakeMirrored();
            Assert.IsTrue(Individual.Fitness > Individual2.Fitness);


            _ = new LocalSearchFlags(false, false, false, true);
            Individual2 = new Individual(Individual);
            Individual2.Crossover(Individual2);
            Assert.IsTrue(Individual.Fitness > Individual2.Fitness);

        }
    }
}
