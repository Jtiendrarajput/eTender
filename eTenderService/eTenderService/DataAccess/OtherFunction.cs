using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace eTenderService.DataAccess
{
    public class OtherFunction
    {
        static string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public static string GenerateOTP()
        {
            Random rendom = new Random();
            string number = rendom.Next(1, 999999).ToString("D6");
            return number;
        }
        public static string GetRoleByUser(string Username)
        {
            string role = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select webpages_Roles.RoleName from tblUser inner join webpages_UsersInRoles on tblUser.UserID = webpages_UsersInRoles.UserId inner join webpages_Roles on webpages_UsersInRoles.RoleId = webpages_Roles.RoleId where tblUser.UserName='" + Username + "'", con);
                con.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    role = dt.Rows[0][0].ToString();
            }
            return role;
        }

        public static bool CheckForMobVerifyUser(string UserName)
        {
            bool b = false;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select ID from [dbo].[tbl_VendorDetails] where MobileConfirmationStatus=1 And Email='" + UserName + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                    b = true;
            }
            return b;
        }

        public static bool CheckForVerifyUser(string UserName)
        {
            bool b = false;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select ID from [dbo].[tbl_VendorDetails] where  Email='" + UserName + "' AND ActiveStatus = 1", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                    b = true;
            }
            return b;
        }

        public static void SetRole(int userid, int roleid)
        {
            string role = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into webpages_UsersInRoles values(" + userid + "," + roleid + ")",con);
                cmd.ExecuteNonQuery();

                //SqlDataAdapter da = new SqlDataAdapter("insert into webpages_UsersInRoles values(" + userid + "," + roleid + ")", con);
                con.Close();

            }


        }

    }
}
