<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeBranchExam.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeBranchExam" %>
<script src="../AdminPanel/JS/CollegeBranch.js" type="text/javascript"></script> 
<input type="hidden" id="hdnExamObject"/>
<div id="divBranchExamCourse">
    <asp:HiddenField runat="server" ID="hdnCourseExamIndex" Value="1">
    </asp:HiddenField>
    <fieldset>
        <legend>Exam</legend>
        <ul>
            <li>
                <label>
                    Course</label>
                <select id="ddlCollegeExamCourse0" tabindex="1" title="Please select Course" onchange="showExamPop(0)"></select>    
                <input type="hidden" id="hdnCollegeExam0"  />          
            </li>     
        </ul>
    </fieldset>
    </div> 
    <div id="courseExamContainer">
                      </div><div id="examPopUpContainer">
                            </div>
<a href="#" style="float: right" onclick="AddExamFields();return false;">AddMore</a>
<script type="text/javascript">
    var examstatus = 1;
    var examResults = [];
    var courseListUrl = "/WebServices/CommonWebServices.asmx/GetAllCourseList";
    BindDropDown($("#ddlCollegeExamCourse0"), courseListUrl);

    function AddExamFields() {
        var index = $("#<%=hdnCourseExamIndex.ClientID %>").val();
        $("#courseExamContainer").append("<input id='hdnCollegeExam" + index + "' type='hidden'  /><fieldset id='fieldSet" + index + "'><legend>Exam</legend> <ul> <li><label>Course</label><select id='ddlCollegeExamCourse" + index + "' onchange= 'showExamPop(" + index + ")'></select></li></ul></fieldset></div>");
        var courseUrl = "../../WebServices/CommonWebServices.asmx/GetAllCourseList";
        BindDropDown($("#ddlCollegeExamCourse" + index), courseUrl);
        index++;
        $("#<%=hdnCourseExamIndex.ClientID %>").val(index);
    }

    function showExamPop(examIndex) {
        if ($("#ddlCollegeExamCourse" + examIndex).val() > 0) {
            if ($("#hdnCollegeExam" + examIndex).val() > 0) {
                alert("You have already selected");
                return false;
            } else {
                var urlExam = "../../WebServices/CommonWebServices.asmx/GetExamListByCourseId";
                $("#examPopUpContainer").append("<div id='divCollegeBranchExam" + examIndex + "' style='display:none;width: 520px' class='popup_block'><a href='#' style='float:right;color:red;' id='close" + examIndex + "' class='close' onclick='closeExamPopUp(" + examIndex + ");return false;'>X</a><fieldset><legend>Exam</legend><ul><li><label> Exam</label><select id='ddlCollegeExam" + examIndex + "' tabindex='1' title='Please select Exam'></select>  </li><li><label>Status</label><input type='checkbox' id='examStatus" + examIndex + "' tabindex='2' title='Please Check'/></li><li><label></label><input id='btnCourseStreamDone' class='close' type='button' onclick='SetExamValues(" + examIndex + ")' value='Next'/></li></ul></fieldset></div>");
                BindExam($("#ddlCollegeExam" + examIndex), $("#ddlCollegeExamCourse" + examIndex), urlExam);
                var popMargTop = ($("#divCollegeBranchExam" + examIndex).height() + 80) / 2;
                var popMargLeft = ($("#divCollegeBranchExam" + examIndex).width() + 80) / 2;
                $("#divCollegeBranchExam" + examIndex).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });
                $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
                $("#divCollegeBranchExam" + examIndex).show();
            }
        }
    }

    function closeExamPopUp(index) {
        if (examResults.length > 0) {
        if ($("#hdnArray").val() == "") {
                $("#hdnArray").val(examResults);
            } else {
                $("#hdnArray").val(examResults);
            }
            $('a.close, #fade').live('click', function() { //When clicking on the close or fade layer...
                $('#fade , .popup_block').fadeOut(300, function() {
                });
                return false;
            });
            $("#divCollegeBranchExam" + index).hide();
        }
    }

    function SetExamValues(index) {
        if (checkExamForm(index)) {
            $("#hdnCollegeExam" + index).val($("#ddlCollegeExamCourse" + index).val());
            if (examResults.length > 0) {
                for (var i = 0; i < examResults.length; i++) {
                    if ($("#ddlCollegeExam" + index).val() == examResults[i + 1].examId) {
                        examstatus = 0;
                        break;
                    }
                }
            }
            if (examstatus == 1) {
                examResults.push(
                    {
                        courseId: $("#ddlCollegeExamCourse" + index).val()
                    },
                    {
                        examId: $("#ddlCollegeExam" + index).val()
                    },
                    {
                        examStatus: $("#examStatus" + index).attr('checked') ? "True" : "False"
                    }
                );
            } else {
                alert('you have already selected');
            }
            clearExamFields(index);
        }
    }

    function checkExamForm(index) {
        var examName = $("#ddlCollegeExam" + index).val();
        if (examName <= 0) {
            alert("Please Select Exam ");
            return false;
        } else {
            return true;
        }
    }

    function clearExamFields(index) {
        $("#ddlCollegeExam" + index).val(0);
        $("#examStatus" + index).attr('checked', false);
        examstatus = 1;
    }
</script>
