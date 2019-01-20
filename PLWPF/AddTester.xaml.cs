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
        public Tester tester;
        IBL bl;
        public AddTester()
        {
            tester = new Tester("");
            bl = BL.FactoryBL.getBl();
            InitializeComponent();
            this.DataContext = tester;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.typeOfCarComboBox.ItemsSource = Enum.GetValues(typeof(BE.Car));
            tester.WorkTable = new bool[6, 5]
            {
                    { false, false, false, false, false},
                    { false, false, false, false, false},
                    { false, false, false, false, false},
                    { false, false, false, false, false},
                    { false, false, false, false, false},
                    { false, false, false, false, false},
              };

        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    string Code = this.code.Password;
                    tester.Code = Code;
                    tester.Address = new Address(this.City.Text, this.Street.Text, this.NumOfHome.Text);
                    bl.AddTester(tester);
                    this.Close();
                }
                catch (Exception ex)
                {
                        this.WarningBox.Content = ex.Message;
            }
            
        }

        private void WorkTableButton_Click(object sender, RoutedEventArgs e)
        {
            WorkTable workTableWin = new WorkTable(tester.WorkTable);
            workTableWin.ShowDialog();
            tester.WorkTable= workTableWin.workTable;
            Button b1 = sender as Button;
            b1.Content = "!סיימת";
        }
    }
}
