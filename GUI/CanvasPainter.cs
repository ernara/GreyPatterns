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

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(Algorithm.Population[0].Genes[i], Individual.N2);
                CurrentMatrix[coordinate.Y, coordinate.X].Fill = ON;
            }

            for (int i = Individual.M; i < Individual.N; i++)
            {
                coordinate = new Coordinate(Algorithm.Population[0].Genes[i], Individual.N2);
                CurrentMatrix[coordinate.Y, coordinate.X].Fill = OFF;
            }
        }

        private void PaintBoard2()
        {
            Coordinate coordinate;

            for (int i = 0; i < Individual.M; i++)
            {
                coordinate = new Coordinate(Algorithm.BestIndividual.Genes[i], Individual.N2);
                BestMatrix[coordinate.Y, coordinate.X].Fill = ON;
            }

            for (int i = Individual.M; i < Individual.N; i++)
            {
                coordinate = new Coordinate(Algorithm.BestIndividual.Genes[i], Individual.N2);
                BestMatrix[coordinate.Y, coordinate.X].Fill = OFF;
            }
        }


    }
}
