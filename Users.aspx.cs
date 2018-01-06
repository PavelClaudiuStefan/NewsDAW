using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string username = HttpContext.Current.User.Identity.Name;
            int userRole = Convert.ToInt32(Session["user_role"]);

            if (username != "" && userRole >= 3)
            {
                SqlDataSource.SelectCommand = "SELECT * FROM dbo.[USER] WHERE role < (SELECT role FROM dbo.[USER] WHERE username = '" + username + "')";
                //SqlDataSource.SelectCommand = "SELECT * FROM dbo.[USER] WHERE role < (SELECT role FROM dbo.[USER] WHERE username = '@username'");
                SaveButton.Visible = true;
                CancelButton.Visible = true;
                NoRightsLabel.Visible = false;
            }
            else
            {
                SqlDataSource.SelectCommand = "";
            }

            //SqlArticleSource.SelectParameters.Add("username", username);
            SqlDataSource.DataBind();
        }
    }

    protected void SetRoles(object sender, EventArgs e)
    {

        int loggedUserRole = Convert.ToInt32(Session["user_role"]);

        foreach (RepeaterItem repeaterItem in Repeater.Items)
        {
            HyperLink UsernameHyperLink = (HyperLink)repeaterItem.FindControl("UserHyperLink");
            string username = UsernameHyperLink.Text;
            string userRole = getUserRole(username);

            RadioButtonList RolesList = (RadioButtonList)repeaterItem.FindControl("RolesList");
            RolesList.SelectedValue = userRole;

            if (loggedUserRole <= 3)
            {
                RolesList.Items[2].Enabled = false;
            }
        }
    }

    private string getUserRole(string username)
    {
        string role = "0";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT role FROM [USER] WHERE username = @username", connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int columnIndex = reader.GetOrdinal("role");
                            int roleInt = reader.GetInt32(columnIndex);
                            role = roleInt.ToString();
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    System.Diagnostics.Debug.Write(sqlException.ToString());
                }
            }
        }
        return role;
    }

    protected void SaveRoles(object sender, EventArgs e)
    {
        foreach (RepeaterItem repeaterItem in Repeater.Items)
        {
            HyperLink UsernameHyperLink = (HyperLink)repeaterItem.FindControl("UserHyperLink");
            string username = UsernameHyperLink.Text;

            RadioButtonList RolesList = (RadioButtonList)repeaterItem.FindControl("RolesList");
            string userRole = RolesList.SelectedValue;

            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE [USER] SET role = @role WHERE username = @username", connection))
                {
                    cmd.Parameters.AddWithValue("@role", userRole);
                    cmd.Parameters.AddWithValue("@username", username);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}