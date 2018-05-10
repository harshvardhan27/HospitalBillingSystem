using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class BO_Appointment
    {
        public Int64? Appointmentid { get; set; }
        public Int64? Patientid { get; set; }
        public Int64? Doctorid { get; set; }
        public DateTime? Appointmentdate { get; set; }
        public string Appointmenttime { get; set; }
        public DateTime Datecreated { get; set; }
        public Int64? Createdby { get; set; }
        public DateTime? Datemodified { get; set; }
        public Int64? Modifiedby { get; set; }
        public Int64? SKEY { get; set; }
        public Int64? Procedureid { get; set; }
        public string Paid { get; set; }
        public Int64? Patienthistoryid { get; set; }
        public string Action { get; set; }
    }
}
