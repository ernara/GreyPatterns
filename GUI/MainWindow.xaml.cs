﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Windows.Threading;
using Algorithms;

namespace GUI
{
    public partial class MainWindow : Window
    {
        public Algorithm Algorithm;

        public bool DontStop;
        public bool BiggerClicked = false;
        public bool NotInProcess = true;

        public int N;
        public int M;

        public MainWindow()
        {
            InitializeComponent();

            AAlgorithmType.ItemsSource = Enum.GetValues(typeof(AlgorithmType));
            AAlgorithmType.SelectedIndex = 1;

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

            AShowingType.ItemsSource = new string[] { "AllSteps", "Fast", "SuperFast" };
            AShowingType.SelectedIndex = 0;

            WhiteCellsBy.Items.Add("By Cells");
            WhiteCellsBy.Items.Add("By Width");
            WhiteCellsBy.SelectedIndex = 0;


            BlackCellsBy.Items.Add("By Cells");
            BlackCellsBy.Items.Add("By Width");
            BlackCellsBy.SelectedIndex = 0;

            OldPopulationSize.Value = (int)PopulationSize.Value / 10;
            NewPopulationSize.Value = (int)PopulationSize.Value / 10;
            CrossPopulationSize.Value = PopulationSize.Value - OldPopulationSize.Value - NewPopulationSize.Value;
            //this.Top = 0;
            //this.Left = 0;
            //this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            //this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            ChangeHistoryComboBoxNumbers();
            Historys.SelectedIndex = 0;
            
        }

        private void savePNG()
        {
            Rect rect = new Rect(Board.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right,
              (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(Board);
            //endcode as PNG
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            //save to memory stream
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            System.IO.File.WriteAllBytes("logo.png", ms.ToArray());
            Console.WriteLine("Done");
        }

        private async void NewAlgorithm(object sender, RoutedEventArgs e)
        {
            CheckBiggerClickedFlag();
            NotInProcess = false;
            //int n = Convert.ToInt32(N_Text.Text);
            //int m = Convert.ToInt32(M_Text.Text);

            //n = n < 4 ? 4 : n > 4096 ? 4096 : Math.Sqrt(n) * Math.Sqrt(n) == n ? n : ((int)Math.Sqrt(n) + 1) * ((int)Math.Sqrt(n) + 1);
            //m = m < 1 ? 1 : m > n ? n : m;

            //N_Text.Text = n.ToString();
            //M_Text.Text = m.ToString();

            if (Convert.ToInt32(M_Text.Text) <= 0)
            {
                M_Text.Text = FirstM.ToString();
            }
            N = WhiteCellsBy.SelectedIndex == 0 ? Convert.ToInt32(N_Text.Text) : Convert.ToInt32(N_Text.Text) * Convert.ToInt32(N_Text.Text);
            M = BlackCellsBy.SelectedIndex == 0 ? Convert.ToInt32(M_Text.Text) : Convert.ToInt32(M_Text.Text) * (int)Math.Sqrt(N);

            if (CountPainted > 0 && Convert.ToInt32(M_Text.Text) == CountPainted)
            {
                List<int> m = new();

                for (int i = 0; i < CurrentMatrix.GetLength(0); i++)
                {
                    for (int y = 0; y < CurrentMatrix.GetLength(1); y++)
                    {
                        if (CurrentMatrix[i, y].Fill == ON1)
                        {
                            m.Add(Coordinate.ReturnNumber(i, y, Convert.ToInt32(Math.Sqrt(Convert.ToInt32(N_Text.Text)))));
                        }
                    }
                }
                m = m.Union(Enumerable.Range(0, Convert.ToInt32(N_Text.Text))).Distinct().ToList();
            }

            ChangeIndividualComboBoxNumbers();

            AssignAlgorithm();

            Bigger.IsEnabled = true;

            CreateCanvas();
            CreateChart();

            Do();

            await Task.Delay(1);
            NotInProcess = true;
            savePNG();
        }

        private void AssignAlgorithm()
        {
            Algorithm = (AlgorithmType)AAlgorithmType.SelectedIndex switch
            {
                (AlgorithmType.Strait) => new Strait(N, M),
                (AlgorithmType.Custom) => new Algorithm(N, M, Convert.ToInt32(PopulationSize_Text.Text),
                     Convert.ToInt32(OldPopulationSize_Text.Text), Convert.ToInt32(CrossPopulationSize_Text.Text),
                     Convert.ToInt32(NewPopulationSize_Text.Text), (IndividualType)ANewIndividualType.SelectedIndex,
                     (CrossoverType)ACrossoverType.SelectedIndex, (RandomChooseType)AIndividualChooserType.SelectedIndex,
                     (MutateType)AMutateType.SelectedIndex, (LocalSearchType)ALocalSearchType.SelectedIndex,
                     (MirrorType)AMirrorType.SelectedIndex, Convert.ToInt32(MutateChance_Text.Text),
                     Convert.ToInt32(LocalSearchChance_Text.Text), new MutateFlags((bool)Mutate0.IsChecked,
                     (bool)Mutate1.IsChecked, (bool)Mutate2.IsChecked, (bool)Mutate3.IsChecked, (bool)PMutate.IsChecked),
                     new LocalSearchFlags((bool)LocalSearch0.IsChecked, (bool)LocalSearch1.IsChecked, (bool)LocalSearch2.IsChecked,
                     (bool)LocalSearch3.IsChecked, (bool)PLocalSearch.IsChecked)),

                _ => throw new Exception("Wrong value"),
            };
        }

        private async void Do()
        {
            CheckErrors();
            Mute();

            if (AShowingType.SelectedIndex == 0 || AShowingType.SelectedIndex == 1)
            {
                Calculate();
            }

            else
            {
                FastCalculate();
            }

            await Task.Delay(1);
            
        }

        private void CheckErrors()
        {
            if (Iterations_Text.Text == "")
            {
                Iterations_Text.Text = "0";
            }

            if (Time_Text.Text == "")
            {
                Time_Text.Text = "0";
            }
        }

        private async void Calculate()
        {
            DontStop = true;

            ProgressBar.Value = 0;

            double tick;
            bool by;

            if (Convert.ToInt32(Iterations_Text.Text) == 0) //cia
            {
                tick = 100.0 / (Convert.ToInt32(Time_Text.Text) * 1000); //cia
                by = true;
            }
            else
            {
                tick = 100.0 / Convert.ToInt32(Iterations_Text.Text);
                by = false;
            }

            Stopwatch stopwatch = new();
            stopwatch.Start();


            for (int i = 0; (i < Convert.ToInt32(Iterations_Text.Text) || stopwatch.ElapsedMilliseconds < Convert.ToInt32(Time_Text.Text) * 1000) && DontStop; i++)
            {
                if ((bool)PMirror.IsChecked)
                {
                    Algorithm.Population[0].MakeMirrored();
                }

                Algorithm.Next();

                PaintSignals();
                PaintBoards();


                if (by)
                {
                    ProgressBar.Value = Convert.ToDouble(stopwatch.ElapsedMilliseconds) * tick;
                }
                else
                {
                    ProgressBar.Value = Convert.ToDouble(i) * tick;
                }

                if (AShowingType.SelectedIndex == 0)
                {
                    await Task.Delay(1);
                }


            }

            await Task.Delay(1); 

            ProgressBar.Value = 100;

            CountPainted = Convert.ToInt32(M_Text.Text);

            stopwatch.Stop();

            Result result = new(Algorithm.BestIndividual);
            result.SaveFile();
            Trace.WriteLine(result.M + " " + result.N);
            ChangeHistoryComboBoxNumbers();
            PaintBoards();
            Unmute();

        }

        private async void FastCalculate()
        {
            DontStop = true;

            ProgressBar.Value = 0;
           
            await Task.Delay(1);


            DontStop = true;

            Algorithm.Next(Convert.ToInt32(Iterations_Text.Text), Convert.ToInt32(Time_Text.Text));

            PaintSignals();
            PaintBoards();

            await Task.Delay(1);

            ProgressBar.Value = 100;

            CountPainted = Convert.ToInt32(M_Text.Text);

            DontStop = false;

            Result result = new(Algorithm.BestIndividual);
            result.SaveFile();
            Trace.WriteLine($"hm{result.ToString()}");
            ChangeHistoryComboBoxNumbers();

            Unmute();

        }

        private async void ReadAndDisplay()
        {
            PaintBoards();
            await Task.Delay(1);

        }

        private async void NextAlgorithm(object sender, RoutedEventArgs e)
        {
            CheckBiggerClickedFlag();
            Do();
            await Task.Delay(1);
        }

        private void CheckBiggerClickedFlag()
        {
            if (BiggerClicked == true)
            {
                CreateBoard();
                BiggerClicked = false;
            }
        }

        private void StopAlgorithm(object sender, RoutedEventArgs e)
        {
            DontStop = false;
            CountPainted = Convert.ToInt32(M_Text.Text);
        }

        private async void BiggerAlgorithm(object sender, RoutedEventArgs e)
        {
            MuteAll();
            await Task.Delay(1);
            CreateBoardBigger();
            BiggerClicked = true;
            Unmute();
        }


        private void R_MouseEnter(object sender, MouseEventArgs e)
        {
            bool leftButton = e.LeftButton == MouseButtonState.Pressed;
            if (!leftButton) return;

            Rectangle pixel = (Rectangle)sender;

            //pixel.Fill = pixel.Fill == ON ? OFF : ON;

            if (pixel.Fill == ON1)
            {
                pixel.Fill = OFF1;

                CountPainted--;
            }
            else
            {
                pixel.Fill = ON1;
                CountPainted++;
            }

            M_Text.Text = CountPainted.ToString();

        }


        private void Mute()
        {
            New.IsEnabled = false;
            Next.IsEnabled = false;
            Stop.IsEnabled = true;
            Bigger.IsEnabled = false;
        }

        private void NewOnly()
        {
            New.IsEnabled = true;
            Next.IsEnabled = false;
            Stop.IsEnabled = false;
            Bigger.IsEnabled = false;
        }

        private void MuteAll()
        {
            New.IsEnabled = false;
            Next.IsEnabled = false;
            Stop.IsEnabled = false;
            Bigger.IsEnabled = false;
        }

        private void Unmute()
        {
            New.IsEnabled = true;
            Next.IsEnabled = true;
            Stop.IsEnabled = false;
            Bigger.IsEnabled = true;
        }


        private void AAlgorithmType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((AlgorithmType)AAlgorithmType.SelectedIndex == AlgorithmType.Custom)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        private async void Individuals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotInProcess)
            {
                CheckBiggerClickedFlag();
                PaintIndividualBySelectedIndividual();
            }
            
           
            await Task.Delay(1);
        }

        private void Historys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotInProcess)
            {
                CheckBiggerClickedFlag();
                PaintIndividualBySelectedHistory();
            }

        }

        private void ChangeIndividualComboBoxNumbers()
        {
            Individuals.Items.Clear();
            for (int i = 0; i < PopulationSize.Value; i++)
            {
                Individuals.Items.Add(i);
            }

            Individuals.SelectedIndex = 0;
        }

        private void ChangeHistoryComboBoxNumbers()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            
            List<FileInfo> files = new(dir.GetFiles("22*", SearchOption.TopDirectoryOnly));

            while (files.Count>10)
            {
                File.Delete(files[0].Name);
                files.RemoveAt(0);
            }

            Historys.Items.Clear();
            
            for(int i= files.Count-1 ; i>=0 ; i--)
            {
                var ss = Result.ReadFileAndRename(files[i].Name);
                Historys.Items.Add(ss);
            }

            Historys.SelectedIndex = 0;
        }


        private void PopulationSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (OldPopulationSize != null)
            {

                if (PopulationSize.Value == 1)
                {
                    OldPopulationSize.Value = 1;
                    NewPopulationSize.Value = 0;
                    CrossPopulationSize.Value = 0;
                }
                else
                {
                    OldPopulationSize.Value = (int)PopulationSize.Value / 10;
                    NewPopulationSize.Value = (int)PopulationSize.Value / 10;
                    CrossPopulationSize.Value = PopulationSize.Value - OldPopulationSize.Value - NewPopulationSize.Value;
                }

            }
        }

        private void Disable()
        {
            for (int i = 2; i < VisualTreeHelper.GetChildrenCount(Menuu); i++)
            {
                Menuu.Children[i].IsEnabled = false;
            }


            for (int i = 15; i < VisualTreeHelper.GetChildrenCount(MainParameters) - 4; i++)
            {
                MainParameters.Children[i].IsEnabled = false;
            }
        }

        private void Enable()
        {
            for (int i = 2; i < VisualTreeHelper.GetChildrenCount(Menuu); i++)
            {
                Menuu.Children[i].IsEnabled = true;
            }
            for (int i = 15; i < VisualTreeHelper.GetChildrenCount(MainParameters) - 4; i++)
            {
                MainParameters.Children[i].IsEnabled = true;
            }
        }

        private void M_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (New != null)
            {
                NewOnly();
            }
        }

        private void N_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (New != null)
            {
                NewOnly();
            }
        }
    }
}
