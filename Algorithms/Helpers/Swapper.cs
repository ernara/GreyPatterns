using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public static class Swapper
    {
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            T temporary = list[i];
            list[i] = list[j];
            list[j] = temporary;
        }
    }
}