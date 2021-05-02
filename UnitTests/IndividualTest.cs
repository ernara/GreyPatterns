using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class IndividualTest :ABaseClassTest
    {
     
        [TestMethod]
        public void TestSetUpParameters()
        {
            Assert.AreEqual(N, Individual.N);
            Assert.AreEqual(M, Individual.M);
            Individual.SetUpParameters(N*2,M*2);
            Assert.AreEqual(N * 2, Individual.N);
            Assert.AreEqual(M * 2, Individual.M);
        }

        [TestMethod]
        public void TestToString()
        {
            string s = "";
            for (int i = 0; i < M; i++)
            {
                s += $"{i} ";
            }

            Assert.AreNotEqual($"Genes: {s}| Fitness: 0", Individual.ToString());
            Individual.Genes = new List<int>(Enumerable.Range(0, Individual.N));
            Individual.Fitness = 0;
            Assert.AreEqual($"Genes: {s}| Fitness: 0", Individual.ToString());
        }
    }
}
