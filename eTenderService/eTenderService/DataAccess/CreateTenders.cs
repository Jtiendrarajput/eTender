using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTenderService.DataModel;
using eTenderService.Tempmodel;

namespace eTenderService.DataAccess
{
    public class CreateTenders
    {
        public static int Add(tbl_TenderDetails Tender)
        {
            try
            {
                using(DB db = new DB())
                {
                    Tender.Status = 1;
                    
                    db.tblTenderDetails.Add(Tender);
                    db.SaveChanges();
                    Tender.TenderID = GetTenderID(Tender.ID);
                    db.SaveChanges();

                }
                return Tender.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int AllowUpdate(int TenderId)
        {
            try
            {
                int Status;
                using (DB db = new DB())
                {
                     tbl_TenderDetails Tender = db.tblTenderDetails.FirstOrDefault(x => x.ID == TenderId);
                     if (Tender.ActiveDate >= DateTime.Now)
                     {
                         Status = 1;
                     }
                     else
                     {
                         Status = 0;
                     }
                }
                return Status;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int Update(tbl_TenderDetails Tender)
        {
            try
            {
                using (DB db = new DB())
                {
                    tbl_TenderDetails update = db.tblTenderDetails.FirstOrDefault(x => x.ID == Tender.ID);
                    update.Title = Tender.Title;
                    update.ActiveDate = Tender.ActiveDate;
                    update.BidStartDate = Tender.BidStartDate;
                    update.FreezeDate = Tender.FreezeDate;
                    update.DepartmentID = Tender.DepartmentID;
                    update.CategoryID = Tender.CategoryID;
                    update.DownloadStartDate = Tender.DownloadStartDate;
                    update.DownloadEndDate = Tender.DownloadEndDate;
                    update.BOQFilePath = Tender.BOQFilePath;
                    update.TenderDocPath = Tender.TenderDocPath;
                    update.TenderNoticePath = Tender.TenderNoticePath;

                    update.TechBidOpenDate = Tender.TechBidOpenDate;
                    update.FinancialBidOpenDate = Tender.FinancialBidOpenDate;
                    //update.ClarificationStartDate = Tender.ClarificationStartDate;
                    //update.ClarificationEndDate = Tender.ClarificationEndDate;
                    update.TenderFee = Tender.TenderFee;
                    update.EMDFee = Tender.EMDFee;                  
                    db.SaveChanges();

                }
                return Tender.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int delete(int TenderId)
        {
            try
            {
                using (DB db = new DB())
                {
                    tbl_TenderDetails update = db.tblTenderDetails.FirstOrDefault(x => x.ID == TenderId);
                    update.Status = 0;
                    db.SaveChanges();
                }
                return TenderId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<Temp_AllTenders> AllTenders()
        {
            try
            {
                List<Temp_AllTenders> lst = new List<Temp_AllTenders>();
                using(DB db = new DB())
                {
                    var allTenders = (from a1 in db.tblTenderDetails
                                      join a2 in db.tbl_Category on a1.CategoryID equals a2.ID
                                      join a3 in db.tbl_Department on a1.DepartmentID equals a3.ID
                                      where a1.AlotStatus == 0 && a1.Status == 1
                                      select new
                                      {
                                          a1.ID,
                                          a1.TenderID,
                                          a1.Title,
                                          a1.DepartmentID,
                                          a1.CategoryID,
                                          a1.ActiveDate,
                                          a1.BidStartDate,
                                          a1.FreezeDate,
                                          a2.CategoryName,
                                          a3.DepartmentName,
                                          a1.DownloadStartDate,
                                          a1.DownloadEndDate,
                                          a1.BOQFilePath,
                                          a1.TenderDocPath,
                                          a1.TenderNoticePath,
                                          a1.PublishDate,
                                          a1.TechBidOpenDate,
                                          a1.FinancialBidOpenDate,
                                          a1.ClarificationStartDate,
                                          a1.ClarificationEndDate,
                                          a1.TenderFee,
                                          a1.EMDFee,
                                          a1.AlotStatus

                                      }).ToList();

                   foreach(var v in allTenders)
                   {
                       Temp_AllTenders Temp = new Temp_AllTenders();
                       Temp.ID = v.ID;
                       Temp.Title = v.Title;
                       Temp.TenderID = v.TenderID;
                       Temp.Title = v.Title;
                       Temp.ActiveDate = v.ActiveDate;
                       Temp.BidStartDate = v.BidStartDate;
                       Temp.FreezeDate = v.FreezeDate;
                       Temp.DepartmentID = v.DepartmentID;
                       Temp.CategoryID = v.CategoryID;
                       Temp.CateName = v.CategoryName;
                       Temp.DeptName = v.DepartmentName;
                       Temp.DownloadStartDate = v.DownloadStartDate;
                       Temp.DownloadEndDate = v.DownloadEndDate;
                       Temp.BOQFilePath = v.BOQFilePath;
                       Temp.TenderDocPath = v.TenderDocPath;
                       Temp.TenderNoticePath = v.TenderNoticePath;
                       Temp.PublishDate = v.PublishDate;
                       Temp.TechBidOpenDate = v.TechBidOpenDate;
                       Temp.FinancialBidOpenDate = v.FinancialBidOpenDate;
                       //Temp.ClarificationStartDate = v.ClarificationStartDate;
                       //Temp.ClarificationEndDate = v.ClarificationEndDate;
                       Temp.TenderFee = v.TenderFee;
                       Temp.EMDFee = v.EMDFee;

                       lst.Add(Temp);
                        
                   }
                   return lst;
                }                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string GetTenderID(int  ID)
        {
            try
            {
                string TenderID = "";
                TenderID = "NPPTEND" + DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + ID.ToString("000000");

                return TenderID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
