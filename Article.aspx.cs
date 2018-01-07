using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

public partial class Article : System.Web.UI.Page
{
    string loggedUser = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string articleId = Request.Params["id"];
            SqlDataSourceArticle.SelectCommand = "SELECT * FROM ARTICLE where id = " + articleId;
            //SqlDataSourceArticle.SelectParameters.Add("id", id);
            SqlDataSourceArticle.DataBind();

            SqlDataSourceComments.SelectCommand = "SELECT * FROM COMMENT where article_id = " + articleId + " AND  parent_id = 0"; //parent_id = 0 -> Comment is article child
                                                                                                                            //SqlDataSourceComments.SelectParameters.Add("id", id);
            SqlDataSourceComments.DataBind();

            try
            {
                loggedUser = HttpContext.Current.User.Identity.Name;
                if (loggedUser != "")
                {
                    LoggedUserLabel.Text = loggedUser;
                }
            }
            catch (Exception ex)
            {
                LoggedUserLabel.Text = ex.ToString();
            }
        }
    }

    protected void Setup_Articles(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            foreach (RepeaterItem repeaterItem in RepeaterArticle.Items)
            {

                //Change user ID to username and add link to profile page
                HyperLink userLabel = (HyperLink)repeaterItem.FindControl("UserLabel");
                string userId = userLabel.Text;
                string username = getUsername(userId);
                userLabel.Text = username;
                userLabel.NavigateUrl = "User.aspx?username=" + username;

                //Change category ID to category title
                HyperLink categoryLabel = (HyperLink)repeaterItem.FindControl("CategoryLabel");
                string categoryId = categoryLabel.Text;
                categoryLabel.Text = getCategoryTitle(categoryId);

                //Hide images if ImageUrl is null
                HiddenField thumbnailData = (HiddenField)repeaterItem.FindControl("ThumbnailHiddenField");
                if (thumbnailData.Value == "")
                {
                    var thumbnail = repeaterItem.FindControl("ArticleImage");
                    thumbnail.Visible = false;
                }

                //Set article score
                Label scoreLabel = (Label)repeaterItem.FindControl("ScoreLabel");
                string id = Request.Params["id"];
                scoreLabel.Text = getArticleScore(id);

                //Set upvote/downvote arrows
                string loggedUserVote = getUserVote(id, userId);
                if (loggedUserVote == "-1")
                {
                    //Do nothing
                } else if (loggedUserVote == "0")
                {
                    //Downvote arrow -> selected
                    ImageButton downVote = (ImageButton)repeaterItem.FindControl("ImageButton2");
                    downVote.ImageUrl = "~/images/downvote_selected.png";
                } else
                {
                    //Upvote arrow -> selected
                    ImageButton upvoteArrow = (ImageButton)repeaterItem.FindControl("ImageButton1");
                    upvoteArrow.ImageUrl = "~/images/upvote_selected.png";
                }

                //Show edit and delete if user is valid
                string loggedUser = HttpContext.Current.User.Identity.Name;
                if (loggedUser == username)
                {
                    HyperLink deleteLink = (HyperLink)repeaterItem.FindControl("DeleteLink");
                    deleteLink.Visible = true;

                    HyperLink editLink = (HyperLink)repeaterItem.FindControl("EditLink");
                    editLink.Visible = true;
                }
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
        loggedUser = HttpContext.Current.User.Identity.Name;
        if (loggedUser == "")
        {
            Response.Redirect("Logon.aspx?ReturnUrl=" + Session["return_url"].ToString());
        }
        else
        {
            string userId = getUserId(loggedUser);
            SetOrUpdateVote(userId, "1");
        }        
    }

    protected void Downvote(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        loggedUser = HttpContext.Current.User.Identity.Name;
        if (loggedUser == "")
        {
            Response.Redirect("Logon.aspx?ReturnUrl=" + Session["return_url"].ToString());
        }
        else
        {
            string userId = getUserId(loggedUser);
            SetOrUpdateVote(userId, "0");
        }
    }

    private void SetOrUpdateVote(string userId, string type)
    {
        string articleId = Request.Params["id"];
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

        foreach (RepeaterItem repeaterItem in RepeaterArticle.Items)
        {
            //Update article score
            Label scoreLabel = (Label)repeaterItem.FindControl("ScoreLabel");
            string id = Request.Params["id"];
            scoreLabel.Text = getArticleScore(id);

            //Set upvote/downvote arrows
            string loggedUserVote = getUserVote(id, userId);
            if (loggedUserVote == "-1")
            {
                //Downvote || Upvote arrow -> Not selected
                ImageButton downVote = (ImageButton)repeaterItem.FindControl("ImageButton2");
                downVote.ImageUrl = "~/images/downvote.png";

                ImageButton upvoteArrow = (ImageButton)repeaterItem.FindControl("ImageButton1");
                upvoteArrow.ImageUrl = "~/images/upvote.png";
            }
            else if (loggedUserVote == "0")
            {
                //Downvote arrow -> Selected
                ImageButton downVote = (ImageButton)repeaterItem.FindControl("ImageButton2");
                downVote.ImageUrl = "~/images/downvote_selected.png";

                //Upvote arrow -> Not selected
                ImageButton upvoteArrow = (ImageButton)repeaterItem.FindControl("ImageButton1");
                upvoteArrow.ImageUrl = "~/images/upvote.png";
            }
            else
            {
                //Downvote arrow -> Not selected
                ImageButton downVote = (ImageButton)repeaterItem.FindControl("ImageButton2");
                downVote.ImageUrl = "~/images/downvote.png";

                //Upvote arrow -> Selected
                ImageButton upvoteArrow = (ImageButton)repeaterItem.FindControl("ImageButton1");
                upvoteArrow.ImageUrl = "~/images/upvote_selected.png";
            }
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
            using (SqlCommand cmd = new SqlCommand("INSERT INTO ARTICLE_VOTE (user_id, article_id, type) VALUES(@user_id, @article_id, @type)", connection))
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

    protected void Setup_Comments(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            foreach (RepeaterItem repeaterItem in RepeaterComments.Items)
            {

                //Change user ID to username
                Label userLabel = (Label)repeaterItem.FindControl("CommentUserLabel");
                string userId = userLabel.Text;
                userLabel.Text = getUsername(userId);

                /*
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
                */
            }
        }
    }

    protected void PostCommentButton_Click(object sender, EventArgs e)
    {
        string articleId = Request.Params["id"];
        loggedUser = HttpContext.Current.User.Identity.Name;
        if (loggedUser == "")
        {
            Response.Redirect("Logon.aspx?ReturnUrl=" + Session["return_url"].ToString());
        }
        else
        {
            string userId = getUserId(loggedUser);
            string commentText = PostCommentTextBox.Text;
            PostCommentTextBox.Text = "";
            if (commentText != "")
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO COMMENT (article_id, user_id, text) VALUES(@article_id, @user_id, @text)", connection))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@article_id", articleId);
                        cmd.Parameters.AddWithValue("@text", commentText);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }

                //TODO - Better way to refresh comments
                Response.Redirect("Article.aspx?id=" + articleId);
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