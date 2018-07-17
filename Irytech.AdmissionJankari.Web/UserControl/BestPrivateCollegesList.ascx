<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BestPrivateCollegesList.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.BestPrivateCollegesList" %>
<asp:HiddenField runat="server" ID="hdnPrivateCollege"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnPrivateCourse"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnAssociation"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnCourseNameAtPrivate"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnAppSettingCourseId"></asp:HiddenField>
<div id="privateTabs" class="tabbed_area" >
    <h2 class="streamCompareH3 fleft">
        Best Private Colleges</h2>
    <label style="color: Gray !important;  font-weight: normal !important; font-size: .65em !important;"
        class="fright marginTop1 rightmargin" id="totalRecordsPrivate"> 
    </label>
    <hr class="hrline" />

    <ul class="tabs" id="ulPrivate">
     
    </ul>
</div>
<div class="tab_container" id="tabPrivateContainer" itemscope itemprop='http://schema.org/EducationalOrganization'>
    <div id="noRecordsPrivate" class="success" style="height: 25px;text-align: center;display: none;padding: 10px 0 0 0;">
       Sorry no records were found.
    </div>
</div>

<div class="Pager pagination" id="privatePaging">
</div>
<center id="Privateloading" style="display: none" >
    <img src='/image.axd?Common=LoadingImage.gif'
       alt="Please Wait Loading "/>
</center>
<input type="hidden" id="hdnPrivateTab" value="0"/>

  



