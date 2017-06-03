using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using eTenderService.Tempmodel;
using eTenderService.DataModel;

namespace eTenderService.DataAccess
{
    public class All_FreezedTenders
    {
        static string ConString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        //[All_FreezedTechnicalTenders]//
        public static List<Temp_AllTenders> AllFreezedTenders()
        {

            try
            {
                string query = "";
                List<Temp_AllTenders> lst = new List<Temp_AllTenders>();
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    query = "Exec [All_FreezedTechnicalTenders] ";
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    con.Open();
                    ada.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Temp_AllTenders Temp = new Temp_AllTenders();
                        Temp.ID = Convert.ToInt32(dr["ID"].ToString());
                        Temp.TenderID = dr["TenderID"].ToString();
                        Temp.UserName = dr["Name"].ToString();
                        Temp.Title = dr["Title"].ToString();

                        Temp.BidStartDate = string.IsNullOrEmpty(dr["BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartDate"].ToString());

                        Temp.DeptCode = dr["DeptCode"].ToString();
                        Temp.DeptName = dr["DepartmentName"].ToString();

                        Temp.CatCode = dr["CatCode"].ToString();
                        Temp.CateName = dr["CategoryName"].ToString();

                        Temp.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());

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

        public static List<Temp_AllTenders> TechnicallyApprovedTenders()
        {
            try
            {
                List<Temp_AllTenders> lst = new List<Temp_AllTenders>();
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec [TechnicallyApprovedTenders]";
                    con.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    ada.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Temp_AllTenders Temp = new Temp_AllTenders();
                        Temp.ID = int.Parse(dr["TID"].ToString());
                        Temp.TenderID = dr["TenderID"].ToString();
                        Temp.UserName = dr["Name"].ToString();
                        Temp.Title = dr["Title"].ToString();

                        Temp.BidStartDate = string.IsNullOrEmpty(dr["BidStartDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidStartDate"].ToString());

                        Temp.DeptCode = dr["DeptCode"].ToString();
                        Temp.DeptName = dr["DepartmentName"].ToString();

                        Temp.CatCode = dr["CatCode"].ToString();
                        Temp.CateName = dr["CategoryName"].ToString();

                        Temp.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
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
        //Financial Bidders
        public static List<Temp_TendersBidders> TendersBidders(int TenderId)
        {
            try
            {
                List<Temp_TendersBidders> lst = new List<Temp_TendersBidders>();
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec TendersBidders  " + TenderId + " ";
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    con.Open();
                    ada.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Temp_TendersBidders Temp = new Temp_TendersBidders();
                        Temp.ID = Convert.ToInt32(dr["ID"].ToString());
                        Temp.CompanyName = dr["CompanyName"].ToString();
                        Temp.RegistrationNo = dr["RegistrationNo"].ToString();
                        Temp.RegisteredAddress = dr["RegisteredAddress"].ToString();
                        Temp.City = dr["City"].ToString();
                        Temp.State = dr["State"].ToString();
                        Temp.PostalCode = int.Parse(dr["PostalCode"].ToString());
                        Temp.Country = dr["Country"].ToString();
                        Temp.DOB = Convert.ToDateTime(dr["DOB"].ToString());
                        Temp.Email = dr["Email"].ToString();
                        Temp.Designation = dr["Designation"].ToString();
                        Temp.CountryCode = int.Parse(dr["CountryCode"].ToString());
                        Temp.MobileNumber = dr["MobileNumber"].ToString();
                        Temp.PhoneNumber = dr["PhoneNumber"].ToString();
                       
                        Temp.BidSubmitDate = string.IsNullOrEmpty(dr["BidSubmitDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidSubmitDate"].ToString());

                        Temp.Status = Convert.ToInt32(dr["Status"].ToString());
                        Temp.TechRefID = dr["TechRefID"].ToString();
                        Temp.TechIssuer = dr["TechIssuer"].ToString();
                        Temp.TechValidUpto = string.IsNullOrEmpty(dr["TechValidUpto"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["TechValidUpto"].ToString());

                        Temp.EMDRefId = dr["EMDRefId"].ToString();
                        Temp.EMDIssuer = dr["EMDIssuer"].ToString();
                        Temp.EMDValidUpto = string.IsNullOrEmpty(dr["EMDValidUpto"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["EMDValidUpto"].ToString());
                        Temp.IPAddress = dr["IPAddress"].ToString();
                        Temp.TechDoc = "~/TechDocumnet/" + dr["TechDoc"].ToString();
                        Temp.FinancialDoc = "~/FinancialDocumnet/" + dr["FinancialDoc"].ToString();

                        Temp.TotalAmount = string.IsNullOrEmpty(dr["TotalAmount"].ToString()) ? 0 : float.Parse(dr["TotalAmount"].ToString());
                        Temp.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                        Temp.LastActivityDate = string.IsNullOrEmpty(dr["LastActivityDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["LastActivityDate"].ToString());

                        Temp.LastActivityIP = dr["LastActivityIP"].ToString();
                        Temp.TenderDoc = dr["TenderDoc"].ToString();
                        Temp.EMDDoc = dr["EMDDoc"].ToString();
                        Temp.BidID = int.Parse(dr["BidID"].ToString());
                       
                            Temp.BidStatus = dr["BidStatus"].ToString();
                       
                            Temp.BidStatusID = int.Parse(dr["BidStatusID"].ToString());
                        
                     

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
        //Technical Bidders
        public static List<Temp_TendersBidders> TendersTechnicalBidders(int TenderId)
        {
            try
            {
                List<Temp_TendersBidders> lst = new List<Temp_TendersBidders>();

                using (SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec TendersTechnicalBidders " + TenderId + " ";
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    con.Open();
                    ada.Fill(dt);
                    con.Close();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Temp_TendersBidders Temp = new Temp_TendersBidders();
                        Temp.ID = Convert.ToInt32(dr["ID"].ToString());
                        Temp.CompanyName = dr["CompanyName"].ToString();
                        Temp.RegistrationNo = dr["RegistrationNo"].ToString();
                        Temp.RegisteredAddress = dr["RegisteredAddress"].ToString();
                        Temp.City = dr["City"].ToString();
                        Temp.State = dr["State"].ToString();
                        Temp.PostalCode = int.Parse(dr["PostalCode"].ToString());
                        Temp.Country = dr["Country"].ToString();
                        Temp.DOB = Convert.ToDateTime(dr["DOB"].ToString());
                        Temp.Email = dr["Email"].ToString();
                        Temp.Designation = dr["Designation"].ToString();
                        Temp.CountryCode = int.Parse(dr["CountryCode"].ToString());
                        Temp.MobileNumber = dr["MobileNumber"].ToString();
                        Temp.ISD_STDCode = int.Parse(dr["ISD_STDCode"].ToString());
                        Temp.PhoneNumber = dr["PhoneNumber"].ToString();

                        Temp.BidSubmitDate = string.IsNullOrEmpty(dr["BidSubmitDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["BidSubmitDate"].ToString());

                        Temp.Status = Convert.ToInt32(dr["Status"].ToString());
                        Temp.TechRefID = dr["TechRefID"].ToString();
                        Temp.TechIssuer = dr["TechIssuer"].ToString();
                        Temp.TechValidUpto = string.IsNullOrEmpty(dr["TechValidUpto"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["TechValidUpto"].ToString());

                        Temp.EMDRefId = dr["EMDRefId"].ToString();
                        Temp.EMDIssuer = dr["EMDIssuer"].ToString();
                        Temp.EMDValidUpto = string.IsNullOrEmpty(dr["EMDValidUpto"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["EMDValidUpto"].ToString());
                        Temp.IPAddress = dr["IPAddress"].ToString();
                        Temp.TechDoc = "~/TechDocumnet/" + dr["TechDoc"].ToString();
                        Temp.FinancialDoc = "~/FinancialDocumnet/" + dr["FinancialDoc"].ToString();

                        Temp.TotalAmount = string.IsNullOrEmpty(dr["TotalAmount"].ToString()) ? 0 : float.Parse(dr["TotalAmount"].ToString());
                        Temp.FreezeDate = string.IsNullOrEmpty(dr["FreezeDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["FreezeDate"].ToString());
                        Temp.LastActivityDate = string.IsNullOrEmpty(dr["LastActivityDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["LastActivityDate"].ToString());

                        Temp.LastActivityIP = dr["LastActivityIP"].ToString();
                        Temp.TenderDoc = dr["TenderDoc"].ToString();
                        Temp.EMDDoc = dr["EMDDoc"].ToString();
                        Temp.BidStatus = dr["BidStatus"].ToString();

                        Temp.BidStatusID = int.Parse(dr["BidStatusID"].ToString());
                        
                     
                        
                        
                        Temp.BidID = int.Parse(dr["BidID"].ToString());
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
    
        public static List<Temp_CommMembersForTenders> CommMembersTender(int TenderId)
        {
            List<Temp_CommMembersForTenders> lst = new List<Temp_CommMembersForTenders>();
            try
            {
                using(SqlConnection con = new SqlConnection(ConString))
                {
                    DataTable dt = new DataTable();
                    string query = "Exec CommMembersForTender " + TenderId + "";
                    con.Open();
                    SqlDataAdapter ada = new SqlDataAdapter(query, con);
                    ada.Fill(dt);
                    con.Close();
                    foreach(DataRow dr in dt.Rows)
                    {
                        Temp_CommMembersForTenders Temp = new Temp_CommMembersForTenders();
                        Temp.TenderID = Convert.ToInt32(dr["TenderID"].ToString());
                        Temp.Name = dr["Name"].ToString();
                        Temp.CountryCode = Convert.ToInt32(dr["CountryCode"].ToString());
                        Temp.MobileNumber = dr["MobileNumber"].ToString();
                        Temp.Email = dr["Email"].ToString();
                        Temp.Address = dr["Address"].ToString();
                        Temp.Department = dr["Department"].ToString();
                        Temp.Designation = dr["Designation"].ToString();
                        Temp.OTP = dr["OTP"].ToString();
                        Temp.MobileVerify = Convert.ToInt32(dr["MobileVerify"].ToString());
                        Temp.ActiveBy = Convert.ToInt32(dr["ActiveBy"].ToString());
                        Temp.MemberID = int.Parse(dr["MemberID"].ToString());
                        //Temp.CDate = string.IsNullOrEmpty(dr["CDate"].ToString()) ? DateTime.Now : Convert.ToDateTime(dr["CDate"].ToString());
                        //Temp.Status = Convert.ToInt32(dr["Status"].ToString());
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

        //public static string SendOTP(string Mobile)
        //{

        //    try
        //    {
        //        string msg = "";
        //        string OTPmsg = "";
        //        using (DB db = new DB())
        //        {
        //            int num = db.tbl_CommiteeMember.Count(x => x.MobileNumber == Mobile);

        //            if (num != 0)
        //            {
        //                tbl_CommiteeMember member = db.tbl_CommiteeMember.FirstOrDefault(x => x.MobileNumber == Mobile);
        //                member.OTP = OtherFunction.GenerateOTP();
        //                OTPmsg = "Nagar Palika Parishad Mathura!!  Your Mobile Verification Code is " + member.OTP;

        //                SMSService.Send(OTPmsg, member.MobileNumber);
        //                msg = "OTP has been sent to your registered mobile number";
        //                db.SaveChanges();
        //            }
        //            else
        //            {
        //                msg = "Your Mobile Number is not Registered";
        //            }
        //        }
        //        return msg;
        //    }
        //    catch (Exception ex) { throw ex; }
        //}


        public static Temp_OTPVerifyCommitteeMember SendOTP(int ActionStatus, string Status, int TenderId, int BiddingId)
                      {
            try
            {
                int StatusReturn = 0;
                string OTPmsg = "";
                bool IsTrue = false;

                Temp_OTPVerifyCommitteeMember SCTemp = new Temp_OTPVerifyCommitteeMember();

                List<Temp_CommMembersForTenders> lst = new List<Temp_CommMembersForTenders>();
                List<Temp_OTPReturnCommitteeList> Memlst = new List<Temp_OTPReturnCommitteeList>();
                lst = CommMembersTender(TenderId);
                using (DB db = new DB())
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        //var ID = db.tbl_OTPStatusCommiteeVerifiys.Single(x => x.TendorID == TenderId && x.CommiteeMemberID == lst[i].MemberID && x.BidderID == BiddingId && x.ActionStatus == ActionStatus && x.StatusType == Status).Verified;

                        int MID = lst[i].MemberID;
                        var Item = (from a in db.tbl_OTPStatusCommiteeVerifiys
                                    orderby a.CurrentDate descending
                                    where a.TendorID == TenderId && a.CommiteeMemberID == MID && a.BidderID == BiddingId && a.ActionStatus == ActionStatus && a.StatusType == Status
                                    select new
                                    {
                                        a.Verified
                                    }).FirstOrDefault();

                        if (Item != null)
                        {
                            if (Item.Verified == 0)
                            {
                                IsTrue = true;
                                break;
                            }
                            else
                            {
                                IsTrue = false;

                            }
                        }
                        else
                        {
                            IsTrue = true;
                            break;
                        }
                    }

                    if (IsTrue == true)
                    {

                        for (int j = 0; j < lst.Count; j++)
                        {

                            //tbl_OTPStatusCommiteeVerifiy member = db.tbl_OTPStatusCommiteeVerifiys.FirstOrDefault(x => x.CommiteeMemberID == lst[j].MemberID &&                                 x.TendorID == TenderId);

                            tbl_OTPStatusCommiteeVerifiy OTPmember = new tbl_OTPStatusCommiteeVerifiy();
                            OTPmember.BidderID = BiddingId;
                            OTPmember.TendorID = TenderId;
                            OTPmember.CommiteeMemberID = lst[j].MemberID;
                            OTPmember.ActionStatus = ActionStatus;
                            OTPmember.StatusType = Status;
                            OTPmember.OTP = OtherFunction.GenerateOTP();
                            OTPmember.CurrentDate = DateTime.Now;
                            OTPmsg = "Nagar Palika Parishad Mathura!!  Your Mobile Verification Code is " + OTPmember.OTP;

                            SMSService.Send(OTPmsg, lst[j].MobileNumber);
                            OTPmember.Verified = 0;

                            db.tbl_OTPStatusCommiteeVerifiys.Add(OTPmember);
                            db.SaveChanges();
                            int MID = lst[j].MemberID;
                            tbl_CommiteeMember Member = db.tbl_CommiteeMember.FirstOrDefault(x => x.ID == MID && x.status == 1);


                            Temp_OTPReturnCommitteeList temp = new Temp_OTPReturnCommitteeList();
                            temp.ID = j+1;
                            temp.MemberID = Member.ID;
                            temp.Name = Member.Name;
                            temp.MobileNumber = Member.MobileNumber;
                            temp.CountryCode = Member.CountryCode;
                            temp.Email = Member.Email;
                            temp.OTPID = OTPmember.ID;

                            Memlst.Add(temp);

                            StatusReturn = 1;
                        }

                     
                    }
                    else { StatusReturn = 0; }


                }

                SCTemp.CommitteeMember = Memlst;
                SCTemp.Status = StatusReturn;
                return SCTemp;
            }
            catch (Exception ex) { throw ex; }
        }

        public static bool VerifyMobile(Temp_OTPVerifyCommitteeMember TempOTPlst)
        {

            try
            {

                bool b = false;
                using (DB db = new DB())
                {
                    for (int i = 0; i < TempOTPlst.CommitteeMember.Count; i++)
                    {
                        int OTPID = TempOTPlst.CommitteeMember[i].OTPID;
                        tbl_OTPStatusCommiteeVerifiy v = db.tbl_OTPStatusCommiteeVerifiys.FirstOrDefault(x => x.ID == OTPID);
                        if (v != null)
                        {
                            if (TempOTPlst.CommitteeMember[i].OTP == v.OTP)
                            {
                                v.Verified = 1;
                                db.SaveChanges();
                                b = true;
                            }
                            else
                            {
                                b = false;
                            }
                        }
                    }
                }
                return b;

            }
            catch (Exception ex) { throw ex; }

        }

        public static bool OTPVerifiedsuccess(int ActionStatus, string Status, int TenderId, int BiddingId)
        {
            try
            {
                using (DB db = new DB())
                {
                    tbl_BidStatus tblbid = new tbl_BidStatus();
                    tblbid.BidID = BiddingId;
                    tblbid.BidType = Status;
                    tblbid.SDate = DateTime.Now;
                    tblbid.Status = ActionStatus;
                    db.tbl_BidStatus.Add(tblbid);
                    db.SaveChanges();
                   

                    if (ActionStatus == 6 && Status == "Financial")
                    {
                        tbl_TenderDetails tendertable = db.tblTenderDetails.FirstOrDefault(x => x.ID == TenderId);
                        tendertable.AlotStatus = 1;
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
