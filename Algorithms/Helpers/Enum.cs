using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public enum AlgorithmType { Strait, Custom}
    public enum IndividualType { Random, Mirror }

    public enum MutateType { Random, Near, RandomAll, NearAll }

    public enum LocalSearchType { Fast, Best, NearBest, NearMutateBest }

    public enum CrossoverType { Random, Part }

    public enum RandomChooseType { Random, FormulaChoose, RankBasedChoose, FitnessBasedChoose }

    public enum MirrorType { Small, Best }

    
}
