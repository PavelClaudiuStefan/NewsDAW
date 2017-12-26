using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void onSearchButtonClicked(object sender, EventArgs e)
    {
        String text = SearchTextBox.Text;
        Response.Redirect("http://localhost:62643/Search.aspx?q=" + text);
    }
}
