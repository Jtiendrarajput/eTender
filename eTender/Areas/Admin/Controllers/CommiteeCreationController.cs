using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using eTenderService.DataModel;
using eTenderService.Tempmodel;
using WebMatrix.WebData;
namespace eTender.Areas.Admin.Controllers
{
     [Authorize(Roles = "TenderAdmin")]
    public class CommiteeCreationController : Controller
    {
        //
        // GET: /Admin/CommiteeCreation/
           
        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(1);
            return View();
        }

        [HttpPost]
        public JsonResult ADDCommitee(Temp_CommitteforTender CommiteeAdd)
        {
            try
            {
                return Json(new { ID = CreateCommitte.Add(CommiteeAdd), msg = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex) { throw ex; }
           
        }

        public ActionResult GetAllTendorForCommitteeCreation() {
            try
            {
                return new JsonResult { Data = CreateCommitte.TenderNotInComMember(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex) { throw ex; }
        
        }

        //public ActionResult GetFormat()
        //{

        //    Temp_CommitteforTender variable = new Temp_CommitteforTender();
        //    variable.TenderID = 1;
        //    int a = 1,b=1;
        //    List<int> newlist = new List<int>();
        //    newlist.Add(a);
        //    newlist.Add(b);
        //    variable.ComMemeberID = newlist;
            
        //    return new JsonResult { Data = variable, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        
    }
}
