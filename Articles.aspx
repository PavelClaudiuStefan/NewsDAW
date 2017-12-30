<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Articles.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Some category - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:SqlDataSource ID="SqlCategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <asp:Repeater ID="RepeaterCategory" runat="server" DataSourceID="SqlCategorySource">
        <ItemTemplate>
            <asp:Label ID="category_name" CssClass="page_name" runat="server" Text='<%# Eval("title") %>' Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        </ItemTemplate>
    </asp:Repeater>

    <div class="sorting">
        <asp:DropDownList ID="OrderByList" runat="server" CssClass="dropdown_list" OnSelectedIndexChanged="Setup_Articles" AutoPostBack="true">
            <asp:ListItem Enabled="true" Text="Order by date" Value="date_created"/>
            <asp:ListItem Text="Order alphabetically" Value="title"/>
        </asp:DropDownList>

        <asp:DropDownList ID="DirectionList" runat="server" CssClass="dropdown_list" OnSelectedIndexChanged="Setup_Articles" AutoPostBack="true">
            <asp:ListItem Enabled="true" Text="Descending" Value="DESC"/>
            <asp:ListItem Text="Ascending" Value="ASC"/>
        </asp:DropDownList>
    </div>

    <asp:SqlDataSource ID="SqlArticleSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <asp:Repeater ID="RepeaterArticle" runat="server" DataSourceID="SqlArticleSource" OnPreRender="Setup_Articles">
        <ItemTemplate>
          <div class="article">
            <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
            <asp:HiddenField ID="ThumbnailHiddenField" runat="server" Value='<%# Eval("thumbnail")%>'/>
            <asp:Image ID="ArticleImage" runat="server" CssClass="article_image"
                ImageUrl='<%# "Image.ashx?id=" + Eval("id")%>'/>
            <asp:HyperLink ID="ArticleHyperLink" runat="server" CssClass="article_title" Target="_blank"
                 Text='<%# Eval("title") %>' NavigateUrl='<%# Eval("ext_url") %>'/>
            <asp:Label runat="server" ID="UserLabel" CssClass="article_user"
                 Text='<%# Eval("user_id") %>' />
            <asp:Label runat="server" ID="DateLabel" CssClass="article_date"
                 Text='<%# Eval("date_created") %>' />
            <asp:Label runat="server" ID="DescriptionLabel" CssClass="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>
 
</asp:Content>

