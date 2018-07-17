<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversityDescription.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Course.UniversityDescription" %>
<%@ Register Src="~/UserControl/UniversityBasicDetails.ascx" TagPrefix="AJ" TagName="UniversityBasicDetails" %>
<%@ Register Src="~/UserControl/UniversityContactDetails.ascx" TagPrefix="AJ" TagName="UniversityContactDetails" %>
<%@ Register Src="~/UserControl/UniversityDescription.ascx" TagPrefix="AJ" TagName="UniversityDescription" %>
<%@ Register Src="~/UserControl/UcCollegeListRelatedUniversity.ascx" TagPrefix="AJ" TagName="UniversityRealtedCollege" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">

<div class="five_sixth fleft last">

 <div class="box bgYellow">
            <ul class="vertical">
            <li><asp:ImageMap ID="CollegeImageHeader" runat="server" align="left" Height="100" Width="100" hspace="5" /></li>
          </ul>
            <div class="clearBoth"></div>
        </div>

  <div class="pageTargetMenu">
    <ul class="vertical">
    <li>
    <a href="#Overview" title="Overview">Overview</a>
    </li>
    <li>
    <a href="#pnlDescrip" title="Description">Description</a>
    </li>
    <li>
    <a href="#pnlContactDetails" title="ContactDetails">Contact Details</a>
    </li>
    </ul>
    <div class="clearBoth"></div>
  </div>
  
  <div class="four_fifth last fleft">
  <div id="Overview">
  <AJ:UniversityBasicDetails runat="server" ID="usUniversityBasicDetails"/>
  </div>
  <div id="pnlDescrip">
  <AJ:UniversityDescription runat="server" ID="usUniversityDescription"/>
  </div>
  <div id="pnlContactDetails">
  <AJ:UniversityContactDetails runat="server" ID="usUniversityContactDetails"/>
  </div>
  </div>
  <div class="one_third fright last" >
   <AJ:UniversityRealtedCollege runat="server" ID="ucUniversityRealtedCollege"/>
  </div>
</div>



</asp:Content>