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

        [TestMethod]

        public void TestChangeToBiggerNumbersWhenMirrorIsSmall()
        {
            Mirror.ChooseMirrorType(MirrorType.Small);

            int number = 3;
            List<int> bigNumbers = number.ChangeToBiggerNumbers(); 

            Assert.AreEqual(3,bigNumbers[0] );
            Assert.AreEqual(7,bigNumbers[1] );
            Assert.AreEqual(11,bigNumbers[2] );
            Assert.AreEqual(15,bigNumbers[3] );
            Assert.AreEqual(207, bigNumbers[15]);

        }

        [TestMethod]

        public void TestChangeToBiggerNumbersWhenMirrorIsBest()
        {
            Mirror.ChooseMirrorType(MirrorType.Best);

            int number = 7;
            List<int> bigNumbers = number.ChangeToBiggerNumbers(); 

            Assert.AreEqual(7, bigNumbers[0]);
            Assert.AreEqual(15, bigNumbers[1]);
            Assert.AreEqual(143, bigNumbers[3]);

        }

        [TestMethod]

        public void TestChangeToSmallerNumber()
        {
            Mirror.ChooseMirrorType(MirrorType.Best);

            Coordinate coordinate = new(15, Individual.N2);
            while (coordinate.X >= Mirror.SmallerN2)
                coordinate.X -= Mirror.SmallerN2;
            while (coordinate.Y >= Mirror.SmallerN1)
                coordinate.Y -= Mirror.SmallerN1;

            Assert.AreEqual(7,coordinate.X * Mirror.SmallerN2 + coordinate.Y);


            coordinate = new(255, Individual.N2);
            while (coordinate.X >= Mirror.SmallerN2)
                coordinate.X -= Mirror.SmallerN2;
            while (coordinate.Y >= Mirror.SmallerN1)
                coordinate.Y -= Mirror.SmallerN1;

            Assert.AreEqual(63, coordinate.X * Mirror.SmallerN2 + coordinate.Y);

        }

        [TestMethod]
        public void TestMirrorsThrewCreatePopulation()
        {
            foreach (var mirrorType in Enum.GetValues(typeof(MirrorType)))
            {
                Algorithm = new(N1, N2, M, PopulationSize,10,80,10,100,(LocalSearchType)0,IndividualType.Mirror);

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
