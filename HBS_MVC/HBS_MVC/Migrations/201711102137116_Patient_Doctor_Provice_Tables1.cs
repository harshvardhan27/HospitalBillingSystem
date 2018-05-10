namespace HBS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patient_Doctor_Provice_Tables1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "ProvienceId");
            DropColumn("dbo.Patients", "ProvienceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "ProvienceId", c => c.Int(nullable: false));
            AddColumn("dbo.Doctors", "ProvienceId", c => c.Int(nullable: false));
        }
    }
}
