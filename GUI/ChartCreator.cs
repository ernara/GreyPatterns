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
            //string[] labels = { "Current", "Best" };
            string labels = "Current";
            string labels2 = "Best";
            SignalPlot = Chart.Plot.AddSignal(CurrentValues);
            SignalPlot.Label = labels;
            SignalPlot = Chart.Plot.AddSignal(BestValues);
            SignalPlot.Label = labels2;

            Chart.Plot.SetAxisLimits(0, 5, Algorithm.BestIndividual.Fitness-1, Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness+1);
            NextPointIndex = 0;

            Chart.Plot.Title("Fitness Results"); //plt.Title
            Chart.Plot.XLabel("Generations");
            Chart.Plot.YLabel("Fitness");
            Chart.Plot.Legend();

            //TODO: make labels

            Chart.Plot.Legend(true, ScottPlot.Alignment.UpperRight);

            Chart.Height = 275;
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
