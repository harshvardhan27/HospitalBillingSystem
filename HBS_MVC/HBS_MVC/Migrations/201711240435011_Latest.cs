namespace HBS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Latest : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Doctors", name: "Province_ID", newName: "ProvinceID");
            RenameColumn(table: "dbo.Patients", name: "Province_ID", newName: "ProvinceID");
            RenameIndex(table: "dbo.Doctors", name: "IX_Province_ID", newName: "IX_ProvinceID");
            RenameIndex(table: "dbo.Patients", name: "IX_Province_ID", newName: "IX_ProvinceID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Patients", name: "IX_ProvinceID", newName: "IX_Province_ID");
            RenameIndex(table: "dbo.Doctors", name: "IX_ProvinceID", newName: "IX_Province_ID");
            RenameColumn(table: "dbo.Patients", name: "ProvinceID", newName: "Province_ID");
            RenameColumn(table: "dbo.Doctors", name: "ProvinceID", newName: "Province_ID");
        }
    }
}
