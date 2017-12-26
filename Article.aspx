<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Some article - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <div class="article_content">

        <asp:Label ID="category_name" CssClass="title" runat="server" Text="Some article" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ARTICLE] WHERE [id] = 1"></asp:SqlDataSource>

        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource">
            <ItemTemplate>
              <div class="article_content">
                <asp:Image ID="Image1" runat="server" class="article_image"/>
                <asp:Label runat="server" ID="Label1" class="article_title"
                     Text='<%# Eval("title") %>' />
                <asp:Label runat="server" ID="Label4" class="article_user"
                     Text='<%# "Posted by: " +  Eval("user_id") %>' />
                <asp:Label runat="server" ID="Label3" class="article_date"
                     Text='<%# "Created on: " + Eval("date_created") %>' />
                <asp:Label runat="server" ID="Label5" class="article_category"
                     Text='<%# "In the category: " + Eval("category_id") %>' />
                <asp:Label runat="server" ID="Label6" class="article_content_text"
                     Text='<%# Eval("text") %>' />
              </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>

</asp:Content>

