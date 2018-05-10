using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HBS_MVC.Models
{
    public class Account
    {
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Email Id is required"), EmailAddress(ErrorMessage = "Please Enter Correct Email"), MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }

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
        public object ID { get; internal set; }
    }
}