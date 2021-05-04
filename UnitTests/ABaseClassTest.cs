using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;
using System;

namespace UnitTests
{
    [TestClass]
    public class ABaseClassTest
    {
        public Random Random = new();

        public static Algorithm Algorithm { get; protected set; }
        public static Individual Individual { get; protected set; }
        public static Individual Individual2 { get; protected set; }
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

        public static int SmallerN2 { get; protected set; }
        public static int SmallerM { get; protected set; }
        public static int SmallerN { get; private set; }

        public static DistancesMatrix DistancesMatrix { get; protected set; }
        public static Coordinate Coordinate { get; protected set; }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            N = 256;
            N2 = 16;
            M = 32;
            PopulationSize = 100;
            OldPopulationSize = 10;
            CrossoverPopulationSize = 80;
            NewPopulationSize = 10;
            IndividualType = IndividualType.Random;
            CrossoverType = CrossoverType.Random;
            RandomChooseType = RandomChooseType.Random;
            MutateType = MutateType.Random;
            LocalSearchType = LocalSearchType.Fast;
            MirrorType = MirrorType.Small;
            MutateChance = 100;
            LocalSearchChance = 100;
            MutateFlags = new();
            LocalSearchFlags = new();

            Iterations = 100;
            Time = 1;

        }

        [TestInitialize]
        public void Initialize()
        {
            Algorithm = new Algorithm(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                    IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                    MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
            Individual = new(Algorithm.Population[0]);
            Individual2 = new(Algorithm.Population[0]);

            SmallerN2 = Individual.N2;
            SmallerM = Individual.M;
            SmallerN = Individual.N;
        }
    }
}
