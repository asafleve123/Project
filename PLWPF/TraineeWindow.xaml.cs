using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TraineeWindow.xaml
    /// </summary>
    public partial class TraineeWindow : Window
    {
        IBL bl;
        public Trainee trainee { get; set; }
        public TraineeWindow(Trainee trainee)
        {
            bl = BL.FactoryBL.getBl();
            InitializeComponent();
            this.typeOfCarComboBox.ItemsSource = Enum.GetValues(typeof(BE.Car));
            this.typeGearBoxComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gearbox));
            for (int i = 9; i <= 15; i++)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = i.ToString() + ":00";
                TestHour.Items.Add(comboBoxItem);
            }
            this.trainee = trainee;
            this.DataContext = trainee;
            Address address = trainee.Address;
            this.City.Text = address.City;
            this.Street.Text = address.Street;
            this.NumOfHome.Text = address.NumOfHome;
            Thread thread = new Thread(load_Func);
            thread.Start();
        }

        private void load_Func()
        {
           IEnumerable<Test> collection=bl.AllTestsBy(T => T.IdTrainee == trainee.Id);
           Action action = () => DataGrid.ItemsSource = collection;
           Dispatcher.BeginInvoke(action);
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            trainee.Address = new Address(this.City.Text, this.Street.Text, this.NumOfHome.Text);
            try
            {
                bl.UpdateTrainee(trainee);
            }
            catch (Exception exp)
            {
                this.WarningBox.Content = exp.Message;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                trainee.Code = this.Code.Password;
                bl.UpdateTrainee(trainee);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                warnningBox delete = new warnningBox(trainee);
                delete.ShowDialog();
                if (delete.IsDelete)
                {
                bl.DeleteTrainee(trainee);
                this.Close();
                }
            }
            catch (Exception exp)
            {
                this.DeleteWarningBox.Content = exp.Message;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sign_click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddTestWarningBox.Text = "";
                if (this.TestCity.Text=="")
                {
                    throw new Exception("הזן שם עיר");
                }
                if (this.TestStreet.Text=="")
                {
                    throw new Exception("הזן שם רחוב");
                }
                if (this.TestNumOfHouse.Text=="")
                {
                    throw new Exception("הזן מספר בית");
                }
                Address address = new Address(this.TestCity.Text,this.TestStreet.Text,this.TestNumOfHouse.Text);
                if (this.TestDate.SelectedDate == null)
                    throw new Exception("!הזן תאריך");
                DateTime dateTime = this.TestDate.SelectedDate.Value;
                if (TestHour.SelectedIndex == -1)
                    throw new Exception("!הכנס שעה");
                dateTime = dateTime.AddHours(TestHour.SelectedIndex+9);
                Test test = new Test(trainee,dateTime,address);
                //bl.AddTest(test,);
                backgroundwork addtest=new backgroundwork(test);
                addtest.ShowDialog();
                if (addtest.Se!=null)
                {
                    throw new Exception(addtest.Se);
                }
                DataGrid.ItemsSource =bl.AllTestsBy(T => T.IdTrainee==trainee.Id);
            }
            catch (Exception exp)
            {
                this.AddTestWarningBox.Text = exp.Message;
            }
        }

        private void Comments_Click(object sender, RoutedEventArgs e)
        {
            Test test= ((Button)sender).DataContext as Test;

                Comments comments = new Comments(test.Comments);
                comments.ShowDialog();
            
        }
        private void Criterions_Click(object sender, RoutedEventArgs e)
        {
            Test test = ((Button)sender).DataContext as Test;
            Criterions criterions = new Criterions(test.Criterions);
            criterions.ShowDialog();
        }
    }
}
