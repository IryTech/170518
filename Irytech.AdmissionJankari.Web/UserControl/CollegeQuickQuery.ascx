<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeQuickQuery.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeQuickQuery" %>
<input type="hidden" id="hdnCourse" />
<input type="hidden" id="hdnCityId" />
<input type="hidden" id="hdnBranchCourseId" />
<input type="hidden" id="hndcollegeName" />
<h4 class="streamCompareH3">
    Quick Query
</h4>	
<hr class="hrline" />
<div class="quickquery login">
    
    <div class="boxPlane" style="border: 1px solid #fff;">
    <span>
        <label id="lblerrMsg" class="hide"></label>
    </span>
        <ul class="horizontal">
            <li><strong class="liststrong textalignRight rightmargin paddingTopBot">
                <%=Resources.label.Name%></strong>
                <input type="text" id="txtName" class="width40Percent fleft" placeholder="Enter your name" title="Enter your name" />
                <span class="errormsgSpan"><label id="lblNameError" class="error" title="Please Enter Name">
                </label></span>
            </li>
           
            <li><strong class="liststrong textalignRight rightmargin paddingTopBot">
                <%=Resources.label.Email%></strong><input type="text" class="width40Percent fleft" id="txtEmailId" placeholder="Enter your email id"
					title="Enter your email id" />
                <span class="errormsgSpan"><label id="lblEmailIdError" class="error"  title="Please Enter Email Id">
                </label></span>
            </li>

			<li>
				<strong class="liststrong textalignRight rightmargin paddingTopBot">
					<%=Resources.label.Mobile%></strong>
				<input type="text" id="txtMobile" class="width40Percent fleft" placeholder="Enter your 10 digit mobile number"
					title="Enter your 10 digit mobile number" />
				<span class="errormsgSpan"><label id="lblNumberError" class="error" title="Please Enter Mobile"></label></span>
			</li>

            <li><strong class="liststrong textalignRight rightmargin paddingTopBot">
                <%=Resources.label.Query%>
            </strong>
                <textarea id="txtQuery" class="txtField width40Percent fleft" placeholder="Enter your query" title="Enter your query"></textarea>
                <span class="errormsgSpan"><label id="lblQueryError" class="error" title="Please Enter Query">
                </label></span>
            </li>
        </ul>
    </div>
    <footer>
              <input type="checkbox" checked="checked" /> I agree 
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Terms-and-Conditions").ToLower()%>' target="_blank"  >T&amp;C</a> and  
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Privacy-Policy").ToLower()%>' target="_blank" >Privacy Policy</a>
      
            <input type="button" title="Click to finish post query" class="button" onclick="return saveQuery()" id="butSubmit" value="<%=Resources.label.SaveQuery%>" />
            <input type="button" title="Clear Field " id="butClear" onclick="ClearControl()" value="Clear" />
        
            <label id="lblMsg" title="Message">
            </label>
    </footer>
  
       
</div>   <span id="spnMsg" class="hide">
          <label id="lbllErMsg"  title="Message" class="msg" >
              You must fill in all of the mandatory fields
          </label>
             </span>
<script type="text/javascript">



    function ClearControl() {
        $("#spnMsg").addClass("hide");
        $("#lblNameError").addClass("hide");
        $("#lblEmailIdError").addClass("hide");
        $("#lblQueryError").addClass("hide");
        $("#lblNumberError").addClass("hide");
        $("#txtQuery").val('');
        $("#txtEmailId").val('')
        $("#txtMobile").val('');
        $("#txtName").val('');
           }

    function saveQuery() {
        var isErrorRegis = false;

        if (!validateQuery()) {
           
            isErrorRegis = true;
        }
        if (isErrorRegis) {
            $("#spnMsg").removeClass("hide");
          
            return false;

        } else {
            $("#spnMsg").addClass("hide");
            InsertUserQuery($("#txtName"), $("#txtEmailId"), $("#txtMobile"), $("#txtQuery"), $("#hdnCityId"), $("#hdnBranchCourseId"), $("#hdnCourse"), $("#hndcollegeName"));
        }

    }
    function validateQuery() {
        var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
        var name = $("#txtName");
        var mono = $("#txtMobile");
        var emailId = $("#txtEmailId");
        var mobileNo = /^[0-9]*$/; 
        var otherCity = $("#ddlCity option:selected");
        var query = $("#txtQuery");
        var isErrorRegis = false;

        if (name.val() == "") {
            $("#lblNameError").removeClass("hide");
            $("#lblNameError").text("Field Name cannot be blank");

            name.focus();

            isErrorRegis = true;
        }
        else {
            if (name.val().length < 2) {
                $("#lblNameError").removeClass("hide");
                $("#lblNameError").text("Field Name is not valid");
                isErrorRegis = true;
            } else {
                $("#lblNameError").text('');
            }
        }


        if (mono.val() == "") {
            $("#lblNumberError").removeClass("hide");
            $("#lblNumberError").text("Field Mobile Number cannot be blank");
            mono.focus();
            isErrorRegis = true;
        }
        else if (mono.val().length < 10 || mono.val().length > 10) {
            $("#lblNumberError").removeClass("hide"); mono.focus();
            $("#lblNumberError").text("Provide 10 digit mobile number");
            isErrorRegis = true;
        }
        else if (!mobileNo.test(mono.val().trim())) {
            $("#lblNumberError").removeClass("hide"); mono.focus();
            $("#lblNumberError").text("Provide 10 digit mobile number in numeric");
            isErrorRegis = true;
        }


        else {
            $("#lblNumberError").text('');
        }

        if (emailId.val() == "") {
            $("#lblEmailIdError").removeClass("hide");
            $("#lblEmailIdError").text("Field Email cannot be blank");
            emailId.focus();
            isErrorRegis = true;
        }
        else if (!reEmail.test(emailId.val())) {
            $("#lblEmailIdError").removeClass("hide"); emailId.focus();
            $("#lblEmailIdError").text("Incorrect Email format, please try again");
            isErrorRegis = true;
        } else {
            $("#lblEmailIdError").text('');
        }

        if (query.val() == '') {
            query.focus();
            $("#lblQueryError").removeClass("hide");
            $("#lblQueryError").text("Field Query cannot be blank");
            isErrorRegis = true;
        }
        else {
            $("#lblQueryError").text('');

        }

        return !isErrorRegis;
    }


    function InsertUserQuery(name, email, number, query, city, collegeCourseId, courseId, collegeName) {
       
        var dataQuery = '{"name":"' + name.val() + '","email":"' + email.val() + '","number":"' + number.val() + '","query":"' + query.val() + '","cityName":"' + city.val() + '","collegeCourseId":"' + collegeCourseId.val() + '","courseId":"' + courseId.val() + '","collegeBranchName":"' + collegeName.val() + '"}';
        var url = "/WebServices/CommonWebServices.asmx/InsertCollegeQuickQuery";
        $.ajax({
        	type: "POST",
        	url: url,
        	data: dataQuery,
        	async: true,
        	contentType: "application/json; charset=utf-8",
        	dataType: "json",
        	success: function (response)
        	{
        		$("#lblerrMsg").removeClass("hide");
        		$("#lblerrMsg").addClass("success");
        		$("#lblerrMsg").text(response.d);
        		$("#lblerrMsg").fadeOut(30000);
        		ClearControl();
        	},
        	error: function (xml, textStatus, errorThrown)
        	{
        		$("#lblerrMsg").removeClass("hide"); ;
        		$("#lblerrMsg").addClass("error");
        		//alert(xml.status + "||" + xml.responseText);
        		ClearControl();
        	}
        });
    }
</script>
