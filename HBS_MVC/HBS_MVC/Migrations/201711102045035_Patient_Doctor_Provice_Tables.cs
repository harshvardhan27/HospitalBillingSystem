namespace HBS_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patient_Doctor_Provice_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 30),
                        Lastname = c.String(nullable: false, maxLength: 30),
                        Appartment = c.String(nullable: false, maxLength: 30),
                        Street = c.String(maxLength: 100),
                        City = c.String(nullable: false, maxLength: 30),
                        ProvienceId = c.Int(nullable: false),
                        Postalcode = c.String(nullable: false, maxLength: 7),
                        Country = c.String(nullable: false, maxLength: 20),
                        Dob = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 30),
                        Contactno = c.String(nullable: false, maxLength: 10),
                        CreatedBy = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(),
                        DateModified = c.DateTime(),
                        DateExpired = c.DateTime(),
                        ExpiredFlag = c.String(maxLength: 1),
                        Province_ID = c.Int(),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.Provinces", t => t.Province_ID)
                .Index(t => t.Province_ID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 30),
                        Lastname = c.String(nullable: false, maxLength: 30),
                        Appartment = c.String(nullable: false, maxLength: 30),
                        Street = c.String(maxLength: 100),
                        City = c.String(nullable: false, maxLength: 30),
                        ProvienceId = c.Int(nullable: false),
                        Postalcode = c.String(nullable: false, maxLength: 7),
                        Country = c.String(nullable: false, maxLength: 20),
                        Dob = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 30),
                        Contactno = c.String(nullable: false, maxLength: 10),
                        CreatedBy = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(),
                        DateModified = c.DateTime(),
                        DateExpired = c.DateTime(),
                        ExpiredFlag = c.String(maxLength: 1),
                        Province_ID = c.Int(),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.Provinces", t => t.Province_ID)
                .Index(t => t.Province_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "Province_ID", "dbo.Provinces");
            DropForeignKey("dbo.Doctors", "Province_ID", "dbo.Provinces");
            DropIndex("dbo.Patients", new[] { "Province_ID" });
            DropIndex("dbo.Doctors", new[] { "Province_ID" });
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
        }
    }
}
