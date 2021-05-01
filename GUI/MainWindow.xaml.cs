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
        public bool BiggerClicked = false;


        public MainWindow()
        {
            InitializeComponent();

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
        public List<double> dataY2 = new();

        public Rectangle[,] CurrentMatrix;

        public Rectangle[,] BestMatrix;

        private const double spacing = 1.0;

        private readonly Brush ON = Brushes.Black;
        private readonly Brush OFF = Brushes.LightGray;
        public Algorithm Algorithm;

        private async void NewAlgorithm(object sender, RoutedEventArgs e)
        {
            dataX = new();
             dataY = new();
            dataY2 = new();
            switch ((AlgorithmType)AAlgorithmType.SelectedIndex)
            {
                case (AlgorithmType.Strait):
                    Algorithm = new Strait(Convert.ToInt32(N_Text.Text),
                        Convert.ToInt32(M_Text.Text));
                    break;
                case (AlgorithmType.Custom):
                    Algorithm = new(Convert.ToInt32(N_Text.Text), Convert.ToInt32(M_Text.Text), Convert.ToInt32(PopulationSize_Text.Text),
                        Convert.ToInt32(OldPopulationSize_Text.Text), Convert.ToInt32(CrossPopulationSize_Text.Text), Convert.ToInt32(NewPopulationSize_Text.Text),
                        (IndividualType)ANewIndividualType.SelectedIndex, (CrossoverType)ACrossoverType.SelectedIndex, (RandomChooseType)AIndividualChooserType.SelectedIndex,
                        (MutateType)AMutateType.SelectedIndex, (LocalSearchType)ALocalSearchType.SelectedIndex, (MirrorType)AMirrorType.SelectedIndex, 100, 100,
                        new MutateFlags((bool)Mutate0.IsChecked, (bool)Mutate1.IsChecked, (bool)Mutate2.IsChecked, (bool)Mutate3.IsChecked, (bool)PMutate.IsChecked),
                        new LocalSearchFlags((bool)LocalSearch0.IsChecked, (bool)LocalSearch1.IsChecked, (bool)LocalSearch2.IsChecked, (bool)LocalSearch3.IsChecked,
                        (bool)true));
                    break;
                default:
                    throw new Exception("Wrong value");
            }

            


            Bigger.IsEnabled = true;

            CreateCanvas();
            Paint();

            await Do();
           

        }

        private async Task Do()
        {
            Mute();

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

                dataX.Add(dataX.Count);
                dataY.Add(Algorithm.BestIndividual.Fitness);
                dataY2.Add(Algorithm.Population[0].Fitness);

                await Task.Delay((int)Math.Max(1.0, 1000.0 / FPS.Value - stopwatchFPS.ElapsedMilliseconds));
                stopwatchFPS.Restart();
            }
            Chart.Reset();
            Chart.Plot.Title("Fitness Results");
            Chart.Plot.XLabel("Generation");
            Chart.Plot.YLabel("Fitness");

            string[] labels = { "Current", "Best",  };
            Chart.Plot.Legend(true,ScottPlot.Alignment.UpperRight);
            Chart.Plot.AddScatter(dataX.ToArray(), dataY.ToArray(),null,1,5,ScottPlot.MarkerShape.filledCircle,ScottPlot.LineStyle.Solid, "Best");
            Chart.Plot.AddScatter(dataX.ToArray(), dataY2.ToArray(), null, 1, 5, ScottPlot.MarkerShape.filledCircle, ScottPlot.LineStyle.Solid, "Current");

            Unmute();

            stopwatch.Stop();

            
        }

        private async void NextAlgorithm(object sender, RoutedEventArgs e)
        {
            if (BiggerClicked==true)
            {
                CreateBoard();
                BiggerClicked = false;
            }
            await Do();
        }

        private void StopAlgorithm(object sender, RoutedEventArgs e)
        {
            DontStop = false;
        }

        private void BiggerAlgorithm(object sender, RoutedEventArgs e)
        {
            CreateBoardBigger();
            BiggerClicked = true;
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
            Algorithm Algorithm = new(Convert.ToInt32(N_Text.Text), Convert.ToInt32(M_Text.Text));
            CreateCanvas();
        }

        private void Board_Loaded2(object sender, RoutedEventArgs e)
        {
        }

        private void Mute()
        {
            New.IsEnabled = false;
            Next.IsEnabled = false;
            Stop.IsEnabled = true;
            Bigger.IsEnabled = false;
        }

        private void Unmute()
        {
            New.IsEnabled = true;
            Next.IsEnabled = true;
            Stop.IsEnabled = false;
            Bigger.IsEnabled = true;
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

        private void AAlgorithmType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((AlgorithmType)AAlgorithmType.SelectedIndex == AlgorithmType.Custom)
            {
                Flags.IsEnabled = true;
                OptionalParameters.IsEnabled = true;
                PopulationSizeIsOneParameters.IsEnabled = true;
            }
            else
            {
                Flags.IsEnabled = false;
                OptionalParameters.IsEnabled = false;
                PopulationSizeIsOneParameters.IsEnabled = false;
            }
        }

        private void PopulationSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (OldPopulationSize!=null)
            {
                OldPopulationSize.Value = (int)PopulationSize.Value / 10;
                NewPopulationSize.Value = (int)PopulationSize.Value / 10;
                CrossPopulationSize.Value = PopulationSize.Value - OldPopulationSize.Value - NewPopulationSize.Value;
            }
           
        }
    }
}
