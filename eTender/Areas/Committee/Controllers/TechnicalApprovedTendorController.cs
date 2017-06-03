using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using WebMatrix.WebData;

namespace eTender.Areas.Committee.Controllers
{
     [Authorize(Roles = "CommitteeAdmin")]
    public class TechnicalApprovedTendorController : Controller
    {
        //
        // GET: /Committee/TechnicalApprovedTendor/

        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(4);
            return View();
        }

        public ActionResult TechnicalApprovedTendor()
        {
            return new JsonResult { Data = All_FreezedTenders.TechnicallyApprovedTenders(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult TechnicalApprovedBidder(int TendorID)
        {
            return new JsonResult { Data = All_FreezedTenders.TendersBidders(TendorID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
