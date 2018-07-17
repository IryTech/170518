<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeDetails.aspx.cs"
    MaintainScrollPositionOnPostback="true" Inherits="IryTech.AdmissionJankari20.Web.College.CollegeDetails" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BO" %>
<%@ Register Src="~/UserControl/CollegeBasicDetails.ascx" TagPrefix="AJ" TagName="CollegeBasicDetails" %>
<%@ Register Src="~/UserControl/CollegeDescription.ascx" TagPrefix="AJ" TagName="CollegeDescription" %>
<%@ Register Src="~/UserControl/CollegeAvailableCourse.ascx" TagPrefix="AJ" TagName="CollegeBasicCourse" %>
<%@ Register Src="~/UserControl/CollegeAvailabelCourseExam.ascx" TagPrefix="AJ" TagName="CollegeCourseExam" %>
<%@ Register Src="~/UserControl/CollegeAvailableFacailties.ascx" TagPrefix="AJ" TagName="CollegeCourseFacality" %>
<%@ Register Src="~/UserControl/CollegeAvailableHostel.ascx" TagPrefix="AJ" TagName="CollegeCourseHostel" %>
<%@ Register Src="~/UserControl/CollegeContactDetails.ascx" TagPrefix="AJ" TagName="ColegeContactsDetails" %>
<%@ Register Src="~/UserControl/ColegeListByCityId.ascx" TagPrefix="AJ" TagName="CollegeListByCity" %>
<%@ Register Src="~/UserControl/UcMostViewedCollege.ascx" TagPrefix="AJ" TagName="MostviewdCollege" %>
<%@ Register Src="~/UserControl/UcCollegeQuery.ascx" TagPrefix="AJ" TagName="CollegeQuery" %>
<%@ Register Src="~/UserControl/RelatedColleges.ascx" TagPrefix="AJ" TagName="RelatedCollege" %>
<%@ Register Src="~/UserControl/CollegeQueryOnDetails.ascx" TagPrefix="AJ" TagName="QuickQUery" %>
<%@ Register Src="~/UserControl/GoogleMap.ascx" TagPrefix="AJ" TagName="GoogleMap" %>
<%@ Register Src="~/UserControl/ColleegRankChart.ascx" TagPrefix="AJ" TagName="CollegeChart" %>
<%@ Register Src="~/UserControl/UcCallFromInstitute.ascx" TagPrefix="AJ" TagName="Call" %>
<%@ Register Src="~/UserControl/UcAjFacebookfan.ascx" TagPrefix="AJ" TagName="FacebookFan" %>
<%@ Register Src="~/UserControl/UcCommonComment.ascx" TagPrefix="AJ" TagName="CommonComment" %>
<%@ Register Src="~/UserControl/UcCollegeBranchEvent.ascx" TagPrefix="AJ" TagName="CollegeEvent" %>
<%@ Register Src="~/UserControl/CommentCount.ascx" TagPrefix="AJ" TagName="CommentCount" %>
<%@ Register Src="~/UserControl/ReportAbuse.ascx" TagPrefix="AJ" TagName="ReportAbuse" %>
<%@ Register Src="~/UserControl/UcRatingControl.ascx" TagPrefix="AJ" TagName="RatingControl" %>
<%@ Register Src="~/UserControl/TotalViews.ascx" TagPrefix="AJ" TagName="TotalViews" %>
<%@ Register Src="~/UserControl/UcReportDonationStory.ascx" TagPrefix="AJ" TagName="ReportDonation" %>
<%@ Register Src="~/UserControl/CollegePalcementShow.ascx" TagPrefix="AJ" TagName="Placement" %>
<%@ Register Src="~/UserControl/CollegeNotice.ascx" TagPrefix="AJ" TagName="Notice" %>
<%@ Register Src="~/UserControl/CollegeHighLightsOnDetails.ascx" TagPrefix="AJ" TagName="CollegeHighLights" %>
<%@ Register Src="~/UserControl/CollegeTestimonialOnDetails.ascx" TagPrefix="AJ"
    TagName="Testimoniala" %>
<%@ Register Src="~/UserControl/ThirdPartyAdvst.ascx" TagPrefix="AJ" TagName="ADVST" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">

<%--<asp:UpdatePanel runat="server"   ID="updateCollegeCityList">
<ContentTemplate>--%>
<div class="five_sixth fleft last">
        <div class="boxPlane bgYellow marginbottom">
            <ul class="vertical" style="margin-left:0px; width:100%;">
            <li class="Imgarrow marginRight"><asp:ImageMap ID="CollegeImageHeader" runat="server"  align="left" Height="100" Width="100" hspace="5" /></li>
            <li class="width50Percent"><h1><asp:Label ID="lblHeaderCollegeName" runat="server"></asp:Label></h1>
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+(IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new IryTech.AdmissionJankari.BL.Common().CourseName))+"/Get-Direct-Admission").ToLower() %>'  class="ultimateLink fleft" id="OnlineCounselling"   title="Hurry up confirm your seats"><span id="spnText" runat="server"> Apply Online</span></a>
            <span class="leftmargin"> 
            <a id="lnkReportdonation" runat="server" class="ultimateLink"  title="Report Donation">Report Donation</a>
            <a id='lnkBookSeat'  class="ultimateLink" Visible="False"  runat="server"  href='#'>Book Your Seat </a>
            <a href="#" id="lnkBook"></a>
            </span>
            
            <hr class="hrline" /> <h3 class="streamCompareH3">
            Admission Helpline :<asp:Label id="txtHelpLineNo" style="font-size:0.95em; color:Maroon;"  runat="server"></asp:Label></h3></li>
            <li class="width33Percent">
                     <AJ:Call runat="server" ID="ucCall" />
                </li>
           </ul>
            <div class="clearBoth"></div>
            </div>
            <div class="pageTargetMenu fleft">
            <ul class="vertical">
            <li visible="false" runat="server" id="liOverview"><a href="#Overview"  rel="Bookmark" title="Overview">Overview</a></li>
            <li visible="false" runat="server" id="liDescription"><a href="#pnlDescrip" rel="Bookmark"  title="Description">Description</a></li>
            <li visible="false" runat="server" id="liCourse"><a href="#course" rel="Bookmark" title="Course Description">Course</a></li>
            <li visible="false" runat="server" id="liExam"><a href="#exam" rel="Bookmark" title="College Contact Details">Exam</a></li>
            <li visible="false" runat="server" id="liFacality"><a href="#facality" rel="Bookmark"  title="College Facality">Facality</a></li>
            <li visible="false" runat="server" id="liHostel"><a href="#hostel"  rel="Bookmark" title="College Hostel">Hostel</a></li>
            <li visible="false" runat="server" id="liContactDetails"><a href="#contactdetails" rel="Bookmark"  title="Contact Details">Contact Details</a></li>
            <li visible="false" runat="server" id="liEvent"><a href="#Event" rel="Bookmark"  title="College Event">Event</a></li>
           <li visible="false" runat="server" id="liCllgSaneCity"><a href="#CllgSaneCity"  class="end" title="College in same city">Colleges In Same City</a></li>  </ul>
            
           </div>
           
         <div class="ratingDiv fleft" id="noPrint" style="width:99.5%; margin-bottom:10px;">
                <div class="cmnt"><AJ:CommentCount ID="ADMJCommentCount"   runat="server">
                     </AJ:CommentCount></div>
                <div class="views">(<AJ:TotalViews ID="ADMJTotalViews" runat="server"  ></AJ:TotalViews>)</div>
                <div class="print socalappTwit">  <span class="ucshare">

                    <a href="http://twitter.com/share" class="twitter-share-button" >
                     </a>
                     </span></div>   
                     <div class="print socalapp">
                     <span class="ucshare">
                            <fb:like  layout="button_count"></fb:like>
                        </span>  
                        </div>
                       <div class="print socalapp"> 
                        <div class="g-plusone" data-size="medium"></div>
                         </div>
                 <div class="reportabus">
                     <span  id="liReportAbuse">
                          <a href="#" title="Report Abuse" id="sndReportAbuse" onclick="CheckLoginForReportAbuse();return false;">
                         <img src="/image.axd?Common=redFlag.png" title="Report Abuse" alt="Report Abuse" />
                          </a></span>
                 </div>
                <div class="ratestar"><AJ:RatingControl ID="UcRating" runat="server" >
                     </AJ:RatingControl></div>
         <div class="loading fademessage" id="divReportMessage">
       
   </div>
         </div>


   <div class="clearBoth"></div>
<div class="four_fifth last fleft">
<div id="Overview">
<AJ:CollegeBasicDetails runat="server" ID="ucCollegeBasicDetails" />
</div>
<div id="pnlDescrip" >
<AJ:CollegeDescription runat="server" ID="ucCollegeDescription" />
</div>
<div id="course">
<AJ:CollegeBasicCourse runat="server" ID="ucCollegeBasicCourse" />
</div>
<div id="Event">
<AJ:CollegeEvent runat="server" ID="ucCollegeEvent" />
</div>
<div id="exam">
<AJ:CollegeCourseExam runat="server" ID="ucCollegeCourseExam" />
</div>
<div id="placement">
<AJ:Placement runat="server" ID="ucPlacement" />
</div>
<div id="HighLights">
<AJ:CollegeHighLights runat="server" ID="UcCollegeHighLights" />
</div>
<div id="facality">
<AJ:CollegeCourseFacality runat="server" ID="ucCollegeCourseFacality" />
</div>
<div id="Div3">
<AJ:Notice runat="server" ID="CollegeNotice" />
</div>
<div id="hostel">
<AJ:CollegeCourseHostel runat="server" ID="ucCollegeCourseHostel" />
</div>
<div id="contactdetails">
<AJ:ColegeContactsDetails runat="server" ID="ucColegeContactsDetails" />
</div>
<div id="Testimonial">
<AJ:Testimoniala  runat="server"  ID="ucTestimonial" />
</div>
<div>
<AJ:CollegeChart runat="server" ID="ucCollegeChart" />
</div>
<div>
<AJ:CollegeQuery runat="server" ID="ucCollegeQuery" />
</div>
<div>
<AJ:GoogleMap runat="server" ID="ucGoogleMap" />
</div>
<div>
<AJ:ReportDonation runat="server" ID="UcReportDonation"   /> 
</div>
<div id="comment" >
<AJ:CommonComment runat="server" ID="UcComment"   />
</div>
</div><div class="one_third fright last" >
<AJ:QuickQUery runat="server" ID="ucQuickQUery" />
</div>
<div class="one_third fright last" >

<AJ:ADVST runat="server" ID="RelatedColleddge1" />
</div>
<div class="one_third fright last" id="CollegeQuery">
<AJ:RelatedCollege runat="server" ID="ucRelatedCollege" />
</div>
<div class="one_third fright last" id="CllgSaneCity">
<AJ:CollegeListByCity runat="server" ID="ucCollegeListByCity" />
</div>
<div class="one_third fright last" id="Div1">
<AJ:MostviewdCollege runat="server" ID="ucMostviedCollege" />
</div>
<div class="one_third fright last" id="Div2">
<AJ:FacebookFan runat="server" ID="ucFacebookFan" />
</div>

<div class="popup_block" id="divReportAbuse">
      <AJ:ReportAbuse ID="ADMJReportAbuse" runat="server"></AJ:ReportAbuse>
</div>
 
  <input type="hidden" id="hdnCollegeCourseId" />
    <input id="hdnCourse" type="hidden" value="0" />
    <input id="hdnCourseValue" type="hidden" value="0" />
      <input id="hdnCollege" type="hidden" value="0" />
        <input id="hdnCity" type="hidden" value="0" />
        <asp:HiddenField runat="server" ID="hdnCollegeCourseIdBook"></asp:HiddenField>
          <asp:HiddenField runat="server" ID="hdnCourseIdBook"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnCourseBook"></asp:HiddenField>
              <asp:HiddenField runat="server" ID="hdnCollegeIdBook"></asp:HiddenField>
               <asp:HiddenField runat="server" ID="hdnCityIdBook"></asp:HiddenField>
</div>
<%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <div id="fb-root">
    </div>
    
    <script src="https://platform.twitter.com/widgets.js" type="text/javascript"></script>

    <script type="text/javascript">
        var vTitle;
        $(document).ready(function () {
            vTitle = $(this).attr('title');
            $(".twitter-share-button").attr("data-url", location.href);
            $(".twitter-share-button").attr("data-counturl", location.href);
            $(".twitter-share-button").attr("data-text", vTitle);
        });


        window.fbAsyncInit = function () {
            FB.init({
                appId: '145890325492400', // App ID
                channelUrl: 'http://www.admissionjankari.com/' + window.location.pathname, // Channel File
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true  // parse XFBML
            });

            // Additional initialization code here
        };

        // Load the SDK Asynchronously
        (function (d) {
            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement('script'); js.id = id; js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            ref.parentNode.insertBefore(js, ref);
        } (document));
    </script>
    <script type="text/javascript">
        (function () {
            var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
            po.src = 'https://apis.google.com/js/plusone.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
        })();
        function CheckLoginForReportAbuse() {
            var status = false;
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/CheckSession",
                data: "{}",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d == false) {

                        status = false;
                    } else {
                        status = true;
                    }
                },
                error: function (response) {

                }
            });

            if (status == true) {

                OpenPoup('divReportAbuse', 450, 'sndReportAbuse');

                return false;
            }

            else {
                var pathArray = window.location.pathname.split('/');

                location.href = "/account/login?ReturnUrl=" + pathArray[1] + "/" + pathArray[2] + "/" + pathArray[3];
            }
        }
          
</script>

<script type = "text/javascript">
    window.onload = function () {
        var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
        if (!isNaN(scrollY)) {
            window.scrollTo(0, scrollY);
        }
    };
    window.onscroll = function () {
        var scrollY = document.body.scrollTop;
        if (scrollY == 0) {
            if (window.pageYOffset) {
                scrollY = window.pageYOffset;
            }
            else {
                scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
            }
        }
        if (scrollY > 0) {
            var input = document.getElementById("scrollY");
            if (input == null) {
                input = document.createElement("input");
                input.setAttribute("type", "hidden");
                input.setAttribute("id", "scrollY");
                input.setAttribute("name", "scrollY");
                document.forms[0].appendChild(input);
            }
            input.value = scrollY;
        }
    };
</script>
</asp:content>
