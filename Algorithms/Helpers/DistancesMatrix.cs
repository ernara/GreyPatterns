using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DistancesMatrix
    {
        public int[,] Array;

        public int this[int i, int j]
        {
            get { return Array[i, j]; }
        }

        public DistancesMatrix()
        {
            throw new NotImplementedException();
        }

        public DistancesMatrix(int n1, int n2)
        {
            int n = n1 * n2;

            Array = new int[n, n];
            
            for (int i = 0; i < Array.GetLength(0); ++i)
            {
                for (int j = 0; j < Array.GetLength(1); ++j)
                {
                    Array[i, j] = CalculateDistance(i, j, n1, n2);
                }
            }
        }

        public static int CalculateDistance(int i, int j, int n1, int n2)
        {
            double value = 0;

            if (i != j)
            {
                double lv;

                Coordinate coordinate1 = new(i, n2);
                Coordinate coordinate2 = new(j, n2);

                for (int w1 = -1; w1 <= 1; w1++)
                    for (int w2 = -1; w2 <= 1; w2++)
                    {
                        var expr1 = Math.Pow(coordinate1.X - coordinate2.X + w1 * n1, 2);
                        var expr2 = Math.Pow(coordinate1.Y - coordinate2.Y + w2 * n2, 2);

                        lv = 1.0 / (expr1 + expr2);
                        value = Math.Max(lv, value);
                    }
            }

            return Convert.ToInt32(value * 100000);
        }
    }
}
