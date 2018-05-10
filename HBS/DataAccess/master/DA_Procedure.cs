using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DataAccess;

namespace DataAccess
{
    public class DA_Procedure
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        #endregion

        public DataTable GetProcedureList()
        {
            string query = "select * from procedure_info";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            adap.Dispose();
            con.Close();
            return dt;
        }

        public static string GetProcedures()
        {
            string strResponse = "";
            DataTable dtProcedure = new DataTable();
            dtProcedure.Columns.Add("DT_RowId");
            dtProcedure.Columns.Add("APPOINTMENT_ID");
            dtProcedure.Columns.Add("PROCEDURE_ID");
            dtProcedure.Columns.Add("PROCEDURE_DESCRIPTION");
            dtProcedure.Columns.Add("PROCEDURE_COST");
            dtProcedure.Columns.Add("PROCEDURE_ID");
            string query = "select * from appointment_view";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString);
                con.Open();
                SqlDataAdapter adap = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtProcedure.NewRow();
                    dr["DT_RowId"] = "row_" + Convert.ToInt64(dt.Rows[i]["PROCEDURE_ID"].ToString());
                    dr["APPOINTMENT_ID"] = Convert.ToInt64(dt.Rows[i]["APPOINTMENT_ID"].ToString());
                    dr["PROCEDURE_ID"] = Convert.ToInt64(dt.Rows[i]["PROCEDURE_ID"].ToString());
                    dr["PROCEDURE_DESCRIPTION"] = dt.Rows[i]["PROCEDURE_DESCRIPTION"].ToString().ToUpper();
                    dr["PROCEDURE_COST"] = Convert.ToDecimal(dt.Rows[i]["PROCEDURE_COST"].ToString());
                    dr["PROCEDURE_ID"] = getProcedureID(Convert.ToInt64(dt.Rows[i]["APPOINTMENT_ID"].ToString()));
                    dtProcedure.Rows.Add(dr);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dtProcedure.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dtProcedure.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                adap.Dispose();
                con.Dispose();
                strResponse = serializer.Serialize(rows);
            }
            catch (SqlException ex)
            {
                strResponse = ex.Message;
            }
            return strResponse;
        }

        private static string getProcedureID(long appId)
        {
            string result = string.Empty;
            string query = "select * from patient_history where appointment_id=" + appId + "";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString);
                con.Open();
                SqlDataAdapter adap = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result += dt.Rows[i]["PROCEDURE_ID"].ToString() + ",";
                }
            }
            catch (SqlException ex)
            {

            }
            return result;
        }
    }
}
