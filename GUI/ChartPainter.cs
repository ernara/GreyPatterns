using System;
using System.Collections.Generic;
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
            CurrentValues[NextPointIndex] = Algorithm.Population[0].Fitness;
            BestValues[NextPointIndex] = Algorithm.BestIndividual.Fitness;
            SignalPlot.MaxRenderIndex = NextPointIndex;
            NextPointIndex++;

            double GenerationsLimit = Chart.Plot.GetAxisLimits().XMax;
            if (NextPointIndex > GenerationsLimit)
                Chart.Plot.SetAxisLimits(xMax: GenerationsLimit + 100);

            double SmallestFitness = Chart.Plot.GetAxisLimits().YMin + Algorithm.Population[0].Fitness;
            if (Algorithm.BestIndividual.Fitness < SmallestFitness)
                Chart.Plot.SetAxisLimits(yMin: Algorithm.BestIndividual.Fitness- Algorithm.Population[0].Fitness / 100);

            double BiggestFitness = Chart.Plot.GetAxisLimits().YMax ;
            if (Algorithm.Population[0].Fitness > BiggestFitness )
                Chart.Plot.SetAxisLimits(yMax: Algorithm.Population[0].Fitness*1.01);

            Chart.Render();
        }

    }
}
