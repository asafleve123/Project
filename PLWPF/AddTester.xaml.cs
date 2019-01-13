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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : Window
    {
        public AddTester()
        {
            InitializeComponent();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.typeOfCarComboBox.ItemsSource = Enum.GetValues(typeof(BE.Car));
        }

       
    }
}
