<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Account.ForgetPassword" %>
<%@ Register Src="~/UserControl/ForgetPassword.ascx" TagPrefix="ADMJ" TagName="ForgetPassword" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div>
    <ADMJ:ForgetPassword ID="ucForgetPassword" runat="server" />
    </div>

</asp:Content>
