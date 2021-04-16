using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Algorithm
    {
        public static int PopulationSize { get; private set; }
        public int OldPopulationSize { get; private set; }
        public int CrossPopulationSize { get; private set; }
        public int NewPopulationSize { get; private set; }
        public int MutateChance { get; private set; }

        public static List<Individual> Population { get; private set; }
        public Individual BestIndividual { get; private set; }

        private Func<int, Individual> ChooseRandomIndividual;

        public Algorithm(int n1, int n2, int m, int populationSize,
            LocalSearchFlag localSearchFlags,

            LocalSearchType localSearchType = LocalSearchType.Fast,
            IndividualType individualType = IndividualType.Random,
            MirrorType mirrorType = MirrorType.Best,
            RandomChooseType randomChooseType = RandomChooseType.Random,
            CrossoverType crossoverType = CrossoverType.Random,
            MutateType mutateType = MutateType.Random
            )
        {
            SetUpParameters(n1, n2, m, populationSize);
            SetUpDelegates(localSearchType, individualType, mirrorType, randomChooseType, crossoverType, mutateType);

            IndividualFitnessCalculator.SetUpParameters();
            Population = PopulationCreator.CreatePopulation();

            BestIndividual = new Individual(Population[0]);
        }

        public Algorithm(int n1, int n2, int m, int populationSize,
            LocalSearchType localSearchType = LocalSearchType.Fast,
            IndividualType individualType = IndividualType.Random,
            MirrorType mirrorType = MirrorType.Best,
            RandomChooseType randomChooseType = RandomChooseType.Random,
            CrossoverType crossoverType = CrossoverType.Random,
            MutateType mutateType = MutateType.Random
            )
        {
            SetUpParameters(n1, n2, m, populationSize);
            SetUpDelegates(localSearchType, individualType, mirrorType, randomChooseType, crossoverType, mutateType);

            _= new LocalSearchFlag(true,false,false,false);
            IndividualFitnessCalculator.SetUpParameters();
            Population = PopulationCreator.CreatePopulation();

            BestIndividual = new Individual(Population[0]);
        }

        public void Next(int generations, int time)
        {
            
            Stopwatch stopwatch = new();

            for (int i = 0; i < generations && stopwatch.ElapsedMilliseconds < time; i++)
            {
                Do();
            }
        }

        public void Do()
        {
            for (int i = OldPopulationSize; i < CrossPopulationSize + OldPopulationSize; ++i)
            {
                Population[i].Crossover(ChooseRandomIndividual(i));
            }

            for (int i = CrossPopulationSize + OldPopulationSize; i < PopulationSize; ++i)
            {
                Population[i] = new Individual();
            }
        }



        private static void SetUpParameters(int n1, int n2, int m, int populationSize)
        {
            CheckParameters(ref n1, ref n2, ref m, ref populationSize);
            Individual.SetUpParameters(n1, n2, m);
            PopulationSize = populationSize;
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

        private static void CheckParameters(ref int n1, ref int n2, ref int m, ref int populationSize)
        {
            n1 = n1 < 2 ? 2 : n1 > 64 ? 64 : n1;
            n2 = n2 < 2 ? 2 : n2 > 64 ? 64 : n2;
            m = m < 1 ? 1 : m > n1 * n2 / 2 ? n1 * n2 / 2 : m;
            populationSize = populationSize < 1 ? 1 : populationSize > 64 ? 64 : populationSize;
        }


        public override string ToString()
        {
            return BestIndividual.ToString();
        }

    }
}
