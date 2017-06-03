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
    public class DepartmentController : Controller
    {
        //
        // GET: /Admin/Department/
            
        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
            //ViewBag.user = ADashUserProfile.GetUserDetails(1);
            return View();
        }


        [HttpPost]
        public JsonResult ADDDept(tbl_Department dept) {

            try
            {

                return Json(new { ID = DepartmentMaster.Add(dept), msg = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }

        }


        [HttpPost]
        public JsonResult UpdateDept(tbl_Department dept)
        {
            try
            {

                return Json(new { ID = DepartmentMaster.Update(dept), msg = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
        
        }


        [HttpPost]
        public JsonResult DeleteDept(int DeptID)
        {
            try
            {

                return Json(new { ID = DepartmentMaster.Delete(DeptID), msg = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
         
            
        }


        public ActionResult GetAllDepartment()
        {

            return new JsonResult { Data = DepartmentMaster.AllDepartment(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
