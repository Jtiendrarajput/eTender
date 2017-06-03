using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
    public class tbl_BidStatus
    {
        public int ID { get; set; }
        public int BidID { get; set; }
        public int Status { get; set; }
        public DateTime SDate { get; set; }
        public string BidType { get; set; }
    }
}
