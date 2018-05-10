using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class BO_Patient
    {
        public Int64? Patientid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Addresslineone { get; set; }
        public string Addresslinetwo { get; set; }
        public string City { get; set; }
        public Int64? ProvienceId { get; set; }
        public string Proviencedescription { get; set; }
        public string Postalcode { get; set; }
        public DateTime? Dob { get; set; }
        public string Email { get; set; }
        public string Contactno { get; set; }
        public DateTime? Datecreated { get; set; }
        public Int64? Createdby { get; set; }
        public DateTime? Datemodified { get; set; }
        public Int64? Modifiedby { get; set; }

        public string DT_RowId { get; set; }

        public Int64? SKEY { get; set; }
    }
}
