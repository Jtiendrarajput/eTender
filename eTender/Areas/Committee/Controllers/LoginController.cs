using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using WebMatrix.WebData;
using eTenderService.Tempmodel;
using eTenderService.DataAccess;
using eTenderService.DataModel;

namespace eTender.Areas.Committee.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Committee/Login/

        public ActionResult Index()
        {
            //ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
           // ViewBag.user = ADashUserProfile.GetUserDetails(4);
            return View();
        }

        [HttpPost]
        public JsonResult Login(_login login)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(login.UserName, login.password))
                {
                    using (DB db = new DB())
                    {

                        bool chk = db.tblUserProfiles.Any(x => x.Name == login.UserName);

                        if (chk == true)
                        {
                            tblUserProfile up = db.tblUserProfiles.FirstOrDefault(x => x.Name == login.UserName);
                            up.LastLoginDatenTime = up.currentLogindate;
                            up.LastLoginIP = up.currentIP;
                            up.currentIP = this.Request.ServerVariables["REMOTE_ADDR"];
                            up.currentLogindate = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            return Json(new { msg = "You are not registered as Operator" });
                        }

                    }

                }
                else
                {
                    return Json(new { msg = "Invalid Username And Password" });
                }
            }

            return Json(new { msg = "You have login successfully" });
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Login");
        }
    }
}
