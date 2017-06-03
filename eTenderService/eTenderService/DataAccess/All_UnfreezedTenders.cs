using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using eTenderService.DataAccess;
using eTenderService.Tempmodel;


namespace eTenderService.DataAccess
{
    public class All_UnfreezedTenders
    {
       

        static string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public static List<Temp_AllTenders> AllUnfreezedTender()
        {
            try
            {
                List<Temp_AllTenders> lst = new List<Temp_AllTenders>();
                using(SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec All_UnfreezedTenders";
                    con.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    ada.Fill(dt);
                    con.Close();
                    foreach(DataRow dr in dt.Rows)
                    {
                        Temp_AllTenders Temp = new Temp_AllTenders();
                        Temp.ID = Convert.ToInt32(dr["ID"].ToString());
                        Temp.UserName = dr["Name"].ToString();
                        Temp.TenderID = dr["TenderID"].ToString();
                        Temp.Title = dr["Title"].ToString();
                        Temp.ActiveDate = string.IsNullOrEmpty(dr["ActiveDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ActiveDate"].ToString());
                        Temp.BidStartDate = string.IsNullOrEmpty(dr["BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartDate"].ToString());
                        Temp.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                        Temp.DeptName = dr["DepartmentName"].ToString();
                        Temp.CateName = dr["CategoryName"].ToString();
                        Temp.DownloadStartDate = string.IsNullOrEmpty(dr["DownloadStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["DownloadStartDate"].ToString());
                        Temp.DownloadEndDate = string.IsNullOrEmpty(dr["DownloadEndDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["DownloadEndDate"].ToString());
                        Temp.BOQFilePath = dr["BOQFilePath"].ToString();
                        Temp.TenderDocPath = dr["TenderDocPath"].ToString();
                        Temp.TenderNoticePath = dr["TenderNoticePath"].ToString();
                        Temp.PublishDate = string.IsNullOrEmpty(dr["PublishDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["PublishDate"].ToString());
                        Temp.TechBidOpenDate = string.IsNullOrEmpty(dr["TechBidOpenDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["TechBidOpenDate"].ToString());

                        Temp.FinancialBidOpenDate = string.IsNullOrEmpty(dr["FinancialBidOpenDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FinancialBidOpenDate"].ToString());

                        Temp.ClarificationStartDate = string.IsNullOrEmpty(dr["ClarificationStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ClarificationStartDate"].ToString());

                        Temp.ClarificationEndDate = string.IsNullOrEmpty(dr["ClarificationEndDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ClarificationEndDate"].ToString());
                        Temp.TenderFee = string.IsNullOrEmpty(dr["TenderFee"].ToString()) ? 0 : float.Parse(dr["TenderFee"].ToString());
                        Temp.EMDFee =   string.IsNullOrEmpty(dr["EMDFee"].ToString()) ? 0 : float.Parse(dr["EMDFee"].ToString());
                        Temp.AlotStatus = Convert.ToInt32(dr["AlotStatus"].ToString());
                        Temp.Status = Convert.ToInt32(dr["Status"].ToString());

                        lst.Add(Temp);
                    }
                }

                return lst;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<Temp_TendersBidders> UnfreezedTendersBidders(int TenderId)
        {
            try
            {
                List<Temp_TendersBidders> lst = new List<Temp_TendersBidders>();
                using(SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec UnfreezedTendersBidders "+TenderId;
                    con.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    ada.Fill(dt);
                    con.Close();
                    foreach(DataRow dr in dt.Rows)
                    {
                        Temp_TendersBidders Temp = new Temp_TendersBidders();
                        Temp.ID = Convert.ToInt32(dr["ID"].ToString());
                        Temp.BidStatus = dr["BidStatus"].ToString();
                        Temp.City = dr["City"].ToString();
                        Temp.CompanyName = dr["CompanyName"].ToString();
                        Temp.Country = dr["Country"].ToString();
                        Temp.CountryCode = int.Parse(dr["CountryCode"].ToString());
                        Temp.Designation = dr["Designation"].ToString();
                        Temp.DOB = Convert.ToDateTime(dr["DOB"].ToString());
                        Temp.Email = dr["Email"].ToString();
                        Temp.MobileNumber = dr["MobileNumber"].ToString();
                        Temp.PhoneNumber = dr["PhoneNumber"].ToString();
                        Temp.ISD_STDCode = int.Parse(dr["ISD_STDCode"].ToString());
                        Temp.PostalCode = int.Parse(dr["PostalCode"].ToString());
                        Temp.RegisteredAddress = dr["RegisteredAddress"].ToString();
                        Temp.RegistrationNo = dr["RegistrationNo"].ToString();
                        Temp.State = dr["State"].ToString();
                        

                        lst.Add(Temp);
                    }
                }

                return lst;
            }
            catch (Exception ex) { throw ex; }
        }


    }
}
