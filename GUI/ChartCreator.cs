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
            Chart.Plot.Clear();

            CurrentValues = new double[100_000];
            BestValues = new double[100_000];
            SignalPlot = Chart.Plot.AddSignal(CurrentValues);
            SignalPlot = Chart.Plot.AddSignal(BestValues);
            Chart.Plot.SetAxisLimits(0, 5, Algorithm.BestIndividual.Fitness-1, Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness+1);
            NextPointIndex = 0;

            Chart.Plot.Title("Fitness Results");
            Chart.Plot.XLabel("Generations");
            Chart.Plot.YLabel("Fitness");

            //TODO: make labels
            //string[] labels = { "Current", "Best", };
            Chart.Plot.Legend(true, ScottPlot.Alignment.UpperRight);

            Chart.Height = 235;
            Chart.Width = 1059;
            PaintSignals();

        }

        //Algorithm Algorithm = new Strait(256, 16);
        //CreateChart();

        //for (int i = 0; i < 10; i++)
        //{
        //    Algorithm.Next();
        //    PaintSignals();
        //}

    }


}
