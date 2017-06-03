using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.Tempmodel
{
   public class Temp_OTPReturnCommitteeList
    {
       public int ID { get; set; }
       public int MemberID { get; set; }
       public string Name { get; set; }
       public int CountryCode { get; set; }
       public string MobileNumber { get; set; }
       public string Email { get; set; }

       public int OTPID { get; set; }
       public string OTP { get; set; }

    }
}
