<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineApplicationInstrucation.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.OnlineApplicationInstrucation" %>
<%@ Register Src="~/UserControl/UcOnlineInstrucation.ascx" TagPrefix="ADMJ" TagName="Instrucation" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
<div style="width:942px; margin:5px auto; height:40px; display:block;">
    <img src="/image.axd?Common=instrunction-page.jpg" /></div>
<ADMJ:Instrucation runat="server" id="instrucation"></ADMJ:Instrucation>
</asp:Content>
