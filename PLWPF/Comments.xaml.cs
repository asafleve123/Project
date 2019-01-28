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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Comments.xaml
    /// </summary>
    public partial class Comments : Window
    {
       public string comments { get { return comm.Text; } set { comm.Text = value; } }
        public Comments()
        {
            InitializeComponent();
            comments = null;
            System.Windows.Input.InputLanguageManager.Current.InputLanguageChanged += language_Func;
            if (System.Windows.Input.InputLanguageManager.Current.CurrentInputLanguage.Name == "en-US")
            {
                comm.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                comm.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        public Comments( string commn)
        {
            InitializeComponent();
            comm.Visibility = Visibility.Hidden;
            commTr.Visibility = Visibility.Visible;
            commTr.Text = commn;
        }

        private void language_Func(object sender, InputLanguageEventArgs e)
        {
            if (e.NewLanguage.DisplayName == "Hebrew (Israel)")
            {
                comm.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                comm.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}
