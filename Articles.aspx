<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Articles.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Some category - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:SqlDataSource ID="SqlCategorySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <asp:Repeater ID="RepeaterCategory" runat="server" DataSourceID="SqlCategorySource">
        <ItemTemplate>
            <asp:Label ID="category_name" CssClass="category_name" runat="server" Text='<%# Eval("title") %>' Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        </ItemTemplate>
    </asp:Repeater>


    <asp:SqlDataSource ID="SqlArticleSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <asp:Repeater ID="RepeaterArticle" runat="server" DataSourceID="SqlArticleSource" OnPreRender="Setup_Articles">
        <ItemTemplate>
          <div class="article">
            <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
            <asp:Image ID="ArticleImage" runat="server" CssClass="article_image"
                ImageUrl="https://www.w3schools.com/w3css/img_fjords.jpg" />
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

