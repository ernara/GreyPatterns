using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class IndividualRandomChooser
    {
        private static readonly Random Random = new();
        private static int TotalSum;

        public static Func<int, Individual> ChooseChooserType(RandomChooseType randomChooseType)
        {
            return randomChooseType switch
            {
                RandomChooseType.Random => new(RandomChoose),
                RandomChooseType.FormulaChoose => new(FormulaChoose),
                RandomChooseType.RankBasedChoose => new(RankBasedChoose),
                RandomChooseType.FitnessBasedChoose => new(FitnessBasedChoose),
                _ => throw new ArgumentException("Wrong RandomChooseType"),
            };
        }

        private static Individual RandomChoose(int index)
        {
            return Algorithm.Population[RandomHelper.ReturnRandomNumber(index, Algorithm.PopulationSize)];
        }

        private static Individual FormulaChoose(int index)
        {
            //int firstRandom = RandomHelper.ReturnRandomNumber(index,Algorithm.PopulationSize);
            //int secondRandom = RandomHelper.ReturnRandomNumber(index,Algorithm.PopulationSize);

            //return Algorithm.Population[Math.Max(firstRandom, secondRandom)-Math.Min(firstRandom, secondRandom)];

            //TODO: padaryti į formulę
            return Algorithm.Population[RandomHelper.ReturnRandomNumber(index, Algorithm.PopulationSize)];

        }

        private static Individual RankBasedChoose(int index)
        {
            List<int> FitnessList = new();

            for (int i = Algorithm.PopulationSize ; i > 0; i--)
            {
                TotalSum += i;
                FitnessList.Add(TotalSum);
            }

            int randomValue;

            while (true)
            {
                randomValue = Random.Next(TotalSum);

                for (int i = 0; i < FitnessList.Count; i++)
                {
                    if (FitnessList[i] > randomValue)
                    {
                        randomValue = i;
                        break;
                    }
                }

                if (randomValue!=index)
                {
                    return Algorithm.Population[randomValue];
                }

            }

        }

        private static Individual FitnessBasedChoose(int index)
        {
            List<int> FitnessList = new();

            for (int i = Algorithm.PopulationSize; i > 0; i--)
            {
                TotalSum += Algorithm.Population[i].Fitness;
                FitnessList.Add(TotalSum);
            }

            int randomValue;

            while (true)
            {
                randomValue = Random.Next(TotalSum);

                for (int i = 0; i < FitnessList.Count; i++)
                {
                    if (FitnessList[i] > randomValue)
                    {
                        randomValue = i;
                        break;
                    }
                }

                if (randomValue != index)
                {
                    return Algorithm.Population[randomValue];
                }

            }
        }

    }
}
