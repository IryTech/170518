<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcMostViewedCollege.ascx.cs"  Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcMostViewedCollege" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<asp:UpdatePanel runat="server" ID="updateCollegeCityList">
<ContentTemplate>
<div class="box1">
<h3 class="streamCompareH3">Most Viewed College 
   </h3>
   <hr class="hrline" />
  <div class="boxPlane">
    <asp:Repeater  ID="rptMostviewdCollege"  runat="server" >
         <ItemTemplate>
            <div class="ucDiv">
        <ul class="vertical  marginbottom">
        <li>
                <a   href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(new Common().CourseName)+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' title=" <%# Eval("CollegeBranchName")%>"> 
                    <img  src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("CollegeBranchLogo").ToString()) ?"NoImage.jpg":Eval("CollegeBranchLogo")) %>' alt='<%# Eval("CollegeBranchName") %>' height="40px" width="40px" /></a>
                </li>
                <li class="width70Percent">
                
                   <a   href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(new Common().CourseName)+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' title=" <%# Eval("CollegeBranchName")%>"> 
                       
                        <%# Eval("CollegeBranchName")%>
                      
                    </a></li>
                    <li><span class="clglistbyCitySpan">
                    Est:<%# Eval("CollegeBranchEst")%>| <%# Eval("CollegeManagementType")%>&nbsp;College</span>
                    </li>
                    <span class="clearBoth dispBlock"></span>
              </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    
    </div>
    <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>