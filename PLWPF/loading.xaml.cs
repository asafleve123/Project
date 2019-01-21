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
        public MainWindow main;
        public int value = 0;
        DispatcherTimer timer;
        List<string> files;
        int index = 0;
        public loading()
        {
            InitializeComponent();
            startClock();
            files = new List<string>();
            files.Add("BE.dll");
            files.Add("BE.pdb");
            files.Add("BL.dll");
            files.Add("BL.pdb");
            files.Add("DL.dll");
            files.Add("DL.pdb");
            files.Add("DS.dll");
            files.Add("DS.pdb");
            files.Add("PLWPF.exe");
            files.Add("PLWPF.exe.config");
            files.Add("PLWPF.pdb");
            

        }

        private void startClock()
        {
            timer = new DispatcherTimer();
            timer.Tick += tick;
            timer.Start();
        }

        private void tick(object sender, EventArgs e)
        {
            if (index == files.Count())
            {
                index = 0;
            }
            Text.Text = files[index++];
            value++;
            if (value == 80)
            {
                Thread.Sleep(2000);
            }
            PBar.Value = value;
            if (value == 100)
            {
                timer.Stop();
            }
        }
    }
}
