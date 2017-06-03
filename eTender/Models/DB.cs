using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using eTenderService.DataModel;
namespace eTender.Models
{
    public class DB:DbContext
    {
        public DbSet<tbl_TenderDetails> tblTenderDetails { get; set; }
        public DbSet<tbl_VendorDetails> tblVendorDetails { get; set; }
        public DbSet<tbl_Bid> tbl_Bid { get; set; }
        public DbSet<tbl_BidStatus> tbl_BidStatus { get; set; }
        public DbSet<tbl_Category> tbl_Category { get; set; }
        public DbSet<tbl_CommiteeMember> tbl_CommiteeMember { get; set; }
        public DbSet<tbl_Department> tbl_Department { get; set; }
        public DbSet<tbl_MemberActionOTPDetails> tbl_MemberActionOTPDetails { get; set; }
        public DbSet<tbl_MemberForTender> tbl_MemberForTender { get; set; }
        public DbSet<tbl_Status> tbl_Status { get; set; }
        public DbSet<tblUserProfile> tblUserProfiles { get; set; }
        public DbSet<tbl_OTPStatusCommiteeVerifiy> tbl_OTPStatusCommiteeVerifiys { get; set; }
    }
}