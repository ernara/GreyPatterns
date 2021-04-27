using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Algorithms;

namespace GUI
{
    public partial class MainWindow : Window
    {
        public void Paint()
        {
            PaintBoard();
            PaintBoard2();
        }

        private void PaintBoard()
        {
            Coordinate coordinate;

            for (int i = Individual.M; i < Individual.N; i++)
            {
                coordinate = new Coordinate(Algorithm.Population[0].Genes[i], Individual.N2);
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = OFF;
            }

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(Algorithm.Population[0].Genes[i], Individual.N2);
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = ON;
            }

        }

        private void PaintBiggerBoard()
        {
            List<int> MultiGenes = new(Algorithm.BestIndividual.Genes.Take(Individual.M));
            List<int> BiggerGenes;
            Coordinate coordinate;

            for (int i = 0; i < MultiGenes.Count; i++)
            {
                BiggerGenes = new List<int>(ChangeToBiggerNumbers(MultiGenes[i]));
                for (int j = 0; j < BiggerGenes.Count; j++)
                {
                    coordinate = new Coordinate(BiggerGenes[j], CurrentMatrix.GetLength(0));
                    CurrentMatrix[coordinate.X, coordinate.Y].Fill = ON;
                }
            }
        }

        public List<int> ChangeToBiggerNumbers(int number)
        {
            List<int> numbers = new();
            Coordinate coordinate = new(number, Individual.N2);

            for (int i = 0; i < CurrentMatrix.GetLength(0) / Individual.N2; i++)
            {
                for (int j = 0; j < CurrentMatrix.GetLength(1) / Individual.N2; j++)
                {
                    numbers.Add((coordinate.X + i * Individual.N2) * CurrentMatrix.GetLength(0) + coordinate.Y + Individual.N2 * j); 
                }
            }

            Trace.WriteLine($"hi: {CurrentMatrix.GetLength(0) / Individual.N2}");

            return numbers;
        }

        private void PaintBoard2()
        {
            Coordinate coordinate;

            for (int i = Individual.M; i < Individual.N; i++)
            {
                coordinate = new Coordinate(Algorithm.BestIndividual.Genes[i], Individual.N2);
                BestMatrix[coordinate.X, coordinate.Y].Fill = OFF;
            }

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(Algorithm.BestIndividual.Genes[i], Individual.N2);
                BestMatrix[coordinate.X, coordinate.Y].Fill = ON;
            }

            //Trace.WriteLine(Algorithm.BestIndividual);


        }


    }
}
