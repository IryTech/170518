<%@ Page Title="Register" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="IryTech.AdmissionJankariVer20.UI.Account.Register" %>
<%@ Register Src="~/UserControl/Register.ascx" TagPrefix="ADMJ" TagName="Register" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    
    <ADMJ:Register ID="ucRegister" runat="server" />
    

</asp:Content>
