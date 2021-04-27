using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Strait : Algorithm
    {
        public Strait(int n1, int n2, int m, int populationSize, int oldPopulationSize = 10, int crossoverPopulationSize = 80, int newPopulationSize = 10, int mutateChance = 100, LocalSearchType localSearchType = LocalSearchType.Fast, IndividualType individualType = IndividualType.Random, MirrorType mirrorType = MirrorType.Best, RandomChooseType randomChooseType = RandomChooseType.Random, CrossoverType crossoverType = CrossoverType.Random, MutateType mutateType = MutateType.Random) : base(n1, n2, m, populationSize, oldPopulationSize, crossoverPopulationSize, newPopulationSize, mutateChance, localSearchType, individualType, mirrorType, randomChooseType, crossoverType, mutateType)
        {
            Population[0].Genes.Sort();
            Population[0].CalculateFitness();
            BestIndividual = new Individual(Population[0]);
        }

        public override void Do()
        {
            NextCombination();
        }

        public static void NextCombination()
        {
            for (int i = Individual.M-1; i >= 0; i--)
            {
                if (Population[0].Genes[i] + Individual.M - i < Individual.N)
                {
                    Population[0].Genes[i]++; //1 2 4 9 9  //1 2 5 6 7
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

        //for (int i = Genes.Count-1;i>=0;i--)
        //    {
        //        if (Genes[i]+M-i<N)
        //        {
        //            Genes[i]++;
        //            for (int j = i+1; j<Genes.Count; j++)
        //            {
        //                Genes[j] = Genes[j - 1]+1;
        //            }
        //            break;
        //        }

        //    }
    }
}
