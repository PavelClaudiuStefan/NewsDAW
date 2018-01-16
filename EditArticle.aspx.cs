using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditArticle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setUserForm();
        }
    }

    protected void Update_Article(object sender, EventArgs e)
    {
        string username = HttpContext.Current.User.Identity.Name;
        string poster = GetPosterUsername();

        if (poster != username)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "You should not be here";
            return;
        }

        if (string.IsNullOrEmpty(ArticleTextTextBox.Text) && string.IsNullOrEmpty(ArticleExtUrlTextBox.Text))
        {
            TextOrExtLabel.Visible = true;
            return;
        }

        if (username == "")
        {
            Response.Redirect("Logon.aspx?ReturnUrl=AddArticle.aspx");
        }
        else if (Convert.ToInt32(Session["user_role"]) < 2)
        {
            ErrorLabel.Text = "You must be an editor or more!";
            ErrorLabel.Visible = true;
        }
        else
        {
            // Read the file and convert it to Byte Array
            string filePath = FileUpload.PostedFile.FileName;
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);

            Stream fs = FileUpload.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);

            string selectCommand;

            if( bytes.Length == 0)
            {
                selectCommand = "UPDATE ARTICLE SET user_id=@user_id, category_id=@category_id, title=@title, text=@text, short_description=@short_description, ext_url=@ext_url WHERE id=";
            }
            else
            {
                selectCommand = "UPDATE ARTICLE SET user_id=@user_id, category_id=@category_id, title=@title, text=@text, short_description=@short_description, ext_url=@ext_url, thumbnail=@thumbnail WHERE id=";
            }

            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string userId = getUserId(username);
                string title = ArticleTitleTextBox.Text;
                string categoryId = DropDownList.SelectedValue;
                string description = ArticleDescriptionTextBox.Text;
                string text = ArticleTextTextBox.Text;
                string extUrl = ArticleExtUrlTextBox.Text;

                using (SqlCommand cmd = new SqlCommand(selectCommand, connection))
                {
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@category_id", categoryId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@short_description", description);
                    cmd.Parameters.AddWithValue("@text", text);
                    cmd.Parameters.AddWithValue("@ext_url", extUrl);
                    cmd.Parameters.Add("@thumbnail", SqlDbType.Binary).Value = bytes;

                    connection.Open();

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }

            }
            Response.Redirect("Default.aspx");
        }
    }

    private string getUserId(string username)
    {
        string userId = "0";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT id FROM [USER] WHERE username = @username", connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int columnIndex = reader.GetOrdinal("id");
                            int roleInt = reader.GetInt32(columnIndex);
                            userId = roleInt.ToString();
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    System.Diagnostics.Debug.Write(sqlException.ToString());
                }
            }
        }
        return userId;
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (string.IsNullOrEmpty(ArticleTextTextBox.Text) && string.IsNullOrEmpty(ArticleExtUrlTextBox.Text))
        {
            args.IsValid = false;
        }
    }

    private void setUserForm()
    {
        string articleId = Request.Params["article_id"];
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT title, category_id, short_description, text, ext_url, thumbnail  FROM ARTICLE WHERE id = @article_id", connection))
            {
                cmd.Parameters.AddWithValue("article_id", articleId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string title = reader.GetString(0);
                            ArticleTitleTextBox.Text = title;

                            int categoryId = reader.GetInt32(1);
                            DropDownList.SelectedValue = categoryId.ToString();

                            string description = reader.GetString(2);
                            ArticleDescriptionTextBox.Text = description;

                            if (!reader.IsDBNull(3))
                            {
                                string text = reader.GetString(3);
                                ArticleTextTextBox.Text = text;
                            }

                            if (!reader.IsDBNull(4))
                            {
                                string extUrl = reader.GetString(4);
                                ArticleExtUrlTextBox.Text = extUrl;
                            }

                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    Console.WriteLine(sqlException.ToString());
                }
            }
        }
    }

    private string GetPosterUsername()
    {
        string articleId = Request.Params["article_id"];
        string username = "";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT username FROM [USER] WHERE id = (SELECT user_id FROM ARTICLE WHERE id=@article_id)", connection))
            {
                cmd.Parameters.AddWithValue("article_id", articleId);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username = reader.GetString(0);
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    System.Diagnostics.Debug.Write(sqlException.ToString());
                }
            }
        }
        return username;
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        string articleId = Request.Params["article_id"];
        Response.Redirect("Article.aspx?id=" + articleId);
    }
}