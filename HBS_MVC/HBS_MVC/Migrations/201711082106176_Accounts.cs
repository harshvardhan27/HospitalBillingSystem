namespace HBS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "ExpiredFlag", c => c.String(maxLength: 1));
            DropColumn("dbo.Accounts", "ExpipredFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "ExpipredFlag", c => c.String());
            DropColumn("dbo.Accounts", "ExpiredFlag");
        }
    }
}
