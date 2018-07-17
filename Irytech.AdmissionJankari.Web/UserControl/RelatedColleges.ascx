<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedColleges.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.RelatedColleges" %>

<div class="box1" id="CollegeHeader" runat="server">
<h3 class="streamCompareH3">Other Courses  </h3>
<hr class="hrline" />
<ul style="margin-left:5px;">
<li>
<asp:Label CssClass="label" ID="lblCollegeName" runat="server"></asp:Label>

</li>
</ul>

<div class="boxPlane">
    
    <asp:Repeater  ID="rptRealtedCollege" runat="server">
         <ItemTemplate>
            <div class="ucDiv">
        <ul class="vertical marginbottom">
        <li class="width20Percent">
                <li class="width70Percent">
                  <a href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("CourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("CollegeBranchName")))).ToLower() %>'>   Course: <%# Eval("CourseName")%>
                   </a>
                    
                    </li>
              </ul>
              <span class="clearBoth dispBlock"></span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
      <div class="clearBoth"></div>
    </div>
     </div>
    