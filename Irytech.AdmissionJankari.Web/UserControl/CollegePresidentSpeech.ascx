<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegePresidentSpeech.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegePresidentSpeech" %>
<div class="box1 fleft">
<h3 class="clgSearchH2 h2margin">President Speech
   </h3>
   <div class="box">
   <center>
<asp:Image ID="imgDirectorImage" runat="server" height="70px" width="70px" /><br />
<strong><label id="lblPersonName" runat="server"></label></strong><hr class="hrline" />
</center>


<p runat="server" id="lblDirectorSpeech">&nbsp;</p>
<span class="readMore"><a href="#" id="lnkPresident" title="Read More" onclick="OpenPoup('divPresidentSpeech',450,'lnkPresident');return false;">Read More &raquo;</a></span>
</div>
</div>
<div class="popup_block" id="divPresidentSpeech" style="width: 750px!important;height: 500px!important;overflow-y: scroll">
<ul class="horizontal"><center>
<li>
<asp:Image ID="imgDirector" runat="server"  height="100px" width="100px"/></li>
<li>
<label id="lblDirectorName"  runat="server"></label><label id="lblDesignation" class="aColor" runat="server"></label>

</li></center>
<li>
<p runat="server" id="lblSpeech"></p></li></ul>
</div>