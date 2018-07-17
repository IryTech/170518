<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookYourSeat.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.BookYourSeat" %>

<%@ Register Src="~/UserControl/UcBookMySeat.ascx" TagName="BookMySeat" TagPrefix="AJ" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
<AJ:BookMySeat ID="bookSeat" runat="server" />
</asp:Content>