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
    public class DA_Appointment
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        BO_AppointmentDetails boAppointmentDetails;
        DA_AppointmentDetails daAppointmentDetails = new DA_AppointmentDetails();
        #endregion

        public static string GetAppointmentDataTable()
        {
            string strResponse = "";
            DataTable dtAppointment = new DataTable();
            dtAppointment.Columns.Add("DT_RowId");
            dtAppointment.Columns.Add("APPOINTMENT_ID");
            dtAppointment.Columns.Add("PATIENT_ID");
            dtAppointment.Columns.Add("PATIENT_NAME");
            dtAppointment.Columns.Add("DOCTOR_ID");
            dtAppointment.Columns.Add("DOCTOR_NAME");
            dtAppointment.Columns.Add("APPOINTMENT_DATE");
            dtAppointment.Columns.Add("APPOINTMENT_TIME");
            dtAppointment.Columns.Add("PROCEDURE_ID");
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
                    DataRow dr = dtAppointment.NewRow();
                    dr["DT_RowId"] = "row_" + Convert.ToInt64(dt.Rows[i]["APPOINTMENT_ID"].ToString());
                    dr["APPOINTMENT_ID"] = Convert.ToInt64(dt.Rows[i]["APPOINTMENT_ID"].ToString());
                    dr["PATIENT_ID"] = Convert.ToInt64(dt.Rows[i]["PATIENT_ID"].ToString());
                    dr["PATIENT_NAME"] = dt.Rows[i]["PATIENT_NAME"].ToString().ToUpper();
                    dr["DOCTOR_ID"] = Convert.ToInt64(dt.Rows[i]["DOCTOR_ID"].ToString());
                    dr["DOCTOR_NAME"] = dt.Rows[i]["DOCTOR_NAME"].ToString().ToUpper();
                    string appointmentDate = String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(dt.Rows[i]["APPOINTMENT_DATE"].ToString()));
                    dr["APPOINTMENT_DATE"] = appointmentDate;
                    dr["APPOINTMENT_TIME"] = dt.Rows[i]["APPOINTMENT_TIME"].ToString().ToUpper();
                    dr["PROCEDURE_ID"] = getProcedureID(Convert.ToInt64(dt.Rows[i]["APPOINTMENT_ID"].ToString()));
                    dtAppointment.Rows.Add(dr);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dtAppointment.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dtAppointment.Columns)
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
                result = result.TrimEnd(',');
            }
            catch (SqlException ex)
            {

            }
            return result;
        }

        public int InsertAppointment(BO_Appointment[] DataObjectAppointment)
        {
            int insertedAppointmentId = -1;
            string query = "insert into appointment(patient_id,doctor_id,appointment_date,appointment_time,date_created,created_by) output INSERTED.APPOINTMENT_ID values (@patient_id,@doctor_id,@appointment_date,@appointment_time,@date_created,@created_by)";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@patient_id", SqlDbType.Int, 10, "patient_id");
            cmd.Parameters["@patient_id"].Value = DataObjectAppointment[0].Patientid;
            cmd.Parameters.Add("@doctor_id", SqlDbType.Int, 10, "doctor_id");
            cmd.Parameters["@doctor_id"].Value = DataObjectAppointment[0].Doctorid;
            cmd.Parameters.Add("@appointment_date", SqlDbType.DateTime, 12, "appointment_date");
            cmd.Parameters["@appointment_date"].Value = DataObjectAppointment[0].Appointmentdate;
            cmd.Parameters.Add("@appointment_time", SqlDbType.VarChar, 12, "appointment_time");
            cmd.Parameters["@appointment_time"].Value = DataObjectAppointment[0].Appointmenttime;
            cmd.Parameters.Add("@date_created", SqlDbType.DateTime, 12, "date_created");
            cmd.Parameters["@date_created"].Value = DataObjectAppointment[0].Datecreated;
            cmd.Parameters.Add("@created_by", SqlDbType.Int, 10, "created_by");
            cmd.Parameters["@created_by"].Value = DataObjectAppointment[0].Createdby;

            insertedAppointmentId = Convert.ToInt32(cmd.ExecuteScalar());
            if (insertedAppointmentId > 0)
            {
                for (int i = 0; i < DataObjectAppointment.Length; i++)
                {
                    boAppointmentDetails = new BO_AppointmentDetails();
                    boAppointmentDetails.Patientid = DataObjectAppointment[i].Patientid;
                    boAppointmentDetails.Appointmentid = insertedAppointmentId;
                    boAppointmentDetails.Doctorid = DataObjectAppointment[i].Doctorid;
                    boAppointmentDetails.Procedureid = DataObjectAppointment[i].Procedureid;
                    boAppointmentDetails.Createdby = DataObjectAppointment[i].Createdby;
                    boAppointmentDetails.Datecreated = System.DateTime.Now;
                    boAppointmentDetails.Paid = DataObjectAppointment[i].Paid;
                    int insertedAppointmentDeatils = daAppointmentDetails.InsertAppointmentDetails(boAppointmentDetails);
                }
            }
            cmd.Dispose();
            con.Close();
            return insertedAppointmentId;
        }

        public int UpdateAppointment(BO_Appointment[] DataObjectAppointment)
        {
            int updatedAppointmentId = -1;
            string query = "update appointment set patient_id=@patient_id,doctor_id=@doctor_id,appointment_date=@appointment_date,appointment_time=@appointment_time,date_modified=@date_modified,modified_by=@modified_by where appointment_id=@appointment_id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@appointment_id", SqlDbType.Int, 10, "appointment_id");
            cmd.Parameters["@appointment_id"].Value = DataObjectAppointment[0].Appointmentid;
            cmd.Parameters.Add("@patient_id", SqlDbType.Int, 10, "patient_id");
            cmd.Parameters["@patient_id"].Value = DataObjectAppointment[0].Patientid;
            cmd.Parameters.Add("@doctor_id", SqlDbType.Int, 10, "doctor_id");
            cmd.Parameters["@doctor_id"].Value = DataObjectAppointment[0].Doctorid;
            cmd.Parameters.Add("@appointment_date", SqlDbType.DateTime, 12, "appointment_date");
            cmd.Parameters["@appointment_date"].Value = DataObjectAppointment[0].Appointmentdate;
            cmd.Parameters.Add("@appointment_time", SqlDbType.VarChar, 12, "appointment_time");
            cmd.Parameters["@appointment_time"].Value = DataObjectAppointment[0].Appointmenttime;
            cmd.Parameters.Add("@date_modified", SqlDbType.DateTime, 12, "date_modified");
            cmd.Parameters["@date_modified"].Value = DataObjectAppointment[0].Datemodified;
            cmd.Parameters.Add("@modified_by", SqlDbType.Int, 10, "modified_by");
            cmd.Parameters["@modified_by"].Value = DataObjectAppointment[0].Modifiedby;
            cmd.ExecuteNonQuery();
            updatedAppointmentId = Convert.ToInt32(DataObjectAppointment[0].Appointmentid);
            if (updatedAppointmentId > 0)
            {
                DataTable dtAppointmentDetails = daAppointmentDetails.GetAppointmentDetails(updatedAppointmentId);
                dtAppointmentDetails.Columns.Add("ACTION", typeof(System.String));
                foreach (DataRow row in dtAppointmentDetails.Rows)
                {
                    row["ACTION"] = "DELETE";
                }

                for (int i = 0; i < DataObjectAppointment.Length; i++)
                {
                    string expression = "PROCEDURE_ID=" + DataObjectAppointment[i].Procedureid;
                    DataRow[] foundRows;
                    foundRows = dtAppointmentDetails.Select(expression);
                    if (foundRows.Length > 0)
                    {
                        foundRows[0]["PATIENT_HISTORY_ID"] = foundRows[0]["PATIENT_HISTORY_ID"];
                        foundRows[0]["PATIENT_ID"] = DataObjectAppointment[i].Patientid;
                        foundRows[0]["APPOINTMENT_ID"] = DataObjectAppointment[i].Appointmentid;
                        foundRows[0]["DOCTOR_ID"] = DataObjectAppointment[i].Doctorid;
                        foundRows[0]["PROCEDURE_ID"] = DataObjectAppointment[i].Procedureid;
                        foundRows[0]["DATE_CREATED"] = foundRows[0]["DATE_CREATED"];
                        foundRows[0]["CREATED_BY"] = foundRows[0]["CREATED_BY"];
                        foundRows[0]["DATE_MODIFIED"] = DataObjectAppointment[i].Datemodified;
                        foundRows[0]["MODIFIED_BY"] = DataObjectAppointment[i].Modifiedby;
                        foundRows[0]["PAID"] = DataObjectAppointment[i].Paid;
                        foundRows[0]["ACTION"] = "UPDATE";
                    }
                    else
                    {
                        DataRow dr = dtAppointmentDetails.NewRow();
                        dr["PATIENT_HISTORY_ID"] = 0;
                        dr["PATIENT_ID"] = DataObjectAppointment[i].Patientid;
                        dr["APPOINTMENT_ID"] = DataObjectAppointment[i].Appointmentid;
                        dr["DOCTOR_ID"] = DataObjectAppointment[i].Doctorid;
                        dr["PROCEDURE_ID"] = DataObjectAppointment[i].Procedureid;
                        dr["DATE_CREATED"] = System.DateTime.Now;
                        dr["CREATED_BY"] = DataObjectAppointment[i].Modifiedby;
                        dr["DATE_MODIFIED"] = System.DateTime.Now;
                        dr["MODIFIED_BY"] = DataObjectAppointment[i].Modifiedby;
                        dr["PAID"] = DataObjectAppointment[i].Paid;
                        dr["ACTION"] = "INSERT";
                        dtAppointmentDetails.Rows.Add(dr);
                        dtAppointmentDetails.AcceptChanges();
                    }
                }

                for (int j = 0; j < dtAppointmentDetails.Rows.Count; j++)
                {
                    if (dtAppointmentDetails.Rows[j]["ACTION"].ToString().ToUpper() == "UPDATE")
                    {
                        boAppointmentDetails = new BO_AppointmentDetails();
                        boAppointmentDetails.Patienthistoryid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["PATIENT_HISTORY_ID"]);
                        boAppointmentDetails.Patientid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["PATIENT_ID"]);
                        boAppointmentDetails.Appointmentid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["APPOINTMENT_ID"]);
                        boAppointmentDetails.Doctorid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["DOCTOR_ID"]);
                        boAppointmentDetails.Procedureid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["PROCEDURE_ID"]);
                        boAppointmentDetails.Modifiedby = Convert.ToInt64(dtAppointmentDetails.Rows[j]["MODIFIED_BY"]);
                        boAppointmentDetails.Datemodified = System.DateTime.Now;
                        boAppointmentDetails.Paid = dtAppointmentDetails.Rows[j]["PAID"].ToString();
                        daAppointmentDetails.UpdateAppointmentDetails(boAppointmentDetails);

                    }
                    else if (dtAppointmentDetails.Rows[j]["ACTION"].ToString().ToUpper() == "INSERT")
                    {

                        boAppointmentDetails = new BO_AppointmentDetails();
                        boAppointmentDetails.Patientid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["PATIENT_ID"]);
                        boAppointmentDetails.Appointmentid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["APPOINTMENT_ID"]);
                        boAppointmentDetails.Doctorid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["DOCTOR_ID"]);
                        boAppointmentDetails.Procedureid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["PROCEDURE_ID"]);
                        boAppointmentDetails.Createdby = Convert.ToInt64(dtAppointmentDetails.Rows[j]["CREATED_BY"]);
                        boAppointmentDetails.Datecreated = System.DateTime.Now;
                        boAppointmentDetails.Paid = dtAppointmentDetails.Rows[j]["PAID"].ToString();
                        int insertedAppointmentDeatils = daAppointmentDetails.InsertAppointmentDetails(boAppointmentDetails);
                    }
                    else
                    {
                        boAppointmentDetails = new BO_AppointmentDetails();
                        boAppointmentDetails.Patienthistoryid = Convert.ToInt64(dtAppointmentDetails.Rows[j]["PATIENT_HISTORY_ID"]);
                        daAppointmentDetails.DeleteAppointmentDetails(boAppointmentDetails);
                    }
                }
            }
            cmd.Dispose();
            con.Close();
            return updatedAppointmentId;
        }

        public void DeleteAppointment(BO_Appointment DataObjectAppointment)
        {
            #region APPOINTMENT_DETAILS
            boAppointmentDetails = new BO_AppointmentDetails();
            boAppointmentDetails.Appointmentid = DataObjectAppointment.Appointmentid;
            daAppointmentDetails.DeleteAppointmentDetails(boAppointmentDetails);
            #endregion

            string query = "delete from appointment where appointment_id=@appointment_id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@appointment_id", SqlDbType.Int, 20, "appointment_id");
            cmd.Parameters["@appointment_id"].Value = DataObjectAppointment.Appointmentid;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

    }
}
