using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using peako = BTechCogitareBankUtils01.pl.pekaobiznes24.www;
using System.Xml.Serialization;

namespace BTechCogitareBankUtils01
{
    public class WSDLPekao01
    {
        public static string getStatement(string cert, string MsgId, string AccId, bool byear, DateTime Dt, string format = "XML", string StId = " ")
        {
            /* cert - path to certificate file *.p12
             * MsgId - Message Id
             * AccId - Account Id
             * byear - true (method takes year and Statement Id) / false (method takes only date to statement request)
             * Dt - date
             * format - format of response (XML / PDF)
             * StId - Statement Id
             */

            peako.pekaoccs00101 loWSDL = new peako.pekaoccs00101();
            peako.MessageIdentyfication1 loMsgId;
            peako.StatementQueryDefinition loQuery;
            peako.GetStatement loStatement;
            peako.StatementRequest loRequest;
            peako.StatementResponse loResponse;

            //--- Message Id
            loMsgId = new peako.MessageIdentyfication1();
            loMsgId.Id = MsgId;

            loQuery = new peako.StatementQueryDefinition();
            loQuery.StmtCrit = new peako.StatementCriteria();
            loQuery.StmtCrit.NewCrit = new peako.NewCriteria1();
            loQuery.StmtCrit.NewCrit.SchCrit = new peako.SearchCriteria1();

            //--- Format
            if(format == "XML")
                loQuery.StmtCrit.NewCrit.SchCrit.StmtFrmt = peako.StatementFormat.XML;
            else
                loQuery.StmtCrit.NewCrit.SchCrit.StmtFrmt = peako.StatementFormat.PDF;

            loQuery.StmtCrit.NewCrit.SchCrit.AcctId = new peako.AccountIdentification1();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt = new peako.StatementValueSearch();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch = new peako.DatePeriodDetails2();

            //--- Statement by date / year + Statement Id
            if (byear == false)
                loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch.Item = Dt;
            else
            {
                loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch.Item = Dt.Year.ToString();
                loQuery.StmtCrit.NewCrit.SchCrit.StmtId = new peako.StatementId
                {
                    EQ = StId
                };
            }

            loQuery.StmtCrit.NewCrit.SchCrit.AcctId.EQ = new peako.AccountIdentification3Choice1
            {
                ItemElementName = peako.ItemChoiceType14.IBAN,
                Item = AccId
            };

            loStatement = new peako.GetStatement();
            loStatement.MsgId = loMsgId;
            loStatement.StmtQryDef = loQuery;

            loRequest = new peako.StatementRequest
            {
                Document = new peako.Document2()
            };

            loRequest.Document.GetStmt = loStatement;

            try
            {
                loWSDL.ClientCertificates.Add(new X509Certificate2(@cert));
                loResponse = loWSDL.GetStatement(loRequest);

                //convert response to string
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(loResponse.Item.GetType());
                serializer.Serialize(stringwriter, loResponse.Item);
                string lvtext = stringwriter.ToString();

                lvtext = lvtext.Replace(" xmlns=\"urn:iso:std:iso:20022:tech:xsd:camt.053.001.02\"", "");

                MessageBox.Show(lvtext);
                return lvtext;
            }
            catch (Exception loError)
            {
                MessageBox.Show(loError.ToString());
            }
            return "Error";
        }

        public static string getAccountBalance(string cert, string MsgId, string BBAN, string IBAN)
        {
            /* cert - path to certificate file *.p12
             * MsgId - Message Id
             * BBAN - Basic Bank Account Number
             * IBAN - International Bank Account Number
             */

            peako.pekaoccs00101 loWSDL = new peako.pekaoccs00101();
            peako.MessageIdentyfication1 loMsgId;
            peako.AccountQueryDefinition4 loQuery;
            peako.GetAccountBalanceRequest loBalance;
            peako.GetAccountBalanceResponse loResponse;

            //--- Message Id
            loMsgId = new peako.MessageIdentyfication1();
            loMsgId.Id = MsgId;

            //--- Account Query Definition
            loQuery = new peako.AccountQueryDefinition4();
            loQuery.AcctCrit = new peako.AccountCriteriaDefinition4Choice();

            peako.AccountCriteria4 loCriteria = new peako.AccountCriteria4();
            loCriteria.SchCrit = new peako.CashAccountSearchCriteria4[1];
            loCriteria.SchCrit[0] = new peako.CashAccountSearchCriteria4();

            //--- Account Id
            loCriteria.SchCrit[0].AcctId = new peako.AccountIdentificationSearchCriteriaChoice[2];

            peako.AccountIdentification1Choice loAccId1 = new peako.AccountIdentification1Choice();
            loAccId1.Item = BBAN; //////

            loAccId1.ItemElementName = peako.ItemChoiceType56.BBAN;

            loCriteria.SchCrit[0].AcctId[0] = new peako.AccountIdentificationSearchCriteriaChoice();
            loCriteria.SchCrit[0].AcctId[0].Item = loAccId1;
            loCriteria.SchCrit[0].AcctId[0].ItemElementName = peako.ItemChoiceType57.EQ;

            peako.AccountIdentification1Choice loAccId2 = new peako.AccountIdentification1Choice();
            loAccId2.Item = IBAN;
            loAccId2.ItemElementName = peako.ItemChoiceType56.IBAN;

            loCriteria.SchCrit[0].AcctId[1] = new peako.AccountIdentificationSearchCriteriaChoice();
            loCriteria.SchCrit[0].AcctId[1].Item = loAccId2;
            loCriteria.SchCrit[0].AcctId[1].ItemElementName = peako.ItemChoiceType57.EQ;

            //--- Bal
            loCriteria.SchCrit[0].Bal = new peako.BalanceDetails4[1];
            loCriteria.SchCrit[0].Bal[0] = new peako.BalanceDetails4();
            loCriteria.SchCrit[0].Bal[0].BalTp = new peako.BalanceType3Choice[2];
            loCriteria.SchCrit[0].Bal[0].BalTp[0] = new peako.BalanceType3Choice();
            loCriteria.SchCrit[0].Bal[0].BalTp[0].Item = peako.BalanceType10Code.AVLB; ///////
            loCriteria.SchCrit[0].Bal[0].BalTp[1] = new peako.BalanceType3Choice();
            loCriteria.SchCrit[0].Bal[0].BalTp[1].Item = peako.BalanceType10Code.BOOK; ///////
            loCriteria.SchCrit[0].Bal[0].CtrPtyTp = peako.BalanceCounterparty1Code.MULT; //////

            loQuery.AcctCrit.Item = loCriteria;

            loBalance = new peako.GetAccountBalanceRequest();
            loBalance.Document = new peako.Document12();
            loBalance.Document.GetAcct = new peako.GetAccountV04();
            loBalance.Document.GetAcct.AcctQryDef = loQuery;
            loBalance.Document.GetAcct.MsgId = new peako.MessageIdentification1();
            loBalance.Document.GetAcct.MsgId.Id = MsgId;

            try
            {
                loWSDL.ClientCertificates.Add(new X509Certificate2(@cert));
                loResponse = loWSDL.GetAccountBalance(loBalance);

                //convert response to string
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(loResponse.GetType());
                serializer.Serialize(stringwriter, loResponse);
                string lvtext = stringwriter.ToString();

                lvtext = lvtext.Replace(" xmlns=\"urn:swift:xsd:camt.004.001.04\"", "");

                MessageBox.Show(lvtext);
                return lvtext;
            }
            catch (Exception loError)
            {
                MessageBox.Show(loError.ToString());
            }
            return "Error";
        }

        public static string getPaymentStatusReport(string cert, string MsgId, DateTime CreDtTm, string OrgnMsgId)
        {
            /* cert - path to certificate file *.p12
             * MsgId - Message Id
             * CreDtTm - Cretion Date Time
             * OrgnMsgId - Original Meassage Id
             */

            peako.pekaoccs00101 loWSDL = new peako.pekaoccs00101();
            peako.MessageIdentyfication1 loMsgId;
            peako.GetPaymentStatusReport loReport;
            peako.PaymentStatusRequest loRequest;
            peako.PaymentStatusResponse loResponse;

            loMsgId = new peako.MessageIdentyfication1();
            loMsgId.Id = MsgId;
            //loMsgId.Id = "GPSR201808220001";

            loReport = new peako.GetPaymentStatusReport();
            loReport.GrpHdr = new peako.GrpHeader();

            //--- Message Id
            loReport.GrpHdr.MsgId = new peako.MessageId();
            loReport.GrpHdr.MsgId.Id = MsgId;
            //loReport.GrpHdr.MsgId.Id = "GPSR201808220001";

            //--- Creation Date Time
            loReport.GrpHdr.CreDtTm = CreDtTm;

            //--- Original Message Id
            loReport.OrgnlGrpInfAndSts = new peako.OriginalGrpInfoAndStatus();
            loReport.OrgnlGrpInfAndSts.OrgnlMsgId = OrgnMsgId;
            //loReport.OrgnlGrpInfAndSts.OrgnlMsgId = "DT201808220001";

            loRequest = new peako.PaymentStatusRequest();
            loRequest.Document = new peako.Document8();
            loRequest.Document.GetPayStsRpt = loReport;

            try
            {
                loWSDL.ClientCertificates.Add(new X509Certificate2(@cert));
                loResponse = loWSDL.GetPaymentStatusReport(loRequest);

                ////convert response to string
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(loResponse.GetType());
                serializer.Serialize(stringwriter, loResponse);
                string lvtext = stringwriter.ToString();

                lvtext = lvtext.Replace(" xmlns=\"urn:iso:std:iso:20022:tech:xsd:pain.002.001.03\"", "");

                MessageBox.Show(lvtext);
                return lvtext;
            }
            catch (Exception loError)
            {
                MessageBox.Show(loError.ToString());
            }
            return "Error";
        }
    }
}
