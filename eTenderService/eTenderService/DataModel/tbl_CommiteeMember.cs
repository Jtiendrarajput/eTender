using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
    public class tbl_CommiteeMember
    {
        public int ID { get; set; }
       
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

     
        public string Address { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string OTP { get; set; }
        public int MobileVerify { get; set; }
        public int ActiveBy { get; set; }
        public int status { get; set; }

    }
}