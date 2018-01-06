using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

public partial class EditUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = Request.Params["username"];
        UserProfileLink.NavigateUrl = "User.aspx?username=" + username;

        if(!IsPostBack)
        {
            setUserForm();
        }
    }

    protected void UpdateUser(object sender, EventArgs e)
    {
        string nameha;
        //TODO - check if the logged user and the edited user are the same
        bool validValues = true;
        string loggedUsername = HttpContext.Current.User.Identity.Name;
        string username = Request.Params["username"];
        if (loggedUsername != username)
        {
            validValues = false;
        }



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
                string previousPassword = PreviousPasswordTextbox.Text;
                selectCommand = "UPDATE [USER] SET email=@email, name=@name WHERE username=@username";
                if (previousPassword != "")
                {
                    if (previousPassword == getUserPassword())
                    {
                        ErrorLabel.Visible = false;
                        if (PasswordTextbox1.Text == PasswordTextbox2.Text)
                        {
                            if (PasswordTextbox1.Text != "")
                            {
                                selectCommand = "UPDATE [USER] SET password=@password, email=@email, name=@name WHERE username=@username";
                            }
                            else
                            {
                                selectCommand = "UPDATE [USER] SET email=@email, name=@name WHERE username=@username";
                            }
                        }
                        else
                        {
                            ErrorLabel.Text = "Passwords don't match";
                            ErrorLabel.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        ErrorLabel.Text = "Previous password is wrong";
                        ErrorLabel.Visible = true;
                        return;
                    }
                }
                

            }
            else
            {
                string previousPassword = PreviousPasswordTextbox.Text;
                selectCommand = "UPDATE [USER] SET email=@email, name=@name WHERE username=@username";
                if (previousPassword != "")
                {
                    if (previousPassword == getUserPassword())
                    {
                        ErrorLabel.Visible = false;
                        if (PasswordTextbox1.Text == PasswordTextbox2.Text)
                        {
                            if (PasswordTextbox1.Text != "")
                            {
                                selectCommand = "UPDATE [USER] SET password=@password, email=@email, name=@name, image=@image WHERE username=@username";
                            }
                            else
                            {
                                selectCommand = "UPDATE [USER] SET, email=@email, name=@name, image=@image WHERE username=@username";
                            }
                        }
                        else
                        {
                            ErrorLabel.Text = "Passwords don't match";
                            ErrorLabel.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        ErrorLabel.Text = "Previous password is wrong";
                        ErrorLabel.Visible = true;
                        return;
                    }
                }
                
            }

            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                string password = PasswordTextbox1.Text;
                string email = EmailTextbox.Text;
                string name = NameTextbox.Text;

                using (SqlCommand cmd = new SqlCommand(selectCommand, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@name", name);
                    nameha = name;
                    cmd.Parameters.Add("@image", SqlDbType.Binary).Value = bytes;

                    connection.Open();

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }

            }
            Response.Redirect("User.aspx?username=" + username);
        } else
        {
            //User or mail is not unique
            return;
        }
    }

    private void setUserForm()
    {
        string username = Request.Params["username"];
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT name, password, email  FROM [USER] WHERE username = @username", connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(0);
                            NameTextbox.Text = name;
                            string password = reader.GetString(1);
                            PasswordTextbox1.Text = password;
                            PasswordTextbox2.Text = password;
                            string email = reader.GetString(2);
                            EmailTextbox.Text = email;

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

    private string getUserPassword()
    {
        string username = Request.Params["username"];
        string password = "";
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT password FROM [USER] WHERE username = @username", connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            password = reader.GetString(0);
                        }
                    }
                }
                catch (SqlException sqlException)
                {
                    Console.WriteLine(sqlException.ToString());
                }
            }
        }
        return password;
    }
}