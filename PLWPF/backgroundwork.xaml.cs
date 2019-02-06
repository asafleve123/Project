using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for backgroundwork.xaml
    /// </summary>
    public partial class backgroundwork : Window
    {
         BackgroundWorker worker;
        IBL bL = FactoryBL.getBl();
        List<Tester> ablesTester;
        Test test;
        public string Se;
        public double Value {
            get {
                return PBar.Value;
            }
            set {
                PBar.Value = value;
                if (value == 100)
                {
                        Finish();
                }
            }
        }

        private void Finish()
        {
            try
            {
                bL.AddTest(test, ablesTester);
            }
            catch(Exception e)
            {
                Se = e.Message;
                this.Close();
             }
        }

        public backgroundwork(Test test)
        {
            InitializeComponent();
            this.test = test;
            ablesTester = new List<Tester>();
            worker = new BackgroundWorker();
            Se = null;
            worker.DoWork += Do_Work;
            worker.ProgressChanged += Progress_Changed;
            worker.RunWorkerCompleted += Run_Worker_Completed;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            this.Loaded += start;
        }

        private void start(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync(bL.TestersCollection().Count());
        }

        private void Run_Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void Progress_Changed(object sender, ProgressChangedEventArgs e)
        {
                Value = e.ProgressPercentage;
                label.Content = string.Format("{0}%", e.ProgressPercentage);
            if ((string)e.UserState!="Error")
                Name.Content = (string)e.UserState;
        }

        private void Do_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                int length = (int)e.Argument;
                for (int i = 1; i <= length; i++)
                {
                    bool IsCan = bL.IsCanTest(bL.TestersCollection()[i - 1], test);
                    if (IsCan)
                    {
                        ablesTester.Add(new Tester(bL.TestersCollection()[i - 1]));
                        e.Result = string.Format("{0}.XML", bL.TestersCollection()[i - 1]);
                    }
                    else
                    {
                        e.Result = "Error";
                    }
                    worker.ReportProgress(i * 100 / length,e.Result);
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                Se = ex.Message;
                worker.CancelAsync();
            }
        }
    }
}
