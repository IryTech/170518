<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Testimonials.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.Testimonials" %>
<%@ Register Src="~/UserControl/FckEditorCostomize.ascx" TagName="TestMonialDesc" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="ucCustomPaging" TagPrefix="Aj"  %>

<div class="box1 marginTop">

<div>
<asp:HiddenField ID="hndCollegeTopHirer" runat="server" />
<asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
                 <asp:Label ID="lblInsertUpdate" runat="server" Text="Insert"></asp:Label>

<div id="divAssociation" class="popup_block">
<ul>
<li>
<Aj:TestMonialDesc ID="txtTestimonial" runat="server" />
</li>

<li>
<label>

TestimonialStatus:

</label>
<asp:CheckBox ID="chkStatus" runat="server" />
</li>

<li>

<asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click"/>

</li>
</ul>
</div>


<asp:Repeater ID="rptTestimonilas" runat="server" 
        onitemcommand="rptTestimonilas_ItemCommand">
  <HeaderTemplate>
 <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                  <th>
                                    
                                   Testimonial
                                    </th>

                                   <th>
                                   Action
                                   
                                   
                                   </th>

                                                                     
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
          <td><%#Container.ItemIndex+1 %></td>
        
           <td>
           <%# Eval("Testimonials")%>
           </td>
                     
          <td>
                 


       
                                    <asp:HiddenField ID="hndCollegeBranchCourseID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />    
                                  <asp:LinkButton ID="BtnEdit" runat="server" Text="Edit"  CommandArgument='<%# Eval("TestimonialID")%>' CommandName="Edit" CausesValidation="false"/>

                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                            
                            </FooterTemplate>
</asp:Repeater>

<Aj:ucCustomPaging ID="UcCustomPaging" runat="server" />

</div>
  <script src="../../Js/CommonFrontScript.js" type="text/javascript"></script>
</div>