using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
   public class tbl_VendorDetails
    {
       public int ID { get; set; }
       public int userID { get; set; }
       public string Email { get; set; }
       public string CompanyName { get; set; }
       public string RegistrationNo { get; set; }
       public string RegisteredAddress { get; set; }
       public string NameOfPartners { get; set; }
       public string City { get; set; }
       public string State { get; set; }
       public int PostalCode { get; set; }
       public string Country { get; set; }
       public int EstablishmentYear { get; set; }
       public string NatureOfBusiness { get; set; }
       public string LegalStatus { get; set; }
       public string CompanyCategory { get; set; }

       public string Title { get; set; }
       public string ContactName { get; set; }
       public DateTime? DOB { get; set; }
       public string ContactEmail { get; set; }
       public string Designation { get; set; }
       public int CountryCode {get; set;}
       public string MobileNumber { get; set; }
       public int ISD_STDCode { get; set; }
       public string PhoneNumber { get; set; }

       public string BidderPreRegisteredWith { get; set; }
       public string OrganisationType { get; set; }
       public string UdyogAadharNumber { get; set; }
       public string BidderRegisteredType { get; set; }
       public string Category { get; set; }

       public DateTime? RegistrationDate
       {
           get;
           set;
       }
       public int MobileConfirmationStatus { get; set; }
       public int EmailConfirmationStatus { get; set; }
       public int CEmailConfirmationStatus { get; set; }
       public string EmailOTP { get; set; }
       public string CEmailOTP { get; set; }
       public string MobileOTP { get; set; }
       public DateTime? ActivationDate { get; set; }
       public int ActiveStatus { get; set; }
       public DateTime? LastLoginDatenTime { get; set; }
       public int ProfileActivatedBy { get; set; }
       public string Password { get; set; }
       public string LastLoginIP { get; set; }
       public DateTime? currentLogindate { get; set; }
       public string currentIP { get; set; }
       public string PANNumber { get; set; }
       public string TINNumber { get; set; }
       //public int EPF { get; set; }
       //public int ESI { get; set; }

    }
}
