<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shadow News</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="logon_container">

            <asp:Label ID="PageNameLabel" CssClass="logon_name" runat="server" Text="Logon page" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            
            <div class="logon">

                <asp:TextBox ID="UsernameTextbox" runat="server" CssClass="logon_textbox" placeholder="Username" />
                <asp:RequiredFieldValidator ID="ReqUserValidator" runat="server" ErrorMessage="*" ControlToValidate="UsernameTextbox" CssClass="req_field_validator"/>

                <asp:TextBox ID="PasswordTextbox" runat="server" CssClass="logon_textbox" placeholder="Password" TextMode="Password"></asp:TextBox> 
                <asp:RequiredFieldValidator ID="ReqPwdValidator" runat="server" ErrorMessage="*" ControlToValidate="PasswordTextbox" CssClass="req_field_validator"/>

                <div class="cookie_checkbox">
                    <asp:CheckBox ID="chkPersistCookie" runat="server" autopostback="false"/>
                    <asp:Label ID="PersistentCookieLabel" runat="server" Text="Remember me"/>
                </div>

                <div class="logon_links">
                    <div class="logon_link_container">
                        <asp:Button ID="LoginButton" runat="server" Text="Log in" CssClass="logon_button" OnClick="LoginButton_Click"/>
                    </div>

                    <div class="logon_link_container">
                        <asp:HyperLink ID="RegisterLink" runat="server" Text="Register" NavigateUrl="#" CssClass="logon_register"/>
                    </div>
                </div>

            </div>

        </div>  
    </form>
</body>
</html>


