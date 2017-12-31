using System;
using System.Configuration;
using System.Data.SqlClient;
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