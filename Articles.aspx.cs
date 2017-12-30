using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string category_id = Request.Params["category_id"];

            SqlCategorySource.SelectCommand = "SELECT * FROM CATEGORY where id = " + category_id;
            //SqlCategorySource.SelectParameters.Add("category_id", category_id);
            SqlCategorySource.DataBind();

            string orderBy = OrderByList.SelectedValue;
            string direction = DirectionList.SelectedValue;

            SqlArticleSource.SelectCommand = "SELECT * FROM ARTICLE where category_id = " + category_id + " ORDER BY " + orderBy + " " + direction;
            //SqlArticleSource.SelectParameters.Add("category_id", category_id);
            SqlArticleSource.DataBind();
        }

    }

    protected void Setup_Articles(object sender, EventArgs e)
    {
        string category_id = Request.Params["category_id"];
        string orderBy = OrderByList.SelectedValue;
        string direction = DirectionList.SelectedValue;

        SqlArticleSource.SelectCommand = "SELECT * FROM ARTICLE where category_id = " + category_id + " ORDER BY " + orderBy + " " + direction;
        //SqlArticleSource.SelectParameters.Add("category_id", category_id);
        SqlArticleSource.DataBind();

        foreach (RepeaterItem repeaterItem in RepeaterArticle.Items)
        {
            //Set local urls if ext_url is null
            HyperLink hyperLink = (HyperLink)repeaterItem.FindControl("ArticleHyperLink");
            if (hyperLink.NavigateUrl == "")
            {
                HiddenField articleIdHiddenField = (HiddenField)repeaterItem.FindControl("ArticleIdHiddenField");
                string articleId = articleIdHiddenField.Value;
                hyperLink.NavigateUrl = "Article.aspx?id=" + articleId;
            }

            //Change user ID to username
            Label userLabel = (Label)repeaterItem.FindControl("UserLabel");
            string userId = userLabel.Text;
            userLabel.Text = getUsername(userId);

            //Hide images if ImageUrl is null
            HiddenField thumbnailData = (HiddenField)repeaterItem.FindControl("ThumbnailHiddenField");
            if (thumbnailData.Value == "")
            {
                Image thumbnail = (Image)repeaterItem.FindControl("ArticleImage");
                thumbnail.Visible = false;
            }
        }
    }

    private string getUsername(string userId)
    {
        string username = "Error";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [USER] WHERE id = @UserId", connection))
            {
                cmd.Parameters.AddWithValue("UserId", userId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        int usernameColumnIndex = reader.GetOrdinal("username");
                        username = reader.GetSqlString(usernameColumnIndex).ToString();
                    }
                }
                catch (SqlException sqlException)
                {
                    username = sqlException.ToString();
                }
            }
        }
        return username;
    }
}