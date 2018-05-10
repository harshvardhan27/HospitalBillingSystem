using System;
using BusinessObject;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAccess
{
    public class DA_Login
    {
        #region Common
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;
        #endregion

        public int DoLogin(BO_Login DataObjectLogin)
        {
            int loggedInUser = 0;
            
            string query = "select * from login where username=@username and password=@password";
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 20, "username");
                cmd.Parameters["@username"].Value = DataObjectLogin.Username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 20, "password");
                cmd.Parameters["@password"].Value = DataObjectLogin.Password;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if ((DataObjectLogin.Username.ToLower() == dr[1].ToString().ToLower()) && (DataObjectLogin.Password.ToLower() == dr[2].ToString().ToLower()))
                    {
                        loggedInUser = GetLoginId(DataObjectLogin);
                    }
                }
                cmd.Dispose();
                con.Close();
            }
            catch (SqlException ex)
            {

            }
            return loggedInUser;
        }

        private int GetLoginId(BO_Login DataObjectLogin)
        {
            int loginId = -1;
            string query = "select login_id from login where username=@username and password=@password";
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 20, "username");
                cmd.Parameters["@username"].Value = DataObjectLogin.Username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 20, "password");
                cmd.Parameters["@password"].Value = DataObjectLogin.Password;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    loginId = Convert.ToInt32(dr[0].ToString());
                }
                cmd.Dispose();
                con.Close();
            }
            catch (SqlException ex) { }
            return loginId;
        }
    }
}
