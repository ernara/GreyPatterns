﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Algorithm
    {
        public static int PopulationSize { get; private set; }
        public static int OldPopulationSize { get; private set; }
        public static int CrossoverPopulationSize { get; private set; }
        public static int NewPopulationSize { get; private set; }
        public static int MutateChance { get; private set; }

        public static List<Individual> Population { get; private set; }
        public Individual BestIndividual { get; private set; }

        private Func<int, Individual> ChooseRandomIndividual;

        public Algorithm(int n1, int n2, int m, int populationSize,
            MutateFlags mutateFlags,
            LocalSearchFlags localSearchFlags,
            int oldPopulationSize = 10, int crossoverPopulationSize = 80, int newPopulationSize = 10, int mutateChance = 10,

            LocalSearchType localSearchType = LocalSearchType.Fast,
            IndividualType individualType = IndividualType.Random,
            MirrorType mirrorType = MirrorType.Best,
            RandomChooseType randomChooseType = RandomChooseType.Random,
            CrossoverType crossoverType = CrossoverType.Random,
            MutateType mutateType = MutateType.Random
            )
        {
            SetUpParameters(n1, n2, m, populationSize, oldPopulationSize, crossoverPopulationSize, newPopulationSize, mutateChance);
            SetUpDelegates(localSearchType, individualType, mirrorType, randomChooseType, crossoverType, mutateType);

            IndividualFitnessCalculator.SetUpParameters();
            Population = PopulationCreator.CreatePopulation();
            SaveBestIndividual();


        }

        public Algorithm(int n1, int n2, int m, int populationSize,
            int oldPopulationSize = 100, int crossoverPopulationSize = 0, int newPopulationSize = 0, int mutateChance = 0,
            LocalSearchType localSearchType = LocalSearchType.Fast,
            IndividualType individualType = IndividualType.Random,
            MirrorType mirrorType = MirrorType.Best,
            RandomChooseType randomChooseType = RandomChooseType.Random,
            CrossoverType crossoverType = CrossoverType.Random,
            MutateType mutateType = MutateType.Random
            )
        {
            SetUpParameters(n1, n2, m, populationSize, oldPopulationSize, crossoverPopulationSize, newPopulationSize, mutateChance);
            SetUpDelegates(localSearchType, individualType, mirrorType, randomChooseType, crossoverType, mutateType);

            _ = new LocalSearchFlags(true, false, false, false);
            IndividualFitnessCalculator.SetUpParameters();
            Population = PopulationCreator.CreatePopulation();

            BestIndividual = new Individual(Population[0]);
        }

        public void Next(int generations, int time)
        {
            Stopwatch stopwatch = new();

            for (int i = 0; i < generations || stopwatch.ElapsedMilliseconds < time; i++)
            {
                Do();
                SaveBestIndividual();
            }
        }

        public void SaveBestIndividual()
        {
            Population = Population.OrderBy(i => i.Fitness).ToList();

            if (Population[0].Fitness < BestIndividual.Fitness)
            {
                BestIndividual = new Individual(Population[0]);
            }
        }

        public void Do()
        {
            for (int i = OldPopulationSize; i < CrossoverPopulationSize + OldPopulationSize; ++i)
            {
                Population[i].Crossover(ChooseRandomIndividual(i));
            }

            for (int i = 0; i < NewPopulationSize; ++i)
            {
                Population[Population.Count - 1 - i] = new Individual();
            }
        }



        private static void SetUpParameters(int n1, int n2, int m, int populationSize,
            int oldPopulationSize, int crossoverPopulationSize, int newPopulationSize, int mutateChance)
        {
            CheckParameters(ref n1, ref n2, ref m, ref populationSize, ref oldPopulationSize, ref crossoverPopulationSize, ref newPopulationSize, ref mutateChance);
            Individual.SetUpParameters(n1, n2, m);

            PopulationSize = populationSize;
            OldPopulationSize = oldPopulationSize;
            CrossoverPopulationSize = crossoverPopulationSize;
            NewPopulationSize = newPopulationSize;
            MutateChance = mutateChance;
        }

        private void SetUpDelegates(LocalSearchType localSearchType, IndividualType individualType, MirrorType mirrorType,
            RandomChooseType chooseType, CrossoverType crossoverType, MutateType mutateType)
        {
            IndividualLocalSearcher.ChooseLocalSearcherType(localSearchType);
            IndividualCreator.ChooseCreatorType(individualType);
            Mirror.ChooseMirrorType(mirrorType);
            ChooseRandomIndividual = IndividualRandomChooser.ChooseChooserType(chooseType);
            IndividualCrossoverer.ChooseCrossoverType(crossoverType);
            IndividualMutator.ChooseMutatorType(mutateType);
        }

        private static void CheckParameters(ref int n1, ref int n2, ref int m, ref int populationSize,
            ref int oldPopulationSize, ref int crossoverPopulationSize, ref int newPopulationSize, ref int mutateChance)
        {
            n1 = n1 < 2 ? 2 : n1 > 64 ? 64 : n1;
            n2 = n2 < 2 ? 2 : n2 > 64 ? 64 : n2;
            m = m < 1 ? 1 : m > n1 * n2 / 2 ? n1 * n2 / 2 : m;
            populationSize = populationSize < 1 ? 1 : populationSize > 128 ? 128 : populationSize;

            oldPopulationSize = oldPopulationSize < 0 ? 0 : oldPopulationSize > 100 ? 100 : oldPopulationSize;
            newPopulationSize = newPopulationSize < 0 ? 0 : newPopulationSize > 100 ? 100 : newPopulationSize;
            mutateChance = mutateChance < 0 ? 0 : mutateChance > 100 ? 100 : mutateChance;
           
            if (oldPopulationSize + crossoverPopulationSize + newPopulationSize > 100)
            {
                oldPopulationSize = oldPopulationSize > 10 ? 10 : oldPopulationSize;
                newPopulationSize = newPopulationSize > 10 ? 10 : newPopulationSize;
            }

            crossoverPopulationSize = 100 - oldPopulationSize - newPopulationSize ;
        }


        public override string ToString()
        {
            return BestIndividual.ToString();
        }

    }
}