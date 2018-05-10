using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class BO_Procedure
    {
        public Int64? Procedureid { get; set; }
        public string Proceduredescription { get; set; }
        public Decimal? Procedurecost { get; set; }
        public DateTime? Datecreated { get; set; }
        public Int64? Createdby { get; set; }
        public DateTime? Datemodified { get; set; }
        public Int64? Modifiedby { get; set; }
    }
}
