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
        string comments;
        public Comments()
        {
            InitializeComponent();
            System.Windows.Input.InputLanguageManager.Current.InputLanguageChanged += language_Func;
            if (System.Windows.Input.InputLanguageManager.Current.CurrentInputLanguage.Name == "en-US")
            {
                comm.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                comm.FlowDirection = FlowDirection.RightToLeft;
            }
            this.Closing += save;
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

        private void save(object sender, CancelEventArgs e)
        {
            comments = comm.Text;
        }
    }
}
