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
    /// Interaction logic for TesterWindow.xaml
    /// </summary>
    public partial class TesterWindow : Window
    {
        IBL bl;
        public Tester tester { get; set; }
        public List<Test> tests
        {
            get
            {
                return new List<Test>(DataGrid.ItemsSource as IEnumerable<Test>);
            }
            set
            {
                Action action = () => DataGrid.ItemsSource = value;
                Dispatcher.BeginInvoke(action);
            }
        }
        public string selection;
        public double Value
        {
            set
            {
                if (value != 0)
                    progressbar.Value += value;
                else
                    progressbar.Value = value;
            }
        }
        Thread thread ;
        public TesterWindow(Tester tester)
        {
            bl = BL.FactoryBL.getBl();
            InitializeComponent();
            this.typeOfCarComboBox.ItemsSource = Enum.GetValues(typeof(BE.Car));
            this.gradecheckbox.ItemsSource = Enum.GetValues(typeof(BE.Grade));
            this.gradecheckbox.FontWeight = FontWeights.DemiBold;
            this.gradecheckbox.FontSize = 20;
            this.tester = tester;
            this.DataContext = tester;
            Address address = tester.Address;
            this.City.Text = address.City;
            this.Street.Text = address.Street;
            this.NumOfHome.Text = address.NumOfHome;
            selection = "הכל";
            Thread thread = new Thread(load_Func);
            thread.Start();
        }

        private void load_Func()
        {
            IEnumerable<Test> collection = bl.AllTestsBy(T => T.IdTester == tester.Id);
            Action action = () => DataGrid.ItemsSource = collection;
            Dispatcher.BeginInvoke(action);
        }
        private void WorkTableButton_Click(object sender, RoutedEventArgs e)
        {
            WorkTable workTableWin = new WorkTable(tester.WorkTable);
            workTableWin.ShowDialog();
            tester.WorkTable = workTableWin.workTable;
            Button b1 = sender as Button;
            b1.Content = "!סיימת";
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            tester.Address = new Address(this.City.Text, this.Street.Text, this.NumOfHome.Text);
            try
            {
                bl.UpdateTester(tester);
            }
            catch (Exception exp)
            {
                this.WarningBox.Content = exp.Message;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                warnningBox delete = new warnningBox(tester);
                delete.ShowDialog();
                if (delete.IsDelete)
                {
                    bl.DeleteTester(tester);
                    this.Close();
                }
            }
            catch (Exception exp)
            {
                this.DeleteWarningBox.Content = exp.Message;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                tester.Code = this.Code.Password;
                bl.UpdateTester(tester);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Criterion_Click(object sender, RoutedEventArgs e)
        {
            Label name = new Label();
            name.Content = ":שם";
            name.FontWeight = FontWeights.DemiBold;
            name.Background = Brushes.White;
            name.HorizontalContentAlignment = HorizontalAlignment.Center;
            TextBox name1 = new TextBox();
            name1.Background = Brushes.White;
            name1.FontWeight = FontWeights.DemiBold;
            name1.FontSize = 20;
            Label grade = new Label();
            grade.Content = ":ציון";
            grade.FontWeight = FontWeights.DemiBold;
            grade.Background = Brushes.White;
            grade.HorizontalContentAlignment = HorizontalAlignment.Center;
            ComboBox grade1 = new ComboBox();
            grade1.ItemsSource = Enum.GetValues(typeof(BE.Grade));
            grade1.FontSize = 20;
            grade1.FontWeight = FontWeights.DemiBold;
            grade1.Background = Brushes.White;
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetColumn(name, 1);
            Grid.SetRow(name, 0);
            Grid.SetColumn(name1, 0);
            Grid.SetRow(name1, 0);
            Grid.SetColumn(grade, 1);
            Grid.SetRow(grade, 1);
            Grid.SetColumn(grade1, 0);
            Grid.SetRow(grade1, 1);
            grid.Children.Add(name);
            grid.Children.Add(name1);
            grid.Children.Add(grade);
            grid.Children.Add(grade1);
            panel.Children.Add(grid);
        }
        private void Enter_Click(object sender, EventArgs e)
        {
            //לעדכן מבחן
        }
        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            Comments comments = new Comments();
            comments.ShowDialog();
            comments.Content = "!סיימת";
            //להכניס לתוך המבחן את ההערות
        }
        private void sinon_Func(object text)
        {
            string Text = text as string;
            if (Text != "")
            {
               tests =new List<Test>( bl.AllTestsBy(T => T.IdTrainee == idStudent.Text, tester.Id));
            }
            else
            {
                switch (selection)
                {
                    case "הכל":
                        tests = new List<Test>(bl.AllTestsBy(T => true, tester.Id));
                        break;
                    case "ללא ציון":
                        tests = new List<Test>(bl.AllTestsBy(T => T.Grade == null, tester.Id));
                        break;
                    case "עם ציון":
                        tests = new List<Test>(bl.AllTestsBy(T => T.Grade != null, tester.Id));
                        break;
                    case "עבר":
                        tests = new List<Test>(bl.AllTestsBy(T => T.Grade == Grade.עבר, tester.Id));
                        break;
                    case "נכשל":
                        tests = new List<Test>(bl.AllTestsBy(T => T.Grade == Grade.נכשל, tester.Id));
                        break;
                    case "לא קרו":
                        tests = new List<Test>(bl.AllTestsBy(T => T.TestDay > DateTime.Now, tester.Id));
                        break;
                }
                Action action = () => Value = 50;
                Dispatcher.BeginInvoke(action);
                Thread.Sleep(1000);
            }

        }
        private void Sinon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Value = 0;
                Thread.Sleep(1000);
                selection = sinon.SelectionBoxItem as string;
                thread = new Thread(sinon_Func);
                Value = 50;
                thread.Start(idStudent.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "אזהרה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
