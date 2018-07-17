<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentAccademicInfo.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.StudentAccademicInfo" %>

   <div  class="tabbed_area" id="tabAccademicStudent">
  <div class="prohead">
  <strong class="RedrightImglink">Academic Details</strong></div>
  <ul class="tabs" id="ulAccademic">
    </ul>
 </div>
    <div class="tab_container "  id="tabPrivateContainerAccademic">
    <span id="Privateloading" style="display:none"> 
    <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" />
    </span>

    </div>
    <div class="hide" id="divChooseCourse">
       <fieldset><legend>Choose Course </legend>
      <ul>
    <li>
    <label><%=Resources.label.Course%></label>
    <select id="slctChooseCourse" title="Select course"></select></li>

    <li>
    <label>&nbsp;</label>
    <input type="button" title="Update course" value="Insert" class="button" onclick="UpdateCourses()" />
    </li>
    </ul></fieldset>
    </div>
    <div class="hide" id="divHighSchool">
    <fieldset><legend>High School </legend>
    <input type="hidden" id="hdnHighSchoolId" value="0" />
    <ul>
    <li><label><%=Resources.label.BoardName%></label>
    <select id="slctBoardName" title="Select board" tabindex='1'></select>
       <label id="lblBoardError" class="hide error" title="Select board"></label>
    </li>
     <li><label>School Name:</label>
    <input type="text" id="txtHighSchoolName" title="Enter school name" placeholder="Enter school name" tabindex="2"/>
       <label id="lblHighSchoolNameError" class="hide error" title="Please enter  high school name"></label>
    </li>
     <li><label>CGPA or % Marks:</label>
    <input type="text" id="txtHighSchoolCgpa" title="Provide cgpa in digit"  placeholder="Provide cgpa in digit" tabindex="3"/>
       <label id="lblHighschoolCgpa" class="hide error" title="Please enter cgpa"></label>
    </li>
     <li><label>Year Of Passing:</label>
    <input type="text" id="txtHighSchoolYop" title="Enter year of passing"  placeholder="Enter year of passing" tabindex="4"/>
     <label id="lblHighSchoolYop" class="hide error" title="Please enter  year Of passing"></label>
    </li>
     <li><label>Study Mode:</label>
     <select id="slctHighSchoolStudeyMode" title="Select study mode"  tabindex='5'></select>
      <label id="lblHighSchoolStudyMode" class="hide error" title="Please select study mode"></label>
    </li>
     <li><label>&nbsp;</label>
    <input type="button" id="btnHighSchool" title="Click to finish process" value="Submit" tabindex="6" onclick="InsertUpdateHighSchool();return false;"/>
    </li>
    </ul>
    </fieldset>
    </div>
     <div class="hide" id="divInterMediate">
      <input type="hidden" id="hdnInterMediate" value="0" />
     <fieldset><legend>Intermediate</legend>
    <ul>
    <li><label><%=Resources.label.BoardName%></label>
    <select id="slctInterBoard" title="Select board " tabindex='1'></select>
    </li>
     <li><label>School Name:</label>
    <input type="text" id="txtInterMediateSchollName" title="Enter school name" placeholder="Enter school name" tabindex="2" />
    </li>
     <li><label>CGPA or % Marks:</label>
    <input type="text" id="txtIntermediateCgpa" title="Provide cgpa in digit" placeholder="Provide cgpa in digit" tabindex="3" />
    </li>
     <li><label>Year Of Passing:</label>
    <input type="text" id="txtInterMedaiteYop" title="Enter year of passing" placeholder="Enter year of passing" tabindex="4" />
    </li>
    <li><label>Specialization:</label>
      <input type="text" id="txtInterSpecilization" title="Enter specialization" placeholder="Enter specialization" tabindex="5" /> 
    </li>
     <li><label>Combination:</label>
    <input type="text" id="txtInterCombination" title="Enter subject combination" placeholder="Enter subject combination" tabindex="6" /> 
    </li>
       <li class="hide"><label>Marks:</label>
    <input type="text" id="txtInterMediateMarks" title="Enter marks" placeholder="Enter marks" tabindex="7" /> 
    </li>
    <li><label>Percentage:</label>
    <input type="text" id="txtInterPerc" title="Enter percentage" placeholder="Enter percentage" tabindex="8" /> 
    </li>
     <li><label>Study Mode:</label>
     <select id="slctInetrMediateStudeyMode" title="Select study mode"  tabindex='9'></select>
    </li>
     <li><label>&nbsp;</label>
    <input type="button" id="btnInterMediate" title="Click to finish process" onclick="InsertUpdateInterSchool();return false;"  value="Submit" tabindex="10"/>
    </li>
    </ul>
    
    </fieldset>
    </div>
    <div class="hide" id="divDiploma">
    <input id="hdnDiploma" value="0" type="hidden" />
     <fieldset><legend>Diploma</legend>
    <ul>
   
     <li><label>College Name:</label>
    <input type="text" id="txtDiplomaCollegName" title="Enter college name" placeholder="Enter college name" tabindex="1" /> 
    </li>
     <li><label>CGPA or % Marks :</label>
    <input type="text" id="txtDiploamaCgpa" title="Provide cgpa in digit" tabindex="2" placeholder="Provide cgpa in digit" /> 
    </li>
     <li><label>Year Of Passing:</label>
    <input type="text" id="txtDiplomaYop" title="Enter year of passing" tabindex="3"  placeholder="Enter year of passing"/> 
    </li>
    <li><label>Specialization:</label>
      <input type="text" id="txtDiplomaCourse" title="Enter specialization" tabindex="4" placeholder="Enter specialization" /> 
    </li>
     <li><label>Percentage:</label>
    <input type="text" id="txtDiplomaPerc" title="Enter percentage" tabindex="5" placeholder="Enter percentage" /> 
    </li>
     <li><label>&nbsp;</label>
    <input type="button" id="btnDiplomaSave" title="Please submit" value="Submit" onclick="InsertUpdateDiplomaSchool()" tabindex="6" /> 
    </li>
    </ul>
    
    </fieldset>
    </div>
     <div class="hide" id="divGraduation">
     <input type="hidden" vlaue="0" id="hdnGraduation" />
     <fieldset class="fleft"><legend>Graduation</legend>
    <ul>
   
     <li><label>College Name:</label>
    <input type="text" id="txtGradCollegeName" title="Enter college name" tabindex="1" placeholder="Enter college name" />
    </li>
     <li><label>CGPA or % Marks:</label>
    <input type="text" id="txtGradCgpa" title="Provide cgpa in digit" tabindex="2" placeholder="Provide cgpa in digit"/>
    </li>
     <li><label>Year Of Passing:</label>
    <input type="text" id="txtGradYop" title="Enter year of passing" tabindex="3" placeholder="Enter year of passing" /> 
    </li>
    <li><label>Specialization</label>
      <input type="text" id="txtGradSpecial" title="Enter specialization" placeholder="Enter specialization" tabindex="4" />
    </li>
     <li><label>Percentage:</label>
    <input type="text" id="txtGradPerc" title="Enter percentage" placeholder="Enter percentage" tabindex="5" /> 
    </li>
     <li><label>&nbsp;</label>
    <input type="button" id="btnGradSave" title="Click to finish process" onclick="InsertUpdateGradSchool()" value="Submit" tabindex="6" />
    </li>
    </ul>
    
    </fieldset>  <asp:HiddenField runat="server" id="hdnAccademicCourse">
    </asp:HiddenField>
    </div>
    <script type="text/javascript">     
        var number = /^[1-9]\d*(\.\d+)?$/; //number in digit and numeric form
        var charFormat = /^[a-zA-Z _ -]+$/; // character form
        var managementMode = "/WebServices/CommonWebServices.asmx/BindManagement";
        $("#slctHighSchoolStudeyMode").empty();
        $("#slctInetrMediateStudeyMode").empty();

        $("#slctBoardName").empty();
        $("#slctInterBoard").empty();
        BindDropDown($("#slctHighSchoolStudeyMode"), managementMode);
        var boardUrl = "/WebServices/CommonWebServices.asmx/GetStudentBoard";
        BindDropDown($("#slctBoardName"), boardUrl);
        BindDropDown($("#slctInterBoard"), boardUrl);
        BindFrontCourseList($("#slctChooseCourse"), $("#<%= hdnAccademicCourse.ClientID %>").val());
        BindDropDown($("#slctInetrMediateStudeyMode"), managementMode);
        function GetAccedemicInfo() {
            $("#tab3 .spnSuccess").html("");
            
            var boardUrl = "/WebServices/CommonWebServices.asmx/GetStudentBoard";
            BindDropDown($("#slctBoardName"), boardUrl);
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetAccademicInfoOfStudent",
                data: "{}",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindTabsAccademic(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });

        }

        function BindTabsAccademic(data) {
         
            $('#tabPrivateContainerAccademic').html("");$('#tabAccademicStudent ul').html('');
            if (data.d.length > 0) {
                if (data.d == 10) {
                    $('#tabAccademicStudent ul').append($('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails();return false;">10th</a></li>'));

                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails();
                }
                else if (data.d == 11) {

                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails();return false;">10th</a></li><li  rel="tabAccademicDiploma"><a href="#tabsAccademic-Diploma " onclick="GetDiplomaDetails();return false"  >Diploma</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademicDiploma" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails()
                }
                else if (data.d == 12) {
                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails();return false;" >10th</a></li><li  rel="tabAccademic12"><a href="#tabsAccademic-12" onclick="GetStudentInterMediateSchoolDetails();return false;" >12th</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademic12" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails();
                }
                else if (data.d == 13) {
                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10"  onclick="GetStudentHighSchoolDetails();return false;" >10th</a></li><li  rel="tabAccademic12"><a href="#tabsAccademic-12" onclick="GetStudentInterMediateSchoolDetails();return false;">12th</a></li><li  rel="tabAccademicDiploma"><a href="#tabsAccademic-Diploma" onclick="GetDiplomaDetails();return false">Diploma</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademic12" class="tab_contentAccademic"></div><div id="tabAccademicDiploma" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails()
                }
                else if (data.d == 15) {
                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails();return false;" >10th</a></li><li  rel="tabAccademic12"><a href="#tabsAccademic-12" onclick="GetStudentInterMediateSchoolDetails();return false;">12th</a></li><li  rel="tabAccademicGraduation"><a href="#tabsAccademic-Graduation" onclick="GetGraduationDetails();return false;" >Graduation</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademic12" class="tab_contentAccademic"></div><div id="tabAccademicGraduation" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails()
                } else {
                    $("#divChooseCourse").show();
                }
                $(".tab_contentAccademic").hide();
                $(".tab_contentAccademic:first").show();

                $("#ulAccademic li").click(function () {
                
                    $("#ulAccademic li").removeClass("active");
                    $(this).addClass("active");
                    $(".tab_contentAccademic").hide();
                    var activeTab = $(this).attr("rel");
                    $("#" + activeTab).fadeIn();
                });
            }
        }


        function GetStudentHighSchoolDetails() {
            $("#Privateloading").show();
            $("#divInterMediate").addClass("hide");
            $("#divDiploma").addClass("hide");
            $("#divGraduation").addClass("hide");
   
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetStudentHighSchoolDetails",
                data: "{}",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                   
                    BindHighShlDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
        function BindHighShlDetails(data) {

            if (data.d.length > 0) {
                $("#Privateloading").hide();
                BindHighSchoolDetails(data);
               
            }
            else {
                $("#Privateloading").hide();
                $("#divHighSchool").removeClass("hide");
               
             
              
               
            }

        }
        function BindHighSchoolDetails(data) {
              
            $("#btnHighSchool").val("Update");
            $("#divHighSchool").removeClass("hide");
            $("#divHighSchool").addClass("popup_block");
            $("#hdnHighSchoolId").val(data.d[0].HigherSecondaryScoreCardId);
            HighSchool(data);
             
            var finalData = "";
            var boardName = data.d[0].BoardName != "" ? data.d[0].BoardName : "N/A";
            var schoolName = data.d[0].HigherSecondarySchoolName != "" ? data.d[0].HigherSecondarySchoolName : "N/A";
            var cgpa = data.d[0].HigherSecondarySchoolScoreCGPA != "" ? data.d[0].HigherSecondarySchoolScoreCGPA : "N/A";
            var passingYear = data.d[0].HigherSecondarySchoolPassingYear != "" ? data.d[0].HigherSecondarySchoolPassingYear : "N/A";
            finalData = "<fieldset><legend>10th Standard</legend><ul><li><label>Board Name:</label><span class='text2'>" + boardName + "</span></li><li><label>School Name:</label><span class='text2'>" + schoolName + "</span></li><li><label>CGPA or % :</label><span class='text2'>" + cgpa + "</span></li><li><label>Passing Year:</label><span class='text2'>" + passingYear + "</span></li><li><a href='#' id='sndHigSchool' onclick='EditStandard(550);return false;'>Edit</a></li><ul></fieldset>";
            $("#tabAccademic10").html('');
          
            $("#tabAccademic10").append(finalData);
        }
        function EditStandard(width) {
            GetStudentHighSchoolDetails();
            OpenPoup('divHighSchool', width, 'sndHigSchool');return false;
        }

        function InsertUpdateHighSchool() {
           
            $("#Privateloading").show();
            var dataQuery="";
            var urlHighSchool = "";
            if ($("#slctBoardName").val() <= 0) {
                alert('Select board');   return false;
            }
            if ($("#txtHighSchoolName").val() == '' || $("#txtHighSchoolName").val().length==0) {
                alert('Field School Name cannot be blank'); return false;
            }
         if (!number.test($("#txtHighSchoolCgpa").val().trim()) && $("#txtHighSchoolCgpa").val().length!=0) {
             alert('Provide cgpa in digit');
                return false;
            }

            if ($("#txtHighSchoolYop").val() > (new Date).getFullYear() && $("#txtHighSchoolYop").val().length!=0) {
                alert('Provide year of passing less than current year');
                return false;
            }
            
           
            if ($("#hdnHighSchoolId").val() > 0) {


                dataQuery = '{"highSchoolId":"' + $("#hdnHighSchoolId").val() + '","HighSchoolName":"' + $("#txtHighSchoolName").val() + '","highSchoolYop":"' + $("#txtHighSchoolYop").val() + '","highSchoolCgp":"' + $("#txtHighSchoolCgpa").val() + '","boardId":"' + $("#slctBoardName").val() + '","studyModeId":"' + $("#slctHighSchoolStudeyMode").val() + '"}';
                urlHighSchool = "/WebServices/CommonWebServices.asmx/UpdateHighSchoolDetails";

            }
            else {
                dataQuery = '{"HighSchoolName":"' + $("#txtHighSchoolName").val() + '","highSchoolYop":"' + $("#txtHighSchoolYop").val() + '","highSchoolCgp":"' + $("#txtHighSchoolCgpa").val() + '","boardId":"' + $("#slctBoardName").val() + '","studyModeId":"' + $("#slctHighSchoolStudeyMode").val() + '"}';
                urlHighSchool = "/WebServices/CommonWebServices.asmx/InsertHighSchoolDetails"

            }
            $.ajax({
                type: "POST",
                url: urlHighSchool,
                data: dataQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d > 0) {
                        if ($("#hdnHighSchoolId").val() > 0) {
                            $("#btnHighSchool").val("Submit");
                            $("#hdnHighSchoolId").val("0");
                            GetStudentHighSchoolDetails();
                            alert("Updated successfully");
                            $("#divHighSchool").hide();
                            $("#fade").hide();
                        } else {
                            alert("Inserted successfully");
                            $("#divHighSchool").addClass("popup_block");
                        }
                    }

                },
                error: function (xml, textStatus, errorThrown) {
                    alert(xml.status + "||" + xml.responseText);
                }

            });

        }
        function GetStudentInterMediateSchoolDetails() {
            $("#divHighSchool").addClass("hide");

            $("#divDiploma").addClass("hide");
            $("#divGraduation").addClass("hide");
           
          
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetInterMediateDetails",
                data: "{}",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindInterSchoolDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
        function BindInterSchoolDetails(data) {

            if (data.d.length > 0) {
                $("#Privateloading").hide();
                BindInetrMediateSchoolDetails(data);

            }
            else {
                $("#Privateloading").hide();
                $("#divInterMediate").removeClass("hide");

                var boardUrl = "/WebServices/CommonWebServices.asmx/GetStudentBoard";
                BindDropDown($("#slctInterBoard"), boardUrl);

            }

        }
        function BindInetrMediateSchoolDetails(data) {
            $("#btnInterMediate").val("Update");
            $("#divInterMediate").removeClass("hide");
            $("#divInterMediate").addClass("popup_block");
            $("#hdnInterMediate").val(data.d[0].SeniorSecondaryScoreCardId);
            InterMediate(data);
            var boardName = data.d[0].BoardName != "" ? data.d[0].BoardName : "N/A";
            var schoolName = data.d[0].SeniorSecondarySchoolName != "" ? data.d[0].SeniorSecondarySchoolName : "N/A";

            var cgpa = data.d[0].SeniorSecondarySchoolScoreCgpa != "" ? data.d[0].SeniorSecondarySchoolScoreCgpa : "N/A";
            var percentage = data.d[0].SeniorSecondarySchoolSubjectPercent != "" ? data.d[0].SeniorSecondarySchoolSubjectPercent : "N/A";

            var subjectCombination = data.d[0].SeniorSecondarySchoolSubjectCombination != "" ? data.d[0].SeniorSecondarySchoolSubjectCombination : "N/A";
            var marks = data.d[0].SeniorSecondarySchoolSubjectMarks != "" ? data.d[0].SeniorSecondarySchoolSubjectMarks : "N/A";
            var passingYear = data.d[0].SeniorSecondarySchoolPassingYear != "" ? data.d[0].SeniorSecondarySchoolPassingYear : "N/A";
            var finalData = "";
            
            if (data.d.length > 0) {
                finalData += "<fieldset><legend  style='text-transform:none !important;'>12th Standard</legend><ul><li><label>Board Name:</label><span class='text2'>" + boardName + "</span></li><li><label>School Name:</label><span class='text2'>" + schoolName + "</span></li><li><label>CGPA or % :</label><span class='text2'>" + cgpa + "</span></li><li><label>Passing Year:</label><span class='text2'>" + passingYear + "</span></li><li><label>Subject Combination:</label><span class='text2'>" + subjectCombination + "</span></li><li><label>Subject Marks:</label><span class='text2'>" + marks + "</span></li><li><label>Percent:</label><span class='text2'>" + percentage + "</span></li><li><a href='#' id='sndInter' onclick='EditInterStandard(550);return false;'>Edit</a></li><ul></fieldset>";

            }
            $("#tabAccademic12").html('');
            $("#tabAccademic12").append(finalData);
        }
        function EditInterStandard(width) {

            GetStudentInterMediateSchoolDetails();
            OpenPoup('divInterMediate', width, 'sndInter'); return false;
        }

        function InsertUpdateInterSchool() {
            var dataQuery = "";
            var urlHighSchool = "";

            if ($("#slctInterBoard").val() <= 0) {
                alert('Select borad');return false;
            }
            if ($("#txtInterMediateSchollName").val() == '' || $("#txtInterMediateSchollName").val().length==0) {
                alert('Field college name cannot be blank'); return false;
            }
            if (!number.test($("#txtIntermediateCgpa").val().trim()) && $("#txtIntermediateCgpa").val().length!=0) {
                alert('Provide cgpa in digit');
                return false;
            }
            if ($("#txtInterMedaiteYop").val() > (new Date).getFullYear() && $("#txtInterMedaiteYop").val().length!=0) {
                alert('Provide year of passing less than current year');
                return false;
            }


            if (!charFormat.test($("#txtInterCombination").val().trim()) && $("#txtInterCombination").val().length !=0) {
                alert('Provide subject combination in character');
                return false;
            }
            if (!charFormat.test($("#txtInterSpecilization").val().trim()) && $("#txtInterSpecilization").val().length != 0) {
                alert('Provide specialization in character');
                return false;
            }
            if (!number.test($("#txtInterPerc").val().trim()) && $("#txtInterPerc").val().length!=0) {
                alert('Provide percentage in digit');
                return false;
            }
            else if ($("#txtInterPerc").val()>100) {
                alert('Provide percentage less than or equal to 100'); return false;
            }
           
            if ($("#hdnInterMediate").val() > 0) {


                dataQuery = '{"interSchoolId":"' + $("#hdnInterMediate").val() + '","interSchoolName":"' + $("#txtInterMediateSchollName").val() + '","interSchoolYop":"' + $("#txtInterMedaiteYop").val() + '","hinterSchoolCgp":"' + $("#txtIntermediateCgpa").val() + '","boardId":"' + $("#slctInterBoard").val() + '","studyModeId":"' + $("#slctInetrMediateStudeyMode").val() + '","interCombination":"' + $("#txtInterCombination").val() + '","interSpecial":"' + $("#txtInterSpecilization").val() + '","interPercentage":"' + $("#txtInterPerc").val() + '","inetrMarks":"' + $("#txtInterMediateMarks").val() + '"}';
                urlInterSchool = "/WebServices/CommonWebServices.asmx/UpdateInterMediateDetails";

            }
            else {
                dataQuery = '{"interSchoolName":"' + $("#txtInterMediateSchollName").val() + '","interSchoolYop":"' + $("#txtInterMedaiteYop").val() + '","hinterSchoolCgp":"' + $("#txtIntermediateCgpa").val() + '","boardId":"' + $("#slctInterBoard").val() + '","studyModeId":"' + $("#slctInetrMediateStudeyMode").val() + '","interCombination":"' + $("#txtInterCombination").val() + '","interSpecial":"' + $("#txtInterSpecilization").val() + '","interPercentage":"' + $("#txtInterPerc").val() + '","inetrMarks":"' + $("#txtInterMediateMarks").val() + '"}';
                urlInterSchool = "/WebServices/CommonWebServices.asmx/InsertInterMediateDetails"

            }
            $.ajax({
                type: "POST",
                url: urlInterSchool,
                data: dataQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d > 0) {
                        if ($("#hdnInterMediate").val() > 0) {

                            $("#btnInterMediate").val("Submit");
                            GetStudentInterMediateSchoolDetails();
                            alert("Update successfully"); $("#divInterMediate").hide();
                            $("#fade").hide();
                        } else {
                            $("#divInterMediate").addClass("popup_block");
                            alert("Inserted successfully");

                        }
                    }

                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });

        }


        function GetDiplomaDetails() {
            $("#Privateloading").show();
            $("#divHighSchool").addClass("hide");

            $("#divGraduation").addClass("hide");
            $("#divInterMediate").addClass("hide");

            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetDiplomaDetails",
                data: "{}",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindDiplomaDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        function BindDiplomaDetails(data) {

            if (data.d.length > 0) {
                BindDiplomaFullDetails(data);

            }
            else {

                $("#divDiploma").removeClass("hide");


            }

        }
        function BindDiplomaFullDetails(data) {
            $("#btnDiplomaSave").val("Update");
            $("#divDiploma").removeClass("hide");
            $("#divDiploma").addClass("popup_block");
            $("#hdnDiploma").val(data.d[0].StudentDicScoreCardId);

            Diploma(data);
         
            var schoolName = data.d[0].StudentDicCollegeName != "" ? data.d[0].StudentDicCollegeName : "N/A";

            var course = data.d[0].StudentDicCourse != "" ? data.d[0].StudentDicCourse : "N/A";
            var percentage = data.d[0].StudentDicPercentage != "" ? data.d[0].StudentDicPercentage : "N/A";

            var cgpa = data.d[0].StudentDicCGPA != "" ? data.d[0].StudentDicCGPA : "N/A";
      var passingYear = data.d[0].StudentDicYOP != "" ? data.d[0].StudentDicYOP : "N/A";
            var finalData = "";
            if (data.d.length > 0) {
                finalData += "<fieldset><legend>Diploma Standard</legend><ul><li><label>School Name:</label><span class='text2'>" + schoolName + "</span></li><li><label>Course:</label><span class='text2'>" + course + "</span></li><li><label>Percent:</label><span class='text2'>" + percentage + "</span></li><li><label>CGPA or % :</label><span class='text2'>" + cgpa + "</span></li><li><label>Passing Year:</label><span class='text2'>" + passingYear + "</span></li><li><a href='#' id='sndDiploma' onclick='EditDiplomaStandard(550);return false;'>Edit</a></li><ul></fieldset>";

            } $("#Privateloading").hide();
            $("#tabAccademicDiploma").html('');
            $("#tabAccademicDiploma").append(finalData);

        }
        function EditDiplomaStandard(width) {
            GetDiplomaDetails();

            OpenPoup('divDiploma', width, 'sndDiploma'); return false;
        }

        function InsertUpdateDiplomaSchool() {
            var dataQuery = "";
            var urlHighSchool = "";
            if ($("#txtDiplomaYop").val() > (new Date).getFullYear() && $("#txtDiplomaYop").val().length!=0) {
                alert('Provide year of passing less than current year');
                return false;
            }
            if (!number.test($("#txtDiploamaCgpa").val().trim()) && $("#txtDiploamaCgpa").val().length != 0) {
                alert('Provide cgpa in digit');
                return false;
            }
            if (!number.test($("#txtDiplomaPerc").val().trim() && $("#txtDiplomaPerc").val().trim())) {
                alert('Provide percentage in digit');
                return false;
            }
            else if ($("#txtDiplomaPerc").val() > 100) {
                alert('Provide percentage less than or equal to 100'); return false;
            }
            if ($("#hdnDiploma").val() > 0) {


                dataQuery = '{"diplomaSchoolId":"' + $("#hdnDiploma").val() + '","diplomaSchoolName":"' + $("#txtDiplomaCollegName").val() + '","diplomaSchoolYop":"' + $("#txtDiplomaYop").val() + '","diplomaSchoolCgp":"' + $("#txtDiploamaCgpa").val() + '","diplomaCourse":"' + $("#txtDiplomaCourse").val() + '","diplomaPer":"' + $("#txtDiplomaPerc").val() + '"}';
                urlDiplomaSchool = "/WebServices/CommonWebServices.asmx/UpdateDiplomaDetails";

            }
            else {
                dataQuery = '{"diplomaSchoolName":"' + $("#txtDiplomaCollegName").val() + '","diplomaSchoolYop":"' + $("#txtDiplomaYop").val() + '","diplomaSchoolCgp":"' + $("#txtDiploamaCgpa").val() + '","diplomaCourse":"' + $("#txtDiplomaCourse").val() + '","diplomaPer":"' + $("#txtDiplomaPerc").val() + '"}';
                urlDiplomaSchool = "/WebServices/CommonWebServices.asmx/InsertDiplomaDetails"

            }
            $.ajax({
                type: "POST",
                url: urlDiplomaSchool,
                data: dataQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d > 0) {
                        if ($("#hdnDiploma").val() > 0) {
                            $("#btnDiplomaSave").val("Submit");
                            GetDiplomaDetails();
                            alert("Update successfully"); $("#divDiploma").hide();
                            $("#fade").hide();
                        } else {
                            alert("Inserted successfully");
                            $("#divDiploma").addClass("popup_block");
                        }
                    }

                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });

        }


        function GetGraduationDetails() {
            $("#Privateloading").show();
            $("#divHighSchool").addClass("hide");


            $("#divInterMediate").addClass("hide");
            $("#divDiploma").addClass("hide");
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetGraduationDetails",
                data: "{}",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindGrdDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
        function BindGrdDetails(data) {

            if (data.d.length > 0) {
                $("#Privateloading").hide();
                BindGraduationDetails(data);

            }
            else {
                $("#Privateloading").hide();

                $("#divGraduation").removeClass("hide");


            }

        }
        function BindGraduationDetails(data) {
            $("#btnGradSave").val("Update");
            $("#divGraduation").removeClass("hide");
            $("#divGraduation").addClass("popup_block");
            $("#hdnGraduation").val(data.d[0].StudentGrdScorecardId);

            Graduation(data);
            var schoolName = data.d[0].StudentGrdCollegeName != "" ? data.d[0].StudentGrdCollegeName : "N/A";

            var special = data.d[0].StudentGrdSpecialization != "" ? data.d[0].StudentGrdSpecialization : "N/A";
            var percentage = data.d[0].StudentGrdPer != "" ? data.d[0].StudentGrdPer : "N/A";

            var cgpa = data.d[0].StudentGrdCGPA != "" ? data.d[0].StudentGrdCGPA : "N/A";
            var passingYear = data.d[0].StudentGrdYOP != "" ? data.d[0].StudentGrdYOP : "N/A";
            var finalData = "";
            if (data.d.length > 0) {
                finalData += "<fieldset><legend>Graduation Standard</legend><ul><li><label>College:</label><span class='text2'>" + schoolName + "</span></li><li><label>Specialization:</label><span class='text2'>" + special + "</span></li><li><label>Percent:</label><span class='text2'>" + percentage + "</span></li><li><label>CGPA:</label><span class='text2'>" + cgpa + "</span></li><li><label>Passing Year:</label><span class='text2'>" + passingYear + "</span></li><li><a href='#' id='sndGrad' onclick='EditGradStandard(550);return false;'>Edit</a></li><ul></fieldset>";

            }

            $("#tabAccademicGraduation").html('');
            $("#tabAccademicGraduation").append(finalData);

        }
        function EditGradStandard(width) {
            GetGraduationDetails();

            OpenPoup('divGraduation', width, 'sndGrad'); return false;
        }

        function InsertUpdateGradSchool() {
            var dataQuery = "";
            var urlGradSchool = "";

            if (!number.test($("#txtGradCgpa").val().trim()) && $("#txtGradCgpa").val().length != 0) {
                alert('Provide cgpa in digit');
                return false;
            }

            if ($("#txtGradYop").val() > (new Date).getFullYear() && $("#txtGradYop").val().length!=0) {
             alert('Provide year of passing less than current year');
                return false;
            }
            if (!charFormat.test($("#txtGradSpecial").val().trim()) && $("#txtGradSpecial").val().length != 0) {
                alert('Provide specialization in character');
                return false;
            }
            if (!number.test($("#txtGradPerc").val().trim()) && $("#txtGradPerc").val().length != 0) {
                alert('Provide percentage in digit');
                return false;
            }
            else if ($("#txtGradPerc").val() > 100) {
                alert('Provide percentage less than or equal to 100'); return false;
            }
            if ($("#hdnGraduation").val() > 0) {


                dataQuery = '{"gradlId":"' + $("#hdnGraduation").val() + '","gradCollegeName":"' + $("#txtGradCollegeName").val() + '","gradYop":"' + $("#txtGradYop").val() + '","gradCgp":"' + $("#txtGradCgpa").val() + '","gradSpecial":"' + $("#txtGradSpecial").val() + '","gradPer":"' + $("#txtGradPerc").val() + '"}';
                urlGradSchool = "/WebServices/CommonWebServices.asmx/UpdateGraduationDetails";

            }
            else {
                dataQuery = '{"gradCollegeName":"' + $("#txtGradCollegeName").val() + '","gradYop":"' + $("#txtGradYop").val() + '","gradCgp":"' + $("#txtGradCgpa").val() + '","gradSpecial":"' + $("#txtGradSpecial").val() + '","gradPer":"' + $("#txtGradPerc").val() + '"}';
                urlGradSchool = "/WebServices/CommonWebServices.asmx/InsertGraduationDetails"

            }
            $.ajax({
                type: "POST",
                url: urlGradSchool,
                data: dataQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d > 0) {
                        if ($("#hdnGraduation").val() > 0) {
                            $("#btnGradSave").val("Submit");
                            
                            GetGraduationDetails();
                            alert("Update successfully"); $("#divGraduation").hide();
                            $("#fade").hide();
                        } else {
                            alert("Inserted successfully");
                            $("#divGraduation").addClass("popup_block");
                        }
                    }

                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });

        }
        function UpdateCourses() {

            var courseQuery = '{"courseId":"' + $("#slctChooseCourse").val() + '"}'
            UpdateCourse(courseQuery);
        }
        function ValidateHighSchool() {

            var status = true;
            if ($("#slctBoardName").val() == 0) {
                $("#lblBoardError").removeClass("hide");
                $("#lblBoardError").text("Select Board");

                status = false;
            }
            else {
                $("#lblBoardError").css("display", "none");
                status = true;
            }

            if ($("#txtHighSchoolName").val() == "") {
                $("#lblHighSchoolNameError").removeClass("hide");
                $("#lblHighSchoolNameError").text("Enter school name");

                status = false;
            } else {
                $("#lblHighSchoolNameError").css("display", "none");
                status = true;
            }
            if ($("#txtHighSchoolCgpa").val() == "") {
                $("#lblHighschoolCgpa").removeClass("hide");
                $("#lblHighschoolCgpa").text("Enter cgpa");

                status = false;
            } else {
                $("#lblHighschoolCgpa").css("display", "none");
                status = true;
            }
            if ($("#txtHighSchoolYop").val() == "") {
                $("#lblHighSchoolYop").removeClass("hide");
                $("#lblHighSchoolYop").text("Enter year of passing");

                status = false;
            } else {
                $("#lblHighSchoolYop").css("display", "none");
                status = true;
            }
            if ($("#slctHighSchoolStudeyMode").val() == "") {
                $("#lblHighSchoolStudyMode").removeClass("hide");
                $("#lblHighSchoolStudyMode").text("Select study mode");

                status = false;
            } else {
                $("#lblHighSchoolStudyMode").css("display", "none");
                status = true;
            } 
       
            if (status == true) {
                return true;
            }
            else {
                return false;
            }

        }
      
        function HighSchool(data) {
            $("#slctBoardName").val(data.d[0].BoardId);
            $("#txtHighSchoolName").val(data.d[0].HigherSecondarySchoolName);
            $("#txtHighSchoolCgpa").val(data.d[0].HigherSecondarySchoolScoreCGPA);
            $("#txtHighSchoolYop").val(data.d[0].HigherSecondarySchoolPassingYear);
            $("#slctHighSchoolStudeyMode").val(data.d[0].StudyModeId);
        }
        function InterMediate(data) {
            $("#slctInterBoard").val(data.d[0].BoardId);
            $("#txtInterMediateSchollName").val(data.d[0].SeniorSecondarySchoolName);
            $("#txtIntermediateCgpa").val(data.d[0].SeniorSecondarySchoolScoreCgpa);
            $("#txtInterMedaiteYop").val(data.d[0].SeniorSecondarySchoolPassingYear);
            $("#slctInetrMediateStudeyMode").val(data.d[0].StudyModeId);
            $("#txtInterPerc").val(data.d[0].SeniorSecondarySchoolSubjectPercent);
            $("#txtInterMediateMarks").val(data.d[0].SeniorSecondarySchoolSubjectMarks);
            $("#txtInterCombination").val(data.d[0].SeniorSecondarySchoolSubjectCombination);
            $("#txtInterSpecilization").val(data.d[0].SeniorSecondarySchoolSpecialization);
        }
        function Diploma(data) {
            $("#txtDiplomaCollegName").val(data.d[0].StudentDicCollegeName);
            $("#txtDiploamaCgpa").val(data.d[0].StudentDicCGPA);
            $("#txtDiplomaYop").val(data.d[0].StudentDicYOP);

            $("#txtDiplomaCourse").val(data.d[0].StudentDicCourse);
            $("#txtDiplomaPerc").val(data.d[0].StudentDicPercentage);
        }
        function Graduation(data) {
            $("#txtGradCollegeName").val(data.d[0].StudentGrdCollegeName);
            $("#txtGradCgpa").val(data.d[0].StudentGrdCGPA);
            $("#txtGradPerc").val(data.d[0].StudentGrdPer);

            $("#txtGradYop").val(data.d[0].StudentGrdYOP);
            $("#txtGradSpecial").val(data.d[0].StudentGrdSpecialization);

        }
    </script>
