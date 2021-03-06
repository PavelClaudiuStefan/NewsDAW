﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="category_name" CssClass="page_name" runat="server" Text="Recent" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ARTICLE] ORDER BY [date_created] DESC"></asp:SqlDataSource>
    
    <asp:Repeater ID="Repeater" runat="server" DataSourceID="SqlDataSource" OnPreRender="Setup_Articles">
        <ItemTemplate>
          <div class="article">
            <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
            <asp:HiddenField ID="ThumbnailHiddenField" runat="server" Value='<%# Eval("thumbnail")%>'/>
            <asp:Image ID="ArticleImage" runat="server" CssClass="article_image" ImageUrl='<%# "Image.ashx?article_id=" + Eval("id")%>'/>
            <asp:HyperLink ID="ArticleHyperLink" runat="server" CssClass="article_title" Target="_blank"
                 Text='<%# Eval("title") %>' NavigateUrl='<%# Eval("ext_url")%>'/>
            <asp:HyperLink runat="server" ID="UserLabel" CssClass="article_sub_element_link"
                 Text='<%# Eval("user_id") %>'/>
            <asp:Label runat="server" ID="DateLabel" CssClass="article_sub_element"
                 Text='<%# Eval("date_created") %>' />
            <asp:HyperLink runat="server" ID="CategoryLabel" CssClass="article_sub_element_link"
                 Text='<%# Eval("category_id") %>' NavigateUrl='<%# "Articles.aspx?category_id=" + Eval("category_id") %>'/>
            <asp:Label runat="server" ID="ScoreLabel" CssClass="article_sub_element"
                 Text="999 points" />
            <asp:Label runat="server" ID="DescriptionLabel" CssClass="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>
 
</asp:Content>

