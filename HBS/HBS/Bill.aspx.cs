using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HBS.Payment
{
    public partial class Bill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SKEY"] != null)
            {
                Int64 SKEY = Convert.ToInt64(Session["SKEY"].ToString());
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        [WebMethod]
        public static string BindDataTable()
        {
            string strToReturn = "";
            strToReturn = DA_Appointment.GetAppointmentDataTable();
            return strToReturn;
        }
    }
}