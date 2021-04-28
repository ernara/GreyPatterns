using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Individual
    {
        public static int N2 { get; private set; }
        public static int N { get; private set; }
        public static int M { get; private set; }
        
        public List<int> Genes;
        public int Fitness;

        public static void SetUpParameters(int n, int m)
        {
            N2 = (int)Math.Sqrt(n);
            N = n;
            M = m;
        }

        public Individual(List<int> genes, int fitness)
        {
            Genes = new List<int>(genes);
            Fitness = fitness;
        }

        public Individual(Individual individual)
        {
            Genes = new List<int>(individual.Genes);
            Fitness = individual.Fitness;
        }

        public Individual()
        {
            Individual individual = IndividualCreator.CreateIndividual();
            Genes = individual.Genes;
            Fitness = individual.Fitness;
        }

        public override string ToString()
        {
            string genes = "";
            for (int i=0; i < Individual.M; ++i)
            {
                genes += Genes[i].ToString() + " ";
            }
            return $"Genes: {genes}| Fitness: {Fitness}";
        }


    }
}
