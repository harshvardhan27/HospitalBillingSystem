namespace HBS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 20),
                        CreatedBy = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(),
                        DateModified = c.DateTime(),
                        DateExpired = c.DateTime(),
                        ExpipredFlag = c.String(),
                    })
                .PrimaryKey(t => t.AccountID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
