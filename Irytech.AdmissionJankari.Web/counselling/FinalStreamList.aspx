<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalStreamList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.FinalStreamList" %>

<%@ Register Src="~/UserControl/UcStudentUpdateStreamPrioty.ascx" TagPrefix="AJ" TagName="StreamList" %>
<%@ Register Src="~/UserControl/UcStudentPersonelInfo.ascx" TagPrefix="AJ" TagName="StudentPersonelInfo" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
<div class="boxPlane four_fifth last fleft border mainBG marginbottom"> 
 <aj:StudentPersonelInfo id="UcStudentPersonelInfo" runat="server"></aj:StudentPersonelInfo>
  </div>
    <h2 class="clearBoth">
       Your college wish list
    </h2>
  <AJ:StreamList ID="UcStreamList" runat="server" />
  
    </asp:Content>