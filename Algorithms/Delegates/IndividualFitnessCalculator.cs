﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class IndividualFitnessCalculator
    {
        private static DistancesMatrix DistancesMatrix;
        private static DistancesMatrix SmallDistancesMatrix;

        public static void SetUpParameters()
        {
            DistancesMatrix = new(Individual.N1, Individual.N2);
            SmallDistancesMatrix = new(Mirror.SmallerN1, Mirror.SmallerN2);
        }
        public static int CalculateFitness(this List<int> genes)
        {
            if (genes.Count==Individual.N)
            {
                return genes.CalculateFitness(Individual.M, DistancesMatrix);
            }
            else if (genes.Count==Mirror.SmallerN)
            {
                return genes.CalculateFitness(Mirror.SmallerM, SmallDistancesMatrix);
            }
            else
            {
                throw new ArgumentException("Wrong genes.Count");
            }
        }

        public static int CalculateFitness(this List<int> genes, int m, DistancesMatrix distancesMatrix)
        {
            int fitness = 0;

            for (int i = 0; i < m-1; ++i)
            {
                for (int j = i + 1; j < m; j++)
                {
                    fitness += distancesMatrix[genes[i], genes[j]];
                }
            }

            return fitness;
        }

        public static void CalculateFitness(this Individual individual)
        {
            individual.Fitness = individual.ReturnCalculatedFitness();
        }

        public static int ReturnCalculatedFitness(this Individual individual)
        {
            return individual.Genes.CalculateFitness();
        }

    }
}
