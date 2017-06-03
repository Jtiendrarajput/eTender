using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;

namespace eTenderService.Tempmodel
{
    public class Temp_TenderBidforStartBid 
    {
        public tbl_TenderDetails TenderDetails { get; set; }
        public tbl_Bid BidDetails { get; set; }
        public string DepartmentName { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
    }
}
