using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class IndividualCrossoverer
    {
        private readonly static Random Random = new();
        private static Action<Individual,Individual> crossoverer = new(RandomCrossover);

        public static void Crossover(this Individual individual, Individual individual2)
        {
            crossoverer(individual, individual2);
            individual.CalculateFitness();

            if (LocalSearchFlag.Crossover)
                individual.LocalSearch();

            individual.CalculateFitness();
        }

        public static void ChooseCrossoverType(CrossoverType crossoverType)
        {
            crossoverer = crossoverType switch
            {
                CrossoverType.Random => new(RandomCrossover),
                CrossoverType.Part => new(PartCrossover),
                
                _ => throw new ArgumentException("Wrong CrossoverType"),
            };
        }

        private static void RandomCrossover(Individual individual, Individual individual2)
        {
            List<int> blackCells = individual.Genes.Take(Individual.M)
                .Union(individual2.Genes.Take(Individual.M))
                .Distinct().OrderBy(g => Random.Next(Individual.M)).ToList();

            individual.Genes = new List<int>(blackCells.Union(individual.Genes).Distinct().ToList());
        }

        private static void PartCrossover(Individual individual, Individual individual2)
        {
            var blackCells = individual.Genes.Take(Individual.M).OrderBy(g => g).Take(Individual.M / 2)
                .Union(individual2.Genes.Take(Individual.M).OrderByDescending(g => g).Take(Individual.M / 2))
                .Distinct().ToList();

            individual.Genes = new List<int>(blackCells.Union(individual.Genes).Distinct().ToList());
        }
    }
}
