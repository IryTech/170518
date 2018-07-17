var results = [];
var collegeStreamMgt;
var status = 1;
var examstatus = 1;
var examResults = [];
var $radStreamMode;
var reEmail = /^[a-z]+(([a-z_0-9]*)|([a-z_0-9]*\.[a-z_0-9]+))*@([a-z_0-9\-]+)((\.[a-z]{3})|((\.[a-z]{2})+)|(\.[a-z]{3}(\.[a-z]{2})+))$/;
var mobileNo = /^[7-9][0-9]{9}$/;
var numericNo = /^\d*[0-9](|.\d*[0-9]|,\d*[0-9])?$/;
function AddFields() {
    if (results.length > 0) {
        var index = $("#hdnCourseValue").val();
        $("#courseContainer").append("<input id='hdnStreamDynamic" + index + "' type='hidden'   /><fieldset id='fieldSet" + index + "'><legend>Course</legend> <ul> <li><label>Course</label><select id='ddlCourse" + index + "' onchange= 'showStreamPop(" + index + ")'></select></li><li><label>University</label> <select id='ddlUniversity" + index + "'></select></li><li><label>Title</label><input id='txtCourseTitle" + index + "' type='text' /></li> <li><label>MetaTag</label><input id='txtCourseMetaTag" + index + "' type='text' /></li> <li><label>Url</label><input id='txtCourseUrl" + index + "' type='text' /></li> <li><label>Meta Desc</label><textarea id='txtCourseMetaDesc" + index + "' type='text' /></li><li><label>Establishment</label><input id='txtCourseEst" + index + "' type='text' /></li><li><label>Status</label><input id='chkCourseStatus" + index + "' type='checkbox' /></li><li><label>Description</label><input id='txtCourseDesc" + index + "' type='text'/></li></ul></fieldset></div>");
        AddFacaLity(index);
        AddHighLights(index);
        AddRankSource(index);
        AddHostel(index);
        var courseUrl = "../../WebServices/CommonWebServices.asmx/GetAllCourseList";
        var universityUrl = "../../WebServices/CommonWebServices.asmx/GetUniversityList";
        BindDropDown($("#ddlCourse" + index), courseUrl);
        BindDropDown($("#ddlUniversity" + index), universityUrl);
        index++;
        $("#hdnCourseValue").val(index);
    }
    else{
        alert("Please Select Course  and stream First");
    }
}
function showStreamPop(index) {
    if ($("#ddlCourse" + index).val() > 0) {
        if ($("#hdnStreamDynamic" + index).val() > 0) {
            alert("You have already selected");
            return false;
        } 
        else {
            var urlStream = "../../WebServices/CommonWebServices.asmx/GetStreamListByCourseId";
            var urlStreamMode = "../../WebServices/CommonWebServices.asmx/GetStreamMode";
            $('#streamContainer').append("<div id='srtpopUp" + index + "' style='display:none;width: 520px' class='popup_block'><a href='#' style='float:right;color:red;' id='close" + index + "' class='close' onclick='closepopUp(" + index + ");return false;'>X</a><input id='hdnStreamDynamic" + index + "' type='hidden'   /> <fieldset width='500px'  id='fieldSet" + index + "'><legend>Stream</legend><p>You can add multiple stream one by one.</p> <ul> <li><label>Stream Name</label><select id='ddlStream" + index + "'></select></li><li><label>Stream Mode</label><div class='rbtStreamModeDynamic' id='rbtStreamMode" + index + "' /></li> <li><label>StreamSeat</label><input id='txtCourseStreamSeat" + index + "' type='text' /></li><li><label>Duration</label><input id='txtCourseStreamDur" + index + "' type='text' /></li> <li><label>Fees</label><input id='txtcourseStreamFees" + index + "' type='text' /></li> <li><label>Eligibility</label><input id='txtCourseStreamEligibilty" + index + "' type='text' /></li> <li><label>Quota Seat</label><input id='txtStreamQuotaSeat" + index + "' type='text' /></li> <li><label>Lateral Entry Seat</label><input id='txtStreamLateralEntrySeat" + index + "' type='text' /></li><li><label>Status</label><input id='chkStreamStatus" + index + "' type='checkbox' /></li><li><label>Description</label><input id='txtCourseStreamDes" + index + "' type='text'/></li><li><label></label><input id='btnCourseStreamDone' class='close' type='button' onclick='SetValues(" + index + ")' value='Add streams'/><input id='close" + index + "' class='close' type='button' onclick='closepopUp(" + index + ");return false;'  value='Close'/></li></ul></fieldset></div>");
            BindStream($("#ddlStream" + index), $("#ddlCourse" + index), urlStream);

            if ($("#rbtStreamMode" + index).html() == "") {
                BindStreamMode($("#rbtStreamMode" + index), urlStreamMode);
            }

            var popMargTop = ($("#srtpopUp" + index).height() + 80) / 2;
            var popMargLeft = ($("#srtpopUp" + index).width() + 80) / 2;
            $("#srtpopUp" + index).css({
                'margin-top': -popMargTop,
                'margin-left': -popMargLeft
            });
            $("#fade").show();
            $("#srtpopUp" + index).show();
        }
    }
}
function closepopUp(index) {
    if (results.length > 0) {
        if ($("#hdnArray").val() == "") {
            $("#hdnArray").val(results);
        }
        else {
            $("#hdnArray").val(results);
        }
    }
    $("#srtpopUp" + index).hide();
    showExamPop(index);
}


function SetValues(index) {
    if (checkstreamForm(index)) {
        $("#hdnStreamDynamic" + index).val($("#ddlCourse" + index).val());
        if (results.length > 0) {
            for (var i = 0; i < results.length; i++) {
                if ($("#ddlStream" + index).val() == results[i].streamId) {
                    status = 0;
                    break;
                }
            }
        }
        if (status == 1) {
            results.push(
                    {
                        courseId: $("#ddlCourse" + index).val()
                    },
                    {
                        streamId: $("#ddlStream" + index).val()
                    },
                    {
                        lateralSeat: $("#txtStreamLateralEntrySeat" + index).val()
                    },
                    {
                        quotaSeat: $("#txtStreamQuotaSeat" + index).val()
                    },
                    {
                        fees: $("#txtcourseStreamFees" + index).val()
                    },
                    {
                        duration: $("#txtCourseStreamDur" + index).val()
                    },
                    {
                        eligibilty: $("#txtCourseStreamEligibilty" + index).val()
                    },
                    {
                        descrition: $("#txtCourseStreamDes" + index).val()
                    },
                    {
                        streamSeat: $("#txtCourseStreamSeat" + index).val()
                    },
                    {
                        streamStatus: $("#chkStreamStatus" + index).attr('checked') ? "True" : "False"
                    },
                   {
                       streamMode: $("input[name='stremam']:checked").val()
                   }
                );
        }
        else {
            alert('you have already selected');
        }

        clearFields(index);
    }
}
function clearFields(index) {
    $("#ddlStream" + index).val(0);
    $("#txtStreamLateralEntrySeat" + index).val('');
    $("#txtStreamQuotaSeat" + index).val('');
    $("#txtcourseStreamFees" + index).val('');
    $("#txtCourseStreamDur" + index).val('');
    $("#txtCourseStreamEligibilty" + index).val('');
    $("#txtCourseStreamDes" + index).val('');
    $("#txtCourseStreamSeat" + index).val('');
    $("#chkStreamStatus" + index).attr('checked', false);
    status = 1;
}
function checkstreamForm(index) {
    var streamName = $("#ddlStream" + index).val();
    var streamSeats = $("#txtCourseStreamSeat" + index).val();
    var streamDuration = $("#txtCourseStreamDur" + index).val();
    var streamQuotaSeats = $("#txtStreamQuotaSeat" + index).val();
    var streamFees = $("#txtcourseStreamFees" + index).val();
    var streamLateralSeat = $("#txtStreamLateralEntrySeat" + index).val();
    var streamDesc = $("#txtCourseStreamDes" + index).val();
    var streamEligibilty = $("#txtCourseStreamEligibilty" + index).val();
    if (streamName <= 0) {
        alert("Please Select Stream ");
        return false;
    }
    if (streamSeats == "") {
        alert("Please Enter Stream Seats");
        return false;
    }
    else if (!numericNo.test(streamSeats)) {
        $("#txtCourseStreamSeat" + index).focus();
        alert('Please Enter Stream Seat in number'); return false;
    }
    else if (streamDuration == "") {
        alert("Please Enter Stream Duration");
        return false;
    }
    else if (!numericNo.test(streamDuration)) {
        $("#txtCourseStreamDur" + index).focus();
        alert('Please Enter Duration in number'); return false;
    }
    else if (streamFees == "") {
        alert("Please Enter Stream Fess");
        return false;
    }
    else if (!numericNo.test(streamFees)) {
        $("#txtcourseStreamFees" + index).focus();
        alert('Please Enter Stream Fees in number'); return false;
    }
    else if (streamEligibilty == "") {
        alert("Please Enter Stream Eligibilty");
        return false;
    } else if (streamQuotaSeats == "") {
        alert("Please Enter Stream Quota");
        return false;
    } else if (!numericNo.test(streamQuotaSeats)) {
        $("#txtStreamQuotaSeat" + index).focus();
        alert('Please Enter Quota seat in number'); return false;
    }
    else if (streamLateralSeat == "") {
        alert("Please Enter Stream Lateral Entry");
        return false;
    }
    else if (!numericNo.test(streamLateralSeat)) {
        $("#txtStreamLateralEntrySeat" + index).focus();
        alert('Please Enter Lateral seat in number'); return false;
    }
    else if (streamDesc == "") {
        alert("Please Enter Stream Description");
        return false;
    } else {
        return true;
    }

}

function BindDropDown(control, url) {
  
    control.empty().append('<option selected="selected" value="0">Please select</option>');
    $.ajax({
        type: "POST",
        url: url,
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            PopulateControl(response.d,control);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}

function BindStream(cntrlStream,cntrlCourse ,url) {
    var course = cntrlCourse.val();
    cntrlStream.empty().append('<option selected="selected" value="0">Please select</option>');
    $.ajax({
        type: "POST",
        url: url,
        data: '{"courseId":"' + course + '"}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            PopulateControl(response.d, cntrlStream);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}

function BindExam(cntrlExam, cntrlCourse, url) {

    var course = cntrlCourse.val(); 
    cntrlExam.empty().append('<option selected="selected" value="0">Please select</option>');
    $.ajax({
        type: "POST",
        url: url,
        data: '{"courseId":"' + course + '"}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            PopulateControl(response.d, cntrlExam);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}
function BindStreamMode(cntrlStreamMode,url) {
    $.ajax({
        type: "POST",
        url: url,
        data: '{}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            PopulateRadioButtonControl(response.d, cntrlStreamMode);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}
function PopulateControl(list, control) {

    if (list.length > 0) {
        control.removeAttr("disabled");
       
        $.each(list, function () {
            control.append($("<option></option>").val(this['Value']).html(this['Text']));

        });
    }
    else {
        control.empty().append('<option selected="selected" value="0">Not available<option>');
    }
}
function PopulateRadioButtonControl(list, control) {
    var i = 0;
    $.each(list, function () {
        var rdb = "<input id=rbtStream" + i + "  type=radio  name='stremam' value=" + this['Value'] + " /><label for=RadioButton" + i + ">" + this['Text'] + "</label>";
        control.append(rdb);
        i++;
    });

}

function insertCollegeDetails(hdnCollegeBranchIdGenerated, colegeInstitute, collegeGroup, collegeAssociate, collegeName, manageMent, collegeEst, branchDesc, popualrName, cityId, webSite, status, emailId, mobileNo, pinCode, fax, address) {
    showProgess();
    var dataQuery = '{"colegeInstitute":"' + colegeInstitute.val() + '","collegeGroup":"' + collegeGroup.val() + '","collegeAssociate":"' + collegeAssociate.val() + '","collegeName":"' + collegeName.val() + '","manageMent":"' + manageMent.val() + '","collegeEst":"' + collegeEst.val() + '","branchDesc":"' + branchDesc.val() + '","popualrName":"' + popualrName.val() + '","cityId":"' + cityId.val() + '","webSite":"' + webSite.val() + '","status":"' + status + '","emailId":"' + emailId.val() + '","mobileNo":"' + mobileNo.val() + '","pinCode":"' + pinCode.val() + '","fax":"' + fax.val() + '","address":"' + address.val() + '"}';
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/InserCollegeDetails",
        data: dataQuery,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.length > 0) {
                if (response.d[0].CollegeId > 0) {
                   $("#btnSubmit").hide();
                    $("#btnNext").show();
                    clearCollegeFields();
                    hdnCollegeBranchIdGenerated.val(response.d[0].CollegeId);
                    $("#collegeId").val(response.d[0].CollegeId);
                    hideProgress();
                    alert(collegeName.val() + " college successFully inserted ");
                }
                else {
                    hideProgress();
                    alert(response.d[1].ErrMsg); 
                }
            }

        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
   
}

function insertCourseDetails(hdnCollegeBranchId) {
    showProgess();
  var courseStatus = false;
    var courseLength = $("#hdnCourseValue").val();
    for (var i = 0; i < courseLength; i++) {
     
        if($("#chkCourseStatus" + i).is(":checked")) {
            courseStatus = true;
        }
        if ($("#ddlCourse" + i).val() > 0) {
            var courseData = $("#ddlCourse" + i).val();
            var collegeCourseQuery = '{"colegeBranchId":"' + hdnCollegeBranchId + '","courseId":"' + $("#ddlCourse" + i).val() + '","universityId":"' + "1" + '","title":"' + $("#txtCourseTitle" + i).val() + '","metatTag":"' + $("#txtCourseMetaTag" + i).val() + '","url":"' + $("#txtCourseUrl" + i).val() + '","metaDesc":"' + $("#txtCourseMetaDesc" + i).val() + '","courseEst":"' + $("#txtCourseEst" + i).val() + '","status":"' + courseStatus + '","description":"' + $("#txtCourseDesc" + i).val() + '"}';

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/InsertCollegeBranchCourseDetails",
                data: collegeCourseQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d[0].CourseBranchId > 0) {
                        showProgess();
                        insertCollegeCourseStreamDetails(response.d[0].CourseBranchId, courseData);
                        insertExamDetails(response.d[0].CourseBranchId, courseData);
                        insertFacality(response.d[0].CourseBranchId);
                        insertHighLights(response.d[0].CourseBranchId);
                        insertRankSourceDetails(response.d[0].CourseBranchId);
                        insertHostelDetails(response.d[0].CourseBranchId);
                    }
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
    }
}
function insertCollegeCourseStreamDetails(hdnCollegeCourseBranchId,courseId) {
    if (results.length > 0) {
   for (var i = 0; i < results.length; i++) {
      if (courseId == results[i].courseId && results[i].courseId!="undefined") {
           var collegeCourseQuery = '{"courseBranchId":"' + hdnCollegeCourseBranchId + '","streamId":"' + results[i+1].streamId + '","streamMode":"' + results[i+10].streamMode + '","streamSeat":"' + results[i+8].streamSeat + '","duration":"' + results[i+5].duration + '","fees":"' + results[i+4].fees + '","eligibilty":"' + results[i+6].eligibilty + '","quotaSeat":"' + results[i+3].quotaSeat + '","lateralSeat":"' + results[i+2].lateralSeat + '","streamStatus":"' + results[i+9].streamStatus + '","descrition":"' + results[i+7].descrition + '"}';
                $.ajax({
                       type: "POST",
                       url: "../../WebServices/CommonWebServices.asmx/InsertCollegeBranchStreamDetails",
                       data: collegeCourseQuery,
                       async: true,
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function(response) {
                          
                       },
                       error: function(xml, textStatus, errorThrown) {
                         //  alert(xml.status + "||" + xml.responseText);
                       }
                   });
               
           }
       }
   }
   }
   function insertExamDetails(hdnCollegeCourseBranchId, courseId) {
         if (examResults.length > 0) {
           for (var i = 0; i < examResults.length; i++) {
          if (courseId == examResults[i].courseId && examResults[i].courseId != "undefined") {
             var examCourseQuery = '{"courseBranchId":"' + hdnCollegeCourseBranchId + '","examId":"' + examResults[i + 1].examId + '","examStatus":"' + examResults[i + 2].examStatus + '"}';
                 
                   $.ajax({
                       type: "POST",
                       url: "../../WebServices/CommonWebServices.asmx/InsertCollegeBranchExamDetails",
                       data: examCourseQuery,
                       async: true,
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (response) {
                       },
                       error: function (xml, textStatus, errorThrown) {
                           //alert(xml.status + "||" + xml.responseText);
                       }
                   });
               }
           }
       }
   }
   function insertFacality(hdnCollegeCourseBranchId) {
       var facalityStatus = false;
       var courseLength = $("#hdnCourseValue").val();
       for (var i = 0; i < courseLength; i++) {
           if ($("#chkCollegeCourseFacality" + i).is(":checked")) {
               facalityStatus = true;
           }
           var faciltyCourseQuery = '{"courseBranchId":"' + hdnCollegeCourseBranchId + '","facality":"' + $("#txtCollegeHighLights" + i).val() + '","facalityStatus":"' + facalityStatus + '","facalityDesc":"' + $("#txtCollegeFacalityDescription" + i).val() + '"}';
  
                   $.ajax({
                       type: "POST",
                       url: "../../WebServices/CommonWebServices.asmx/InsertCollegeBranchFacalityDetails",
                       data: faciltyCourseQuery,
                       async: true,
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function(response) {
                       },
                       error: function(xml, textStatus, errorThrown) {
                          // alert(xml.status + "||" + xml.responseText);
                       }
                   });
               }
           }

           function insertHighLights(hdnCollegeCourseBranchId) {
          
               var highLightsStatus = false;
               var courseLength = $("#hdnCourseValue").val();
               for (var i = 0; i < courseLength; i++) {
                 if ($("#chkCollegeHighLightStatus" + i).is(":checked")) {
                       highLightsStatus = true;
                   }
                   var highLightsCourseQuery = '{"courseBranchId":"' + hdnCollegeCourseBranchId + '","highlights":"' + $("#txtCollegeHighLights" + i).val() + '","highlightsStatus":"' +  highLightsStatus + '"}';
                $.ajax({
                       type: "POST",
                       url: "../../WebServices/CommonWebServices.asmx/InsertCollegeBranchHighLightsDetails",
                       data: highLightsCourseQuery,
                       async: true,
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (response) {
                       },
                       error: function (xml, textStatus, errorThrown) {
                          // alert(xml.status + "||" + xml.responseText);
                       }
                   });
               }
           }
           function insertRankSourceDetails(hdnCollegeCourseBranchId) {
               var rankStatus = false;
               var courseLength = $("#hdnCourseValue").val();
               for (var i = 0; i < courseLength; i++) {

                   if ($("#chkCollegeBranchRankSourceStatus" + i).is(":checked")) {
                       rankStatus = true;
                   }
                   var rankSourceCourseQuery = '{"courseBranchId":"' + hdnCollegeCourseBranchId + '","rankSource":"' + $("#ddlCollegeRankSource" + i).val() + '","souceYear":"' + $("#txtCollegeRankSourceYear" + i).val() + '","rankOverAll":"' + $("#txtRankOverall" + i).val() + '","rankStatus":"' + rankStatus + '"}';
                  $.ajax({
                       type: "POST",
                       url: "../../WebServices/CommonWebServices.asmx/InsertCollegeBranchRankSouceDetails",
                       data: rankSourceCourseQuery,
                       async: true,
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (response) {
                       },
                       error: function (xml, textStatus, errorThrown) {
                          // alert(xml.status + "||" + xml.responseText);
                       }
                   });
               }
           }

           function insertHostelDetails(hdnCollegeCourseBranchId) {
              
               var hostelStatus = false;
               var courseLength = $("#hdnCourseValue").val();
               for (var i = 0; i < courseLength; i++) {
                  if ($("#chkHostelStatus" + i).is(":checked")) {
                       hostelStatus = true;
                   }
               var hostelSourceCourseQuery = '{"courseBranchId":"' + hdnCollegeCourseBranchId + '","hostelCategory":"' + $("#ddlCollegeHostelCategory" + i).val() + '","location":"' + $("#txtCollegeHostelLocation" + i).val() + '","charge":"' + $("#txtHostelCharge" + i).val() + '","hostelStatus":"' + hostelStatus + '","ac":"' + $("input[name='AC" + i + "']:checked").val() + '","loundary":"' + $("input[name='Loundary" + i + "']:checked").val() + '","internet":"' + $("input[name='InterNet" + i + "']:checked").val() + '","powerBackUp":"' + $("input[name='Power" + i + "']:checked").val()+'"}';
               $.ajax({
                   type: "POST",
                   url: "../../WebServices/CommonWebServices.asmx/InsertCollegeHostelDetails",
                   data: hostelSourceCourseQuery,
                   async: true,
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {

                       hideProgress();
                       //location.href = "/College/CollegeDetails.aspx";
                                           alert("College asscociate details inserted successfull");
                   },
                   error: function (xml, textStatus, errorThrown) {
                       //alert(xml.status + "||" + xml.responseText);
                   }
               });
               }
           }
   function GetUrlLength(control, cssName,  hdnValue) {
        var dataLength =  hdnValue.val() - ($(control).val().length );
  if(dataLength<=0) {
            alert("Full");
        }
        else {
            jQuery("#" + cssName).html(dataLength + " remaining out of " + hdnValue.val());
        }
    }


	function CopyContent(contl1, contl2, contl3, contl4, contl5) {
	    contl2.val($(contl1).val());
	    contl3.val($(contl1).val());
	    contl4.val($(contl1).val());
	    contl5.val($(contl1).val());

	}

	function showExamPop(examIndex) {
	    $("#hdnExamIndex").val(examIndex);
	    var urlExam = "../../WebServices/CommonWebServices.asmx/GetExamListByCourseId";
	   // $("#examPopUpContainer").append("<div id='divCollegeBranchExam" + examIndex + "' style='display:none;width: 520px' class='popup_block'><a href='#' style='float:right;color:red;' id='examClose" + examIndex + "' class='close' onclick='closeExamPopUp(" + examIndex + ");return false;'>X</a><fieldset><legend>Exam</legend><ul><li><label> Exam</label><select id='ddlCollegeExam" + examIndex + "' tabindex='1' title='Please select Exam'></select>  </li><li><label>Status</label><input type='checkbox' id='examStatus" + examIndex + "' tabindex='2' title='Please Check'/></li><li><label></label><input id='btnCourseStreamDone' class='close' type='button' onclick='SetExamValues(" + examIndex + ")' value='Next'/></li></ul></fieldset></div>");
	    BindExam($("#ddlCollegeExam"), $("#ddlCourse" + examIndex), urlExam);
	    var popMargTop = ($("#examPopUpContainer" ).height() + 80) / 2;
	    var popMargLeft = ($("#examPopUpContainer").width() + 80) / 2;
	    $("#examPopUpContainer").css({
	        'margin-top': -popMargTop,
	        'margin-left': -popMargLeft
	    });

	    $("#examPopUpContainer").show();
	}

	function closeExamPopUp() {
	    if (examResults.length > 0) {
	        if ($("#hdnExamArray").val() == "") {
	            $("#hdnExamArray").val(examResults);
	        } else {
	            $("#hdnExamArray").val(examResults);
	        }
	    }
	    $("#examPopUpContainer").hide(); $("#fade").hide();
	}

	function SetExamValues() {
	    var hdnExamindex = $("#hdnExamIndex").val();
	    if (checkExamForm()) {
	                if (examResults.length > 0) 
                          {
                              for (var i = 0; i < examResults.length; i++) 
                {
                    if ($("#ddlCollegeExam").val() == examResults[i].examId)
                    {
	                    examstatus = 0;
	                    break;
	                }
	            }
	        }
	       	        if (examstatus == 1) {
	            examResults.push(
                    {
                        courseId: $("#ddlCourse" + hdnExamindex).val()
                    },
                    {
                        examId: $("#ddlCollegeExam").val()
                    },
                    {
                        examStatus: $("#examStatus").attr('checked') ? "True" : "False"
                    }
                );
	        } else {
	            alert('you have already selected');
	        }
	        
	        clearExamFields();
	    }
	}

	function checkExamForm() {
	    var examName = $("#ddlCollegeExam").val();
	    if (examName <= 0) {
	        alert("Please Select Exam ");
	        return false;
	    } else {
	        return true;
	    }
	}
	function showProgess() {
	    $("#progress").show();
	}
	function hideProgress() {
	    $("#progress").hide();
	}
	function clearExamFields() {
	    $("#ddlCollegeExam" ).val(0);
	    $("#examStatus").attr('checked', false);
	    examstatus = 1;
	}
	function AddFacaLity(index) {
	    $("#divCourseBranchFacailty").append("<input id='hdnFacalityCourse" + index + "' type='hidden'  /><fieldset id='fieldSet" + index + "'><legend>Facality</legend> <ul> <li><label>Facality</label>    <input type='text' id='txtCollegeFacality" + index + "'  tabindex='1' title='Please Enter College Facality'/></li><li><li><label>Status</label><input type='checkbox' id='chkCollegeCourseFacality" + index + "' title='Please Select Status' tabindex='2'/></li><li><label>Description</label> <input  type='text' id='txtCollegeFacalityDescription" + index + "'  tabindex='3' title='Please Enter  Facality Description'/>     </li></ul></fieldset></div>");
	}
	
	function AddHighLights(index) {
	    $("#divCourseHighLights").append("<input id='hdnHighLightsCourse" + index + "' type='hidden'  /><fieldset id='fieldSet" + index + "'><legend>HighLights</legend> <ul> <li><label>HighLights</label>    <input type='text' id='txtCollegeHighLights" + index + "'  tabindex='1' title='Please Enter College Facality'/></li><li><li><label>Status</label><input type='checkbox' id='chkCollegeHighLightStatus" + index + "' title='Please Select Status' tabindex='2'/></li></ul></fieldset></div>");
	}
	function AddRankSource(index) {
	    $("#divCourseRankSourceDyn").append("<input id='hdnRankSource" + index + "' type='hidden'  /><fieldset id='fieldSet" + index + "'><legend>Rank Source</legend> <ul> <li><label>Rank Source</label><select id='ddlCollegeRankSource" + index + "' ></select></li><li><label>Source OverAll</label>    <input type='text' id='txtCollegeRankSourceYear" + index + "'  tabindex='1' title='Please Enter Source '/></li><li><label>Rank OverALL</label>    <input type='text' id='txtRankOverall" + index + "'  tabindex='1' title='Please Enter College Facality'/></li><li><li><label>Status</label><input type='checkbox' id='chkCollegeBranchRankSourceStatus" + index + "' title='Please Select Status' tabindex='2'/></li></ul></fieldset></div>");
	    var rankSourceUrl = "../../WebServices/CommonWebServices.asmx/GetRankSourcelist";
	    BindDropDown($("#ddlCollegeRankSource" + index), rankSourceUrl);
	}
	function AddHostel(index) {
	    $("#divHostelCource").append("<input id='hdnHostelCategory" + index + "' type='hidden'  /><fieldset id='fieldSet" + index + "'><legend>Hostel</legend> <ul> <li><label>Hostel Category</label><select id='ddlCollegeHostelCategory" + index + "' ></select></li><li><label>Location</label>    <input type='text' id='txtCollegeHostelLocation0" + index + "'  tabindex='1' title='Please Enter Location'/></li><li>  <label>InterNet</label>  <input type='radio' name='InterNet" + index + "' id='rbtHostelInternetYes" + index + "' value='0' checked title='Please Select Yes'/><label>Yes </label> <input type='radio' name='InterNet" + index + "' id='rbtHostelInternetNo" + index + "' value='1'  title='Please Select Yes'/>No</li><li>  <label>Laundry</label>  <input type='radio' name='Loundary" + index + "' id='rbtLoundaryYes" + index + "' value='0' checked title='Please Select Yes'/><label>Yes</label>   <input type='radio' name='Loundary" + index + "' id='rbtLoundaryNo" + index + "' value='0'  title='Please Select Yes'/>No</li><li>  <label>AC</label>  <input type='radio' name='AC" + index + "' id='rbtACYes' value='0'  title='Please Select Yes'/><label>Yes </label> <input type='radio' name='AC" + index + "' id='rbtACNo" + index + "' value='1'  title='Please Select Yes'/>No</li><li>  <label>Power</label>  <input type='radio' name='Power" + index + "' id='rbtPowerYes" + index + "' value='0' checked title='Please Select Yes'/><label>Yes </label> <input type='radio' name='Power" + index + "' id='rbtPowerNo" + index + "' value='1'  title='Please Select Yes'/>No</li><li><label>Charge </label>    <input type='text' id='txtHostelCharge" + index + "'  tabindex='1' title='Please Enter Charge'/></li><li><li><label>Status</label><input type='checkbox' id='chkHostelStatus" + index + "' title='Please Select Status' tabindex='2'/></li></ul></fieldset></div>");
	    var hostelUrl = "../../WebServices/CommonWebServices.asmx/GetCollegeHostel";
	    BindDropDown($("#ddlCollegeHostelCategory"+index), hostelUrl);
	}
	function clearCollegeFields() {
	    $("#ctl00_ContentPlaceHolderMain_ucCollegeBranchBasicInfo_txtCollegeBranch").val(''); $("#ctl00_ContentPlaceHolderMain_ucCollegeBranchBasicInfo_txtCollegePopularName").val(''); $("#ctl00_ContentPlaceHolderMain_ucCollegeBranchBasicInfo_txtCollegeWebsite").val('');
	    $("#ctl00_ContentPlaceHolderMain_ucCollegeBranchBasicInfo_txtEmailId").val(''); $("#ctl00_ContentPlaceHolderMain_ucCollegeBranchBasicInfo_txtCollegeMobile").val(''); $("#txtPinCode").val(''); $("#ctl00_ContentPlaceHolderMain_ucCollegeBranchBasicInfo_txtCollegeFax").val(''); $("#ctl00_ContentPlaceHolderMain_ucCollegeBranchBasicInfo_txtAddress").val('');
	}
	function CollegeGroup(control) {
	    $.ajax({
	        type: "POST",
	        url: "../../WebServices/CommonWebServices.asmx/GetCollegeGroupDetails",
	        async: true,
	        data: '{}',
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function (msg) {
	            data = msg.d.split(",");
	              control.autocomplete(data);
	        }
	    });
	}
	function InstituteName(control) {
	    $.ajax({
	        type: "POST",
	        url: "../../WebServices/CommonWebServices.asmx/GetInstituteNameDetails",
	        async: true,
	        data: '{}',
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function (msg) {
	            data = msg.d.split(",");
	            control.autocomplete(data);
	        }
	    });
	}
	function InsertCollegeGroup(control) {
	    var collegeGroup = control.val();
	    $.ajax({
	        type: "POST",
	        url: "../../WebServices/CommonWebServices.asmx/InsertCollegeGroup",
	        data: '{"collegeGroup":"' + collegeGroup + '"}',
	        async: true,
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function (response) {
	        },
	        error: function (xml, textStatus, errorThrown) {
	            //alert(xml.status + "||" + xml.responseText);
	        }
	    });
	}
	function BindCollegeGroup(control, url) {
	    control.empty().append('<option selected="selected" value="0">Please select</option>');
	    $.ajax({
	        type: "POST",
	        url: url,
	        data: "{}",
	        async: true,
	        contentType: "application/json; charset=utf-8",
	        dataType: "json",
	        success: function (response) {
	            PopulateControlGroup(response.d, control);
	        },
	        error: function (xml, textStatus, errorThrown) {
	            //alert(xml.status + "||" + xml.responseText);
	        }
	    });
	}
	function PopulateControlGroup(list, control) {
	 	    if (list.length > 0) {
	        control.removeAttr("disabled");
	        control.empty().append('<option selected="selected" value="0">Please select</option>');
	        $.each(list, function () {
	            control.append($("<option></option>").val(this['Value']).html(this['Text']));

	        });
	        control.find("option").each(function () {
	         
	            if (this.text.toLowerCase() == $("#hdnCollegeGroupName").val().toLowerCase()) {
	               control.val(this.value);
	            }
	        });
	     	    }
	    else {
	        control.empty().append('<option selected="selected" value="0">Not available<option>');
	    }
	}