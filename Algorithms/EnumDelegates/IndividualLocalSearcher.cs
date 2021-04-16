using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class IndividualLocalSearcher
    {
        private static Action<Individual, int, int> searcher = new(FastLocalSearch);

        public static void LocalSearch(this Individual individual, bool on = true)
        {
            if (on)
            {
                if (individual.Genes.Count == Individual.N)
                {
                    searcher(individual, Individual.N, Individual.M);

                }
                else if (individual.Genes.Count == Mirror.SmallerN)
                {
                    searcher(individual, Mirror.SmallerN, Mirror.SmallerN);
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
                _ => throw new ArgumentException("Wrong LocalSearchType"),
            };
        }

        private static void FastLocalSearch(this Individual individual, int n, int m)
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
        private static void BestLocalSearch(Individual individual, int n, int m)
        {
            int currentFitness = individual.Fitness;
            while (true)
            {
                FastLocalSearch(individual, n, m);
                if (currentFitness > individual.Fitness)
                    currentFitness = individual.Fitness;
                else break;
            }
        }
    }
}
