<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucExamQuickQuery.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ucExamQuickQuery" %>
<div class="box1">
<h3 class="streamCompareH3">Quick Query For <span runat="server" id="lblExamName"></span></h3>
<asp:HiddenField ID="hndCourseId" runat="server" />
<input type="hidden" id="hndExamId" runat="server" />
<hr class="hrline" />
 
       <label id="lblerrMsg" class="hide" style="line-height:18px;border:0px solid; margin-left:0px !important; padding-left:0px !important;">
        </label>

<div class="box">

        <ol class="marginleft style">
       <li><strong><%=Resources.label.Name%> </strong> 
        <input type="text" id="txtName" title="Enter your name" placeholder="Enter your name" />
           <span id="lblNameError" class="error hide" ></span>
       </li>
       <li>
        <strong><%=Resources.label.Email%> </strong>
        <input type="text" id="txtEmailId" title="Enter your email id" placeholder="Enter your email id"/>
        <span id="lblEmailIdError" class="error hide" ></span>
            </li>
       <li>
        <strong><%=Resources.label.Mobile%> </strong>
         <input type="text" id="txtNumber" title="Enter your 10 digit mobile number" placeholder="Enter your 10 digit mobile number" />
             <span id="lblNumberError" class="error hide"></span>
              </li>
       
      
       <li>
        <strong >
               <%=Resources.label.City%> </strong>
        
            <select id="ddlCity"  title="Select city">
            </select>
            <span id="lblCityError" class="error hide" title="Select city">
            </span>
         </li>
       <li>
        <span id="otherCity" style="display: none;">
            <input type="text" title="Enter city name" id="txtOtherCity" />
            <span id="lblOtherCity" class="error hide" title="Select  other city"></span>
            </span>
         </li>
       <li>
           <strong>
             <%=Resources.label.Query%> 
            </strong>
        
            <textarea id="txtQuery"   title="Enter your query"></textarea>
            <span id="lblQueryError" class="error hide" title="Enter  query">
            </span>
             </li>
       
            
            <li style="font-size:11px; color:Gray;"><input type="checkbox" checked="checked" /> I agree 
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Terms-and-Conditions"%>' target="_blank"  >T&amp;C</a> and  
            <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Privacy-Policy"%>' target="_blank" >Privacy Policy</a></li>
            </ol>
      <footer>
            <input type="button" title="Click to submit your query"  class="button" id="butSubmit" value="Submit" />
            <input type="button" title="Clear Field " id="butClear" onclick="ClearControl()" value="Clear" />
        
            <label id="lblMsg" title="Message">
            </label><br/> 
            <span id="spnBookMsg" class="hide">
          <label id="lbllErMsg"  title="Message" class="msg" >
              You must fill in all of the mandatory fields
          </label>
             </span>
        </footer>
 
</div>
</div>
<script type="text/javascript" defer="defer">

    $(document).ready(function () {
        $("[id$='lblExamName']").text() != "" ? $("[id$='lblExamName']").show() : $("#<%=lblExamName.ClientID %>").show();
        BindFrontCity($("#ddlCity"));
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


        $("#butSubmit").click(function () {

            var isErrorRegis = false;

            if (!ValidateFrom()) {

                isErrorRegis = true;
            }
            if (isErrorRegis) {
                $("#spnBookMsg").removeClass("hide");
                return false;

            } else {
                $("#spnBookMsg").addClass("hide");

                SaveQuery();
            }
        });
    });

    function ValidateFrom() {
        var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
        var name = $("#txtName");
        var mono = $("#txtNumber");
        var emailId = $("#txtEmailId");
        var city = $("#ddlCity");
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
        if (city.val() <=0) {
            $("#lblCityError").removeClass("hide");
            $("#lblCityError").text("Select city");
            city.focus();
            isErrorRegis = true;
        } else {
            $("#lblCityError").text('');
        }

        if (otherCity.text() == 'Other') {
            if ($("#txtOtherCity").val() == '') {
                $("#lblOtherCity").removeClass("hide");
                $("#lblOtherCity").text("Select other city");
                isErrorRegis = true;
            }
            else {
                $("#lblOtherCity").text('');
           
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
        $("#spnBookMsg").addClass("hide");
        $("#lblCityError").addClass("hide");
        $("#lblOtherCity").addClass("hide");
        $("#lblNameError").addClass("hide");
        $("#lblEmailIdError").addClass("hide");
        $("#lblQueryError").addClass("hide");
        $("#lblNumberError").addClass("hide");
        $("#txtQuery").val('');
        $("#txtEmailId").val('');
        $("#txtNumber").val('');
        $("#txtName").val('');
        $('#ddlCity option:eq(0)').attr('selected', 'selected');
        $('#ddlCourse option:eq(0)').attr('selected', 'selected');
        $("#otherCity").css("display", "none");
    }

    function SaveQuery() {
        var cityName;
        var course = $('#<%= hndCourseId.ClientID %>').val();
      if ($("#ddlCity option:selected").text() == 'Other') {
          cityName = $("#txtOtherCity").val();
      } else {
            cityName = $("#ddlCity option:selected").text();
        }
        var exam = $("[id$='lblExamName']").text() != "" ? $("[id$='lblExamName']").text() : $("#<%=lblExamName.ClientID %>").text();

        var json = "{'name':'" + $("[id$='txtName']").val() + "','emailId':'" +$("[id$='txtEmailId']").val() + "','mobileNo':'" + $("[id$='txtNumber']").val() + "','cityName':'" + escape(cityName) + "','courseId':'" +course + "','examId':" + $("[id$='hndExamId']").val() + ",'query':'" + $("[id$='txtQuery']").val() + "','examName':'" +exam + "'}";
 
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/SaveStudentExamQuery",
            data: json,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#lblerrMsg").removeClass("hide");
                $("#lblerrMsg").addClass("success");
                $("#lblerrMsg").html(response.d);
                $("#lblerrMsg").fadeOut(30000);
                ClearControl();
            }, error: function (response) {
                $("#lblerrMsg").removeClass("hide");
                $("#lblerrMsg").addClass("error");
                $("#lblerrMsg").html(response.d);
                ClearControl();
            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }

         

</script>
