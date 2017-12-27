<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Some article - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <div class="article_content">

        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource">
            <ItemTemplate>
                <asp:Image ID="ArticleImage" runat="server" class="article_content_image"/>
                <asp:Label runat="server" ID="TitleLabel" class="article_content_title"
                     Text='<%# Eval("title") %>' />
                <asp:Label runat="server" ID="UserLabel" class="article_content_user"
                     Text='<%# "Posted by: " +  Eval("user_id") %>' />
                <asp:Label runat="server" ID="DateLabel" class="article_content_date"
                     Text='<%# "Created on: " + Eval("date_created") %>' />
                <asp:Label runat="server" ID="CategoryLabel" class="article_content_category"
                     Text='<%# "In the category: " + Eval("category_id") %>' />
                <asp:Label runat="server" ID="ContentLabel" class="article_content_text"
                     Text='<%# Eval("text") %>' />
            </ItemTemplate>
        </asp:Repeater>

    </div>

</asp:Content>

