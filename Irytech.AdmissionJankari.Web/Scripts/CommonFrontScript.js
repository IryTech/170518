
       
var courseList = [];
function BindFrontCourseList() {
    objControl = arguments[0];
    var defaultValue = (arguments[1]) ? arguments[1] : 0
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/BindFrontCourse",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        processData: false,
        dataType: "json",
        success: function (response) {

            BindCourseObject(response.d, objControl, defaultValue);

            
        },
        error: function (xml, textStatus, errorThrown) {
            ////alert(xml.status + "||" + xml.responseText);
        }
    });
}


function BindCourseObject(data, control, selectedValue) {
    PopulateControl(data, control);
    control.val(selectedValue);
    $("#lblShowCollege").text($("#ddlCourse option:selected").text());
}


function BindCourseListHavingStream(objControl) {
    objControl = arguments[0];
    var defaultValue = (arguments[1]) ? arguments[1] : 0
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCourseListHavingStream",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            BindCourseObject(response.d, objControl, defaultValue);
          
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}

function BindFrontExamListByCourse(objControl,courseId) {

    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetExamListByCourseId",
        data: '{courseId:"' + courseId + '"}',
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            PopulateControl(response.d, objControl);

        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}

function BindStateList(objControl) {

    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetAllState",
        data: '{}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            PopulateControl(response.d, objControl);

        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}


function BindFrontCity(objControl) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetAllCityWithoutId",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            
            PopulateControl(response.d, objControl);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });

}
function BindCityByState(objControl,stateId) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCity",
        data: '{stateId:"' + stateId + '"}',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            
            PopulateControl(response.d, objControl);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });

}


function BindFrontUserType(objControl) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetUserType",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            PopulateControl(response.d, objControl);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}


function BindBankList(objControl) {

    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/BankList",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            PopulateControl(response.d, objControl);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}

function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}
//Code Starts
function validatePhone(sMobileNo) {

    
    var filter = /^[0-9-+]+$/;
    if (filter.test(sMobileNo)) {
       
        if (sMobileNo.length < 10 || sMobileNo.length > 11) {

            return false;
        }
        else {

            return true;
        }
        
    }
    else {
        return false;
    }
}

// validate the date in the form of the dd/mm/YYYY

function isValidDate(sDate) {
   
    var filter = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/.exes(sDate);
    if (!filter) return false;
    else return true;
}

// validate the numeric value
function isNumeric(sValue) {
    var filter = /^[0-9-+]+$/;
    if (filter.test(sValue)) {
        return true;
    } else {
        return false;
    }
}

// Validate the pin code for india

function isValidPinCode(sPincode) {

    var filter = /^\d{6}$/;
    
    if (filter.test(sPincode)) {
        
            return true;
            
    } else {
        return false;
    }
}

function OpenPopus(url) {


    var popID = $(url).attr('rel'); //Get Popup Name
    
    var popURL = $(url).attr('href'); //Get Popup href to define size

    var query = popURL.split('?');
    var dim = query[1].split('&');
    var popWidth = dim[0].split('=')[1]; //Gets the first query string value

   
    $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>');
    var popMargTop = ($('#' + popID).height() + 80) / 2;
    var popMargLeft = ($('#' + popID).width() + 80) / 2;
    $('#' + popID).css({

        'margin-top': -popMargTop,

        'margin-left': -popMargLeft

    });
    $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.

    $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer



    $('a.close, #fade').click(function () { //When clicking on the close or fade layer...
      $('#fade , .popup_block').fadeOut(function () {

            $('#fade, a.close').remove();
        }); //fade them both out
        $("#" + popID + ' ' + 'input[type=text]').val('');
        $("#" + popID + ' ' + 'textarea').val('');
        $("#" + popID + ' ' + 'select').val('');
        $("#" + popID + ' ' + 'label.error').addClass("hide");
        $("#" + popID + ' ' + 'label.success').addClass("hide");
        return false;
    });
}

function OpenPoup(Divid, Width, linkId) {
    var urls;
    urls = document.getElementById(linkId);
    urls.href = "#?w=" + Width;
    urls.rel = Divid
    OpenPopus(urls);
}
function BindDropDownCommon(objControl, url) {
    $.ajax({
        type: "POST",
        url: url,
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
           data = msg.d.split(",");
           objControl.autocomplete(data);
          
        },
        error: function (xml, textStatus, errorThrown) {
            alert(xml.status + "||" + xml.responseText);
        }
    });
}
function BindAutoCompleteForData(objControl, url, dataQuery) {
 
    $.ajax({
        type: "POST",
        url: url,
        data: dataQuery,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");
            objControl.autocomplete(data);
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}


      
//Search Course by Stream
function showCollegeDetailsWiseCollegeNameAndCourseId(Control1, Control2, Control3, Control4, value) {
    
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCollegeByCourseSearch",
        async: true,
        data: '{courseId:"' + value + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

        if(msg.d.length>0)
        {
            data = msg.d.split(",");

            Control1.autocomplete(data);
            Control2.autocomplete(data);
            Control3.autocomplete(data);
            Control4.autocomplete(data);
            }
            else{
                 Control1.autocomplete('');
            Control2.autocomplete('');
            Control3.autocomplete('');
            Control4.autocomplete('');
            }

        }
    });
}
function AdmissionJankari(a, b) {
    var c = '<a style = "cursor:pointer" class="page" page = "{1}">{0}</a>';
    var d = "<span>{0}</span>"; var e, f, g; var g = 5;
    var h = Math.ceil(b.RecordCount / b.PageSize); if (b.PageIndex > h) { b.PageIndex = h }
    var i = ""; if (h > 1) {
        f = h > g ? g : h; e = b.PageIndex > 1 && b.PageIndex + g - 1 < g ? b.PageIndex : 1;
        if (b.PageIndex > g % 2) {
            if (b.PageIndex == 2) f = 5;
            else f = b.PageIndex + 2
        }
        else { f = g - b.PageIndex + 1 }
        if (f - (g - 1) > e) { e = f - (g - 1) }
        if (f > h) { f = h; e = f - g + 1 > 0 ? f - g + 1 : 1 }
        var j = (b.PageIndex - 1) * b.PageSize + 1;
        var k = j + b.PageSize - 1;

        if (b.PageIndex > 1) { i += c.replace("{0}", "&laquo;&laquo;").replace("{1}", "1"); i += c.replace("{0}", "&laquo;").replace("{1}", b.PageIndex - 1) }
        for (var l = e; l <= f; l++) {
            if (l == b.PageIndex) { i += d.replace("{0}", l) }
            else { i += c.replace("{0}", l).replace("{1}", l) }
        }
        if (b.PageIndex < h) { i += c.replace("{0}", "&raquo;").replace("{1}", b.PageIndex + 1); i += c.replace("{0}", "&raquo;&raquo;").replace("{1}", h) }
    } a.html(i);
    try { a[0].disabled = false }
    catch (m) { }
} (function (a) {
    a.fn.AdmissionJankari_Pager = function (b) {
        var c = {}; var b = a.extend(c, b);
        return this.each(function () { AdmissionJankari(a(this), b) })
    }
})(jQuery);


//Search Course by Stream
function showCourseWiseStreamDetails(Control1, Control2, Control3, Control4, value) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetStreamListByCourse",
        async: true,
        data: '{courseId:"' + value + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            data = msg.d.split(",");

            Control1.autocomplete(data);
            Control2.autocomplete(data);
            Control3.autocomplete(data);
            Control4.autocomplete(data);

        }
    });
}

function BindStreamByCourse(Control, value) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetStreamListByCourseId",
        async: false,
        data: '{courseId:"' + value + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
       
             PopulateControl(msg.d, Control);
        }
    });
}


//Search CollegeSpeechName by Stream
function showCollegeTopHirerName(Control1, value) {
        $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCollegeByCourseSearch",
        async: true,
        data: '{courseId:"' + value + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");
          Control1.autocomplete(data);


        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}

//Search CollegeSpeechName by Stream
function showCollegeTopHirereName(Control1) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetCollegeDetails",
        async: true,
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");
            Control1.autocomplete(data);


        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}

//Search CollegeSpeechName by Stream
function showMobileDetailsOfTestimonial(Control1) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetMobileNoList",
        async: true,
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");
            Control1.autocomplete(data);


        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}



function BindDropDown(objControl, url) {
    $.ajax({
        type: "POST",
        url: url,
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            BindCommonPopulateList(response.d, objControl);

        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}
function BindDropDownForData(objControl,url, data) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            BindCommonPopulateList(response.d, objControl);

        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}
function BindCommonPopulateList(data, objControl) {

    PopulateControl(data, objControl);
}
function PopulateControl(list, control) {

    if (list != null) {

        if (list.length > 0) {

            control.removeAttr("disabled");
             control.empty().append('<option selected="selected" value="0">Select</option>');
            $.each(list, function () {

                   
                control.append($("<option></option>").val(this['Value']).html(this['Text']));

            });
        }
        else {
            control.empty().append('<option selected="selected" value="0">Select</option>');
        }
    }
}


function ChangeCourseId(courseId) {
 
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/UpdateCourseId",
        data: '{"courseId":"' + courseId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (response) {
            $("#ddlCourse").val(response.d);
        }, error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
}
function RemoveIlegalCharecter(str) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/RemoveIlLegalCahrachter",
        data: '{"str":"' + str + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: true,
        success: function (response) {
            str = response.d;
        }, error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
    return str;
}
function FillBankName(control) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/GetBankName",
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

    function InsertCollegePrefer(courseId,collegeName ) {
        var collegQuery = '{"courseId":"' + courseId + '","collegeName":" ' + collegeName + '"}';

    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/StudentCollegePrefrenceInsertById",
        data: collegQuery,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
        },
        error: function (xml, textStatus, errorThrown) {
          //  alert(xml.status + "||" + xml.responseText);
        }
    });
}

    function InsertCityPrefer(cityId) {
        var cityQuery = '{"cityId":"' + cityId + '"}';

        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/StudentCityPrefrenceInsertById",
            data: cityQuery,
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {

            },
            error: function(xml, textStatus, errorThrown) {
                // a//lert(xml.status + "||" + xml.responseText);
            }
        });
    }

    function InsertInterestedCollegePrefer(collegeBranchCourseId) {
    var colegeQuery = '{"collegeCourseId":"' + $("#hdnCollegeCourseId").val() + '"}';

    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/InsertStudentCollegeInterestedById",
        data: colegeQuery,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

           return response.d
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
            return "Something went wrong please try again"
        }
    });
}
function InsertUpdateUserTransctionalDetails(courseId) {
    var colegeQuery = '{"courseId":"' + courseId + '"}';

    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/InsertUpdateUserTransctionalDetails",
        data: colegeQuery,
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

function CheckSession(examName) {
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/CheckSession",
                data: "{}",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
           
                    if (response.d == false) {
                        OpenPoup('divRegisteration', '650', 'sndExamRegister');
                    }else {
                        ShowMessage(examName);
                    }
                },
                error: function(response) {
                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("error");
                    $("#lblerrMsg").html(response.d);
                },
            });
        }
          function ShowMessage(examName) {
            alert("We will revert you soon with result for "+ examName);
        }
function CurrentMenuSelect(current) {
        alert(current);
         $(current).addClass("select");
     }
     

   function RemoveIlegalCharecterFromCourse(str) {
    $.ajax({
        type: "POST",
        url: "/WebServices/CommonWebServices.asmx/RemoveIllegealFromCourse",
        data: '{"str":"' + str + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        cache: false,
        success: function (response) {
            str = response.d;
        }, error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });
    return str;
}

   function RemoveChahracterfromCorse(str) {str = str.split(" ").join("-");
       str = str.split('/').join("-"); 
       return str;
   }

   function show(course) {

       var courseLink = "ulBanner" + course;
       $("#divTopBannner ul").addClass("hide");
       $("#" + courseLink).removeClass('hide');

   }


   function GetUserLoginCategory()
   {
        var ucat=-1
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetLoginUserCategory",
            data:"{}",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                ucat=response.d;

            },
            error: function (xml, textStatus, errorThrown) {
               
            }
        });
        return ucat;
   }

    
    function GetUserLoginName()
    {
       var uLoginName='';
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetLoginUserName",
            data:"{}",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                
                uLoginName=response.d;

            },
            error: function (xml, textStatus, errorThrown) {
               
            }
        });
        return uLoginName;
    }
    function CheckUserLogin() {

    $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/CheckSession",
            data:"{}",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                  
                    if(response.d==true)
                    {
                         $("#divProfile").removeClass("hide1");
                        $("#loginRegisterPanel").hide();
                        $("#divLogout").removeClass("hide1");
                        $("#lblUserName").text(GetUserLoginName());
                        if (GetUserLoginCategory() == 6) 
                        {
                            $("#clgProfile").removeClass("hide");
                            $("#spnUserProfile").addClass("hide");
                        } 
                        else {
               
                            $("#spnUserProfile").removeClass("hide");
                             
                        }
                    }
                    else
                    {
                        $("#loginRegisterPanel").removeClass("hide1");
                    }
            },
            error: function (xml, textStatus, errorThrown) {
               
            }
        });


      
    }
    function Logout() {
        $("#loginRegisterPanel").show();
        $("#divProfile").hide(); $("#divLogout").hide();

        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/Logout",
            data:"{}",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                location.href = "/";
            },
            error: function (xml, textStatus, errorThrown) {
               
            }
        });

    }

    $(document).ready(function () {
    CheckUserLogin();
       
        var url = location.href;

        if (url.indexOf("city") > -1) {

            url = url.split('/');

            $("#" + url[url.length - 1]).addClass("select");
            $("#liCollSrch").addClass("selected1"); $("#sndCollSrch").addClass("selected1");
        } else {
            ChangeMenuSelection(url);
        }
    });
    function ChangeMenuSelection(url) {
         

        if (url.indexOf("compare-colleges") > -1 ) {
            $("#liCollCompare").addClass("selected1"); $("#sndCollCompare").addClass("selected1");
        }
        else if ((url.indexOf("course") > -1) || (url.indexOf("college-details") > -1) || (url.indexOf("collegesearch") > -1) || (url.indexOf("college/") > -1)) {
            $("#liCollSrch").addClass("selected1"); $("#sndCollSrch").addClass("selected1");

        }
        else if (url.indexOf("compare-streams") > -1 || (url.indexOf("stream-detail") > -1)) {
            $("#liCourseCompare").addClass("selected1"); $("#sndCourseCompare").addClass("selected1");
        }
        else if (url.indexOf("exams") > -1 || (url.indexOf("exam-details") > -1)) {
            $("#liExamLink").addClass("selected1"); $("#sndExamLink").addClass("selected1");
        }
        else if (url.indexOf("latest-news") > -1 || (url.indexOf("news-details") > -1)) {
            $("#linews").addClass("selected1"); $("#news").addClass("selected1");
        }
        else if (url.indexOf("admission-notices") > -1 || (url.indexOf("notice-details") > -1)) {
            $("#linotice").addClass("notice"); $("#notice").addClass("selected1");
        }
        else if (url.indexOf("education-loan") > -1 || (url.indexOf("loan-details") > -1)) {
            $("#liloan").addClass("selected1"); $("#loan").addClass("selected1");
        }
        else if (url.indexOf("ReportDonation") > -1) {
            $("#lidonation").addClass("selected1"); $("#donation").addClass("selected1");
        }
        else if (url.indexOf("blog") > -1) {
            $("#liblog").addClass("selected1"); $("#blog").addClass("selected1");
        }
        else if (url.indexOf("forum") > -1) {
            $("#liforum").addClass("selected1"); $("#forum").addClass("selected1");
        }
        else if ((url.indexOf("get-direct-admission") > -1) || (url.indexOf("counselling") > -1)) {
            
            $("#liDirectAdmission").addClass("selected1"); $("#sndDirectAdmission").addClass("selected1");
        }
        else if (url.indexOf("bookseat") > -1) {

            $("#libookseat").addClass("selected1"); 
        }
        else if (url.indexOf("login") > -1) {
            $("#sndLogin").addClass("selected1");
        }
        else if (url.indexOf("Registration") > -1) {
            $("#sndRegister").addClass("selected1");
        } else if (url.indexOf("account") > -1) {
            $("#accountLink").addClass("selected1");
        } 
        else {
             $("#lnkHome").addClass("selected1"); $("#sndHome").addClass("selected1");
        }

    }

    $(document).ready(function () {
    var speed = 5000;
    var run = setInterval('rotate()', speed);
    //grab the width and calculate left value
    var itemWidth = $('#slides ul li').outerWidth();
    var leftValue = itemWidth * (-1);
    //move the last item before first item, just in case user click prev button
    $('#slides li:first').before($('#slides li:last'));
    //set the default item to the correct position 
    $('#slides ul').css({ 'left': leftValue });

    //if user clicked on prev button
    $('#prev').click(function () {

        //get the right position            
        var leftIndent = parseInt($('#slides ul').css('left')) + itemWidth;

        //slide the item            
        $('#slides ul:not(:animated)').animate({ 'left': leftIndent }, 200, function () {

            //move the last item and put it as first item            	
            $('#slides li:first').before($('#slides li:last'));

            //set the default item to correct position
            $('#slides ul').css({ 'left': leftValue });

        });

        //cancel the link behavior            
        return false;

    });

    //if user clicked on next button
    $('#next').click(function () {

        //get the right position
        var leftIndent = parseInt($('#slides ul').css('left')) - itemWidth;

        //slide the item
        $('#slides ul:not(:animated)').animate({ 'left': leftIndent }, 260, function () {

            //move the first item and put it as last item

            if ($("#slides li").size() > 1) {

                $('#slides li:last').after($('#slides li:first'));
            }
            //set the default item to correct position
            // $('#slides ul').css({ 'left': leftValue });

        });

        //cancel the link behavior
        return false;

    });

    //if mouse hover, pause the auto rotation, otherwise rotate it
    $('#slides').hover(

		function () {
		    clearInterval(run);
		},
		function () {
		    run = setInterval('rotate()', speed);
		}
	);

});

//a simple function to click next link
//a timer will call this function, and the rotation will begin :)  
function rotate() {

    $('#next').click();
}
 function show(course) {
        $(".href").attr('href', ("/"+course+"/Get-Direct-Admission").toLowerCase());
    }
    function BindBanner(course,courseId) {
          $("[id*=ulBannerList]").html("");
        var query='{"courseId":"' + courseId + '","positionId":"1"}';
     
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/WebServices/CommonWebServices.asmx/GetBannerList",
            data: query,
            async: true,
            cache:true,
            success: function(response) {
                var xmlDoc =$.parseXML(response.d);
                var xml =$(xmlDoc);
                var rowIndex = 0;
                var collegeList =xml.find("Table");
                var ulList="<ul id='ulBannerList'>";
                var liLIst="";
                $.each(collegeList, function(i) {
                    var courseName = RemoveChahracterfromCorse($(this).find("AjCourseName").text().trim());
                    rowIndex = ++i;
                    var collegeName=$(this).find("AjCollegeBranchName").text().trim();
                    var imageText=$(this).find("AjBannerImage").text().trim();
                     var collegeUrl=$(this).find("CollegeUrl").text().trim();
                   
                    var tooltip = $(this).find("AjBannerToolTip").text().trim()!=""?$(this).find("AjBannerToolTip").text().trim():collegeName;
                    var imageDataTop = "<img id='collegeImage' height='90px' width='260px' title='"+tooltip+"' alt="+collegeName+" src='/image.axd?Banner="+imageText+"'/>";
                    var courseId=$(this).find("AjCourseId").text().trim();
                    var lidId="ulBanner"+courseId;
                    if ($(this).find("AjBannerUrl").text().trim()!=""&& $(this).find("AjBannerUrl").text().trim()!=null) {
                        liLIst+="<li id='"+lidId+"'><a id='sndDirect1' target='_blank' href='"+collegeUrl+"' rel='canonical' title='"+tooltip+"'>"+imageDataTop+"</a></li>";
                    } else {
                         liLIst+="<li id='"+lidId+"'><a class='href' id='sndDirect1' target='_blank' href='"+collegeUrl+"' rel='canonical' title='"+tooltip+"'>"+imageDataTop+"</a></li>";
                    }
                });
                if (liLIst.length != 0) {
                 var ulList1=ulList+liLIst+"</ul>";
                    $("[id*=slides]").append(ulList1);
                }
              //  show(course);
            },
        });
        
    }
    

    //common method to post json data......

    AjaxPostCallBack = function(url, dataQuery, sucessCallBack,id) {
        $.ajax({
            type: "POST",
            url: url,
            data: dataQuery,
            async: true,
            contentType: "application/json; charset=utf-8",
            processData: false,
            dataType: "json",
            success: function(response) {
              
                if (response.d=="401") {
                    location.href = "/account/college-login";
                } else {
                    sucessCallBack(response,id);
                }
            },
            error: function(xml, textStatus, errorThrown) {
              
                alert(xml.status + "||" + xml.responseText);
            }
        });
    };

 