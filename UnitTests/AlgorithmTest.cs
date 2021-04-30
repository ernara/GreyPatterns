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
            Algorithm.Next(1000, 0);
            Assert.AreNotEqual(Algorithm.Population[0].ToString(), Individual.ToString());
        }

        [TestMethod]
        public void TestToString()
        {
            Individual.Genes = new List<int>(Enumerable.Range(0,Individual.N)) ;
            Individual.Fitness = 0;
            Assert.AreEqual("Genes: 0 1 2 3 4 5 6 7 | Fitness: 0", Individual.ToString() );
        }

    }
}
