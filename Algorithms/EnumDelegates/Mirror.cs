using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public static class Mirror
    {
        public static int SmallerN2 { get; private set; }
        public static int SmallerN { get; private set; }
        public static int SmallerM { get; private set; }

        public static void ChooseMirrorType(MirrorType mirrorType)
        {
            SmallerN2 = Individual.N2;
            SmallerM = Individual.M;

            switch (mirrorType)
            {
                case MirrorType.Small:
                    SmallMirror();
                    break;
                case MirrorType.Best:
                    BestMirror();
                    break;
                default:
                    throw new ArgumentException("Wrong MirrorType");
            }

            SmallerN = SmallerN2 * SmallerN2;

        }

        public static void MakeMirrored(this Individual individual)
        {
            individual.ChangeToSmallMirror();

            individual.Mutate(MutateFlags.SmallMirror);
            individual.LocalSearch(LocalSearchFlags.SmallMirror);

            individual.ChangeToBigMirror();

            individual.Mutate(MutateFlags.BigMirror);
            individual.LocalSearch(LocalSearchFlags.BigMirror);
        }

        private static void ChangeToSmallMirror(this Individual individual)
        {
            for (int i = 0; i < Individual.M; i++)
            {
                individual.Genes[i] = individual.Genes[i].ChangeToSmallerNumber();
            }

            individual.Genes = individual.Genes.Take(Individual.M).Union(Enumerable.Range(0, SmallerN)).Distinct().ToList();
            individual.CalculateFitness();
        }

        private static void ChangeToBigMirror(this Individual individual)
        {
            List<int> genes = new();

            for (int i = 0; i < SmallerM; i++)
            {
                genes.AddRange(individual.Genes[i].ChangeToBiggerNumbers());
            }

            individual.Genes = genes.Take(Individual.M).Union(Enumerable.Range(0, Individual.N)).Distinct().ToList();
            individual.CalculateFitness();
        }


        public static void SmallMirror()
        {
            while (SmallerN2 > 3 && SmallerM > 3)
            {
                SmallerN2 /= 2;
                SmallerM /= 4;
            }
        }

        private static void BestMirror()
        {
            while (SmallerN2 > 7 && SmallerM > 19)
            {
                SmallerN2 /= 2;
                SmallerM /= 4;
            }
        }


        private static int ChangeToSmallerNumber(this int number)
        {
            Coordinate coordinate = new(number, Individual.N2);
            while (coordinate.X >= SmallerN2)
                coordinate.X -= SmallerN2;
            while (coordinate.Y >= SmallerN2)
                coordinate.Y -= SmallerN2;

            return coordinate.X * SmallerN2 + coordinate.Y;
        }

        public static List<int> ChangeToBiggerNumbers(this int number)
        {
            List<int> numbers = new();
            Coordinate coordinate = new(number, SmallerN2);

            for (int i = 0; i < Individual.N2 / SmallerN2; i++)
            {
                for (int j = 0; j < Individual.N2 / SmallerN2; j++)
                {
                    numbers.Add((coordinate.X + i * SmallerN2) * Individual.N2 + coordinate.Y + SmallerN2 * j);
                }
            }
            return numbers;
        }




    }
}
