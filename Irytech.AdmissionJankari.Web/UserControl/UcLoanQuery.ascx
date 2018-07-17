<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcLoanQuery.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcLoanQuery" %>
<div class="box marginall">
<asp:HiddenField ID="hndCourseId" runat="server" />
<label id="lblerrMsg" class="hide"></label>

    <ol class="marginleft style">
      <li>
      <strong>Name:</strong>
	  <input type="text"  id="txtName" title="Enter your name" placeholder="Enter your name" />
      <label id="lblNameError"  class="error hide" title="Please Enter Name"></label>
      </li><li>
       <strong>Email:</strong>
      <input type="text" id="txtEmailId" title="Enter your email id"  placeholder="Enter your email id" />
      <label id="lblEmailIdError"   class="error hide"  title="Please Enter Email Id"></label>
     </li><li>
      <strong>Mobile:</strong>
      <input type="text" id="txtNumber" title="Enter your 10 digit mobile number"  placeholder="Enter your 10 digit mobile number" />
      <label id="lblNumberError"   class="error hide"  title="Please Enter Mobile"></label>
   </li><li>
     <strong>Select Bank:</strong>
      <select id="ddlBankList"    title="Select bank"></select>
      <label id="lblBankError"   class="error hide"  title="Please Select Bank"></label>
      </li><li>
      <strong>Amount:</strong>
       <input type="text" id="txtAmount" title="Enter amount" placeholder="Enter amount" />
      <label id="lblAmountError"   class="error hide"  title="Please Enter Amount"></label>
      </li><li>
     <strong>Course:</strong>
      <select id="ddlCourse"   title="Select course" ></select>
      <label id="lblCourseError" class="error hide" title="Please Select Bank"></label>
     </li><li>
     <strong>City:</strong>
      <select id="ddlCity"    title="Select city"></select>
      <label id="lblCityError"   class="error hide" title="Please Enter City"></label>
      </li><li>
      <input type="text" title="Enter city" class="hide"  id="txtOtherCity" />
      <label id="lblOtherCity"  class="error hide" title="Please Enter other City"></label>
    </li><li>
    <strong>Your Query:</strong>
      <textarea id="txtQuery" cols="20" rows="2"  title="Enter your Query" placeholder="Enter your Query"></textarea>
      <label id="lblQueryError"  class="error hide" title="Please Enter Query"></label>
       
      </li>
      <li><input type="checkbox" checked="checked" /> I agree T&amp;C and Privacy Policy</li>
      </ol>
    <footer>
	  
       <input type="button" title="Click to submit your query" id="butSubmit" value="Submit" />
      <input type="button" title="Clear Field " id="butClear" onclick="ClearControl()" value="Clear" />
	</footer>
    <label id="lblMsg" title="Message"></label>
    <span id="spnMsg" class="hide">
          <label id="lbllErMsg"  title="Message" class="msg" >
              You must fill in all of the mandatory fields
          </label>
             </span>

</div> 
<script type="text/javascript" defer="defer">

    $(document).ready(function () {
        BindFrontCourseList($("#ddlCourse"), $("#<%= hndCourseId.ClientID %>").val());
        BindFrontCity($("#ddlCity"));
        BindBankList($("#ddlBankList"));
        $("#ddlCity").change(function () {

            $("#ddlCity").change(function () {

                if ($("#ddlCity option:selected").text() == 'Other') {
                    $("#otherCity").css("display", "block");
                    $("#lblCityError").css("display", "none");
                } else {
                    $("#otherCity").css("display", "none");
                    if ($("#ddlCity").val() == 0) {
                        $("#lblCityError").css("display", "block");
                        $("#lblCityError").text("Error: Select City");
                    }
                    else {
                        $("#lblCityError").css("display", "none");
                    }
                }
            });
            $("#ddlCourse").change(function () {

                if ($("#ddlCourse").val() == 0) {
                    $("#lblCourseError").css("display", "block");
                    $("#lblCourseError").text("Error: Select Course");

                } else {
                    $("#lblCourseError").css("display", "none");
                }
            });
        });

        $("#butSubmit").click(function () {
            var isErrorRegis = false;

            if (!ValidateFrom()) {

                isErrorRegis = true;
            }
            if (isErrorRegis) {
                $("#spnMsg").removeClass("hide");
                return false;

            } else {
                $("#spnMsg").addClass("hide");
                SaveQuery();
            }
            
        });
    });

    function ValidateFrom() {

        var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
        var name = $("#txtName");
        var mono = $("#txtNumber");
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
            $("#lblNameError").text('');
        }


        if (mono.val() == "") {
            $("#lblNumberError").removeClass("hide");
            $("#lblNumberError").text("Field Mobile Number can not be blank");
            mono.focus();
            isErrorRegis = true;
        }
        else if (mono.val().length < 10 || mono.val().length > 10) {
            $("#lblNumberError").removeClass("hide"); mono.focus();
            $("#lblNumberError").text(" Provide 10 digit mobile number");
            isErrorRegis = true;
        }
        else if (!mobileNo.test(mono.val().trim())) {
            $("#lblNumberError").removeClass("hide"); mono.focus();
            $("#lblNumberError").text(" Provide 10 digit mobile number in numeric");
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

        if ($("#ddlCourse").val() == 0) {
            $("#lblCourseError").removeClass("hide");
            $("#lblCourseError").text("Error: Select Course");
            isErrorRegis = true;

        } else {
            $("#lblCourseError").addClass("hide");
         
        }
        if ($("#ddlBankList").val() == 0) {
            $("#lblBankError").removeClass("hide");
            $("#lblBankError").text("Select bank");
            isErrorRegis = true;

        } else {
            $("#lblBankError").addClass("hide");

        }
        if ($("#ddlCity").val() == 0) {
            $("#lblCityError").removeClass("hide");
            $("#lblCityError").text("Select city");
            $("#ddlCity").focus();
            isErrorRegis = true;
        }
        else {
            $("#lblCityError").addClass("hide");
       
        }


        if ($("#ddlCity option:selected").text() == 'Other') {
            if ($("#txtOtherCity").val() == '') {
                $("#lblOtherCity").removeClass("hide");
                $("#lblOtherCity").text("Enter other city");
                isErrorRegis = true;
            }
            else {
                $("#lblOtherCity").addClass("hide");
            
            }
        }

        if (query.val() == '') {
            query.focus();
            $("#lblQueryError").removeClass("hide");
            $("#lblQueryError").text("Field Query can not be blank");
            isErrorRegis = true;
        }
        else {
            $("#lblQueryError").text('');

        }


        return !isErrorRegis;
    }


    function ClearControl() {
        $("#spnMsg").addClass("hide");

        $("#lblCityError").addClass("hide");
        $("#lblOtherCity").addClass("hide");
        $("#lblNameError").addClass("hide");
        $("#lblEmailIdError").addClass("hide");
        $("#lblQueryError").addClass("hide");
        $("#lblNumberError").addClass("hide");
        $("#lblAmountError").addClass("hide");
        $("#lblCourseError").addClass("hide");
        $("#lblBankError").addClass("hide");
        $("#txtQuery").val('')
        $("#txtAmount").val('')
        $("#txtEmailId").val('')
        $("#txtEmailId").val('')
        $("#txtNumber").val('')
        $("#txtName").val('')
        $('#ddlCity option:eq(0)').attr('selected', 'selected');
        $("#otherCity").css("display", "none");
    }


    function SaveQuery() {
        var cityName;
        if ($("#ddlCity option:selected").text() == 'Other') {
            cityName = $("#txtOtherCity").val();
        } else {
            cityName = $("#ddlCity option:selected").text();
        }

        var json = "{'name':'" + escape($("[id$='txtName']").val()) + "','emailId':'" + escape($("[id$='txtEmailId']").val()) + "','mobileNo':'" + escape($("[id$='txtNumber']").val()) + "','cityName':'" + escape(cityName) + "','courseId':'" + escape($("#ddlCourse").val()) + "','query':'" + 'Amount:' + escape($("[id$='txtAmount']").val()) + ' ' + escape($("[id$='txtQuery']").val()) + "','Amount':'" + escape($("[id$='txtAmount']").val()) + "','realQuery':'" + escape($("[id$='txtQuery']").val()) + "','bankName':'" + escape($("#ddlBankList option:selected").text()) + "'}";

        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/SaveStudentLoanQuery",
            data: json,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#lblerrMsg").css("display", "block");
                $("#lblerrMsg").addClass("success");
                $("#lblerrMsg").html(response.d)
                $("#lblMsg").fadeOut(30000);
                ClearControl();
            }, error: function (response) {
                $("#lblerrMsg").css("display", "block");
                $("#lblerrMsg").addClass("error");
                $("#lblerrMsg").html(response.d)
                ClearControl();
            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }


</script>
