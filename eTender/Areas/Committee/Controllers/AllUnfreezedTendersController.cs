using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using eTenderService.Tempmodel;
using WebMatrix.WebData;

namespace eTender.Areas.Committee.Controllers
{
    [Authorize(Roles = "CommitteeAdmin")]
    public class AllUnfreezedTendersController : Controller
    {
        //
        // GET: /Committee/UnfreezedTenders/

        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
           // ViewBag.user = ADashUserProfile.GetUserDetails(4);
            return View();
        }

        public ActionResult UnfreezedTenderslist()
        {
            return new JsonResult { Data = All_UnfreezedTenders.AllUnfreezedTender(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult AllUnfreezedTendersBidders(int TenderId)
        {
            return new JsonResult { Data = All_UnfreezedTenders.UnfreezedTendersBidders(TenderId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
