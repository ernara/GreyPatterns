using OxyPlot;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Algorithms
{
    public partial class MainWindow : Window
    {
        public int N1;
        public int N2;
        public int M;
        public int Time;
        public int Generations;

        public int PopSize;
        public int OldPopulationSize;
        public int CrossPopulationSize;
        public int NewPopulationSize;
        public int MutateChance;

        public Cell[,] CurrentCells;
        public Cell[,] BestCells;

        public PlotModel PlotModel { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            AlgorithType.ItemsSource = Enum.GetValues(typeof(AlgorithmType));

            AlgorithType.SelectedIndex = 5;

        }

        private async void NewAlgorithm(object sender, RoutedEventArgs e)
        {
            SetUp();

            AAlgorithmOLDNX AAlgorithm;

            switch (IsGeneticAlgorithm())
            {
                case true:

                    switch (IsGeneticV1())
                    {
                        case true:
                            AAlgorithm = AlgorithmCreator.Create(N1, N2, M, (AlgorithmType)AlgorithType.SelectedValue, false, 
                               Convert.ToInt32(PopSize_Text.Text), Convert.ToInt32(OldPopulationSize_Text.Text), Convert.ToInt32(CrossPopulationSize_Text.Text),
                               Convert.ToInt32(NewPopulationSize_Text.Text), Convert.ToInt32(MutateChance_Text.Text));
                            break;
                        case false:
                            AAlgorithm = AlgorithmCreator.Create(N1, N2, M, (AlgorithmType)AlgorithType.SelectedValue, (bool)Local_Search_Check.IsChecked, 
                                Convert.ToInt32(PopSize_Text.Text), Convert.ToInt32(OldPopulationSize_Text.Text), 
                                Convert.ToInt32(CrossPopulationSize_Text.Text), 
                                Convert.ToInt32(NewPopulationSize_Text.Text), Convert.ToInt32(MutateChance_Text.Text));
                            break;
                    }
                    
                    break;
                case false:
                    AAlgorithm = AlgorithmCreator.Create(N1, N2, M, (AlgorithmType)AlgorithType.SelectedValue, (bool)Local_Search_Check.IsChecked);
                    break;
            }


            Stopwatch stopwatch = new();
            stopwatch.Start();

            Stopwatch stopwatchFPS = new();
            stopwatchFPS.Start();

            DrawLines();
            InitializeCells();

            List<DataPoint> dataPoints = new();


            for (int generation = 0; stopwatch.ElapsedMilliseconds < Time * 1000 || generation < Generations; generation++)
            {
                AAlgorithm.Next();
                DrawCells(CurrentCells, AAlgorithm.Population[0].Genes);
                DrawCells(BestCells, AAlgorithm.BestIndividual.Genes);

                dataPoints.Add(new DataPoint(generation * 2, generation));

                await Task.Delay(Convert.ToInt32(Math.Max(1.0,1000.0/FPS.Value - stopwatchFPS.ElapsedMilliseconds)));
                stopwatchFPS.Restart();
            }
        }

        private void SetUp()
        {
            N1 = Convert.ToInt32(N1_Text.Text);
            N2 = Convert.ToInt32(N2_Text.Text);
            M = Convert.ToInt32(M_Text.Text);
            Time = Convert.ToInt32(Time_Text.Text);
            Generations = Convert.ToInt32(Generations_Text.Text);

            //PopSize = Convert.ToInt32(PopSize_Text.Text);
            //OldPopulationSize = Convert.ToInt32(OldPopulationSize_Text);
            //CrossPopulationSize = Convert.ToInt32(CrossPopulationSize_Text);
            //NewPopulationSize = Convert.ToInt32(NewPopulationSize_Text);
            //MutateChance = Convert.ToInt32(MutateChance_Text);
        }

        private void DrawLines()
        {
            CurrentCanvas.Children.Clear();
            BestCanvas.Children.Clear();

            Line line;
            Line line2;

            for (var i = 1; i < N1; i++)
            {
                line = new Line()
                {
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    X1 = CurrentCanvas.Width / N1 * i,
                    X2 = CurrentCanvas.Width / N1 * i,
                    Y1 = 0,
                    Y2 = CurrentCanvas.Height
                };

                line2 = new Line()
                {
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    X1 = BestCanvas.Width / N1 * i,
                    X2 = BestCanvas.Width / N1 * i,
                    Y1 = 0,
                    Y2 = BestCanvas.Height
                };

                CurrentCanvas.Children.Add(line);
                BestCanvas.Children.Add(line2);

            }

            for (int i = 1; i < N2; i++)
            {
                line = new Line()
                {
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    X1 = 0,
                    X2 = CurrentCanvas.Width,
                    Y1 = CurrentCanvas.Height / N2 * i,
                    Y2 = CurrentCanvas.Height / N2 * i,
                };

                line2 = new Line()
                {
                    StrokeThickness = 1,
                    Stroke = Brushes.Black,
                    X1 = 0,
                    X2 = BestCanvas.Width,
                    Y1 = BestCanvas.Height / N2 * i,
                    Y2 = BestCanvas.Height / N2 * i,
                };

                CurrentCanvas.Children.Add(line);
                BestCanvas.Children.Add(line2);

            }

        }

        private void InitializeCells()
        {
            CurrentCells = new Cell[N1, N2];
            BestCells = new Cell[N1, N2];


            var cellWidth = CurrentCanvas.Width / N1;
            var cellHeight = CurrentCanvas.Height / N2;

            for (var x = 0; x < N1; x++)
            {
                for (var y = 0; y < N2; y++)
                {
                    CurrentCells[x, y] = new Cell();
                    BestCells[x, y] = new Cell();

                    CurrentCells[x, y].Rect = new Rectangle()
                    {
                        Fill = Brushes.White,
                        Height = cellHeight - 4,
                        Width = cellWidth - 4,
                        StrokeThickness = 0
                    };

                    BestCells[x, y].Rect = new Rectangle()
                    {
                        Fill = Brushes.White,
                        Height = cellHeight - 4,
                        Width = cellWidth - 4,
                        StrokeThickness = 0
                    };

                    CurrentCanvas.Children.Add(CurrentCells[x, y].Rect);
                    BestCanvas.Children.Add(BestCells[x, y].Rect);

                    Canvas.SetTop(CurrentCells[x, y].Rect, y * cellHeight + 2);
                    Canvas.SetLeft(CurrentCells[x, y].Rect, x * cellWidth + 2);

                    Canvas.SetTop(BestCells[x, y].Rect, y * cellHeight + 2);
                    Canvas.SetLeft(BestCells[x, y].Rect, x * cellWidth + 2);
                }
            }

            
        }

        private void DrawCells(Cell[,] Cells, List<int> Genes )
        {
            Coordinate coordinate;

            for (int i = 0; i < N1; i++)
            {
                for (int j = 0; j < N2; j++)
                {
                    Cells[i, j].SetWhite();
                }
            }

            for (int i = 0; i < Genes.Take(M).ToList().Count; i++)
            {
                coordinate = new Coordinate(Genes[i], N2);
                Cells[coordinate.X, coordinate.Y].SetBlack();
            }
        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {

        }

        private void AlgorithType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (IsGeneticAlgorithm())
            {
                case true:
                    OptionalParameters.IsEnabled = true;
                    break;
                case false:
                    OptionalParameters.IsEnabled = false;
                    Local_Search_Check.IsEnabled = true;
                    break;
            };

            switch (IsGeneticV1())
            {
                case true:
                    Local_Search_Check.IsEnabled = false;
                    break;
                case false:
                    Local_Search_Check.IsEnabled = true;
                    break;
            }
        }

        private bool IsGeneticAlgorithm()
        {
            return ((AlgorithmType)AlgorithType.SelectedValue == AlgorithmType.GeneticV1 ||
                   (AlgorithmType)AlgorithType.SelectedValue == AlgorithmType.GeneticV2 ||
                   (AlgorithmType)AlgorithType.SelectedValue == AlgorithmType.GeneticV3);
        }

        private bool IsGeneticV1()
        {
            return (AlgorithmType)AlgorithType.SelectedValue == AlgorithmType.GeneticV1;
        }


    }
}
