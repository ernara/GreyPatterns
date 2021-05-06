using System.Linq;

namespace Algorithms
{
    public class Strait : Algorithm
    {
        public Strait(int n, int m) : base(n, m)
        {

        }

        protected override void Do()
        {
            NextCombination();
        }

        private static void NextCombination()
        {
            for (int i = Individual.M - 1; i >= 0; i--)
            {
                if (Population[0].Genes[i] + Individual.M - i < Individual.N)
                {
                    Population[0].Genes[i]++;
                    for (int j = i + 1; j < Individual.M; j++)
                    {
                        Population[0].Genes[j] = Population[0].Genes[j - 1] + 1;
                    }
                    break;
                }
            }

            Population[0].Genes = Population[0].Genes.Union(Enumerable.Range(0, Individual.N)).Distinct().ToList();
            Population[0].CalculateFitness();
        }
    }
}
