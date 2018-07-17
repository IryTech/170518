<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.error" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
	<!-- The content div -->
		<div id="content">
		
			<!-- text -->
			<div id="text" style="width:55%; float:left;">
				<h1 style="color:Red;">Ooops! An unexpected error has occurred.</h1>
				<p style="font-weight:bold;line-height:20px;">
               This one's down to me! Please accept my apologies for this - I'll see to it
                that the developer responsible for this happening is given 20 lashes 
                (but only after he or she has fixed this problem).</p>
              
				</div>
		
		<div style="width:35%; float:left;">
			<!-- Book icon -->
			<img id="book" src="/image.axd?Common=errorImage.png" alt="Please accept my apologies for this" />
			<!-- End Book icon -->
			</div>
			<div style="clear:both;"></div>
		</div>
		<!-- End Content -->


        <script type="text/javascript" >

            $(function () {
                var timeout = 60000;
                $(document).bind("idle.idleTimer", function () {
                    // function you want to fire when the user goes idle
                    $.timeoutDialog({ timeout: 1, countdown: 60, logout_redirect_url: 'http://www.aspdotnet-suresh.com', restart_on_yes: true });
                });
                $(document).bind("active.idleTimer", function () {
                    // function you want to fire when the user becomes active again
                });
                $.idleTimer(timeout);
            });
        </script>
   </asp:Content>