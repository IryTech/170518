<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.Payment" %>

<%@ Register Src="~/UserControl/UcCommonPayment.ascx" TagPrefix="AJ" TagName="Payment" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
 
      <AJ:Payment ID="UcPayment" runat="server" />
   
</asp:Content>
