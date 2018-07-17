<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentAcademic.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentAcademic" %>
<%@ Register Src="~/UserControl/UcHighSchoolInfo.ascx" TagPrefix="ADMJ" TagName="Student10AcademicInfo" %>
<%@ Register Src="~/UserControl/UcIntermediateInfo.ascx" TagPrefix="ADMJ" TagName="Student12AcademicInfo" %>
<%@ Register Src="~/UserControl/UcGraduateInfo.ascx" TagPrefix="ADMJ" TagName="StudentGrdAcademicInfo" %>
<%@ Register Src="~/UserControl/UcDiplomaInfo.ascx" TagPrefix="ADMJ" TagName="StudentDicAcademicInfo" %>

<div style="margin:5px;">
    <%--<h3 class="wizardTopHeading">Your Academic Details</h3>--%>
    <asp:UpdatePanel ID="updateCourse" runat="server">
    <ContentTemplate>
    <ul>
    <li id="ltrEntry" runat="server" >
    <fieldset> <legend style="background-color:#000; color:Yellow;" >* please select one of the  following options if you are diploma holder</legend>
    
    <span><asp:RadioButtonList ID="rbtCourseAdmissionEligibilty" AutoPostBack="true"  Width="400px" RepeatDirection="Horizontal" 
            runat="server" 
            onselectedindexchanged="rbtCourseAdmissionEligibilty_SelectedIndexChanged">
      </asp:RadioButtonList> </span>
        </fieldset>
    </li>
    <li>
     <ADMJ:StudentGrdAcademicInfo  ID="grdInfo" runat="server" />
    </li>
    
    <li>
    <ADMJ:Student12AcademicInfo  ID="Student12Info" runat="server" />
    </li>
    <li>
    <ADMJ:Student10AcademicInfo  ID="Student10info" runat="server" />
    </li>
    <li>
    <ADMJ:StudentDicAcademicInfo  ID="dicInfo" runat="server" />
    
    </li>
    </ul>
    </ContentTemplate>
    </asp:UpdatePanel>
</div>

