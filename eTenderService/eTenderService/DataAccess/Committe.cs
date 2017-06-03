using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;
using eTenderService.Tempmodel;

namespace eTenderService.DataAccess
{
    public class Committe
    {


        public static int Add(tbl_CommiteeMember ComMember)
        {
            try
            {
                using (DB db = new DB())
                {
                    int Count = db.tbl_CommiteeMember.Count(x => x.MobileNumber == ComMember.MobileNumber && x.Email == ComMember.Email);
                    if (Count > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        ComMember.status = 1;
                        ComMember.MobileVerify = 0;
                        ComMember.OTP = OtherFunction.GenerateOTP();
                        db.tbl_CommiteeMember.Add(ComMember);
                        db.SaveChanges();
                        //SMSService.Send(ComMember.OTP, ComMember.MobileNumber);

                        return ComMember.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static int update(tbl_CommiteeMember ComMember)
        {
            try
            {
                using (DB db = new DB())
                {

                    tbl_CommiteeMember UpComm = db.tbl_CommiteeMember.FirstOrDefault(x => x.ID == ComMember.ID);
                    if (ComMember.MobileNumber != UpComm.MobileNumber)
                    {
                        UpComm.MobileVerify = 0;
                        UpComm.CountryCode = ComMember.CountryCode;
                        UpComm.MobileNumber = ComMember.MobileNumber;
                        UpComm.Email = ComMember.Email;
                        UpComm.Address = ComMember.Address;
                        UpComm.Department = ComMember.Department;
                        UpComm.Designation = ComMember.Designation;
                        UpComm.OTP = OtherFunction.GenerateOTP();
                        UpComm.ActiveBy = ComMember.ActiveBy;
                        db.SaveChanges();
                        SMSService.Send(ComMember.OTP, ComMember.MobileNumber);
                    }
                    else
                    {
                        
                        UpComm.CountryCode = ComMember.CountryCode;
                        UpComm.MobileNumber = ComMember.MobileNumber;
                        UpComm.Email = ComMember.Email;
                        UpComm.Address = ComMember.Address;
                        UpComm.Department = ComMember.Department;
                        UpComm.Designation = ComMember.Designation;
                        UpComm.ActiveBy = ComMember.ActiveBy;
                        db.SaveChanges();
                    }
                    return UpComm.ID;
                }

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
                using (DB db = new DB())
                {
                    tbl_CommiteeMember UpCom = db.tbl_CommiteeMember.FirstOrDefault(x => x.ID == ID);
                    UpCom.status = 0;
                    db.SaveChanges();
                  
                }
                return ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static bool VerifyMobile (tbl_CommiteeMember cmember)
        {
            try
            {
                bool b = false;
                using(DB db= new DB())
                {
                    tbl_CommiteeMember utc = db.tbl_CommiteeMember.FirstOrDefault(x => x.ID == cmember.ID);
                    if(utc.OTP==cmember.OTP)
                    {
                        utc.MobileVerify = 1;
                        db.SaveChanges();
                        b = true;
                    }
                }
                return b;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public static void sentOTP(tbl_CommiteeMember cmember)
        {
            try
            {
                
                using (DB db = new DB())
                {
                    tbl_CommiteeMember cmem = db.tbl_CommiteeMember.FirstOrDefault(x => x.ID == cmember.ID);
                    cmem.OTP = OtherFunction.GenerateOTP();
                    db.SaveChanges();
                   
                }
              
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public static List<tbl_CommiteeMember> AllCommitte()
        {
            List<tbl_CommiteeMember> lst = new List<tbl_CommiteeMember>();
            try
            {
                using(DB db = new DB())
                {

                    lst = db.tbl_CommiteeMember.Where(x => x.status == 1).ToList();
                }

                return lst;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }


    }
}
