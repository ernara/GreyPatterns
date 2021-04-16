using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                population[i].LocalSearch(LocalSearchFlags.PopulationCreation);
            }

            return population.OrderBy(individual => individual.Fitness).ToList();
        }
    }
}
