using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;
using eTenderService.Tempmodel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace eTenderService.DataAccess
{
    public class CreateCommitte
    {
        static string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public static int Add(Temp_CommitteforTender ComTender)
        {
            try
            {
                using(DB db = new DB())
                {
                    
                    foreach(var memberID in ComTender.ComMemeberID)
                    {
                        db.tbl_MemberForTender.Add(new tbl_MemberForTender() { MemberID = memberID, TenderID = ComTender.TenderID, CDate = DateTime.Now, Status =                        1 });
                    }
                    
                    db.SaveChanges();

                    return ComTender.TenderID;
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static int Update(Temp_CommitteforTender MemTender)
        {
            try
            {
                //List<int> oldIds;
                using (DB db = new DB())
                {

                    var oldIds = (from a1 in db.tbl_MemberForTender
                                  join a2 in db.tbl_CommiteeMember on a1.MemberID equals a2.ID
                                  where a1.TenderID == MemTender.TenderID && a1.Status == 1
                                  select new { a1.MemberID ,a1.ID}).ToList();

                    Boolean updatestatus = false;
                    for(int i=0;i<oldIds.Count;i++)
                    {
                        for(int j=0;j<MemTender.ComMemeberID.Count;j++)
                        {
                                if(oldIds[i].MemberID != MemTender.ComMemeberID[j])
                                {
                                    updatestatus = true;
                                }
                                else
                                {
                                    updatestatus = false;
                                    break;
                                }
                        }

                        if(updatestatus == true)
                        {
                            int idddd = oldIds[i].ID;
                            tbl_MemberForTender mft = db.tbl_MemberForTender.FirstOrDefault(x => x.ID == idddd);
                            mft.Status = 0;
                            db.SaveChanges();
                        }
                    }

                    Boolean AddStatus = false;

                    for (int i = 0; i < MemTender.ComMemeberID.Count; i++)
                    {
                        for (int j = 0; j < oldIds.Count; j++)
                        {
                            if (oldIds[j].MemberID != MemTender.ComMemeberID[i])
                            {
                                AddStatus = true;
                            }
                            else
                            {
                                AddStatus = false;
                                break;
                            }
                        }
                        if(AddStatus == true)
                        {
                            
                                tbl_MemberForTender mft = new tbl_MemberForTender();
                                mft.CDate = DateTime.Now;
                                mft.MemberID = MemTender.ComMemeberID[i];
                                mft.TenderID = MemTender.TenderID;
                                mft.Status = 1;
                                db.tbl_MemberForTender.Add(mft);
                                db.SaveChanges();
                        }
                    }

                    

                }
                return MemTender.TenderID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static int delete(int ID)
        {
            try
            {
                using(DB db = new DB())
                {
                   List<tbl_MemberForTender> UpCom = db.tbl_MemberForTender.Where(x => x.TenderID == ID).ToList();
                   for (int i = 0; i < UpCom.Count; i++)
                   {
                       UpCom[i].Status = 0;
                   }
                    db.SaveChanges();
                    return ID;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<Temp_TenderWithComMember> detail()
        {
            try
            {
                List<Temp_TenderWithComMember> lst = new List<Temp_TenderWithComMember>();
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec TenderWithComMember ";
                    con.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    ada.Fill(dt);
                    con.Close();

                    foreach (DataRow dr in dt.Rows)
                    {

                        Temp_TenderWithComMember Temp = new Temp_TenderWithComMember();
                        Temp.ID = Convert.ToInt32(dr["ID"].ToString());
                        Temp.Title = dr["Title"].ToString();
                        Temp.TenderID = dr["TenderID"].ToString();
                        Temp.ActiveDate = string.IsNullOrEmpty(dr["ActiveDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ActiveDate"].ToString());
                        Temp.BidStartDate = string.IsNullOrEmpty(dr["BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartDate"].ToString());
                        Temp.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                        Temp.CategoryName = dr["CategoryName"].ToString();
                        Temp.DeptName = dr["DepartmentName"].ToString();
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
                        Temp.UserName = dr["Name"].ToString();
                        Temp.TenderFee = string.IsNullOrEmpty(dr["TenderFee"].ToString()) ? 0 : float.Parse(dr["TenderFee"].ToString());
                        Temp.EMDFee = string.IsNullOrEmpty(dr["EMDFee"].ToString()) ? 0 : float.Parse(dr["EMDFee"].ToString());
                        Temp.TenderMemCount = int.Parse(dr["MemberCount"].ToString());

                        lst.Add(Temp);
                    }

                    return lst;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<tbl_CommiteeMember> TenderComMebers(int TenderId)
        {
            try
            {
                List<tbl_CommiteeMember> lst = new List<tbl_CommiteeMember>();
                using (DB db = new DB())
                {
                    var allMemeber = (from a1 in db.tbl_MemberForTender
                                      join a2 in db.tbl_CommiteeMember on a1.MemberID equals a2.ID
                                      where a1.TenderID == TenderId && a2.status == 1 && a1.Status ==1
                                        select new
                                      {

                                          a2.ID,
                                          a2.Name,
                                          a2.CountryCode,
                                          a2.MobileNumber,
                                          a2.Email,
                                          a2.Address,
                                          a2.Department,
                                          a2.Designation,

                                      }).ToList();

                    foreach (var v in allMemeber)
                    {
                        tbl_CommiteeMember member = new tbl_CommiteeMember();
                        member.ID = v.ID;
                        member.Name = v.Name;
                        member.CountryCode = v.CountryCode;
                        member.MobileNumber = v.MobileNumber;
                        member.Email = v.Email;
                        member.Address = v.Address;
                        member.Department = v.Department;
                        member.Designation = v.Designation;

                        lst.Add(member);
                    }


                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Temp_TenderWithComMember> TenderNotInComMember()
        {
            try
            {
                List<Temp_TenderWithComMember> lst = new List<Temp_TenderWithComMember>();
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec TenderNotInComMember ";
                    con.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    ada.Fill(dt);
                    con.Close();

                    foreach (DataRow dr in dt.Rows)
                    {

                        Temp_TenderWithComMember Temp = new Temp_TenderWithComMember();
                        Temp.ID = Convert.ToInt32(dr["ID"].ToString());
                        Temp.Title = dr["Title"].ToString();
                        Temp.TenderID = dr["TenderID"].ToString();
                        Temp.ActiveDate = string.IsNullOrEmpty(dr["ActiveDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["ActiveDate"].ToString());
                        Temp.BidStartDate = string.IsNullOrEmpty(dr["BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartDate"].ToString());
                        Temp.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                        Temp.CategoryName = dr["CategoryName"].ToString();
                        Temp.DeptName = dr["DepartmentName"].ToString();
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
                        Temp.EMDFee = string.IsNullOrEmpty(dr["EMDFee"].ToString()) ? 0 : float.Parse(dr["EMDFee"].ToString());

                        lst.Add(Temp);
                    }

                    return lst;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
