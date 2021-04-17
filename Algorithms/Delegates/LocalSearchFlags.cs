using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class LocalSearchFlags
    {
        public static bool PopulationCreation { get; private set; }
        public static bool SmallMirror { get; private set; }
        public static bool BigMirror { get; private set; }
        public static bool Crossover { get; private set; }

        public LocalSearchFlags(bool populationCreation = false, bool smallMirror = false, bool bigMirror = false, bool crossover = false)
        {
            PopulationCreation = populationCreation;
            SmallMirror = smallMirror;
            BigMirror = bigMirror;
            Crossover = crossover;
        }

    }
}
