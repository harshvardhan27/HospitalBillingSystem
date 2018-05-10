using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DA_Doctor
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        #endregion

        public DataTable GetDoctorsList()
        {
            string query = "select * from doctor_view";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            adap.Dispose();
            con.Close();
            return dt;
        }
    }
}
