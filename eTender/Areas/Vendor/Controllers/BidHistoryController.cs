using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using WebMatrix.WebData;

namespace eTender.Areas.Vendor.Controllers
{
     [Authorize(Roles = "Vendor")]
    public class BidHistoryController : Controller
    {
        //
        // GET: /Vendor/BidHistory/

        public ActionResult Index()
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            return View();
        }

        public ActionResult AllBidHistoy()
        {
            return new JsonResult { Data = BidHistory.AllBidHistoy(WebSecurity.CurrentUserId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult BidHistoryDetail(int TenderID)
        {
            return new JsonResult { Data = BidHistory.BidHistoyDetail(TenderID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult TendorMemberList(int TenderID)
        {
            return new JsonResult { Data = BidHistory.TenderMemberslist(TenderID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

       
    }
}
