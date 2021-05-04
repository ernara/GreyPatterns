using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class MirrorTest : ABaseClassTest
    {

        [TestMethod]
        public void TestMakeMirrored()
        {
            foreach (var mirrorType in Enum.GetValues(typeof(MirrorType)))
            {
                Mirror.ChooseMirrorType((MirrorType)mirrorType);
                IndividualFitnessCalculator.SetUpParameters();

                Assert.IsTrue(Mirror.SmallerN2 < Individual.N2);
                Assert.IsTrue(Mirror.SmallerM < M);

                Individual.MakeMirrored();

                Assert.AreNotEqual(Individual.ToString(), Algorithm.BestIndividual.ToString());
                Assert.IsTrue(Individual.Fitness > 0);
            }
        }


        private int ChangeToSmallerNumber(int number)
        {
            Coordinate coordinate = new(number, Individual.N2);
            while (coordinate.X >= SmallerN2)
                coordinate.X -= SmallerN2;
            while (coordinate.Y >= SmallerN2)
                coordinate.Y -= SmallerN2;

            return coordinate.X * SmallerN2 + coordinate.Y;
        }

        [TestMethod]
        private static void TestChangeToSmallMirror()
        {
            foreach (var mirrorType in Enum.GetValues(typeof(MirrorType)))
            {
                Mirror.ChooseMirrorType((MirrorType)mirrorType);

                Individual.Genes = Individual.Genes.Take(Individual.M).Union(Enumerable.Range(0, SmallerN)).Distinct().ToList();
                Individual.CalculateFitness();

                Assert.IsTrue(Individual.Fitness > 0);
                Individual.Genes.Sort();
                Assert.AreEqual(0, Individual.Genes[0]);
                Assert.AreEqual(Mirror.SmallerN-1, Individual.Genes[^1]);
            }
        }

        [TestMethod]
        private static void TestChangeToBigMirror()
        {
            List<int> genes = new();

            foreach (var mirrorType in Enum.GetValues(typeof(MirrorType)))
            {
                Mirror.ChooseMirrorType((MirrorType)mirrorType);

                for (int i = 0; i < SmallerM; i++)
                {
                    genes.AddRange(Individual.Genes[i].ChangeToBiggerNumbers());
                }

                Individual.Genes = genes.Take(Individual.M).Union(Enumerable.Range(0, Individual.N)).Distinct().ToList();
                Individual.CalculateFitness();

                Assert.IsTrue(Individual.Fitness > 0);
                Individual.Genes.Sort();
                Assert.AreEqual(0, Individual.Genes[0]);
                Assert.AreEqual(Individual.N - 1, Individual.Genes[^1]);
            }
        }

        [TestMethod]
        public void TestChangeToSmallerNumber()
        {
            Mirror.ChooseMirrorType(MirrorType.Best);

            Coordinate coordinate = new(15, Individual.N2);
            while (coordinate.X >= Mirror.SmallerN2)
                coordinate.X -= Mirror.SmallerN2;
            while (coordinate.Y >= Mirror.SmallerN2)
                coordinate.Y -= Mirror.SmallerN2;

            Assert.AreEqual(7, coordinate.X * Mirror.SmallerN2 + coordinate.Y);

            coordinate = new(255, Individual.N2);
            while (coordinate.X >= Mirror.SmallerN2)
                coordinate.X -= Mirror.SmallerN2;
            while (coordinate.Y >= Mirror.SmallerN2)
                coordinate.Y -= Mirror.SmallerN2;

            Assert.AreEqual(63, coordinate.X * Mirror.SmallerN2 + coordinate.Y);

            Mirror.ChooseMirrorType(MirrorType.Small);

            coordinate = new(15, Individual.N2);
            while (coordinate.X >= Mirror.SmallerN2)
                coordinate.X -= Mirror.SmallerN2;
            while (coordinate.Y >= Mirror.SmallerN2)
                coordinate.Y -= Mirror.SmallerN2;

            Assert.AreEqual(3, coordinate.X * Mirror.SmallerN2 + coordinate.Y);


            coordinate = new(255, Individual.N2);
            while (coordinate.X >= Mirror.SmallerN2)
                coordinate.X -= Mirror.SmallerN2;
            while (coordinate.Y >= Mirror.SmallerN2)
                coordinate.Y -= Mirror.SmallerN2;

            Assert.AreEqual(15, coordinate.X * Mirror.SmallerN2 + coordinate.Y);

        }


        [TestMethod]
        public void TestChangeToBiggerNumbers()
        {
            Mirror.ChooseMirrorType(MirrorType.Small);
            int number = 3;
            List<int> bigNumbers = number.ChangeToBiggerNumbers(); 
            Assert.AreEqual(3,bigNumbers[0] );
            Assert.AreEqual(7,bigNumbers[1] );
            Assert.AreEqual(11,bigNumbers[2] );
            Assert.AreEqual(15,bigNumbers[3] );
            Assert.AreEqual(207, bigNumbers[15]);

            Mirror.ChooseMirrorType(MirrorType.Best);
            number = 7;
            bigNumbers = number.ChangeToBiggerNumbers();

            Assert.AreEqual(7, bigNumbers[0]);
            Assert.AreEqual(15, bigNumbers[1]);
            Assert.AreEqual(143, bigNumbers[3]);
        }

        [TestMethod]
        public void TestSmallMirror()
        {
            while (SmallerN2 > 3 && SmallerM > 3)
            {
                SmallerN2 /= 2;
                SmallerM /= 4;
            }

            Assert.AreEqual(4, SmallerN2);
            Assert.AreEqual(2, SmallerM);

        }

        [TestMethod]
        public void TestBestMirror()
        {
            while (SmallerN2 > 7 && SmallerM > 19)
            {
                SmallerN2 /= 2;
                SmallerM /= 4;
            }

            Assert.AreEqual(8, SmallerN2);
            Assert.AreEqual(8, SmallerM);
        }
    }
}
