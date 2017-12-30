<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Some article - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <div class="article_content">

        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

        <asp:Repeater ID="Repeater" runat="server" DataSourceID="SqlDataSource" OnPreRender="Setup_Articles">
            <ItemTemplate>
                <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
                <asp:HiddenField ID="ThumbnailHiddenField" runat="server" Value='<%# Eval("thumbnail")%>'/>
                <asp:Image ID="ArticleImage" runat="server" CssClass="article_content_image" ImageUrl='<%# "Image.ashx?id=" + Eval("id")%>'/>
                <asp:Label runat="server" ID="TitleLabel" CssClass="article_content_title"
                     Text='<%# Eval("title") %>' />
                <asp:Label runat="server" ID="UserLabel" CssClass="article_content_user"
                     Text='<%# Eval("user_id") %>' />
                <asp:Label runat="server" ID="DateLabel" CssClass="article_content_date"
                     Text='<%# Eval("date_created") %>' />
                <asp:Label runat="server" ID="CategoryLabel" CssClass="article_content_category"
                     Text='<%# Eval("category_id") %>' />
                <asp:Label runat="server" ID="ContentLabel" CssClass="article_content_text"
                     Text='<%# Eval("text") %>' />
            </ItemTemplate>
        </asp:Repeater>

    </div>

</asp:Content>

