<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditArticle.aspx.cs" Inherits="EditArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Add new article</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="page_name" CssClass="page_name" runat="server" Text="Edit article" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <asp:Panel ID="Panel1" runat="server">

        <div class="add_article">
            <!-- Article Title -->
            <asp:Label ID="ArticleTitle" runat="server" CssClass="article_form_label"
                Text="Title:" />
            <asp:TextBox ID="ArticleTitleTextBox" runat="server" CssClass="article_form_textbox" Width="100%"/>
            <asp:RequiredFieldValidator ID="ReqTitleValidator" runat="server" ErrorMessage="*" ControlToValidate="ArticleTitleTextBox" CssClass="req_field_validator"/>

            <!-- Article Category -->
            <asp:Label ID="Category" runat="server" CssClass="article_form_label"
                Text="Category:" />
            <asp:DropDownList ID="DropDownList" runat="server" DataSourceID="SqlDataSource" DataTextField="title" DataValueField="id"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [id], [title] FROM [CATEGORY]"></asp:SqlDataSource>

            <!-- Article Short Description -->
            <asp:Label ID="ArticleDescription" runat="server" CssClass="article_form_label"
                Text="Description:" />
            <asp:TextBox ID="ArticleDescriptionTextBox" runat="server" CssClass="article_form_textbox" TextMode="MultiLine" Width="100%" Height="100"/>
            <asp:RequiredFieldValidator ID="ReqDescriptionValidator" runat="server" ErrorMessage="*" ControlToValidate="ArticleDescriptionTextBox" CssClass="req_field_validator"/>

            <!-- Article Text -->
            <asp:Label ID="ArticleText" runat="server" CssClass="article_form_label"
                Text="Text:" />
            <asp:TextBox ID="ArticleTextTextBox" runat="server" CssClass="article_form_textbox" TextMode="MultiLine" Width="100%" Height="200"/>

            <!-- Article External URL -->
            <asp:Label ID="ArticleExtUrl" runat="server" CssClass="article_form_label"
                Text="External URL:" />
            <asp:TextBox ID="ArticleExtUrlTextBox" runat="server" CssClass="article_form_textbox" Width="100%"/>
            <!-- 
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="You must provide either text or an external URL" ValidateEmptyText="true"
                ControlToValidate="ArticleExtUrlTextBox" OnServerValidate="CustomValidator1_ServerValidate"/>
                -->
            <asp:Label ID="TextOrExtLabel" runat="server" Text="You must provide either text or an external URL" Visible="false"/>

            <!-- Article Image -->
            <asp:Label ID="ArticleImage" runat="server" CssClass="article_form_label"
                Text="Image:" />
            <asp:FileUpload ID="FileUpload" runat="server" />

            <!-- Save and cancel Button -->
            <div class="form_buttons">
                <asp:Button ID="SubmitButton" runat="server" Text="Save" OnClick="Update_Article" CssClass="article_form_button"/>
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" CssClass="article_form_button"/>
            </div>

            <asp:Label ID="ErrorLabel" runat="server" Visible="false" />

        </div>

    </asp:Panel>

</asp:Content>

