<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdmissionJankariTestimonial.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.AdmissionJankariTestimonial" %>
 
 <ul id="coll_testimonialSliderContent">
  <asp:Repeater ID="rptTestimonial" runat="server" >

    <ItemTemplate>
    <li class="coll_testimonialSliderImage">
    <span class="testimonialImg Imgarrow">
    <img width="100px" height="100px" title='<%# Eval("AjTestimonialPersonName")%>' alt='<%# Eval("AjTestimonialPersonName")%>'  src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("AjTestimonialPersonImage").ToString()) ?"NoImage.jpg":Eval("AjTestimonialPersonImage")) %>' />
   </span><span class="testimonialName">
      <span class="testimonialPerson"><%# Eval("AjTestimonialPersonName")%></span>
          <hr class="hrline" />
        <span class="testimonialdesi"><%# Eval("AjTestimonialPersonDesignation")%></span>
        </span>
        
     <%# Eval("AjTestimonialText")%>
   
    </li>
             
    </ItemTemplate>
            </asp:Repeater>
          </ul>
        