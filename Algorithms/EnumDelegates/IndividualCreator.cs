using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class IndividualCreator
    {
        private readonly static Random Random = new();

        private static Func<List<int>> genesCreator = new(CreateRandomIndividual);

        public static Individual CreateIndividual()
        {
            List<int> Genes = genesCreator();
            return new Individual(Genes, Genes.CalculateFitness());
        }

        public static void ChooseCreatorType(IndividualType individualType)
        {
            genesCreator = individualType switch
            {
                IndividualType.Random => new(CreateRandomIndividual),
                IndividualType.Mirror => new(CreateMirrorIndividual),
                _ => throw new ArgumentException("Wrong IndividualType"),
            };
        }

        private static List<int> CreateRandomIndividual()
        {
            List<int> Genes = Enumerable.Range(0, Individual.N).OrderBy(g => Random.Next(Individual.N)).ToList();
            return Genes;
        }

        private static List<int> CreateRandomSmallIndividual()
        {
            List<int> Genes = Enumerable.Range(0, Mirror.SmallerN).OrderBy(g => Random.Next(Mirror.SmallerN)).ToList();
            return Genes;
        }

        private static List<int> CreateMirrorIndividual()
        {
            List<int> SmallGenes = CreateRandomSmallIndividual().Take(Mirror.SmallerM).ToList(); 
            List<int> BigGenes = new();

            for (int i = 0; i < SmallGenes.Count; i++)
            {
                BigGenes.AddRange(SmallGenes[i].ChangeToBiggerNumbers());
            }

            return BigGenes.Union(Enumerable.Range(0, Individual.N)).Distinct().ToList();
        }
    }
}
