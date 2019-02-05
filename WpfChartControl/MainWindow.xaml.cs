using System;
using System.Collections.Generic;
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

namespace WpfChartControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            showChart();
        }
        private void showChart()
        {
            List<KeyValuePair<string, int>> BySchool = new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> ByTeacher= new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> ByTests = new List<KeyValuePair<string, int>>();
            BySchool.Add(new KeyValuePair<string, int>("Administration", 2));
            BySchool.Add(new KeyValuePair<string, int>("Management", 3));
            ByTeacher.Add(new KeyValuePair<string, int>("Development", 8));
            ByTeacher.Add(new KeyValuePair<string, int>("Support", 2));
            ByTests.Add(new KeyValuePair<string, int>("Sales", 14));
            ByTests.Add(new KeyValuePair<string, int>("A", 15));
            ColumnChart1.DataContext = BySchool;
            ColumnChart2.DataContext = ByTeacher;
            ColumnChart3.DataContext = ByTests;
            ColumnChart1.Visibility = Visibility.Hidden;
            ColumnChart2.Visibility = Visibility.Hidden;
            ColumnChart3.Visibility = Visibility.Hidden;
        }

        private void School_Click(object sender, RoutedEventArgs e)
        {
            ColumnChart1.Visibility = Visibility.Visible;
            ColumnChart2.Visibility =Visibility.Hidden;
            ColumnChart3.Visibility =Visibility.Hidden;
        }

        private void Teacher_Click(object sender, RoutedEventArgs e)
        {
            ColumnChart1.Visibility = Visibility.Hidden;
            ColumnChart2.Visibility = Visibility.Visible;
            ColumnChart3.Visibility = Visibility.Hidden;
        }

        private void Tests_Click(object sender, RoutedEventArgs e)
        {
            ColumnChart1.Visibility = Visibility.Hidden;
            ColumnChart2.Visibility = Visibility.Hidden;
            ColumnChart3.Visibility =Visibility.Visible;
        }
    }
}
