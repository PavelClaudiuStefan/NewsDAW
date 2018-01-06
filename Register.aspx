<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shadow News</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:Label ID="PageNameLabel" CssClass="register_page_name" runat="server" Text="Register page" Font-Bold="True" Font-Size="XX-Large"></asp:Label>

        <div class="register_page">

            <div class="register_container">
                    
                <div class="register_line">
                    <div class="register_line_left">
                        <asp:Label ID="UsernameLabel" runat="server" Text="Username:" CssClass="register_line_left_label"></asp:Label>
                    </div>

                    <div class="register_line_right">
                        <asp:TextBox ID="UsernameTextbox" runat="server" CssClass="register_line_right_textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="UsernameTextbox" CssClass="register_line_right_validator"/>
                        <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="UsernameTextbox" ErrorMessage="Spaces are not allowed!" ValidationExpression="[^\s]+" CssClass="register_line_right_validator"/>
                    </div>
                </div>

                <div class="register_line">
                    <div class="register_line_left">
                        <asp:Label ID="NameLabel" runat="server" Text="Name:" CssClass="register_line_left_label"></asp:Label>
                    </div>

                    <div class="register_line_right">
                        <asp:TextBox ID="NameTextbox" runat="server" CssClass="register_line_right_textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="NameTextbox" CssClass="register_line_right_validator"/>
                    </div>
                </div>

                <div class="register_line">
                    <div class="register_line_left">
                        <asp:Label ID="PasswordLabel1" runat="server" Text="Password:" CssClass="register_line_left_label"></asp:Label>
                    </div>

                    <div class="register_line_right">
                        <asp:TextBox ID="PasswordTextbox1" runat="server" CssClass="register_line_right_textbox" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="PasswordTextbox1" CssClass="register_line_right_validator"/>
                    </div>
                </div>

                <div class="register_line">
                    <div class="register_line_left">
                        <asp:Label ID="PasswordLabel2" runat="server" Text="Password confirmation:" CssClass="register_line_left_label"></asp:Label>
                    </div>

                    <div class="register_line_right">
                        <asp:TextBox ID="PasswordTextbox2" runat="server" CssClass="register_line_right_textbox" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="PasswordTextbox2" CssClass="register_line_right_validator"/>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords don't match" ControlToCompare="PasswordTextbox1" ControlToValidate="PasswordTextbox2"  CssClass="register_line_right_validator"/>
                    </div>
                </div>

                <div class="register_line">
                    <div class="register_line_left">
                        <asp:Label ID="EmailLabel" runat="server" Text="Email:" CssClass="register_line_left_label"></asp:Label>
                    </div>

                    <div class="register_line_right">
                        <asp:TextBox ID="EmailTextbox" runat="server" CssClass="register_line_right_textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="EmailTextbox" CssClass="register_line_right_validator"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Not a valid mail" ControlToValidate="EmailTextbox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  CssClass="register_line_right_validator"/>
                    </div>
                </div>

                <div class="register_line">
                    <div class="register_line_left">
                        <asp:Label ID="ImageLabel" runat="server" Text="Image (Optional):" CssClass="register_line_left_label"></asp:Label>
                    </div>

                    <div class="register_line_right">
                        <asp:FileUpload ID="FileUpload" runat="server" CssClass="register_line_right_textbox"/>
                    </div>
                </div>

                <div class="register_line">
                    <asp:Button ID="RegisterButton" runat="server" Text="Register" CssClass="register_button" OnClick="RegisterUser"/>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
