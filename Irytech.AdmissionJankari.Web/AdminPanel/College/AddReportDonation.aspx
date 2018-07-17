<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddReportDonation.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.AddReportDonation" %>
 <%@ Register Src="~/UserControl/UcReportDonation.ascx" TagName="ReportDonation" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<fieldset>
            <legend>Insert</legend>
            <ul>
                <li>
                   <Aj:ReportDonation ID="reportDonation" runat="server" />
                </li>
                
          
            </ul>
        </fieldset>
</asp:Content>
