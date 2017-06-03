using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
    public class tbl_Bid
    {
        public int ID { get; set; }
        public int VendorID { get; set; }
        public int TenderID { get; set; }
        public DateTime? BidSubmitDate { get; set; }
        public int Status { get; set; }
        public string TechRefID { get; set; }
        public string TechIssuer { get; set; }
        public DateTime? TechValidUpto { get; set; }
        public string TenderDoc { get; set; }
        public string EMDDoc { get; set; }
        public string EMDRefId { get; set; }
        public string EMDIssuer { get; set; }
        public DateTime? EMDValidUpto { get; set; }
        public string IPAddress { get; set; }
        public string TechDoc { get; set; }
        public string FinancialDoc { get; set; }
        public float TotalAmount { get; set; }
        public int FreezeStatus { get; set; }
        public DateTime? FreezeDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public string LastActivityIP { get; set; }

    }
}
