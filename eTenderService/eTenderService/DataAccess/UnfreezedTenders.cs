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
    public class UnfreezedTenders
    {
        static string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public static List<Temp_UnfreezedTenders> Unfreezedtenders(int UserId)
        {
            List<Temp_UnfreezedTenders> lst = new List<Temp_UnfreezedTenders>();
            using(SqlConnection con = new SqlConnection(ConString))
            {
                DataTable dt = new DataTable();
                string query = "Exec UnFreezedTenders " + UserId + " ";
                con.Open();
                SqlDataAdapter ada = new SqlDataAdapter(query, con);
                ada.Fill(dt);
                con.Close();
                foreach(DataRow dr in dt.Rows)
                {
                    Temp_UnfreezedTenders UF = new Temp_UnfreezedTenders();
                    UF.ID = Convert.ToInt32(dr["ID"].ToString());
                    UF.TenderID = dr["TenderID"].ToString();
                  
                    UF.Title = dr["Title"].ToString();
                    UF.BidStartDate = string.IsNullOrEmpty(dr["BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartDate"].ToString());
                    UF.FreezDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                    UF.DeptCode = dr["DeptCode"].ToString();
                    UF.DepartmentName = dr["DepartmentName"].ToString();
                    UF.CatCode = dr["CatCode"].ToString();
                    UF.CategoryName = dr["CategoryName"].ToString();
                    
                    lst.Add(UF);
                }
            }

            return lst;
        }


    }
}
