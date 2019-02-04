using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for backgroundwork.xaml
    /// </summary>
    public partial class backgroundwork : Window
    {
        BackgroundWorker worker;
        public double Value { get { return PBar.Value; } set { PBar.Value = value; } }
        public backgroundwork()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Do_Work;
            worker.ProgressChanged += Progress_Changed;
            worker.RunWorkerCompleted += Run_Worker_Completed;
        }

        private void Run_Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Progress_Changed(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Do_Work(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
