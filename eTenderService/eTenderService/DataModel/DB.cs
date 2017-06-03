using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using eTender.EncryptDecrypt;
using System.Data.Entity.Core.Objects;
namespace eTenderService.DataModel
{
    public class DB : DbContext
    {
        //public DbSet<CommiteeMember> CommiteeMembers { get; set; }


        public DB()
        {

            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += new ObjectMaterializedEventHandler(ObjectMaterialized);

        }
        void ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            DecryptEntity(e.Entity);
        }
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

        public override int SaveChanges()
        {
            var contextAdapter = ((IObjectContextAdapter)this);

            contextAdapter.ObjectContext.DetectChanges();

            var pendingEntities = contextAdapter.ObjectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                .Where(en => !en.IsRelationship).ToList();

            foreach (var entry in pendingEntities) //Encrypt all pending changes
                EncryptEntity(entry.Entity);

            int result = base.SaveChanges();

            foreach (var entry in pendingEntities) //Decrypt updated entities for continued use
                DecryptEntity(entry.Entity);

            return result;
        }


        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }
        public void EncryptEntity(object entity)
        {
            var encryptedProperties = entity.GetType().GetProperties()
             .Where(p => p.GetCustomAttributes(typeof(Encrypted), true).Any(a => p.PropertyType == typeof(String)));
            foreach (var property in encryptedProperties)
            {
                string value = property.GetValue(entity, null) as string;
                if (!String.IsNullOrEmpty(value))
                {
                    string encryptedValue = value.Encrypt();
                    property.SetValue(entity, encryptedValue, null);
                }
            }


        }
        public void DecryptEntity(object entity)
        {
            string value = string.Empty;
            var encryptedProperties = entity.GetType().GetProperties()
            .Where(p => p.GetCustomAttributes(typeof(Encrypted), true).Any(a => p.PropertyType == typeof(String)));

            foreach (var property in encryptedProperties)
            {
                string encryptedValue = property.GetValue(entity, null) as string;
                if (!String.IsNullOrEmpty(encryptedValue))
                {
                    try
                    {
                        value = encryptedValue.Decrypt();
                    }
                    catch
                    {
                        value = encryptedValue; 
                    }
                    this.Entry(entity).Property(property.Name).OriginalValue = value;
                    this.Entry(entity).Property(property.Name).IsModified = false;
                }
            }
        }


    }
}
