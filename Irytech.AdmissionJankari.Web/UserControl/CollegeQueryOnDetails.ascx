<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeQueryOnDetails.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeQueryOnDetails" %>
<asp:HiddenField  ID="hdnCourse" runat="server" />
<asp:HiddenField  ID="hdnCityId" runat="server" />
<asp:HiddenField  ID="hdnBranchCourseId" runat="server" />
<asp:HiddenField  ID="hdnPageName" runat="server" />
<asp:HiddenField  ID="hdnCollegeEmailId" runat="server" />

<div class="box1">
<h3 class="streamCompareH3">Admission Query </h3>
<hr class="hrline" />
<span ><label id="lblerrMsg" style="display:block;" class="hide">
        </label></span>
<div class="box">
    <ol class="marginleft style">
        <li><strong style="color:#385886; font-size:12px;">To, 
        <asp:Label runat="server"  ID="lblCollegeaNameForQuery"></asp:Label></strong>
        </li>
        <li><strong><%=Resources.label.Name%></strong>
                <input type="text" id="txtName" title="Enter your name" placeholder="Enter your name" />
                <label id="lblNameError" class="error hide" title="Please Enter Name">
                </label>
            </li>
           
            
         
            <li><strong><%=Resources.label.Email%></strong><input type="text" id="txtEmailId" placeholder="Enter your email id"
					title="Enter your email id" />
                <label id="lblEmailIdError" class="error hide" title="Please Enter Email Id">
                </label>
            </li>
		<li>
			<strong>
				<%=Resources.label.Mobile%></strong>
			<input type="text" id="txtMobile" title="Enter your 10 digit mobile number" placeholder="Enter your 10 digit mobile number" />
			<label id="lblNumberError" class="error hide" title="Please Enter Mobile"></label>
		</li>
             <li><strong >Stream:</strong>
                <asp:DropDownList runat="server" ID="slctCoursStream" ToolTip="Select your stream">
            </asp:DropDownList>
                <label id="lblStreamError" class="error" >
                </label>
            </li>
            <li><strong><%=Resources.label.Query%></strong>
                <textarea id="txtColQuery"  title="Enter your query" placeholder="Enter your query"></textarea>
                <label id="lblQueryError" class="error hide" >
                </label>
            </li>
       <li><input type="checkbox" checked="checked" /> I agree 
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Terms-and-Conditions"%>' target="_blank"  >T&amp;C</a> and  
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Privacy-Policy"%>' target="_blank" >Privacy Policy</a></li>
      </ol>
    
    <footer>
            <input type="button" title="Click to finish post query" class="button" onclick="saveQuery()" id="butSubmit" value="<%=Resources.label.SaveQuery%>" />
            <input type="button" title="Clear Field " id="butClear" onclick="ClearControl()" value="Clear" />
        
            <%--<label id="lblMsg" title="Message">
            </label>--%>
        </footer>
        <span id="spnMsg" class="hide">
          <label id="lbllErMsg"  title="Message" class="msg">
              You must fill in all of the mandatory fields
          </label>
             </span>
</div>
 
</div>
<script type="text/javascript">
 
    function ClearControl() {

        $("#spnMsg").addClass("hide");
        $("#lblNameError").addClass("hide");
        $("#lblEmailIdError").addClass("hide");
        $("#lblQueryError").addClass("hide");
        $("#lblNumberError").addClass("hide");
        $("#lblCourseError").addClass("hide");
        $("#lblStreamError").addClass("hide");
        $("#txtColQuery").val('')
        $("#txtEmailId").val('')
        $("#txtMobile").val('')
        $("#txtName").val('')
      
        $("#slctCoursStream").val(0)
    }

    function saveQuery() {
        var isErrorRegis = false;

        if (!ValidateFrom()) {

            isErrorRegis = true;
        }
        if (isErrorRegis) {
            $("#spnMsg").removeClass("hide");
            return false;

        } else {
            $("#spnMsg").addClass("hide");
            InsertUserQuery($("#txtName"), $("#txtEmailId"), $("#txtMobile"), $("#txtColQuery"), $("#<%=hdnCityId.ClientID %>"), $("#<%=hdnBranchCourseId.ClientID%>"), $("#<%=hdnCourse.ClientID %>"), $("#<%=slctCoursStream.ClientID %>"), $("#<%=lblCollegeaNameForQuery.ClientID %>"), $("#<%=hdnCollegeEmailId.ClientID %>"));
        }
        
      

    }

    function ValidateFrom() {
    	var mobileNo = /^[0-9]*$/;
        var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
        var name = $("#txtName");
        var mono = $("#txtMobile");
        var emailId = $("#txtEmailId");
        var stream = $("#<%=slctCoursStream.ClientID %>");
        var query = $("#txtColQuery");
        var isErrorRegis = false;

        if (name.val() == "") {
            $("#lblNameError").removeClass("hide");
            $("#lblNameError").text("Field Name cannot be blank");

            name.focus();

            isErrorRegis = true;
        }
        else {
            $("#lblNameError").text('');
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
            $("#lblNumberError").text("Provide 10 digit mobile number in numeric!");
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
       
        if (query.val() == '' || query.val().length==0) {
            query.focus();
            $("#lblQueryError").removeClass("hide");
            $("#lblQueryError").text("Field Query cannot be blank");
            isErrorRegis = true;
        }
        else {
            $("#lblQueryError").text('');

        }
        if (stream.val()<=0) {
            stream.focus();
            $("#lblStreamError").removeClass("hide");
            $("#lblStreamError").text("Select stream");
            isErrorRegis = true;
        }
        else {
            $("#lblStreamError").text('');

        }
        return !isErrorRegis;

    }


    function InsertUserQuery(name, email, number, query, city, collegeCourseId, courseId, streamId,collegeName,collegeEmailId) {

        var dataQuery = '{"name":"' + name.val() + '","email":"' + email.val() + '","number":"' + number.val() + '","query":"' + query.val() + '","cityName":"' + city.val() + '","collegeCourseId":"' + collegeCourseId.val() + '","courseId":"' + courseId.val() + '","streamId":"' + streamId.val() + '","collegeBranchName":"' + collegeName.text() + '","collegeEmailId":"' + collegeEmailId.val() + '","streamName":"' + $("#<%=slctCoursStream.ClientID %> option:selected").text() + '"}';

        var url = "/WebServices/CommonWebServices.asmx/InsertSponserCollegeQuickQuery";
        $.ajax({
            type: "POST",
            url: url,
            data: dataQuery,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#lblerrMsg").removeClass("hide"); ;
                $("#lblerrMsg").addClass("success");
                $("#lblerrMsg").html(response.d);
                $("#lblerrMsg").fadeOut(30000);
                ClearControl();
            },
            error: function (xml, textStatus, errorThrown) {
                $("#lblerrMsg").removeClass("hide"); ;
                $("#lblerrMsg").addClass("error");
                alert(xml.status + "||" + xml.responseText);
                ClearControl();
            }
        });
    }
</script>
