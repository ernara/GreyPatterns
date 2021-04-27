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
		public void CreateCanvas()
		{
			CreateBoard();
			CreateBoard2();
		}

		private void CreateBoard()
        {
			CurrentMatrix = new Rectangle[Individual.N1, Individual.N2];

			Board.Children.RemoveRange(0, Board.Children.Count);

			for (int y = 0; y < Individual.N1; y++)
			{
				for (int x = 0; x < Individual.N2; x++)
				{
					Rectangle r = new()
					{
						Width = Board.ActualWidth / Individual.N1 - spacing,
						Height = Board.ActualHeight / Individual.N2 - spacing,
						Fill = OFF
					};
					Board.Children.Add(r);

                    Canvas.SetLeft(r, x * Board.ActualWidth / Individual.N1);
                    Canvas.SetTop(r, y * Board.ActualHeight / Individual.N2);

					CurrentMatrix[y, x] = r;

					r.MouseDown += R_MouseEnter;
					r.MouseEnter += R_MouseEnter;
				}
			}
		}

		private void CreateBoard2()
		{
			BestMatrix = new Rectangle[Individual.N1, Individual.N2];

			Board2.Children.RemoveRange(0, Board2.Children.Count);

			for (int y = 0; y < Individual.N1; y++)
			{
				for (int x = 0; x < Individual.N2; x++)
				{
					Rectangle r = new()
					{
						Width = Board2.ActualWidth / Individual.N1 - spacing,
						Height = Board2.ActualHeight / Individual.N2 - spacing,
						Fill = OFF
					};
					Board2.Children.Add(r);

					Canvas.SetLeft(r, Board.ActualWidth + 20 + x * Board2.ActualWidth / Individual.N1);
					Canvas.SetTop(r, y * Board2.ActualHeight / Individual.N2);

					BestMatrix[y, x] = r;

					r.MouseDown += R_MouseEnter;
					r.MouseEnter += R_MouseEnter;
				}
			}
		}

		//Todo: bigeer board
		private void CreateBoardBigger()
        {
			int size = CurrentMatrix.GetLength(0) * 2;

			CurrentMatrix = new Rectangle[size, size];

			Board.Children.RemoveRange(0, Board.Children.Count);

			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					Rectangle r = new()
					{
						Width = Board.ActualWidth / size - spacing,
						Height = Board.ActualHeight / size - spacing,
						Fill = OFF
					};
					Board.Children.Add(r);

					Canvas.SetLeft(r, x * Board.ActualWidth / size);
					Canvas.SetTop(r, y * Board.ActualHeight / size);

					CurrentMatrix[y, x] = r;

					r.MouseDown += R_MouseEnter;
					r.MouseEnter += R_MouseEnter;
				}
			}

			PaintBiggerBoard();

		}

	}
}
