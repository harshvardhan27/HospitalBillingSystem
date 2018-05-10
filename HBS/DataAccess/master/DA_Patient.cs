using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BusinessObject;

namespace DataAccess
{
    public class DA_Patient
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        #endregion

        public DataTable GetPatientList()
        {
            string query = "select * from patient_view";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            adap.Dispose();
            con.Close();
            return dt;
        }

        public static string GetPatientDataTable()
        {
            string strResponse = "";
            DataTable dtPatient = new DataTable();
            dtPatient.Columns.Add("DT_RowId");
            dtPatient.Columns.Add("PATIENTID");
            dtPatient.Columns.Add("NAME");
            dtPatient.Columns.Add("FIRSTNAME");
            dtPatient.Columns.Add("LASTNAME");
            dtPatient.Columns.Add("ADDRESSLINEONE");
            dtPatient.Columns.Add("ADDRESSLINETWO");
            dtPatient.Columns.Add("CITY");
            dtPatient.Columns.Add("PROVIENCEID");
            dtPatient.Columns.Add("PROVIENCEDESCRIPTION");
            dtPatient.Columns.Add("POSTALCODE");
            dtPatient.Columns.Add("DOB");
            dtPatient.Columns.Add("EMAIL");
            dtPatient.Columns.Add("CONTACTNO");
            string query = "select * from patient_view";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString);
                con.Open();
                SqlDataAdapter adap = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtPatient.NewRow();
                    dr["DT_RowId"] = "row_" + Convert.ToInt64(dt.Rows[i]["PATIENT_ID"].ToString());
                    dr["PATIENTID"] = Convert.ToInt64(dt.Rows[i]["PATIENT_ID"].ToString());
                    dr["NAME"] = dt.Rows[i]["PATIENT_NAME"].ToString().ToUpper();
                    dr["FIRSTNAME"] = dt.Rows[i]["FIRSTNAME"].ToString().ToUpper();
                    dr["LASTNAME"] = dt.Rows[i]["LASTNAME"].ToString().ToUpper();
                    dr["ADDRESSLINEONE"] = dt.Rows[i]["ADDRESS_LINE_1"].ToString().ToUpper();
                    dr["ADDRESSLINETWO"] = dt.Rows[i]["ADDRESS_LINE_2"].ToString().ToUpper();
                    dr["CITY"] = dt.Rows[i]["CITY"].ToString().ToUpper();
                    dr["PROVIENCEID"] = Convert.ToInt64(dt.Rows[i]["PROVIENCE_ID"].ToString());
                    dr["POSTALCODE"] = dt.Rows[i]["POSTAL_CODE"].ToString().ToUpper();
                    dr["PROVIENCEDESCRIPTION"] = dt.Rows[i]["PROVIENCE_DESCRIPTION"].ToString().ToUpper();
                    dr["DOB"] = Convert.ToDateTime(dt.Rows[i]["DOB"].ToString());
                    dr["EMAIL"] = dt.Rows[i]["EMAIL"].ToString();
                    dr["CONTACTNO"] = dt.Rows[i]["CONTACT_NO"].ToString();
                    dtPatient.Rows.Add(dr);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dtPatient.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dtPatient.Columns)
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

        public int InsertPatient(BO_Patient DataObjectPatient)
        {
            int insertedPateintId = -1;
            string query = "insert into patient(firstname,lastname,address_line_1,address_line_2,city,provience_id,postal_code,dob,email,contact_no,date_created,created_by) output INSERTED.PATIENT_ID values (@firstname,@lastname,@address_line_1,@address_line_2,@city,@provience_id,@postal_code,@dob,@email,@contact_no,@date_created,@created_by)";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 20, "firstname");
            cmd.Parameters["@firstname"].Value = DataObjectPatient.Firstname;
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 20, "lastname");
            cmd.Parameters["@lastname"].Value = DataObjectPatient.Lastname;
            cmd.Parameters.Add("@address_line_1", SqlDbType.VarChar, 25, "address_line_1");
            cmd.Parameters["@address_line_1"].Value = DataObjectPatient.Addresslineone;
            cmd.Parameters.Add("@address_line_2", SqlDbType.VarChar, 25, "address_line_2");
            cmd.Parameters["@address_line_2"].Value = DataObjectPatient.Addresslinetwo;
            cmd.Parameters.Add("@city", SqlDbType.VarChar, 20, "city");
            cmd.Parameters["@city"].Value = DataObjectPatient.City;
            cmd.Parameters.Add("@provience_id", SqlDbType.Int, 10, "provience_id");
            cmd.Parameters["@provience_id"].Value = DataObjectPatient.ProvienceId;
            cmd.Parameters.Add("@postal_code", SqlDbType.VarChar, 7, "postal_code");
            cmd.Parameters["@postal_code"].Value = DataObjectPatient.Postalcode;
            cmd.Parameters.Add("@dob", SqlDbType.DateTime, 12, "dob");
            cmd.Parameters["@dob"].Value = DataObjectPatient.Dob;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 25, "email");
            cmd.Parameters["@email"].Value = DataObjectPatient.Email;
            cmd.Parameters.Add("@contact_no", SqlDbType.VarChar, 12, "contact_no");
            cmd.Parameters["@contact_no"].Value = DataObjectPatient.Contactno;
            cmd.Parameters.Add("@date_created", SqlDbType.DateTime, 12, "date_created");
            cmd.Parameters["@date_created"].Value = DataObjectPatient.Datecreated;
            cmd.Parameters.Add("@created_by", SqlDbType.Int, 10, "created_by");
            cmd.Parameters["@created_by"].Value = DataObjectPatient.Createdby;

            insertedPateintId = Convert.ToInt32(cmd.ExecuteScalar());
            /*int rowsReturned = cmd.ExecuteNonQuery();
            if (rowsReturned > 0)
            {
                insertedPateintId = GetPatientId(DataObjectPatient);
            }*/
            cmd.Dispose();
            con.Close();
            return insertedPateintId;
        }

        public int UpdatePatient(BO_Patient DataObjectPatient)
        {
            int insertedPateintId = -1;
            string query = "update patient set firstname=@firstname,lastname=@lastname,address_line_1=@address_line_1,address_line_2=@address_line_2,city=@city,provience_id=@provience_id,postal_code=@postal_code,dob=@dob,email=@email,contact_no=@contact_no,date_modified=@date_modified,modified_by=@modified_by where patient_id=@patient_id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@patient_id", SqlDbType.Int, 20, "patient_id");
            cmd.Parameters["@patient_id"].Value = DataObjectPatient.Patientid;
            cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 20, "firstname");
            cmd.Parameters["@firstname"].Value = DataObjectPatient.Firstname;
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 20, "lastname");
            cmd.Parameters["@lastname"].Value = DataObjectPatient.Lastname;
            cmd.Parameters.Add("@address_line_1", SqlDbType.VarChar, 25, "address_line_1");
            cmd.Parameters["@address_line_1"].Value = DataObjectPatient.Addresslineone;
            cmd.Parameters.Add("@address_line_2", SqlDbType.VarChar, 25, "address_line_2");
            cmd.Parameters["@address_line_2"].Value = DataObjectPatient.Addresslinetwo;
            cmd.Parameters.Add("@city", SqlDbType.VarChar, 20, "city");
            cmd.Parameters["@city"].Value = DataObjectPatient.City;
            cmd.Parameters.Add("@provience_id", SqlDbType.Int, 10, "provience_id");
            cmd.Parameters["@provience_id"].Value = DataObjectPatient.ProvienceId;
            cmd.Parameters.Add("@postal_code", SqlDbType.VarChar, 7, "postal_code");
            cmd.Parameters["@postal_code"].Value = DataObjectPatient.Postalcode;
            cmd.Parameters.Add("@dob", SqlDbType.DateTime, 12, "dob");
            cmd.Parameters["@dob"].Value = DataObjectPatient.Dob;
            cmd.Parameters.Add("@email", SqlDbType.VarChar, 25, "email");
            cmd.Parameters["@email"].Value = DataObjectPatient.Email;
            cmd.Parameters.Add("@contact_no", SqlDbType.VarChar, 12, "contact_no");
            cmd.Parameters["@contact_no"].Value = DataObjectPatient.Contactno;
            cmd.Parameters.Add("@date_modified", SqlDbType.DateTime, 12, "date_modified");
            cmd.Parameters["@date_modified"].Value = DataObjectPatient.Datemodified;
            cmd.Parameters.Add("@modified_by", SqlDbType.Int, 10, "modified_by");
            cmd.Parameters["@modified_by"].Value = DataObjectPatient.Modifiedby;

            int rowsReturned = cmd.ExecuteNonQuery();
            if (rowsReturned > 0)
            {
                insertedPateintId = GetPatientId(DataObjectPatient);
            }
            cmd.Dispose();
            con.Close();
            return insertedPateintId;
        }

        public void DeletePatient(BO_Patient DataObjectPatient)
        {
            string query = "delete from patient where patient_id=@patient_id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@patient_id", SqlDbType.Int, 20, "patient_id");
            cmd.Parameters["@patient_id"].Value = DataObjectPatient.Patientid;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        private int GetPatientId(BO_Patient DataObjectPatient)
        {
            int patientId = -1;
            string query = "select patient_id from patient where firstname=@firstname and lastname=@lastname";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 20, "firstname");
            cmd.Parameters["@firstname"].Value = DataObjectPatient.Firstname;
            cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 20, "lastname");
            cmd.Parameters["@lastname"].Value = DataObjectPatient.Lastname;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                patientId = Convert.ToInt32(dr[0].ToString());
            }
            cmd.Dispose();
            con.Close();
            return patientId;
        }
    }
}
