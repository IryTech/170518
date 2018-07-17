<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAdmission.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ucAdmission" %>



<div id="divRegister" >
<h4 class="streamCompareH3">
            Apply for direct Admission in the College 
                 </h4>

                 <hr class="hrline" />
       <div id="divPoupup" class="quickquery login">
		 
    <span>
        <label id="lblerrMsg" class="hide">
        </label>
    </span>
    
      
       <div class="boxPlane" id="fldRegister" style="border:1px solid #fff;">
       <ul class="horizontal">
            
           
            <li><strong class="liststrong textalignRight rightmargin paddingTopBot"><%=Resources.label.Name%></strong>
                <input type="text" id="txtNameForRegis" title="Enter your name" class="width40Percent fleft" placeholder="Enter your name" />
                <span class="errormsgSpan"><label id="lblNameRegisError" class="error" title="Please Enter Name">
                </label></span>
            </li>
            
            <li><strong class="liststrong textalignRight rightmargin paddingTopBot"><%=Resources.label.Email%></strong><input type="text" class="width40Percent fleft" id="txtEmailIdForGregis"
					placeholder="Enter your email id " title="Enter your email id" />
               <span class="errormsgSpan"> <label id="lblEmailIdRegisError" class="error" title="Please Enter Email Id">
                </label></span>
            </li>
		   <li>
			   <strong class="liststrong textalignRight rightmargin paddingTopBot">
				   <%=Resources.label.Mobile%></strong>
			   <input type="text" id="txtMobileNumber" title="Enter your 10 digit mobile number" class="width40Percent fleft"
				   placeholder="Enter your 10 digit mobile number" />
			   <span class="errormsgSpan"><label id="lblNumberRegisError" class="error" title="Please Enter Mobile"></label></span>
		   </li>

             <li><strong class="liststrong textalignRight rightmargin paddingTopBot"><%=Resources.label.DOB%></strong>
             <asp:TextBox runat="server" ID="txtDob" title="Please Enter Dob" class="width40Percent fleft"  placeholder="Enter your Dob"></asp:TextBox>
             <Ajex:CalendarExtender ID="ajexSaleExectEndDate2"  TargetControlID="txtDob"  Format="dd/MM/yyyy" PopupPosition="Right"  runat="server" CssClass="CalendarCSS" PopupButtonID="txtDob"></Ajex:CalendarExtender>
               <span class="errormsgSpan"> <label id="lblDob" class="error" title="Enter your Date of birth">
                     
                </label></span>
            </li> 
        </ul>
        <footer>
            <input type="checkbox" checked="checked" />
            I agree <a href="/Terms-and-Conditions" target="_blank">T&amp;C</a> and <a href="/Privacy-Policy" target="_blank">Privacy Policy</a> 
            <input type="button" title="Submit the for Registrrtion" class="button" onclick="ResisterUser()" id="butSubmit" value="Proceed" />
           <input type="button" title="Clear Field " id="butClear" onclick="ClearRegistationControl()" value="Clear" />
                   
        </footer>
              
      </div>      
    
    
</div><span id="lbllErrMsg" class="hide">
          <label id="lbllErMsg"  title="Message" class="msg" >
              You must fill in all of the mandatory fields
          </label>
             </span>
    </div>
    
    <script type="text/javascript" defer="defer">
   
        function ResisterUser() {
            var isErrorRegis = false;
            
            if (!ValidateRegistationForm()) {

                isErrorRegis = true;
            }
            if (isErrorRegis) {
          $("#lbllErrMsg").removeClass("hide");
                return false;

            } else {
                $("#lbllErrMsg").addClass("hide");
                Register();
            }
           
                
        
        }

        function Register() {
          
            $("#lblerrMsg").html('');
            var dataQuery = '{"mobileNo":"' + $("#txtMobileNumber").val() + '","emailId":"' + $("#txtEmailIdForGregis").val() + '","name":"' + $("#txtNameForRegis").val() + '","dob":"' + $("#<%=txtDob.ClientID %>").val() + '"}';
        $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/RegisterStudent",
                data: dataQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#divDirectAdmssion").addClass("popup_block");
                    InsertCollegePrefer($("#hdnCourse").val(), $("#hdnCollege").val());
                    InsertUpdateUserTransctionalDetails($("#hdnCourse").val());
                    InsertCityPrefer($("#hdnCity").val());
                    var msg = InsertInterestedCollegePrefer($("#hdnCollegeCourseId").val());
                    OpenPoup("divPaymentUser", "750", "sndDirect");
                    $("#divRegister").hide();
                    $("#lblerrMsg").removeClass("error");
                    $("#lblerrMsg").addClass("sucess");
                    $("#lblerrMsg").html(msg);
                    $("#lblerrMsg").fadeOut(2500); window.ClearLoginControl();
                }, error: function (response) {
                 
                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("error");
                    $("#lblerrMsg").html(response.d);
                    $("#lblerrMsg").fadeOut(2500);

                }
                
            });
        }
        function ValidateRegistationForm() {
          var emailId=$("#txtEmailIdForGregis");
            var mono =$("#txtMobileNumber");
            var name =$("#txtNameForRegis");
            var dob = $("#<%=txtDob.ClientID %>");
            var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
            var mobileNo = /^[0-9]*$/; 
            var isErrorRegis = false;
            if (name.val() == "") {
            	$("#lblNameRegisError").html("Field Name cannot be blank");

                name.focus();
              
                isErrorRegis = true;
            }



            if (mono.val() == "") {
            	$("#lblNumberRegisError").html("Field Mobile Number cannot be blank");

                mono.focus();
                isErrorRegis = true;
            }
            else if (!mobileNo.test(mono.val()) && mono.val() > 0) {
            	$("#lblNumberRegisError").html("Provide 10 digit mobile number");

                isErrorRegis = true;
            }

            else if (mono.val().length < 10 || mono.val().length > 10) {
            	$("#lblNumberRegisError").html("Provide 10 digit mobile number in numeric");
     
                isErrorRegis = true;
            }

            if (emailId.val() == "") {
            	$("#lblEmailIdRegisError").html("Field Email cannot be blank");

                emailId.focus();
                isErrorRegis = true;
            }
            else if (!reEmail.test(emailId.val())) {
            	$("#lblEmailIdRegisError").html("Incorrect Email format, please try again");
                isErrorRegis = true;
            }
           
            if (dob.val() == "") {
                $("#lblDob").html("Field Date Of Birth cannot be blank");
                dob.focus();
                isErrorRegis = true;
            }
          
            return !isErrorRegis;

        }
        function ClearRegistationControl() {
            $("#lbllErrMsg").addClass("hide");
            $("#txtEmailIdForGregis").val('');
            $("#txtMobileNumber").val('');
            $("#txtNameForRegis").val('');
            $("#<%=txtDob.ClientID %>").val('');
            $("#lblNameRegisError").html('');
            $("#lblNumberRegisError").html('');
            $("#lblEmailIdRegisError").html('');
            $("#lblDob").html('');
        }

	</script><style>.back{border:currentColor: #ff7171}</style>