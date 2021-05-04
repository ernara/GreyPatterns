using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class AlgorithmTest : ABaseClassTest
    {

        [TestMethod]
        public void TestCheckParameters()
        {
            int n = -10;
            int m = -10;
            FindMainParameters(ref n,ref m);
            Assert.AreEqual(4, n);
            Assert.AreEqual(1, m);

            n = N;
            m = M;
            FindMainParameters(ref n,ref m);
            Assert.AreEqual(N, n);
            Assert.AreEqual(M, m);

            n = int.MaxValue;
            m = int.MaxValue;
            FindMainParameters(ref n, ref m);
            Assert.AreEqual(4096, n);
            Assert.AreEqual(4096, m);

            int populationSize = 1;
            int oldPopulationSize = 0;
            int crossoverPopulationSize = 1;
            int newPopulationSize = 0;
            FindOptionalParameters(populationSize, ref oldPopulationSize, ref crossoverPopulationSize, ref newPopulationSize);
            Assert.AreEqual(1, oldPopulationSize);
            Assert.AreEqual(0, crossoverPopulationSize);
            Assert.AreEqual(0, newPopulationSize);

            populationSize = PopulationSize;
            oldPopulationSize = OldPopulationSize;
            crossoverPopulationSize = CrossoverPopulationSize;
            newPopulationSize = NewPopulationSize;
            FindOptionalParameters(populationSize, ref oldPopulationSize, ref crossoverPopulationSize, ref newPopulationSize);
            Assert.AreEqual(OldPopulationSize, oldPopulationSize);
            Assert.AreEqual(CrossoverPopulationSize, crossoverPopulationSize);
            Assert.AreEqual(NewPopulationSize, newPopulationSize);

            populationSize = 100;
            oldPopulationSize = 100;
            crossoverPopulationSize = 100;
            newPopulationSize = 100;
            FindOptionalParameters(populationSize, ref oldPopulationSize, ref crossoverPopulationSize, ref newPopulationSize);
            Assert.AreEqual(10, oldPopulationSize);
            Assert.AreEqual(80, crossoverPopulationSize);
            Assert.AreEqual(10, newPopulationSize);
        }

        private static void FindMainParameters(ref int n,ref int m)
        {
            n = n < 4 ? 4 : n > 4096 ? 4096 : (int)Math.Sqrt(n) * (int)Math.Sqrt(n) == n ? n : (int)((Math.Sqrt(n) + 1) * (int)(Math.Sqrt(n) + 1));
            m = m < 1 ? 1 : m > n ? n : m;
        }

        private static void FindOptionalParameters(int populationSize, ref int oldPopulationSize, ref int crossoverPopulationSize, ref int newPopulationSize)
        {
            oldPopulationSize = oldPopulationSize < 0 ? 0 : oldPopulationSize > populationSize ? populationSize : oldPopulationSize;
            newPopulationSize = newPopulationSize < 0 ? 0 : newPopulationSize > populationSize ? populationSize : newPopulationSize;
            crossoverPopulationSize = crossoverPopulationSize < 0 ? 0 : crossoverPopulationSize > populationSize ? populationSize : crossoverPopulationSize;

            if (populationSize == 1)
            {
                oldPopulationSize = 1;
                newPopulationSize = 0;
                crossoverPopulationSize = 0;
            }
            else if (oldPopulationSize + crossoverPopulationSize + newPopulationSize != populationSize)
            {
                oldPopulationSize = populationSize / 10;
                newPopulationSize = populationSize / 10;
                crossoverPopulationSize = populationSize - oldPopulationSize - newPopulationSize;
            }
        }

        [TestMethod]
        public void TestNext()
        {
            Assert.AreEqual(Algorithm.Population[0].ToString(), Individual.ToString());
            Algorithm.Next(Iterations,Time);
            Assert.AreNotEqual(Algorithm.Population[0].ToString(), Individual.ToString());
        }


        [TestMethod]
        public  void TestSaveBestIndividual()
        {
            Individual = new Individual(Algorithm.BestIndividual);
            Algorithm.Next(Iterations, Time);
            Assert.IsTrue(Algorithm.BestIndividual.Fitness < Individual.Fitness);
        }
    }
}
