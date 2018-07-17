<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error404.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.error404" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="~/UserControl/CollegeSearch.ascx" TagPrefix="AJ" TagName="CollegeSearch" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">


<div class="boxPlane" id="content" style="background-image:url(/image.axd?Common=404.png); border:0px solid #fff; background-repeat:no-repeat; background-position:top right;">
<h1 style="color:Red; padding-left:20px;">Oops, page not found</h1>

<ul id="text">
<li><h3>Somebody really liked this page, or maybe your mis-typed the URL. Try again!:(</h3></li>
<li><p>Sorry, Evidently the document you were looking for has either been moved or no longer exists. Please use the<br /> navigational links to the right to locate additional resources and information.</p></li>
<li><h3>Lost? We suggest...</h3></li>
</ul>
<div style="width:60%; margin-left:20px;"><AJ:CollegeSearch ID="collegesearch" runat="server" /> </div>

</div>

</asp:Content>