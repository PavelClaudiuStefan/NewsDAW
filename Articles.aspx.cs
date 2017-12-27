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
        String category_id = Request.Params["category_id"];

        SqlCategorySource.SelectCommand = "SELECT * FROM CATEGORY where id = " + category_id;
        //SqlCategorySource.SelectParameters.Add("category_id", category_id);
        SqlCategorySource.DataBind();

        SqlArticleSource.SelectCommand = "SELECT * FROM ARTICLE where category_id = " + category_id + " ORDER BY date_created DESC";
        //SqlArticleSource.SelectParameters.Add("category_id", category_id);
        SqlArticleSource.DataBind();
    }
}