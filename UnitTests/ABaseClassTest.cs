using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms;

namespace UnitTests
{
    [TestClass]
    public class ABaseClassTest
    {
        public static Algorithm Algorithm;
        public static Individual Individual;
        public static int N;
        public static int M;
        public static int PopulationSize;
        public static int OldPopulationSize;
        public static int CrossoverPopulationSize;
        public static int NewPopulationSize;
        public static IndividualType IndividualType;
        public static CrossoverType CrossoverType;
        public static RandomChooseType RandomChooseType;
        public static MutateType MutateType;
        public static LocalSearchType LocalSearchType;
        public static MirrorType MirrorType;
        public static int MutateChance;
        public static int LocalSearchChance;
        public static MutateFlags MutateFlags;
        public static LocalSearchFlags LocalSearchFlags;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            N = 256;
            M = 8;
            PopulationSize = 1;
            OldPopulationSize = 1;
            CrossoverPopulationSize = 0;
            NewPopulationSize = 0;
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
            Algorithm = new Algorithm(N, M, PopulationSize, OldPopulationSize, CrossoverPopulationSize, NewPopulationSize,
                    IndividualType, CrossoverType, RandomChooseType, MutateType, LocalSearchType, MirrorType,
                    MutateChance, LocalSearchChance, MutateFlags, LocalSearchFlags);
        }

        [TestInitialize]
        public void Initialize()
        {
            Algorithm.Population[0] = new();
            Individual = new(Algorithm.Population[0]);
        }

        

    }
}
