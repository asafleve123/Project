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
        bool[,] worktable = new bool[6, 5]
{
                    { true, true, true, true, true},
                    { true, true, true, true, true},
                    { true, true, true, true, true},
                    { true, true, true, true, true},
                    { true, true, true, true, true},
                    { true, true, true, true, true},
  };
        string idTr=null;
        string idTe=null;
        string passwordTr=null;
        string passwordTe = null;
        public MainWindow()
        {
            InitializeComponent();
            //Button_Click(this,new RoutedEventArgs());
            Closing += Close_click;
            Tester a = new Tester("212384507", "Levi", "Asaf", new DateTime(1950, 2, 2), Gender.זכר, "0503363230", new Address { City = "Jer", Street = "somewhere", NumOfHome = "11" }, DateTime.Now, 20, Car.רכב_פרטי, worktable, 10, "1");
            MyBl.AddTester(a);
            this.tz.Text = a.Id;
            this.passwordTester.Password = a.Code;
            Trainee q = new Trainee("322263310", "Yossef", "Katri", Gender.נקבה, "1234567890", new Address { City = "Jer", Street = "somewhere", NumOfHome = "22" }, new DateTime(1940, 12, 12), Car.רכב_פרטי, Gearbox.אוטומטי, "toryarok", "Shimi", 20, false,"2");
            MyBl.AddTrainee(q);
            this.tz1.Text = q.Id;
            this.passwordTrainne.Password = q.Code;
        }

        private void Close_click(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult ans = MessageBoxResult.None;
            ans = MessageBox.Show("?אתה בטוח", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
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
            try
            {
                if (passwordTester.Password == MyBl.GetTester(tz.Text).Code)
                {
                    if (RemmberTe.IsChecked==true)
                    {
                        idTe = tz.Text;
                        passwordTe = passwordTester.Password;
                        tz.Background = Brushes.LightYellow;
                        passwordTester.Background = Brushes.LightYellow;
                    }
                    TesterWindow testerWindow = new TesterWindow(MyBl.GetTester(tz.Text));
                    testerWindow.ShowDialog();
                }
                else
                {
                    throw new Exception("הסיסמא או התז אינם נכונים");
                }
            }
            catch (Exception ex)
            {
                RemmberTe.Visibility = Visibility.Hidden;
                WarnningTester.Visibility = Visibility.Visible;
                WarnningTester.Text = ex.Message;
            }
        }

        private void ConnectTrainee(object sender, RoutedEventArgs e)
        {

            try
            {
                if (passwordTrainne.Password == MyBl.GetTrainee(tz1.Text).Code)
                {
                    if (RemmberTr.IsChecked == true)
                    {
                        idTr = tz1.Text;
                        passwordTr = passwordTrainne.Password;
                        tz1.Background = Brushes.LightYellow;
                        passwordTrainne.Background = Brushes.LightYellow;
                    }
                    TraineeWindow testerWindow = new TraineeWindow(MyBl.GetTrainee(tz1.Text));
                    testerWindow.ShowDialog();
                }
                else
                {
                    throw new Exception("הסיסמא או התז אינם נכונים");
                }
            }
            catch (Exception ex)
            {
                RemmberTr.Visibility = Visibility.Hidden;
                WarnningTrainee.Visibility = Visibility.Visible;
                WarnningTrainee.Text = ex.Message;
            }
        }
    }
}
