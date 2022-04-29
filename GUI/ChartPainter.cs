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
        public void PaintSignals()
        {
            CurrentValues[NextPointIndex] = Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness;
            SignalPlot.MaxRenderIndex = NextPointIndex;
            NextPointIndex++;

            BestValues[NextPointIndex] = Algorithm.BestIndividual.Fitness;
            SignalPlot2.MaxRenderIndex = NextPointIndex2;
            NextPointIndex2++;

            double GenerationsLimit = Chart.Plot.GetAxisLimits().XMax;
            if (NextPointIndex > GenerationsLimit)
                Chart.Plot.SetAxisLimits(xMax: GenerationsLimit + 100); 

            double SmallestFitness = Chart.Plot.GetAxisLimits().YMin + Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness;
            if (Algorithm.BestIndividual.Fitness < SmallestFitness)
                Chart.Plot.SetAxisLimits(yMin: Algorithm.BestIndividual.Fitness- Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness / 10);

            double BiggestFitness = Chart.Plot.GetAxisLimits().YMax ;
            if (Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness > BiggestFitness )
                Chart.Plot.SetAxisLimits(yMax: Algorithm.Population[Math.Min(Algorithm.Population.Count - 1, Algorithm.CrossoverPopulationSize)].Fitness*1.01);

            Chart.Render();
        }

    }
}
