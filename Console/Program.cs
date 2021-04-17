﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithms;
using System.Threading;
using System.Linq;

namespace Consolee
{
    static class Program
    {
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }
        public static void Main()
        {
            
        }
    }
}
