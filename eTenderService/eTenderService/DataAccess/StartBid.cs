using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;
using eTenderService.Tempmodel;

namespace eTenderService.DataAccess
{
    public class StartBid
    {
        public static Temp_TenderBidforStartBid StartBidding(string TenderID,int VenderID){
            try
            {
                Temp_TenderBidforStartBid tenderbid = new Temp_TenderBidforStartBid();
                using (DB db = new DB())
                {
                    tbl_TenderDetails tendor = db.tblTenderDetails.FirstOrDefault(x => x.TenderID == TenderID);
                    List<tbl_Bid> Bid = db.tbl_Bid.Where(x => x.VendorID == VenderID && x.TenderID == tendor.ID).ToList();
                    tbl_Bid BidTable = new tbl_Bid();
                    if (Bid.Count == 0)
                    {
                        BidTable.TenderID = tendor.ID;
                        BidTable.VendorID = VenderID;
                        BidTable.Status = 1;
                        BidTable.TotalAmount = 0;
                        BidTable.FreezeStatus = 0;
                        db.tbl_Bid.Add(BidTable);
                        db.SaveChanges();

                        tbl_BidStatus bidstatus = new tbl_BidStatus();
                        bidstatus.BidID = BidTable.ID;
                        bidstatus.BidType = "Technical";
                        bidstatus.SDate = DateTime.Now;
                        bidstatus.Status = 1;
                        db.tbl_BidStatus.Add(bidstatus);
                        db.SaveChanges();
                    }
                    else
                    {
                        BidTable.ID = Bid[0].ID;
                        BidTable.LastActivityDate = Bid[0].LastActivityDate;
                        BidTable.LastActivityIP = Bid[0].LastActivityIP;
                        BidTable.Status = Bid[0].Status;
                        BidTable.TechDoc = Bid[0].TechDoc;
                        BidTable.TechIssuer = Bid[0].TechIssuer;
                        BidTable.TechRefID = Bid[0].TechRefID;
                        BidTable.TechValidUpto = Bid[0].TechValidUpto;
                        BidTable.TenderDoc = Bid[0].TenderDoc;
                        BidTable.TenderID = Bid[0].TenderID;
                        BidTable.TotalAmount = Bid[0].TotalAmount;
                        BidTable.VendorID = Bid[0].VendorID;
                        BidTable.BidSubmitDate = Bid[0].BidSubmitDate;
                        BidTable.EMDDoc = Bid[0].EMDDoc;
                        BidTable.EMDIssuer = Bid[0].EMDIssuer;
                        BidTable.EMDRefId = Bid[0].EMDRefId;
                        BidTable.EMDValidUpto = Bid[0].EMDValidUpto;
                        BidTable.FinancialDoc = Bid[0].FinancialDoc;
                        BidTable.FreezeDate = Bid[0].FreezeDate;
                        BidTable.FreezeStatus = Bid[0].FreezeStatus;
                        db.SaveChanges();
                    }

                    tenderbid.BidDetails = BidTable;
                    tenderbid.TenderDetails = tendor;

                    tbl_Department tbldept = db.tbl_Department.FirstOrDefault(x => x.ID == tenderbid.TenderDetails.DepartmentID);

                    tenderbid.DepartmentName = tbldept.DepartmentName;

                    tbl_Category tblcat = db.tbl_Category.FirstOrDefault(x => x.ID == tenderbid.TenderDetails.CategoryID);
                    tenderbid.CategoryName = tblcat.CategoryName;

                    tblUserProfile tblup = db.tblUserProfiles.FirstOrDefault(x => x.ID == tenderbid.TenderDetails.UserID);
                    tenderbid.UserName = tblup.Name;

                    return tenderbid;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        public static tbl_Bid UpdateBid(tbl_Bid BidDetails)
        {
            try
            {
                using(DB db = new DB())
                {
                    tbl_Bid bid = db.tbl_Bid.FirstOrDefault(x => x.ID == BidDetails.ID);
                    bid.BidSubmitDate = DateTime.Now;

                    bid.EMDIssuer = BidDetails.EMDIssuer;
                    bid.EMDRefId = BidDetails.EMDRefId;
                    bid.EMDValidUpto = BidDetails.EMDValidUpto;

                    bid.TechIssuer = BidDetails.TechIssuer;
                    bid.TechRefID = BidDetails.TechRefID;
                    bid.TechValidUpto = BidDetails.TechValidUpto;
                    db.SaveChanges();

                    return bid;
                }

            }
            catch (Exception ex) { throw ex; }
        }

        public static tbl_Bid FreezeBid(tbl_Bid BidDetails)
        {
            try
            {
                using (DB db = new DB())
                {
                    tbl_Bid bid = db.tbl_Bid.FirstOrDefault(x => x.ID == BidDetails.ID);
                    tbl_TenderDetails td = db.tblTenderDetails.FirstOrDefault(x => x.ID == bid.TenderID);
                    bid.BidSubmitDate = DateTime.Now;

                    bid.EMDIssuer = BidDetails.EMDIssuer;
                    bid.EMDRefId = BidDetails.EMDRefId;
                    bid.EMDValidUpto = BidDetails.EMDValidUpto;

                    bid.TechIssuer = BidDetails.TechIssuer;
                    bid.TechRefID = BidDetails.TechRefID;
                    bid.TechValidUpto = BidDetails.TechValidUpto;

                    bid.LastActivityIP = BidDetails.LastActivityIP;
                    bid.LastActivityDate = DateTime.Now;

                    if (td.FreezeDate >= DateTime.Now)
                    {
                        bid.FreezeDate = DateTime.Now;
                        bid.FreezeStatus = 1;
                    }

                    tbl_VendorDetails tblvender = db.tblVendorDetails.FirstOrDefault(x => x.userID == bid.VendorID);
                    string msg = "Your Bid is Freezed for Tender ID " + td.TenderID;
                    SMSService.Send(msg, tblvender.MobileNumber);

                    db.SaveChanges();

                    return bid;
                }

            }
            catch (Exception ex) { throw ex; }
        }
    }
}
