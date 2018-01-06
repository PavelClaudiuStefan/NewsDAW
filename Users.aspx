<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:label runat="server" text="Users rights" Font-Size="XX-Large" CssClass="page_name"></asp:label>

    <div class="users_container">

        <asp:Label ID="NoRightsLabel" runat="server" Text="You don't have the rights to be here!" ForeColor="Red" CssClass="no_rights_label"></asp:Label>

        <asp:Panel ID="Panel" runat="server" DefaultButton="SaveButton">

            <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
    
            <asp:Repeater ID="Repeater" runat="server" DataSourceID="SqlDataSource" OnPreRender="SetRoles">
                <ItemTemplate>
                  <div class="user">
                    <asp:HyperLink ID="UserHyperLink" runat="server" CssClass="article_title" Target="_blank"
                         Text='<%# Eval("username") %>' NavigateUrl='<%# "User.aspx?username=" +  Eval("username")%>'/>

                    <asp:RadioButtonList ID="RolesList" runat="server">
                        <asp:ListItem Text="Normal user" Value="1" />
                        <asp:ListItem Text="Editor" Value="2" />
                        <asp:ListItem Text="Admin" Value="3"/>
                    </asp:RadioButtonList>
                
                  </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveRoles" Visible="false" CssClass="save_button"/>
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="SetRoles" Visible="false" CssClass="cancel_button"/>

        </asp:Panel>
    </div>

</asp:Content>

