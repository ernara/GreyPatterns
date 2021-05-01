using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class IndividualMutator
    {
        private readonly static Random Random = new();
        private static Action<Individual, int, int, int> mutator = new(RandomMutate);

        public static void Mutate(this Individual individual, bool on)
        {
            if (on)
            {
                if (individual.Genes.Count == Individual.N)
                {
                    mutator(individual, Individual.N, Individual.N2, Individual.M);
                }
                else if (individual.Genes.Count == Mirror.SmallerN)
                {
                    mutator(individual, Mirror.SmallerN, Mirror.SmallerN2, Mirror.SmallerM);
                }
                else
                {
                    throw new ArgumentException("Wrong individual.Genes.Count");
                }
                individual.CalculateFitness();
            }

        }

        public static void ChooseMutatorType(MutateType mutateType)
        {
            mutator = mutateType switch
            {
                MutateType.Random => new(RandomMutate),
                MutateType.Near => new(RandomNearMutate),
                MutateType.RandomAll => new(RandomAllMutate),
                MutateType.NearAll => new(NearAllMutate),
                _ => throw new ArgumentException("Wrong MutateType"),
            };


        }

        private static void RandomMutate(Individual individual, int n, int n2, int m)
        {
            individual.Genes.Swap(Random.Next(m), Random.Next(m,n));
        }

        private static void RandomNearMutate(Individual individual, int n, int n2, int m)
        {
            int randomGeneIndex = Random.Next(m); 
            List<int> mutuableVariations = new(individual.FindMutableGenes(randomGeneIndex,n,n2)); 
            
            if (mutuableVariations.Count > 0)
            {
                int mutuableVariation = mutuableVariations[Random.Next(mutuableVariations.Count)];
                int mutuableVariationindex = individual.Genes.FindIndex(x => { return x == mutuableVariation; });
                individual.Genes.Swap(randomGeneIndex, mutuableVariationindex);
            }

        }

        private static void RandomAllMutate(Individual individual, int n, int n2, int m)
        {
            for (int i = 0; i < m; i++)
            {
                individual.Genes.Swap(i, Random.Next(m, n - m));
            }
        }

        private static void NearAllMutate(Individual individual, int n, int n2, int m)
        {
            List<int> mutuableVariations;

            for (int i = 0; i < Individual.M; ++i)
            {
                mutuableVariations = new List<int>(individual.FindMutableGenes(i,n,n2));
                if (mutuableVariations.Count > 0)
                {
                    int mutuableVariation = mutuableVariations[Random.Next(mutuableVariations.Count)];
                    int mutuableVariationindex = individual.Genes.FindIndex(x => { return x == mutuableVariation; });
                    individual.Genes.Swap(i, mutuableVariationindex);
                }
            }

        }

        public static List<int> FindMutableGenes(this Individual Individual, int index, int n, int n2)
        {
            List<int> blackCells = new(Individual.Genes.Take(Individual.M));
            List<int> mutableCells = new();

            if (SameLine(Individual.Genes[index], blackCells[index] + 1,n2))
            {
                mutableCells.Add(blackCells[index] + 1);
                mutableCells.Add(blackCells[index] - n2 + 1);
                mutableCells.Add(blackCells[index] + n2 + 1);
            }

            if (SameLine(Individual.Genes[index], blackCells[index] - 1, n2))
            {
                mutableCells.Add(blackCells[index] - 1);
                mutableCells.Add(blackCells[index] - n2 - 1);
                mutableCells.Add(blackCells[index] + n2 - 1);
            }
                
            mutableCells.Add(blackCells[index] - n2);
            mutableCells.Add(blackCells[index] + n2);

            for (int i = 0; i < mutableCells.Count; ++i)
            {
                if (mutableCells[i] < 0 || mutableCells[i] >= n || blackCells.Contains(mutableCells[i]))
                {
                    mutableCells.RemoveAt(i);
                    i--;
                }
            }

            return mutableCells;
        }

        public static bool SameLine(int gene, int gene2, int n2)
        {
            return gene / n2 == gene2 / n2;
        }

    }
}
