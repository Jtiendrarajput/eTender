using eTender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace eTender.Areas.Vendor.Controllers
{

    [Authorize(Roles = "Vendor")]
    public class VDashController : Controller
    {
        //
        // GET: /Vendor/VDash/

        public ActionResult Index()
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            return View();
        }

        public ActionResult ChangePassword()
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(_ChangePassword cp)
        {
            string msg = "";
            try
            {
                if (cp.newpassword == cp.confirmPassword)
                {

                    if (WebSecurity.ChangePassword(User.Identity.Name, cp.oldpassword, cp.newpassword))
                    {
                        msg = "success";
                    }
                    else
                        msg = "Invalid old password";
                }
                else
                    msg = "New Password and Confirm Passwod is not match!";
            }
            catch (Exception ex) { msg = ex.Message; }

            return Json(new { msg=msg });
        }
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "VDash");
        }
    }
}
