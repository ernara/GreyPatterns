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

		public Rectangle[,] CurrentMatrix = new Rectangle[16, 16];

		private const double spacing = 1.0;

		private readonly Brush ON = Brushes.Black;
		private readonly Brush OFF = Brushes.LightGray;


		private readonly DispatcherTimer timer = new DispatcherTimer();

		private async void NewAlgorithm(object sender, RoutedEventArgs e)
		{
			Algorithm Algorithm = new Algorithm(Convert.ToInt32(N1_Text.Text), Convert.ToInt32(N2_Text.Text), Convert.ToInt32(M_Text.Text), Convert.ToInt32(PopulationSize_Text.Text));
			CreateCanvas();

			Stopwatch stopwatch = new();

			stopwatch.Start();

			for (int i = 0; i < Convert.ToInt32(Iterations_Text.Text) || stopwatch.ElapsedMilliseconds < Convert.ToInt32(Time_Text.Text) * 1000; i++)
			{
                Algorithm.Next();

			    Paint();
                
                await Task.Delay(1);
            }

			stopwatch.Stop();

        }


		private void R_MouseEnter(object sender, MouseEventArgs e)
		{
			coordinate.Content = Mouse.GetPosition(Board);

			bool leftButton = e.LeftButton == MouseButtonState.Pressed;
			if (!leftButton) return;

			Rectangle pixel = (Rectangle)sender;

			pixel.Fill = pixel.Fill == ON ? OFF : ON;
		}


		private void gameSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			var slider = sender as Slider;
			double value = slider.Value;

			timer.Interval = TimeSpan.FromMilliseconds(1000 - Math.Ceiling(value * 100));
		}

        private void Board_Loaded(object sender, RoutedEventArgs e)
        {
			Algorithm Algorithm = new Algorithm(Convert.ToInt32(N1_Text.Text), Convert.ToInt32(N2_Text.Text), Convert.ToInt32(M_Text.Text), 1);

			CreateCanvas();


		}
	}
}
