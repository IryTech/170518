<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StreamDecsription.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Course.StreamDecsription" %>
<%@ Register Src="~/UserControl/CourseStreamBasicDetails.ascx"  TagPrefix="AJ" TagName="CourseStreamBasicDetails"%>
<%@ Register Src="~/UserControl/CourseStreamDescription.ascx" TagPrefix="AJ" TagName="CourseStreamDescription" %>
<%@ Register Src="~/UserControl/CourseStreamFuture.ascx" TagPrefix="AJ" TagName="CourseStreamFuture" %>
<%@ Register Src="~/UserControl/CourseStreamCoreCompanies.ascx" TagPrefix="AJ" TagName="CourseStreamCoreCompanies" %>
<%@ Register Src="~/UserControl/CourseStreamHistory.ascx" TagPrefix="AJ" TagName="CourseStreamHistory" %>
<%@ Register Src="~/UserControl/CourseStreamRelatedIndustry.ascx" TagPrefix="AJ" TagName="CourseStreamRelatedIndustry" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">

<div class="five_sixth fleft last">
 <div class="box bgYellow">
            <ul class="vertical">
            <li><asp:ImageMap ID="CollegeImageHeader" runat="server" align="left" Height="100" Width="100" hspace="5" /></li>
            <li><h2><asp:Label ID="lblHeaderCollegeName" runat="server"></asp:Label></h2><br /><h2 class="streamCompareH3 "><label>Admission Helpline : 8800 567 733</label></h2></li>
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
    <a href="#pnlHistory" title="History">History</a>
    </li>
    <li>
    <a href="#pnlFuture" title="Future">Future</a>
    </li>
    <li>
    <a href="#pnlRelatedIndustry" title="RelatedIndustry">Realted Industry</a>
    </li>
    <li>
    <a href="#pnlCoreCompanies" title="CoreCOmpanies">Core Companies</a>
    </li>
    </ul>
    <div class="clearBoth"></div>
  </div>
 <div class="four_fifth last fleft">
  <div id="Overview">
  <AJ:CourseStreamBasicDetails runat="server" ID="usCourseStreamBasicDetails"/>
  </div>
  <div id="pnlDescrip">
  <AJ:CourseStreamDescription runat="server" ID="usCourseStreamDescription"/>
  </div>
  <div id="pnlHistory">
  <AJ:CourseStreamFuture runat="server" ID="usCourseStreamFuture"/>
  </div>
  <div id="pnlFuture">
  <AJ:CourseStreamCoreCompanies runat="server" ID="usCourseStreamCoreCompanies"/>
  </div>
  <div id="pnlRelatedIndustry">
  <AJ:CourseStreamHistory runat="server" ID="usCourseStreamHistory"/>
  </div>
  <div id="pnlCoreCompanies">
  <AJ:CourseStreamRelatedIndustry runat="server" ID="usCourseStreamRelatedIndustry"/>
  </div>
  </div>
    <div class="one_third fright border last" >
  <h2>Streams</h2>
  </div>
</div>
<%--<link rel="stylesheet" type="text/css" href="../AdminPanel/StyleSheets/Admin_style.css" />--%>
</asp:Content>