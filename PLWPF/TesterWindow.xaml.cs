﻿using System;
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
        Comments comments = new Comments();
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
            try
            {
                if (DataGrid.SelectedItem == null)
                    throw new Exception("אתה צריך לבחור מבחן");
                Test test = DataGrid.SelectedItem as Test;
                if (comments.comments != null)
                {
                    test.Comments = comments.comments;
                }
                if (gradecheckbox.SelectedIndex == 0)
                    test.Grade = Grade.עבר;
                else if (gradecheckbox.SelectedIndex == 1)
                    test.Grade = Grade.נכשל;
                else
                {
                    throw new Exception("אתה צריך להזין ציון");
                }
                foreach (Grid item in panel.Children)
                {
                        if (item.RowDefinitions.Count == 2 && item.ColumnDefinitions.Count == 2)
                        {
                            ComboBox grade=new ComboBox();
                            TextBox name=new TextBox();
                            foreach (Control item1 in item.Children)
                            {
                                if (item1 is ComboBox)
                                {
                                if ((item1 as ComboBox).SelectedIndex == -1)
                                    throw new Exception("אתה צריך לבחור ציונים לכל הקריטריונים");
                                    grade = item1 as ComboBox;
                                }
                                if (item1 is TextBox)
                                {
                                if ((item1 as TextBox).Text == "")
                                    throw new Exception("אתה צריך לבחור שמות לכל הקריטריונים");
                                    name = item1 as TextBox;
                                }
                            }
                            test.Criterions.Add(new Criterion(name.Text,(grade.SelectedIndex==0 ? Grade.עבר : Grade.נכשל)));
                        }
                }
                bl.Update(test);
                Sinon_Click(this,new RoutedEventArgs());
            }
            catch (Exception ex)
            {
                WBox.Visibility = Visibility.Visible;
                WBox.Content = ex.Message;
            }
        }
        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            comments.ShowDialog();
            BuComments.Content = "!סיימת";
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
        private void Clear_Click(object sender, EventArgs e)
        {
            gradecheckbox.SelectedIndex = -1;
            BuComments.Content = "הערות";
            comments = new Comments();
            WBox.Visibility = Visibility.Hidden;
            panel.Children.Clear();
        }

        private void Change_Object(object sender, EventArgs e)
        {
            Test test = DataGrid.SelectedItem as Test;
            //if (test.TestDay>DateTime.Now)
            //{
            //    foreach (Control item in grid11.Children)
            //    {
            //        item.Visibility = Visibility.Hidden;
            //    }
            //}
            //else{
            if (test == null)
                return;
                if (test.Grade == null && test.Comments == null && test.Criterions.Count == 0)
                {
                    Clear_Click(this, e);
                }
                else
                {
                    if (test.Grade != null)
                    {
                        gradecheckbox.SelectedItem = test.Grade;
                    }
                    if (test.Comments != null)
                    {
                        comments = new Comments();
                        comments.comments = test.Comments;
                        BuComments.Content = "!סיימת";
                    }
                    if (test.Criterions.Count != 0)
                    {
                        foreach (Criterion item in test.Criterions)
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
                            name1.Text = item.name;
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
                            grade1.SelectedItem = item.grade;
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
                    }
                }
            //}

        }
        private void Comments_Click(object sender, RoutedEventArgs e)
        {
            Test test = ((Button)sender).DataContext as Test;
            if (test.Comments == null)
            {
                Comments comments = new Comments("אין הערות");
                comments.ShowDialog();
            }
            else
            {
                Comments comments = new Comments(test.Comments);
                comments.ShowDialog();
            }
        }
        private void Criterions_Click(object sender, RoutedEventArgs e)
        {
            Test test = ((Button)sender).DataContext as Test;
            Criterions criterions = new Criterions(test.Criterions);
            criterions.ShowDialog();
        }
    }
}
