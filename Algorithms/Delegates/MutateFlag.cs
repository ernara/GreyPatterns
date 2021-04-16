﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class MutateFlag
    {
        public static bool PopulationCreation { get; private set; }
        public static bool SmallMirror { get; private set; }
        public static bool BigMirror { get; private set; }
        public static bool Crossover { get; private set; }

        public MutateFlag(bool populationCreation, bool smallMirror = false, bool bigMirror = false, bool crossover = false)
        {
            PopulationCreation = populationCreation;
            Crossover = crossover;
            SmallMirror = smallMirror;
            BigMirror = bigMirror;
        }
    }
}
