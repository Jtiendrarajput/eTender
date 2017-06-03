using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.Tempmodel
{
    public class Temp_BidHistory
    {
        public int ID { get; set; }
        public int VendorID { get; set; }
        public string  TenderID { get; set; }
        public string Title { get; set; }
        //public DateTime Tender_ActiveDate { get; set; }
        //public DateTime Tender_BidStartDate { get; set; }
        //public DateTime Tender_FreezeDate { get; set; }
        //public DateTime Tender_DownloadStartDate { get; set; }
        //public DateTime Tender_DownloadEndDate { get; set; }
        //public DateTime Tender_PublishDate { get; set; }
        //public DateTime Tender_TechBidOpenDate { get; set; }
        //public DateTime Tender_FinancialBidOpenDate { get; set; }
        //public DateTime Tender_ClarificationStartDate { get; set; }
        //public DateTime Tender_ClarificationEndDate { get; set; }

        public DateTime BidSubmitDate { get; set; }
        public int Status { get; set; }
        public string TechRefID { get; set; }
        public string TechIssuer { get; set; }
        public DateTime TechValidUpto { get; set; }
        public string EMDRefId { get; set; }
        public string EMDIssuer { get; set; }
        public DateTime EMDValidUpto { get; set; }
        public string IPAddress { get; set; }
        public string TechDoc { get; set; }
        public string FinancialDoc { get; set; }
        public float TotalAmount { get; set; }
        public int FreezeStatus { get; set; }
        public DateTime FreezeDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string LastActivityIP { get; set; }
        public int TenderAutoID { get; set; }
        public string FinalBidStatus { get; set; }
        public string BidType { get; set; }
    }
}
