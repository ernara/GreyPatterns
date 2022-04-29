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
        public int NextPointIndex2;

        public ScottPlot.Plottable.SignalPlot SignalPlot;
        public ScottPlot.Plottable.SignalPlot SignalPlot2;

        public void CreateChart()
        {
            Chart.Plot.Clear();

            CurrentValues = new double[100_000];
            CurrentValues[0] = Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness;
            BestValues = new double[100_000];
            BestValues[0] = Algorithm.BestIndividual.Fitness;
            string labels = "Current";
            string labels2 = "Best";

            SignalPlot = Chart.Plot.AddSignal(CurrentValues);
            SignalPlot.Label = labels;
            NextPointIndex = 0;

            SignalPlot2 = Chart.Plot.AddSignal(BestValues);
            SignalPlot2.Label = labels2;
            NextPointIndex2 = 0;

            Chart.Plot.SetAxisLimits(0, 5, Algorithm.BestIndividual.Fitness-1, Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness+1);
            
            Chart.Plot.Title("Fitness Results"); 
            Chart.Plot.XLabel("Generations");
            Chart.Plot.YLabel("Fitness");
            Chart.Plot.Legend();

            Chart.Plot.Legend(true, ScottPlot.Alignment.UpperRight);

            Chart.Height = 275;
            Chart.Width = 1059;

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
