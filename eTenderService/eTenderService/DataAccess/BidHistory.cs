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

    public class BidHistory
    {
        static string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public static List<Temp_BidHistory> AllBidHistoy(int VendorId)
        {
            List<Temp_BidHistory> lst = new List<Temp_BidHistory>();
            using(SqlConnection  con = new  SqlConnection(ConString))
            {
                DataTable dt = new DataTable();
                string query = "Exec  All_BidHistory " + VendorId + " ";
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(query, con);
                ada.Fill(dt);
                con.Close();
                foreach(DataRow dr in dt.Rows)
                {
                    Temp_BidHistory TB = new Temp_BidHistory();
                    TB.ID = int.Parse(dr["ID"].ToString());
                    TB.VendorID = Convert.ToInt32(dr["VendorID"].ToString());
                    TB.TenderID = dr["TenderID"].ToString();
                    TB.Title = dr["Title"].ToString();
                    TB.TenderAutoID = Convert.ToInt32(dr["TenderAutoID"].ToString());
                    //TB.Tender_ActiveDate = string.IsNullOrEmpty(dr["ActiveDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ActiveDate"].ToString());

                    //TB.Tender_BidStartDate = string.IsNullOrEmpty(dr["Tender_BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_BidStartDate"].ToString());
                    //TB.Tender_FreezeDate = string.IsNullOrEmpty(dr["Tender_FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_FreezeDate"].ToString());
                    //TB.Tender_DownloadStartDate = string.IsNullOrEmpty(dr["Tender_DownloadStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_DownloadStartDate"].ToString());
                    //TB.Tender_DownloadEndDate = string.IsNullOrEmpty(dr["Tender_DownloadEndDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_DownloadEndDate"].ToString());
                    //TB.Tender_PublishDate = string.IsNullOrEmpty(dr["Tender_PublishDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_PublishDate"].ToString());
                    //TB.Tender_TechBidOpenDate = string.IsNullOrEmpty(dr["Tender_TechBidOpenDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_TechBidOpenDate"].ToString());
                    //TB.Tender_FinancialBidOpenDate = string.IsNullOrEmpty(dr["Tender_FinancialBidOpenDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_FinancialBidOpenDate"].ToString());
                    //TB.Tender_ClarificationStartDate = string.IsNullOrEmpty(dr["Tender_ClarificationStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_ClarificationStartDate"].ToString());
                    //TB.Tender_ClarificationEndDate = string.IsNullOrEmpty(dr["Tender_ClarificationEndDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["Tender_ClarificationEndDate"].ToString());

                    TB.BidSubmitDate = string.IsNullOrEmpty(dr["BidSubmitDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidSubmitDate"].ToString());
                    //TB.Status = Convert.ToInt32(dr["Status"].ToString());
                    TB.TechRefID = dr["TechRefID"].ToString();
                    TB.TechIssuer = dr["TechIssuer"].ToString();
                    TB.TechValidUpto = string.IsNullOrEmpty(dr["TechValidUpto"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["TechValidUpto"].ToString());
                    TB.EMDRefId = dr["EMDRefId"].ToString();
                    TB.TechIssuer = dr["TechIssuer"].ToString();
                    TB.EMDValidUpto = string.IsNullOrEmpty(dr["EMDValidUpto"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["EMDValidUpto"].ToString());
                    TB.IPAddress = dr["IPAddress"].ToString();
                    TB.TechDoc = dr["TechDoc"].ToString();
                    TB.FinancialDoc = dr["FinancialDoc"].ToString();
                    TB.TotalAmount = string.IsNullOrEmpty(dr["TotalAmount"].ToString()) ? 0 : float.Parse(dr["TotalAmount"].ToString());
                    TB.FreezeStatus = Convert.ToInt32(dr["FreezeStatus"].ToString());
                    TB.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                    TB.LastActivityDate = string.IsNullOrEmpty(dr["LastActivityDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["LastActivityDate"].ToString());

                    TB.LastActivityIP = dr["LastActivityIP"].ToString();
                    TB.FinalBidStatus = dr["finalBidStatus"].ToString();
                    TB.BidType = dr["BidType"].ToString();
                    lst.Add(TB);
                }
            }


            return lst;
            
        }

        public static List<Temp_BidHistorryDetail> BidHistoyDetail(int TenderId)
        {
            List<Temp_BidHistorryDetail> lst = new List<Temp_BidHistorryDetail>();
            using (SqlConnection con = new SqlConnection(ConString))
            {

                DataTable dt = new DataTable();
                string query = "Exec TenderAllBidders " + TenderId + " ";
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(query, con);
                ada.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    Temp_BidHistorryDetail BH = new Temp_BidHistorryDetail();

                    BH.BidID = Convert.ToInt32(dr["ID"].ToString());
                    BH.BidSubmitDate = string.IsNullOrEmpty(dr["BidSubmitDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidSubmitDate"].ToString());
                    BH.CompanyName = dr["CompanyName"].ToString();
                    BH.LegalStatus = dr["LegalStatus"].ToString();
                    BH.CompanyCategory = dr["CompanyCategory"].ToString();
                    BH.BidStatus = dr["BidStatus"].ToString();
                    BH.BidType = dr["BidType"].ToString();

                    lst.Add(BH);
                }

            }

            return lst;
        }

        public static List<Temp_BidHistorryDetail> TenderMemberslist(int TenderId)
        {
            List<Temp_BidHistorryDetail> lst = new List<Temp_BidHistorryDetail>();
            try
            {
                using (DB db = new DB())
                {
                    var details = (from a1 in db.tblTenderDetails
                                   join a2 in db.tbl_MemberForTender on a1.ID equals a2.TenderID
                                   join a3 in db.tbl_CommiteeMember on a2.MemberID equals a3.ID
                                   where a1.ID == TenderId && a2.Status == 1
                                   select new
                                   {
                                       a1.ID,
                                       a1.TenderID,
                                       a1.Title,
                                       a1.BidStartDate,
                                       a1.FreezeDate,
                                       a3.Name,
                                       a3.Department,
                                       a3.Designation

                                   }).ToList();

                    foreach (var v in details)
                    {
                        Temp_BidHistorryDetail Temp = new Temp_BidHistorryDetail();
                        Temp.TenderAID = v.ID;
                        Temp.TenderID = v.TenderID;
                        Temp.Title = v.Title;
                        Temp.BidStartDate = v.BidStartDate;
                        Temp.FreezeDate = v.FreezeDate;
                        Temp.MemberName = v.Name;
                        Temp.Department = v.Department;
                        Temp.Designation = v.Designation;

                        lst.Add(Temp);

                    }
                }
                return lst;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
