<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Home.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="../StyleSheets/login.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="headerTop">
    </div>
    <div class="header">
        <div class="logofield">
            <span class="register"></span></div>
    </div>
    <div class="loginPage">
        <h2>
            <img src="../Images/CommonImages/lock.png" alt="Login" />Login</h2>
        <span class="massage">Welcome back!</span>
         <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success"></asp:Label>
         <asp:Label ID="lblInfo" runat="server" Text="" Visible="false" CssClass="info"></asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error"></asp:Label>
        <div class="logiBox">
            <div class="labelarea">
                <strong>Username</strong><div class="clear">
                </div>
                <small>Or email address</small></div>
            <div class="inputarea">
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUserName" SetFocusOnError="True" runat="server" ControlToValidate="txtUserName" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revUserName" SetFocusOnError="True" runat="server" ControlToValidate="txtUserName" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            <div class="labelarea bottomNoborder">
                <strong>Password</strong><div class="clear">
                </div>
                <small><a href="#">Forgot it?</a></small></div>
            <div class="inputarea noborder ">
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvPassword" SetFocusOnError="True" runat="server" ControlToValidate="txtPassword" Display="Dynamic">
                </asp:RequiredFieldValidator>
                </div>
        </div>
        <div class="clear">
        </div>
        <div class="bottomDiv">
            <asp:Button ID="btnLogin" CssClass="loginButton fright" OnClick="LoginButtonClick" Text="Sign In" runat="server" /></div>
    </div>
    </form>
</body>
</html>
