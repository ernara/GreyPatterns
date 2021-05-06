using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public static class PopulationCreator
    {
        public static List<Individual> CreatePopulation()
        {
            List<Individual> population = new();

            for (int i = 0; i < Algorithm.PopulationSize; ++i)
            {
                population.Add(new Individual());
                population[i].Mutate(MutateFlags.PopulationCreation);
                population[i].LocalSearch(LocalSearchFlags.PopulationCreation);
            }

            return population.OrderBy(individual => individual.Fitness).ToList();
        }

        public static List<Individual> CreatePopulation(List<int> matrix)
        {
            List<Individual> population = new();

            for (int i = 0; i < Algorithm.PopulationSize; ++i)
            {
                population.Add(new Individual(matrix,matrix.CalculateFitness()));
            }

            return population;
        }
    }
}
