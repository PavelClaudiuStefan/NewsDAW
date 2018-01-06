<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Article" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title>Some article - Shadow News</title>

</asp:Content>

<asp:Content ID="CategoryContent" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <div class="article_content">

        <asp:SqlDataSource ID="SqlDataSourceArticle" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

        <asp:Repeater ID="RepeaterArticle" runat="server" DataSourceID="SqlDataSourceArticle" OnPreRender="Setup_Articles">
            <ItemTemplate>
                <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>
                <asp:HiddenField ID="ThumbnailHiddenField" runat="server" Value='<%# Eval("thumbnail")%>'/>

                <!-- Vote arrows and score -->
                <div class="vote_arrows">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/upvote.png" CssClass="vote_item" CausesValidation="false" OnClick="Upvote"/>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/downvote.png" CssClass="vote_item" CausesValidation="false" OnClick="Downvote"/>
                    <asp:Label ID="ScoreLabel" runat="server" Text="100" CssClass="vote_item"></asp:Label>
                </div>

                <asp:Image ID="ArticleImage" runat="server" CssClass="article_content_image" ImageUrl='<%# "Image.ashx?article_id=" + Eval("id")%>'/>

                <div class="article_content_header">
                    <asp:Label runat="server" ID="TitleLabel" CssClass="article_content_title"
                         Text='<%# Eval("title") %>' />
                    <asp:Label runat="server" ID="UserLabel" CssClass="article_content_user"
                         Text='<%# Eval("user_id") %>' />
                    <asp:Label runat="server" ID="DateLabel" CssClass="article_content_date"
                         Text='<%# Eval("date_created") %>' />
                    <asp:Label runat="server" ID="CategoryLabel" CssClass="article_content_category"
                         Text='<%# Eval("category_id") %>' />
                </div>

                <asp:Label runat="server" ID="ContentLabel" CssClass="article_content_text"
                     Text='<%# Eval("text") %>' />
            </ItemTemplate>
        </asp:Repeater>

        <div class="comments">

            <asp:Label ID="Label1" runat="server" Text="Comments" CssClass="comment_section_title"></asp:Label>

            <asp:Panel ID="Panel1" runat="server" DefaultButton="PostCommentButton">
                <div class="comment">    
                    <!-- Comment Image -->    
                    <asp:Image ID="PostCommentImage" runat="server" CssClass="comment_image" ImageUrl="~/images/default_user_image.png"/>
                
                    <div class="comment_content">
                        <!-- Comment Header -->
                        <div class="comment_header">
                            <asp:Label ID="LoggedUserLabel" runat="server" Text="Not logged in" CssClass="comment_header_item"></asp:Label>
                        </div>

                        <!-- Comment Text Box -->
                        <asp:TextBox ID="PostCommentTextBox" runat="server" CssClass="comment_text"></asp:TextBox>
                        <asp:Button ID="PostCommentButton" runat="server" Text="Post" CssClass="post_comment_button" OnClick="PostCommentButton_Click"/>
                        <asp:RequiredFieldValidator ID="ReqCommentValidator" runat="server" ErrorMessage="Comment cannot be empty" ControlToValidate="PostCommentTextBox" CssClass="req_field_validator"/>
                    </div>
                </div>
            </asp:Panel>


            <asp:SqlDataSource ID="SqlDataSourceComments" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>

            <asp:Repeater ID="RepeaterComments" runat="server" DataSourceID="SqlDataSourceComments" OnPreRender="Setup_Comments">
                <ItemTemplate>
                    <asp:HiddenField ID="ArticleIdHiddenField" runat="server" Value='<%# Eval("id")%>'/>

                    <div class="comment">
                        <!-- Comment Image -->    
                        <asp:Image ID="CommentImage" runat="server" CssClass="comment_image" ImageUrl="~/images/default_user_image.png"/>
                
                        <div class="comment_content">
                            <!-- Comment Header -->
                            <div class="comment_header">
                                <asp:Label ID="CommentUserLabel" runat="server" Text='<%# Eval("user_id")%>' CssClass="comment_header_item"></asp:Label>
                                <asp:Label ID="CommentDateLabel" runat="server" Text='<%# Eval("date_created")%>' CssClass="comment_header_item"></asp:Label>
                                <asp:Label ID="CommentScoreLabel" runat="server" Text="100 points" CssClass="comment_header_item"></asp:Label>
                            </div>

                            <!-- Comment Text -->
                            <asp:Label ID="CommentTextLabel" runat="server" Text='<%# Eval("text")%>' CssClass="comment_text"/>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


        </div>
    </div>
</asp:Content>

