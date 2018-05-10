using HBS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HBS_MVC.DAL
{
    public class HbsContext : DbContext
    {
        public HbsContext() : base("HospitalBillingSystemContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }
    }
}