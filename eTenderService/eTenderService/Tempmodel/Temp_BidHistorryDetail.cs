using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.Tempmodel
{
    public class Temp_BidHistorryDetail
    {
        public int ID { get; set; }
        public string TenderID { get; set; }
        public string Title { get; set; }
        public DateTime BidStartDate { get; set; }
        public DateTime FreezeDate { get; set; }
        public string MemberName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int BidID { get; set; }
        public DateTime BidSubmitDate { get; set; }
        public string BidStatus { get; set; }
        public string BidType { get; set; }
        public string CompanyName { get; set;}
        public string LegalStatus {get; set;}
        public string CompanyCategory { get; set; }
        public int TenderAID { get; set; }
    }
}
