using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Article : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.Params["id"];
        SqlDataSource.SelectCommand = "SELECT * FROM ARTICLE where id = " + id;
        //SqlDataSource.SelectParameters.Add("id", id);
        SqlDataSource.DataBind();
    }

    protected void Setup_Articles(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            foreach (RepeaterItem repeaterItem in Repeater.Items)
            {

                //Change user ID to username
                Label userLabel = (Label)repeaterItem.FindControl("UserLabel");
                string userId = userLabel.Text;
                userLabel.Text = getUsername(userId);

                //Change category ID to category title
                Label categoryLabel = (Label)repeaterItem.FindControl("CategoryLabel");
                string categoryId = categoryLabel.Text;
                categoryLabel.Text = getCategoryTitle(categoryId);

                //Hide images if ImageUrl is null
                HiddenField thumbnailData = (HiddenField)repeaterItem.FindControl("ThumbnailHiddenField");
                if (thumbnailData.Value == "")
                {
                    Image thumbnail = (Image)repeaterItem.FindControl("ArticleImage");
                    thumbnail.Visible = false;
                }

                //Set article score
                Label scoreLabel = (Label)repeaterItem.FindControl("ScoreLabel");
                string id = Request.Params["id"];
                scoreLabel.Text = getArticleScore(id);
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

    private string getCategoryTitle(string categoryId)
    {
        string categoryTitle = "Error";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [CATEGORY] WHERE id = @CategoryId", connection))
            {
                cmd.Parameters.AddWithValue("CategoryId", categoryId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        int categoryTitleColumnIndex = reader.GetOrdinal("title");
                        categoryTitle = reader.GetSqlString(categoryTitleColumnIndex).ToString();
                    }
                }
                catch (SqlException sqlException) {
                    categoryTitle = sqlException.ToString();
                }
            }
        }
        return categoryTitle;
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

    protected void Upvote(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        SetOrUpdateVote("1");
    }

    protected void Downvote(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        SetOrUpdateVote("0");
    }

    private void SetOrUpdateVote(string type)
    {
        string articleId = Request.Params["id"];
        //TODO - Change from hardcoded user to actual user
        string userId ="1";
        string userVote = getUserVote(articleId, userId);

        if (userVote == "-1")
        {
            insertVote(articleId, userId, type);
        }
        else if (userVote == type)
        {
            deleteVote(articleId, userId);
        }
        else
        {
            updateVote(articleId, userId, type);
        }

        foreach (RepeaterItem repeaterItem in Repeater.Items)
        {
            //Update article score
            Label scoreLabel = (Label)repeaterItem.FindControl("ScoreLabel");
            string id = Request.Params["id"];
            scoreLabel.Text = getArticleScore(id);
        }
    }

    /*
         * -1 - Didn't vote
         *  0 - Downvote
         *  1 - Upvote
         */
    private string getUserVote(string articleId, string userId)
    {
        
        string type = "-1";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("select type from ARTICLE_VOTE where article_id = @article_id and user_id = @user_id", connection))
            {
                cmd.Parameters.AddWithValue("article_id", articleId);
                cmd.Parameters.AddWithValue("user_id", userId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int scoreColumnIndex = reader.GetOrdinal("type");
                            type = reader.GetSqlValue(scoreColumnIndex).ToString();
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    System.Diagnostics.Debug.WriteLine(sqlException.ToString());
                }
            }
        }
        return type;
    }

    private void insertVote(string articleId, string userId, string type)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {            
            using (SqlCommand cmd = new SqlCommand("INSERT INTO ARTICLE_VOTE VALUES(@user_id, @article_id, @type)", connection))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@article_id", articleId);
                cmd.Parameters.AddWithValue("@type", type);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    private void updateVote(string articleId, string userId, string type)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE ARTICLE_VOTE SET type = @type WHERE article_id = @article_id AND user_id = @user_id", connection))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@article_id", articleId);
                cmd.Parameters.AddWithValue("@type", type);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    private void deleteVote(string articleId, string userId)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM ARTICLE_VOTE WHERE article_id = @article_id AND user_id = @user_id", connection))
            {
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@article_id", articleId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}