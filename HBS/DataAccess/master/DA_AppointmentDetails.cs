using BusinessObject;
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
    public class DA_AppointmentDetails
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        #endregion

        public int InsertAppointmentDetails(BO_AppointmentDetails DataObjectAppointmentDetails)
        {
            int insertedAppointmentDetailsId = -1;
            string query = "insert into patient_history(patient_id,appointment_id,doctor_id,procedure_id,date_created,created_by,paid) output INSERTED.PATIENT_HISTORY_ID values (@patient_id,@appointment_id,@doctor_id,@procedure_id,@date_created,@created_by,@paid)";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@patient_id", SqlDbType.Int, 10, "patient_id");
            cmd.Parameters["@patient_id"].Value = DataObjectAppointmentDetails.Patientid;
            cmd.Parameters.Add("@appointment_id", SqlDbType.Int, 10, "appointment_id");
            cmd.Parameters["@appointment_id"].Value = DataObjectAppointmentDetails.Appointmentid;
            cmd.Parameters.Add("@doctor_id", SqlDbType.Int, 10, "doctor_id");
            cmd.Parameters["@doctor_id"].Value = DataObjectAppointmentDetails.Doctorid;
            cmd.Parameters.Add("@procedure_id", SqlDbType.Int, 10, "procedure_id");
            cmd.Parameters["@procedure_id"].Value = DataObjectAppointmentDetails.Procedureid;
            cmd.Parameters.Add("@date_created", SqlDbType.DateTime, 12, "date_created");
            cmd.Parameters["@date_created"].Value = DataObjectAppointmentDetails.Datecreated;
            cmd.Parameters.Add("@created_by", SqlDbType.Int, 10, "created_by");
            cmd.Parameters["@created_by"].Value = DataObjectAppointmentDetails.Createdby;
            cmd.Parameters.Add("@paid", SqlDbType.VarChar, 1, "paid");
            cmd.Parameters["@paid"].Value = DataObjectAppointmentDetails.Paid;

            insertedAppointmentDetailsId = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Dispose();
            con.Close();
            return insertedAppointmentDetailsId;
        }

        public void UpdateAppointmentDetails(BO_AppointmentDetails DataObjectAppointmentDetails)
        {
            string query = "update patient_history set patient_id=@patient_id,appointment_id=@appointment_id,doctor_id=@doctor_id,procedure_id=@procedure_id,date_modified=@date_modified,modified_by=@modified_by,paid=@paid where patient_history_id=@patient_history_id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@patient_history_id", SqlDbType.Int, 10, "patient_history_id");
            cmd.Parameters["@patient_history_id"].Value = DataObjectAppointmentDetails.Patienthistoryid;
            cmd.Parameters.Add("@patient_id", SqlDbType.Int, 10, "patient_id");
            cmd.Parameters["@patient_id"].Value = DataObjectAppointmentDetails.Patientid;
            cmd.Parameters.Add("@appointment_id", SqlDbType.Int, 10, "appointment_id");
            cmd.Parameters["@appointment_id"].Value = DataObjectAppointmentDetails.Appointmentid;
            cmd.Parameters.Add("@doctor_id", SqlDbType.Int, 10, "doctor_id");
            cmd.Parameters["@doctor_id"].Value = DataObjectAppointmentDetails.Doctorid;
            cmd.Parameters.Add("@procedure_id", SqlDbType.Int, 10, "procedure_id");
            cmd.Parameters["@procedure_id"].Value = DataObjectAppointmentDetails.Procedureid;
            cmd.Parameters.Add("@date_modified", SqlDbType.DateTime, 12, "date_modified");
            cmd.Parameters["@date_modified"].Value = DataObjectAppointmentDetails.Datemodified;
            cmd.Parameters.Add("@modified_by", SqlDbType.Int, 10, "modified_by");
            cmd.Parameters["@modified_by"].Value = DataObjectAppointmentDetails.Modifiedby;
            cmd.Parameters.Add("@paid", SqlDbType.VarChar, 1, "paid");
            cmd.Parameters["@paid"].Value = DataObjectAppointmentDetails.Paid;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void DeleteAppointmentDetails(BO_AppointmentDetails DataObjectAppointmentDetails)
        {
            string query = "delete from patient_history where appointment_id=@appointment_id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@appointment_id", SqlDbType.Int, 10, "appointment_id");
            cmd.Parameters["@appointment_id"].Value = DataObjectAppointmentDetails.Appointmentid;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public DataTable GetAppointmentDetails(int appointmentId)
        {
            DataTable dtDetails = new DataTable();
            string query = "select * from patient_history where appointment_id=" + appointmentId + "";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            adap.Fill(dtDetails);
            return dtDetails;
        }

        public DataTable GetBillingDetails(int appointmentId)
        {
            DataTable dtDetails = new DataTable();
            string query = "select * from appointment_detail_view where appointment_id=" + appointmentId + "";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            adap.Fill(dtDetails);
            return dtDetails;
        }

        public void UpdateAppointmentPaymentStatus(BO_AppointmentDetails DataObjectAppointmentDetails)
        {
            string query = "update patient_history set date_modified=@date_modified,modified_by=@modified_by,paid=@paid where appointment_id=@appointment_id";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@appointment_id", SqlDbType.Int, 10, "appointment_id");
            cmd.Parameters["@appointment_id"].Value = DataObjectAppointmentDetails.Appointmentid;
            cmd.Parameters.Add("@date_modified", SqlDbType.DateTime, 12, "date_modified");
            cmd.Parameters["@date_modified"].Value = DataObjectAppointmentDetails.Datemodified;
            cmd.Parameters.Add("@modified_by", SqlDbType.Int, 10, "modified_by");
            cmd.Parameters["@modified_by"].Value = DataObjectAppointmentDetails.Modifiedby;
            cmd.Parameters.Add("@paid", SqlDbType.VarChar, 1, "paid");
            cmd.Parameters["@paid"].Value = DataObjectAppointmentDetails.Paid;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}
