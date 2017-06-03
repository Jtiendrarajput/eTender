using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
    public class tbl_MemberActionOTPDetails
    {
        public int ID { get; set; }
        public int TenderID { get; set; }
        public int BidID { get; set; }
        public int MemberID { get; set; }
        public int ActionStatus { get; set; }
        public int OTP { get; set; }
        public int OTPVerify { get; set; }
        public DateTime VerifyDate { get; set; }
    }
}
