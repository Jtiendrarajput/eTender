using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using eTender.Models;
using System.Xml.Linq;
using eTender.Models;
namespace eTender.Controllers
{
    public class DSCcommandController : Controller
    {
        //
        // GET: /DSCcommand/

        public ActionResult Index()
        {
            string s = "<certificate><attribute name='CN' >RIZWAN KHAN</attribute><attribute name='O'>Personal , CID - 4612668</attribute><attribute name='OU' ></attribute><attribute name='E' ></attribute><attribute name='CA' >(n)Code Solutions CA 2014</attribute><attribute name='SN' >1397732599</attribute><attribute name='Valid Date' >10/07/2016 19:57</attribute><attribute name='Expiry Date' >10/08/2018 00:00</attribute><attribute name='CRLVerify' >CRl Verify is valid</attribute><attribute name='OCSPVerify' >OCSP Verify is invalid</attribute></certificate>";
           
            return View();
        }

        public JsonResult Signin()
        {
            var bytes = Encoding.UTF8.GetBytes("Check for Device");
            string s = Convert.ToBase64String(bytes);
            StringBuilder sb = new StringBuilder();
            sb.Append("<request> <command>pkiNetworkSign</command> <ts>");
            sb.Append(DateTime.Now.ToString());
            sb.Append("</ts><txn>");
            sb.Append("unique id</txn><certificate><attribute name='CN'></attribute><attribute name='O'></attribute>");
            sb.Append("<attribute name='OU'></attribute><attribute name='T'></attribute><attribute name='E'></attribute>");
            sb.Append("<attribute name='SN'></attribute><attribute name='CA'></attribute>");
            sb.Append("<attribute name='TC'>SG</attribute>");
            sb.Append("<attribute name='AP'>1</attribute><attribute name='VD'></attribute>");
            sb.Append("</certificate>");
            sb.Append("<file><attribute name='type'>text</attribute></file>");
            sb.Append("<data>");
            sb.Append(s);
            sb.Append("</data></request>");
            string data = sb.ToString();
           return new JsonResult{Data = new { resp = sb.ToString() }, JsonRequestBehavior= JsonRequestBehavior.AllowGet};
        }
        public JsonResult SignPDF(int ID)
        {
            if (Request.Files.Count > 0)
            {
                string filename = string.Empty;
                try
                {
                    var file = Request.Files["tech"];
                    var filesize = file.ContentLength;
                    if (((filesize / 1024) / 1024) > 50)
                    {
                        return new JsonResult { Data = new { msg = "File is not Uploaded!!!  Upload file of size less than 50 MB  ", resp = "" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        
                    }

                    string[] s = file.FileName.Split('.');
                    filename = "TechDocument" + ID + "." + s[s.Length - 1];
                    //convert Pdf into Byte
                    Stream fs = file.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    //Convert byte into Base64
                    string Bs64 = Convert.ToBase64String(bytes);

                    //Generate Command
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<request> <command>pkiNetworkSign</command> <ts>");
                    sb.Append(DateTime.Now.ToString());
                    sb.Append("</ts><txn>");
                    sb.Append("unique id</txn><certificate><attribute name='CN'></attribute><attribute name='O'></attribute>");
                    sb.Append("<attribute name='OU'></attribute><attribute name='T'></attribute><attribute name='E'></attribute>");
                    sb.Append("<attribute name='SN'></attribute><attribute name='CA'></attribute>");
                    sb.Append("<attribute name='TC'>SG</attribute>");
                    sb.Append("<attribute name='AP'>1</attribute><attribute name='VD'></attribute>");
                    sb.Append("</certificate>");
                    sb.Append("<file><attribute name='type'>pdf</attribute></file>");
                    sb.Append("<pdf><page>1</page><cood>78,56</cood><size>200,100</size></pdf>");
                    sb.Append("<data>");
                    sb.Append(Bs64);
                    sb.Append("</data></request>");
                    string data = sb.ToString();
                    return new JsonResult { Data = new { msg = "success", resp = sb.ToString() }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                catch (Exception ex) { return new JsonResult { Data = new { msg = ex.Message, resp = "" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; }

            }
            else
                return new JsonResult { Data = new { msg =  "Please Upload a file", resp=""}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               
            
            
        }
        public JsonResult CertExtCommanccd()
        {
            StringBuilder sb = new StringBuilder("<request><command>pkiNetworkCertExt</command><ts>");
            sb.Append(DateTime.Now.ToString());
            sb.Append("</ts><txn>unique id</txn><certificate><attribute name='CN'></attribute><attribute name='O'></attribute><attribute name='OU'></attribute><attribute name='T'></attribute><attribute name='E'></attribute><attribute name='SN'>‎‎</attribute><attribute name='CA'></attribute><attribute name='TC'>sg</attribute><attribute name='AP'>1</attribute></certificate></request>");
            string cmd = sb.ToString();
            return new JsonResult { Data = new {resp = cmd }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AuthCommanccd()
        {
            StringBuilder sb = new StringBuilder("<request><command>pkiNetworkCertAuth</command><ts>");
            sb.Append(DateTime.Now.ToString());
            sb.Append("</ts><txn>unique id</txn><certificate><attribute name='CN'></attribute><attribute name='O'></attribute><attribute name='OU'></attribute><attribute ='T'></attribute><attribute name='E'></attribute><attribute name='SN'>‎‎</attribute><attribute name='CA'></attribute><attribute name='TC'>sg</attribute><attribute name='AP'>1</attribute></certificate></request>​");
            string cmd = sb.ToString();
            return new JsonResult { Data = new { resp = cmd }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCertificateDetails(string baseString)
        {
            byte[] bytecert = Convert.FromBase64String(baseString);
            string s = Encoding.UTF8.GetString(bytecert);
            CertficateExtratedetails CE = new CertficateExtratedetails();
            XElement Xe = XElement.Parse(s);
            CE.SubjectName_CN = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "CN").Value;
            //CE.ApplicationValidityDate_AD = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "AD").Value;
            CE.CertifyingAuthority_CA = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "CA").Value;
            CE.Email_E = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "E").Value;
            CE.Organization_O= Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "O").Value;
            CE.OrganizationUnit_OU = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "OU").Value;
            CE.Serialnumber_SN = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "SN").Value;
            //CE.Title_T = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "T").Value;
            //CE.Typeofcertificate_TC = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "TC").Value;
            CE.Expiry_Date = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "Expiry Date").Value;
            CE.OCSPVerify = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "OCSPVerify").Value;
            CE.Valid_Date = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "Valid Date").Value;
            CE.CRLVerify = Xe.Elements().FirstOrDefault(x => x.Attribute("name").Value.ToString() == "CRLVerify").Value;
            return new JsonResult { Data = new { CertDetails = CE }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
