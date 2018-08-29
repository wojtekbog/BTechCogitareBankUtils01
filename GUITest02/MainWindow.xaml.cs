using System;
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
                    if (GetStPropertyList.SelectedIndex == 0)
                        lvformat = "XML";
                    else
                        lvformat = "PDF";

                    //string lvyear;
                    string lvStId;
                    if (GetStToggle.IsChecked == true)
                    {
                        lvStId = GetStTbStId.Text;
                        gvresponse = WSDLPekao01.getStatement(gvfile, GetStTbMsgId.Text, GetSTTbAccId.Text, true, GetStDatePicker.SelectedDate.Value.Date, lvformat, lvStId);
                    }
                    else
                        gvresponse = WSDLPekao01.getStatement(gvfile, GetStTbMsgId.Text, GetSTTbAccId.Text, false, GetStDatePicker.SelectedDate.Value.Date, lvformat);
                    break;

                case 1: //GetAccountBalance

                    gvresponse = WSDLPekao01.getAccountBalance(gvfile, GetAccBalTbMsgId.Text, GetAccBalTbBBAN.Text, GetAccBalTbIBAN.Text);

                    break;

                case 2: //GetPaymentStatusReport

                    DateTime CrDtTm = new DateTime();
                    CrDtTm = GetPayStReDatePicker.SelectedDate.Value.Date;
                    TimeSpan ts = new TimeSpan(Int32.Parse(GetPayStReHour.Text), Int32.Parse(GetPayStReMin.Text), Int32.Parse(GetPayStReSec.Text));
                    CrDtTm = CrDtTm + ts;

                    gvresponse = WSDLPekao01.getPaymentStatusReport(gvfile, GetPayStReTbMsgId.Text, CrDtTm, GetPayStReTbOrgMsgId.Text);

                    break;

                case 3: //DomesticTransfer

                    gvresponse = WSDLPekao01.DomesticTransfer(gvfile);

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

            switch (lvMethod)
            {
                case 0: //GetStatement

                    GetStPanel.Visibility = Visibility.Visible;
                    GetAccBalPanel.Visibility = Visibility.Collapsed;
                    GetPayStRePanel.Visibility = Visibility.Collapsed;

                    GetStDatePicker.SelectedDate = new DateTime(2018, 04, 23);
                    GetStTbMsgId.Text = "GS201109050031111111";
                    GetSTTbAccId.Text = "PL90124062921111001045475455";

                    GetStPropertyList.SelectedIndex = 0;

                    break;

                case 1: //GetAccountBalance

                    GetStPanel.Visibility = Visibility.Collapsed;
                    GetAccBalPanel.Visibility = Visibility.Visible;
                    GetPayStRePanel.Visibility = Visibility.Collapsed;

                    GetAccBalTbMsgId.Text = "GS201109050031111111";
                    GetAccBalTbBBAN.Text = "94124062921111001080877861";
                    GetAccBalTbIBAN.Text = "PL79124062921111001045475556";

                    break;

                case 2: //GetPaymentStatusReport

                    GetStPanel.Visibility = Visibility.Collapsed;
                    GetAccBalPanel.Visibility = Visibility.Collapsed;
                    GetPayStRePanel.Visibility = Visibility.Visible;

                    GetPayStReTbMsgId.Text = "GPSR201808220001";
                    GetPayStReTbOrgMsgId.Text = "DT201808220001";
                    GetPayStReDatePicker.SelectedDate = new DateTime(2018, 07, 24);

                    GetPayStReHour.Text = "12";
                    GetPayStReMin.Text = "49";
                    GetPayStReSec.Text = "12";

                    break;

                case 3: //DomesticTransfer

                    GetStPanel.Visibility = Visibility.Collapsed;
                    GetAccBalPanel.Visibility = Visibility.Collapsed;
                    GetPayStRePanel.Visibility = Visibility.Collapsed;

                    break;

                case 4:
                    break;

                case -1:
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
                gvfile = dlg.FileName;
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

        private void GetStToggle_Click(object sender, RoutedEventArgs e)
        {
            if (GetStToggle.IsChecked == true)
            {
                System.Windows.MessageBox.Show("Choose year (day and month does not matter)");

                GetStatementYearStPanel.Visibility = Visibility.Visible;

                GetStTStId.Text = "Statement ID";
                GetStTbStId.Text = "1";
            }
            else
            {
                GetStatementYearStPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
