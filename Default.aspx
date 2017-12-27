<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="category_name" CssClass="category_name" runat="server" Text="Recent" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ARTICLE] ORDER BY [category_id], [date_created] DESC"></asp:SqlDataSource>

    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource">
        <ItemTemplate>
          <div class="article">
            <asp:Image ID="Image1" runat="server" class="article_image"/>
            <asp:HyperLink ID="HyperLink1" runat="server" class="article_title"
                 Text='<%# Eval("title") %>' NavigateUrl='<%# "Article.aspx?id=" + Eval("id") %>'/>
            <asp:Label runat="server" ID="Label4" class="article_user"
                 Text='<%# "Posted by: " +  Eval("user_id") %>' />
            <asp:Label runat="server" ID="Label3" class="article_date"
                 Text='<%# Eval("date_created") %>' />
            <asp:Label runat="server" ID="Label5" class="article_category"
                 Text='<%# "In the category: " + Eval("category_id") %>' />
            <asp:Label runat="server" ID="Label2" class="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>
 
</asp:Content>

