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
using System.Windows.Shapes;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Criterions.xaml
    /// </summary>
    public partial class Criterions : Window
    {
        public Criterions(List<Criterion> criterions)
        {
            InitializeComponent();
            foreach (Criterion item in criterions)
            {
                Label name = new Label();
                name.Content = ":שם";
                name.FontWeight = FontWeights.Thin;
                name.Background = Brushes.White;
                name.HorizontalContentAlignment = HorizontalAlignment.Center;
                name.Background = Brushes.LightGoldenrodYellow;
                name.FontSize = 30;
                Label name1 = new Label();
                name1.Background = Brushes.White;
                name1.FontWeight = FontWeights.DemiBold;
                name1.FontSize = 20;
                name1.Content = item.name;
                Label grade = new Label();
                grade.Content = ":ציון";
                grade.FontWeight = FontWeights.Thin;
                grade.Background = Brushes.White;
                grade.HorizontalContentAlignment = HorizontalAlignment.Center;
                grade.Background = Brushes.LightGoldenrodYellow;
                grade.FontSize = 30;
                ComboBox grade1 = new ComboBox();
                grade1.ItemsSource = Enum.GetValues(typeof(BE.Grade));
                grade1.FontSize = 20;
                grade1.FontWeight = FontWeights.DemiBold;
                grade1.Background = Brushes.White;
                grade1.SelectedItem = item.grade;
                grade1.IsHitTestVisible = false;
                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(name, 1);
                Grid.SetRow(name, 0);
                Grid.SetColumn(name1, 0);
                Grid.SetRow(name1, 0);
                Grid.SetColumn(grade, 1);
                Grid.SetRow(grade, 1);
                Grid.SetColumn(grade1, 0);
                Grid.SetRow(grade1, 1);
                grid.Children.Add(name);
                grid.Children.Add(name1);
                grid.Children.Add(grade);
                grid.Children.Add(grade1);
                panel.Children.Add(grid);
            }
        }
    }
}
