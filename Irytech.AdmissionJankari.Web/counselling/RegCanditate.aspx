<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegCanditate.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.RegCanditate" %>

<%@ Register Src="~/UserControl/UcStudentDirectAdmissionStep1.ascx" TagName="DirectAdmissionStep1" TagPrefix="AJ"%>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">

<div>
<AJ:DirectAdmissionStep1 ID="step1" runat="server" />
</div>
</asp:Content>