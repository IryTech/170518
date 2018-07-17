<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParticipatedCollegeList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.College.ParticipatedCollegeList" %>
<%@ Register Src="~/UserControl/UcParticipatedCollegeList.ascx" TagPrefix="AJ" TagName="Participating" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">

<AJ:Participating id="ucarticipating" runat="server"></AJ:Participating>

</asp:content>