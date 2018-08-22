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
        public static string getStatement(string cert, string MsgId, string AccId, bool byear, DateTime Dt, string format = "XML", string StID = " ")
        {
            peako.pekaoccs00101 loWSDL = new peako.pekaoccs00101();
            peako.MessageIdentyfication1 loMsgId;
            peako.StatementQueryDefinition loQuery;
            peako.GetStatement loStatement;
            peako.StatementRequest loRequest;
            peako.StatementResponse loResponse;
            //          String lvXml = textXml.Text;

            loMsgId = new peako.MessageIdentyfication1();
            loMsgId.Id = MsgId;
            //loMsgId.Id = "GS201109050031111111";

            loQuery = new peako.StatementQueryDefinition();
            loQuery.StmtCrit = new peako.StatementCriteria();
            loQuery.StmtCrit.NewCrit = new peako.NewCriteria1();
            loQuery.StmtCrit.NewCrit.SchCrit = new peako.SearchCriteria1();

            if(format == "XML")
                loQuery.StmtCrit.NewCrit.SchCrit.StmtFrmt = peako.StatementFormat.XML;
            else
                loQuery.StmtCrit.NewCrit.SchCrit.StmtFrmt = peako.StatementFormat.PDF;

            loQuery.StmtCrit.NewCrit.SchCrit.AcctId = new peako.AccountIdentification1();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt = new peako.StatementValueSearch();
            loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch = new peako.DatePeriodDetails2();

            if (byear == false)
                loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch.Item = Dt;
            else
            {
                loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch.Item = Dt.Year.ToString();
                loQuery.StmtCrit.NewCrit.SchCrit.StmtId = new peako.StatementId
                {
                    EQ = StID
                };
         
            }

            //loQuery.StmtCrit.NewCrit.SchCrit.StmtValDt.DtSch.Item = new DateTime(2018, 04, 23);

            loQuery.StmtCrit.NewCrit.SchCrit.AcctId.EQ = new peako.AccountIdentification3Choice1
            {
                ItemElementName = peako.ItemChoiceType14.IBAN,
                Item = AccId
                //Item = "PL90124062921111001045475455"
            };

            loStatement = new peako.GetStatement();
            loStatement.MsgId = loMsgId;
            loStatement.StmtQryDef = loQuery;

            loRequest = new peako.StatementRequest();
            loRequest.Document = new peako.Document2();
            loRequest.Document.GetStmt = loStatement;

            try
            {
                loWSDL.ClientCertificates.Add(new X509Certificate2(@cert));
                loResponse = loWSDL.GetStatement(loRequest);
                //MessageBox.Show(loResponse.ToString());

                //convert response to string
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(loResponse.Item.GetType());
                serializer.Serialize(stringwriter, loResponse.Item);
                string text = stringwriter.ToString();
                MessageBox.Show(text);
                return text;
            }
            catch (Exception loError)
            {
                MessageBox.Show(loError.ToString());
            }
            return "Error";
        }

        public static string getAccountBalance(string cert, string MsgId)
        {
            peako.pekaoccs00101 loWSDL = new peako.pekaoccs00101();
            peako.MessageIdentyfication1 loMsgId;
            peako.AccountQueryDefinition4 loQuery;
            peako.GetAccountBalanceRequest loBalance;

            peako.StatementRequest loRequest;
            peako.StatementResponse loResponse;

            loMsgId = new peako.MessageIdentyfication1();
            loMsgId.Id = MsgId;

            loQuery = new peako.AccountQueryDefinition4();
            loQuery.AcctCrit = new peako.AccountCriteriaDefinition4Choice();
            //loQuery.AcctCrit.



            return "Error";
        }
    }
}
