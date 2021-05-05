using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Result
    {
        public int N;
        public int M;
        public int Fitness;
        //public List<int> Genes;
        public Result(int n, int m,  int fitness) // List <int> genes
        {
            N = n;
            M = m;
            Fitness = fitness;
            //Genes = new List<int>(genes);
        }

        public Result(Individual individual) // List <int> genes
        {
            N = Individual.N;
            M = Individual.M;
            Fitness = individual.Fitness;
            //Genes = new List<int>(individual.Genes);
        }

        public override string ToString()
        {
            return $"{N} {M} {Fitness}";
        }
    }
}
