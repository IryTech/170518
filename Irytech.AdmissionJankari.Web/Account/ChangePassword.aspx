<%@ Page Title="Change Password" Language="C#"  AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="IryTech.AdmissionJankariVer20.UI.Account.ChangePassword" %>
    <%@ Register Src="~/UserControl/UcChangePassword.ascx" TagPrefix="ADMJ" TagName="ChangePassword" %>

    <asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div>
    <ADMJ:ChangePassword ID="ucChangePassword" runat="server" />
    </div>

</asp:Content>