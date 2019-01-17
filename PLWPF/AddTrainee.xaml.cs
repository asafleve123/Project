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
using BL;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTrainee.xaml
    /// </summary>
    public partial class AddTrainee : Window
    {
        public Trainee trainee;
        IBL bl;
        public AddTrainee()
        {
            trainee = new Trainee("");
            bl = BL.FactoryBL.getBl();
            InitializeComponent();
            this.DataContext = trainee;
            this.GenderBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.typeGearBoxComboBox.ItemsSource = Enum.GetValues(typeof(Gearbox));
            this.typeOfCarComboBox.ItemsSource = Enum.GetValues(typeof(Car));
            //this.GenderBox
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Code = this.codeTextBox.Password;
                trainee.Code = Code;
                trainee.Address = new Address(this.City.Text, this.Street.Text, this.NumOfHome.Text);
                bl.AddTrainee(trainee);
                this.Close();

            }
            catch (Exception exp)
            {
                this.WarningBox.Content = exp.Message;
            }
        }
    }
}
