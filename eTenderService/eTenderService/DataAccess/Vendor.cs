using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;

namespace eTenderService.DataAccess
{
    public class Vendor
    {
        public static  tbl_VendorDetails Add(tbl_VendorDetails vendor)
        {
            try
            {
                string OTPmsg = "";
                using(DB db = new DB())
                {
                    vendor.RegistrationDate = DateTime.Now;
                    vendor.MobileOTP = OtherFunction.GenerateOTP();
                    OTPmsg = "Nagar Palika Parishad Mathura!!  Your Mobile Verification Code is " + vendor.MobileOTP;

                    db.tblVendorDetails.Add(vendor);
                    db.SaveChanges();
                }

                
                 SMSService.Send(OTPmsg, vendor.MobileNumber);
            }
            catch (Exception ex) { throw ex; };
            return vendor;
        }

        public static tbl_VendorDetails Update(tbl_VendorDetails vendor)
        {
            try
            {
                using (DB db = new DB())
                {
                    tbl_VendorDetails vtable = db.tblVendorDetails.FirstOrDefault(x => x.userID == vendor.userID);
                    if (vtable.MobileNumber != vendor.MobileNumber)
                    {

                        vtable.CompanyName = vendor.CompanyName;
                        vtable.RegistrationNo = vendor.RegistrationNo;
                        vtable.RegisteredAddress = vendor.RegisteredAddress;
                        vtable.NameOfPartners = vendor.NameOfPartners;
                        vtable.City = vendor.City;
                        vtable.State = vendor.State;
                        vtable.PostalCode = vendor.PostalCode;
                        vtable.Country = vendor.Country;
                        vtable.EstablishmentYear = vendor.EstablishmentYear;
                        vtable.NatureOfBusiness = vendor.NatureOfBusiness;
                        vtable.LegalStatus = vendor.LegalStatus;
                        vtable.CompanyCategory = vendor.CompanyCategory;
                        vtable.Title = vendor.Title;
                        vtable.ContactName = vendor.ContactName;
                        vtable.DOB = vendor.DOB;
                        vtable.ContactEmail = vendor.ContactEmail;
                        vtable.Designation = vendor.Designation;
                        vtable.CountryCode = vendor.CountryCode;
                        vtable.MobileNumber = vendor.MobileNumber;
                        vtable.ISD_STDCode = vendor.ISD_STDCode;
                        vtable.PhoneNumber = vendor.PhoneNumber;

                        vtable.MobileOTP = OtherFunction.GenerateOTP();
                        vtable.MobileConfirmationStatus = 0;
                        vtable.BidderPreRegisteredWith = vendor.BidderPreRegisteredWith;
                        vtable.OrganisationType = vendor.OrganisationType;
                        vtable.UdyogAadharNumber = vendor.UdyogAadharNumber;
                        vtable.BidderRegisteredType = vendor.BidderRegisteredType;
                        vtable.Category = vendor.Category;
                        db.SaveChanges();

                        SMSService.Send(vendor.MobileOTP, vendor.MobileNumber);
                    }
                    else
                    {
                        vtable.CompanyName = vendor.CompanyName;
                        vtable.RegistrationNo = vendor.RegistrationNo;
                        vtable.RegisteredAddress = vendor.RegisteredAddress;
                        vtable.NameOfPartners = vendor.NameOfPartners;
                        vtable.City = vendor.City;
                        vtable.State = vendor.State;
                        vtable.PostalCode = vendor.PostalCode;
                        vtable.Country = vendor.Country;
                        vtable.EstablishmentYear = vendor.EstablishmentYear;
                        vtable.NatureOfBusiness = vendor.NatureOfBusiness;
                        vtable.LegalStatus = vendor.LegalStatus;
                        vtable.CompanyCategory = vendor.CompanyCategory;
                        vtable.Title = vendor.Title;
                        vtable.ContactName = vendor.ContactName;
                        vtable.DOB = vendor.DOB;
                        vtable.ContactEmail = vendor.ContactEmail;
                        vtable.Designation = vendor.Designation;
                        vtable.CountryCode = vendor.CountryCode;
                        vtable.MobileNumber = vendor.MobileNumber;
                        vtable.ISD_STDCode = vendor.ISD_STDCode;
                        vtable.PhoneNumber = vendor.PhoneNumber;
                        vtable.BidderPreRegisteredWith = vendor.BidderPreRegisteredWith;
                        vtable.OrganisationType = vendor.OrganisationType;
                        vtable.UdyogAadharNumber = vendor.UdyogAadharNumber;
                        vtable.BidderRegisteredType = vendor.BidderRegisteredType;
                        vtable.Category = vendor.Category;
                        db.SaveChanges();
                    }
                    return vtable;
                }

               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool VerifyMobile(string mobilenumber, string otp)
        {
            bool b = false;
            using(DB db = new DB())
            {
                tbl_VendorDetails v = db.tblVendorDetails.FirstOrDefault(x => x.MobileNumber == mobilenumber && x.MobileOTP==otp);
                if(v!=null)
                {
                    v.MobileConfirmationStatus = 1;
                    db.SaveChanges();
                    b = true;
                }

            }
            return b;
        }

        public static tbl_VendorDetails GetVendor(int userID)
        {
            tbl_VendorDetails tb = new tbl_VendorDetails();
            using (DB db = new DB())
            {
                tb = db.tblVendorDetails.FirstOrDefault(x => x.userID == userID);
            }
            return tb;
        }

        public static List<tbl_VendorDetails> GetVendorNotActivated()
        {
            using (DB db = new DB())
            {
                List<tbl_VendorDetails> tb = db.tblVendorDetails.Where(x => x.ActiveStatus == 0).ToList();
                return tb;
            }
            
        }

        public static bool VendorApproveFunc(int ID)
        {
            try { 
            using(DB db = new DB())
            {
                bool v = db.tblVendorDetails.Any(x => x.userID == ID);
                if(v == true)
                {
                    tbl_VendorDetails tblvendor = db.tblVendorDetails.FirstOrDefault(x => x.userID == ID);
                    tblvendor.ActivationDate = DateTime.Now;
                    tblvendor.ActiveStatus = 1;
                    db.SaveChanges();
                }
                return v;
            }
            }
            catch (Exception ex) { throw ex; }
        }

        public static string REOTP(string mobile) {

            try
            {
                string msg = "";
                string OTPmsg = "";
                using (DB db = new DB())
                {
                    int num = db.tblVendorDetails.Count(x => x.MobileNumber == mobile);

                    if (num != 0)
                    {
                        tbl_VendorDetails tblvd = db.tblVendorDetails.FirstOrDefault(x => x.MobileNumber == mobile);
                        tblvd.MobileOTP = OtherFunction.GenerateOTP();
                        OTPmsg = "Nagar Palika Parishad Mathura!!  Your Mobile Verification Code is " + tblvd.MobileOTP;

                        SMSService.Send(OTPmsg, tblvd.MobileNumber);
                        msg = "OTP has been sent to your registered mobile number";
                        db.SaveChanges();
                    }
                    else
                    {
                        msg = "Your Mobile Number is not Registered";
                    }
                }
                return msg;
            }
            catch (Exception ex) { throw ex; }
        }


        public static string REOTPforEmail(string email)
        {

            try
            {
                string msg = "";
                string MsgBody = "";
                string MsgSubject = "";
                using (DB db = new DB())
                {
                    int num = db.tblVendorDetails.Count(x => x.ContactEmail == email);

                    if (num != 0)
                    {
                        tbl_VendorDetails tblvd = db.tblVendorDetails.FirstOrDefault(x => x.ContactEmail == email);
                        tblvd.CEmailOTP = OtherFunction.GenerateOTP();

                        MsgBody = "Nagar Palika Parishad Mathura!!  Your Email Verification Code is " + tblvd.CEmailOTP;
                        MsgSubject = "Nagar Palika Parishad | Email Verification";
                        
                        MailHelper.sendmail(email, MsgSubject, MsgBody);
                  
                        msg = "OTP has been sent to your registered email";
                        db.SaveChanges();
                    }
                    else
                    {
                        msg = "Your Email is not Registered";
                    }
                }
                return msg;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string REOTPforLoginEmail(string email)
        {

            try
            {
                string msg = "";
                string MsgBody = "";
                string MsgSubject = "";
                using (DB db = new DB())
                {
                    int num = db.tblVendorDetails.Count(x => x.Email == email);

                    if (num != 0)
                    {
                        tbl_VendorDetails tblvd = db.tblVendorDetails.FirstOrDefault(x => x.Email == email);
                        tblvd.EmailOTP = OtherFunction.GenerateOTP();

                        MsgBody = "Nagar Palika Parishad Mathura!!  Your Email Verification Code is " + tblvd.EmailOTP;
                        MsgSubject = "Nagar Palika Parishad | Email Verification";

                        MailHelper.sendmail(email, MsgSubject, MsgBody);

                        msg = "OTP has been sent to your registered email";
                        db.SaveChanges();
                    }
                    else
                    {
                        msg = "Your Email is not Registered";
                    }
                }
                return msg;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string VerifyContactEmail(string email, string OTP) {
            try {
                string   msg = "";
                using(DB db = new DB())
                {
                    tbl_VendorDetails vtable = db.tblVendorDetails.FirstOrDefault(x => x.ContactEmail == email);
                    if(vtable.CEmailOTP == OTP)
                    {
                        vtable.CEmailConfirmationStatus = 1;
                        db.SaveChanges();
                        msg = "Email is verified successfully";
                    }
                    else
                    {
                        msg = "Email is not Verified";
                    }

                }
                return msg;
            
            }
            catch (Exception ex) { throw ex; }
        
        }

        public static string VerifyEmail(string email, string OTP)
        {
            try
            {
                string msg = "";
                using (DB db = new DB())
                {
                    tbl_VendorDetails vtable = db.tblVendorDetails.FirstOrDefault(x => x.Email == email);
                    if (vtable.EmailOTP == OTP)
                    {
                        vtable.EmailConfirmationStatus = 1;
                        db.SaveChanges();
                        msg = "Email is verified successfully";
                    }
                    else
                    {
                        msg = "Email is not Verified";
                    }

                }
                return msg;

            }
            catch (Exception ex) { throw ex; }

        }
    }
}
