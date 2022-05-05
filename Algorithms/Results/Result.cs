using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Algorithms
{
    public class Result
    {
        [JsonInclude]
        public int N { get; set; }
        [JsonInclude]
        public int M { get; set; }
        [JsonInclude]
        public int Fitness { get; set; }
        [JsonInclude]
        public List<int>? Genes { get; set; }

        public Result()
        {

        }

        public Result(Individual individual)
        {
            N = Individual.N;
            M = Individual.M;
            Fitness = individual.Fitness;
            Genes = new List<int>(individual.Genes);
        }


        public override string ToString()
        {
            return $"{N} {M} {Fitness}";
        }

        public static string ReadFileAndRename(string fileName)
        {
            //Trace.WriteLine
            //Trace.WriteLine(File.ReadAllText(fileName));
            var result = JsonSerializer.Deserialize<Result>(File.ReadAllText(fileName));

            return fileName.Substring(0,fileName.Length - 5) + " N:" + result.N + " M:" +  result.M;
        }

        public void SaveFile()
        {
            string fileName = DateTime.Now.ToString("yy-MM-dd HH;mm;ss") + ".json";
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(fileName, jsonString);
        }


        public static Result? ReadFile(string fileName)
        {
            return JsonSerializer.Deserialize<Result>(File.ReadAllText(fileName));
        }

        private static Result? NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}
