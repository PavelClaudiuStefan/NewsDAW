﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String queryText = Request.Params["q"];
        SqlDataSource.SelectCommand = "SELECT * FROM ARTICLE where title like '%" + queryText + "%' ORDER BY date_created DESC";
        //SqlDataSource.SelectCommand = "SELECT * FROM ARTICLE where title like '%@query_text%' ORDER BY date_created DESC";

        //SqlDataSource.SelectParameters["query_text"].DefaultValue = queryText;
        //PageNameLabel.Text = SqlDataSource.SelectParameters["query_text"].DefaultValue;

        SqlDataSource.DataBind();
    }

    protected void Setup_Articles(object sender, EventArgs e)
    {
        foreach (RepeaterItem repeaterItem in Repeater.Items)
        {
            //Set local urls if ext_url is null
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
                        if (reader.Read())
                        {
                            int usernameColumnIndex = reader.GetOrdinal("username");
                            username = reader.GetSqlString(usernameColumnIndex).ToString();
                        }
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
                        if (reader.Read())
                        {
                            int categoryTitleColumnIndex = reader.GetOrdinal("title");
                            categoryTitle = reader.GetSqlString(categoryTitleColumnIndex).ToString();
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    Console.WriteLine(sqlException.ToString());
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
}