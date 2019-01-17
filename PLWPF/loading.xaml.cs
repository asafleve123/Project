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
using System.Windows.Threading;
using System.Threading;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for loading.xaml
    /// </summary>
    public partial class loading : Page
    {
        public int value = 0;
        DispatcherTimer timer;
        public loading()
        {
            InitializeComponent();
            startClock();
        }

        private void startClock()
        {
            timer = new DispatcherTimer();
            timer.Tick += tick;
            timer.Start();
        }

        private void tick(object sender, EventArgs e)
        {
            value++;
            if (value==80)
            {
                Thread.Sleep(2000);
            }
            PBar.Value = value;
        }
    }
}
