using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class LocalSearchFlag
    {
        public static bool SmallMirror { get; private set; }
        public static bool BigMirror { get; private set; }
        public static bool Crossover { get; private set; }

        public static bool Mutate { get; private set; }

        public LocalSearchFlag(bool smallMirror = false, bool bigMirror = false, bool crossover = false, bool mutate = false)
        {
            Crossover = crossover;
            SmallMirror = smallMirror;
            BigMirror = bigMirror;
            Mutate = mutate;
        }
        
    }
}
