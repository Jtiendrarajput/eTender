using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.Tempmodel
{
    public class Temp_TendersHistory
    {
        public int ID { get; set; }
        public string TenderID { get; set; }
        public string Title { get; set; }
        public DateTime BidStartDate { get; set; }
        public DateTime FreezDate { get; set; }
        public string DeptCode { get; set; }
        public string DepartmentName { get; set; }
        public string CatCode { get; set; }
        public string CategoryName { get; set; }
        public string StatusType { get; set; }
        public string Status { get; set; }
    }
}
