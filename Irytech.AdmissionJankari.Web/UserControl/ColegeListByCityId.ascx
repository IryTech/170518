<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColegeListByCityId.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ColegeListByCityId" %>
<%@ Register src="CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>
<%--<asp:UpdatePanel runat="server" ID="updateCollegeCityList">
<ContentTemplate>--%>
<div class="box1 fleft" runat="server" id="divCollegeInCity">
<h3 class="streamCompareH3">Colleges in - <asp:Label  runat="server" ID="lblCollegeName" ForeColor="Maroon" Font-Size="15px" Font-Italic="true" Text=""></asp:Label> </h3>
<hr class="hrline" />

<div class="boxPlane">
    
    <asp:Repeater  ID="dtlCollegeCity" runat="server">
         <ItemTemplate>
            <div class="ucDiv">
        <ul class="vertical marginbottom">
        <li>
                 <a href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower()%>'>
                    <img src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("CollegeBranchLogo").ToString()) ?"NoImage.jpg":Eval("CollegeBranchLogo")) %>' alt='<%# Eval("CollegeBranchName") %>' height="40px" width="40px" /></a>
                </li>
                <li class="width70Percent">
                
                    <a href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>' rel="canonical"  title='<%# Eval("CollegeBranchName")%>'><%# Eval("CollegeBranchName")%>
                      
                    </a></li>
                    <li>
                    <span class="clglistbyCitySpan"> Est:<%# Eval("CollegeBranchEst")%>| <%# Eval("CollegeManagementType")%>&nbsp;College</span> 
                    </li>
                    <span class="clearBoth dispBlock"></span>
              </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <AJ:CustomPaging ID="ucCustomPagingCollegeByCity" runat="server" />
   
    </div>
     </div>
   
  <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>