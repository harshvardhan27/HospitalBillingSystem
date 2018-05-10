using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HBS_MVC.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Firstname is required"), MaxLength(30)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required"), MaxLength(30)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Address is required"), MaxLength(30), DisplayName("Appartment")]
        public string Appartment { get; set; }

        [DisplayName("Street"), MaxLength(100)]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required"), MaxLength(30)]
        public string City { get; set; }

        //[Required(ErrorMessage ="Please Select Province")]
        //public int ProvienceId { get; set; }

        [Required(ErrorMessage = "PostalCode is required"), MaxLength(7)]
        public string Postalcode { get; set; }

        [Required(ErrorMessage = "Country is required"), MaxLength(20)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Date of birth is required"), DataType(DataType.Date), DisplayName("Date of birth")]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "Email Id is required"), EmailAddress(ErrorMessage = "Please Enter Correct Email"), MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact No is required"), MaxLength(10), DisplayName("Contact No"), StringLength(10, ErrorMessage = "Must Not Exceed 10 Char")]
        public string Contactno { get; set; }

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

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Lastname + ", " + Firstname;
            }
        }

        [Required(ErrorMessage = "Province is required")]
        public int ProvinceID { get; set; }
        public virtual Province Province { get; set; }
    }
}