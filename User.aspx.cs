using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = Request.Params["username"];
        SqlUserSource.SelectCommand = "SELECT * FROM [USER] WHERE username = '" + username + "'";
        SqlUserSource.DataBind();

        string userId = getuserId(username);
        SqlArticleSource.SelectCommand = "SELECT * FROM [ARTICLE] where user_id = " + userId;
        SqlArticleSource.DataBind();
    }

    private string getuserId(string username)
    {
        string userId = "0";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [USER] WHERE username = @username", connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        int columnIndex = reader.GetOrdinal("id");
                        int id = reader.GetInt32(columnIndex);
                        userId = id.ToString();
                    }
                }
                catch (SqlException sqlException)
                {
                    Console.WriteLine(sqlException.ToString());
                }
            }
        }
        return userId;
    }

    protected void SetRole(object sender, EventArgs e)
    {
        foreach (RepeaterItem repeaterItem in RepeaterUser.Items)
        {
            Label roleLabel = (Label)repeaterItem.FindControl("RoleLabel");
            string role = roleLabel.Text;

            switch (role)
            {
                case ("1"):
                    roleLabel.Text = "User";
                    break;
                case ("2"):
                    roleLabel.Text = "Editor";
                    break;
                case ("3"):
                    roleLabel.Text = "Admin";
                    break;
                case ("9"):
                    roleLabel.Text = "Owner";
                    break;
                default:
                    roleLabel.Text = "Visitor";
                    break;
            }
        }
    }

    protected void Setup_Articles(object sender, EventArgs e)
    {
        string username = Request.Params["username"];
        foreach (RepeaterItem repeaterItem in RepeaterArticle.Items)
        {
            //Set local urls and score label if ext_url is null
            HyperLink hyperLink = (HyperLink)repeaterItem.FindControl("ArticleHyperLink");
            if (hyperLink.NavigateUrl == "")
            {
                HiddenField articleIdHiddenField = (HiddenField)repeaterItem.FindControl("ArticleIdHiddenField");
                string articleId = articleIdHiddenField.Value;
                hyperLink.NavigateUrl = "Article.aspx?id=" + articleId;

                //Set article score
                Label scoreLabel = (Label)repeaterItem.FindControl("ScoreLabel");
                scoreLabel.Text = getArticleScore(articleId) + " points";
            }
            else
            {
                Label scoreLabel = (Label)repeaterItem.FindControl("ScoreLabel");
                scoreLabel.Visible = false;
            }

            //Change user ID to username
            Label userLabel = (Label)repeaterItem.FindControl("UserLabel");
            string userId = userLabel.Text;
            userLabel.Text = username;

            //Hide images if ImageUrl is null
            HiddenField thumbnailData = (HiddenField)repeaterItem.FindControl("ThumbnailHiddenField");
            if (thumbnailData.Value == "")
            {
                var thumbnail = repeaterItem.FindControl("ArticleImage");
                thumbnail.Visible = false;
            }
        }
    }

    private string getArticleScore(string articleId)
    {
        string score = "0";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("select upvotes - downvotes as points from (select count(*) as upvotes from ARTICLE_VOTE where article_id = @article_id and type = 1) as up, (select count(*) as downvotes from ARTICLE_VOTE where article_id = @article_id and type = 0) as down", connection))
            {
                cmd.Parameters.AddWithValue("article_id", articleId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int scoreColumnIndex = reader.GetOrdinal("points");
                            score = reader.GetSqlValue(scoreColumnIndex).ToString();
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    Console.WriteLine(sqlException.ToString());
                }
            }
        }
        return score;
    }
}