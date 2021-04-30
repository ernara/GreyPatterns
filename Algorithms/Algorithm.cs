using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
    public class Algorithm
    {
        public static int PopulationSize { get; private set; }
        public static int OldPopulationSize { get; private set; }
        public static int CrossoverPopulationSize { get; private set; }
        public static int NewPopulationSize { get; private set; }
        public static int MutateChance { get; private set; }
        public static int LocalSearchChance { get; private set; }

        public static List<Individual> Population { get; private set; }
        public static Individual BestIndividual { get; protected set; }

        private Func<Individual> ChooseRandomIndividual;

        public Algorithm(int n, int m, int populationSize,
            int oldPopulationSize, int crossoverPopulationSize, int newPopulationSize, 
            IndividualType individualType, CrossoverType crossoverType, RandomChooseType randomChooseType, 
            MutateType mutateType, LocalSearchType localSearchType , MirrorType mirrorType,
            int mutateChance, int localSearchChance, MutateFlags mutateFlags, LocalSearchFlags localSearchFlags)
        {
            SetUpParameters(n, m, populationSize, oldPopulationSize, crossoverPopulationSize, newPopulationSize);
            SetUpDelegates(individualType, crossoverType, randomChooseType, mutateType, localSearchType, mirrorType);
            SetUpChancesAndFlags(mutateChance, localSearchChance, mutateFlags, localSearchFlags);

            Population = PopulationCreator.CreatePopulation();
            BestIndividual = new(Population[0]);
        }

        public Algorithm(int n, int m)
        {
            n = n < 4 ? 4 : n > 4096 ? 4096 : Math.Sqrt(n) * Math.Sqrt(n) == n ? n : (int)((Math.Sqrt(n) + 1) * (Math.Sqrt(n) + 1));
            m = m < 1 ? 1 : m > n ? n : m;

            Population = new List<Individual>();
            Individual.SetUpParameters(n, m);

            List<int> genes = Enumerable.Range(0, n).ToList();

            Population.Add(new Individual(genes, genes.CalculateFitness()));
            BestIndividual = new Individual(Population[0]);
        }

        private static void SetUpParameters(int n, int m, int populationSize,
           int oldPopulationSize, int crossoverPopulationSize, int newPopulationSize)
        {
            CheckParameters(ref n, ref m, ref populationSize, ref oldPopulationSize, ref crossoverPopulationSize, ref newPopulationSize);
            Individual.SetUpParameters(n, m);

            PopulationSize = populationSize;
            OldPopulationSize = oldPopulationSize;
            CrossoverPopulationSize = crossoverPopulationSize;
            NewPopulationSize = newPopulationSize;
        }

        private void SetUpDelegates(IndividualType individualType, CrossoverType crossoverType, RandomChooseType chooseType,
            MutateType mutateType, LocalSearchType localSearchType, MirrorType mirrorType)
        {
            IndividualCreator.ChooseCreatorType(individualType);
            IndividualCrossoverer.ChooseCrossoverType(crossoverType);
            ChooseRandomIndividual = IndividualRandomChooser.ChooseChooserType(chooseType);

            IndividualMutator.ChooseMutatorType(mutateType);
            IndividualLocalSearcher.ChooseLocalSearcherType(localSearchType);
            Mirror.ChooseMirrorType(mirrorType);
        }

        private static void CheckParameters(ref int n, ref int m, ref int populationSize,
            ref int oldPopulationSize, ref int crossoverPopulationSize, ref int newPopulationSize)
        {
            n = n < 4 ? 4 : n > 4096 ? 4096 : (int)Math.Sqrt(n) * (int)Math.Sqrt(n) == n ? n : (int)((Math.Sqrt(n) + 1) * (int)(Math.Sqrt(n) + 1));
            m = m < 1 ? 1 : m > n ? n : m;

            populationSize = populationSize < 1 ? 1 : populationSize > 128 ? 128 : populationSize;

            oldPopulationSize = oldPopulationSize < 0 ? 0 : oldPopulationSize > populationSize ? populationSize : oldPopulationSize;
            newPopulationSize = newPopulationSize < 0 ? 0 : newPopulationSize > populationSize ? populationSize : newPopulationSize;
            crossoverPopulationSize = crossoverPopulationSize < 0 ? 0 : crossoverPopulationSize > populationSize ? populationSize : crossoverPopulationSize;

            if (PopulationSize == 1)
            {
                oldPopulationSize = 1;
                newPopulationSize = 0;
                crossoverPopulationSize = 0;
            }
            else if (oldPopulationSize + crossoverPopulationSize + newPopulationSize != populationSize)
            {
                oldPopulationSize = PopulationSize / 10;
                newPopulationSize = PopulationSize / 10;
                crossoverPopulationSize = PopulationSize - oldPopulationSize - newPopulationSize;
            }
        }

        private static void SetUpChancesAndFlags(int mutateChance, int localSearchChance, MutateFlags mutateFlags, LocalSearchFlags localSearchFlags)
        {
            MutateChance = mutateChance;
            LocalSearchChance = localSearchChance;
            _ = new MutateFlags(MutateFlags.PopulationCreation, MutateFlags.Crossover, MutateFlags.SmallMirror, MutateFlags.BigMirror);
            _ = new LocalSearchFlags(LocalSearchFlags.PopulationCreation, LocalSearchFlags.Crossover, LocalSearchFlags.SmallMirror, LocalSearchFlags.BigMirror);
        }


        public void Next()
        {
            Next(1, 0);
        }

        public void Next(int generations, int time)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            for (int i = 0; i < generations || stopwatch.ElapsedMilliseconds < time * 1000; i++)
            {
                Do();
                SaveBestIndividual();
            }
        }

        private static void SaveBestIndividual()
        {
            Population = Population.OrderBy(i => i.Fitness).ToList();

            if (Population[0].Fitness < BestIndividual.Fitness)
            {
                BestIndividual = new Individual(Population[0]);
            }
        }

        protected virtual void Do()
        {
            List<Individual> NewPopulation = new(Population.Take(OldPopulationSize));

            Individual child;

            for (int i = 0; i < CrossoverPopulationSize; ++i)
            {
                child = ChooseRandomIndividual();

                child.Crossover(ChooseRandomIndividual());

                NewPopulation.Add(child);
            }

            for (int i = 0; i < NewPopulationSize; ++i)
            {
                NewPopulation.Add(new Individual());
            }

            Population = NewPopulation;

            if (Population.Count == 1)
            {
                Population[0].Mutate(MutateFlags.PopulationSizeIsOne);
                Population[0].LocalSearch(LocalSearchFlags.PopulationSizeIsOne);
            }
        }

        public override string ToString()
        {
            return BestIndividual.ToString();
        }

    }
}
