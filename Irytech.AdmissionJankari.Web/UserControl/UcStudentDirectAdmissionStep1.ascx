<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentDirectAdmissionStep1.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentDirectAdmissionStep1" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="~/UserControl/Login.ascx" TagPrefix="ADMJ" TagName="Login" %>

<div style="width:942px; margin:5px auto; display:block; height:35px;">
    <img src="/image.axd?Common=instrunction-page.jpg" alt="Instruction" /></div>
<div class="one_half  fleft last marginTop box1 mainBG heightThreeH"  >

    <h3 class="streamCompareH3" >
        Candidate Registration</h3>
    <hr class="hrline" />
    <div class="box  marginall" style="height:215px;">
        <h3 class="directh3">
            3 Simple Steps to Apply Online</h3>
        <ol class="direct">
            <li></li>
            <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                1</span>Fill the Application Form</span>  </li>
            <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                2</span>Appear in online counseling</span></li>
            <li><span class="linedotted">.....</span><span class="mainStepSpan"><span class="stepsSpan">Step
                3</span> Confirm your seat</span></li>
        </ol>
        
           <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+  (IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/Counselling/OnlineApplicationForm/").ToLower() %>' style="margin:5px 30px; display:inline-block;background: green!important; padding:7px 10px; color:#fff;"  class="button" title="Proceed to apply Online">Proceed to Apply Online</a>
         
    </div>
</div>
<div class="one_half fright last marginTop box1 mainBG heightThreeH">
    <h3 class="streamCompareH3">
         Already Registered</h3>
    <hr class="hrline" />
    <div class="box marginall">
       <ADMJ:Login ID="ucLogin" runat="server" />
    </div>
</div>
