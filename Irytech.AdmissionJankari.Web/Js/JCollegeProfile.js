$("#colgeName").html($("#hdnCollegeName").val() + " Profile");
$("#colgeName1").html($("#hdnCollegeName").val());
$(".tab_content").hide();
$(".tab_content:first").show();
$(".tab_content1").hide();
$(".tab_content1:first").show();
$(".tab_content2").hide();
$(".tab_content2:first").show();
$("#ddlStream").empty().append('<option selected="selected" value="0">Select</option>');
$("#ddlExam").empty().append('<option selected="selected" value="0">Select</option>'); $("#" + $("#hdnTabs").val()).show();

function SetOuterTab(relTab, liControl, ulControl) {
    $(".tab_content").hide();
    $("#hdnMainTabs").val(relTab);
    $("#" + relTab).show();
    $("#" + ulControl).find("li").removeClass("active");
    $(liControl).addClass("active");
    return false;
}

function SetInnerTab(relTab, liControl, ulControl) {
    $(".tab_content1").hide();
    $("#hdnTabs").val(relTab);
    $("#" + relTab).show();
    $("#"+ulControl).find("li").removeClass("active");
    $(liControl).addClass("active");
    return false;
}


$("#ulQuery li").click(function () {
    $(".tab_content2").hide();
    var activeTab = $(this).attr("rel");
    $("#hdnTabQuery").val(activeTab);
    $("#" + activeTab).show();
    $("#ulQuery li").removeClass("active");
    $(this).addClass("active");

    return false;
});

function pageLoad(sender, args) {

    if (args.get_isPartialLoad()) {

        GetCollLastQuery();
        $("#colgeName").html($("#hdnCollegeName").val());
        $(".tab_content1").hide();

        SetTabOnPartialUpdate();
        SetMainTabOnPartialUpdate();
        SetInnerTab = function(relTab, liControl, ulControl) {
            $(".tab_content1").hide();
            $("#hdnTabs").val(relTab);
            $("#" + relTab).show();
            $("#" + ulControl).find("li").removeClass("active");
            $(liControl).addClass("active");
            return false;
        };
        SetOuterTab = function(relTab, liControl, ulControl) {
            $(".tab_content").hide();
            $("#hdnMainTabs").val(relTab);
            $("#" + relTab).show();
            $("#" + ulControl).find("li").removeClass("active");
            $(liControl).addClass("active");
            return false;
        };
       $("#ulQuery li").click(function () {
            ClearLabel();
            $("#ulQuery li").removeClass("active");
            $(this).addClass("active");
            $(".tab_content2").hide();
            var activeTab = $(this).attr("rel");
            $("#hdnTabQuery").val(activeTab);
            $("#" + activeTab).show();
            return false;
        });

        $("#ddlCourseForQuery").change(function () {
            var tab = $("#hdnTabQuery").val();
            course = $("#ddlCourseForQuery").val();
            switch (tab) {
                case "Query1":
                    GetCollegeQuery(queryPageIndex);
                    break;
                case "Query2":
                    GetCollLastQuery();
                    break;
                case "Query3":
                    GetUnAnsweredQuery(queryPageIndex);
                    break;
                case "Query4":
                    GetAnsweredQuery(queryPageIndex);
                    break;
            }
        });
        $("#ddlState").change(function () {
            $("#divImage1").removeClass('hide');
            BindCityByState($("#ddlCollegeCity"), $("#ddlState").val());
            $("#divImage1").addClass('hide');
        });
        $("#ddlCourseStream").change(function () {

            BindStreamByCourse($("#ddlStream"), $("#ddlCourseStream").val());

        });
        $("#ddlCoursesExam").change(function () {

            BindFrontExamListByCourse($("#ddlExam"), $("#ddlCoursesExam").val());

        });

        $("#ddlStream").change(function () {
            $("#hndStreamId").val($("#ddlStream").val());
        });
        $("#ddlExam").change(function () {
            $("#hndSelectedExam").val($("#ddlExam").val());
        });
        $("#ddlStream").empty().append('<option selected="selected" value="0">Select</option>');
        $("#ddlExam").empty().append('<option selected="selected" value="0">Select</option>');
        var solazy = {};
        (solazy.$);
        var planBox6Ui = {
            opt: {},
            init: function (t) { planBox6Ui.opt = t, planBox6Ui.bindUI(); },
            showTerm: function (t) {

                if (t) {
                    var a = t.attr("data-view").split("|"), n = t.parents(".plan"), e = a[0], l = a[1], i = a[2];
                    n && (n.children(".price").html(e + "<span>" + planBox6Ui.opt.duration_label + "</span>"), n.find(".reg-price").css("visibility", 0 < parseInt(i) ? "visible" : "hidden"), n.find(".reg-price").html("<strike>" + l + "</strike><span>" + planBox6Ui.opt.savings_label + " " + i + "%</span>"))
                }
            },
            bindUI: function () {
                $(planBox6Ui.opt.selector + " > ul > li").click(function () {

                    var t = $(this), a = t.parents(".plan-droplist-select");
                    t.siblings("li").removeClass("selected"), t.addClass("selected"), planBox6Ui.showTerm(t),
                            planBox6Ui.changeSelected(t, a)
                }),
                        $(planBox6Ui.opt.selector).click(function () {

                            var t = $(this);
                            t.hasClass("droplist-open") ? t.removeClass("droplist-open") : t.addClass("droplist-open")
                        }), $(".plan-droplist-select").mouseleave(function () { $(this).removeClass("droplist-open") })
            },
            changeSelected: function (t, a) {

                var n = t.find("div[data-main=true]").html();
                a.find(".plan-droplist-selected").html(n), t.parents(".plan").find("a.flt-btn-orng").attr("data-cart", t.data("main")), $("#frmAddToCart input#product").val(t.attr("data-main"))
            }
        };
        planBox6Ui.init({
            selector: '.plan-droplist-select',
            duration_label: '/month',
            savings_label: 'Save'
        });

        $("a.comboproduct").click(function () {
            var t = $(this).attr("data-cart");
            GetCollegeCourseForThisProduct(t.split('|')[0], 1);
        });
        $("a.bannerAnchor").click(function () {
            var t = $(this).attr("data-cart");
            GetCollegeCourseForThisProduct(t.split('|')[0], 2);
        });
        $("a.textadsanchor").click(function () {
            var t = $(this).attr("data-cart");
            GetCollegeCourseForThisProduct(t.split('|')[0], 3);
        });

    }
}

function ClearLabel() {
    $(".success").hide();
    $(".info").hide();
    $(".error").hide();
}



function SetTabOnPartialUpdate() {

    $(".tab_content1").hide();

    $("#" + $("#hdnTabs").val()).show();
    $("#ulTopRanked li").removeClass("active");
    $("#li" + $("#hdnTabs").val()).addClass("active");
    return false;
}
function SetMainTabOnPartialUpdate() {
    $("#" + $("#hdnMainTabs").val()).show();
    $("#ulBasics li").removeClass("active");
    $("#li" + $("#hdnMainTabs").val()).addClass("active");
    return false;
}

var prm = Sys.WebForms.PageRequestManager.getInstance();
// Add initializeRequest and endRequest
prm.add_initializeRequest(prm_InitializeRequest);
prm.add_endRequest(prm_EndRequest);

// Called when async postback begins
function prm_InitializeRequest(sender, args) {
    // get the divImage and set it to visible

    $("#divStreamImage").show();
    $("#divImage1").show(); $("#divCourseImage").show();
    $("#divImageVisitors").show();

    $("#divExamImage").show();
    $("#divHighImage").show();
    $("#divRankImage").show();

    $("#divNoticeImage").show();
    $("#divHostelImage").show();
    $("#divEventImage").show();

    $("#divPlacementImage").show();


}
// Called when async postback ends
function prm_EndRequest(sender, args) {
    $("#divCourseImage").hide(); $("#divStreamImage").hide();
    $("#divImage1").hide();
    $("#divImageVisitors").hide();

    $("#divExamImage").hide();
    $("#divHighImage").hide();
    $("#divRankImage").hide();

    $("#divNoticeImage").hide();
    $("#divHostelImage").hide();
    $("#divEventImage").hide();
    $("#divPlacementImage").hide();
}


var queryPageSize = 5;
var queryPageIndex = 1;
var course = $("#ddlCourseForQuery").val();
course = course != null ? course : 0;
$("#ddlCourseForQuery").change(function () {
    var tab = $("#hdnTabQuery").val();
    course = $("#ddlCourseForQuery").val();
    switch (tab) {
        case "Query1":
            GetCollegeQuery(queryPageIndex);
            break;
        case "Query2":
            GetCollLastQuery();
            break;
        case "Query3":
            GetUnAnsweredQuery(queryPageIndex);
            break;
        case "Query4":
            GetAnsweredQuery(queryPageIndex);
            break;
    }
});

GetCollLastQuery();             /*.....to get college last posted query......*/

function GetCollegeQuery(queryPageIndex) {
    $("#Query1").after("<span class='loading'><img src='/image.axd?Common=LoadingImage.gif' alt='Loading'/></span>");
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCollegeQuery",
        data: '{"pageNumber":"' + (queryPageIndex - 1) + '","pageSize":" ' + queryPageSize + '","collegeId":" ' + $("#hdnCollegeId").val() + '","course":" ' + course + '"}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $(".loading").remove();
            var privatetotal = response.d.TotalRecords;
            if (privatetotal > 0) {

                FillCollegeAnsweredQuery(response, privatetotal, queryPageIndex);
            }
            else {
                $("#Query1").html("");
                $("#Query1").append("<br/><label id='noQuery' class='info'>Sorry, you don't have any query</label>");

            }
        },
        error: function (xml, textStatus, errorThrown) {
            $(".loading").remove();
            alert(xml.status + "||" + xml.responseText);
        }
    });
}

function FillCollegeAnsweredQuery(data, privatetotal, queryPageIndex) {

    var columnData = "<div>";
    var finalData = "";
    var reply = "";
    var dataAnswer = "";
    if (data.d.MessageList.length) {
        for (var i = 0; i < data.d.MessageList.length; i++) {
            if (data.d.MessageList[i].QueryReply == null || data.d.MessageList[i].QueryReply == "") {
                dataAnswer = "Not Answered";
            }
            else {
                dataAnswer = data.d.MessageList[i].QueryReply;
            }
            if (data.d.MessageList[i].ReplyStatus == false || data.d.MessageList[i].ReplyStatus == null) {
                reply = "<li><a class='reply right' style='font-size:11px; font-weight:bold; font-style:italic; color:teal;' href='#' id='user-" + data.d.MessageList[i].UserId + "' onclick='QuerReply(" + data.d.MessageList[i].UserId + ", " + data.d.MessageList[i].StudentQueryId + ")'   title='Reply for " + data.d.MessageList[i].StudentQuery + "'>Reply</a></li>";
            }

            finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li class='question' style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Query " + (i + 1) + ":</strong><i style='color:#006a93;'>" + data.d.MessageList[i].StudentQuery + "(" + data.d.MessageList[i].StudentName + ":" + data.d.MessageList[i].UserEmailId + ")" + "</i></li><li><strong style='margin-right:10px;font-size:11px;'>Answer:</strong>" + dataAnswer + "</li>" + reply + "</ol>";
            reply = "";

        }
        $("#Query1").html("");
        finalData = columnData + finalData + "</div><div class='QuerPager pagination' id='QuerPager'></div>";
        $("#Query1").append(finalData);


    }
    $(".QuerPager").AdmissionJankari_Pager({
        ActiveCssClass: "current",
        PagerCssClass: "pager",
        PageIndex: queryPageIndex,
        PageSize: queryPageSize,
        RecordCount: privatetotal
    });
    $(".QuerPager .page").live("click", function () {
        var pageIndex = parseInt($(this).attr('page'));
        GetCollegeQuery(pageIndex);
    });
}
function QuerReply(userId, queryId) {

    $("#hdnReply").val(userId);
    $("#hdnQueryId").val(queryId);

    OpenPoup('divReply', '600', 'sndLink');
}


function GetCollLastQuery() {

    $("#Query2").append("<span class='loading'><img src='/image.axd?Common=LoadingImage.gif' alt='Loading'/></span>");
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCollegeLastQuery",
        data: '{"collegeId":"' + $("#hdnCollegeId").val() + '","course":" ' + course + '"}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $(".loading").remove();

            FillLastQueryData(response);

        },
        error: function (xml, textStatus, errorThrown) {
            $(".loading").remove();
            alert(xml.status + "||" + xml.responseText);
        }
    });
}
function FillLastQueryData(data) {

    var columnData = "<div>";
    var finalData = "";
    var dataAnswer = "";
    var reply = "";
    if (data.d.length > 0) {
        for (var i = 0; i < data.d.length; i++) {
            if (data.d[i].QueryReply == null || data.d[i].QueryReply == "") {
                dataAnswer = "Not Answered";
            } else {
                dataAnswer = data.d[i].QueryReply;
            }
            if (data.d[i].ReplyStatus == false || data.d[i].ReplyStatus == null) {
                reply = "<li><a class='reply right' style='font-size:11px; font-weight:bold; font-style:italic; color:teal;' href='#'  title='Reply for " + data.d[i].StudentQuery + "' onclick='QuerReply(" + data.d[i].UserId + "," + data.d[i].StudentQueryId + ");return false;'>Reply</a></li>";

            }

            finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Query " + (i + 1) + ":</strong><i style='color:#006a93;'>" + data.d[i].StudentQuery + "(" + data.d[i].StudentName + ":" + data.d[i].UserEmailId + ")" + "</i></li><li><strong style='margin-right:10px;font-size:11px;'>Answer:</strong>" + dataAnswer + "</li>" + reply + "</ol>";
            reply = "";
        }
        $("#Query2").html("");
        finalData = columnData + finalData + "</div>";
        $("#Query2").append(finalData);
    }
    else {
        $("#Query2").html("");
        $("#Query2").append("<br/><label id='noQuery' class='info'>Sorry, you don't have any query</label>");
    }
}

function GetUnAnsweredQuery(queryPageIndex) {
    $("#Query3").append("<span class='loading'><img src='/image.axd?Common=LoadingImage.gif' alt='Loading'/></span>");
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCollegeUnAnsweredQuery",
        data: '{"pageNumber":"' + (queryPageIndex - 1) + '","pageSize":" ' + queryPageSize + '","collegeId":" ' + $("#hdnCollegeId").val() + '","course":" ' + course + '"}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $(".loading").remove();
            var privatetotal = response.d.TotalRecords;

            if (privatetotal > 0) {

                FillUnAnsweredQueryData(response, privatetotal, queryPageIndex);
            } else {
                $("#Query3").html("");
                $("#Query3").append("<br/><label id='noQuery' class='info'>Sorry, no query available to be answer</label>");
            }
        },
        error: function (xml, textStatus, errorThrown) {
            $(".loading").remove();
            alert(xml.status + "||" + xml.responseText);
        }
    });
}

function FillUnAnsweredQueryData(data, privateTotal, queryPageIndex) {
    var columnData = "<div>";
    var finalData = "";
    var reply = "";
    if (data.d.MessageList.length > 0) {
        for (var i = 0; i < data.d.MessageList.length; i++) {


            if (data.d.MessageList[i].ReplyStatus == false || data.d.MessageList[i].ReplyStatus == null) {
                reply = "<li><a class='reply right' style='font-size:11px; font-weight:bold; font-style:italic; color:teal;' href='#' id='user-" + data.d.MessageList[i].UserId + "' title='Reply for " + data.d.MessageList[i].StudentQuery + "' onclick='QuerReply(" + data.d.MessageList[i].UserId + "," + data.d.MessageList[i].StudentQueryId + ");return false;'>Reply</a></li>";

            }

            finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Query " + (i + 1) + ":</strong><i style='color:#006a93;'>" + data.d.MessageList[i].StudentQuery + "(" + data.d.MessageList[i].StudentName + ":" + data.d.MessageList[i].UserEmailId + ")" + "</i></li>" + reply + "</ol>";
            reply = "";
        }
        $("#Query3").html("");
        finalData = columnData + finalData + "</div><div id='unAnsweredPager' class='unAnsweredPager pagination'></div>";
        $("#Query3").append(finalData);
    }

    $(".unAnsweredPager").AdmissionJankari_Pager({
        ActiveCssClass: "current",
        PagerCssClass: "pager",
        PageIndex: queryPageIndex,
        PageSize: queryPageSize,
        RecordCount: privateTotal
    });

}

$(".unAnsweredPager .page").live("click", function () {
    var pageIndex = parseInt($(this).attr('page'));
    GetUnAnsweredQuery(pageIndex);

});
function GetAnsweredQuery(queryPageIndex) {
    $("#Query4").append("<span class='loading'><img src='/image.axd?Common=LoadingImage.gif' alt='Loading'/></span>");
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCollegeAnsweredQuery",
        data: '{"pageNumber":"' + (queryPageIndex - 1) + '","pageSize":" ' + queryPageSize + '","collegeId":" ' + $("#hdnCollegeId").val() + '","course":" ' + course + '"}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $(".loading").remove();
            var privateTotal = response.d.MessageList.length;
            if (privateTotal > 0) {

                FillAnsweredQueryData(response, privateTotal, queryPageIndex);
            } else {
                $("#Query4").html("");
                $("#Query4").append("<br/><label id='noQuery' class='info'>Sorry, you don't have any query</label>");
            }
        },
        error: function (xml, textStatus, errorThrown) {
            $(".loading").remove();
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}
function FillAnsweredQueryData(data, privateTotal, queryPageIndex) {

    var columnData = "<div>";
    var finalData = ""; ;
    if (data.d.MessageList.length > 0) {

        for (var i = 0; i < data.d.MessageList.length; i++) {

            finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Query " + (i + 1) + ":</strong><i style='color:#006a93;'>" + data.d.MessageList[i].StudentQuery + "(" + data.d.MessageList[i].StudentName + ":" + data.d.MessageList[i].UserEmailId + ")" + "</i></li><li><strong style='margin-right:10px;font-size:11px;'>Answer:</strong>" + data.d.MessageList[i].QueryReply + "</li></ol>";

        }
        $("#Query4").html("");
        finalData = columnData + finalData + "</div><div id='AnsweredPager' class='AnsweredPager pagination'></div>";
        $("#Query4").append(finalData);

        

        $(".AnsweredPager").AdmissionJankari_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: queryPageIndex,
            PageSize: queryPageSize,
            RecordCount: privateTotal
        });

    }
    else {
        $("#Query4").html("");
        $("#Query4").append("<br/><label id='noQuery' class='info'>Sorry, you don't have any query</label>");
    }
}
$(".AnsweredPager .page").live("click", function () {
    var pageIndex = parseInt($(this).attr('page'));
    GetUnAnsweredQuery(pageIndex);


});

$("#btnReply").live("click", function () {
    var streamQuery = '{"userId":"' + $("#hdnReply").val() + '","textReply":"' + $("#txtReply").val() + '","collegeName":"' + $("#hdnCollegeName").val() + '","queryId":"' + $("#hdnQueryId").val() + '"}';
    if ($("#txtReply").val().length == 0) {
        alert("Please enter reply text");
        return false;

    } else {
        ReplyToUser(streamQuery);
    }
});
function ReplyToUser(query) {
    $("#btnReply").after("<span class='loading'><img src=/image.axd?Common=LoadingImage.gif alt='Loading'/></span>");
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/ReplyToUserByCollege",
        data: query,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#txtReply").val("");
            $(".loading").remove();
            GetCollegeQuery(1);
            GetAnsweredQuery(1);
            GetCollLastQuery();
            GetUnAnsweredQuery(1);
            $("#divReply").hide();
            $("#fade").hide();
            //alert(response.d);
        },
        error: function (xml, textStatus, errorThrown) {
            $("#xtReply").val("");
            alert(xml.status + "||" + xml.responseText);
        }
    });
}

$("#ddlState").change(function () {
    $("#divImage1").removeClass('hide');
    BindCityByState($("#ddlCollegeCity"), $("#ddlState").val());
    $("#divImage1").addClass('hide');
});
$("#ddlCourseStream").change(function () {

    BindStreamByCourse($("#ddlStream"), $("#ddlCourseStream").val());

});
$("#ddlCoursesExam").change(function () {

    BindFrontExamListByCourse($("#ddlExam"), $("#ddlCoursesExam").val());

});

$("#ddlStream").change(function () {
    $("#hndStreamId").val($("#ddlStream").val());
});
$("#ddlExam").change(function () {
    $("#hndSelectedExam").val($("#ddlExam").val());
});
function ValidiateCollegeBasicProfile() {
    var status = true;

    if ($("#ddlCollegeMgt").val() == '0') {
        $("#spnCollegeManagementError").removeClass('hide');
        $("#spnCollegeManagementError").text($("#rfvCollegeMgt").val())
        status = false;
    } else {
        $("#spnCollegeManagementError").addClass('hide');
    }

    if ($("#txtCollegeEst").val() == '') {
        $("#spnCollegeest").removeClass('hide');
        $("#spnCollegeest").text($("#rfvCollegeEst").val())
        status = false;
    } else {
        if (!isNumeric($("#txtCollegeEst").val())) {
            $("#spnCollegeest").removeClass('hide');
            $("#spnCollegeest").text($("#revCollegeEst").val())
            status = false;

        } else {
            $("#spnCollegeest").addClass('hide');
        }

    }

    if ($("#ddlState").val() == 0) {
        $("#spnCollegeBasicState").removeClass('hide');
        $("#spnCollegeBasicState").text($("#rfvState").val())
        status = false;
    } else {
        $("#spnCollegeBasicState").addClass('hide');
    }
    if ($("#ddlCollegeCity").val() == 0) {
        $("#spnCollegeBasicCity").removeClass('hide');
        $("#spnCollegeBasicCity").text($("#rfvCity").val())
        status = false;
    } else {
        $("#spnCollegeBasicCity").addClass('hide');
    }

    if ($("#txtEmailId").val() == '') {
        $("#spnCollegeBasicEmailId").removeClass('hide');
        $("#spnCollegeBasicEmailId").text($("#rfvEmailId").val())
        status = false;
    } else {
        if (!validateEmail($("#txtEmailId").val())) {
            $("#spnCollegeBasicEmailId").removeClass('hide');
            $("#spnCollegeBasicEmailId").text($("#revEmailId").val())
            status = false;

        } else {
            $("#spnCollegeBasicEmailId").addClass('hide');
        }
    }
    if ($("#txtCollegeMobile").val() == '') {
        $("#spnCollegeBasicMobile").removeClass('hide');
        $("#spnCollegeBasicMobile").text($("#rfvMobile").val())
        status = false;
    } else {
        if (!validatePhone($("#txtCollegeMobile").val())) {
            $("#spnCollegeBasicMobile").removeClass('hide');
            $("#spnCollegeBasicMobile").text($("#revMobile").val())
            status = false;

        } else {
            $("#spnCollegeBasicMobile").addClass('hide');
        }
    }
    if (!isNumeric($("#txtPinCode").val())) {
        $("#spnCollegeBasicPincode").removeClass('hide');
        $("#spnCollegeBasicPincode").text($("#revCollegePinCode").val())
        status = false;

    } else {
        $("#spnCollegeBasicPincode").addClass('hide');
    }
    if (!isNumeric($("#txtCollegeFax").val())) {
        $("#spnCollegeBasicFax").removeClass('hide');
        $("#spnCollegeBasicFax").text($("#revFax").val())
        status = false;

    } else {
        $("#spnCollegeBasicFax").addClass('hide');
    }
    return status;
}
//#region COURTSE DETAILS
function GetCollegeCourseDeatils(collegeBranchCourseId) {
    $("#hdnCollegeCourseId").val(collegeBranchCourseId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeBranchCourseDetails", '{"collegeBranchCourseId":"' + collegeBranchCourseId + '"}', SuccessCallBackCourse);

}

function SuccessCallBackCourse(response) {

    BindCollegeBranchCourseDetails(response.d);
}

function BindCollegeBranchCourseDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {

                $("#ddlCourseInsert").val(this['CourseId']);
                $("#ddlUniversityInsert").val(this['UniversityId']);
                $("#txtEstInsert").val(this['CollegeBranchCourseEst']);
                $('#chkHasHostelInsert').attr('checked', this['HasHostel']);
                $('#chkCourseStatus').attr('checked', this['CollegeBranchCourseStatus']);
                $("#btnCourseInsert").attr('value', 'Update');

            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}
//#endregion course details

// #region stream details
function GetCollegeCourseStreamDeatils(collegeBranchCourseStreamId) {
    $("#hdnCollegeCourseStreamId").val(collegeBranchCourseStreamId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeBranchCourseStreamDetails",'{"collegeCourseStreamId":"' + collegeBranchCourseStreamId + '"}', SuccessCallBackStream);
  
}
function SuccessCallBackStream(response) {
    BindCollegeBranchCourseStreamDetails(response.d);
}
function BindCollegeBranchCourseStreamDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {

                $("#ddlCourseStream").val(this['CourseId']);
                BindStreamByCourse($("#ddlStream"), this['CourseId']);
                $("#ddlStream").val(this['StreamId']);
                $("#ddlStreammode").val(this['CollegeBranchCourseStreamModeId']);
                $("#txtStreamDurationInsert").val(this['CollegeBranchCourseStreamDuration']);
                $("#txtStreamFeesInsert").val(this['CollegeBranchCourseStreamFees']);
                $("#txtStreamEligibiltyINsert").val(this['CollegeBranchCourseStreamEligibity']);
                $("#txtStreamSeatInsert").val(this['CollegeBranchCourseStreamSeat']);
                $("#txtStreamQuotaSeatInsert").val(this['CollegeBranchCourseStreamManagementQuotaSeat']);
                $("#txtLateralSeatInsert").val(this['CollegeBranchCourseStreamLateralEntrySeat']);
                $('#chkStreamStatus').attr('checked', this['CollegeBranchCourseStreamStatus']);
                $("#hdnCollegeCourseId").val(this['CollegeBranchCourseId']);
                $("#btnStreamInsert").attr('value', 'Update');

            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}
//#endgion stream

//#region Exam details
function GetCollegeCourseExamDeatils(collegeCourseExamId) {
    $("#hdnExamId").val(collegeCourseExamId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeBranchCourseExamDetails", '{"collegeCourseExamId":"' + collegeCourseExamId + '"}', SuccessCallBackExam);

}

function SuccessCallBackExam(response) {
    BindCollegeBranchCourseExamDetails(response.d);
}
function BindCollegeBranchCourseExamDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {
                $("#ddlCoursesExam").val(this['CourseId']);
                BindFrontExamListByCourse($("#ddlExam"), this['CourseId']);
                $("#hdnCourseExamId").val(this['CollegeBranchCourseId']);
                $("#ddlExam").val(this['ExamId']);
                $('#chkExamStatus').attr('checked', this['CollegeCourseExamStatus']);
                $("#btnExam").attr('value', 'Update');
                $("#hndSelectedExam").val(this['ExamId']);

            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}

//#endregion exam details

//#region hostel details 
function GetCollegeCourseHostelDeatils(collegeCourseHostelId) {
    $("#hdnHostelId").val(collegeCourseHostelId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeBranchCourseHostelDetails", '{"collegeCourseHostelId":"' + collegeCourseHostelId + '"}', SuccessCallBackHostel);
    
}
function SuccessCallBackHostel(response) {
    BindCollegeBranchCourseHostelDetails(response.d);
}
function BindCollegeBranchCourseHostelDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {

                $("#ddlCoursesHostel").val(this['CourseId']);
                $("#hdnHostelCourseId").val(this['CollegeBranchCourseId']);
                $("#ddlHostelMasterInsert").val(this['HostelCategoryId']);
                $("#txtHostelLocationInsert").val(this['CollegeBranchCourseHostelLocation']);
                $("#txtHostelChargeInsert").val(this['CollegeBranchCourseHostelCharge']);
                var v = this['IsCollegeBranchCourseHostelHasAC'] == true ? 0 : 1;
                $('#rbtAcInsert').find("input[type='radio'][value=" + v + "]").attr("checked", "checked");
                v = this['IsCollegeBranchCourseHostelHasLoundry'] == true ? 0 : 1;
                $('#rbtLoundaryInsert').find("input[type='radio'][value=" + v + "]").attr("checked", "checked");
                v = this['IsCollegeBranchCourseHostelHasPowerBackup'] == true ? 0 : 1;
                $('#rbtPowerInsert').find("input[type='radio'][value=" + v + "]").attr("checked", "checked");
                v = this['IsCollegeBranchCourseHostelHasInternet'] == true ? 0 : 1;
                $('#rbtInternetInsert').find("input[type='radio'][value=" + v + "]").attr("checked", "checked");
                $('#chkHostelStatus').attr('checked', this['CollegeBranchCourseHostelStatus']);
                $("#btnHostelInsert").attr('value', 'Update');


            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}
//endregion hostel details

//#region rank details
function GetCollegeCourseRankDeatils(collegeCourseRankId) {
    $("#hdnRankId").val(collegeCourseRankId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeBranchCourseRankDetails", '{"collegeCourseRankId":"' + collegeCourseRankId + '"}', SuccessCallBackRank);
   
}
function SuccessCallBackRank(response) {
     BindCollegeBranchCourseRankDetails(response.d);
}
function BindCollegeBranchCourseRankDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {

                $("#ddlCoursesRank").val(this['CourseId']);
                $("#hdnCourseRankId").val(this['CollegeBranchCourseId']);
                $("#ddlRankSourceInsert").val(this['CollegeRankSourceId']);
                $("#txtRankOverallInsert").val(this['CollegeOverAllRank']);
                $("#txtRanKYearInsert").val(this['CollegeRankYear']);
                $('#chkRankStatus').attr('checked', this['CollegeRankStatus']);
                $("#btnRankOverAllInsert").attr('value', 'Update');


            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}
//#endregion rank detials

//#region HighLights details
function GetCollegeCourseHighLightsDeatils(collegeCourseHighLightsId) {
    $("#hdnHighLights").val(collegeCourseHighLightsId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeBranchCourseHighLightsDetails", '{"collegeCourseRankId":"' + collegeCourseHighLightsId + '"}', SuccessCallBackHighLights);
}

function SuccessCallBackHighLights(response) {

    BindCollegeBranchCourseHighLightsDetails(response.d);
}

function BindCollegeBranchCourseHighLightsDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function() {

                $("#ddlCoursesHigh").val(this['CourseId']);
                $("#hdnCourseHighLightsId").val(this['CollegeBranchCourseId']);

                CKEDITOR.instances['ctl00_cphBody_txtCourseHighLightsInsert_txtFckEditorCostomize'].setData(this['CollegeBranchCourseHighlight']);

                $('#chkHighlightsStatus').attr('checked', this['CollegeBranchCourseHighlightStatus']);
                $("#btnHighLightsInsert").attr('value', 'Update');
            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}

//#endregion HighLights details
//#region placement details
function GetCollegeCoursePlacementDeatils(collegeCoursePlacementId) {
    $("#hdnPlacementId").val(collegeCoursePlacementId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeBranchCoursePlacementDetails", '{"collegeCourseplacementId":"' + collegeCoursePlacementId + '"}', SuccessCallBackPlacement);
   
}
function SuccessCallBackPlacement(response) {
    BindCollegeBranchCoursePlacementDetails(response.d);
}
function BindCollegeBranchCoursePlacementDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {

                $("#ddlCoursesPlacement").val(this['CourseId']);
                $("#hdnPlacementCourseiD").val(this['CollegeBranchCourseId']);
                $("#txtCompanyName").val(this['CollegeBranchCoursePlacementCompanyName']);
                $("#txtCompanyNameyear").val(this['CollegeBranchCoursePlacementYear']);
                $("#txtStudentHired").val(this['CollegeBranchCoursePlacementNoOfStudentHired']);
                $("#txtStudentSalary").val(this['CollegeBranchCoursePlacementAvgSalaryOffered']);
                $('#chkPlacement').attr('checked', this['CollegeBranchCoursePlacementStatus']);
                $("#btnPlacementInsert").attr('value', 'Update');
            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}
//#ednregion placement details
function GetCollegeEventDeatils(collegeEventId) {
    $("#hndEventId").val(collegeEventId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeEventDetails", '{"collegeEventId":"' + collegeEventId + '"}', BindCollegeEventDetails);
   
}

function BindCollegeEventDetails(response) {
    var list = $.parseJSON(response.d);
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {

                $("#ddlCourseEvent").val(this['Expr11']);
                $("#txtEventName").val(this['AjCollegeEventName']);
                $("#txtEventDate").val(DateFormate(this['AjCollegeEventDate']));
                $("#txtEventDesc").val(this['AjCollegeBranchEventDesc']);
                $("#txtEventLocation").val(this['AjCollegeEventLocation']);
                $('#chkEvent').attr('checked', this['AjCollegeEventStatus']);
                $("#btnSave").attr('value', 'Update');
            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}
function DateFormate(dateString) {
    dateString = dateString.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    return day + "/" + month + "/" + year;

}

function GetCollegeNoticeDeatils(collegeNoticeId) {
    $("#hdnNoticeId").val(collegeNoticeId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeNoticeDetails", '{"collegeNoticeId":"' + collegeNoticeId + '"}', SuccesscallBackNotice);

}

function SuccesscallBackNotice(response) {
    BindCollegeNoticeDetails(response.d);
}

function BindCollegeNoticeDetails(list) {
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {

                $("#ddlNoticeCategory").val(this['NoticeTypeId']);
                $("#txtNoticeSubject").val(this['NoticeSubject']);
                $("#txtNoticeShortDesc").val(this['NoticeShortDesc']);
                CKEDITOR.instances['ctl00_cphBody_txtCollegeNotice_txtFckEditorCostomize'].setData(this['NoticeDesc']);
                $("#imgNoticeImage").removeClass('hide');
                $("#hdnNoticeImage").val(this['NoticeImage']);
                $("#imgNoticeImage").attr('src', '/image.axd?Notice=' + this['NoticeImage']);
                $('#chkNotice').attr('checked', this['NoticeStatus']);
                $("#btnSaveNotice").attr('value', 'Update');
            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}
function GetCollegeTestomonialDeatils(collegeTestomonialId) {
    $("#hdnTestimonial").val(collegeTestomonialId);
    AjaxPostCallBack("/WebServices/CommonWebServices.asmx/GetCollegeTestomonialDetails", '{"testomonialId":"' + collegeTestomonialId + '"}', BindCollegeTestomonialDetails);
   
}
function BindCollegeTestomonialDetails(response) {
    var list = $.parseJSON(response.d);
    if (list != null) {
        if (list.length > 0) {
            $.each(list, function () {
                CKEDITOR.instances['ctl00_cphBody_txtTestimonial_txtFckEditorCostomize'].setData(this['AjTestimonial']);
                $('#chkTestimonialStatus').attr('checked', this['AjTestimonialStatus']);
                $("#btnSaveTestimonial").attr('value', 'Update');
            });
        } else {
            alert('No Record found for the course');
        }
    } else {
        alert('No Record found for the course');
    }
}


///*---------------------------start for banner and combo and text product------------------------------*/
//$(function () {
//    new AjaxUpload('#UploadButton', {
//        action: '/image.axd?Folder=Banner',
//        onComplete: function (file, response) {
//            $("#hdnBanner").val(response);
//            document.getElementById("bannerImage").src = "/image.axd?Banner=" + response;
//        },
//        onSubmit: function (file, ext) {
//            if (!(ext && /^(jpg|png|gif|jpeg)$/i.test(ext))) {
//                alert('Invalid File Format.');
//                return false;
//            }

//        }
//    });

//});

SetTabOnRedirect();
function SetTabBanner() {

    $(".tab_content").hide();
    $("#divBanner").show();
    $("#ulBasics li").removeClass("active");
    $("#liBanner").addClass("active");

}
function SetTabOnRedirect() {
    var url = window.location.search;
    url = url.replace("?", ''); // remove the ?
    if (url.indexOf("T=") != -1) {
        if (url.split('=')[1] == "4") {
            $(".tab_content").hide();
            $("#tabAdvertise1").show();
            $("#ulBasics li").removeClass("active");
            $("#liTab").addClass("active");
        } else {
            SetTabBanner();
        }

    }
}

function GetPaymentProductList() {
    $("#divProductList").html("");
    $.when(GetComboProduct()).done(GetBannerProductAfterPayment).done(GetTextProductAfterPayment);  //TO ADD METHODS IN QUEUE SO THAT EXECUTE FIRST IS COMPLETE AND SECOND SO ON...
}

function GetComboProduct() {

    var url = "/WebServices/CommonWebServices.asmx/GetProductAfterPayment";
    var dataQuery = '{advertisementType:"' + 1 + '"}';
    AjaxPostCallBack(url, dataQuery, SuccessCallBackComboProduct);

}

function SuccessCallBackComboProduct(response) {
    var columnData = "<div><h2>Combo Product List</h2><table class='grdView' style='width:100%'> <tr style='background:#eff2f7'><td>S.No</td><td>Product</td><td>Course</td><td>Amount</td><td></td></tr>";
    BindProduct(response.d, columnData);
}
function GetBannerProductAfterPayment() {
    var url = "/WebServices/CommonWebServices.asmx/GetProductAfterPayment";
    var dataQuery = '{advertisementType:"' + 2 + '"}';
    AjaxPostCallBack(url, dataQuery, SuccessCallBackBannerProductAfterPayment);

}
function SuccessCallBackBannerProductAfterPayment(response) {
    var columnData = "<div><h2>Banner Product List</h2><table class='grdView' style='width:100%'> <tr style='background:#eff2f7'><td>S.No</td><td>Product</td><td>Course</td><td>Amount</td><td></td></tr>";
    BindProduct(response.d, columnData);
}

function GetTextProductAfterPayment() {
    var url = "/WebServices/CommonWebServices.asmx/GetProductAfterPayment";
    var dataQuery = '{advertisementType:"' + 3 + '"}';
    AjaxPostCallBack(url, dataQuery, SuccessCallBackTextAdsProductAfterPayment);

}

function SuccessCallBackTextAdsProductAfterPayment(response) {
    var columnData = "<div><h2>Micro Features List</h2><table class='grdView' style='width:100%'> <tr style='background:#eff2f7'><td>S.No</td><td>Product</td><td>Course</td><td>Amount</td><td></td></tr>";
    BindProduct(response.d, columnData);
}

function BindProduct(data, columnData) {
    HideLoadingProgress();
    var rowIndex = 0;

    var xmlDoc = $.parseXML(data);
    var xml = $(xmlDoc);

    var productList = xml.find("Table");
    var finalData = "";
    var paymentLink = "";

    $.each(productList, function (i) {
        if ($(this).find("AjPaymentStatus").text().toLowerCase() == "false") {
            paymentLink = "<a href='/account/paymentoption.aspx?id=" + $(this).find("AjProductPaymentId").text() + "'>Make Payment</a>";
        } else {
            paymentLink = "";
        }
        finalData += "<tr><td>" + (i + 1) + "</td><td style='width:250px;'>" + $(this).find("AjProductName").text() + "</td><td id='id-" + $(this).find("AjProductPaymentId").text() + "'>" + GetCourseByPaymentId($(this).find("AjProductPaymentId").text()) + "</td><td>" + $(this).find("Amount").text() + "</td><td>" + paymentLink + "</td></tr>";
        rowIndex = ++i;
    });
    if (rowIndex > 0) {
        finalData = columnData + finalData + "</table>";
        $("#divProductList").append(finalData);
        $("#divProductList").show();
    }
}
function ShowLoadingProgress() {
    $("#progress").show();

} function HideLoadingProgress() {
    $("#progress").hide();

}

function GetBannerDetails(bannerId) {
    var url = "/WebServices/CommonWebServices.asmx/GetBannerById";
    var dataQuery = '{bannerId:"' + bannerId + '"}';
    AjaxPostCallBack(url, dataQuery, SuccessCallBackBannerDetails);

}
function SuccessCallBackBannerDetails(response) {
    BindBannerDetails(response.d);

}
function BindBannerDetails(list) {
    list = $.parseJSON(list);

    if (list != null) {
        if (list.length > 0) {

            $.each(list, function () {
                $("#hdnBannerId").val(this["AjBannerId"]);
                $("#ddlBannerCourse").val(this['AjCourseId']);
                $("#txtToolTip").val(this['AjBannerToolTip']);
                $("#txtUrl").val(this['AjBannerUrl']);
                $("#hdnBanner").val(this['AjBannerImage']);
                document.getElementById("bannerImage").src = "/image.axd?Banner=" + this['AjBannerImage'];
            });
        } else {
            alert('No Record found');
        }
    } else {
        alert('No Record found');
    }
}

function GetCourseByPaymentId(paymentId) {
    var url = "/WebServices/CommonWebServices.asmx/GetPaymentedCourse";
    var dataQuery = '{paymentId:"' + paymentId + '"}';
    AjaxPostCallBack(url, dataQuery, SuccessCourseCallBack, paymentId);

}

function SuccessCourseCallBack(response,id) {
 
    $("#id-"+id).html(response.d);
}

/*--------------End For banner and combo and text product------------------------------*/

function UploadCollegeImage() {
    var data = $("#fulCollegeImage").attr("files")[0];
    if (data != null) {
        var formData = new window.FormData(); // Creating object of FormData class
        formData.append("file", data);

        $.ajax({
            type: "POST",
            url: "/image.axd?Folder=College",
            data: formData,
            async: false,
            contentType: false,
            dataType: "json",
            processData: false,
            beforeSend: function () {
                //                $("#progresss").show();
                //                //clear everything
                //                $("#bar").width('0%');
                $("#message").html("");
                //                $("#percent").html("0%");
            },
            uploadProgress: function (event, position, total, percentComplete) {
                //                alert('hi');
                //                $("#bar").width(percentComplete + '%');
                //                $("#percent").html(percentComplete + '%');

            },
            success: function () {
                //                $("#bar").width('100%');
                //                $("#percent").html('100%');


            },
            complete: function (response) {
                UpdateCollegeImage(response.responseText);
                $("#imgCollege").attr('src', '/image.axd?College=' + response.responseText);
                $("#message").removeClass('hide');
                $("#message").html("College Logo uploaded sucessfully");
            },
            error: function (data, status, e) {
               // alert(e);
            }
        });
    } else {
    alert('Please select the college logo to upload');
    }

}

function UpdateCollegeImage(imageName) {
    var collegeBranchId = $("#hdnCollegeId").val();
    var url = "/WebServices/CommonWebServices.asmx/UpdateCollegeImage";
    var dataQuery = "{'collegeBranchId':" + collegeBranchId + ",'collegeLogo':'" + imageName + "'}";
    AjaxPostCallBack(url, dataQuery, MsgUploadImage);
}

function MsgUploadImage() {
    $("#lblBasicDetailsMsg").addClass("show");
    $("#lblBasicDetailsMsg").text("Image upload sucessfully");
}

// Validates date dd/MM/yyyy format.
function isDate(txtDate) {

    var currVal = txtDate;
    if (currVal == '')
        return false;

    //Declare Regex  
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

//    //Checks for mm/dd/yyyy format.
//    dtMonth = dtArray[1];
//    dtDay = dtArray[3];
//    dtYear = dtArray[5];

    //Checks for dd/mm/yyyy format.
    dtDay = dtArray[1];
    dtMonth = dtArray[3];
    dtYear = dtArray[5];   


    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}