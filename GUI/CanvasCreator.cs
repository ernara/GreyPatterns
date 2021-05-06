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
		public int CountPainted = 0;
		public int FirstM = 32;

		public Rectangle[,] CurrentMatrix;

		public Rectangle[,] BestMatrix;

		private const double spacing = 1.0;

		private readonly Brush ON = Brushes.Black;
		private readonly Brush OFF = Brushes.LightGray;

		public void CreateCanvas()
		{
			if (CountPainted>0 && Convert.ToInt32(M_Text.Text)==CountPainted)
            {
				
			}
			else
            {
				if (Convert.ToInt32(M_Text.Text)<=0)
                {
					M_Text.Text = FirstM.ToString();
				}
				CountPainted = 0;
				CurrentMatrix = new Rectangle[Individual.N2, Individual.N2];
				BestMatrix = new Rectangle[Individual.N2, Individual.N2];
				Board.Children.RemoveRange(0, Board.Children.Count);
				Board2.Children.RemoveRange(0, Board2.Children.Count);
				CreateBoard();
				CreateBoard2();
			}
			
			
		}

		public void CreateBoard()
        {
			for (int y = 0; y < Individual.N2; y++)
			{
				for (int x = 0; x < Individual.N2; x++)
				{
					Rectangle r = new()
					{
						Width = Board.ActualWidth / Individual.N2 - spacing,
						Height = Board.ActualHeight / Individual.N2 - spacing,
						Fill = OFF
					};
					Board.Children.Add(r);

                    Canvas.SetLeft(r, x * Board.ActualWidth / Individual.N2);
                    Canvas.SetTop(r, y * Board.ActualHeight / Individual.N2);

					CurrentMatrix[y, x] = r;

					r.MouseDown += R_MouseEnter;
					r.MouseEnter += R_MouseEnter;
				}
			}
		}

		private void CreateBoard2()
		{
			for (int y = 0; y < Individual.N2; y++)
			{
				for (int x = 0; x < Individual.N2; x++)
				{
					Rectangle r = new()
					{
						Width = Board2.ActualWidth / Individual.N2 - spacing,
						Height = Board2.ActualHeight / Individual.N2 - spacing,
						Fill = OFF
					};
					Board2.Children.Add(r);

					Canvas.SetLeft(r, Board.ActualWidth + 20 + x * Board2.ActualWidth / Individual.N2);
					Canvas.SetTop(r, y * Board2.ActualHeight / Individual.N2);

					BestMatrix[y, x] = r;

				}
			}
		}

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

				}
			}

			PaintBiggerBoard();

		}

		private void Board_Loaded(object sender, RoutedEventArgs e)
		{
			Algorithm Algorithm = new(Convert.ToInt32(N_Text.Text), Convert.ToInt32(M_Text.Text));
			CreateCanvas();
		}

		private void Board_Loaded2(object sender, RoutedEventArgs e)
		{
			
		}

	}
}
