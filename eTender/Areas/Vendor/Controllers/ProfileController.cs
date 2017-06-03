using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using eTenderService.Tempmodel;
using eTenderService.DataModel;
using WebMatrix.WebData;


namespace eTender.Areas.Vendor.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Vendor/Profile/

        public ActionResult Index()
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            return View();
        }


        public ActionResult VendorDetails()
        {
            return new JsonResult { Data = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult UpdateVendor(tbl_VendorDetails vendor)
        {
            try
            {
                return Json(new { ID = eTenderService.DataAccess.Vendor.Update(vendor), msg = "success" });
            }
            catch (Exception ex) { throw ex; }

        }

       
        public JsonResult SendCEmailOTP(string email,int emailtype)
        {
            try {
                if (emailtype == 0)
                {
                    return new JsonResult { Data = eTenderService.DataAccess.Vendor.REOTPforEmail(email), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = eTenderService.DataAccess.Vendor.REOTPforLoginEmail(email), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
               // return Json(new { ID = eTenderService.DataAccess.Vendor.REOTPforEmail(email), JsonRequestBehavior = JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex) { throw ex; }
        }


        [HttpPost]
        public JsonResult VerifyEmail(int emailtype, string email,string OTP)
        {
            try
            {
                if (emailtype == 0)
                {
                    //verify contact email
                    return Json(new { ID = eTenderService.DataAccess.Vendor.VerifyContactEmail(email, OTP), msg = "success" });
                }
                else
                {
                    //verify login email
                    return Json(new { ID = eTenderService.DataAccess.Vendor.VerifyEmail(email, OTP), msg = "success" });
                }
            }
            catch (Exception ex) { throw ex; }

        }

    }
}
