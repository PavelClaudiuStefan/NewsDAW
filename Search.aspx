<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Search - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="category_name" CssClass="category_name" runat="server" Text="Search results" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource">
        <ItemTemplate>
          <div class="article">
            <!-- Imagine -->
            <asp:Image ID="Image1" runat="server" CssClass="article_image"/>
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="article_title"
                 Text='<%# Eval("title") %>' NavigateUrl='<%# "Article.aspx?id=" + Eval("id") %>'/>
            <asp:Label runat="server" ID="Label4" CssClass="article_user"
                 Text='<%# "Posted by: " +  Eval("user_id") %>' />
            <asp:Label runat="server" ID="Label3" CssClass="article_date"
                 Text='<%# "Created on: " + Eval("date_created") %>' />
            <asp:Label runat="server" ID="Label2" CssClass="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>
 
</asp:Content>

