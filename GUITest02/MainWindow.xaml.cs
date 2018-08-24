﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BTechCogitareBankUtils01;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace GUITest02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DatePicker1.Visibility = Visibility.Hidden;
            DatePicker2.Visibility = Visibility.Hidden;

            Toggle1.Visibility = Visibility.Hidden;

            t1.Visibility = Visibility.Hidden;
            t2.Visibility = Visibility.Hidden;
            t3.Visibility = Visibility.Hidden;
            t4.Visibility = Visibility.Hidden;
            t5.Visibility = Visibility.Hidden;

            tb1.Visibility = Visibility.Hidden;
            tb2.Visibility = Visibility.Hidden;
            tb3.Visibility = Visibility.Hidden;
            tb4.Visibility = Visibility.Hidden;
            tb5.Visibility = Visibility.Hidden;

            buttonSave.IsEnabled = false;
        }

        string gvfile;
        string gvresponse;

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            int lvMethod = MethodList.SelectedIndex;

            switch (lvMethod)
            {
                case 0: //GetStatement

                    string lvformat;
                    if (PropertyList.SelectedIndex == 0)
                        lvformat = "XML";
                    else
                        lvformat = "PDF";

                    //string lvyear;
                    string lvStId;
                    if(Toggle1.IsChecked == true)
                    {
                        //lvyear = tb3.Text;
                        lvStId = tb3.Text;
                        gvresponse = WSDLPekao01.getStatement(gvfile, tb1.Text, tb2.Text, true, DatePicker1.SelectedDate.Value.Date, lvformat, lvStId);
                    }
                    else
                        gvresponse = WSDLPekao01.getStatement(gvfile, tb1.Text, tb2.Text, false, DatePicker1.SelectedDate.Value.Date, lvformat);
                    break;

                case 1: //GetAccountBalance

                    gvresponse = WSDLPekao01.getAccountBalance(gvfile, tb1.Text, tb2.Text, tb3.Text);

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                default:
                    System.Windows.MessageBox.Show("invalid method chosen");
                    break;
            }
            buttonSave.IsEnabled = true;
        }

        private void buttonProperties_Click(object sender, RoutedEventArgs e)
        {
            int lvMethod = MethodList.SelectedIndex;
            
            switch(lvMethod)
            {
                case 0: //GetStatement

                    t1.Text = "Message ID";
                    t2.Text = "Account ID";
                    DatePicker1.SelectedDate = new DateTime(2018, 04, 23);

                    t1.Visibility = Visibility.Visible;
                    t2.Visibility = Visibility.Visible;
                    t3.Visibility = Visibility.Hidden;
                    t4.Visibility = Visibility.Hidden;
                    t5.Visibility = Visibility.Hidden;

                    tb1.Text = "GS201109050031111111";
                    tb2.Text = "PL90124062921111001045475455";

                    tb1.Visibility = Visibility.Visible;
                    tb2.Visibility = Visibility.Visible;
                    tb3.Visibility = Visibility.Hidden;
                    tb4.Visibility = Visibility.Hidden;
                    tb5.Visibility = Visibility.Hidden;

                    DatePicker1.Visibility = Visibility.Visible;
                    DatePicker2.Visibility = Visibility.Hidden;

                    PropertyList.Visibility = Visibility.Visible;
                    property1.Content = "XML";
                    property2.Content = "PDF";
                    PropertyList.SelectedIndex = 0;

                    Toggle1.Visibility = Visibility.Visible;
                    Toggle1.Content = "Year Statement";

                    break;

                case 1: //GetAccountBalance

                    t1.Text = "Message ID";
                    t2.Text = "Basic Bank Account Number";
                    t3.Text = "International Bank Account Number";

                    t1.Visibility = Visibility.Visible;
                    t2.Visibility = Visibility.Visible;
                    t3.Visibility = Visibility.Visible;
                    t4.Visibility = Visibility.Hidden;
                    t5.Visibility = Visibility.Hidden;

                    tb1.Text = "GS201109050031111111";
                    tb2.Text = "94124062921111001080877861";
                    tb3.Text = "PL79124062921111001045475556";

                    tb1.Visibility = Visibility.Visible;
                    tb2.Visibility = Visibility.Visible;
                    tb3.Visibility = Visibility.Visible;
                    tb4.Visibility = Visibility.Hidden;
                    tb5.Visibility = Visibility.Hidden;

                    DatePicker1.Visibility = Visibility.Hidden;
                    DatePicker2.Visibility = Visibility.Hidden;
                    PropertyList.Visibility = Visibility.Hidden;
                    Toggle1.Visibility = Visibility.Hidden;

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                default:
                    System.Windows.MessageBox.Show("invalid method chosen");
                    break;
            }
        }

        private void buttonCertificate_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                InitialDirectory = "C:\\Users\\Ania\\Pobrane",
                DefaultExt = ".p12", // Default file extension
                Filter = "Certificates (.p12)|*.p12" // Filter files by extension
            };

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                gvfile= dlg.FileName;
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            int lvMethod = MethodList.SelectedIndex;

            switch (lvMethod)
            {
                case 0: //GetStatement

                    if (Toggle1.IsChecked == true)
                    {
                        System.Windows.MessageBox.Show("Choose year (day and month does not matter)");

                        t3.Visibility = Visibility.Visible;
                        tb3.Visibility = Visibility.Visible;
                        t3.Text = "Statement ID";
                        tb3.Text = "1";
                    }
                    else
                    {
                        t3.Visibility = Visibility.Hidden;
                        tb3.Visibility = Visibility.Hidden;

                        DatePicker1.Visibility = Visibility.Visible;
                    }

                    break;

                case 1: //GetAccountBalance

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;

                default:
                    System.Windows.MessageBox.Show("invalid method chosen");
                    break;
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string lvpath = null;

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                lvpath = string.Concat(dialog.FileName, "\\", fileName.Text, ".xml");
            }

            if (!File.Exists(lvpath) && lvpath != null)
            {
                File.WriteAllText(lvpath, gvresponse);
            }
        }
    }
}
