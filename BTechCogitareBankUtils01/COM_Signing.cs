using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace BTechCogitare.Bank.Utils01
{
    [Guid("740F4C88-A0E5-4FE4-94F3-E7A806F92F73")]
    public interface ICOM_Signing_Interface
    {
        [DispId(1)]
        String SignXml(String xml);
    }

    // Events interface Database_COMObjectEvents 
    [Guid("0B9E4EFD-F796-4D82-8025-78FFB0C47C78"),
        InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ICOM_Signing_Events
    {
    }

    [Guid("FCA3A3C0-17B6-4271-81C1-566106DA78B4"),
        ComVisible(true),
        ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(ICOM_Signing_Events))]
    public class COM_Signing : ICOM_Signing_Interface
    {

        public String SignXml(String xml)
        {
            try
            {
                CspParameters cspParams = new CspParameters
                {
                    KeyContainerName = "XML_DSIG_RSA_KEY"
                };

                RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);
                XmlDocument xmlDoc = new XmlDocument
                {
                    // Load an XML file into the XmlDocument object.
                    PreserveWhitespace = true
                };
                xmlDoc.LoadXml(xml);
                
                // Sign the XML document. 
                _SignXml(xmlDoc, rsaKey);

                // Save the document.
                StringWriter sw = new StringWriter();
                XmlWriter xw = XmlWriter.Create(sw);
                xmlDoc.WriteContentTo(xw);
                xw.Close();

                String ret = sw.ToString();
                MessageBox.Show(ret);
                return (ret.ToString());

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return ("");
            }

        }

        protected void _SignXml(XmlDocument xmlDoc, RSA key)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (key == null)
                throw new ArgumentException("key");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc)
            {
                // Add the key to the SignedXml document.
                SigningKey = key
            };

            // Create a reference to be signed.
            Reference reference = new Reference
            {
                Uri = ""
            };

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }
    }
}
