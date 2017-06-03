namespace eTender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_VendorDetails", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_VendorDetails", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
