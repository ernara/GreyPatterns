namespace Algorithms
{
    public class MutateFlags
    {
        public static bool PopulationCreation { get; private set; }
        public static bool SmallMirror { get; private set; }
        public static bool BigMirror { get; private set; }
        public static bool Crossover { get; private set; }
        public static bool PopulationSizeIsOne { get; private set; }

        public MutateFlags(bool populationCreation= false, bool smallMirror = false, bool bigMirror = false, bool crossover = false,
            bool populationSizeIsOne = false)
        {
            PopulationCreation = populationCreation;
            Crossover = crossover;
            SmallMirror = smallMirror;
            BigMirror = bigMirror;
            PopulationSizeIsOne = populationSizeIsOne;
        }
    }
}
