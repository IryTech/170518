<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcReportDonationCollegeList.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcReportDonationCollegeList" %>
<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<asp:UpdatePanel runat="server" ID="updateCollegeReport">
<ContentTemplate>
<div class="box1 fleft" runat="server" id="divCollegeInCity">
<h3 class="streamCompareH3">Donation reported against the colleges </h3>
<span class="ucshare">

                    <a href="http://twitter.com/share" class="twitter-share-button" >                     </a>                     </span>
                

                     <span class="ucshare">
                            <fb:like layout="button_count"></fb:like>                        </span>  
                    

                  

                        <div class="g-plusone" data-size="medium"></div>
                         

<hr class="hrline" />

<div class="boxPlane">
    
    <asp:Repeater  ID="rptCollegeList" runat="server">
         <ItemTemplate>
            <div class="ucDiv">
        <ul class="vertical marginbottom">
        <li>
                 <a href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("AjCourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("AjCollegeBranchName")))).ToLower()%>'>
                    <img src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("AjCollegeBranchLogo").ToString()) ?"NoImage.jpg":Eval("AjCollegeBranchLogo")) %>' alt='<%# Eval("AjCollegeBranchName") %>' height="40px" width="40px" /></a>
                </li>
                <li class="width70Percent">
                
                    <a href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("AjCourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("AjCollegeBranchName")))).ToLower() %>' rel="canonical"  title='<%# Eval("AjCollegeBranchName")%>'><%# Eval("AjCollegeBranchName")%>
                      
                    </a></li>
                    <li>
                    <span class="clglistbyCitySpan"> Est:<%# Eval("AjCollegeBranchEst")%></span> 
                    </li>
                    <span class="clearBoth dispBlock"></span>
              </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <AJ:CustomPaging ID="ucCustomPagingCollegeByCity" runat="server" />
   
    </div>
     </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
    </div><script src="https://platform.twitter.com/widgets.js" type="text/javascript"></script>
    <script type="text/javascript">
        var vTitle;
        $(document).ready(function () {
            vTitle = $(this).attr('title');
            $(".twitter-share-button").attr("data-url", location.href);
            $(".twitter-share-button").attr("data-counturl", location.href);
            $(".twitter-share-button").attr("data-text", vTitle);
        });
    
         (function () {
            var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
            po.src = 'https://apis.google.com/js/plusone.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
        })();

        window.fbAsyncInit = function () {
            FB.init({
                appId: '411083678951629', // App ID
                //                channelUrl: '//WWW.YOUR_DOMAIN.COM/channel.html', // Channel File
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