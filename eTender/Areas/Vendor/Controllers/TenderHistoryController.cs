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
    public class TenderHistoryController : Controller
    {
        //
        // GET: /Vendor/TenderHistory/

        public ActionResult Index()
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            return View();
        }

        public ActionResult TenderHistoryDetail()
        {
            return new JsonResult { Data = TenderHistory.TenderHistoryDetail(WebSecurity.CurrentUserId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
