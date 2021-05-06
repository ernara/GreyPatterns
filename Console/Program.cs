using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithms;
using System.Threading;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Consolee
{
    public static class Program
    {
        public static Algorithm Algorithm { get; private set; }
        public static Individual Individual { get; private set; }
        public static Individual Individual2 { get; private set; }
        public static int N { get; private set; }
        public static int N2 { get; private set; }
        public static int M { get; private set; }
        public static int PopulationSize { get; private set; }
        public static int OldPopulationSize { get; private set; }
        public static int CrossoverPopulationSize { get; private set; }
        public static int NewPopulationSize { get; private set; }
        public static IndividualType IndividualType { get; private set; }
        public static CrossoverType CrossoverType { get; private set; }
        public static RandomChooseType RandomChooseType { get; private set; }
        public static MutateType MutateType { get; private set; }
        public static LocalSearchType LocalSearchType { get; private set; }
        public static MirrorType MirrorType { get; private set; }
        public static int MutateChance { get; private set; }
        public static int LocalSearchChance { get; private set; }
        public static MutateFlags MutateFlags { get; private set; }
        public static LocalSearchFlags LocalSearchFlags { get; private set; }


        public static int Iterations { get; private set; }
        public static int Time { get; private set; }

        public static int SmallerN2 { get; private set; }
        public static int SmallerM { get; private set; }
        public static int SmallerN { get; private set; }

        public static DistancesMatrix DistancesMatrix { get; private set; }
        public static Coordinate Coordinate { get; private set; }

        public static void Main()
        {
            N = 256;
            M = 32;
            PopulationSize = 100;
            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
            IndividualType = IndividualType.Random;
            CrossoverType = CrossoverType.Part;
            RandomChooseType = RandomChooseType.Random;
            MutateType = MutateType.Random;
            LocalSearchType = LocalSearchType.Fast;
            MirrorType = MirrorType.Small;
            MutateChance = 10;
            LocalSearchChance = 0;
            MutateFlags = new(false, true, false, false, false);
            LocalSearchFlags = new(false, true, false, false, false);

            Console.WriteLine("started");

            //DifferentNewPopulationSizeParameter();
            //DifferentCrossoverType();
            //DifferentNewPopulationSizeParameter2();
            //DifferentOldPopulationSizeParameter();
            //DifferentIndividualTypeParameterNewSize100();
            //DifferentIndividualTypeParameter();
            //DifferentIndividualTypeParameterNewSize100M125();
            //DifferentIndividualTypeParameterM125();
            //DifferentIndividualRandomChooseParameter();
            //DifferentMutateTypeParameter();
            DifferentLocalSearchType();

            //for (int i = 0; i < 1000; i++)
            //{
            //    Algorithm.Next();
            //    Console.WriteLine(Algorithm.Population[0]);

            //}
        }


        public static void DifferentNewPopulationSizeParameter()
        {
            List<int> DifferentOld = new List<int>() {   11, 11, 10, 10,  8,  5, 0 };
            List<int> DifferentCross = new List<int>() { 89, 88, 85, 80, 72, 45, 0 };
            List<int> DifferentNew = new List<int>() {    0,  1,  5, 10, 20, 50, 100 };

            for (int i = 0; i < DifferentNew.Count; i++)
            {
                OldPopulationSize = DifferentOld[i];
                CrossoverPopulationSize = DifferentCross[i];
                NewPopulationSize = DifferentNew[i];

                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                    IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                    MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentNewPopulationSizeParameter.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {i}");
            }

            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
        }

        

        public static void DifferentCrossoverType()
        {
            foreach (var crossoverType in Enum.GetValues(typeof(CrossoverType)))
            {
                CrossoverType = (CrossoverType)crossoverType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                    IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                    MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentCrossoverType.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {crossoverType}");
            }

            IndividualCrossoverer.ChooseCrossoverType(CrossoverType.Random);
           
        }

        public static void DifferentNewPopulationSizeParameter2()
        {
            List<int> DifferentOld = new List<int>() { 11, 11, 10, 10, 8, 5, 0 };
            List<int> DifferentCross = new List<int>() { 89, 88, 85, 80, 72, 45, 0 };
            List<int> DifferentNew = new List<int>() { 0, 1, 5, 10, 20, 50, 100 };

            for (int i = 0; i < DifferentNew.Count; i++)
            {
                OldPopulationSize = DifferentOld[i];
                CrossoverPopulationSize = DifferentCross[i];
                NewPopulationSize = DifferentNew[i];

                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                    IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                    MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentNewPopulationSizeParameter2.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {i}");
            }

            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
        }

        public static void DifferentOldPopulationSizeParameter()
        {
            List<int> DifferentOld = new List<int>() { 0, 1, 5, 10, 20, 50, 100 };
            List<int> DifferentCross = new List<int>() { 89, 88, 85, 80, 72, 45, 0 };
            List<int> DifferentNew = new List<int>() { 11, 11, 10, 10, 8, 5, 0 };

            for (int i = 0; i < DifferentNew.Count; i++)
            {
                OldPopulationSize = DifferentOld[i];
                CrossoverPopulationSize = DifferentCross[i];
                NewPopulationSize = DifferentNew[i];

                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                    IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                    MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentOldPopulationSizeParameter.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {i}");
            }

            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
        }

        public static void DifferentIndividualTypeParameterNewSize100()
        {
            OldPopulationSize = 0;
            CrossoverPopulationSize = 0;
            NewPopulationSize = 100;


            foreach (var individualType in Enum.GetValues(typeof(IndividualType)))
            {
                IndividualType = (IndividualType)individualType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                   IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                   MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentIndividualTypeParameterNewSize100.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {individualType}");
            }

            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
            IndividualType = IndividualType.Random;
        }

        public static void DifferentIndividualTypeParameter()
        {
            foreach (var individualType in Enum.GetValues(typeof(IndividualType)))
            {
                IndividualType = (IndividualType)individualType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                   IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                   MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentIndividualTypeParameter.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {individualType}");
            }

            IndividualType = IndividualType.Random;
        }

        public static void DifferentIndividualTypeParameterNewSize100M125()
        {
            OldPopulationSize = 0;
            CrossoverPopulationSize = 0;
            NewPopulationSize = 100;
            M = 125;

            foreach (var individualType in Enum.GetValues(typeof(IndividualType)))
            {
                IndividualType = (IndividualType)individualType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                   IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                   MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentIndividualTypeParameterNewSize100M125.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {individualType}");
            }

            M = 32;
            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
            IndividualType = IndividualType.Random;
        }

        public static void DifferentIndividualTypeParameterM125()
        {
            M = 125;

            foreach (var individualType in Enum.GetValues(typeof(IndividualType)))
            {
                IndividualType = (IndividualType)individualType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                   IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                   MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentIndividualTypeParameterM125.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {individualType}");
            }

            M = 32;
            IndividualType = IndividualType.Random;
        }

        public static void DifferentIndividualRandomChooseParameter()
        {
            foreach (var randomChooseType in Enum.GetValues(typeof(RandomChooseType)))
            {
                RandomChooseType = (RandomChooseType)randomChooseType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                   IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                   MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentIndividualRandomChooseParameter.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {randomChooseType}");
            }

            RandomChooseType = RandomChooseType.Random;

        }

        public static void DifferentMutateTypeParameter()
        {
            foreach (var mutateType in Enum.GetValues(typeof(MutateType)))
            {
                MutateType = (MutateType)mutateType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                   IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                   MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentMutateTypeParameter.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {mutateType}");
            }

            MutateType = MutateType.Random;
        }

        public static void DifferentLocalSearchType()
        {
            LocalSearchChance = 1;
            foreach (var localSearchType in Enum.GetValues(typeof(LocalSearchType)))
            {
                LocalSearchType = (LocalSearchType)localSearchType;
                Algorithm Algorithm = new(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                   IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                   MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
                string fileName = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\ResearchResults\DifferentLocalSearchType.txt";

                Algorithm.NextResearch(10, fileName);
                Console.WriteLine($"Doneeee {localSearchType}");
            }

            LocalSearchChance = 0;
            LocalSearchType = LocalSearchType.Fast;
        }



        //DifferentLocalSearchType











    }


}


