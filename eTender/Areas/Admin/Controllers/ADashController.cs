using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using WebMatrix.WebData;
namespace eTender.Areas.Admin.Controllers
{
     [Authorize(Roles = "TenderAdmin")]
    public class ADashController : Controller
    {
        //
        // GET: /Admin/ADash/     /
        
        
       
        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(1);
            return View();
        }

        }
}
