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
            /*
              var ans = MessageBox.Show("תבחר מבין שתי רקעים שונים על ידי כן ולא","תמונות",MessageBoxButton.YesNo);
            if (ans==MessageBoxResult.Yes)
            {
                this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this),"Pictures/cars.jpg")));
            }
            else
            {
                this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this),"Pictures/fire-and-water-wallpaper.jpg")));
            }
            */
            Closing += Close_click;
        }

        private void Close_click(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult ans = MessageBoxResult.None;
            ans = MessageBox.Show("?אתה בטוח", "אזהרה", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ans == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
