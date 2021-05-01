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
        public void TestSetUpParameters()
        {
            IndividualFitnessCalculator.SetUpParameters();
        }

        [TestMethod]
        
        public void TestCalculateFitness()
        {
            List <int> Genes = new List<int>(Enumerable.Range(0, N));
            Assert.IsTrue(Genes.CalculateFitness() > 0);

            Genes = new List<int>(Enumerable.Range(0, Mirror.SmallerN));
            Assert.IsTrue(Genes.CalculateFitness() > 0);

            //Genes = new List<int>(Enumerable.Range(0,1));
            //Assert.ThrowsException(   (List<int>) => Genes.CalculateFitness()    );
            //Assert.ThrowsException(   (List<int>)=>  IndividualFitnessCalculator.CalculateFitness(Genes)      );
            
            
        }

        //[TestMethod]
        //public int CalculateFitness(this List<int> genes, int m, DistancesMatrix distancesMatrix)
        //{
        //    int fitness = 0;

        //    for (int i = 0; i < m - 1; ++i)
        //    {
        //        for (int j = i + 1; j < m; j++)
        //        {
        //            fitness += distancesMatrix[genes[i], genes[j]];
        //        }
        //    }

        //    return fitness * 2;
        //}

        //[TestMethod]
        //public void CalculateFitness(this Individual individual)
        //{
        //    individual.Fitness = individual.ReturnCalculatedFitness();
        //}

        //public int ReturnCalculatedFitness(this Individual individual)
        //{
        //    return individual.Genes.CalculateFitness();
        //}


    }
}
