<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Registeration.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.Registeration" %>

       <div id="divPoupup" class="quickquery login" style="padding:5px;">
       <asp:HiddenField runat="server" ID="hdnCourseId" runat="server"/><input type="hidden" id="ExamName"/>
    <span>
        <label id="lblerrMsg" class="hide">
        </label>
    </span>
    <h2 style="font-size:1.5em;" id="lblExam">
            

              </h2>
		
              <hr class="hrline" />
            <fieldset class="boxBody " id="fldRegister" >
            <ul>
            
            
            <li><strong class="liststrong"> <%=Resources.label.Name%></strong>
                <input type="text" id="txtRegisName" title="Enter your name" onfocus="clear(this)" placeholder="Enter your name" /> 
                <span id='nameError' class="error" style="clear:both !important; display:block; margin-left:20% !important;"></span> 
            </li>
               <li><strong class="liststrong"> <%=Resources.label.Email%></strong><input type="text" id="txtRegisEmailId"
					   onfocus="clear(this)" placeholder="Enter your email id" title="Enter your email id" />
                 <span id='EmailError' class="error" style="clear:both !important; display:block; margin-left:20% !important;"></span>
            </li> 
            <li><strong class="liststrong"> <%=Resources.label.Mobile%></strong>
                <input type="text" id="txtMobileNumber" title="Enter your 10 digit mobile number" onfocus="clear(this)"
					placeholder="Enter your 10 digit mobile number" />
                  <span id='MobileNumberError' class="error" style="clear:both !important; display:block; margin-left:20% !important;"></span>
            </li>
         
       
        <li>
            <input type="checkbox" checked="checked" />
            I agree <a href="/Terms-and-Conditions" target="_blank">T&amp;C</a> and <a href="/Privacy-Policy" target="_blank">Privacy Policy</a> </li>
           <li> <input type="button" title="Click to get result" class="button" onclick="ResisterUser()" id="butSubmit" value="Get Result" />
           <input type="button" title="Clear Field " id="butClear" onclick="ClearRegistationControl()" value="Clear" /></li>
                   
        </ul> 
              
            
    </fieldset>
   <span id="spnMsg" class="hide" >
          <label id="lbllErMsg"  title="Message" class="msg" style="text-align:center !important;" >
              You must fill in all of the mandatory fields
          </label>
             </span>
    
</div> 
 
 
    <script type="text/javascript">
         
        function ResisterUser() {

            var isErrorRegis = false;

            if (!ValidateRegistationForm()) {

                isErrorRegis = true;
            }
            if (isErrorRegis) {
              $("#spnMsg").removeClass("hide");
                return false;

            } else { 
                $("#spnMsg").addClass("hide");
                Register(MessageShow);
            }
        }

        function Register(messageShow) {
            $("#lblerrMsg").text('');
            var dataQuery = '{"mobileNo":"' + $("#txtMobileNumber").val() + '","emailId":"' + $("#txtRegisEmailId").val() + '","name":"' + $("#txtRegisName").val() + '","courseId":"' + $("#<%= hdnCourseId.ClientID %>").val() + '"}';

            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/CheckReult",
                data: dataQuery,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: messageShow,
                error: function (response) {
                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("error");
                    $("#lblerrMsg").html(response.d);
                },
                
            });
        }
       
        function ValidateRegistationForm() {
            var emailId = $("#txtRegisEmailId");
            var mono = $("#txtMobileNumber");
            var name = $("#txtRegisName");
            var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
            var mobileNo =  /^[0-9]*$/;
            var isErrorRegis = false;
            if (name.val() == "") {
                $("#nameError").html("Field Name cannot be blank");
      
                name.focus();

                isErrorRegis = true;
            }
            else {
                $("#nameError").html('');
            }


            if (mono.val() == "") {
                $("#MobileNumberError").html("Field Mobile Number cannot be blank"); 
                mono.focus();
                isErrorRegis = true;
            }
            else if (!mobileNo.test(mono.val()) && mono.val() > 0) {
                $("#MobileNumberError").html("Provide 10 digit mobile number");
                isErrorRegis = true;
            }

            else if (mono.val().length < 10 || mono.val().length > 10) {
                $("#MobileNumberError").html("Provide 10 digit mobile number in numeric!"); 
                isErrorRegis = true;
            }else {
                $("#MobileNumberError").html('');
            }

            if (emailId.val() == "") {
                $("#EmailError").html("Field Email cannot be blank"); 
                emailId.focus();
                isErrorRegis = true;
            }
            else if (!reEmail.test(emailId.val())) {
                $("#EmailError").html("Incorrect Email format, please try again"); 
                isErrorRegis = true;
            }else {
                $("#EmailError").html('');
            }

           
            return !isErrorRegis;

        }
        function ClearRegistationControl() { $("#spnMsg").addClass("hide");
            $("#txtRegisEmailId").val('');
            $("#txtMobileNumber").val('');
            $("#txtRegisName").val('');
            $("#EmailError").html('');$("#MobileNumberError").html(''); $("#nameError").html('');
        
        }

        function clear(control) { $("#spnMsg").addClass("hide");
            
            $(control).val('');

            $(control).removeClass("back");
        }
    function   MessageShow(reponse) {
       // $("#loginRegisterPanel").addClass("hide1");
//                    $("#divProfile").removeClass("hide1");
//                    $("#divLogout").removeClass("hide1");
//                    $("#lblUserName").text('<%= Session["LoginUserName"] %>'.toString());
                    ClearRegistationControl();
                    $("#divRegisteration").hide(); $("#fade").hide();
                    ShowMessage($("#ExamName").val());
        }
	</script>
 