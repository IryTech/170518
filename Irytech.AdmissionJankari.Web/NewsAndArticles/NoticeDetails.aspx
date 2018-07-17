<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.NewsAndArticles.NoticeDetails" %>
<%@ Register Src="~/UserControl/UcMostViewdNotice.ascx" TagPrefix="AJ" TagName="MostViewdNotice" %>
<%@ Register Src="~/UserControl/LatestNotice.ascx" TagPrefix="AJ" TagName="LatestNotice" %>
<%@ Register Src="~/UserControl/UcMostViewedCollege.ascx" TagPrefix="AJ" TagName="MostViewdCollege" %>
<%@ Register Src="~/UserControl/ucFacebookfanNewsArticles.ascx" TagPrefix="AJ" TagName="ADMJFacebookfan" %>
<%@ Register Src="~/UserControl/UcCommonComment.ascx" TagPrefix="AJ" TagName="CommonComment" %>
<%@ Register Src="~/UserControl/TotalViews.ascx" TagPrefix="AJ" TagName="TotalViews" %>
<%@ Register Src="~/UserControl/UcRatingControl.ascx" TagPrefix="AJ" TagName="RatingControl" %>
<%@ Register Src="~/UserControl/CommentCount.ascx" TagPrefix="AJ" TagName="CommentCount" %>
<%@ Register Src="~/UserControl/Print.ascx" TagPrefix="AJ" TagName="Print" %>
<%@ Register Src="~/UserControl/ReportAbuse.ascx" TagPrefix="AJ" TagName="ReportAbuse" %>
<%@ Register Src="~/UserControl/ForwardFriends.ascx" TagPrefix="AJ" TagName="ForwardEmail" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<div class="five_sixth fleft last">
    <div class="four_fifth last fleft">
        <div class="border fleft marginbottom paddingBottom boxPlane " id="print">
             <h2 class="streamCompareH3 " id="NoticeTitle"  runat="server"></h2>
              <div class="ratingDiv fleft" id="noPrint">




                <div class="cmnt"><AJ:CommentCount ID="ADMJCommentCount" runat="server">
                     </AJ:CommentCount></div>
                <div class="views">(<AJ:TotalViews ID="ADMJTotalViews" runat="server"></AJ:TotalViews>)</div>
                <div class="print"><AJ:Print ID="ADMJPrint" runat="server"></AJ:Print></div>
                
            <%--    <a href="#" id="shareMsg" onclick="OpenPoup('shareMessage',250,'shareMsg');return false;">
                        <img src="/image.axd?Common=share.png" title="Share" alt="share" /></a>--%>
                   
               <%-- <a href="#" id="shareMsg" onclick="OpenPoup('shareMessage',250,'shareMsg');return false;" title="Share">
                        <img src="/image.axd?Common=share.png" title="Share" alt="share" /></a>--%>
                          <div class="print"><span class="ucshare">
                         <a href="#" id="sndEmailPop" onclick="ForwardEmailTpoFrnd();return false;" title="Send Email to your friend">
     <img src="/image.axd?Common=email-add.png" class="fleft" title="Email to a friend" alt="Email to a friend" />Email
     </a></span>
 </div>


  <div class="print" style="width:70px !important;"><span class="ucshare">
    <fb:like layout="button_count"></fb:like></span>
</div>
 <div class="print" style="width:80px !important;"><span class="ucshare">

<a href="http://twitter.com/share" class="twitter-share-button"  >
 </a></span>
 </div>
  <div id="socialPlusOne" style="width:60px !important;" class="print">
   <span class="ucshare">
	 <span class="g-plusone" data-size="medium"></span></span> 
		
	</div>
                      
                        
                 <div class="reportabus">
                     <span  id="liReportAbuse">
                          <a href="#" title="Report Abuse" id="sndReportAbuse" onclick="CheckLoginForReportAbuse();return false;">
                         <img src="/image.axd?Common=redFlag.png" title="Report Abuse" alt="share" />
                          </a></span>
                 </div>
                <div class="ratestar"><AJ:RatingControl ID="UcRating" runat="server">
                     </AJ:RatingControl></div>         
   </div>
   
        
   <div class="clearBoth"></div>
             <strong class="publish">Published : <span id="spnDate" runat="server"></span> By - Admission Jankari </strong>
            <ul class="vertical">
                <li class="marginRight"><span class="Imgarrow"><asp:Image ID="ImgNotice" runat="server" height="120px" width="100px"  /></span></li>
                <li class="width75Percent"><p id="noticeDesc" runat="server"></p>
                </li>
            </ul>
        
        </div>
       
        <div class="marginTop clearBoth">
         <AJ:ADMJFacebookfan ID="UCADMJFacebookfan" runat="server"></AJ:ADMJFacebookfan>
        </div>
        <asp:UpdatePanel ID="updNoticeMaster" runat="server">
<ContentTemplate>
        <div id="comment" >
        <AJ:CommonComment runat="server" ID="UcComment"   />
        </div></ContentTemplate>
</asp:UpdatePanel> 
    </div>
     <div class="one_third fright last">
         <div class="box1" id="divLatestNews" >
                <a href="http://www.hotelscombined.com/?a_aid=94784&label=Image300250" target="_blank" rel="nofollow">
                <img width="99%" src="http://media.datahc.com/banners/affiliate/en/inspirational_300x250.gif" alt="Compare hotel prices and find the best deal - HotelsCombined.com" title="Compare hotel prices and find the best deal - HotelsCombined.com" border="0" /></a>
            </div>
            </div>
            <div class="one_third fright last">
                <AJ:LatestNotice ID="ucLatestNotice" runat="server"></AJ:LatestNotice>
            </div>
            <div class="one_third fright last">
                <AJ:MostViewdNotice ID="ucMostviewdNotice" runat="server"></AJ:MostViewdNotice>
            </div>
            <div class="one_third fright last">
                <AJ:MostViewdCollege ID="ucMostViewdCollege" runat="server"></AJ:MostViewdCollege>
            </div>
            </div>
           <%-- <div class="popup_block" id="shareMessage">
     <AJ:Share ID="ADMJShare" runat="server"></AJ:Share>
</div>--%>
<div class="popup_block" id="divReportAbuse">
      <AJ:ReportAbuse ID="ADMJReportAbuse" runat="server"></AJ:ReportAbuse>
</div>
<div id="divEmailPop"  class=" popup_block">
    <AJ:ForwardEmail ID="ADMJForwardEmail" runat="server"></AJ:ForwardEmail>
              
      </div>
  <span id="lblReportMsg" style="display: none;width:auto ;height:40px;padding-left: 50px" ></span>

  <div class="loading fademessage" id="divReportMessage">
       
   </div>
       <script src="https://platform.twitter.com/widgets.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
 
<script language="javascript" type="text/javascript"> 
    function printSelection() {

        var divToPrint = document.getElementById('print');

        var content = divToPrint.innerHTML;
        var pwin = window.open('', 'print_content', 'width=700,height=500');

        pwin.document.open();
        pwin.document.write('<html><style>#noPrint{display:none}</style><body onload="window.print()">' + content + '</body></html>');
        pwin.document.close();


    }

   
    function CheckLoginForReportAbuse() {   
        var status = false;
         $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/CheckSession",
                data: "{}",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {

                    if (response.d == false) {
                       
                        status= false;
                    }else {
                        status = true;
                    }
                },
                error: function(response) {

                },
            });
          
            if (status == true) { 
              
                 OpenPoup('divReportAbuse', 450, 'sndReportAbuse');
              
                return false;
            }
         
        else {
            var  pathArray = window.location.pathname.split( '/' );

          location.href = "account/login?ReturnUrl=" + pathArray[1]+"/"+pathArray[2];
            }
    }
</script> 
 <script type="text/javascript">     var vTitle;
     $(document).ready(function () {
         vTitle = $(this).attr('title');
         $(".twitter-share-button").attr("data-url", location.href);
         $(".twitter-share-button").attr("data-counturl", location.href);
         $(".twitter-share-button").attr("data-text", vTitle);

     });

     function ForwardEmailTpoFrnd() {
         $("#sndMessage").hide();
         ClearFrndForm();
         OpenPoup('divEmailPop', '550', 'sndEmailPop');
     }
       
    </script>    

</asp:content>