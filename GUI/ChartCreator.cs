using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Algorithms;


namespace GUI
{
    public partial class MainWindow : Window
    {
        public double[] CurrentValues;
        public double[] BestValues;
        public int NextPointIndex;
        public ScottPlot.Plottable.SignalPlot SignalPlot;
        
        public void CreateChart()
        {
            Chart.Reset();

            CurrentValues = new double[100_000];
            BestValues = new double[100_000];
            SignalPlot = Chart.Plot.AddSignal(CurrentValues);
            SignalPlot = Chart.Plot.AddSignal(BestValues);
            Chart.Plot.SetAxisLimits(0, 5, Algorithm.BestIndividual.Fitness-1, Algorithm.Population[0].Fitness+1);
            NextPointIndex = 0;

            Chart.Plot.Title("Fitness Results");
            Chart.Plot.XLabel("Generations");
            Chart.Plot.YLabel("Fitness");

            string[] labels = { "Current", "Best", };
            Chart.Plot.Legend(true, ScottPlot.Alignment.UpperRight);

            Chart.Height = 300;
            Chart.Width = Board.ActualWidth + Board2.ActualWidth +100;
        }

        private void Chart_Loaded(object sender, RoutedEventArgs e)
        {
            Algorithm Algorithm = new(Convert.ToInt32(N_Text.Text), Convert.ToInt32(M_Text.Text));
            CreateChart();
        }

    }

    
}
