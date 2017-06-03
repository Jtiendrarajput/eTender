using eTenderService.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using eTenderService.DataAccess;

using System.IO;
namespace eTender.Areas.Vendor.Controllers
{
     [Authorize(Roles = "Vendor")]
    public class StartTenderBidController : Controller
    {
        //
        // GET: /Vendor/StartTenderBid/
        static string TenderID = "";
        public ActionResult Index(string TID)
        {
            ViewBag.user = eTenderService.DataAccess.Vendor.GetVendor(WebSecurity.CurrentUserId);
            ViewBag.TenderID = TID;
            return View();
        }





        public JsonResult StartBidding(string tenderID)
        {
            return new JsonResult { Data = StartBid.StartBidding(tenderID, WebSecurity.CurrentUserId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
         

        [HttpPost]
        public JsonResult BidPayment(tbl_Bid bid)
        {
            return new JsonResult { Data = StartBid.UpdateBid(bid), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult FreezeBid(tbl_Bid bid)
        {
            bid.LastActivityIP = this.Request.UserHostAddress;
            return new JsonResult { Data = StartBid.FreezeBid(bid), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UploadTechDocument(string ID)
        {
            if (Request.Files.Count > 0)
            {
                string filename = string.Empty;
                try
                {
                    var file = Request.Files["tech"];
                    var filesize = file.ContentLength;
                    if(((filesize/1024)/1024) > 50)
                    {
                        return Json(new { msg = "File is not Uploaded!!!  Upload file of size less than 50 MB  " });
                    }

                    string[] s = file.FileName.Split('.');
                    filename = "TechDocument" + ID + "." + s[s.Length - 1];
                    string path = Server.MapPath("~/TechDocument/") + filename;
                    file.SaveAs(path);
                    using (DB db = new DB())
                    {
                        int id = int.Parse(ID);
                        tbl_Bid bd = db.tbl_Bid.FirstOrDefault(x => x.ID == id);
                        bd.TechDoc = filename;
                        db.SaveChanges();
                    }
                    return Json(new { msg = "success" });
                }
                catch (Exception ex) { return Json(new { msg = ex.Message }); }
                
            }
            else
            return Json(new { msg = "Please Upload a file" });
        }
        
        public JsonResult UploadTechDocumentWithSign(eTender.Models._select s)
        {
            try
            {
              
                int id = int.Parse(s.val);
                byte[] bytes = Convert.FromBase64String(s.txt);
                string filename = "TechDocument" + id + ".pdf";
                string path = Server.MapPath("~/TechDocument/") + filename;

                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                using (DB db = new DB())
                {
                    
                    tbl_Bid bd = db.tbl_Bid.FirstOrDefault(x => x.ID == id);
                    bd.TechDoc = filename;
                    db.SaveChanges();
                }
                return new JsonResult { Data = new { msg = "success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { msg = ex.Message}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult UploadFinaDocument(string ID)
        {
            if (Request.Files.Count > 0)
            {
                string filename = string.Empty;
                try
                {
                    var file = Request.Files["final"];
                    var filesize = file.ContentLength;
                    if (((filesize / 1024) / 1024) > 5)
                    {
                        return Json(new { msg = "File is not Uploaded!!! Upload file of size less than 5 MB " });
                    }
                    string[] s = file.FileName.Split('.');
                    filename = "FinancialDocument" + ID + "." + s[s.Length - 1];
                    string path = Server.MapPath("~/FinancialDocument/") + filename;
                    file.SaveAs(path);
                    using (DB db = new DB())
                    {
                        int id = int.Parse(ID);
                        tbl_Bid bd = db.tbl_Bid.FirstOrDefault(x => x.ID == id);
                        bd.FinancialDoc = filename;
                        db.SaveChanges();
                    }
                    return Json(new { msg = "success" });
                }
                catch (Exception ex) { return Json(new { msg = ex.Message }); }

            }
            else
                return Json(new { msg = "Please Upload a file" });
        }

        //public JsonResult UploadTenderFeeDocument(string ID)
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        string filename = string.Empty;
        //        try
        //        {
        //            var file = Request.Files["Tenderfee"];
        //            string[] s = file.FileName.Split('.');
        //            filename = "TenderFeeDocument" + ID + "." + s[s.Length - 1];
        //            string path = Server.MapPath("~/TenderFeeDocument/") + filename;
        //            file.SaveAs(path);
        //            using (DB db = new DB())
        //            {
        //                int id = int.Parse(ID);
        //                tbl_Bid bd = db.tbl_Bid.FirstOrDefault(x => x.ID == id);
        //                bd.TenderDoc = filename;
        //                db.SaveChanges();
        //            }
        //            return Json(new { msg = "success" });
        //        }
        //        catch (Exception ex) { return Json(new { msg = ex.Message }); }

        //    }
        //    else
        //        return Json(new { msg = "Please Upload a file" });
        //}

        //public JsonResult UploadEMDFeeDocument(string ID)
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        string filename = string.Empty;
        //        try
        //        {
        //            var file = Request.Files["EMDfee"];
        //            string[] s = file.FileName.Split('.');
        //            filename = "EMDFeeDocument" + ID + "." + s[s.Length - 1];
        //            string path = Server.MapPath("~/EMDFeeDocument/") + filename;
        //            file.SaveAs(path);
        //            using (DB db = new DB())
        //            {
        //                int id = int.Parse(ID);
        //                tbl_Bid bd = db.tbl_Bid.FirstOrDefault(x => x.ID == id);
        //                bd.EMDDoc = filename;
        //                db.SaveChanges();
        //            }
        //            return Json(new { msg = "success" });
        //        }
        //        catch (Exception ex) { return Json(new { msg = ex.Message }); }

        //    }
        //    else
        //        return Json(new { msg = "Please Upload a file" });
        //}
    }
}
