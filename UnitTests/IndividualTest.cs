using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class IndividualTest
    {
        int N1;
        int N2;
        int M;
        Algorithm Algorithm;

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
            Algorithm = new(N1, N2, M, 1);
        }

        [TestMethod]
        public void TestParametersComesFromAlgorithm()
        {
            Assert.AreEqual(Individual.N1, N1);
            Assert.AreEqual(Individual.N2, N2);
            Assert.AreEqual(Individual.N, N1 * N2);
            Assert.AreEqual(Individual.M, M);
        }

        [TestMethod]
        public void TestSetUpParameters()
        {
            Individual.SetUpParameters(N1 * 2, N2 * 2, M * 2);
            Assert.AreEqual(Individual.N1, N1 * 2);
            Assert.AreEqual(Individual.N2, N2 * 2);
            Assert.AreEqual(Individual.N, N1 * N2 * 4);
            Assert.AreEqual(Individual.M, M * 2);

        }

        [TestMethod]
        public void TestSort()
        {

        }
    }
}
