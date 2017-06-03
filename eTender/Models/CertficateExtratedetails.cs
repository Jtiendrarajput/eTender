using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eTender.Models
{
    public class CertficateExtratedetails
    {
        public string SubjectName_CN { get; set; }
        public string Organization_O { get; set; }
        public string OrganizationUnit_OU { get; set; }
        public string Title_T { get; set; }
        public string Email_E { get; set; }
        public string Serialnumber_SN { get; set; }
        public string CertifyingAuthority_CA { get; set; }
        public string Typeofcertificate_TC { get; set; }
        public string Application_AP { get; set; }
        public string ApplicationValidityDate_AD { get; set; }
        public string Valid_Date { get; set; }
        public string Expiry_Date { get; set; }
        public string CRLVerify { get; set; }
        public string OCSPVerify { get; set; }
    }
}