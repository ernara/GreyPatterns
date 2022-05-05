using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public void PaintBoards()
        {
            PaintBoard();
            PaintBoard2();
        }

        private void PaintBoard()
        {
            Coordinate coordinate;

            int Show = Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize);

            for (int i = 0; i < Individual.N; i++)
            {
                coordinate = new Coordinate(i, Individual.N2);
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = OFF1;
            }

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(Algorithm.Population[Show].Genes[i], Individual.N2);
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = ON1;
            }
        }

        private void PaintIndividualBySelectedIndividual()
        {
            Coordinate coordinate;

            int Show = Individuals.SelectedIndex;

            for (int i = 0; i < Individual.N; i++)
            {
                coordinate = new Coordinate(i, Individual.N2);
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = OFF1;
            }

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(Algorithm.Population[Show].Genes[i], Individual.N2);
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = ON1;
            }
        }

        private void PaintIndividualBySelectedHistory()
        {
            Coordinate coordinate;

            string Show = Historys.Text;

            for (int i = 0; i < Individual.N; i++)
            {
                coordinate = new Coordinate(i, Individual.N2);
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = OFF1;
            }

            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());

            List<FileInfo> files = new(dir.GetFiles("22*", SearchOption.TopDirectoryOnly));

            int neededIndex= Historys.Items.Count - Historys.SelectedIndex - 1;

            Result result = new Result();

            for (int i =0  - 1; i < files.Count; i++)
            {
                if (i==neededIndex)
                {
                    result = Result.ReadFile(files[i].Name);
                    break;
                }
            }

            //TODO: uztrigs sekantis loopas, jei N ar M bus kitoks nei globalus N ar M.

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(result.Genes[i], Individual.N2); 
                CurrentMatrix[coordinate.X, coordinate.Y].Fill = ON1;
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
                    CurrentMatrix[coordinate.X, coordinate.Y].Fill = ON1;
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
                BestMatrix[coordinate.X, coordinate.Y].Fill = OFF1;
            }

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(Algorithm.BestIndividual.Genes[i], Individual.N2);
                BestMatrix[coordinate.X, coordinate.Y].Fill = ON1;
            }

            //Trace.WriteLine(Algorithm.BestIndividual);


        }


    }
}
