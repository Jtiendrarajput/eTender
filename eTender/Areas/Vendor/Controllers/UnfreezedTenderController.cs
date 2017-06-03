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
    public class UnfreezedTenderController : Controller
    {
       
        // GET: /Vendor/UnfreezedTender/

        public ActionResult Index()
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            return View();
        }

        public ActionResult UnfreezedTender()
        {
            return new JsonResult { Data = UnfreezedTenders.Unfreezedtenders(WebSecurity.CurrentUserId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
