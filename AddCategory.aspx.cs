using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Add_Category(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            string username = HttpContext.Current.User.Identity.Name;
            
            if (username == "")
            {
                Response.Redirect("Logon.aspx?ReturnUrl=AddCategory.aspx");
            }
            else if (Convert.ToInt32(Session["user_role"]) < 2)
            {
                ErrorLabel.Text = "You must be an editor or more!";
                ErrorLabel.Visible = true;
            }
            else
            {
                string userId = getUserId(username);
                page_name.Text = userId;
                string title = CategoryTitleTextBox.Text;

                using (SqlCommand cmd = new SqlCommand("INSERT INTO [CATEGORY] (user_id,title) VALUES(@user_id,@title)", connection))
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@title", title);

                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        page_name.Text = ex.ToString();
                    }
                    //cmd.ExecuteNonQuery();
                    connection.Close();

                }
                Response.Redirect("Default.aspx");
            }
        }
    }

    private string getUserId(string username)
    {
        string userId = "";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("select id from [USER] where username = @username", connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idColumnIndex = reader.GetOrdinal("id");
                            userId = reader.GetSqlValue(idColumnIndex).ToString();
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    System.Diagnostics.Debug.WriteLine(sqlException.ToString());
                }
            }
        }
        return userId;
    }
}