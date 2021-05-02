using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class IndividualCreatorTest : ABaseClassTest
    {
        
        [TestMethod]
        public void TestIndividualCreators()
        {
            foreach (var individualType in Enum.GetValues(typeof(IndividualType)))
            {
                IndividualCreator.ChooseCreatorType((IndividualType)individualType);
                Individual = new Individual();
                Assert.AreEqual(N, Individual.Genes.Count);
                Assert.AreEqual(N, Individual.Genes.Distinct().Count());
            }

        }

        [TestMethod]

        public void TestCreateMirrorIndividual()
        {
            foreach (var mirrorType in Enum.GetValues(typeof(MirrorType)))
            {
                IndividualCreator.ChooseCreatorType(IndividualType.Mirror);
                Mirror.ChooseMirrorType((MirrorType)mirrorType);

                List<int> SmallGenes = TestCreateRandomIndividual().Take(Mirror.SmallerM).ToList();
                SmallGenes.Sort();
                Assert.IsTrue(SmallGenes[^1] < Mirror.SmallerN);

                List<int> BigGenes = new();

                for (int i = 0; i < SmallGenes.Count; i++)
                {
                    BigGenes.AddRange(SmallGenes[i].ChangeToBiggerNumbers());
                    BigGenes.Sort();
                    Assert.IsTrue(BigGenes[^1] < Individual.N);
                }

                List<int> Genes = BigGenes.Union(Enumerable.Range(0, Individual.N)).Distinct().ToList();
                Assert.AreEqual(Individual.N, Genes.Count);
            }
        }

        [TestMethod]
        private List<int> TestCreateRandomIndividual()
        {
            List<int> Genes = Enumerable.Range(0, Individual.N).OrderBy(g => Random.Next(Individual.N)).ToList();
            Assert.AreEqual(Individual.N, Genes.Count);
            return Genes;
        }

    }
}
