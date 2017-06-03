using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataModel;
using eTenderService.DataAccess;
using eTenderService.Tempmodel;
using WebMatrix.WebData;
using eTender.EncryptDecrypt;
using eTenderService.Extension;

namespace eTender.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Vendor/

        public ActionResult Index()
        {
            //WebSecurity.CreateUserAndAccount("Operator", "Operator@123");
           // WebSecurity.CreateUserAndAccount("Anjali", "Admin@123");
           // WebSecurity.CreateUserAndAccount("Shilpa", "Admin@123");
            return View();
        }
        [HttpPost]
        public JsonResult Signup(tbl_VendorDetails vendor)
        {
            string msg = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (WebSecurity.GetUserId(vendor.Email) <= 1)
                    {
                        WebSecurity.CreateUserAndAccount(vendor.Email, vendor.Password);
                        vendor.userID = WebSecurity.GetUserId(vendor.Email);
                        vendor.RegistrationDate = DateTime.Now;
                       
                        vendor = Vendor.Add(vendor);
                        msg = "success";
                        OtherFunction.SetRole(vendor.userID, 4);
                        return Json(new { MobileNo = vendor.MobileNumber, msg = msg });
                    }
                    else
                        return Json(new { msg = "this user already exist" });
                }
                else
                {
                    msg = "Please Fill All Feilds";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(new { msg = msg });
        }

        [HttpPost]
        public JsonResult VerifyMobile(string MobileNumber, string OTP)
        {
            try
            {
                if (Vendor.VerifyMobile(MobileNumber, OTP))
                    return Json(new { msg = "success" });
                else
                    return Json(new { msg = "Invalid OTP" });

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Login(_login login)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(login.UserName, login.password))
                {
                    using (DB db = new DB())
                    {

                        tbl_VendorDetails vd = db.tblVendorDetails.FirstOrDefault(x => x.Email == login.UserName);//.DecryptEntity< tbl_VendorDetails>();
                        vd.LastLoginDatenTime = vd.currentLogindate;
                        vd.LastLoginIP = vd.currentIP;
                        vd.currentIP = this.Request.ServerVariables["REMOTE_ADDR"];
                        vd.currentLogindate = DateTime.Now;
                        db.SaveChanges();

                      
                    }
                    if (OtherFunction.CheckForMobVerifyUser(login.UserName))
                    {
                        if (OtherFunction.CheckForVerifyUser(login.UserName))
                        {
                            switch (OtherFunction.GetRoleByUser(login.UserName))
                            {
                                case "Vendor":
                                    return Json(new { msg = "success", url = Url.Action("Index", "Vendor/VDash") });
                                case "":
                                    return Json(new { msg = "success", url = Url.Action("Index", "Vendor/VDash") });

                            }
                        }
                        else
                            return Json(new { msg = "Account is not Activated" });
                    }
                    else
                    {
                        return Json(new { msg = "Mobile Number is not Verified" });
                    }
                }
                return Json(new { msg = "Invalid Username And Password" });
            }

            return Json(new { msg = "success" });
        }


        //[HttpPost]
        public JsonResult RegenerateOTP(string mobile)
        {
            string msg = Vendor.REOTP(mobile);
            return Json(new { msg = msg });

        }
    }
}
