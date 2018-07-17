$(document).ready(function () {
    BindFrontCourseList($("#ddlCourse"), $("#hndCourseId").val());
    BindFrontCity($("#ddlCity"));
    $("#ddlCity").change(function () {

        if ($("#ddlCity option:selected").text() == 'Other') {
            $("#otherCity").removeClass("hide");
            $("#lblCityError").css("display", "none");
        } else {
            $("#otherCity").css("display", "none");
            if ($("#ddlCity").val() == 0) {
                $("#lblCityError").removeClass("hide");
                $("#lblCityError").text(" Select city");
            }
            else {
                $("#lblCityError").css("display", "none");
            }
        }
    });
    $("#ddlCourse").change(function () {

        if ($("#ddlCourse").val() == 0) {
            $("#lblCourseError").removeClass("hide");
            $("#lblCourseError").text("Select the course you are interested in");

        } else {
            $("#lblCourseError").css("display", "none");
        }
    });
    $("#butSubmit").click(function () {
        var isError = false;
        if (!ValidateFrom()) {
            $("#lbllErrMsg").removeClass("hide");
             isError = true;
           

        } else {
            $("#lbllErrMsg").addClass("hide");
                 SaveQuery();

        }
    });
});

function ValidateFrom() {

    var status = false;
    if ($("#ddlCourse").val() == 0) {
        $("#lblCourseError").removeClass("hide");
        $("#lblCourseError").text("Select the course you are interested in");

        status = true;
    } else {
        $("#lblCourseError").addClass("hide");

    }

    if ($("#ddlCity").val() == 0) {

        $("#lblCityError").removeClass("hide");
        $("#lblCityError").text("Select city");
        status = true;
    }
    else {
        $("#lblCityError").addClass("hide");

    }

    if ($("#ddlCity option:selected").text() == 'Other') {
        if ($("#txtOtherCity").val() == '') {
            $("#lblOtherCity").removeClass("hide");
            $("#lblOtherCity").text("Select city");
            status = true;
        }
        else {
            $("#lblOtherCity").addClass("hide");

        }
    }

    if ($("#txtName").val() == '') {
        $("#lblNameError").removeClass("hide");
        $("#lblNameError").text("Field Name cannot be blank");
        status = true;
    }
    else {
        $("#lblNameError").addClass("hide");

    }
    if ($("#txtNumber").val() == '') {
        $("#lblNumberError").removeClass("hide");
        $("#lblNumberError").text("Field Mobile Number cannot be blank");
        status = true;
    }
    else if ($("#txtNumber").val().length > 10 || $("#txtNumber").val().length < 10) {
        $("#lblNumberError").removeClass("hide");
        $("#lblNumberError").text("Provide 10 digit mobile number");
        status = true;
    }
    else {
        if (validatePhone($("#txtNumber").val())) {

            $("#lblNumberError").addClass("hide");
        }
        else {
            $("#lblNumberError").removeClass("hide"); status = true;
            $("#lblNumberError").text("Provide 10 digit mobile number");
        }
    }
    if ($("#txtEmailId").val() == '') {

        $("#lblEmailIdError").removeClass("hide");
        $("#lblEmailIdError").text("Field Email cannot be blank");
        status = true;
    }
    else {
        if (validateEmail($("#txtEmailId").val())) {

            $("#lblEmailIdError").addClass("hide");
        }
        else {
            $("#lblEmailIdError").removeClass("hide"); status = true;
            $("#lblEmailIdError").text("Incorrect Email format, please try again");
        }
    }
    if ($("#txtQuery").val() == '') {
        $("#lblQueryError").removeClass("hide");
        $("#lblQueryError").text("Field Query cannot be blank");
        status = true;
    }
    else {
        $("#lblQueryError").addClass("hide");

    }

    return !status;
}



function ClearControl() {
    $("#lbllErrMsg").addClass("hide");
    $("#lblCourseError").addClass("hide");
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
    $("#otherCity").addClass("hide");
}


function SaveQuery() {
    var CityName;
    if ($("#ddlCity option:selected").text() == 'Other') {
        CityName = $("#txtOtherCity").val()
    } else {
        CityName = $("#ddlCity option:selected").text();
    }
    var json = "{'name':'" + $("[id$='txtName']").val() + "','emailId':'" + escape($("[id$='txtEmailId']").val()) + "','mobileNo':'" + escape($("[id$='txtNumber']").val()) + "','cityName':'" + escape(CityName) + "','courseId':'" + $("#ddlCourse").val() + "','query':'" + $("#txtQuery").val() + "','studentcoursename':'" + $("#ddlCourse option:selected").text() + "'}";
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/SaveStudentCommonQuery",
        data: json,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
          
            if ($("#hndShowPackage").val() == "1") {
                $("#lblMsg").css("display", "block");
                $("#lblMsg").addClass("success");
                $("#lblMsg").html(response.d);

                $("#lblMsg").fadeOut(30000);
                OpenPoup('divPackageList', '550px', 'sndAddCollegePresedentSpeech')
            } 

            ClearControl();
        },
        error: function (xml, textStatus, errorThrown) {
            alert(xml.status + "||" + xml.responseText);
        }
    });

}
