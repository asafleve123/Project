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
    /// Interaction logic for warnningBox.xaml
    /// </summary>
    public partial class warnningBox : Window
    {
        public Trainee  trainee{ get; set; }
        public Tester tester { get; set; }
        public bool IsDelete = false;
        public warnningBox(Trainee trainee)
        {
            InitializeComponent();
            this.trainee = trainee;
            this.tester = null;
            privateName.TextChanged += Change_Click;
        }
        public warnningBox(Tester tester)
        {
            InitializeComponent();
            this.trainee = null;
            this.tester = tester;
            privateName.TextChanged += Change_Click;
        }

        private void Change_Click(object sender, TextChangedEventArgs e)
        {
            TextBox PName = sender as TextBox;
            PName.Foreground = Brushes.Black;
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if (privateName.Text==(tester==null?trainee.PrivateName:tester.PrivateName))
            {
                IsDelete = true;
                Close();
            }
            else
            {
                warnning.Visibility = Visibility.Visible;
                warnning.Content = "השם הפרטי לא נכון";
            }
        }
    }
}
