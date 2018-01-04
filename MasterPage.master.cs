using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.IsNewSession)
        {
            Response.Write("new session");
        }
        Session["return_url"] = HttpContext.Current.Request.Url.AbsoluteUri;
        Session["user_role"] = getUserRole();

        try
        {
            string loggedUser = HttpContext.Current.User.Identity.Name;
            if (loggedUser != "")
            {
                if(Convert.ToInt32(Session["user_role"]) > 1)
                {
                    AddArticleLink.Visible = true;
                    AddCategoryLink.Visible = true;
                }
                LoggedUserLink.Text = loggedUser;
                SignOutButton.Visible = true;
                LogonLink.Visible = false;
            }
            else
            {
                LogonLink.NavigateUrl = "~/Logon.aspx?ReturnUrl=" + Session["return_url"].ToString();
            }
        }
        catch (Exception ex)
        {
            LoggedUserLink.Text = ex.ToString();
        }
    }

    protected void onSearchButtonClicked(object sender, EventArgs e)
    {
        String text = SearchTextBox.Text;
        Response.Redirect("Search.aspx?q=" + text);
    }

    protected void SignButton_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect(Session["return_url"].ToString(), true);
    }

    private int getUserRole()
    {
        string loggedUser = HttpContext.Current.User.Identity.Name;
        if (loggedUser == "")
        {
            return 0;
        }
        else
        {
            int role = 0;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT role FROM [USER] WHERE username = @username", connection))
                {
                    cmd.Parameters.AddWithValue("username", loggedUser);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int columnIndex = reader.GetOrdinal("role");
                                role = reader.GetInt32(columnIndex);
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
    }
}
