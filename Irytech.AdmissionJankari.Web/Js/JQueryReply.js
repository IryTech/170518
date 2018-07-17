var status = false;
var UserqueryId = 0;
var UserQueryEmail = "";
var UserFullName = "";
OpenReply = function (query, queryId, queryUserEmail, userName) {


    UserqueryId = queryId;
    UserQueryEmail = queryUserEmail;
    UserFullName = userName;
    $("#txtQuery").val(query);
    BindFrontUserType($("#ddlUserType"));

    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/CheckSession",
        data: "{}",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d == false) {

                status = false;
            } else {
                status = true;
            }
        },
        error: function (response) {

        }
    });

    if (status) {

        $('ul#registertabReply').addClass("hide");
    } else {
        $('ul#registertabReply').removeClass("hide");
    }
    ClearReplyFields();
    OpenPoup('divQueryReply', 450, 'lnkreply');

};


ReplyQuery = function() {
    var replyMsg;
    if (!status) {

        if (ValidateBeforeLogin()) {

            var msg = Registerusre();
            if (msg == '') {
                replyMsg = ReplyUserQuery(UserQueryEmail, UserqueryId);
                ClearReplyFields();
                $("#lblReplyerrMsg").removeClass("hide");
                $("#lblReplyerrMsg").addClass("success");
                $("#lblReplyerrMsg").text(replyMsg);


            } else {

                $("#lbllReplyErMsg").text(msg);

            }
        } else {
            $("#spnReplyMsg").removeClass("hide");
        }
    } else {


        if (ValidateAfterLogin()) {
            replyMsg = ReplyUserQuery(UserQueryEmail, UserqueryId);
            ClearReplyFields();
            $("#lblReplyerrMsg").removeClass("hide");
            $("#lblReplyerrMsg").addClass("success");
            $("#lblReplyerrMsg").html(replyMsg);

        } else {

            $("#spnReplyMsg").removeClass("hide");
        }
    }
};

ValidateBeforeLogin = function() {
    var validatestatus = false;
    if ($("#ddlUserType").val() == 0) {
        $("#userTypeError").removeClass("hide");
        $("#userTypeError").text("Select user category");
        $("#ddlUserType").focus();
        validatestatus = true;
    } else {
        $("#userTypeError").addClass("hide");
    }
    if ($("#txtReplyUserName").val() == '') {
        $("#nameReplyError").removeClass("hide");
        $("#nameReplyError").text("Field Name cannot be blank");
        $("#txtReplyUserName").focus();
        validatestatus = true;
    } else {
        $("#nameReplyError").addClass("hide");

    }
    if ($("#txtReplyEmailId").val() == '') {
        $("#EmailReplyError").removeClass("hide");
        $("#EmailReplyError").text("Field Email cannot be blank");
        $("#txtReplyEmailId").focus();
        validatestatus = true;
    } else {
        $("#EmailReplyError").addClass("hide");

    }
    if ($("#txtReplyMobileNumber").val() == '') {
        $("#MobileReplyNumberError").removeClass("hide");
        $("#MobileReplyNumberError").text("Field Mobile Number cannot be blank");
        validatestatus = true;
    } else if ($("#txtReplyMobileNumber").val().length > 10 || $("#txtReplyMobileNumber").val().length < 10) {
        $("#MobileReplyNumberError").removeClass("hide");
        $("#MobileReplyNumberError").text("Provide 10 digit mobile number");
        validatestatus = true;
    } else {
        if (validatePhone($("#txtReplyMobileNumber").val())) {

            $("#MobileReplyNumberError").addClass("hide");
        } else {
            $("#MobileReplyNumberError").removeClass("hide");
            validatestatus = true;
            $("#MobileReplyNumberError").text("Provide 10 digit mobile number");
        }
    }
    if ($("#txtReply").val() == '') {
        $("#ReplyError").removeClass("hide");
        $("#ReplyError").text("Field Reply cannot be blank");
        $("#ReplyError").focus();
        validatestatus = true;
    } else {
        $("#ReplyError").addClass("hide");

    }
    return !validatestatus;
};

ValidateAfterLogin = function() {
    var validatestatus = false;
    if ($("#txtReply").val() == '') {
        $("#ReplyError").removeClass("hide");
        $("#ReplyError").text("Field Reply cannot be blank");
        $("#ReplyError").focus();
        validatestatus = true;
    } else {
        $("#ReplyError").addClass("hide");

    }
    return !validatestatus;
};


Registerusre = function() {
    var json = "{'mobileNo':'" + $("[id$='txtReplyMobileNumber']").val() + "','emailId':'" + $("[id$='txtReplyEmailId']").val() + "','name':'" + ($("[id$='txtReplyUserName']").val()) + "','courseId':" + $("#ctl00_cphBody_hndCourseId").val() + ",'userType':" + $("#ddlUserType option:selected").val() + "}";
    var msg = "";
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/UserRegister",
        data: json,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            msg = response.d;
        },
        error: function(xml, textStatus, errorThrown) {
            alert(xml.status + "||" + xml.responseText);
        }
    });
    return msg;
};

ReplyUserQuery = function (queryUserEmail, queryId) {
    var json = "{'queryId':'" + queryId + "','queryReply':'" + $("[id$='txtReply']").val() + "','queryUserEmail':'" + queryUserEmail + "','userFullName':'" + UserFullName + "','query':'" + $("[id$='txtQuery']").val() + "'}";
  
    var msg = "";
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/ReplyUserQuery",
        data: json,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            msg = response.d;
        },
        error: function (xml, textStatus, errorThrown) {
            alert(xml.status + "||" + xml.responseText);
        }
    });
    return msg;
};

ClearReplyFields = function() {
    $("#txtReplyUserName").val('');
    $("#txtReplyEmailId").val('');
    $("#txtReplyMobileNumber").val('');
    $("#txtReplyUserName").val('');
    $("#txtReply").val('');
    $("#ddlUserType").val(0);
    $("#userTypeError").addClass("hide");
    $("#nameReplyError").addClass("hide");
    $("#EmailReplyError").addClass("hide");
    $("#MobileReplyNumberError").addClass("hide");
    $("#ReplyError").addClass("hide");
    $("#spnReplyMsg").addClass("hide");
};