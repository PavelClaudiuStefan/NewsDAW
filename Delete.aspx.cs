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

public partial class Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void YesButton_Click(object sender, EventArgs e)
    {
        string articleId = Request.Params["article_id"];
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM ARTICLE WHERE id = @article_id", connection))
            {
                cmd.Parameters.AddWithValue("@article_id", articleId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        string returnUrl = Session["return_url"].ToString();
        Response.Redirect(returnUrl);
    }

    protected void NoButton_Click(object sender, EventArgs e)
    {
        string articleId = Request.Params["article_id"];
        Response.Redirect("Article.aspx?id=" + articleId);
    }
}