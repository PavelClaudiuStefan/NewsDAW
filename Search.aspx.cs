using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String text = Request.Params["q"];
        SqlDataSource.SelectCommand = "SELECT * FROM ARTICLE where title like'%" + text + "%' ORDER BY date_created DESC";
        //SqlDataSource.SelectParameters.Add("text", text);
        SqlDataSource.DataBind();
    }
}