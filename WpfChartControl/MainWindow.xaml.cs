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
        public List<KeyValuePair<string, int>> ByTests { get; set; }
        public List<KeyValuePair<string, int>> BySchool { get; set; }
        public List<KeyValuePair<string, int>> ByTeacher { get; set; }
        public MainWindow(List<KeyValuePair<string, int>> s, List<KeyValuePair<string, int>> teacher, List<KeyValuePair<string, int>> tests)
        {
            InitializeComponent();
            ByTests = tests;
            BySchool = s;
            ByTeacher =teacher;
            showChart();
        }
        private void showChart()
        {
            
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
