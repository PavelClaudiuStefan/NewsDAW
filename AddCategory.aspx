<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddCategory.aspx.cs" Inherits="AddCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="page_name" CssClass="page_name" runat="server" Text="Add new category" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <div class="add_article">
        <!-- Category Title -->
        <asp:Label ID="CategoryTitle" runat="server" CssClass="article_form_label"
            Text="Title:" />
        <asp:TextBox ID="CategoryTitleTextBox" runat="server" CssClass="article_form_textbox" Width="100%"/>
        <asp:RequiredFieldValidator ID="ReqUserValidator" runat="server" ErrorMessage="Title must not be empty" ControlToValidate="CategoryTitleTextBox" CssClass="req_field_validator"/>
        <!-- Submit Button -->
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="Add_Category" CssClass="article_form_button"/>
        <asp:Label ID="ErrorLabel" runat="server" Visible="false" />

    </div>

</asp:Content>

