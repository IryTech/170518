<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCollegeListRelatedUniversity.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCollegeListRelatedUniversity" %>
<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<asp:UpdatePanel runat="server" ID="updateCollegeCityList">
<ContentTemplate>
<div class="box1 fleft">
  <h2>Affiliated Colleges</h2>
  <hr class="hrline" />
   <div class="boxPlane fleft">
    <asp:Repeater  ID="rptCollegeList" runat="server">
         <ItemTemplate>
        <div class="ucDiv">
        <ul class="vertical  marginbottom" >
        <li>
                 <a rel="canonical" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Eval("CourseName").ToString())+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' rel="canonical"  title='<%# Eval("CollegeBranchName")%>'>
                    <img  src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("CollegeBranchLogo").ToString()) ?"NoImage.jpg":Eval("CollegeBranchLogo")) %>' alt='<%# Eval("CollegeBranchName") %>' height="50px" width="50px" /></a>
                </li>
                <li class="width70Percent">
                
                    <a rel="canonical" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Eval("CourseName").ToString())+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' rel="canonical"  title='<%# Eval("CollegeBranchName")%>'><%# Eval("CollegeBranchName")%>
                      
                    </a>
                    </li>
                     <li class="width70Percent"><span class="clglistbyCitySpan">
                       Course:<%# Eval("CourseName")%></span><div class="clearBoth"></div><span class="clglistbyCitySpan">
                    Est:<%# Eval("CollegeBranchEst")%>| <%# Eval("CollegeManagementType")%></span>
                    </li>
                    <span class="clearBoth dispBlock"></span>
              </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>