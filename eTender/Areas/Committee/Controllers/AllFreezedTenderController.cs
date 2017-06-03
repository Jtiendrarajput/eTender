using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eTenderService.DataAccess;
using eTenderService.Tempmodel;
using WebMatrix.WebData;


namespace eTender.Areas.Committee.Controllers
{
    [Authorize(Roles = "CommitteeAdmin")]
    public class AllFreezedTenderController : Controller
    {
        //
        // GET: /Committee/AllFreezedTender/

        public ActionResult Index()
        {
            ViewBag.user = ADashUserProfile.GetUserDetails(WebSecurity.CurrentUserId);
           // ViewBag.user = ADashUserProfile.GetUserDetails(4);
            return View();
        }

        //[All_FreezedTechnicalTenders]//
        //Tendors list after freeze date for processing technical bid

        public ActionResult FreezedTendorsForTechnical()
        {
            return new JsonResult { Data = All_FreezedTenders.AllFreezedTenders(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Tendor List after Technical Bid Approved

        public ActionResult AllTechnicallyApprovedTenders()
        {
            return new JsonResult { Data = All_FreezedTenders.TechnicallyApprovedTenders(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Bidders Details of Tendor waiting for Financial Process

        public ActionResult TendersBiddersList(int TenderID)
        {
            return new JsonResult { Data = All_FreezedTenders.TendersBidders(TenderID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Bidders Details of Tendor waiting for Tachnical Process

        public ActionResult TendersTechnicalBidders(int TenderID)
        {
            return new JsonResult { Data = All_FreezedTenders.TendersTechnicalBidders(TenderID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Return List of Committee Members based on Tendor ID

        public ActionResult TendersCommMember(int TenderID)
        {
            return new JsonResult { Data = All_FreezedTenders.CommMembersTender(TenderID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SendOTPtoAllCM(int ActionStatus, string Status, int TenderId, int BiddingId)
        {
            return new JsonResult { Data = All_FreezedTenders.SendOTP(ActionStatus, Status, TenderId, BiddingId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
       
        [HttpPost]
        public JsonResult OTPVerifyForAll(Temp_OTPVerifyCommitteeMember TempOTPLst)
        {
            return new JsonResult { Data = All_FreezedTenders.VerifyMobile(TempOTPLst), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult OTPVerifiedSuccessfully(int ActionStatus, string Status, int TenderId, int BiddingId)
        {
            return new JsonResult { Data = All_FreezedTenders.OTPVerifiedsuccess(ActionStatus, Status, TenderId, BiddingId), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
