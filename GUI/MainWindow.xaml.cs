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
        public bool DontStop;
        public MainWindow()
        {
            InitializeComponent();
            double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };
            Chart.Plot.AddScatter(dataX, dataY);

            AAlgorithmType.ItemsSource = Enum.GetValues(typeof(AlgorithmType));
            AAlgorithmType.SelectedIndex = 0;

            ANewIndividualType.ItemsSource = Enum.GetValues(typeof(IndividualType));
            ANewIndividualType.SelectedIndex = 0;

            ACrossoverType.ItemsSource = Enum.GetValues(typeof(CrossoverType));
            ACrossoverType.SelectedIndex = 0;

            AIndividualChooserType.ItemsSource = Enum.GetValues(typeof(RandomChooseType));
            AIndividualChooserType.SelectedIndex = 0;

            AMirrorType.ItemsSource = Enum.GetValues(typeof(MirrorType));
            AMirrorType.SelectedIndex = 0;

            AMutateType.ItemsSource = Enum.GetValues(typeof(MutateType));
            AMutateType.SelectedIndex = 0;

            ALocalSearchType.ItemsSource = Enum.GetValues(typeof(LocalSearchType));
            ALocalSearchType.SelectedIndex = 0;

        }

        public List<double> dataX = new();
        public List<double> dataY = new();
        public List<double> dataX2 = new();
        public List<double> dataY2 = new();

        public Rectangle[,] CurrentMatrix;

        public Rectangle[,] BestMatrix;

        private const double spacing = 1.0;

        private readonly Brush ON = Brushes.Black;
        private readonly Brush OFF = Brushes.LightGray;
        public Algorithm Algorithm;

        private async void NewAlgorithm(object sender, RoutedEventArgs e)
        {

            switch ((AlgorithmType)AAlgorithmType.SelectedIndex)
            {
                case (AlgorithmType.Strait):
                    Algorithm = new Strait(Convert.ToInt32(N1_Text.Text), Convert.ToInt32(N2_Text.Text),
                        Convert.ToInt32(M_Text.Text), Convert.ToInt32(PopulationSize_Text.Text));
                    break;
                case (AlgorithmType.Custom):
                    Algorithm = new(Convert.ToInt32(N1_Text.Text), Convert.ToInt32(N2_Text.Text),
                        Convert.ToInt32(M_Text.Text), Convert.ToInt32(PopulationSize_Text.Text),
                        new MutateFlags(Mutate0.IsChecked, Mutate1.IsChecked, Mutate2.IsChecked, Mutate3.IsChecked, PMutate.IsChecked),
                        new LocalSearchFlags(LocalSearch0.IsChecked, LocalSearch1.IsChecked, LocalSearch2.IsChecked, LocalSearch3.IsChecked, PLocalSearch.IsChecked),
                        Convert.ToInt32(OldPopulationSize_Text.Text), Convert.ToInt32(CrossPopulationSize_Text.Text),
                        Convert.ToInt32(NewPopulationSize_Text.Text), Convert.ToInt32(MutateChance_Text.Text),
                        (LocalSearchType)ALocalSearchType.SelectedIndex, (IndividualType)ANewIndividualType.SelectedIndex,
                        (MirrorType)AMirrorType.SelectedIndex, (RandomChooseType)AIndividualChooserType.SelectedIndex,
                        (CrossoverType)ACrossoverType.SelectedIndex, (MutateType)AMutateType.SelectedIndex);
                    break;
                default:
                    throw new Exception("Wrong value");
            }


            Bigger.IsEnabled = true;

            

            CreateCanvas();
            Paint();

            List<double> dataX = new();
            List<double> dataY = new();
            List<double> dataX2 = new();
            List<double> dataY2 = new();
            Chart.Plot.Clear();
            await Do();

        }

        private async Task Do()
        {
            DontStop = true;
            Stopwatch stopwatch = new();
            stopwatch.Start();

            Stopwatch stopwatchFPS = new();
            stopwatchFPS.Start();



            for (int i = 0; (i < Convert.ToInt32(Iterations_Text.Text) || stopwatch.ElapsedMilliseconds < Convert.ToInt32(Time_Text.Text) * 1000) && DontStop; i++)
            {
                if ((bool)PMirror.IsChecked)
                {
                    Algorithm.Population[0].MakeMirrored();
                }
                Algorithm.Next();

                Paint();

                dataX.Add(i);
                dataX2.Add(i);
                dataY.Add(Algorithm.BestIndividual.Fitness);
                dataY2.Add(Algorithm.Population[0].Fitness);

                await Task.Delay((int)Math.Max(1.0, 1000.0 / FPS.Value - stopwatchFPS.ElapsedMilliseconds));
                stopwatchFPS.Restart();
            }

            stopwatch.Stop();


        }

        private async void NextAlgorithm(object sender, RoutedEventArgs e)
        {
            await Do();
        }

        private async void StopAlgorithm(object sender, RoutedEventArgs e)
        {
            DontStop = false;
        }

        private async void BiggerAlgorithm(object sender, RoutedEventArgs e)
        {
            CreateBoardBigger();
        }


        private void R_MouseEnter(object sender, MouseEventArgs e)
        {
            bool leftButton = e.LeftButton == MouseButtonState.Pressed;
            if (!leftButton) return;

            Rectangle pixel = (Rectangle)sender;

            pixel.Fill = pixel.Fill == ON ? OFF : ON;
        }


        private void Board_Loaded(object sender, RoutedEventArgs e)
        {
            Algorithm Algorithm = new(Convert.ToInt32(N1_Text.Text), Convert.ToInt32(N2_Text.Text), Convert.ToInt32(M_Text.Text), 1);
            CreateCanvas();
        }

        private void Board_Loaded2(object sender, RoutedEventArgs e)
        {
        }

        private void PopulationSize_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (Convert.ToInt32(PopulationSize_Text.Text) > 1)
            //{
            //    Board.Visibility = Visibility.Hidden;
            //}
            //else if (Convert.ToInt32(PopulationSize_Text.Text) == 1)
            //{
            //    Board.Visibility = Visibility.Visible;
            //}
        }

        private void AAlgorithmType_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
