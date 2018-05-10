using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HBS_MVC.Models
{
    public class Province
    {
        public int ProvinceID { get; set; }

        [Column("ProvinceName"), Required(ErrorMessage = "Name is required"), MaxLength(30)]
        public string Name { get; set; }

        public int CreatedBy { get; set; } = Convert.ToInt32(1);

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int? ModifiedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateExpired { get; set; }

        [MaxLength(1)]
        public string ExpiredFlag { get; set; } = "N";

        //[Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Patient> Patients { get; set; }

        //[Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}