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
        public void CreateTable()
        {
            //// Create the parent FlowDocument...
            //FlowDoc = new FlowDocument();

            //// Create the Table...
            //ResultsTable = new Table();
            //// ...and add it to the FlowDocument Blocks collection.
            //FlowDoc.Blocks.Add(ResultsTable);

            // Set some global formatting properties for the table.
            ResultsTable.CellSpacing = 10;
            ResultsTable.Background = Brushes.White;

            // Create 6 columns and add them to the table's Columns collection.
            int numberOfColumns = BestResults.BestKnownResults.Count;
            for (int x = 0; x < numberOfColumns; x++)
            {
                ResultsTable.Columns.Add(new TableColumn());

                // Set alternating background colors for the middle colums.
                if (x % 2 == 0)
                    ResultsTable.Columns[x].Background = Brushes.Beige;
                else
                    ResultsTable.Columns[x].Background = Brushes.LightSteelBlue;
            }

            // Create and add an empty TableRowGroup to hold the table's Rows.
            ResultsTable.RowGroups.Add(new TableRowGroup());

            // Add the first (title) row.
            ResultsTable.RowGroups[0].Rows.Add(new TableRow());

            // Alias the current working row for easy reference.
            TableRow currentRow = ResultsTable.RowGroups[0].Rows[0];

            // Global formatting for the title row.
            currentRow.Background = Brushes.Silver;
            currentRow.FontSize = 40;
            currentRow.FontWeight = System.Windows.FontWeights.Bold;

            // Add the header row with content,
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Best Fitness"))));
            // and set the row to span all 6 columns.
            currentRow.Cells[0].ColumnSpan = 6;

            // Add the second (header) row.
            ResultsTable.RowGroups[0].Rows.Add(new TableRow());
            currentRow = ResultsTable.RowGroups[0].Rows[1];

            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the second row.

            for (int i = 0; i < BestResults.BestKnownResults.Count; i++)
            {
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(BestResults.BestKnownResults[i].ToString()))));

            }
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Quarter 1"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Quarter 2"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Quarter 3"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Quarter 4"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("TOTAL"))));

            // Add the third row.
            ResultsTable.RowGroups[0].Rows.Add(new TableRow());
            currentRow = ResultsTable.RowGroups[0].Rows[2];

            // Global formatting for the row.
            currentRow.FontSize = 12;
            currentRow.FontWeight = FontWeights.Normal;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Widgets"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("$50,000"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("$55,000"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("$60,000"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("$65,000"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("$230,000"))));

            // Bold the first cell.
            currentRow.Cells[0].FontWeight = FontWeights.Bold;

            ResultsTable.RowGroups[0].Rows.Add(new TableRow());
            currentRow = ResultsTable.RowGroups[0].Rows[3];

            // Global formatting for the footer row.
            currentRow.Background = Brushes.LightGray;
            currentRow.FontSize = 18;
            currentRow.FontWeight = System.Windows.FontWeights.Normal;

            // Add the header row with content,
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Projected 2004 Revenue: $810,000"))));
            // and set the row to span all 6 columns.
            currentRow.Cells[0].ColumnSpan = 6;
        }

        private void ResultsTable_Loaded(object sender, RoutedEventArgs e)
        {
            //Algorithm Algorithm = new(Convert.ToInt32(N_Text.Text), Convert.ToInt32(M_Text.Text));
            CreateTable();
        }
    }
}
