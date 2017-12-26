<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Articles.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Some category - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="category_name" CssClass="category_name" runat="server" Text="Technology" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ARTICLE] WHERE [category_id] = 4 ORDER BY [date_created] DESC"></asp:SqlDataSource>

    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource">
        <ItemTemplate>
          <div class="article">
            <!-- Imagine -->
            <asp:Image ID="Image1" runat="server" class="article_image"
                ImageUrl="https://www.w3schools.com/w3css/img_fjords.jpg" />
            <asp:Label runat="server" ID="Label1" class="article_title"
                 Text='<%# Eval("title") %>' />
            <asp:Label runat="server" ID="Label4" class="article_user"
                 Text='<%# "Posted by: " +  Eval("user_id") %>' />
            <asp:Label runat="server" ID="Label3" class="article_date"
                 Text='<%# "Created on: " + Eval("date_created") %>' />
            <asp:Label runat="server" ID="Label2" class="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>
 
</asp:Content>

