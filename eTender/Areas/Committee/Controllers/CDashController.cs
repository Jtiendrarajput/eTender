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
    public class CDashController : Controller
    {
        //
        // GET: /Committee/CDash/

        public ActionResult Index()
        {           
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(4);
            return View();
        }

    }
}
