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
using BE;
using BL;
using System.Threading;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL MyBl = FactoryBL.getBl();
        public MainWindow()
        {
            InitializeComponent();
            //Button_Click(this,new RoutedEventArgs());
            //Closing += Close_click;
        }

        private void Close_click(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult ans = MessageBoxResult.None;
            ans = MessageBox.Show("?אתה בטוח", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            ans = MessageBox.Show("?אתה בטוח בטוח", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
                //MessageBoxResult ans = MessageBoxResult.None;
                //ans = MessageBox.Show("?האם אתה נותן לנו 100","?אזהרה",MessageBoxButton.YesNo,MessageBoxImage.Question);
                //if (ans==MessageBoxResult.No)
                //{
                //    e.Cancel = true;
                //}
            }

        private void New_Tester(object sender, RoutedEventArgs e)
        {
            var AddTesterWin = new AddTester();
            AddTesterWin.ShowDialog();
        }

        private void Add_Dialog(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog color = new System.Windows.Forms.ColorDialog();
            if (color.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                SolidColorBrush Chosen = new SolidColorBrush(Color.FromArgb(color.Color.A, color.Color.R, color.Color.G, color.Color.B));
                B1.Background = Chosen;
                Grid.Background = Chosen;
                if (color.Color.GetBrightness()>0.5)
                {
                    B1.Foreground = Brushes.Black;
                }
                else
                {
                    B1.Foreground = Brushes.White;
                }
            }
        }
        private void New_Trainee(object sender, RoutedEventArgs e)
        {
            var AddTrainneWin = new AddTrainee();
            AddTrainneWin.ShowDialog();
        }

        private void ConnectTester(object sender, RoutedEventArgs e)
        {
            if (password.Password==MyBl.GetTesterPassword(tz.Text))
            {
             loading l = new loading();
             this.Content = l;
            }
            else
            { 
                    
            }
        }
    }
}
