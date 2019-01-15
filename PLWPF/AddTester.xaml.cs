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
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : Window
    {
        Tester tester;
        IBL bl;
        public AddTester()
        {
            InitializeComponent();
            bl = BL.FactoryBL.getBl();
            tester = new Tester("not to add");
            this.DataContext = tester;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.typeOfCarComboBox.ItemsSource = Enum.GetValues(typeof(BE.Car));
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {

            
                try
                {

                    bl.AddTester(tester);
                    tester = new Tester("not to add");
                    this.Close();
                }
                catch (Exception ex)
                {
                    this.WarningBox.Content = ex.Message;
                }
            
        }
    }
}
