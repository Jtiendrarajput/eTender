using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
   public class tblUserProfile
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }


        public string Address { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public string ActiveBy { get; set; }
        public string password { get; set; }

        public string LastLoginIP { get; set; }
        public DateTime? currentLogindate { get; set; }
        public string currentIP { get; set; }
        public DateTime? LastLoginDatenTime { get; set; }
    }
}
