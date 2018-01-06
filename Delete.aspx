<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="Delete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shadow News</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="delete_container">
            <asp:Label ID="DeleteLabel" runat="server" Text="Do you really want to delete it?" CssClass="delete_label"></asp:Label>

            <div class="buttons">
                <asp:Button ID="YesButton" runat="server" Text="Yes" CssClass="delete_button" OnClick="YesButton_Click"/>

                <asp:Button ID="NoButton" runat="server" Text="No" CssClass="delete_button" OnClick="NoButton_Click"/>
            </div>
        </div>
    </form>
</body>
</html>


