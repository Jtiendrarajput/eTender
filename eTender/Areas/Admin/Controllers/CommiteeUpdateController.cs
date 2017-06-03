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
    public class CommiteeUpdateController : Controller
    {
        //
        // GET: /Admin/CommiteeUpdate/
          
        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(1);
            return View();
        }



        public ActionResult GetAllTendor()
        {
            return new JsonResult { Data = CreateCommitte.detail(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetAllCommiteeMemberOfTendor(int TendorID)
        {
            return new JsonResult { Data = CreateCommitte.TenderComMebers(TendorID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult UpdateCommitee(Temp_CommitteforTender Commitee)
        {
            return new JsonResult { Data = CreateCommitte.Update(Commitee), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }




        [HttpPost]
        public JsonResult DeleteCommitee(int TendorID)
        {
            try
            {
                return Json(new { ID = CreateCommitte.delete(TendorID), msg = "success" });
            }
            catch (Exception ex) { throw ex; }

        }
        
    }
}
