using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class AlgorithmTest
    {
        int N1;
        int N2;
        int M;
        int PopulationSize;

        int OldPopulationSize;
        int CrossoverPopulationSize;
        int NewPopulationSize;
        int MutateChance;

        public Algorithm Algorithm;

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 5;
            N2 = 5;
            M = 3;
            PopulationSize = 1;

            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
            MutateChance = 10;
        }

        [TestMethod]
        public void TestN1()
        {
            Algorithm = new Algorithm(-10, N2, M, PopulationSize);
            Assert.AreEqual(Individual.N1, 2);
            Algorithm = new Algorithm(10, N2, M, PopulationSize);
            Assert.AreEqual(Individual.N1, 10);
            Algorithm = new Algorithm(1000, N2, M, PopulationSize);
            Assert.AreEqual(Individual.N1, 64);
        }

        [TestMethod]
        public void TestN2()
        {
            Algorithm = new Algorithm(N1, -10, M, PopulationSize);
            Assert.AreEqual(Individual.N2, 2);
            Algorithm = new Algorithm(N1, 10, M, PopulationSize);
            Assert.AreEqual(Individual.N2, 10);
            Algorithm = new Algorithm(N1, 1000, M, PopulationSize);
            Assert.AreEqual(Individual.N2, 64);
        }

        [TestMethod]
        public void TestM()
        {
            Algorithm = new Algorithm(N1, N2, -10, PopulationSize);
            Assert.AreEqual(Individual.M, 1);
            Algorithm = new Algorithm(N1, N2, 10, PopulationSize);
            Assert.AreEqual(Individual.M, 10);
            Algorithm = new Algorithm(N1, N2, 1000, PopulationSize);
            Assert.AreEqual(Individual.M, N1 * N2 / 2);
        }

        [TestMethod]
        public void TestPopulationSize()
        {
            Algorithm = new Algorithm(N1, N2, M, -10);
            Assert.AreEqual(Algorithm.PopulationSize, 1);
            Algorithm = new Algorithm(N1, N2, M, 10);
            Assert.AreEqual(Algorithm.PopulationSize, 10);
            Algorithm = new Algorithm(N1, N2, M, 1000);
            Assert.AreEqual(Algorithm.PopulationSize, 128);
        }

        [TestMethod]
        public void TestOldPopulationSize()
        {
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, -10, CrossoverPopulationSize, NewPopulationSize, MutateChance); 
            Assert.AreEqual(Algorithm.OldPopulationSize, 0);
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, 5, CrossoverPopulationSize, NewPopulationSize, MutateChance);
            Assert.AreEqual(Algorithm.OldPopulationSize, 5);
            Assert.AreEqual(Algorithm.CrossoverPopulationSize, 85);

            Algorithm = new Algorithm(N1, N2, M, PopulationSize, 1000, CrossoverPopulationSize, NewPopulationSize, MutateChance); 
            Assert.AreEqual(Algorithm.OldPopulationSize, 10);

            Algorithm = new Algorithm(N1, N2, M, PopulationSize, 95, 3, 2, MutateChance);
            Assert.AreEqual(Algorithm.OldPopulationSize, 95);
        }

        [TestMethod]
        public void TestCrossoverPopulationSize()
        {
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, -100, NewPopulationSize, MutateChance);
            Assert.AreEqual(Algorithm.CrossoverPopulationSize, 80);
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, 80, NewPopulationSize, MutateChance);
            Assert.AreEqual(Algorithm.CrossoverPopulationSize, 80);
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, 1000, NewPopulationSize, MutateChance);
            Assert.AreEqual(Algorithm.CrossoverPopulationSize, 80);

            Algorithm = new Algorithm(N1, N2, M, PopulationSize, 3, 95, 2, MutateChance);
            Assert.AreEqual(Algorithm.CrossoverPopulationSize, 95);
        }

        [TestMethod]
        public void TestNewPopulationSize()
        {
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, -10, MutateChance);
            Assert.AreEqual(Algorithm.NewPopulationSize, 0);
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, 5, MutateChance);
            Assert.AreEqual(Algorithm.NewPopulationSize, 5);
            Assert.AreEqual(Algorithm.CrossoverPopulationSize, 85);
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, 1000, MutateChance);
            Assert.AreEqual(Algorithm.NewPopulationSize, 10);

            Algorithm = new Algorithm(N1, N2, M, PopulationSize, 3, 2, 95, MutateChance);
            Assert.AreEqual(Algorithm.NewPopulationSize, 95);
        }

        [TestMethod]
        public void TestMutateChance()
        {
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize, -10);
            Assert.AreEqual(Algorithm.MutateChance, 0);
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize, 10);
            Assert.AreEqual(Algorithm.MutateChance, 10);
            Algorithm = new Algorithm(N1, N2, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize, 1000);
            Assert.AreEqual(Algorithm.MutateChance, 100);
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual(new Individual(new List<int>() { 0,1,2},0).ToString(), "Genes: 0 1 2 | Fitness: 0");
        }

        [TestMethod]

        public void TestNext()
        {
            //Individual individual = new(Algorithm.);
            //Algorithm.Next(10, 0);

        }

    }
}
