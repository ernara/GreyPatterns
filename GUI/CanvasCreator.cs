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
		public void CreateCanvas()
		{
			for (int y = 0; y < Individual.N1; y++)
			{
				for (int x = 0; x < Individual.N2; x++)
				{
					Rectangle r = new Rectangle
					{
						Width = Board.ActualWidth / gameWidth - spacing,
						Height = Board.ActualHeight / gameHeight - spacing,
						Fill = OFF
					};
					Board.Children.Add(r);

					Canvas.SetLeft(r, x * Board.ActualWidth / gameWidth);
					Canvas.SetTop(r, y * Board.ActualHeight / gameHeight);

					matrix[y, x] = r;

					r.MouseDown += R_MouseEnter;
					r.MouseEnter += R_MouseEnter;
				}
			}
		}
	}
}
