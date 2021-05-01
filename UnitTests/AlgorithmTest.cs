using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class AlgorithmTest : ABaseClassTest
    {
        [TestMethod]
        public void TestNext()
        {
            Assert.AreEqual(Algorithm.Population[0].ToString(), Individual.ToString());
            Algorithm.Next(1000, 0);
            Assert.AreNotEqual(Algorithm.Population[0].ToString(), Individual.ToString());
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreNotEqual("Genes: 0 1 2 3 4 5 6 7 | Fitness: 0", Individual.ToString());
            Algorithm.Population[0].Genes = new List<int>(Enumerable.Range(0,Individual.N)) ;
            Algorithm.Population[0].Fitness = 0;
            Assert.AreEqual("Genes: 0 1 2 3 4 5 6 7 | Fitness: 0", Algorithm.Population[0].ToString());
        }

    }
}
