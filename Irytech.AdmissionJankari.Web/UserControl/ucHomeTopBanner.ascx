<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucHomeTopBanner.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ucHomeTopBanner" %>

<div id="carousel">

	<div id="buttons">
		<a href="#" id="prev"></a>
		<a href="#" id="next"></a>
		<div class="clear"></div>
	</div>
	
	<div class="clear"></div>
    <asp:HiddenField runat="server" ID="hdnCourseBanner">
    </asp:HiddenField>  <asp:HiddenField runat="server" ID="hdnCourseBannerId">
    </asp:HiddenField>
 <div id="slides" > 
	
</div> </div> 
<script type="text/javascript">
    $(document).ready(function() {
        BindBanner($("#<%=hdnCourseBanner.ClientID %>").val(), $("#<%=hdnCourseBannerId.ClientID %>").val());
    });
 
</script>

	