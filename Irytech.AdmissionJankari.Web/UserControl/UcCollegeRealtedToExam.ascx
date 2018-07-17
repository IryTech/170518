<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCollegeRealtedToExam.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCollegeRealtedToExam" %>

<asp:UpdatePanel runat="server" ID="updateCollegeCityList">
<ContentTemplate>
<div id="CollegeRealtedToExam" runat="server" class="box1">
<h3 class="streamCompareH3">College Realted to <asp:Label ID="lblExamName" runat="server"></asp:Label>
   </h3>
   <hr class="hrline" />
  <div class="boxPlane">
    <asp:Repeater  ID="rptCollegeRealtedToExam"  runat="server" >
         <ItemTemplate>
            <div class="ucDiv">
        <ul class="vertical marginbottom">
        <li>
                 <a rel="canonical" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' rel="canonical"  title='<%# Eval("CollegeBranchName")%>'>
                    <img  src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Convert.ToString(Eval("CollegeBranchLogo"))) ?"NoImage.jpg":Eval("CollegeBranchLogo")) %>' alt='<%# Eval("CollegeBranchName") %>' height="40px" width="40px" /></a>
                </li>
                <li class="width70Percent">
                
                    <a rel="canonical" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' rel="canonical"  title='<%# Eval("CollegeBranchName")%>'><%# Eval("CollegeBranchName")%>
                      
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
     <asp:Panel runat="server" id="pnlPager" CssClass="pagination">
     </asp:Panel>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>