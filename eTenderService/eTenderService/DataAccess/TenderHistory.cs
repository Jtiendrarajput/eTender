using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using eTenderService.Tempmodel;

namespace eTenderService.DataAccess
{
   
    public class TenderHistory
    {
        static string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public static List<Temp_TendersHistory> TenderHistoryDetail(int UserId)
        {
            List<Temp_TendersHistory> lst = new List<Temp_TendersHistory>();
            using(SqlConnection con = new SqlConnection(ConString))
            {
                DataTable dt = new DataTable();
                string query = "Exec TendersHistory " + UserId + " ";
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(query, con);
                ada.Fill(dt);
                con.Close();
                foreach(DataRow dr in dt.Rows)
                {
                    Temp_TendersHistory TH = new Temp_TendersHistory();
                    TH.ID = Convert.ToInt32(dr["ID"].ToString());
                    TH.TenderID = dr["TenderID"].ToString();
                    TH.StatusType = dr["StatusType"].ToString();
                    TH.Title = dr["Title"].ToString();
                    TH.BidStartDate = string.IsNullOrEmpty(dr["BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartDate"].ToString());
                    TH.FreezDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                    TH.DeptCode = dr["DeptCode"].ToString();
                    TH.DepartmentName = dr["DepartmentName"].ToString();
                    TH.CatCode = dr["CatCode"].ToString();
                    TH.CategoryName = dr["CategoryName"].ToString();
                    TH.Status = dr["Status"].ToString();

                    lst.Add(TH);
                }
            }

            return lst;
        }








    }
}
