<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalUserIntertestedList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.counselling.FinalUserIntertestedList" %>
<%@ Register Src="~/UserControl/UcFinalIntertestedCollegeList.ascx" TagPrefix="AJ" TagName="CollegeList" %>
<%@ Register Src="~/UserControl/UcStudentPersonelInfo.ascx" TagPrefix="AJ" TagName="StudentPersonelInfo" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
 
     
<div class="boxPlane four_fifth last fleft border mainBG marginbottom"> 
 <aj:StudentPersonelInfo id="UcStudentPersonelInfo" runat="server"></aj:StudentPersonelInfo>
   </div>
  <div class="boxPlane four_fifth last fleft">
  <h2 >
       Final Interested College List
    </h2>
    <hr class="hrline" />
    
   
  <AJ:CollegeList ID="collegeSearch" runat="server" /> 
  </div>
    </asp:Content>