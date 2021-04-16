﻿using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class IndividualFitnessCalculatorTest
    {
        int N1;
        int N2;
        int M;
        int PopulationSize;
        public Algorithm Algorithm;
        Individual Individual;

        [TestCleanup]
        public void TestCleanup()
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            N1 = 16;
            N2 = 16;
            M = 32;
            PopulationSize = 1;

            Algorithm = new Algorithm(N1, N2, M, PopulationSize);
            Individual = new();
        }

        [TestMethod]
        public void TestAllCalculateFitness()
        {
            IndividualFitnessCalculator.CalculateFitness(Individual.Genes);
            int CalculatedFitnessByGenes = Individual.Fitness;
            IndividualFitnessCalculator.CalculateFitness(Individual);
            int CalculatedFitnessByIndividual = Individual.Fitness;

            int CalculatedFitnessByReturn = IndividualFitnessCalculator.ReturnCalculatedFitness(Individual);

            Assert.AreEqual(CalculatedFitnessByGenes,CalculatedFitnessByIndividual);
            Assert.AreEqual(CalculatedFitnessByIndividual, CalculatedFitnessByReturn);
        }

        [TestMethod]
        public void TestCalculateFitness()
        {
            Individual Individual2 = new(Individual.Genes,0);
            IndividualFitnessCalculator.CalculateFitness(Individual2);
            Assert.IsTrue(Individual.Fitness>0);
            Assert.AreEqual(Individual.Fitness, Individual2.Fitness);
        }

        [TestMethod]
        public void TestCalculateFitnessMirror()
        {
            foreach (var mirrorType in Enum.GetValues(typeof(MirrorType)))
            {
                Mirror.ChooseMirrorType((MirrorType)mirrorType);
                Individual.Genes = Enumerable.Range(0, Mirror.SmallerN).ToList();
                IndividualFitnessCalculator.CalculateFitness(Individual);
                Assert.IsTrue(Individual.Fitness > 0);
            }
        }

        [TestMethod]
        public void TestCalculateFitnessException()
        {
            Individual.Genes.RemoveAt(0);
            Assert.ThrowsException<ArgumentException>(() => IndividualFitnessCalculator.CalculateFitness(Individual));
        }

    }
}