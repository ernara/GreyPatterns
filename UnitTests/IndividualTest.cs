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
            Assert.AreNotEqual("Genes: 0 1 2 3 4 5 6 7 | Fitness: 0", Individual.ToString());
            Individual.Genes = new List<int>(Enumerable.Range(0, Individual.N));
            Individual.Fitness = 0;
            Assert.AreEqual("Genes: 0 1 2 3 4 5 6 7 | Fitness: 0", Individual.ToString());
        }
    }
}
