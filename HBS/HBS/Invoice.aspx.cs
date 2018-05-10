using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HBS
{
    public partial class Invoice : System.Web.UI.Page
    {
        #region Common
        DA_AppointmentDetails daAppointmentDetails = new DA_AppointmentDetails();
        BO_AppointmentDetails boAppointmentDetails = new BO_AppointmentDetails();
        public const decimal HST = .13m;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["billid"].ToString() != null)
            {

            }
            if (Session["SKEY"] != null)
            {
                boAppointmentDetails.SKEY = Convert.ToInt64(Session["SKEY"].ToString());
                int appointmentId = Convert.ToInt32(Request.QueryString["billid"].ToString());
                Populate(appointmentId);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        public void Populate(int appointmentId)
        {
            lblInvoiceNo.Text = "INV-" + appointmentId;
            DataTable dtAppointmentDetails = daAppointmentDetails.GetBillingDetails(appointmentId);
            lblPatientName.Text = dtAppointmentDetails.Rows[0]["PATIENT_NAME"].ToString();
            lblAddress.Text = dtAppointmentDetails.Rows[0]["ADDRESS_LINE_1"].ToString() + " " + dtAppointmentDetails.Rows[0]["ADDRESS_LINE_2"].ToString();
            lblCity.Text = dtAppointmentDetails.Rows[0]["CITY"].ToString();
            lblProvince.Text = dtAppointmentDetails.Rows[0]["PROVIENCE_DESCRIPTION"].ToString();
            lblPostalCode.Text = dtAppointmentDetails.Rows[0]["POSTAL_CODE"].ToString();
            DateTime currentDate = System.DateTime.Now;
            lblInvoiceDate.Text = currentDate.ToString("MMMM dd, yyyy");
            decimal subTotal = 0;
            decimal tax = 0;
            decimal total = 0;
            for (int i = 0; i < dtAppointmentDetails.Rows.Count; i++)
            {
                TableRow row = new TableRow();

                TableCell cell_Description = new TableCell();
                cell_Description.Text = dtAppointmentDetails.Rows[i]["PROCEDURE_DESCRIPTION"].ToString();
                row.Cells.Add(cell_Description);

                TableCell cell_Price = new TableCell();
                decimal unitPrice = Convert.ToDecimal(dtAppointmentDetails.Rows[i]["PROCEDURE_COST"].ToString());
                subTotal += unitPrice;
                cell_Price.Text = unitPrice.ToString("C2");
                row.Cells.Add(cell_Price);

                tblBillDetails.Rows.Add(row);
            }
            lblSubTotal.Text = subTotal.ToString("C2");
            tax = subTotal * HST;
            lblTax.Text = tax.ToString("C2");
            total = subTotal + tax;
            lblTotal.Text = total.ToString("C2");
            string paid = dtAppointmentDetails.Rows[0]["PAID"].ToString().ToUpper();
            if (paid == "N")
            {
                payment.Visible = true;
                paymentNote.Visible = false;
            }
            else
            {
                payment.Visible = false;
                paymentNote.Visible = true;
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["billid"].ToString());
            boAppointmentDetails.Appointmentid = Convert.ToInt32(Request.QueryString["billid"].ToString());
            boAppointmentDetails.Datemodified = System.DateTime.Now;
            boAppointmentDetails.Modifiedby = boAppointmentDetails.SKEY;
            boAppointmentDetails.Paid = "Y";
            daAppointmentDetails.UpdateAppointmentPaymentStatus(boAppointmentDetails);
        }
    }
}