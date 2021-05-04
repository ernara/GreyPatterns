using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Exceptions
    {
        public static string WrongGenesCountMessage { get; private set; } = $"Wrong Individual Genes Count. Individual Genes Count must be {Individual.N} or {Mirror.SmallerN}";
        public static string FlagsAreNullMessage { get; private set; } = $"Both or one of the flags are null";
    }
}
