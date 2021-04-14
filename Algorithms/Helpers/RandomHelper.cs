using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class RandomHelper
    {
        private static readonly Random Random = new();

        public static int ReturnRandomNumber(int exceptNumber, int maxValue)
        {
            int value;
            while (true)
            {
                value = Random.Next(maxValue);
                if (value != exceptNumber)
                    return value;
            }
        }
        public static int ReturnRandomNumber(int exceptNumber, int minValue, int maxValue)
        {
            int value;
            while (true)
            {
                value = Random.Next(minValue, maxValue);
                if (value != exceptNumber)
                    return value;
            }
        }

    }
}
