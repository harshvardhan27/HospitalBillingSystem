using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class DA_Province
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        #endregion

        public DataTable GetProvienceList()
        {
            string query = "select * from provience";
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
