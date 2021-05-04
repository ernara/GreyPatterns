using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class StraitTest : ABaseClassTest
    {
        [TestMethod]
        public void TestNextCombination()
        {
            Individual.Genes = new List<int>(Enumerable.Range(0, N).ToList());

            for (int i = M; i < N; i++)
            {
                Assert.AreEqual(i-1, Individual.Genes[M-1]);
                for (int j = Individual.M - 1; j >= 0; j--)
                {
                    if (Individual.Genes[j] + Individual.M - j < Individual.N)
                    {
                        Individual.Genes[j]++;
                        for (int k = j + 1; k < Individual.M; k++)
                        {
                            Individual.Genes[k] = Individual.Genes[k - 1] + 1;
                        }
                        break;
                    }
                }
            }

            Individual.Genes = Individual.Genes.Union(Enumerable.Range(0, Individual.N)).Distinct().ToList();
            Individual.CalculateFitness();
        }
    }
}
