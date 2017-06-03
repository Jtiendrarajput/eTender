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
    public class CreateTendorController : Controller
    {
        //
        // GET: /Admin/CreateTendor/
            
        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
           // ViewBag.user = ADashUserProfile.GetUserDetails(1);
            return View();
        }

        [HttpPost]
        public JsonResult ADDTendor(tbl_TenderDetails Tendor)
        {
            try
            {
                //Tendor.UserID = WebSecurity.CurrentUserId;
                Tendor.UserID = 1;
                Tendor.PublishDate = DateTime.Now;
                return Json(new { ID = CreateTenders.Add(Tendor), msg = "success" });
            }
            catch (Exception ex) { throw ex; }
        }


        [HttpPost]
        public JsonResult UpdateTendor(tbl_TenderDetails Tendor)
        {

            return Json(new { ID = CreateTenders.Update(Tendor), msg = "success" });
        }


        public ActionResult GetAccesstoUpdate(int TendorID) {

            return Json(new { Status = CreateTenders.AllowUpdate(TendorID), msg = "success" });
        }


        [HttpPost]
        public JsonResult DeleteTendor(int TendorID)
        {

            return Json(new { ID = CreateTenders.delete(TendorID), msg = "success" });
        }


        public ActionResult GetAllTendor()
        {
            return new JsonResult { Data = CreateTenders.AllTenders(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult UploadFiles(string ID)
        {
            if (Request.Files.Count > 0)
            {
                string filename = string.Empty;
                string filename2 = string.Empty;
                string filename3 = string.Empty;
                try
                {
                    
                    string path = Server.MapPath("~/TenderDocument/");

                    var keysname = Request.Files.AllKeys;
                    using (DB db = new DB())
                        {
                            int id = int.Parse(ID);
                            tbl_TenderDetails tendortbl = db.tblTenderDetails.FirstOrDefault(x => x.ID == id);
                                 for (int i = 0; i < keysname.Length; i++)
                                    {

                                        if (keysname[i] == "tendornotice")
                                        {
                                            var tendornoticefile = Request.Files["tendornotice"];
                                            string[] s = tendornoticefile.FileName.Split('.');
                                            filename = "TenderNotice" + ID + "." + s[s.Length - 1];
                                            tendornoticefile.SaveAs(path + filename);
                                            tendortbl.TenderNoticePath = filename;
                                        }

                                        else if (keysname[i] == "tendordoc")
                                        {
                                            var tendordocfile = Request.Files["tendordoc"];
                                            string[] s2 = tendordocfile.FileName.Split('.');
                                            filename2 = "TenderDocument" + ID + "." + s2[s2.Length - 1];
                                            tendordocfile.SaveAs(path + filename2);
                                            tendortbl.TenderDocPath = filename2;
                                        }
                                        else if (keysname[i] == "boqfile")
                                        {

                                            var tendorboqfile = Request.Files["boqfile"];
                                            string[] s3 = tendorboqfile.FileName.Split('.');
                                            filename3 = "TenderBOQFile" + ID + "." + s3[s3.Length - 1];
                                            tendorboqfile.SaveAs(path + filename3);
                                            tendortbl.BOQFilePath = filename3;
                                        }

                    }

                                 if (tendortbl.TenderID == null || tendortbl.TenderID == " ")
                                 {
                                     tendortbl.TenderID = CreateTenders.GetTenderID(id);
                                 }
                                db.SaveChanges();
                        }
                    return Json(new { msg = "success" });
                }
                catch (Exception ex) { return Json(new { msg = ex.Message }); }

            }
            else
                return Json(new { msg = "Please Upload a file" });
        }

        public JsonResult UploadUpdatedFiles(string ID)
        {
            if (Request.Files.Count > 0)
            {
                string filename = string.Empty;
                string filename2 = string.Empty;
                string filename3 = string.Empty;
                try
                {

                    string path = Server.MapPath("~/TenderDocument/");

                    var keysname = Request.Files.AllKeys;
                    using (DB db = new DB())
                    {
                        int id = int.Parse(ID);
                        tbl_TenderDetails tendortbl = db.tblTenderDetails.FirstOrDefault(x => x.ID == id);
                        for (int i = 0; i < keysname.Length; i++)
                        {

                            if (keysname[i] == "tendornotice")
                            {
                                var tendornoticefile = Request.Files["tendornotice"];
                                string[] s = tendornoticefile.FileName.Split('.');
                                filename = "TenderNotice" + ID + "." + s[s.Length - 1];
                                tendornoticefile.SaveAs(path + filename);
                                tendortbl.TenderNoticePath = filename;
                            }

                            else if (keysname[i] == "tendordoc")
                            {
                                var tendordocfile = Request.Files["tendordoc"];
                                string[] s2 = tendordocfile.FileName.Split('.');
                                filename2 = "TenderDocument" + ID + "." + s2[s2.Length - 1];
                                tendordocfile.SaveAs(path + filename2);
                                tendortbl.TenderDocPath = filename2;
                            }
                            else if (keysname[i] == "boqfile")
                            {

                                var tendorboqfile = Request.Files["boqfile"];
                                string[] s3 = tendorboqfile.FileName.Split('.');
                                filename3 = "TenderBOQFile" + ID + "." + s3[s3.Length - 1];
                                tendorboqfile.SaveAs(path + filename3);
                                tendortbl.BOQFilePath = filename3;
                            }

                        }

                        if (tendortbl.TenderID == null || tendortbl.TenderID == " ")
                        {
                            tendortbl.TenderID = CreateTenders.GetTenderID(id);
                        }
                        db.SaveChanges();
                    }
                    return Json(new { msg = "success" });
                }
                catch (Exception ex) { return Json(new { msg = ex.Message }); }

            }
            else
                return Json(new { msg = "" });
        }
    }
}
