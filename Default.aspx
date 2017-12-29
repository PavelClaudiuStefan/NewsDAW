<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:Label ID="category_name" CssClass="category_name" runat="server" Text="Recent" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ARTICLE] ORDER BY [category_id], [date_created] DESC"></asp:SqlDataSource>
    
    <asp:Repeater ID="Repeater" runat="server" DataSourceID="SqlDataSource" OnPreRender="Repeater_Load" >
        <ItemTemplate>
          <div class="article">
            <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
            <asp:Image ID="Image1" runat="server" CssClass="article_image"/>
            <asp:HyperLink ID="ArticleHyperLink" runat="server" CssClass="article_title" Target="_blank"
                 Text='<%# Eval("title") %>' NavigateUrl='<%# Eval("ext_url")%>'/>
            <asp:Label runat="server" ID="Label4" CssClass="article_user"
                 Text='<%# "Posted by: " +  Eval("user_id") %>' />
            <asp:Label runat="server" ID="Label3" CssClass="article_date"
                 Text='<%# Eval("date_created") %>' />
            <asp:Label runat="server" ID="Label5" CssClass="article_category"
                 Text='<%# "In the category: " + Eval("category_id") %>' />
            <asp:Label runat="server" ID="Label2" CssClass="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>
 
</asp:Content>

