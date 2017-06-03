using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using eTenderService.Tempmodel;
using eTenderService.DataModel;



namespace eTenderService.DataAccess
{
   public class Vendor_sTendor
    {
        static  string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public static List<Temp_AllActiveTendors> All_ActiveTendors()
        {
            List<Temp_AllActiveTendors> lst = new List<Temp_AllActiveTendors>();
            using (SqlConnection con = new SqlConnection(ConString))
            {
                DataTable dt = new DataTable();
                string query = "Exec  All_ActiveTendors";
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(query, con);
                ada.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    Temp_AllActiveTendors At = new Temp_AllActiveTendors();
                    At.ID = Convert.ToInt32(dr["ID"].ToString());
                    At.UserName = dr["Name"].ToString();
                    At.Mobile = dr["MobileNumber"].ToString();
                    At.DeptCode = dr["DeptCode"].ToString();
                    At.DeptName = dr["DepartmentName"].ToString();
                    At.CateCode = dr["CatCode"].ToString();
                    At.CategoryName = dr["CategoryName"].ToString();

                    At.Title = dr["Title"].ToString();
                    At.ActiveDate = string.IsNullOrEmpty(dr["ActiveDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ActiveDate"].ToString());
                    At.BidStartDate = string.IsNullOrEmpty(dr["BidStartdate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartdate"].ToString());
                    At.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                    At.DownloadStartDate = string.IsNullOrEmpty(dr["DownloadStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["DownloadStartDate"].ToString());

                    At.TenderID = dr["TenderID"].ToString();

                    At.DownloadEndDate = string.IsNullOrEmpty(dr["DownloadEndDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["DownloadEndDate"].ToString());

                    At.BOQFilePath = dr["BOQFilePath"].ToString();
                    At.TenderDocPath = dr["TenderDocPath"].ToString();
                    At.TenderNoticePath = dr["TenderNoticePath"].ToString();

                    At.PublishDate = string.IsNullOrEmpty(dr["PublishDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["PublishDate"].ToString());
                    At.TechBidOpenDate = string.IsNullOrEmpty(dr["TechBidOpenDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["TechBidOpenDate"].ToString());
                    At.FinancialBidOpenDate = string.IsNullOrEmpty(dr["FinancialBidOpenDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FinancialBidOpenDate"].ToString());
                    At.ClarificationStartDate = string.IsNullOrEmpty(dr["ClarificationStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ClarificationStartDate"].ToString());
                    At.ClarificationEndDate = string.IsNullOrEmpty(dr["ClarificationEndDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ClarificationEndDate"].ToString());

                    At.TenderFee = string.IsNullOrEmpty(dr["TenderFee"].ToString()) ? 0 : float.Parse(dr["TenderFee"].ToString());
                    At.EMDFee = string.IsNullOrEmpty(dr["EMDFee"].ToString()) ? 0 : float.Parse(dr["EMDFee"].ToString());
                    
                    if(DateTime.Now >= At.DownloadStartDate && DateTime.Now <= At.DownloadEndDate)
                    {
                        At.Status = true;
                    }
                    else
                    {
                        At.Status = false;
                    }
                    lst.Add(At);
                }
            }

            return lst;
        }


        public static int CheckBiddStatus(int VendorID, int TendorID) {

            try
            {
                using (DB db = new DB())
                {
                    int n  =0;
                    tbl_TenderDetails tdtable = db.tblTenderDetails.FirstOrDefault(x => x.ID == TendorID);
                    if (tdtable.BidStartDate > DateTime.Now)
                    {
                        n = 3;
                    }
                    else
                    {
                        n = db.tbl_Bid.Count(x => x.VendorID == VendorID && x.TenderID == TendorID && x.FreezeStatus == 1);
                    }
                    return n;
                }
            }
            catch(Exception ex){throw ex;}
        
        }


















    }
}
