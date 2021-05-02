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
            string s = "";
            for (int i = 0; i < M; i++)
            {
                s += $"{i} ";
            }

            Assert.AreNotEqual($"Genes: {s}| Fitness: 0", Algorithm.Population[0].ToString());
            Algorithm.Population[0].Genes = new List<int>(Enumerable.Range(0,Individual.N)) ;
            Algorithm.Population[0].Fitness = 0;
            Assert.AreEqual($"Genes: {s}| Fitness: 0", Algorithm.Population[0].ToString());
        }

    }
}
