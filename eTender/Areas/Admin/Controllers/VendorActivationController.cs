using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using eTenderService.Tempmodel;
using WebMatrix.WebData;

namespace eTender.Areas.Admin.Controllers
{
       [Authorize(Roles = "TenderAdmin")]
    public class VendorActivationController : Controller
    {
        //
        // GET: /Admin/VendorActivation/
         
        public ActionResult Index()
        {
           // ViewBag.user = ADashUserProfile.GetUserDetails(1);
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            return View();
        }

        public ActionResult GetAllNonActivatedVendor()
        {

            return new JsonResult { Data = eTenderService.DataAccess.Vendor.GetVendorNotActivated(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public JsonResult ApproveVendor(int ID)
        {
            try
            {
                return Json(new { Data = eTenderService.DataAccess.Vendor.VendorApproveFunc(ID), msg = "success" });
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
