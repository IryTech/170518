<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeBasicDetails.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeBasicDetails" %>

<div class="box1">
<h3 class="streamCompareH3" id="lblCollegebranchName" runat="server"></h3>
<hr class="hrline" />
<div class="box">

<ol id="blackListed"  runat="server">
<li><strong class="strongDetails">PopularName</strong>:
<span id="lblPopularNameValue" runat="server"></span>
</li>
<li><strong class="strongDetails" >Course</strong>:
<span id="lblCourseName" runat="server"></span>
</li>
<li><strong class="strongDetails" >Establishment</strong>:
<span id="lblEstYear" runat="server"></span><%--<a class="fright greenlink" style="color:#fff;" title="Book Your Seat" href="">Book Your Seat</a>--%>
</li>
<li><strong class="strongDetails" >Management</strong>:
<span id="lblMgtValue" runat="server"></span>
</li>
<li><strong class="strongDetails" >Affiliated To</strong>:
 <a id="lnkUniversity" runat="server" > <span id="lblUniversityName" runat="server"></span></a>
</li>
</ol>
</div>
</div>