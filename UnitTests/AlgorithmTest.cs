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
            Assert.AreEqual(Algorithm.PopulationSize, 64);
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual(new Individual(new List<int>() { 0,1,2},0).ToString(), "Genes: 0 1 2 | Fitness: 0");
        }
    }
}
