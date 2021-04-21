using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class IndividualLocalSearcher
    {
        private static readonly Random Random = new ();

        private static Action<Individual, int, int, int> searcher = new(FastLocalSearch);


        public static void LocalSearch(this Individual individual, bool? on)
        {
            if ((bool)on)
            {
                if (individual.Genes.Count == Individual.N)
                {
                    searcher(individual, Individual.N, Individual.N2, Individual.M);

                }
                else if (individual.Genes.Count == Mirror.SmallerN)
                {
                    searcher(individual, Mirror.SmallerN, Mirror.SmallerN2, Mirror.SmallerN);
                }
                else
                {
                    throw new ArgumentException("Wrong individual.Genes.Count");
                }
                individual.CalculateFitness();
            }
        }

        public static void ChooseLocalSearcherType(LocalSearchType localSearchType)
        {
            searcher = localSearchType switch
            {
                LocalSearchType.Fast => new(FastLocalSearch),
                LocalSearchType.Best => new(BestLocalSearch),
                LocalSearchType.NearBest => new (NearBestLocalSearch),
                LocalSearchType.NearMutateBest => new (NearMutateBestLocalSearch),
                _ => throw new ArgumentException("Wrong LocalSearchType"),
            };
        }

        private static void FastLocalSearch(this Individual individual, int n, int n2, int m)
        {
            Individual currentIndividual = new(individual);
            int localFitness;

            for (int blackIndex = 0; blackIndex < m; blackIndex++)
            {
                for (int whiteIndex = m; whiteIndex < n; whiteIndex++)
                {
                    currentIndividual.Genes.Swap(blackIndex, whiteIndex);
                    localFitness = currentIndividual.ReturnCalculatedFitness();

                    if (localFitness > currentIndividual.Fitness)
                        currentIndividual.Genes.Swap(blackIndex, whiteIndex);
                    else currentIndividual.Fitness = localFitness;
                }
            }

            individual.Genes = currentIndividual.Genes;
        }
        private static void BestLocalSearch(Individual individual, int n, int n2, int m)
        {
            int currentFitness = individual.Fitness;
            while (true)
            {
                FastLocalSearch(individual, n, n2, m);
                if (currentFitness > individual.Fitness)
                    currentFitness = individual.Fitness;
                else break;
            }
        }

        private static void NearBestLocalSearch(Individual individual, int n, int n2, int m)
        {
            Individual currentIndividual = new(individual);
            int localFitness = individual.Fitness;

            List<int> mutuableVariations;


            for (int i = 0; i < m; ++i)
            {
                mutuableVariations = new List<int>(individual.FindMutableGenes(i,n,n2));
                localFitness.FindBestIndividual(individual, currentIndividual, mutuableVariations, i);

            }

            individual.Genes = currentIndividual.Genes;
        }

        private static void NearMutateBestLocalSearch(Individual individual, int n, int n2, int m)
        {
            Individual currentIndividual = new(individual);
            int localFitness;

            List<int> mutuableVariations;

            for (int i = 0; i < Individual.M; ++i)
            {
                mutuableVariations = new List<int>(individual.FindMutableGenes(i,n,n2));

                if (mutuableVariations.Count>0)
                {
                    int randomIndex = Random.Next(mutuableVariations.Count);
                    int mutuableVariationindex = individual.Genes.FindIndex(x => { return x == mutuableVariations[randomIndex]; });
                    mutuableVariations.RemoveAt(randomIndex);

                    currentIndividual.Genes.Swap(i, mutuableVariationindex);
                    localFitness = currentIndividual.ReturnCalculatedFitness();
                    localFitness.FindBestIndividual(individual, currentIndividual, mutuableVariations, i);
                }
            }

            individual.Genes = currentIndividual.Genes;
        }

        

        private static void FindBestIndividual(this ref int localFitness, Individual individual, Individual currentIndividual, List<int> mutuableVariations, int i)
        {
            for (int j = 0; j < mutuableVariations.Count; ++j)
            {
                int mutuableVariationindex = individual.Genes.FindIndex(x => { return x == mutuableVariations[j]; });
                currentIndividual.Genes.Swap(i, mutuableVariationindex);
                localFitness = currentIndividual.ReturnCalculatedFitness();

                if (localFitness > currentIndividual.Fitness)
                    currentIndividual.Genes.Swap(i, mutuableVariationindex);
                else currentIndividual.Fitness = localFitness;
            }

        }
    }
}
