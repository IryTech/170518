<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopRankedColleges.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.TopRankedColleges" %>
<div id="tab" class="tabbed_area">
    <h2 class="streamCompareH3 fleft">Top Ranked Colleges</h2>
    <label class="fright marginTop1 rightmargin" id="totalRecords"></label>
    <hr class="hrline" />
    <asp:HiddenField runat="server" ID="hdnTopCourse"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnTopCourseName"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnTopCourseInAppSetting"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnTopAssociation"></asp:HiddenField>
    <ul class="tabs" id="ulTopRanked">
    </ul>
</div>
<div class="tab_container ">

    <div id="noRecords" class="success" style="height: 25px; text-align: center; display: none;
        padding: 10px 0 0 0;">
        Sorry no records were found.
    </div>
</div>
<div class="TopPage pagination" id="topPrivatePaging">
</div>
<center id="loading" style="display: none">
     <img src='/image.axd?Common=LoadingImage.gif' alt="Please Wait Loading College..." />
</center>
<asp:HiddenField runat="server" id="hdnTopColleges">
</asp:HiddenField> <input type="hidden" id="hdnTopTab" value="0"/>

