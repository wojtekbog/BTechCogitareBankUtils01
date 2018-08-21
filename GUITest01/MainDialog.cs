using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using peako = BTechCogitareBankUtils01.pl.pekaobiznes24.www;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace GUITest01
{
    public partial class MainDialog : Form
    {
        public MainDialog()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!
            System.Diagnostics.Process.Start("http://aka.ms/dotnet-get-started-desktop");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            peako.pekaoccs00101 loWSDL = new peako.pekaoccs00101();
            peako.MessageIdentyfication1 loMsgId;
            peako.StatementQueryDefinition loQuery;
            peako.GetStatement loStatement;
            peako.StatementRequest loRequest;
            peako.StatementResponse loResponse;
            String lvXml = textXml.Text;

            loMsgId = new peako.MessageIdentyfication1();
            loMsgId.Id = "GS201109050031111111";

            loQuery = new peako.StatementQueryDefinition();
            loQuery.StmtCrit = new peako.StatementCriteria();
            loQuery.StmtCrit.NewCrit = new peako.NewCriteria1();
            loQuery.StmtCrit.NewCrit.SchCrit = new peako.SearchCriteria1();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtFrmt = peako.StatementFormat.XML;
            loQuery.StmtCrit.NewCrit.SchCrit.AcctId = new peako.AccountIdentification1();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt = new peako.StatementValueSearch();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch = new peako.DatePeriodDetails2();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch.Item = new DateTime(2018, 04, 23);


            loQuery.StmtCrit.NewCrit.SchCrit.AcctId.EQ = new peako.AccountIdentification3Choice1
            {
                ItemElementName = peako.ItemChoiceType14.IBAN,
                Item = "PL90124062921111001045475455"
            };

            loStatement = new peako.GetStatement();
            loStatement.MsgId       = loMsgId;
            loStatement.StmtQryDef  = loQuery;

            loRequest = new peako.StatementRequest();
            loRequest.Document = new peako.Document2();
            loRequest.Document.GetStmt = loStatement;

            try
           {
                loWSDL.ClientCertificates.Add(new X509Certificate2("d:\\projekty\\btech\\cogitare\\przelewy-bankowe\\wsdl\\certificates\\cis_22705460.p12"));
                loResponse = loWSDL.GetStatement(loRequest);
                MessageBox.Show(loResponse.ToString());

            } catch(Exception loError)
            {
                MessageBox.Show(loError.ToString());
            }
        }
    }
}
