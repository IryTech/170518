<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Share.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.Share" %>

<%@ Register Src="~/UserControl/ForwardFriends.ascx" TagPrefix="AJ" TagName="ForwardEmail" %>

     <div>
     <h3>Share</h3>
     <hr class="hrline" />
     <div class="ucshare"><a href="#" id="sndEmailPop" onclick="ForwardEmailTpoFrnd();ClearFrndForm();return false;" title="Send Email To friend">
     <img src="/image.axd?Common=email-add.png" class="fleft" title="Email to a friend" alt="Email to a friend" />Email
     </a>
 </div>
 <div class="ucshare">
    <fb:like layout="button_count"></fb:like></div>
<div class="ucshare">

<a href="http://twitter.com/share" class="twitter-share-button" > </a> </div>
 <div id="socialPlusOne">
	 <g:plusone></g:plusone>
		
	</div>

    
    </div>
    <div id="divEmailPop"  class=" popup_block">
    <AJ:ForwardEmail ID="ADMJForwardEmail" runat="server"></AJ:ForwardEmail>
              
      </div> 
      <div id="fb-root">
    </div>    <script src="https://platform.twitter.com/widgets.js" type="text/javascript"></script>    <script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
    <script type="text/javascript"> var vTitle;
        $(document).ready(function() {
            vTitle = $(this).attr('title');
            $(".twitter-share-button").attr("data-url", location.href);
            $(".twitter-share-button").attr("data-counturl", location.href);
            $(".twitter-share-button").attr("data-text", vTitle);
           
        });
       
       
        window.fbAsyncInit = function () {
            FB.init({
                appId: '228866610572164', // App ID
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

  
    <script type="text/javascript">
        function ForwardEmailTpoFrnd() {
            $("#sndMessage").hide();
            OpenPoup('divEmailPop', '550', 'sndEmailPop');
        }
       
    </script>
   
    
