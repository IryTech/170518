<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeTestimonialOnDetails.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeTestimonialOnDetails" %>
<div class="box1"  runat="server" id="divTestimonial" visible="false" >
 <h3 class="streamCompareH3">Testimonial</h3>
 <hr class="hrline" />
 <div class="box"><br />
 <asp:Repeater ID="rptTestimonial" runat="server" >
                            <ItemTemplate>
                            <div >
	                         
                              <%#Eval("AjTestimonial")%>
                               
                               </div>
                            </ItemTemplate>
                            </asp:Repeater>
                            </div>
                            </div>
                 