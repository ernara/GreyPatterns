using System;
using System.Collections.Generic;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class MirrorTest
    {
        int N1;
        int N2;
        int M;
        int PopulationSize;
        Algorithm Algorithm;
        Individual Individual;
        //Individual Individual2;

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
            Algorithm = new(N1, N2, M, PopulationSize);
            Individual = new(Algorithm.BestIndividual);
        }

        [TestMethod]
        public void TestMirrors()
        {
            foreach (var mirrorType in Enum.GetValues(typeof(MirrorType)))
            {
                Mirror.ChooseMirrorType((MirrorType)mirrorType);

                Assert.IsTrue(Mirror.SmallerN1 < N1);
                Assert.IsTrue(Mirror.SmallerN2 < N2);
                Assert.IsTrue(Mirror.SmallerM < M);

                Individual.MakeMirrored();
                
                Assert.AreNotEqual(Individual.ToString(), Algorithm.BestIndividual.ToString());
                Assert.IsTrue(Individual.Fitness > 0);
            }
        }
    }
}
