using System.Collections.Generic;
using System.Linq;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class IndividualFitnessCalculatorTest : ABaseClassTest
    {

        [TestMethod]
        public void TestCalculateFitness()
        {
            List <int> Genes = new (Enumerable.Range(0, N));
            Assert.IsTrue(Genes.CalculateFitness() > 0);

            Genes = new List<int>(Enumerable.Range(0, Mirror.SmallerN));
            Assert.IsTrue(Genes.CalculateFitness() > 0);

        }

        [TestMethod]
        public void TestCalculateFitness2()
        {
            int Fitness = Individual.ReturnCalculatedFitness();
            Assert.AreEqual(Individual.Fitness, Fitness);
        }


    }
}
