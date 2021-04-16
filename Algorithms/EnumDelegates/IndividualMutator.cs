using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class IndividualMutator
    {
        private readonly static Random Random = new();
        private static Action<Individual> mutator = new(RandomMutate);

        public static void Mutate(this Individual individual, bool on = true)
        {
            if (on)
            {
                mutator(individual);
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

        private static void RandomMutate(Individual individual)
        {
            individual.Genes.Swap(Random.Next(Individual.M), Random.Next(Individual.M, Individual.N - Individual.M));
        }

        private static void RandomNearMutate(Individual individual)
        {
            int randomGeneIndex = Random.Next(Individual.M); 
            List<int> mutuableVariations = new(individual.FindMutableGenes(randomGeneIndex)); 
            
            if (mutuableVariations.Count > 0)
            {
                int mutuableVariation = mutuableVariations[Random.Next(mutuableVariations.Count)];
                int mutuableVariationindex = individual.Genes.FindIndex(x => { return x == mutuableVariation; });
                individual.Genes.Swap(randomGeneIndex, mutuableVariationindex);
            }

        }

        private static void RandomAllMutate(Individual individual)
        {
            for (int i = 0; i < Individual.M; i++)
            {
                individual.Genes.Swap(i, Random.Next(Individual.M, Individual.N - Individual.M));
            }
        }

        private static void NearAllMutate(Individual individual)
        {
            List<int> mutuableVariations;

            for (int i = 0; i < Individual.M; ++i)
            {
                mutuableVariations = new List<int>(individual.FindMutableGenes(i));
                if (mutuableVariations.Count > 0)
                {
                    int mutuableVariation = mutuableVariations[Random.Next(mutuableVariations.Count)];
                    int mutuableVariationindex = individual.Genes.FindIndex(x => { return x == mutuableVariation; });
                    individual.Genes.Swap(i, mutuableVariationindex);
                }
            }

        }

        private static List<int> FindMutableGenes(this Individual Individual, int index)
        {
            List<int> blackCells = new(Individual.Genes.Take(Individual.M));
            List<int> mutableCells = new();

            mutableCells.Add(blackCells[index] + 1);
            mutableCells.Add(blackCells[index] - 1);
            mutableCells.Add(blackCells[index] - Individual.N2 + 1);
            mutableCells.Add(blackCells[index] + Individual.N2 + 1);
            mutableCells.Add(blackCells[index] - Individual.N2 - 1);
            mutableCells.Add(blackCells[index] + Individual.N2 - 1);
            mutableCells.Add(blackCells[index] - Individual.N2);
            mutableCells.Add(blackCells[index] + Individual.N2);

            for (int i = 0; i < mutableCells.Count; ++i)
            {
                if (mutableCells[i] < 0 || mutableCells[i] >= Individual.N || blackCells.Contains(mutableCells[i]) 
                    || !SameLine(Individual.Genes[index],mutableCells[i]))
                {
                    mutableCells.RemoveAt(i);
                    i--;
                }
            }

            return mutableCells;
        }

        public static bool SameLine(int gene, int gene2)
        {
            return gene / Individual.N2 == gene2 / Individual.N2;
        }

    }
}
