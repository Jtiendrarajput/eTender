using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.Tempmodel
{
    public class Temp_TendersBidders
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNo { get; set; }
        public string RegisteredAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public int  CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public int ISD_STDCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FreezeStatus { get; set; }

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
     
        public DateTime FreezeDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string LastActivityIP { get; set; }
        public string TenderDoc { get; set; }
        public string EMDDoc { get; set; }

        public int BidStatusID { get; set; }

        public string BidStatus { get; set; }
        public int BidID { get; set; }
            

    }
}
