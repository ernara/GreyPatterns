using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class IndividualRandomChooser
    {
        private static readonly Random Random = new();
        private static int TotalSum;

        public static Func<Individual> ChooseChooserType(RandomChooseType randomChooseType)
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

        private static Individual RandomChoose()
        {
            return Algorithm.Population[Random.Next(Algorithm.PopulationSize)];
        }

        private static Individual FormulaChoose()
        {
            //TODO: padaryti į formulę
            return Algorithm.Population[Random.Next(Algorithm.PopulationSize)];
        }

        private static Individual RankBasedChoose()
        {
            List<int> FitnessList = new();
            TotalSum = 0;

            for (int i = Algorithm.PopulationSize; i > 0; i--)
            {
                TotalSum += i;
                FitnessList.Add(TotalSum);
            }

            int randomValue;
           
            randomValue = Random.Next(TotalSum);

            for (int i = 0; i < FitnessList.Count; i++)
            {
                if (FitnessList[i] > randomValue)
                {
                    randomValue = i;
                    break;
                }
            }
               
            return Algorithm.Population[randomValue];
        }

        private static Individual FitnessBasedChoose()
        {
            List<int> FitnessList = new();
            TotalSum = 0;

            for (int i = Algorithm.PopulationSize-1; i >= 0; i--)
            {
                TotalSum += Algorithm.Population[i].Fitness;
                FitnessList.Add(TotalSum);
            }

            int randomValue;

            
            randomValue = Random.Next(TotalSum);

            for (int i = 0; i < FitnessList.Count; i++)
            {
                if (FitnessList[i] > randomValue)  
                {
                    randomValue = i;
                    break;
                }
            }
                
            return Algorithm.Population[randomValue];
                
        }

    }
}
