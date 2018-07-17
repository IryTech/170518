<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SponserCollegeDetails.aspx.cs"  Inherits="IryTech.AdmissionJankari20.Web.College.SponserCollegeDetails" %>

<%@ Register Src="/UserControl/CollegeBasicDetails.ascx" TagPrefix="AJ" TagName="CollegeBasicDetails" %>
<%@ Register Src="/UserControl/CollegeDescription.ascx" TagPrefix="AJ" TagName="CollegeDescription" %>
<%@ Register Src="/UserControl/CollegeAvailableCourse.ascx" TagPrefix="AJ" TagName="CollegeBasicCourse" %>
<%@ Register Src="/UserControl/CollegeAvailabelCourseExam.ascx" TagPrefix="AJ"
    TagName="CollegeCourseExam" %>
<%@ Register Src="/UserControl/CollegeAvailableFacailties.ascx" TagPrefix="AJ"
    TagName="CollegeCourseFacality" %>
<%@ Register Src="/UserControl/CollegeAvailableHostel.ascx" TagPrefix="AJ" TagName="CollegeCourseHostel" %>
<%@ Register Src="/UserControl/CollegeContactDetails.ascx" TagPrefix="AJ" TagName="ColegeContactsDetails" %>
<%@ Register Src="~/UserControl/UcCallFromInstitute.ascx" TagPrefix="AJ" TagName="Call" %>
<%@ Register Src="/UserControl/UcCollegeQuery.ascx" TagPrefix="AJ" TagName="CollegeQuery" %>
<%@ Register Src="/UserControl/RelatedColleges.ascx" TagPrefix="AJ" TagName="RelatedCollege" %>
<%@ Register Src="/UserControl/CollegePresidentSpeech.ascx" TagPrefix="AJ" TagName="PresidentSpeech" %>
<%@ Register Src="/UserControl/CollegeCourseHighLIghts.ascx" TagPrefix="AJ" TagName="HighLights" %>
<%@ Register Src="/UserControl/CollegeTopHirer.ascx" TagPrefix="AJ" TagName="TopHirer" %>
<%@ Register Src="/UserControl/CollegeGallery.ascx" TagPrefix="AJ" TagName="CollegeBranchGallery" %>
<%@ Register Src="/UserControl/ColleegRankChart.ascx" TagPrefix="AJ" TagName="CollegeChart" %>
<%@ Register Src="/UserControl/GoogleMap.ascx" TagPrefix="AJ" TagName="GoogleMap" %>
<%@ Register Src="/UserControl/CollegeQueryOnDetails.ascx" TagPrefix="AJ" TagName="QuickQUery" %>
<%@ Register Src="~/UserControl/UcCommonComment.ascx" TagPrefix="AJ" TagName="CommonComment" %>
<%@ Register Src="~/UserControl/UcAjFacebookfan.ascx" TagPrefix="AJ" TagName="FacebookFan" %>
<%@ Register Src="~/UserControl/CollegePalcementShow.ascx" TagPrefix="AJ" TagName="Placement" %>


<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">

<div class="five_sixth fleft last">
        <div class="box bgYellow">
            <ul class="vertical" style="margin-left:0px; width:100%;">
            <li class="Imgarrow marginRight">
                <asp:ImageMap ID="CollegeImageHeader" runat="server"  align="left" Height="100" Width="100" hspace="5" />
            </li>
            <li class="width50Percent"><h1><asp:Label style="font-size:25px;" ID="lblHeaderCollegeName" runat="server"></asp:Label></h1><br /><span class="marginTop1" id="Span1"  runat="server" Visible='true'>
                     <a href="/counselling/DirectAdmission.aspx"  class="greenbutton"   runat="server" id="sndOnLineCounselling" title="OnLineCounselling">Apply Online</a> 
                     </span><br /><br /> <h2 class="streamCompareH3 " style="font-size:20px !important;">Admission Helpline: <asp:Label id="txtHelpLineNo" style="font-size:20px; color:Maroon;" runat="server"></asp:Label></h2>
                      <a id='lnkBookSeat'  class="ultimateLink" Visible="False"  runat="server"  href='#'>Book Your Seat </a>
            <a href="#" id="lnkBook"></a></li>
                <li class="width33Percent">
                     <AJ:Call runat="server" ID="ucCall" />
                </li>
            </ul>
            <div class="clearBoth"></div>
            </div>

        <div class="pageTargetMenu">
           <ul class="vertical">
           <li runat="server" visible="false" id="liOverview"><a href="#Overview"  rel="Bookmark" title="Overview">Overview</a></li>
           <li runat="server"  visible="false" id="liDescription"><a href="#pnlDescrip" rel="Bookmark"  title="Description">Description</a></li>
           <li runat="server"  visible="false" id="liCourse"><a href="#Course" rel="Bookmark" title="Loan Range">Course</a></li>
           <li runat="server"  visible="false" id="liExam"><a href="#Exam" rel="Bookmark" title="Bank Contact Details">Exam</a></li>
           <li runat="server"  visible="false" id="liFacality"><a href="#Facality" rel="Bookmark"  title="Contact Details">Facality</a></li>
           <li  visible="false"  runat="server" id="liHostel"><a href="#Hostel"  rel="Bookmark" title="Contact Details">Hostel</a></li>
           <li  visible="false" runat="server" id="liContactDetails"><a href="#ContactDetails" rel="Bookmark"  title="Contact Details">ContactDetails</a></li>
           <li   visible="false" runat="server" id="liCllgSaneCity"><a href="#CllgSaneCity"  class="end" title="Contact Details">Colleges In Same City</a></li> 
           <li  visible="false"  runat="server" id="liHighLights"><a href="#HighLights"  rel="Bookmark" title="Contact Details">HighLights</a></li>
           <li  visible="false" runat="server" id="liGallery"><a href="#Gallery" rel="Bookmark"  title="Contact Details">Gallery</a></li>
           <li   visible="false" runat="server" id="liPresidentSpeech"><a href="#PresidentSpeech"  class="end" title="Contact Details">PresidentSpeech</a></li> 
           
            </ul>

            <div class="clearBoth"></div>
        </div>

        
<div class="four_fifth last fleft">
<div id="Overview">
<AJ:CollegeBasicDetails runat="server" ID="ucCollegeBasicDetails" />
</div>
<div id="pnlDescrip" >
<AJ:CollegeDescription runat="server" ID="ucCollegeDescription" />
</div>
<div id="Course">
<AJ:CollegeBasicCourse runat="server" ID="ucCollegeBasicCourse" />
</div>
<div id="Exam">
<AJ:CollegeCourseExam runat="server" ID="ucCollegeCourseExam" />
</div>
<div id="Facality">
<AJ:CollegeCourseFacality runat="server" ID="ucCollegeCourseFacality" />
</div>
<div id="placement">
<AJ:Placement runat="server" ID="ucPlacement" />
</div>
<div id="Div6">
<AJ:CollegeChart runat="server" ID="ucCollegeChart" />
</div>
<div id="Hostel">
<AJ:CollegeCourseHostel runat="server" ID="ucCollegeCourseHostel" />
</div>
<div id="ContactDetails">
<AJ:ColegeContactsDetails runat="server" ID="ucColegeContactsDetails" />
</div>

<div id="Div1">
<AJ:GoogleMap runat="server" ID="ucGoogleMap" />
</div>
<div id="comment">
<AJ:CommonComment runat="server" ID="UcComment"   />
</div>
</div>

<div class="one_third fright last" >
<AJ:QuickQUery runat="server" ID="ucQuickQUery" />
</div>
<div class="one_third fright last" id="CollegeQuery">
<AJ:RelatedCollege runat="server" ID="ucRelatedCollege" />
</div>
<div class="one_third fright last" id="linkCollegeQuery">
<AJ:CollegeQuery runat="server" ID="ucCollegeQuery" />
</div>
<div class="one_third fright last" id="HighLights">
<AJ:HighLights runat="server" ID="ucHighLights" />
</div>
<div class="one_third fright last" id="Div4">
<AJ:TopHirer runat="server" ID="ucTopHirer" />
</div>
<div class="one_third fright last" id="Gallery">
<AJ:CollegeBranchGallery runat="server" ID="ucCollegeBranchGallery" />
</div>

<div class="one_third fright last" id="Div2">
<AJ:FacebookFan runat="server" ID="ucFacebookFan" />
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
<div class="one_sixth last fright marginTop" id="PresidentSpeech" >
<AJ:PresidentSpeech runat="server" ID="ucPresidentSpeech" />
</div>

<script type="text/javascript">

    function OpenBooksSeatPopUp() {
        $("#hdnCourse").val($("#<%=hdnCourseIdBook.ClientID%>").val());
        $("#hdnCollege").val($("#<%=hdnCollegeIdBook.ClientID%>").val());
        $("#hdnCity").val($("#<%=hdnCityIdBook.ClientID%>").val());
        $("#hdnCollegeCourseId").val($("#<%=hdnCollegeCourseIdBook.ClientID%>").val());
        $("#divCollegeBookesSeat").hide();
        $("#hBkSeat").hide();
        $("#hdnCourseValue").val($("#<%=hdnCourseBook.ClientID%>").val());

        $(".register").show();

        if ('<%= Session["UID"] %>' == '') {
            OpenPoup("divBookSeat", "550", "lnkBook");
        }
        else
            window.CheckCollegeBookSeatStatus();
    }
    window.fbAsyncInit = function () {
        FB.init({
            appId: '411083678951629', // App ID
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
</asp:content>
