<%@ Page Title="Profile page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <div class="article_content">

        <asp:SqlDataSource ID="SqlUserSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" />

        <asp:Repeater ID="RepeaterUser" runat="server" DataSourceID="SqlUserSource" OnPreRender="SetRole">
            <ItemTemplate>
                <asp:HiddenField ID="UserIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
                <asp:HiddenField ID="ThumbnailHiddenField" runat="server" Value='<%# Eval("image")%>'/>

                <asp:Label runat="server" ID="NameLabel" CssClass="user_profile_name" Font-Size="XX-Large"
                        Text='<%# Eval("name") %>' />


                <div class="user_profile">
                    <div class="user_profile_left">
                        <asp:Image ID="UserImage" runat="server" CssClass="user_profile_image" ImageUrl='<%# "Image.ashx?user_id=" + Eval("id")%>'/>
                        <asp:Image ID="DefaultImage" runat="server" CssClass="user_profile_image" ImageUrl="~/images/default_user_image.png" Visible="false"/>

                        <div class="user_profile_username">
                            <asp:Label ID="Label1" runat="server" Text="Username:" />
                            <asp:Label runat="server" ID="UsernameLabel" CssClass="user_profile_username"
                                Text='<%# Eval("username") %>' />
                        </div>
                    </div>

                    <div class="user_profile_right">
                        <div class="user_profile_item">
                            <asp:Label ID="Label2" runat="server" Text="E-mail:"></asp:Label>
                            <asp:Label runat="server" ID="EmailLabel" CssClass="user_profile_email"
                            Text='<%# Eval("email") %>' />
                        </div>

                        <div class="user_profile_item">
                            <asp:Label ID="Label3" runat="server" Text="Date created:"></asp:Label>
                            <asp:Label runat="server" ID="DateLabel" CssClass="user_profile_date"
                                Text='<%# Eval("date_created") %>' />
                        </div>

                        <div class="user_profile_item">
                            <asp:Label ID="Label4" runat="server" Text="Role:"></asp:Label>
                            <asp:Label runat="server" ID="RoleLabel" CssClass="user_profile_role"
                                Text='<%# Eval("role") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>



    <asp:SqlDataSource ID="SqlArticleSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

    <asp:Repeater ID="RepeaterArticle" runat="server" DataSourceID="SqlArticleSource" OnPreRender="Setup_Articles">
        <ItemTemplate>
          <div class="article">
            <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
            <asp:HiddenField ID="ThumbnailHiddenField" runat="server" Value='<%# Eval("thumbnail")%>'/>
            <asp:Image ID="ArticleImage" runat="server" CssClass="article_image"
                ImageUrl='<%# "Image.ashx?article_id=" + Eval("id")%>'/>
            <asp:HyperLink ID="ArticleHyperLink" runat="server" CssClass="article_title" Target="_blank"
                 Text='<%# Eval("title") %>' NavigateUrl='<%# Eval("ext_url") %>'/>
            <asp:Label runat="server" ID="UserLabel" CssClass="article_sub_element"
                 Text='<%# Eval("user_id") %>' />
            <asp:Label runat="server" ID="DateLabel" CssClass="article_sub_element"
                 Text='<%# Eval("date_created") %>' />
            <asp:Label runat="server" ID="ScoreLabel" CssClass="article_sub_element"
                 Text="999 points" />
            <asp:Label runat="server" ID="DescriptionLabel" CssClass="article_description"
                 Text='<%# Eval("short_description") %>' />
          </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>

