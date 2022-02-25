using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public static class IndividualCrossoverer
    {
        private readonly static Random Random = new();
        private static Action<Individual, Individual> crossoverer = new(RandomCrossover);

        public static void Crossover(this Individual individual, Individual individual2)
        {
            crossoverer(individual, individual2);
            individual.CalculateFitness();

            individual.Mutate(MutateFlags.Crossover);
            individual.LocalSearch(LocalSearchFlags.Crossover);

            individual.CalculateFitness();
        }

        public static void ChooseCrossoverType(CrossoverType crossoverType)
        {
            crossoverer = crossoverType switch
            {
                CrossoverType.OnePoint => new (OnePointCrossover),
                CrossoverType.TwoPoint => new (TwoPointCrossover),
                CrossoverType.Random => new(RandomCrossover),
                CrossoverType.Part => new(PartCrossover),

                _ => throw new ArgumentException("Wrong CrossoverType"),
            };
        }

        private static void OnePointCrossover(Individual individual, Individual individual2)
        {
            int cutPoint = Random.Next(Individual.M);
            List<int> blackCells = individual.Genes.Take(cutPoint)
                .Union(individual2.Genes.Take(Individual.M-cutPoint))
                .Distinct().ToList();

            individual.Genes = new List<int>(blackCells.Union(individual.Genes).Distinct().ToList());
        }

        private static void TwoPointCrossover(Individual individual, Individual individual2)
        {
            int cutPoint = Random.Next(Individual.M);
            int cutPoint2 = Random.Next(cutPoint, Individual.M);
           
            List<int> blackCells = individual.Genes.Take(cutPoint)
            .Union(individual2.Genes.Skip(cutPoint).Take(cutPoint2-cutPoint))
            .Union(individual.Genes.Skip(cutPoint2).Take(Individual.M-cutPoint2))
            .Distinct().ToList();
            
            individual.Genes = new List<int>(blackCells.Union(individual.Genes).Distinct().ToList());
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

        //private static void MultiCrossover(Individual individual, Individual individual2)
        //{
        //    var firstIndividualCells = individual.Genes.Skip(Individual.M / 3 * 2).Take(Individual.M / 3);
        //    var secondIndividualCells = individual2.Genes.Take(Individual.M);
        //    var blackCells = firstIndividualCells.Take(Individual.M / 3).Union();
        //}
    }
}
