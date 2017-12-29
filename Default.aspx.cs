using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void Repeater_Load(object sender, EventArgs e)
    {
        foreach (RepeaterItem repeaterItem in Repeater.Items)
        {
            HyperLink hyperLink = (HyperLink)repeaterItem.FindControl("ArticleHyperLink");
            if (hyperLink.NavigateUrl == "")
            {
                HiddenField articleIdHiddenField = (HiddenField)repeaterItem.FindControl("ArticleIdHiddenField");
                string articleId = articleIdHiddenField.Value;
                hyperLink.NavigateUrl = "Article.aspx?id=" + articleId;
            }
        }
    }
}