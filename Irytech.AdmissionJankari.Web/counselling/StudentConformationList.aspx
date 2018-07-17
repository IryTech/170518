<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentConformationList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.StudentConformationList" %>

<%@ Register Src="~/UserControl/UcUserFinalList.ascx" TagPrefix="AJ" TagName="ConfomationList" %>
<%@ Register Src="~/UserControl/UcStudentPersonelInfo.ascx" TagPrefix="AJ" TagName="StudentPersonelInfo" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<div class="four_fifth last fleft border mainBG"> 
 <aj:StudentPersonelInfo id="UcStudentPersonelInfo" runat="server"></aj:StudentPersonelInfo>
  </div>

  <div class="four_fifth last fleft border mainBG marginTop">
    <h3 class="streamCompareH3">
      Student Conformation List  
    </h3>
    <hr class="hrline" />
   
  <AJ:ConfomationList ID="UcConfomationList" runat="server" />
 
  </div>
 
    </asp:content>
