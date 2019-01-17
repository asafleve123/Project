using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for WorkTable.xaml
    /// </summary>
    public partial class WorkTable : Window
    {
        public WorkTable(bool[,] workTable1)
        {
            InitializeComponent();
            workTable = workTable1;
            foreach (Control item in Grid.Children)
            {
                if (item is Button)
                {
                    Button_Click_Start(item);
                }
            }
        }

        private void Button_Click_Start(object sender)
        {
            int row = Grid.GetRow(sender as Button) - 1;
            int column = Grid.GetColumn(sender as Button);
            Button b = sender as Button;
            if (workTable[row, column])
            {
                b.Content = "עובד";

                b.Background = Brushes.Black;
                b.Foreground = Brushes.White;
            }
            else
            {
                b.Content = "לא עובד";
                b.Background = Brushes.White;
                b.Foreground = Brushes.Black;

            }
        }

        public bool[,] workTable { get; set; } 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int row = Grid.GetRow(sender as Button)-1;
            int column = Grid.GetColumn(sender as Button);
            Button b = sender as Button;
            workTable[row, column] = !workTable[row, column];
            if (workTable[row, column])
            {
                b.Content = "עובד";
               
                b.Background = Brushes.Black;
                b.Foreground = Brushes.White;
            }
            else
            {
                b.Content = "לא עובד";
                b.Background = Brushes.White;
                b.Foreground = Brushes.Black;

            }
        }
    }
}
