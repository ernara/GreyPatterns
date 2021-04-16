using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{

    public class Individual
    {
        public static int N1 { get; private set; }
        public static int N2 { get; private set; }
        public static int N { get; private set; }

        public static int M { get; private set; }
        
        public List<int> Genes;
        public int Fitness;

        public static void SetUpParameters(int n1, int n2, int m)
        {
            N1 = n1;
            N2 = n2;
            N = n1 * n2;
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
            foreach (int i in Genes)
            {
                genes += i.ToString() + " ";
            }
            return $"Genes: {genes}| Fitness: {Fitness}";
        }


    }
}
