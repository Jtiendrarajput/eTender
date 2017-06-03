using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using WebMatrix.WebData;


namespace eTender.Areas.Vendor.Controllers
{

     [Authorize(Roles = "Vendor")]
    public class ViewTenderController : Controller
    {
        //
        // GET: /ViewTender/

        public ActionResult Index()
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            return View();
        }

        public ActionResult AllActiveTendors()
        {
            return new JsonResult { Data = Vendor_sTendor.All_ActiveTendors(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };  
        }
        [HttpPost]
        public int CheckBidding(int ID)
        {
            return Vendor_sTendor.CheckBiddStatus(WebSecurity.CurrentUserId, ID);
        }

    }
}
