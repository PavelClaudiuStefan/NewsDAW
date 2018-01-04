using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string loggedUser = HttpContext.Current.User.Identity.Name;
            if (loggedUser != "")
            {
                LoggedUserLink.Text = loggedUser;
                SignOutButton.Visible = true;
                LogonLink.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LoggedUserLink.Text = ex.ToString();
        }
    }

    protected void onSearchButtonClicked(object sender, EventArgs e)
    {
        String text = SearchTextBox.Text;
        Response.Redirect("http://localhost:62643/Search.aspx?q=" + text);
    }

    protected void SignButton_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Default.aspx", true);
    }
}
