namespace HBS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProvinceInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceID = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(nullable: false, maxLength: 30),
                        CreatedBy = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(),
                        DateModified = c.DateTime(),
                        DateExpired = c.DateTime(),
                        ExpiredFlag = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.ProvinceID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Provinces");
        }
    }
}
