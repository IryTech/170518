<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ForgetPassword" %>
<fieldset>
    <legend>Get Password</legend>
    <ul>
     <li><center><span id="errMsg" class="hide success"> </span></center></li>
        <li>
            <label>
           <%=Resources.label.Email%> </label>  
          <input type="text" id="txtEmailId" tabindex="1"   title="Enter email id with which you have registered with us"  /> 
           </li> 
           <li>
            <span id="lblEmailIdError" class="hide error" style="margin-left:25%;" title="Enter email id with which you have registered with us"></span>
       </li>         
         <li>
            <label>&nbsp;
               </label>  
         <input type="button" id="btnFrgtPwd" tabindex="2" title="Get Password" value="<%=Resources.label.FrogetPwdBtnText%>" />  
          <input type="button" title="Clear Field "  id="butClear" onclick="ClearControl()"  value="Clear" />
           <span style="display: none" id="progress">
                        <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" />
                    </span>                
               </li> 
    </ul>
</fieldset>

<script type="text/javascript">


    $("#btnFrgtPwd").click(function () {
        if (ValidateFrom() == true) {
            var dataQuery = '{"emailId":"' + $("#txtEmailId").val() + '"}';
            showLoading();
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetPassword",
                data: dataQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    hideLoading();
                    $("#errMsg").html(""); $("#txtPasswrd").val(""); $("#lblEmailIdError").addClass("hide");
                    $("#errMsg").removeClass("hide")
                    $("#errMsg").html(response.d)
                    


                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
    });
    function showLoading() {
        $("#progress").show();
     }

     function hideLoading() {
         $("#progress").hide();
     }
      function ValidateFrom() {

            var status = true;
            if ($("#txtEmailId").val() == '') {
                $("#lblEmailIdError").removeClass("hide");
                $("#lblEmailIdError").text("Field Email cannot be blank");
                status = false;
            }
            else {
                if (validateEmail($("#txtEmailId").val())) {
                    status = true;
                    $("#lblEmailIdError").removeClass("hide");
                   
                }
                else {
                    status = false;
                    $("#lblEmailIdError").removeClass("hide");
                    $("#lblEmailIdError").text("Incorrect Email format, please try again");
                }
            }
            if (status == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function ClearControl() {

            $("#lblEmailIdError").addClass("hide");
            $("#errMsg").addClass("hide");
            $("#txtEmailId").val('');
        }
</script>
            
          