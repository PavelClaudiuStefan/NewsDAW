using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;
using System.Configuration;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        if (ValidateUser(UsernameTextbox.Text, PasswordTextbox.Text))
            FormsAuthentication.RedirectFromLoginPage(UsernameTextbox.Text,
            chkPersistCookie.Checked);
        else
        { 
            //Response.Redirect("logon.aspx", true);
            LoginFailedLabel.Visible = true;
        }
    }

    private bool ValidateUser(string userName, string passWord)
    {
        string lookupPassword = null;
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("Select password from [USER] where username=@userName", connection))
            {
                cmd.Parameters.AddWithValue("userName", userName);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int passwordColumnIndex = reader.GetOrdinal("password");
                            lookupPassword = reader.GetSqlString(passwordColumnIndex).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
                }
            }
        }

        if (null == lookupPassword)
        {
            // You could write failed login attempts here to event log for additional security.
            return false;
        }

        // Compare lookupPassword and input passWord, using a case-sensitive comparison.
        return (0 == string.Compare(lookupPassword, passWord, false));

    }
}