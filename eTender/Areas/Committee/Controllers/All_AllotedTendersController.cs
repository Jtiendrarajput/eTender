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
    public class All_AllotedTendersController : Controller
    {
        //
        // GET: /Committee/All_AllotedTenders/

        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(4);
            return View();
        }

        public ActionResult AllotedTenders()
        {
            return new JsonResult { Data = All_AllotedTenders.AllotedTendres(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
