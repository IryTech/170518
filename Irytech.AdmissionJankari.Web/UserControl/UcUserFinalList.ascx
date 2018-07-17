<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcUserFinalList.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcUserFinalList" %>
<asp:UpdatePanel ID="updateCollegeList" runat="server">
    <ContentTemplate>
    
             <div>
             <asp:Repeater ID="rptCollegeDetails"  runat="server"  >
                  
                    <ItemTemplate>
                        <div class="boxPlane marginall">
                            <ul class="vertical">
                                <asp:HiddenField ID="hndCollegeBranchCourseId" runat="server" Value=' <%# Eval("AjCollegeBranchCourseId")%>' />
                               
                                                               
                                <li class="width98Percent">
                                    <h3>
                                        <a class="astrong" target="_blank" href='<%#IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"College-Details/" +IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse(Convert.ToString(Eval("AjCourseName")))+"/"+ IryTech.AdmissionJankari.Components.Utils.RemoveIllegalCharacters(Convert.ToString(Eval("AjCollegeBranchName"))) %>'
                                            title=" <%# Eval("AjCollegeBranchName")%>">
                                            <%# Eval("AjCollegeBranchName")%>
                                        </a>
                                    </h3>
                                 <span class="fright"><label> College priority : </label><label class="aColor">
                                    <%# String.Format("{0}", Eval("AjAdmissionPriority") ?? "N/A")%>
                                        </label>                               
                               </span>
                               </li>

                            </ul>
                            <spa class="clearBoth dispBlock">
                            </span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <hr class="hrline" />
  <div class="marginTop">
  <div class="boxPlane marginall">
   <p>To confirm above list of preferred college and to proceed for admission, please pay the amount of  <strong>Rs.25,000</strong>.</p>
                    <asp:Button ID="btnConfirm" Text="Pay Now"  
         CssClass="button" runat="server" onclick="btnConfirm_Click" ></asp:Button><hr class="hrline" />
                
                
                   <strong>Please note:</strong>
                   <ol>
                   <li>College is only authority and reserves final decisions for your admissions. In any case college may deny your admission, it may or may not state the cause.</li>
                   <li>AdmissionJankari.com is not be at all responsible for any acceptance or denial of admission. </li>
                   <li>Please do not accept or offer any form of donation, if found AdmissionJankari.com may raise the legal action.</li>
                   </ol>
                   </div>
 </div>
    </ContentTemplate>
</asp:UpdatePanel>