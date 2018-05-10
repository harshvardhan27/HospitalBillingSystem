using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using DataAccess;


namespace HBS
{
    public partial class Login : System.Web.UI.Page
    {
        #region Common
        BO_Login boLogin = new BO_Login();
        DA_Login dalogin = new DA_Login();
        private string strValidationFailedItem = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateData(out strValidationFailedItem, HBS_Utility.CRUDOperation.Select))
            {

                boLogin.Username = txtUsername.Text;
                boLogin.Password = txtPassword.Text;
                int skey = dalogin.DoLogin(boLogin);
                if (skey > 0)
                {
                    Session["SKEY"] = skey;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    showMessage("Error", "Please provide correct username & password", HBS_Utility.ReturnCode.Failure);
                    Clear();
                    txtUsername.Focus();
                }
            }
        }

        private void Clear()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private bool ValidateData(out string strValidationFailedItem, HBS_Utility.CRUDOperation operation)
        {
            bool ValidationStatus = true;
            string strToReturn = string.Empty;

            if (operation == HBS_Utility.CRUDOperation.Select)
            {
                strToReturn += (string.IsNullOrEmpty(txtUsername.Text)) ? "Please enter username. <br />" : ((!System.Text.RegularExpressions.Regex.IsMatch(txtUsername.Text, "^[a-zA-Z]+$")) ? "Username accepts only alphabetical characters. <br />" : "");
                strToReturn += (string.IsNullOrEmpty(txtPassword.Text)) ? "Please enter password. <br />" : ((!System.Text.RegularExpressions.Regex.IsMatch(txtUsername.Text, "^[a-zA-Z0-9]+$")) ? "Password accepts only alphabetical & numeric characters. <br />" : "");
            }

            ValidationStatus = (string.IsNullOrEmpty(strToReturn.Trim())) ? true : false;
            strValidationFailedItem = strToReturn;
            showMessage("Validation Error", strValidationFailedItem, HBS_Utility.ReturnCode.Failure);

            return ValidationStatus;
        }


        private void showMessage(string errorMessage_Head, string errorMessage_Body, string errorCode)
        {
            errorPanel.Visible = true;
            string successClass = "panel panel-success", errorClass = "panel panel-danger", warningClass = "panel panel-warning", finalClass = "";
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