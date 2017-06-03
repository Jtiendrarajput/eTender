using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
    public class tbl_OTPStatusCommiteeVerifiy
    {
        public int ID { get; set; }
        public int BidderID { get; set; }
        public int TendorID { get; set; }
        public int CommiteeMemberID { get; set; }
        public int ActionStatus { get; set; }
        public string StatusType { get; set; }
        public string OTP { get; set; }
        public int Verified { get; set; }
        public DateTime? CurrentDate { get; set; }
    }
}
