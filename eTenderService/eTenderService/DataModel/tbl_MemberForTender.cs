using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.DataModel
{
    public class tbl_MemberForTender
    {
        public int ID { get; set; }
        public int TenderID { get;set; }
        public int MemberID { get; set; }
        public DateTime CDate { get; set; }
        public int Status { get; set; }
    }
}
