<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddArticle.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Add new article</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="category_name" CssClass="page_name" runat="server" Text="Add new article" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <div class="add_article">
        <!-- Article Title -->
        <asp:Label ID="ArticleTitle" runat="server" CssClass="article_form_label"
            Text="Title:" />
        <asp:TextBox ID="ArticleTitleTextBox" runat="server" CssClass="article_form_textbox" Width="100%"/>

        <!-- Article Category -->
        <asp:Label ID="Category" runat="server" CssClass="article_form_label"
            Text="Category:" />
        <asp:DropDownList ID="DropDownList" runat="server" DataSourceID="SqlDataSource" DataTextField="title" DataValueField="id"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [id], [title] FROM [CATEGORY]"></asp:SqlDataSource>

        <!-- Article Short Description -->
        <asp:Label ID="ArticleDescription" runat="server" CssClass="article_form_label"
            Text="Description:" />
        <asp:TextBox ID="ArticleDescriptionTextBox" runat="server" CssClass="article_form_textbox" TextMode="MultiLine" Width="100%" Height="100"/>

        <!-- Article Text -->
        <asp:Label ID="ArticleText" runat="server" CssClass="article_form_label"
            Text="Text:" />
        <asp:TextBox ID="ArticleTextTextBox" runat="server" CssClass="article_form_textbox" TextMode="MultiLine" Width="100%" Height="200"/>

        <!-- Article External URL -->
        <asp:Label ID="ArticleExtUrl" runat="server" CssClass="article_form_label"
            Text="External URL:" />
        <asp:TextBox ID="ArticleExtUrlTextBox" runat="server" CssClass="article_form_textbox" Width="100%"/>

        <!-- Article Image -->
        <asp:Label ID="ArticleImage" runat="server" CssClass="article_form_label"
            Text="Image:" />
        <asp:FileUpload ID="FileUpload" runat="server" />

        <!-- Submit Button -->
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="Add_Article" CssClass="article_form_button"/>

    </div>

</asp:Content>

