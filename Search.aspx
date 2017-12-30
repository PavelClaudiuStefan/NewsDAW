<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Search - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="category_name" CssClass="category_name" runat="server" Text="Search results" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <asp:Repeater ID="Repeater" runat="server" DataSourceID="SqlDataSource" OnPreRender="Setup_Articles">
        <ItemTemplate>
          <div class="article">
            <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
            <asp:HiddenField ID="ThumbnailHiddenField" runat="server" Value='<%# Eval("thumbnail")%>'/>
            <asp:Image ID="ArticleImage" runat="server" CssClass="article_image" ImageUrl='<%# "Image.ashx?id=" + Eval("id")%>'/>
            <asp:HyperLink ID="ArticleHyperLink" runat="server" CssClass="article_title" Target="_blank"
                 Text='<%# Eval("title") %>' NavigateUrl='<%# Eval("ext_url")%>'/>
            <asp:Label runat="server" ID="UserLabel" CssClass="article_user"
                 Text='<%# Eval("user_id") %>' />
            <asp:Label runat="server" ID="DateLabel" CssClass="article_date"
                 Text='<%# Eval("date_created") %>' />
            <asp:Label runat="server" ID="CategoryLabel" CssClass="article_category"
                 Text='<%# Eval("category_id") %>' />
            <asp:Label runat="server" ID="DescriptionLabel" CssClass="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>
 
</asp:Content>

