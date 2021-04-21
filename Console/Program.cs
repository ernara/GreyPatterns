using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithms;
using System.Threading;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Consolee
{
    public static class Program
    {

        public static void Main()
        {

            Console.WriteLine("startssss");

            int n1 = 64;
            int n2 = 64;


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Hm hm = new ();
            hm.Parralel(n1, n2);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine($"result: :{hm.Array[0, 10] + hm.Array[10, 10] + hm.Array[20, 10] + hm.Array[7, 9] + hm.Array[n1 - 1, n2 - 2]}");

            stopwatch.Restart();

            hm.NotParralel(n1, n2);


            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine($"result: :{hm.Array[0, 10] + hm.Array[10, 10] + hm.Array[20, 10] + hm.Array[7, 9] + hm.Array[n1 - 1, n2 - 2]}");

            stopwatch.Restart();

            hm.ParralelWithSignal(n1, n2);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine($"result: :{hm.Array[0, 10] + hm.Array[10, 10] + hm.Array[20, 10] + hm.Array[7, 9] + hm.Array[n1 - 1, n2 - 2]}");



        }

    }

    public class Hm
    {
        public int[,] Array;

        public void NotParralel(int n1, int n2)
        {
            int n = n1 * n2;

            for (int i = 0; i < Array.GetLength(0); ++i)
            {
                for (int j = 0; j < Array.GetLength(1); ++j)
                {
                    Array[i, j] = CalculateDistance(i, j, n1, n2);
                }
            }
        }

        

        public void CalculatePart(int from, int to, int n1, int n2)
        {
            for (int i = 0; i < Array.GetLength(0); ++i)
            {
                for (int j = from; j < to; ++j)
                {
                    Array[i, j] = CalculateDistance(i, j, n1, n2);
                }
            }
        }

        public void Parralel(int n1, int n2)
        {
            int n = n1 * n2;

            Array = new int[n, n];

            List<Thread> threads = new();

            int onePart = n / (Environment.ProcessorCount);

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                int localNum = i;
                int a = n1;
                int a2 = n2;
                threads.Add(new Thread(() => CalculatePart(localNum * onePart, localNum * onePart + onePart, a, a2)));
            }

            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threads.Count; i++) { threads[i].Join(); };

        }


        public void ParralelWithSignal(int n1, int n2)
        {

            int n = n1 * n2;

            Array = new int[n, n];

            int onePart = n / Environment.ProcessorCount;

            using (CountdownEvent counter = new CountdownEvent(Environment.ProcessorCount))
            {

                for (int i = 0; i < Environment.ProcessorCount; i++)
                {
                    int localNum = i;
                    int a = n1;
                    int a2 = n2;
                    ThreadPool.QueueUserWorkItem(_ => CalculatePart(localNum * onePart, localNum * onePart + onePart, a, a2,counter));
                }
                counter.Wait();
            }


        }

        public void CalculatePart(int from, int to, int n1, int n2, CountdownEvent countdown)
        {
            for (int i = 0; i < Array.GetLength(0); ++i)
            {
                for (int j = from; j < to; ++j)
                {
                    Array[i, j] = CalculateDistance(i, j, n1, n2);
                }
            }

            countdown.Signal();

        }

        public int CalculateDistance(int i, int j, int n1, int n2)
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
