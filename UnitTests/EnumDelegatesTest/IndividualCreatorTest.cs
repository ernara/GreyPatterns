using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class IndividualCreatorTest
    {
        int N1;
        int N2;                               
        int M;
        int PopulationSize;
        public Algorithm Algorithm;
        Individual individual;


        [TestCleanup]
        public void TestCleanup()
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 3;
            N2 = 3;
            M = 3;
            PopulationSize = 1;

            Algorithm = new(N1, N2, M, PopulationSize);
        }

        [TestMethod]
        public void TestIndividualCreators()
        {
            foreach (var individualType in Enum.GetValues(typeof(IndividualType)))
            {
                IndividualCreator.ChooseCreatorType((IndividualType)individualType);
                individual = new Individual();
                Assert.AreEqual(N1 * N2, individual.Genes.Count);
                Assert.AreEqual(N1 * N2, individual.Genes.Distinct().Count());
            }
                
        }

    }
}
