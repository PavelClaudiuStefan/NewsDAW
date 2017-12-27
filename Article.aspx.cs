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
        String id = Request.Params["id"];
        SqlDataSource.SelectCommand = "SELECT * FROM ARTICLE where id = " + id;
        //SqlDataSource.SelectParameters.Add("id", id);
        SqlDataSource.DataBind();
    }
}