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

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RegisterUser(object sender, EventArgs e)
    {
        //check if the user and mail have been used already
        bool validValues = true;



        if (validValues)
        {
            // Read the file and convert it to Byte Array
            string filePath = FileUpload.PostedFile.FileName;
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);

            Stream fs = FileUpload.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);

            string selectCommand;

            if (bytes.Length == 0)
            {
                selectCommand = "INSERT INTO [USER] (username, password, email, name) VALUES(@username, @password, @email, @name)";
            }
            else
            {
                selectCommand = "INSERT INTO [USER] (username, password, email, name, image) VALUES(@username, @password, @email, @name, @image)";
            }

            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string username = UsernameTextbox.Text;
                string password = PasswordTextbox1.Text;
                string email = EmailTextbox.Text;
                string name = NameTextbox.Text;

                using (SqlCommand cmd = new SqlCommand(selectCommand, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.Add("@image", SqlDbType.Binary).Value = bytes;

                    connection.Open();

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }

            }
            Response.Redirect("Default.aspx");
        }
    }

}