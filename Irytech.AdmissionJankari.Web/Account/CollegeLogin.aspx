<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeLogin.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Account.CollegeLogin" %>

<%@ Register TagPrefix="ADMJ" TagName="Forgot" Src="~/UserControl/ForgetPassword.ascx" %>
<%@ Register TagPrefix="ADMJ" TagName="Testimonial" Src="~/UserControl/AdmissionJankariTestimonial.ascx" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<div>  
<div class="one_half fleft last bgblue border"> 
 <h1 style="padding:0 5px;">Create a profile to get included in the search results instantly.
     <strong style="line-height:35px; font-weight:bold;">It's free and easy.</strong></h1>
     <hr class="hrline" />
     <div class="marginall" style="height:375px; overflow:hidden; border:1px solid #fff;">
        <div id="coll_testimonialSlider">
            <ADMJ:Testimonial ID="UcTestimonial" runat="server" />
          
        </div>
    </div>
    </div>
    <div class="one_half fright last bgblue" style="border-radius:5px; padding:5px; border:1px solid #005eb1; box-shadow:0px 2px 4px 2px #234d7d;"> 

    <h3 class="paddinleft">College Login</h3>
    
    <hr class="hrline" />
    <div class="box marginall" style="background-color:#f1f1f1;">
    <fieldset id="fldLogin">
         <p class="paraStrong">Please enter your email address and password below to access your account.</p>
                        <ol class="style fleft tabmargin">
                        <li class="lftbor"><asp:Label ID="lblError" runat="server" Text="" Visible="false" ></asp:Label></li>
                        <li class="unorederlist"><strong style="font-size:15px !important;">Email:</strong><hr class="hrline" />
                        <asp:TextBox ID="txtUserName"  runat="server" placeholder="Enter email id with which you have registered" style="max-width:270px; text-indent:15px; width:100%;" TabIndex="1" ToolTip="Enter email id with which you have registered with us"></asp:TextBox>
                         <span class="errormsgSpan" style="padding-left:0px;"><asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" SetFocusOnError="True" ToolTip="Email is required." Display="Dynamic" ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ValidationGroup="LoginUserValidationGroup" ID="revEmail" Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtUserName">
                        </asp:RegularExpressionValidator>
                        </span>
                        </li>
                       <li  class="unorederlist"><strong style="font-size:15px !important;">Password: </strong><hr class="hrline" />
                         <asp:TextBox ID="txtPassword" runat="server" TabIndex="1" TextMode="Password" placeholder="Enter password" style="max-width:260px; width:100%;" ToolTip="Enter password"></asp:TextBox>
                          <span class="errormsgSpan" style="padding-left:0px;"><asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" SetFocusOnError="True" ToolTip="Field Password cannot be blank" Display="Dynamic" ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator></span>
                         </li>
                         <li  class="fleft lftbor"><asp:Button ID="LoginButton" CssClass="ultimateLink btnpadding" runat="server" Text="Sign In"  ValidationGroup="LoginUserValidationGroup" OnClick="LoginButtonClick" />
                        <a class="rightImglink fleft" style=" margin-left:17%; margin-top:5px; font-size:12px;" href="#" id="sndFrogetPwd" onclick="OpenForgetPopUp();return false;">Forgot Password</a>
                        </li>
                        </ol>
                        </fieldset>
    <div class="marginall">
          <h3 class="collloginh3">New to AdmissionJankari.com ? </h3>
          <hr class="hrline" />                               
        <div >
        <ol class="tabmargin">
        <li class="fleft"><span class="collegeLoginSpan">Create &amp; Update</span>
        <span class="collegeSpanTag">your college profile</span><hr class="hrline" /><a class="ultimateLink btnpadding" href="<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Account/"+"College-Registeration").ToLower()%>">Register Free!</a></li>
         
        <li class="lftbor"></li>
        </ol></div>
        
        <div class="clear"></div> 
        </div>
    </div>
    </div>
    <div class="clearBoth"></div>
 </div>
<div id="fade"></div>
<div class="popup_block"  id="divForgot">
        <ADMJ:Forgot ID="ucForgot" runat="server" />
    </div><script type="text/javascript" src="/Js/S3Silder.js"></script>
<script type="text/javascript">
    $("#<%=txtUserName.ClientID %>").focus();
    function OpenForgetPopUp() {
        $("#msg").html("");
        $("#lblEmailIdError").addClass("hide");
        OpenPoup('divForgot', 650, 'sndFrogetPwd');
    }
</script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#coll_testimonialSlider').s3Slider({
                    timeOut: 7000
                });
            }); 
  </script>
   <style type="text/css">
       #coll_testimonialSlider
       {
           width: auto;
           height: 375px;
           position: relative; /* important */
           overflow: hidden; /* important */
       }
       #coll_testimonialSliderContent
       {
           width: auto;
           position: absolute;
           top: 0;
           margin-left: 0;
       }
       .coll_testimonialSliderImage
       {
           float: left;
           position: relative;
           display: none;
       }
       .clear
       {
           clear: both;
       }
       
       .top
       {
           top: 0;
           left: 0;
       }
       .bottom
       {
           bottom: 0;
           left: 0;
       }
   </style>
        </asp:content>
