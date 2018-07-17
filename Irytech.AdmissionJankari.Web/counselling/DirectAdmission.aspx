<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectAdmission.aspx.cs"  Inherits="IryTech.AdmissionJankari20.Web.counselling.DirectAdmission" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="~/UserControl/ucFontTestimonialsDetails.ascx" TagPrefix="AJ" TagName="Testimonial" %>
<%@ Register Src="~/UserControl/UcDownloadApplicationfotm.ascx" TagPrefix="AJ" TagName="Applicationform" %>
<%@ Register Src="~/UserControl/UcStudentDirectAdmissionStep1.ascx" TagName="DirectAdmissionStep1" TagPrefix="AJ"%>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<div class="five_sixth fleft last">
<h1 class="fleft">Direct Online Admission</h1>
<hr class="hrline" />
 <AJ:DirectAdmissionStep1 ID="step1" runat="server" />
    
<div class="clearBoth"></div>
  <div class="border box1 marginTop" >
   <h3 class="streamCompareH3">What is direct admission offer?</h3>
   <hr class="hrline" />
   <div class="box">
               
                <p >
                    <strong>AdmissionJankari.com</strong> has boldly started an <strong>Anti Donation Campaign.</strong>
                
                    In this noble event, a huge number of colleges are participating from all over India to offer 
                    <strong>Direct Admission</strong>
                    to students against their vacant seats without any donation, capitation or any hidden charges.                                              
                    It is a great opportunity for students where they can be offered spot admission absolutely at 
                    <strong> No Donation.</strong>
                </p>
            
              
                <h3>How to take direct admission?</h3>
                <hr class="hrline" />

                <p>
                    It is an <strong> Online Admission Counseling</strong>. The step has been taken to save your travel cost and time.<br />
                    You have to fill online counseling form at AdmissionJankari.com,
                    <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+  (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Counselling/OnlineApplicationForm").ToLower() %>'>Click here to Apply Now.</a> It takes only 5 minutes. It is absolutely free! You can also secure priority in admission by choosing premium option.
                    
                   <strong> Direct admission </strong> here refers to the admission against vacant seats after counseling where 
                    participating institutes would select candidates based on the eligibility criterion and seat availability.
                </p>
                </div>
            </div>
 
   
   
  
</div>
<div class="one_sixth last fright" >

<AJ:Applicationform ID="ucApplicationform" runat="server" />
</div>

<hr class="hrline" />
<AJ:Testimonial ID="ucTestimonial" runat="server" />



</asp:content>