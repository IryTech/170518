<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" EnableSessionState="True" CodeBehind="Default.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web._Default" %>

<%@ Register Src="~/UserControl/CollegeSearch.ascx" TagPrefix="AJ" TagName="CollegeSearch" %>
<%@ Register Src="~/UserControl/TopRankedColleges.ascx" TagPrefix="AJ" TagName="ToRankedCollege" %>
<%@ Register Src="~/UserControl/BestPrivateCollegesList.ascx" TagPrefix="AJ" TagName="BestPrivateCollege" %>
<%@ Register Src="~/UserControl/ucLatestNews.ascx" TagPrefix="AJ" TagName="LatestNews" %>
<%@ Register Src="~/UserControl/CommonQuickQuery.ascx" TagPrefix="AJ" TagName="Query" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">

 <script type="text/javascript" src="/Js/jquery.tipTip.js"></script>
 <link rel="stylesheet" type="text/css" href="/Styles/tipTip.css" />

 <div class="three_fourth fleft last" style="width:76.5% !important">
 <div>
    <AJ:CollegeSearch ID="ucCollegeName" runat="server" />
 </div >
 <div class="one_half fleft marginTop  border bgblue" id="divTopRanked" ><AJ:ToRankedCollege ID="ucToRankedCollege" runat="server" />
 
 </div>
 <div class="one_half fleft marginTop last border bgblue" id="divPrivtaePaging" ><AJ:BestPrivateCollege ID="ucBestPrivateCollege" runat="server" />
 </div>
 </div>

 <div class="one_fourth  fright last"><AJ:Query ID="ucQuery" runat="server" /></div>
 <div class="one_fourth  fright last"><AJ:LatestNews ID="ucLatestNews" runat="server" /></div>
  <script src="/Js/JHome.js" type="text/javascript"></script>
  <script type="text/javascript">
      $(".image_tooltip").tipTip({ maxWidth: "auto", delay: 50 });
  </script>
</asp:content>
