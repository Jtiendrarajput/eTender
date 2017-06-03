using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataModel;
using eTenderService.DataAccess;
using WebMatrix.WebData;

namespace eTender.Areas.Admin.Controllers
{
     [Authorize(Roles = "TenderAdmin")]
    public class CategoryController : Controller
    {
        //
        // GET: /Admin/Category/
           
        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(1);
            return View();
        }


        [HttpPost]
        public JsonResult ADDCategory(tbl_Category cat)
        {
            try
            {
                return Json(new { ID = CategoryMaster.Add(cat), msg = "success" });
            }
            catch (Exception ex) { throw ex; }
        }


        [HttpPost]
        public JsonResult UpdateCategory(tbl_Category cat)
        {
            try
            {
                return Json(new { ID = CategoryMaster.Update(cat), msg = "success" });
            }
            catch (Exception ex) { throw ex; }
       
        }


        [HttpPost]
        public JsonResult DeleteCategory(int CatID)
        {
            try
            {
                return Json(new { ID = CategoryMaster.Delete(CatID), msg = "success" });
            }
            catch (Exception ex) { throw ex; }

        }


        public ActionResult GetAllCategory()
        {

            return new JsonResult { Data = CategoryMaster.AllCategory(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          
        }
    }
}
