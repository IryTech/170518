<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UniversityBasicDetails.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UniversityBasicDetails" %>
 
<div class="box1">
<h3 class="streamCompareH3" runat="server" id="lblUniversityName" ></h3>
<hr class="hrline" />
<div class="box">
<ol>
<li><strong class="strongDetails">Popular Name:</strong>

<span id="lblPopularName" runat="server"></span></li>
<li>
<strong class="strongDetails">Establishment:</strong>
<span runat="server" id="lblEstablishment"></span>
</li>
<li>
<strong class="strongDetails">Management:</strong>
<span id="lblUniversityCategoryName" runat="server"></span></li>
</ol>
</div>
</div>