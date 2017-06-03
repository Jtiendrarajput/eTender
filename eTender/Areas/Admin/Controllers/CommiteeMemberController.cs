using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using eTenderService.DataModel;
using WebMatrix.WebData;
namespace eTender.Areas.Admin.Controllers
{
      [Authorize(Roles = "TenderAdmin")]
    public class CommiteeMemberController : Controller
    {
        //
        // GET: /Admin/CommiteeMember/
          
        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(1);
            return View();
        }

        [HttpPost]
        public JsonResult ADDCommiteeMember(tbl_CommiteeMember Cmember)
        {

            try
            {

                return Json(new { ID = Committe.Add(Cmember), msg = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult UpdateCommiteeMember(tbl_CommiteeMember Cmember)
        {
            try
            {
                return Json(new { ID = Committe.update(Cmember), msg = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult DeleteCommiteeMember(int CMemberID)
        {
            try
            {
                return Json(new { ID = Committe.delete(CMemberID), msg = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult VerifyMobile(tbl_CommiteeMember cmember)
        {
            try
            {
                if (Committe.VerifyMobile(cmember))
                    return Json(new { msg = "success" });
                else
                    return Json(new { msg = "Invalid" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult SentOTP(tbl_CommiteeMember cmember)
        {
            try
            {
                Committe.sentOTP(cmember);
                    return Json(new { msg = "success" });
                
            }
            catch (Exception ex) {
                return Json(new { msg = ex.Message });
            }
        }
        public ActionResult GetAllCommiteeMember()
        {
            return new JsonResult { Data = Committe.AllCommitte(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
