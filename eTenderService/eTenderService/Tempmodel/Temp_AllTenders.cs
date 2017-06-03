﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eTenderService.Tempmodel
{
    public class Temp_AllTenders
    {
        public int ID { get; set; }
        public string TenderID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime BidStartDate { get; set; }
        public DateTime FreezeDate { get; set; }
        public int DepartmentID { get; set; }
        public string DeptName { get; set; }
        public string CateName { get; set; }
        public int CategoryID { get; set; }
        public DateTime DownloadStartDate { get; set; }
        public DateTime DownloadEndDate { get; set; }
        public string BOQFilePath { get; set; }
        public string TenderDocPath { get; set; }
        public string TenderNoticePath { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime TechBidOpenDate { get; set; }
        public DateTime FinancialBidOpenDate { get; set; }
        public DateTime ClarificationStartDate { get; set; }
        public DateTime ClarificationEndDate { get; set; }
        public float TenderFee { get; set; }
        public float EMDFee { get; set; }
        public int AlotStatus { get; set; }
        public int Status { get; set; }
        public string CompanyName { get; set; }
        public string DeptCode { get; set; }
        public string CatCode { get; set; }
    }
}
