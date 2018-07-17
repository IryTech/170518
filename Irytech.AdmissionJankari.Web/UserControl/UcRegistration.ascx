<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcRegistration.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcRegistration" %>
<div id="divPoupup" class="quickquery login">
    <span>
        <label id="lblerrMsg" class="hide">
        </label>
    </span>
    <fieldset class="boxBody">
    <h3>
        Login</h3>
        <ul>
            
            <li><strong class="liststrong"><%=Resources.label.Email%></strong><input type="text" id="txtEmail"
                title="Enter EmailId" />
                <label id="lblEmailError" class="hide error" title="Please Enter Email Id">
                </label>
            </li>
            <li><strong class="liststrong"><%=Resources.label.pwd%>: </strong>
                <input type="password" class="Field" id="txtPassword"  title="Enter your Password" />
                <label id="lblpasswordError" class="hide error" title="Please Enter Your Password">
                </label>
            </li>
            <li><strong class="liststorng">
                <input type="checkbox" checked="checked" />
                Remember
                <input type="button" title="Click to Login" class="button" onclick="Login()" id="btnLogin" value="Login" />
                 <input type="button" title="Clear" onclick="ClearLoginControl()" class="button" id="btnClear" value="Clear" />
            </strong></li>
            <h3>
                New User Signup-It's free</h3>
            <li><strong class="liststrong">You Are:</strong>
                <select id="ddlUserType" class="drpdown" title="Select UserType">
                </select>
                <label id="lblUserTypeError" class="hide error" title="Please select UserType">
                </label>
            </li>
            <li><strong class="liststrong"><%=Resources.label.Name%></strong>
                <input type="text" runat="server" id="txtName" title="Enter Name" />
                <label id="lblNameError" class="hide error" title="Please Enter Name">
                </label>
            </li>
            <li><strong class="liststrong"><%=Resources.label.Mobile%></strong>
                <input type="text" runat="server" id="txtMobileNumber" title="Enter Mobile" />
                <label id="lblNumberError" class="hide error" title="Please Enter Mobile">
                </label>
            </li>
            <li><strong class="liststrong"><%=Resources.label.Email%></strong><input type="text" id="txtEmailId"
                title="Enter EmailId" runat="server" />
                <label id="lblEmailIdError" class="hide error" title="Please Enter Email Id">
                </label>
            </li>
        </ul>
    </fieldset>
    <footer>
            <input type="checkbox" checked="checked" />
            I agree <a href="" target="_blank">T&amp;C</a> and <a href="" target="_blank">Privacy Policy</a> 
            <input type="button" title="Submit the for Registrrtion" class="button" onclick="ResisterUser()" id="butSubmit" value="Register" />
           <input type="button" title="Clear Field " id="butClear" onclick="ClearRegistationControl()" value="Clear" />
                   
        </footer>
</div>

<script type="text/javascript" defer="defer">
    $(document).ready(function () {
        BindFrontUserType($("#ddlUserType"));
    });
    function Login() {

        if (ValidateLoginForm() == true) {
            ValidateLogin();
        }
    }


    function ValidateLoginForm() {
        var status = true;
         if ($("#txtEmail").val() == '') {
            $("#lblEmailError").css("display", "block");
            $("#lblEmailError").text("Error: Enter Email ID");
            status = false;
        }
        else {
            if (validateEmail($("#txtEmail").val())) {
                status = true;
                $("#lblEmailError").css("display", "none");
            }
            else {
                status = false;
                $("#lblEmailError").css("display", "block");
                $("#lblEmailError").text("Error:Email ID is not valid");
                
               }
        }
        if ($("#txtPassword").val() == '') {

            $("#lblpasswordError").css("display", "block");
            $("#lblpasswordError").text("Error:Enter Your Password");
            status = false;
        }
        else {
            $("#lblpasswordError").css("display", "none");
            status = true;
        }
      
        if (status == true) {
            return true;
        }
        else {
            return false;
        }
    }

    function ResisterUser() {
        if (ValidateRegistationForm() == true) {

        }
    }

    function ValidateRegistationForm() {
        var status = true;
        if ($("#txtEmailId").val() == '') {
            $("#lblEmailIdError").css("display", "block");
            $("#lblEmailIdError").text("Error: Enter Email ID");
            status = false;
        }
        else {
            if (validateEmail($("#txtEmailId").val())) {
                status = true;
                $("#lblEmailIdError").css("display", "none");
            }
            else {
                status = false;
                $("#lblEmailIdError").css("display", "block");
                $("#lblEmailIdError").text("Error:Email ID is not valid");

            }
        }
        if ($("#ddlUserType").val() <=0) {
            $("#lblUserTypeError").css("display", "block");
            $("#lblUserTypeError").text("Error: Plese select User Type");
            status = false;
        } else {
            status = true;
            $("#lblUserTypeError").css("display", "none");
        }
        if ($("#txtMobileNumber").val() == '') {

            $("#lblNumberError").css("display", "block");
            $("#lblNumberError").text("Error:Enter Your Mobile No");
            status = false;
        }
        else {
            $("#lblNumberError").css("display", "none");
            status = true;
        }
        if ($("#txtName").val() == '') {

            $("#lblNameError").css("display", "block");
            $("#lblNameError").text("Error:Enter Your Name");
            status = false;
        }
        else {
            $("#lblNameError").css("display", "none");
            status = true;
        }

        if (status == true) {
            return true;
        }
        else {
            return false;
        }
    }

    function ValidateLogin() {
        var json = "{'emailId':'" + escape($("[id$='txtEmail']").val()) + "','password':'" + escape($("[id$='txtPassword']").val()) + "'}";
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/ValidateLogin",
            data: json,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d == '') {
                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("success");
                     ClearLoginControl();
                } else {

                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("error");
                    $("#lblerrMsg").html(response.d)
                }
            }, error: function (response) {
                $("#lblerrMsg").css("display", "block");
                $("#lblerrMsg").addClass("error");
                $("#lblerrMsg").html(response.d)
                ClearLoginControl();
            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }

    function ClearRegistationControl() {
        $("#ddlUserType").val(0);
        $("#txtEmailId").val('');
        $("#txtMobileNumber").val('');
        $("#txtName").val('');
        $("#lblNameError").css("display", "none");
        $("#lblUserTypeError").css("display", "none");
        $("#lblEmailIdError").css("display", "none");
    }

    function ClearLoginControl() {
        $("#txtEmail").val('')
        $("#txtPassword").val('')
        $("#lblEmailError").css("display", "none");
        $("#lblpasswordError").css("display", "none");

    }


</script>
