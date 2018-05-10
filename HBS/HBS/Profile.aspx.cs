using System;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;
using System.Web.Services;
using System.Configuration;
using BusinessObject;
using System.Globalization;

namespace HBS
{
    public partial class Profile : System.Web.UI.Page
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        DA_Province daProvince = new DA_Province();
        DA_Patient daPateint = new DA_Patient();
        BO_Patient boPatient = new BO_Patient();
        private string strValidationFailedItem = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SKEY"] != null)
            {
                GetProvinceList();
                boPatient.SKEY = Convert.ToInt64(Session["SKEY"].ToString());
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        public void GetProvinceList()
        {
            DataTable dtProvince = daProvince.GetProvienceList();
            ddlProvince.DataSource = dtProvince;
            ddlProvince.DataTextField = "PROVIENCE_DESCRIPTION";
            ddlProvince.DataValueField = "PROVIENCE_ID";
            ddlProvince.DataBind();
            //ddlProvince.Items.Insert(0, new ListItem("Select", "-1"));
        }


        [WebMethod]
        public static string BindDataTable()
        {
            string strToReturn = "";
            strToReturn = DA_Patient.GetPatientDataTable();
            return strToReturn;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hidPrimaryKey.Value == string.Empty)
            {
                if (ValidateData(out strValidationFailedItem, HBS_Utility.CRUDOperation.Insert))
                {
                    boPatient.Firstname = txtFirstname.Text;
                    boPatient.Lastname = txtLastname.Text;
                    boPatient.Addresslineone = txtAppartment.Text;
                    boPatient.Addresslinetwo = txtStreet.Text;
                    boPatient.City = txtCity.Text;
                    boPatient.ProvienceId = Convert.ToInt64(hidSelectedProvince.Value);
                    boPatient.Postalcode = txtPostalCode.Text;
                    boPatient.Dob = DateTime.ParseExact(txtDob.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    boPatient.Email = txtEmailId.Text;
                    boPatient.Contactno = txtContactNo.Text;
                    boPatient.Datecreated = System.DateTime.Now;
                    boPatient.Createdby = boPatient.SKEY;
                    int patientId = daPateint.InsertPatient(boPatient);
                    if(patientId > 0)
                    {
                        showMessage(HBS_Utility.ReturnMessage.Success, HBS_Utility.ReturnMessage.InsertSuccess, HBS_Utility.ReturnCode.Success);
                    }
                }

            }
            else
            {
                if (ValidateData(out strValidationFailedItem, HBS_Utility.CRUDOperation.Update))
                {
                    boPatient.Patientid = Convert.ToInt64(hidPrimaryKey.Value);
                    boPatient.Firstname = txtFirstname.Text;
                    boPatient.Lastname = txtLastname.Text;
                    boPatient.Addresslineone = txtAppartment.Text;
                    boPatient.Addresslinetwo = txtStreet.Text;
                    boPatient.City = txtCity.Text;
                    boPatient.ProvienceId = Convert.ToInt64(hidSelectedProvince.Value);
                    boPatient.Postalcode = txtPostalCode.Text;
                    boPatient.Dob = DateTime.ParseExact(txtDob.Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    boPatient.Email = txtEmailId.Text;
                    boPatient.Contactno = txtContactNo.Text;
                    boPatient.Datemodified = System.DateTime.Now;
                    boPatient.Modifiedby = boPatient.SKEY;
                    int patientId = daPateint.UpdatePatient(boPatient);
                    if (patientId > 0)
                    {
                        showMessage(HBS_Utility.ReturnMessage.Success, HBS_Utility.ReturnMessage.UpdateSuccess, HBS_Utility.ReturnCode.Success);
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            boPatient.Patientid = Convert.ToInt64(hidPrimaryKey.Value);
            daPateint.DeletePatient(boPatient);
        }

        private bool ValidateData(out string strValidationFailedItem, HBS_Utility.CRUDOperation operation)
        {
            bool ValidationStatus = true;
            string strToReturn = string.Empty;

            if (operation == HBS_Utility.CRUDOperation.Insert || operation == HBS_Utility.CRUDOperation.Update)
            {
                strToReturn += (string.IsNullOrEmpty(txtFirstname.Text)) ? "Please enter firstname. <br />" : ((!System.Text.RegularExpressions.Regex.IsMatch(txtFirstname.Text, "^[a-zA-Z]+$")) ? "Firstname accepts only alphabetical characters. <br />" : "");
                strToReturn += (string.IsNullOrEmpty(txtLastname.Text)) ? "Please enter lastname. <br />" : ((!System.Text.RegularExpressions.Regex.IsMatch(txtLastname.Text, "^[a-zA-Z]+$")) ? "Lastname accepts only alphabetical characters. <br />" : "");
                strToReturn += (string.IsNullOrEmpty(txtDob.Text)) ? "Please enter date of birth. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(txtEmailId.Text)) ? "Please enter email id. <br />" : ((!System.Text.RegularExpressions.Regex.IsMatch(txtEmailId.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*")) ? "Invalid email format. <br />" : "");
                strToReturn += (string.IsNullOrEmpty(txtContactNo.Text)) ? "Please enter Contact no. <br />" : ((!System.Text.RegularExpressions.Regex.IsMatch(txtContactNo.Text, "^[0-9]+$")) ? "Contact no accepts only numeric characters. <br />" : "");
                strToReturn += (string.IsNullOrEmpty(txtAppartment.Text)) ? "Please enter appartment. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(txtStreet.Text)) ? "Please enter street. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(txtCity.Text)) ? "Please enter city. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(hidSelectedProvince.Value)) ? "Please select province. <br />" : "";
                strToReturn += (string.IsNullOrEmpty(txtPostalCode.Text)) ? "Please enter postal code. <br />" : "";
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