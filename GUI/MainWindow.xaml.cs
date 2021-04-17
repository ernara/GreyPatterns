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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		Algorithm Algorithm = new Algorithm(16, 16, 32, 100, new MutateFlags(), new LocalSearchFlags());

		private const int gameWidth = 16;

		private const int gameHeight = gameWidth;
		private const double spacing = 1.0;

		private readonly Brush ON = Brushes.Black;
		private readonly Brush OFF = Brushes.LightGray;

		private int roundCount = 0;

		public Rectangle[,] matrix = new Rectangle[gameHeight, gameWidth];
		private readonly DispatcherTimer timer = new DispatcherTimer();

		private async void NewAlgorithm(object sender, RoutedEventArgs e)
		{
            for (int ii = 0; ii < 1000000; ii++)
            {
				roundCount++;
				Algorithm.Next();

				Coordinate coordinate;

				for (int i = 0; i < Individual.M; i++)
				{
					coordinate = new Coordinate(Algorithm.Population[99].Genes[i], Individual.N2);
					matrix[coordinate.Y, coordinate.X].Fill = ON;
				}

				for (int i = Individual.M; i < Individual.N; i++)
				{
					coordinate = new Coordinate(Algorithm.Population[99].Genes[i], Individual.N2);
					matrix[coordinate.Y, coordinate.X].Fill = OFF;
				}
				await Task.Delay(1);

			}


		}

		private void buildCanvas()
		{
			for (int y = 0; y < gameHeight; y++)
			{
				for (int x = 0; x < gameWidth; x++)
				{
					Rectangle r = new Rectangle
					{
						Width = Board.ActualWidth / gameWidth - spacing,
						Height = Board.ActualHeight / gameHeight - spacing,
						Fill = OFF
					};
					Board.Children.Add(r);

					Canvas.SetLeft(r, x * Board.ActualWidth / gameWidth);
					Canvas.SetTop(r, y * Board.ActualHeight / gameHeight);

					matrix[y, x] = r;

					r.MouseDown += R_MouseEnter;
					r.MouseEnter += R_MouseEnter;
				}
			}
		}

		private void R_MouseEnter(object sender, MouseEventArgs e)
		{
			coordinate.Content = Mouse.GetPosition(Board);

			bool leftButton = e.LeftButton == MouseButtonState.Pressed;
			if (!leftButton) return;

			Rectangle pixel = (Rectangle)sender;

			pixel.Fill = pixel.Fill == ON ? OFF : ON;
		}

		private void game_Loaded(object sender, RoutedEventArgs e) => buildCanvas();


		private void gameSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			var slider = sender as Slider;
			double value = slider.Value;

			timer.Interval = TimeSpan.FromMilliseconds(1000 - Math.Ceiling(value * 100));
		}
	}
}
