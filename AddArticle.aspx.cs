﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Add_Article(object sender, EventArgs e)
    {

        string username = HttpContext.Current.User.Identity.Name;

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
                selectCommand = "INSERT INTO ARTICLE (user_id, category_id, title, text, short_description, ext_url) VALUES(@user_id, @category_id, @title, @text, @short_description, @ext_url)";
            }
            else
            {
                selectCommand = "INSERT INTO ARTICLE (user_id, category_id, title, text, short_description, ext_url, thumbnail) VALUES(@user_id, @category_id, @title, @text, @short_description, @ext_url, @thumbnail)";
            }

            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                int userId = 1;
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
}