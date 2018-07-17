var queryPageSize = 10;
var queryPageIndex = 1;

var countryList = "/WebServices/CommonWebServices.asmx/GetCountryList";
var stateList = "/WebServices/CommonWebServices.asmx/GetAllState";
var cityList = "/WebServices/CommonWebServices.asmx/GetAllCityWithoutId";
        BindDropDown($("#slctCountry"), countryList);
        BindDropDown($("#slctState"), stateList);
        BindDropDown($("#slctCity"), cityList);
       
     $(".tab_content").hide();
     $(".tab_content:first").show('slow');

     $("#ulTopRanked li").click(function () {
        
         $("#tab3 .spnSuccess").html("");
         $("#noRecords").hide();
         $("#ulTopRanked li").removeClass("active");
         $(this).addClass("active");
         $(".tab_content").hide();
         var activeTab = $(this).attr("rel");
      
         $("#" + activeTab).show('slow');
         return false;
     });
        $(".tab_content1").hide();
        $(".tab_content1:first").show('slow');

        $("#ulPrivate li").click(function () {
            $("#tab3 .spnSuccess").html("");

            $("#ulPrivate li").removeClass("active");
            $(this).addClass("active");
            $(".tab_content1").hide();
            var activeTab = $(this).attr("rel");
            $("#" + activeTab).show('slow'); return false;
        });


         
        GetLoginDetails();
        function openFullName(control1, control2, control3, control4, control5) {
            
            $("#tab3 .spnSuccess").html("");
            $(control1).removeClass("hide");
            $(control1).addClass("show");

            $(control2).removeClass("show");
            $(control2).addClass("hide");

            $(control3).removeClass("show");
            $(control3).addClass("hide");

            $(control4).removeClass("hide");
            $(control4).addClass("show");


            $(control5).removeClass("hide");
            $(control5).addClass("show");
         

        }
        function openDobName(control1, control2, control3, control4, control5) {

            $("#tab3 .spnSuccess").html("");
            $(control1).removeClass("hide");
            $(control1).addClass("show");

            $(control2).removeClass("show");
            $(control2).addClass("hide");

            $(control3).removeClass("show");
            $(control3).addClass("hide");

            $(control4).removeClass("hide");
            $(control4).addClass("show");


            $(control5).removeClass("hide");
            $(control5).addClass("show");

            $("#spnDob").removeClass("hide");
        }
        function Update(control1, control2, control3, control4, control5, field) {
          
            
            
            if ($(control1).val() == "") {
                alert("Please  Input Field"); return false;
            }
            switch (field) {
                case "AjUserMobile":
                    {
                        if (!validatePhone($(control1).val())) {
                            alert("Enter your 10 digit mobile number"); return false;
                        }
                        break;
                    }

                case "AjUserDOB":
                    {

                        var dob = $(control1).val().match(/^\d\d?\/\d\d?\/\d\d\d\d$/);
                      
                        if (dob == null) {
                            alert(" Invalid Fields selection, please try again"); return false;
                        }
                        break;
                    }
                case "AjUserPhoneNo":
                    {
                        var phone = $(control1).val();
                       var intRegex = /[0-9 -()+]+$/;
                        if ((phone.length < 6) || (!intRegex.test(phone))) {
                            alert(' Incorrect phone format, please try again');
                            return false;
                        }

                        break;
                    }
                case "AjUserPincode":
                    {
                        if (!isValidPinCode($(control1).val())) {
                            alert(" Incorrect pincode format, please try again"); return false;
                        }
                        break;
                    }

            }
            UpdateFields(control1, control2, control3, control4, control5, field);
            
            
        }
        function Cancel(control1, control2, control3, control4, control5) {
            $("#tab3 .spnSuccess").html("");
            $(control1).removeClass("show");
            $(control1).addClass("hide");

            $(control2).removeClass("hide");
            $(control2).addClass("show");

            $(control3).removeClass("hide");
            $(control3).addClass("show");

            $(control4).removeClass("show");
            $(control4).addClass("hide");


            $(control5).removeClass("show");
            $(control5).addClass("hide");

            $("#spnDob").addClass("hide");
        }

        function openCityEdit(control1, control3, control4, control5) {
            $("#tab3 .spnSuccess").html("");
            $("#lblCountry").addClass("hide");
            $("#lblCountry").removeClass("show");

            $("#lblState").removeClass("show");
            $("#lblState").addClass("hide");
            $("#lblCity").removeClass("show");
            $("#lblCity").addClass("hide");

            $("#slctCountry").addClass("show");
            $("#slctCountry").removeClass("hide");

            $("#slctState").removeClass("hide");
            $("#slctState").addClass("show");
            $("#slctCity").removeClass("hide");
            $("#slctCity").addClass("show");

            $(control3).removeClass("show");
            $(control3).addClass("hide");

            $(control4).removeClass("hide");
            $(control4).addClass("show");

            $(control5).removeClass("hide");
            $(control5).addClass("show"); $("#spnDob").addClass("hide");
        }
        function CancelCity(control1, control3, control4, control5) {
            $("#tab3 .spnSuccess").html("");
            $("#lblCountry").addClass("show");
            $("#lblCountry").removeClass("hide");

            $("#lblState").removeClass("hide");
            $("#lblState").addClass("show");
            $("#lblCity").removeClass("hide");
            $("#lblCity").addClass("show");

            $("#slctCountry").addClass("hide");
            $("#slctCountry").removeClass("show");

            $("#slctState").removeClass("show");
            $("#slctState").addClass("hide");
            $("#slctCity").removeClass("show");
            $("#slctCity").addClass("hide");
           
            $(control3).removeClass("hide");
            $(control3).addClass("show");

            $(control4).removeClass("show");
            $(control4).addClass("hide");

            $(control5).removeClass("show");
            $(control5).addClass("hide"); $("#spnDob").addClass("hide");
        }
        function UpdateCity(control1, control3, control4, control5) {
        
            if ($("#slctCountry").val() <= 0) {
                alert("Please select country");
                return false;
            }
            else if ($("#slctState").val() <= 0) {
                alert("Please select state");
                return false;
            }
            else if ($("#slctCity").val() <= 0) {
                alert("Please select city");
                return false;
            }
            else {
                var dataQuery1 = '{"country":"' + $("#slctCountry").val() + '","state":"' + $("#slctState").val() + '","city":"' + $("#slctCity").val() + '"}';

                $.ajax({
                    type: "POST",
                    url: "/WebServices/CommonWebServices.asmx/UpdateUserCityDetails",
                    data: dataQuery1,
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d > 0) {
                            $("#spnDob").addClass("hide");
                            GetCityFields(control1, control3, control4, control5);
                        } else {
                            alert("Something wrong while updating ");
                        }
                    },
                    error: function (xml, textStatus, errorThrown) {
                        //alert(xml.status + "||" + xml.responseText);
                    }
                });

                $(control3).removeClass("hide");
                $(control3).addClass("show");

                $(control4).removeClass("show");
                $(control4).addClass("hide");

                $(control5).removeClass("show");
                $(control5).addClass("hide");
            }
        }
        function openGender(control1, control3, control4, control5) {
            
            $("#tab3 .spnSuccess").html("");
            $("#lblGender").removeClass("show");
            $("#lblGender").addClass("hide");
            $("#ctl00_cphBody_rbtGender").removeClass("hide");
            $("#ctl00_cphBody_rbtGender").addClass("show");
            $(control3).removeClass("show");
            $(control3).addClass("hide");

            $(control4).removeClass("hide");
            $(control4).addClass("show");

            $(control5).removeClass("hide");
            $(control5).addClass("show");
        }
        function UpdateGender(name, control1, control3, control4, control5) {
            if ($("#ctl00_cphBody_rbtGender").val() <= 0) {
                alert("Please select gender");
             } else {
                updateGender(control1, control3, control4, control5);

                $(control3).removeClass("hide");
                $(control3).addClass("show");

                $(control4).removeClass("show");
                $(control4).addClass("hide");

                $(control5).removeClass("show");
                $(control5).addClass("hide");
            }
        }
        function CancelGender(control1, control3, control4, control5) {
            $("#tab3 .spnSuccess").html("");
            $("#lblGender").removeClass("hide");
            $("#lblGender").addClass("show");
            $("#ctl00_cphBody_rbtGender").removeClass("show");
            $("#ctl00_cphBody_rbtGender").addClass("hide");

            $(control3).removeClass("hide");
            $(control3).addClass("show");

            $(control4).removeClass("show");
            $(control4).addClass("hide");

            $(control5).removeClass("show");
            $(control5).addClass("hide");
        }
        function GetLoginDetails() {
            $("#progress").show();
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetUserListById",
                data: "{}",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindLoggedUserDetails(response);
                    FillCoursePreferedData(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
        function BindLoggedUserDetails(response) {
            
            if (response.d.length > 0) {
                $("#progress").hide();
            for(var i=0;i<response.d.length;i++){
                $("#lblFullName").text(response.d[i].UserFullName);
                $("#txtFullName").val(response.d[i].UserFullName); 
                $("#lblEmailId").text(response.d[i].UserEmailid);
                $("#txtEmailIdPwd").val(response.d[i].UserEmailid);
                $("#lblMobileNumber").text(response.d[i].MobileNo);
                $("#txtMobileNumber").val(response.d[i].MobileNo);
                $("#hdnUserCourseId").val(response.d[i].CourseId);

                if (response.d[i].UserImage == null || response.d[i].UserImage == "") {
                    $("#userImage").attr("src", "/image.axd?User=NoImage.jpg");
                    }
                else {
                    $("#userImage").attr("src", "/image.axd?User=" + response.d[i].UserImage);
                }
               
                $("#lblPhoneNumber").text((response.d[i].PhoneNo != "" || response.d[i].PhoneNo != null) ? response.d[i].PhoneNo : "N/A");
                $("#txtPhoneNumber").val(response.d[i].PhoneNo);

                $("#lblPinCode").text((response.d[i].UserPincode!=""||response.d[i].UserPincode!=null)?response.d[i].UserPincode:"N/A");
                $("#txtPinCode").val(response.d[i].UserPincode);

                $("#lblMailingAddress").text((response.d[i].UserCorrespondenceAddress != "" || response.d[i].UserCorrespondenceAddress != null) ? response.d[i].UserCorrespondenceAddress : "N/A");
                $("#txtMailingAddress").val(response.d[i].UserCorrespondenceAddress);
                $("#lblPermanentAddress").text((response.d[i].UserPermanentAddress != "" || response.d[i].UserPermanentAddress != null) ? response.d[i].UserPermanentAddress : "N/A");
                $("#txtPermanentAddress").val(response.d[i].UserPermanentAddress);
                var ds = new Date(parseInt(/\/Date\((\d+).*/.exec(response.d[i].UserDOB)[1]));
                $("#lblDob").text(ds.format("dd/MM/yyyy"));
                $("#ctl00_cphBody_txtDOB").val(ds.format("dd/MM/yyyy"));
               if (response.d[i].UserGender == "1") {

                    $("#lblGender").text("Male");
                }
                 
                else if (response.d[i].UserGender.trim() == "2") {
                   $("#lblGender").text("Female");
                }
                else {
                    $("#lblGender").text("N/A");
                }
               
                $("#ctl00_cphBody_rbtGender").val(response.d[i].UserGender);
             
                $("#lblCountry").text(response.d[i].CountryName);
                $("#lblState").text(response.d[i].StateName);
                $("#lblCity").text(response.d[i].CityName);
                $("#slctCountry").val(response.d[i].CountryCode);
                $("#slctState").val(response.d[i].StateId);
                $("#slctCity").val(response.d[i].CityId);
            }
           }
    }
    function GetCity() {
        var stateId = $("#slctState").val();
        $("#slctCity").empty(); $("#slctCity").append('<option selected="selected" value="0">--Select--</option>');
        if (stateId != 0) {
            var stateQuery = '{"stateId":"' + stateId + '"}';

            var cityUrl = "/WebServices/CommonWebServices.asmx/GetCity";
            BindDropDownForData($("#slctCity"), cityUrl, stateQuery);
        } else {
            $("#slctCity").append('<option selected="selected" value="0">--Select--</option>');
        }
    }
    function GetState() {

        var countryId = $("#slctCountry").val();
       $("#slctState").empty();
        $("#slctCity").empty(); $("#slctCity").append('<option selected="selected" value="0">--Select--</option>');
    
        if (countryId != 0) {

           
            var countryQuery = '{"countryId":"' + countryId + '"}';
            var stateUrl = "/WebServices/CommonWebServices.asmx/GetState";
            BindDropDownForData($("#slctState"), stateUrl, countryQuery);
        } else {
            $("#slctState").append('<option selected="selected" value="0">--Select--</option>');
        }
     }

     function UpdateFields(control, control2, control3, control4, control5, field) {

         var dataQuery = '{"value":"' + $(control).val() + '","field":"' + field + '"}';

         $.ajax({
             type: "POST",
             url: "/WebServices/CommonWebServices.asmx/UpdateUserProfileDetails",
             data: dataQuery,
             async: true,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 if (response.d > 0) {
                     $("#spnDob").addClass("hide");
                     GetFields(control, control2, control3, control4, control5);
                 } else {
                     alert("Something wrong while updating " + $(control).val());
                 }
             },
             error: function (xml, textStatus, errorThrown) {
                 //alert(xml.status + "||" + xml.responseText);
             }
         });
     }
     function GetFields(control, control2, control3, control4, control5) {
         $("#tab3 .spnSuccess").html("");
         $("#tab3 .spnSuccess").append('<label id="lblSuccess" class="success">Your record has been updated uccessFully</label>');
         GetLoginDetails();
         $(control).removeClass("show");
         $(control).addClass("hide");

         $(control2).removeClass("hide");
         $(control2).addClass("show");

         $(control3).removeClass("hide");
         $(control3).addClass("show");

         $(control4).removeClass("show");
         $(control4).addClass("hide");


         $(control5).removeClass("show");
         $(control5).addClass("hide");
        
     }
     function GetCityFields(control, control3, control4, control5) {

         $("#tab3 .spnSuccess").html("");
         $("#tab3 .spnSuccess").append('<label id="lblSuccess" class="success">Your record has been updated successFully</label>');
         GetLoginDetails();
         $("#lblCountry").addClass("show");
         $("#lblCountry").removeClass("hide");

         $("#lblState").removeClass("hide");
         $("#lblState").addClass("show");
         $("#lblCity").removeClass("hide");
         $("#lblCity").addClass("show");

         $("#slctCountry").addClass("hide");
         $("#slctCountry").removeClass("show");

         $("#slctState").removeClass("show");
         $("#slctState").addClass("hide");
         $("#slctCity").removeClass("show");
         $("#slctCity").addClass("hide");
         
        
         $(control3).removeClass("hide");
         $(control3).addClass("show");

         $(control4).removeClass("show");
         $(control4).addClass("hide");


         $(control5).removeClass("show");
         $(control5).addClass("hide");
      
     }
     function updateGender(control1, control3, control4, field) {
         var dataQuery = '{"value":"' + $("#ctl00_cphBody_rbtGender").val() + '","field":"' + field + '"}';

         $.ajax({
             type: "POST",
             url: "/WebServices/CommonWebServices.asmx/UpdateUserProfileDetails",
             data: dataQuery,
             async: true,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 if (response.d > 0) {

                     GetFieldsGender(control1, control3, control4);
                 } else {
                     alert("Something wrong while updating " + $(control).val());
                 }
             },
             error: function (xml, textStatus, errorThrown) {
                 //alert(xml.status + "||" + xml.responseText);
             }
         });
     }
     function GetFieldsGender(control1, control2, control3) {
         $("#tab3 .spnSuccess").html("");
         $("#tab3 .spnSuccess").append('<label id="lblSuccess" class="success">Your record has been updated uccessFully</label>');
         GetLoginDetails();
         $("#lblGender").removeClass("hide");
         $("#lblGender").addClass("show");
         $("#ctl00_cphBody_rbtGender").removeClass("show");
         $("#ctl00_cphBody_rbtGender").addClass("hide");
         $(control1).removeClass("hide");
         $(control1).addClass("show");

         $(control2).removeClass("show");
         $(control2).addClass("hide");


         $(control3).removeClass("show");
         $(control3).addClass("hide");
       
     }
     function GetExamAppearedForStudent() {
         $("#tab3 .spnSuccess").html("");
         $("#noExamAppeared").html("");
        $("#progress").show();
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetStudentExamAppeared",
            data: "{}",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {


                FillExamAppearedData(response);

            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillExamAppearedData(data) {
        var columnData = "<table class='grdView' style='width:100%;'><tr style='background:#eff2f7'><td>S.No</td><td>Exam Name</td><td> Exam Rank</td></tr>";
        var finalData="";;
        if (data.d.length) { 
        for(var i=0;i<data.d.length; i++){
            finalData += "<tr><td>" + (i+1) + "</td><td>" + data.d[i].ExamName + "</td><td>" + data.d[i].AjExamAppRank + "</td></tr>";

        }$("#examAppeared").html("");
        finalData = columnData + finalData + "</table>";
        $("#progress").hide();
        $("#examAppeared").append(finalData);
        }
    else {
        $("#progress").hide();
        $("#noExamAppeared").addClass("show");
        $("#noExamAppeared").text("You have not appeared in any exam");
         }
}

function AddExamFields() {
    var examCount = $("#hdnExamCount").val();
   
    if (examCount < 3) {
        var examData = "";

        examData += "<li id='liExam" + examCount + "'><label>Exam Name</label><input type='text' id='txtExamName" + examCount + "' title='Please Enter Exam' tabindex='1' /></li><li id='liRank" + examCount + "'><label>Exam Rank</label><input type='text' id='txtExamRank" + examCount + "' title='Please Enter Rank' tabindex='1' /><a href='#' onclick='Remove(" + examCount + ");return false;'>X</a></li>";
        examCount++;
        if (examCount==3) {
        $("#anchAdd").hide();}
        $("#hdnExamCount").val(examCount);
    }

    $(".liExam").append(examData);
    var url = "/WebServices/CommonWebServices.asmx/GetAllExamListFront";
    for (var j = 1; j < examCount; j++) {
        BindDropDownCommon($("#txtExamName"+j), url);
    }
}

function InsertExamAppeared() {
    if ($("#txtExamName0").val() == "") {
        alert("Please enter exam name"); return false;
    } 
     else {
         for (var i = 0; i < $("#hdnExamCount").val(); i++) {
             var examRank = $("#txtExamRank" + i).val() !== "" ? $("#txtExamRank" + i).val() : "N/A";
            if ($("#txtExamName" + i).val() != "") {
                var dataQuery = '{"examName":"' + $("#txtExamName" + i).val() + '","rank":"' + examRank + '"}';

                $.ajax({
                    type: "POST",
                    url: "/WebServices/CommonWebServices.asmx/StudentInsertExamAppeared",
                    data: dataQuery,
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if ($("#hdnExamCount").val() <= 3) {
                            $("#examPopup").hide(); $("#fade").hide();
                            ClearExamFields();
                            GetExamAppearedForStudent();
                        }

                    },
                    error: function (xml, textStatus, errorThrown) {
                        //alert(xml.status + "||" + xml.responseText);
                    }
                });
            }
        } 
    }
}
function ClearExamFields() {
    for (var i = 0; i < $("#hdnExamCount").val(); i++) {
        $("#txtExamName" + i).val('');
        $("#txtExamRank" + i).val('');
    }
}
function showExam() {
    var url = "/WebServices/CommonWebServices.asmx/GetAllExamListFront";
    BindDropDownCommon($("#txtExamName0"), url);
    ClearExamFields();
    OpenPoup('examPopup', 500, 'sndExam');

}
function Remove(index) {
    
    $("#liExam" + index).empty();
    $("#liRank" + index).empty();
    var examCount = $("#hdnExamCount").val();
    examCount=examCount-1;
    $("#hdnExamCount").val(examCount);
}
function InsertCollege() {

    if ($("#txtCollegeInsert").val() == "") {
        alert("Please enter college");
        return false;
    } else {

        if ($("#hdnFinalCourse").val() > 0) {
            $("#progress").show();
            var collegQuery = '{"courseId":"' + $("#hdnFinalCourse").val() + '","collegeName":" ' + $("#txtCollegeInsert").val() + '"}';
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/StudentCollegePrefrenceInsert",
                data: collegQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d > 0) {
                        $("#txtCollegeInsert").val('');
                        GetStudentCollegePreference();
                    } else {
                        $("#progress").hide();
                        alert("You have already inserted college " + $("#txtCollegeInsert").val());
                        $("#txtCollegeInsert").val("");
                    }
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        } else {
            alert("Please choose course first");
            return false;
        }
    }
}

function GetStudentCollegePreference() {
    $("#txtCollegeInsert").val('');
    $("#tab3 .spnSuccess").html("");
    var url = "/WebServices/CommonWebServices.asmx/GetCollegeByCourseSearch";
    var dataQuery = '{"courseId":"' + $("#hdnUserCourseId").val() + '" }';
 
        $("#CollegePreFfered").html("");
        $("#progress").show();
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetStudentCollegePreference",
            data: "{}",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {


                FillCollegePreferdData(response);

            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillCollegePreferdData(data) {
        var columnData = "<table class='grdView' style='width:100%'><tr style='background:#eff2f7'><td>S.No</td><td>College Name</td><td>Popular Name</td><td>WebSite</td><td>Action</td></tr>";
        var finalData = "";
        if (data.d.length == 3) { $("#divCollegeInsert").removeClass("show"); $("#divCollegeInsert").addClass("hide"); } else { $("#divCollegeInsert").addClass("show"); }
        if (data.d.length) {
            for (var i = 0; i < data.d.length; i++) {
                finalData += "<tr><td>" + (i + 1) + "</td><td>" + data.d[i].CollegeName + "</td><td>" + data.d[i].CollegePopularName + "</td><td><a target='_blank' href='http://" + data.d[i].AjCollegeBranchWebSite + "' title='" + data.d[i].AjCollegeBranchWebSite + "'>" + data.d[i].AjCollegeBranchWebSite + "</a></td><td><a href='#' onclick='DeleteCollegePreference(" + data.d[i].AjStudentCollegePreferenceId + ");return false;'>Delete</a></td></tr>";

            } $("#CollegePreFfered").html("");
            finalData = columnData + finalData + "</table>";
            $("#progress").hide();
            $("#CollegePreFfered").append(finalData);
        }
        else {
           
            $("#progress").hide();$("#divCollegeInsert").removeClass("hide") ;
            
          }
    }
    function DeleteCollegePreference(collegePrefernceId) {
      
        if (confirm("Are you sure to delete this college ")) {
            var collegQuery = '{"collegePrefernceId":"' + collegePrefernceId + '"}';
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/DeleteCollegePreference",
                data: collegQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    if (response.d > 0) {
                        GetStudentCollegePreference();
                    }
                },
                error: function(xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
    }

    function StreamPrefernceInsert() {
        $("#tab3 .spnSuccess").html("");


        if ($("#slctStreamPrefernceInsert").val() <= 0) {
            alert("Please select stream to insert");
            return false;
        } else {
            if ($("#hdnFinalCourse").val() > 0) {
              
                var streamQuery = '{"streamId":"' + $("#slctStreamPrefernceInsert").val() + '"}';
                $.ajax({
                    type: "POST",
                    url: "/WebServices/CommonWebServices.asmx/StreamPrefernceInsert",
                    data: streamQuery,
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d > 0) {
                            $("#slctStreamPrefernceInsert ").val(0);
                            GetCourseStreamPreferedByStudent();
                        } else {

                            alert("You have already prefered " + $("#slctStreamPrefernceInsert option:selected").text() + " stream"); $("#slctStreamPrefernceInsert ").val(0);
                            return false;
                          
                        }
                     
                    },
                    error: function (xml, textStatus, errorThrown) {
                        //alert(xml.status + "||" + xml.responseText);
                    }
                });
            } else {
                alert("Please choose course first");
            }
        }
    }

    function DeleteStreamPreference(streamId) {
        if (confirm("Are you sure to delete this stream ")) {
            var steramgQuery = '{"streamPreferId":"' + streamId + '"}'
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/DeleteStreamPrefernce",
                data: steramgQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    if (response.d > 0) {
                        GetCourseStreamPreferedByStudent();
                    }
                },
                error: function(xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
    }

    function GetCourseStreamPreferedByStudent() {
        
        $("#tab3 .spnSuccess").html("");
        $("#courseStreamPreference").html("");
        $("#progress").show();
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetCourseStreamPreferedByStudent",
            data: "{}",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {


                FillCourseStreamPreferedData(response);

            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillCourseStreamPreferedData(data) {
        var columnData = "<table class='grdView' style='width:100%'><tr style='background:#eff2f7'><td>S.No</td><td>Stream Name</td><td>Core Company</td><td>Action</td></tr>";
        var finalData = ""; ;
        if (data.d.length == 3) { $("#divStreamInsert").removeClass("show"); $("#divStreamInsert").addClass("hide"); } else { $("#divStreamInsert").addClass("show"); }
        if (data.d.length) {
            for (var i = 0; i < data.d.length; i++) {
               
                var coreCompany = (data.d[i].CourseStreamCoreCompanies != "" || data.d[i].CourseStreamCoreCompanies!=null) ?data.d[i].CourseStreamCoreCompanies:"N/A" ;
                
                finalData += "<tr><td>" + (i + 1) + "</td><td>" + data.d[i].CourseStreamName + "</td><td>" + coreCompany + "</td><td><a href='#' onclick='DeleteStreamPreference(" + data.d[i].StudentStreamPreferenceId + ");return false;'>Delete</a></tr>";

            }
            $("#courseStreamPreference").html("");
            finalData = columnData + finalData + "</table>";
            $("#progress").hide();
            $("#courseStreamPreference").append(finalData);
        }
        else {
            $("#progress").hide();
          
        }
    }

    function FillCoursePreferedData(data) {
        var url = "/WebServices/CommonWebServices.asmx/GetCollegeByCourseSearch";

        var dataQuery = '{"courseId":"' + data.d[0].CourseId + '" }';
        $("#hdnFinalCourse").val(data.d[0].CourseId);
        $("#txtCollegeInsert").val(""); $(".ac_results").html("");
        BindAutoCompleteForData($("#txtCollegeInsert"), url, dataQuery);

        var streamdataQuery = '{"courseId":"' + data.d[0].CourseId + '" }';
        var streamList = "/WebServices/CommonWebServices.asmx/GetStreamListByCourseId";
        $("#slctStreamPrefernceInsert").empty();
        BindDropDownForData($("#slctStreamPrefernceInsert"), streamList, streamdataQuery);
        $("#slctlabel").html(data.d[0].CourseName); $("#slctlabelStream").html(data.d[0].CourseName);
        
        var columnData = "<table class='grdView' style='width:100%'><tr style='background:#eff2f7'><td>S.No</td><td>Course Name<td>Action</td></tr>";
        var finalData = ""; ;
        if (data.d.length>0) {
          
                finalData = "<tr><td>" + 1 + "</td><td>" + data.d[0].CourseName + "</td><td><a href='#' id='sndCourseUpdate' onclick='UpdateCoursePreference(" + data.d[0].CourseId + ");return false;'>Edit</a></td></tr>";

       
            $("#coursePreference").html("");
            finalData = columnData + finalData + "</table>";
            $("#progress").hide();
            $("#coursePreference").append(finalData);
        }
        else {
            $("#progress").hide();
           
            }
    }
    function CityPrefernceInsert() {
        $("#tab3 .spnSuccess").html("");

        if ($("#slctCityPrefernceInsert").val() <= 0) {
            alert("Please select city to insert");return false;
        } else {
            $("#progress").show();
            var cityQuery = '{"cityId":"' + $("#slctCityPrefernceInsert option:selected").text() + '"}';

            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/StudentCityPrefrenceInsert",
                data: cityQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#progress").hide();
                    if (response.d > 0) {
                        $("#slctCityPrefernceInsert").val(0);
                        GetCityPreferenceByStudent();
                    } else {
                    
                        alert("You have already preferred " + $("#slctCityPrefernceInsert option:selected").text() + " city"); $("#slctCityPrefernceInsert").val(0);
                    }

                    $("#progress").hide();
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        } 
    }

    function deleteCityPreference(cityPrefernceId) {
        if (confirm("Are you sure to delete this city  ")) {

            var cityQuery = '{"cityPrefernceId":"' + cityPrefernceId + '"}'
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/DeleteCityPrefernce",
                data: cityQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    if (response.d > 0) {
                        GetCityPreferenceByStudent();
                    }
                },
                error: function(xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
    }

    function GetCityPreferenceByStudent() {
        $("#tab3 .spnSuccess").html("");
        $("#cityPreference").html("");
        $("#progress").show();
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetCityPreferenceByStudent",
            data: "{}",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {


                FillCityPreferedData(response);

            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillCityPreferedData(data) {
        var columnData = "<table class='grdView' style='width:100%'><tr style='background:#eff2f7'><td>S.No</td><td>Country</td><td> State</td><td>City</td><td>Action</td></tr>";
        var finalData = "";
        if (data.d.length == 3) { $("#divCityInsert").removeClass("show"); $("#divCityInsert").addClass("hide"); } else { $("#divCityInsert").addClass("show"); }
        if (data.d.length) {
            for (var i = 0; i < data.d.length; i++) {
                finalData += "<tr><td>" + (i + 1) + "</td><td>" + data.d[i].CountryName + "</td><td>" + data.d[i].StateName + "</td><td>" + data.d[i].CityName + "</td><td><a href='#' onclick='deleteCityPreference(" + data.d[i].CityPreferId + ")'>Delete</a></td></tr>";

            }
            $("#cityPreference").html("");
            finalData = columnData + finalData + "</table>";
            $("#progress").hide();
            $("#cityPreference").append(finalData);
        }
        else {
            $("#progress").hide();
            $("#cityPreference").addClass('<label id="noMsg" class="info" >Sorry no city preffered by you found</label>');
           
        }
    }
    function GetStudentQuery(queryPageIndex) {
        $("#tab3 .spnSuccess").html("");
        $("#studentQuery").html("");
       
        $("#progress").show();
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetStudentQuery",
            data: '{"pageNumber":"' + (queryPageIndex - 1) + '","pageSize":" ' + queryPageSize + '"}',
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var privatetotal = response.d.TotalRecords;
                $("#QuerPager").html("");
                if (privatetotal > 0) {
                    $(".QuerPager").show();

                    FillStudentQueryData(response, privatetotal, queryPageIndex);
                } else {
                    $("#studentQuery").append("<br/><label id='noQuery' class='info'>Sorry, you don't have any query</label>");
                    
                  }
            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillStudentQueryData(data, privatetotal, queryPageIndex) {
        var columnData = "<div>";
        var finalData = ""; 
       
        if (data.d.MessageList.length) {
            for (var i = 0; i < data.d.MessageList.length; i++) {
            if(data.d.MessageList[i].QueryAnswer==null || data.d.MessageList[i].QueryAnswer==""){
            var dataAnswer="Not Answered";
            }
            else{dataAnswer=data.d.MessageList[i].QueryAnswer;
            }
            finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Question" + ((queryPageIndex-1)*10 + i + 1) + ":</strong><i style='color:#006a93;'>" + data.d.MessageList[i].Query + "</i></li><li><strong style='margin-right:10px;font-size:11px;'>Answer:</strong>" + dataAnswer + "</li></ol>";

            }
            $("#studentQuery").html("");
            finalData = columnData + finalData + "</div>";
            $("#progress").hide();
            $("#studentQuery").append(finalData);
        }
        
        $(".QuerPager").AdmissionJankari_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: queryPageIndex,
            PageSize: queryPageSize,
            RecordCount: privatetotal
        });
    }
    $(".QuerPager .page").live("click", function () {
        pageIndex = parseInt($(this).attr('page'));
        GetStudentQuery(pageIndex)
       

    });
    $(".tab_content2").hide();
    $(".tab_content2:first").show();

    $("#ulQuery li").click(function () {

        $("#ulQuery li").removeClass("active");
        $(this).addClass("active");
        $(".tab_content2").hide();
        var activeTab = $(this).attr("rel");
        $("#" + activeTab).fadeIn();
    });

    function GetAnsweredQuery(queryPageIndex) {
     
        $("#tab3 .spnSuccess").html("");
        $("#answeredQuery").html("");
        $("#progress").show();

        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetAnsweredQuery",
            data: '{"pageNumber":"' + (queryPageIndex - 1) + '","pageSize":" ' + queryPageSize + '"}',
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var Privatetotal = msg.d.TotalRecords;

                $("#AnsweredPager").html("");
                if (Privatetotal > 0) {
                    $(".AnsweredPager").show();


                    FillAnsweredQueryData(msg, Privatetotal, queryPageIndex);
                } else {
                    $("#answeredQuery").append("<label id='noQuery' class='info'>Sorry, you don't have any query</label>");

                }

            },

            error: function (xml, textStatus, errorThrown) {

                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillAnsweredQueryData(data, Privatetotal, queryPageIndex) {
      
        var columnData = "<div>";
        var finalData = "";

        if (data.d.MessageList.length) {

            for (var i = 0; i < data.d.MessageList.length; i++) {
                if (data.d.MessageList[i].QueryAnswer == null || data.d.MessageList[i].QueryAnswer == "") {
                    var dataAnswer = "Not Answered";
                }
                else {
                    dataAnswer = data.d.MessageList[i].QueryAnswer;
                }
                finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Question" + ((queryPageIndex - 1) * 10 + i + 1) + ":</strong><i style='color:#006a93;'>" + data.d.MessageList[i].Query + "</i></li><li><strong style='margin-right:10px;font-size:11px;'>Answer:</strong>" + dataAnswer + "</li></ol>";


            }

            $("#answeredQuery").html("");
            finalData = columnData + finalData + "</div>";
            $("#progress").hide();
            $("#answeredQuery").append(finalData);

        }
      
        $(".AnsweredPager").AdmissionJankari_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: queryPageIndex,
            PageSize: queryPageSize,
            RecordCount: Privatetotal
        });
    

    }

    $(".AnsweredPager .page").live("click", function () {
        pageIndex = parseInt($(this).attr('page'));

        GetAnsweredQuery(pageIndex);
    });
    function GetUnAnsweredQuery(queryPageIndex) {
        $("#unAnsweredPager").html("");
        $("#tab3 .spnSuccess").html("");
        $("#progress").show();
        
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetUnAnsweredQuery",
         
            data: '{"pageNumber":"' + (queryPageIndex - 1) + '","pageSize":" ' + queryPageSize + '"}',
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var Privatetotal = msg.d.TotalRecords;

                $("#unAnsweredPager").html("");
                if (Privatetotal > 0) {
                    $(".unAnsweredPager").show();


                    FillUnAnsweredQueryData(msg, Privatetotal, queryPageIndex);
                } else {
                    $("#unAnsweredQuery").append("<label id='noQuery' class='info'>Sorry, you don't have any query</label>");
                    
                }

            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillUnAnsweredQueryData(data, Privatetotal, queryPageIndex) {
        var columnData = "<div>";
        var finalData = "";
      
        if (data.d.MessageList.length) {
            for (var i = 0; i < data.d.MessageList.length; i++) {
                if(data.d.MessageList[i].QueryAnswer==null || data.d.MessageList[i].QueryAnswer==""){
                    var dataAnswer="Not Answered";
                }
                else {
                    dataAnswer=data.d.MessageList[i].QueryAnswer;
                }
               
                finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Question " + ((queryPageIndex-1)*10 + i + 1) + ":</strong><i style='color:#006a93;'>" + data.d.MessageList[i].Query + "</i></li><li ><strong style='margin-right:10px;font-size:11px;'>Answer:</strong>" + dataAnswer + "</li></ol>";
               
            }
            $("#unAnsweredQuery").html("");
            finalData = columnData + finalData + "</div>";
            $("#progress").hide();
            $("#unAnsweredQuery").append(finalData);
        }
       
        $(".unAnsweredPager").AdmissionJankari_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: queryPageIndex,
            PageSize: queryPageSize,
            RecordCount: Privatetotal
        });

    }
    $(".unAnsweredPager .page").live("click", function () {
        pageIndex = parseInt($(this).attr('page'));
      
        GetUnAnsweredQuery(pageIndex);


    });



    

    function GetLastQuery() {
        $("#tab3 .spnSuccess").html("");
        $("#lastQuery").html("");
        $("#progress").show();
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetLastQuery",
            data: "{}",
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {


                FillLastQueryData(response);

            },
            error: function (xml, textStatus, errorThrown) {
                //alert(xml.status + "||" + xml.responseText);
            }
        });
    }
    function FillLastQueryData(data) {
        $("#lastQuery").html("");
        var columnData = "<div>";
        var finalData = "";
        if (data.d.length > 0) {
            for (var i = 0; i < data.d.length; i++) {
                if (data.d[i].QueryAnswer == null || data.d[i].QueryAnswer == "") {
                    var dataAnswer = "Not Answered";
                }
                else {
                    dataAnswer = data.d[i].QueryAnswer;
                }
                finalData += "<ol style='border-bottom:2px dotted #f1f1f1; margin-bottom:5px; margin-top:10px;'><li style='color:#1b3251; font-weight:bold; font-size:11px;'><strong style='margin-right:10px;'>Question"+(i+1)+":</strong><i style='color:#006a93;'>" + data.d[i].Query + "</i></li><li><strong style='margin-right:10px;font-size:11px;'>Answer:</strong>" + dataAnswer + "</li></ol>";
            }
            $("#lastQuery").html("");
            finalData = columnData + finalData + "</div>";
            $("#progress").hide();
            $("#lastQuery").append(finalData);
        }
        else {
            $("#lastQuery").append("<label id='noQuery' class='info'>Sorry, you don't have any query</label>");
        }
    }

    function ChangePassword() {
        $("#tab3 .spnSuccess").html("");
        if (validPwd()) {
            var dataQuery = '{"emailId":"' + $("#txtEmailIdPwd").val() + '","newPwd":"' + $("#txtNewPassword").val() + '"}';
            $("#progress").show();
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/ChangePassword",
                data: dataQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $(".spnSuccess").addClass("info");
                        $(".spnSuccess").html("");
                    $(".spnSuccess").html(response.d);
                    $(".spnSuccess").fadeOut(30000);
                    $("#progress").hide();
                    ClearPassword();


                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
    }
    function ClearPassword() {
        $("#txtOldPassword").val("");
        $("#txtNewPassword").val("");
        $("#txtConfirmPassword").val("");
    }
    function validPwd() {
        $(".error").hide();
        var hasError = false;
        var passwordVal = $("#txtNewPassword").val();
        var checkVal = $("#txtConfirmPassword").val();
      if (passwordVal == '') {
            $("#txtNewPassword").after('<span class="error">Please enter new password.</span>');
            hasError = true;
        } else if (checkVal == '') {
            $("#txtConfirmPassword").after('<span class="error">Please re-enter your password.</span>');
            hasError = true;
        } 
        else if (passwordVal != checkVal) {
            $("#txtConfirmPassword").after('<span class="error">Passwords do not match.</span>');
            hasError = true;
        }
        if (hasError == true) { return false; } else { return true; }
    }

    function UpdateCoursePreference(courseId, divId, width, lnkId) {
        $("#slctChooseCoursePrefer").empty();
        BindFrontCourseList($("#slctChooseCoursePrefer"), courseId);
        OpenPoup('divCoursePrefereInsert',550,'sndCourseUpdate');return false;
    }
    function UpdateCoursePrefer() {
        if ($("#slctChooseCoursePrefer").val() <= 0) {
            alert("Please select course");return false;
        } else {
            $("#slctlabel").html($("#slctChooseCoursePrefer option:selected").text());
            $("#slctlabelStream").html($("#slctChooseCoursePrefer option:selected").text());
            var courseQuery = '{"courseId":"' + $("#slctChooseCoursePrefer").val() + '"}';

            UpdateCourse(courseQuery);
     
            var url = "/WebServices/CommonWebServices.asmx/GetCollegeByCourseSearch";
            
            var dataQuery = '{"courseId":"' + $("#slctChooseCoursePrefer").val() + '" }';
            $(".ac_results").html("");
            
            BindAutoCompleteForData($("#txtCollegeInsert"), url, dataQuery);

            var streamdataQuery = '{"courseId":"' + $("#slctChooseCoursePrefer").val() + '" }';
            var streamList = "/WebServices/CommonWebServices.asmx/GetStreamListByCourseId";
            $("#slctStreamPrefernceInsert").empty();
            BindDropDownForData($("#slctStreamPrefernceInsert"), streamList, streamdataQuery);
            $("#hdnFinalCourse").val($("#slctChooseCoursePrefer").val());
        }
    }

    function UpdateCourse(courseQuery) {
        if (confirm("Are you sure to update course " + $("#slctChooseCoursePrefer option:selected").text())) {
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/UpdateCourseByUser",
                data: courseQuery,
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    if (response.d > 0) {
                        $("#divCoursePrefereInsert").hide();
                        $("#fade").hide();
                        GetLoginDetails();
                        window.GetAccedemicInfo();
                        $("#divChooseCourse").hide();


                    }
                },
                error: function(xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        } else {
            $("#divCoursePrefereInsert").hide();
            $("#fade").hide();
            $("#divChooseCourse").hide();
        }
    }


    function clearpwdfield() {
        $("span.error").text(''); $("#txtOldPassword").val('');
        $("#txtNewPassword").val('');
        $("#txtConfirmPassword").val('');

    }

    function CheckPassWord() {
        $(".error").hide();
 
        var passwordVal = $("#txtNewPassword").val();
        var checkVal = $("#txtConfirmPassword").val();
        if (passwordVal != checkVal) {
            $("#txtConfirmPassword").after('<span class="error">Passwords do not match.</span>');
          
        } 

    }