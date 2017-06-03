using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.Tempmodel;
using WebMatrix.WebData;
using eTenderService.DataModel;
using eTenderService.DataAccess;

namespace eTender.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/

        public ActionResult Index()
        {
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
                            return Json(new { msg = "You are not registered as Admin" });
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
