//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace Algorithms
//{
//    public static class BestResults
//    {
//        private static readonly string BestKnownValues = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\Results\BestKnownValues.txt";
//        private static readonly string BestFoundValues = @"C:\Users\Ernestas\source\repos\GreyPatterns\Algorithms\Results\BestFoundValues.txt";
//        public static List<Result> BestKnownResults;
//        public static List<Result> BestFoundResults;
//        public static void SetUpResults()
//        {
//            BestKnownResults = new List<Result>();
//            BestFoundResults = new List<Result>();
//            //ReadFile(BestKnownValues, BestKnownResults); // atitirinti kazkada reikes
//            //ReadFile(BestFoundValues, BestFoundResults); //atitirinti kazkada reikes

//            //Results = new List<Result>();
//            //ReadFromFile();
//        }
//        //private static void ReadFromFile()
//        //{
//        //    IFormatter formatter = new BinaryFormatter();

//        //    var stream = new FileStream(BestKnownValues, FileMode.Open, FileAccess.Read);
//        //    Results = (List<Result>)formatter.Deserialize(stream);
//        //}

//        private static void ReadFile(string fileName, List<Result> results)
//        {
//            string[] lines = File.ReadAllLines(fileName);

//            int n = Convert.ToInt32(lines[0]);

//            for (int i = 1; i < 5; ++i)
//            {
//                var parts = lines[i].Split('\t');

//                for (int j = 0; j < parts.Length - 2; j += 2)
//                {
//                    results.Add(new Result(n, Convert.ToInt32(parts[j]), Convert.ToInt32(parts[j + 1])));
//                }
//            }

//            n = Convert.ToInt32(lines[5]);

//            for (int i = 6; i < 22; ++i)
//            {
//                var parts = lines[i].Split('\t');

//                for (int j = 0; j < parts.Length - 2; j += 2)
//                {
//                    results.Add(new Result(n, Convert.ToInt32(parts[j]), Convert.ToInt32(parts[j + 1])));
//                }
//            }
//        }

//        public static void WriteToFile(string fileName, List<Result> results)
//        {
//            using StreamWriter sw = new(fileName);
//            sw.Write(64);

//            int i;

//            for (i = 0; results[i].N != 256; i++)
//            {
//                if (i % 8 == 0)
//                {
//                    sw.WriteLine();
//                }
//                sw.Write($"{results[i].M}\t{results[i].Fitness}\t");
//            }

//            sw.WriteLine();
//            sw.Write(256);

//            for (int j = 0; i < results.Count; ++i, ++j)
//            {
//                if (j % 8 == 0)
//                {
//                    sw.WriteLine();
//                }
//                sw.Write($"{results[i].M}\t{results[i].Fitness}\t");
//            }
//        }


//    }
//}
