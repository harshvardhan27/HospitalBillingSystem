namespace HBS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pateint_doctor_model_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "Province_ID", "dbo.Provinces");
            DropForeignKey("dbo.Patients", "Province_ID", "dbo.Provinces");
            DropIndex("dbo.Doctors", new[] { "Province_ID" });
            DropIndex("dbo.Patients", new[] { "Province_ID" });
            AlterColumn("dbo.Doctors", "Province_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Patients", "Province_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "Province_ID");
            CreateIndex("dbo.Patients", "Province_ID");
            AddForeignKey("dbo.Doctors", "Province_ID", "dbo.Provinces", "ProvinceID", cascadeDelete: true);
            AddForeignKey("dbo.Patients", "Province_ID", "dbo.Provinces", "ProvinceID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "Province_ID", "dbo.Provinces");
            DropForeignKey("dbo.Doctors", "Province_ID", "dbo.Provinces");
            DropIndex("dbo.Patients", new[] { "Province_ID" });
            DropIndex("dbo.Doctors", new[] { "Province_ID" });
            AlterColumn("dbo.Patients", "Province_ID", c => c.Int());
            AlterColumn("dbo.Doctors", "Province_ID", c => c.Int());
            CreateIndex("dbo.Patients", "Province_ID");
            CreateIndex("dbo.Doctors", "Province_ID");
            AddForeignKey("dbo.Patients", "Province_ID", "dbo.Provinces", "ProvinceID");
            AddForeignKey("dbo.Doctors", "Province_ID", "dbo.Provinces", "ProvinceID");
        }
    }
}
