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
        public int N { get; set; }
        public int M { get; set; }
        public int Fitness { get; set; }
        public List<int>? Genes { get; set; }

        [JsonConstructor]
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

        public void SaveFile()
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json";
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(fileName, jsonString);
        }

        public static string ReadFileAndRename(string fileName)
        {
            Result? result = JsonSerializer.Deserialize<Result>(File.ReadAllText(fileName));

            return fileName + result.N + result.M;

        }
       

        public static Result? ReadRenamedFileAndReturnResult(string fileName)
        {
            return NotImplementedException();
            Result? result = JsonSerializer.Deserialize<Result>(File.ReadAllText(fileName));
            return result;
        }

        private static Result? NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}
