using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HBS
{
    public partial class Appointment : System.Web.UI.Page
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        DA_Patient daPateint = new DA_Patient();
        DA_Doctor daDoctor = new DA_Doctor();
        DA_Procedure daProcedure = new DA_Procedure();
        DA_Appointment daAppointment = new DA_Appointment();
        BO_Appointment boAppointment = new BO_Appointment();
        DA_AppointmentDetails daAppointmentDetails = new DA_AppointmentDetails();


        private string strValidationFailedItem = string.Empty;
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SKEY"] != null)
            {
                GetPatientList();
                GetDoctorList();
                GetProcedureList();
                boAppointment.SKEY = Convert.ToInt64(Session["SKEY"].ToString());
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        public void GetPatientList()
        {
            DataTable dtPateint = daPateint.GetPatientList();
            ddlPatient.DataSource = dtPateint;
            ddlPatient.DataTextField = "PATIENT_NAME";
            ddlPatient.DataValueField = "PATIENT_ID";
            ddlPatient.DataBind();
        }

        public void GetDoctorList()
        {
            DataTable dtDoctor = daDoctor.GetDoctorsList();
            ddlDoctor.DataSource = dtDoctor;
            ddlDoctor.DataTextField = "DOCTOR_NAME";
            ddlDoctor.DataValueField = "DOCTOR_ID";
            ddlDoctor.DataBind();
        }

        public void GetProcedureList()
        {
            DataTable dtProcedure = daProcedure.GetProcedureList();
            ddlProcedure.DataSource = dtProcedure;
            ddlProcedure.DataTextField = "PROCEDURE_DESCRIPTION";
            ddlProcedure.DataValueField = "PROCEDURE_ID";
            ddlProcedure.DataBind();
        }

        [WebMethod]
        public static string BindDataTable()
        {
            string strToReturn = "";
            strToReturn = DA_Appointment.GetAppointmentDataTable();
            return strToReturn;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hidPrimaryKey.Value == string.Empty)
            {
                if (ValidateData(out strValidationFailedItem, HBS_Utility.CRUDOperation.Insert))
                {
                    string[] proceduresArr = Convert.ToString(hidSelectedProcedure.Value).Split(',');
                    BO_Appointment[] boAppointmentArr = new BO_Appointment[proceduresArr.Length];
                    for (int i = 0; i < proceduresArr.Length; i++)
                    {
                        boAppointmentArr[i] = new BO_Appointment();
                        boAppointmentArr[i].Patientid = Convert.ToInt64(hidSelectedPatient.Value);
                        boAppointmentArr[i].Doctorid = Convert.ToInt64(hidSelectedDoctor.Value);
                        boAppointmentArr[i].Procedureid = Convert.ToInt64(proceduresArr[i].ToString());
                        boAppointmentArr[i].Appointmentdate = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        boAppointmentArr[i].Appointmenttime = txtAppointmentTime.Text;
                        boAppointmentArr[i].Datecreated = System.DateTime.Now;
                        boAppointmentArr[i].Createdby = boAppointment.SKEY;
                        boAppointmentArr[i].Paid = "N";
                    }

                    int appointmentId = daAppointment.InsertAppointment(boAppointmentArr);
                    if (appointmentId > 0)
                    {
                        showMessage(HBS_Utility.ReturnMessage.Success, HBS_Utility.ReturnMessage.InsertSuccess, HBS_Utility.ReturnCode.Success);
                    }
                }

            }
            else
            {
                if (ValidateData(out strValidationFailedItem, HBS_Utility.CRUDOperation.Update))
                {
                    string[] proceduresArr = Convert.ToString(hidSelectedProcedure.Value).Split(',');
                    BO_Appointment[] boAppointmentArr = new BO_Appointment[proceduresArr.Length];
                    for (int i = 0; i < proceduresArr.Length; i++)
                    {
                        boAppointmentArr[i] = new BO_Appointment();
                        boAppointmentArr[i].Appointmentid = Convert.ToInt64(hidPrimaryKey.Value);
                        boAppointmentArr[i].Patientid = Convert.ToInt64(hidSelectedPatient.Value);
                        boAppointmentArr[i].Doctorid = Convert.ToInt64(hidSelectedDoctor.Value);
                        boAppointmentArr[i].Procedureid = Convert.ToInt64(proceduresArr[i].ToString());
                        boAppointmentArr[i].Appointmentdate = DateTime.ParseExact(txtAppointmentDate.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        boAppointmentArr[i].Appointmenttime = txtAppointmentTime.Text;
                        boAppointmentArr[i].Datemodified = System.DateTime.Now;
                        boAppointmentArr[i].Modifiedby = boAppointment.SKEY;
                        boAppointmentArr[i].Paid = "N";
                    }

                    int appointmentId = daAppointment.UpdateAppointment(boAppointmentArr);
                    if (appointmentId > 0)
                    {
                        showMessage(HBS_Utility.ReturnMessage.Success, HBS_Utility.ReturnMessage.UpdateSuccess, HBS_Utility.ReturnCode.Success);
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            boAppointment.Appointmentid = Convert.ToInt64(hidPrimaryKey.Value);
            daAppointment.DeleteAppointment(boAppointment);
        }

        private bool ValidateData(out string strValidationFailedItem, HBS_Utility.CRUDOperation operation)
        {
            bool ValidationStatus = true;
            string strToReturn = string.Empty;

            if (operation == HBS_Utility.CRUDOperation.Insert || operation == HBS_Utility.CRUDOperation.Update)
            {
                strToReturn += (string.IsNullOrEmpty(hidSelectedPatient.Value)) ? "Please select patient. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(hidSelectedDoctor.Value)) ? "Please select doctor. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(hidSelectedProcedure.Value)) ? "Please select procedure. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(txtAppointmentDate.Text)) ? "Please enter appointment date. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(txtAppointmentTime.Text)) ? "Please enter appointment time. <br />" : "";
            }

            ValidationStatus = (string.IsNullOrEmpty(strToReturn.Trim())) ? true : false;
            strValidationFailedItem = strToReturn;
            showMessage("Validation Error", strValidationFailedItem, HBS_Utility.ReturnCode.Failure);

            return ValidationStatus;
        }

        private void showMessage(string errorMessage_Head, string errorMessage_Body, string errorCode)
        {
            errorPanel.Visible = true;
            string successClass = "panel panel-primary", errorClass = "panel panel-danger", warningClass = "panel panel-warning", finalClass = "";
            switch (errorCode)
            {
                case "00": finalClass = successClass; break;
                case "02": finalClass = errorClass; break;
                case "01": finalClass = warningClass; break;
                default: finalClass = successClass; break;
            }
            errorPanel.Attributes["class"] = finalClass;
            panelHead.InnerHtml = errorMessage_Head;
            panelContent.InnerHtml = errorMessage_Body;
        }
    }
}