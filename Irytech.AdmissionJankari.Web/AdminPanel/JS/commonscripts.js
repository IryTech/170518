
function BindDropDownCommonForAdminAutoCompletebySponserCourseStateCity(objControl, filter, courseid, stateid, cityid, url) {
    $.ajax({
        type: "POST",
        url: url,
        data: "{'filter':'" + filter + "', 'courseid':'" + courseid + "', 'stateid':'" + stateid + "', 'cityid':'" + cityid  + "'}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");

            objControl.autocomplete(data);

        },
        error: function (xml, textStatus, errorThrown) {
            //  alert(xml.status + "||" + xml.responseText);
        }
    });
}

function BindDropDownCommonForAdminAutoComplete(objControl, url) {
 
    $.ajax({
        type: "POST",
        url: url,
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");

            objControl.unautocomplete(); 
            objControl.autocomplete(data);

        },
        error: function (xml, textStatus, errorThrown) {
            alert(xml.status + "||" + xml.responseText);
        }
    });
}

function BindDropDownCommonForAdminAutoCompletebyCourseID(objControl, course, url) {
   
    $.ajax({
        type: "POST",
        url: url,
        data: "{'courseid':'" + course + "'}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            objControl.unautocomplete()
            data = msg.d.split(",");

            objControl.autocomplete(data);

        },
        error: function (xml, textStatus, errorThrown) {
            //  alert(xml.status + "||" + xml.responseText);
        }
    });
}


function BindDropDownCommonForAdminAutoCompletebyCourseIDParticipated(objControl, filter, course, url) {

    $.ajax({
        type: "POST",
        url: url,
        data: "{'filter':'"+filter+"' , 'courseid':'" + course + "'}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");

            objControl.autocomplete(data);

        },
        error: function (xml, textStatus, errorThrown) {
            //  alert(xml.status + "||" + xml.responseText);
        }
    });
}

function OpenModalPopUp(url) {


    var popId = $(url).attr('rel'); //Get Popup Name

    var popUrl = $(url).attr('href'); //Get Popup href to define size

    var query = popUrl.split('?');
    var dim = query[1].split('&');
    var popWidth = dim[0].split('=')[1]; //Gets the first query string value
    $('#' + popId).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>');
    var popMargTop = ($('#' + popId).height() + 50) / 2;
    var popMargLeft = ($('#' + popId).width() + 280) / 2;
//    $('#' + popId).css({

//        'margin-top': -popMargTop,

//        'margin-left': -popMargLeft

//    });
   
    $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.

    $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer



    $('.close, #fade').click(function () { //When clicking on the close or fade layer...
        $('#fade , .popup_block').fadeOut(function () {

            $('#fade, a.close').remove();
        }); //fade them both out
        $("#" + popId + ' ' + 'input[type=text]').val('');
        $("#" + popId + ' ' + 'textarea').val('');
        $("#" + popId + ' ' + 'select').val('');
        $("#" + popId + ' ' + 'label.error').addClass("hide");
        $("#" + popId + ' ' + 'label.success').addClass("hide");
        return false;
    });
}

function OpenPoup(divid, width, linkId) {
    var urls;
    urls = document.getElementById(linkId);
    urls.href = "#?w=" + width;
    urls.rel = divid
    OpenModalPopUp(urls);
}


function BindDropDownForData(objControl, url) {
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
           alert(xml.status + "||" + xml.responseText);
        }
    });
}
function BindCommonPopulateList(data, objControl) {

    PopulateControl(data, objControl);
}
function PopulateControl(list, control) {
    if (list != null) {

        if (list.length > 0) {

         
            control.empty().append('<option selected="selected" value="0">--Select--</option>');
            $.each(list, function () {

                control.append($("<option></option>").val(this['Value']).html(this['Text']));

            });
        }
        else {
            control.empty().append('<option selected="selected" value="0">Not available</option>');
        }
    }

}

function UniversityCategory(universityCategoryObject) {
    universityCategoryObject.empty().append('<option selected="selected" value="0">Select University</option>');
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/BindUniversityCategory",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            BindUniversityPopulateList(response.d, universityCategoryObject);
           
        },
        error: function (xml, textStatus, errorThrown) {
           // alert(xml.status + "||" + xml.responseText);
        }
    });
}
function CountryList(countryObject) {
    countryObject.empty().append('<option selected="selected" value="0">Select Country</option>');
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/GetCountryList",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            BindUniversityCountryPopulateList(response.d, countryObject);
           
        },
        error: function (xml, textStatus, errorThrown) {
           // alert(xml.status + "||" + xml.responseText);
        }
    });
}

function State(stateObj, countryObj) {
    var countryId = countryObj.val();
    alert(countryId);
    if (countryId > 0) {
        stateObj.empty();
        $("#ddlUniversityStateName").empty().append('<option selected="selected" value="0">Select State</option>');
        $.ajax({
            type: "POST",
            url: "../../WebServices/CommonWebServices.asmx/GetState",
            async: true,
            data: '{"countryId":"' + countryId + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                BindUniversityStatePopulateList(response.d, stateObj);
               
            },
            error: function (xml, textStatus, errorThrown) {
               // alert(xml.status + "||" + xml.responseText);
            }
        });
    }
}
function City(stateObj, cityObj) {

    var stateId = stateObj.val();
    if (stateId > 0) {
        cityObj.empty();
        $("#ddlUniversityCityName").empty().append('<option selected="selected" value="0">Select City</option>');
        $.ajax({
            type: "POST",
            url: "../../WebServices/CommonWebServices.asmx/GetCity",
            async: true,
            data: '{"stateId":"' + stateId + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                BindUniversityCityPopulateList(response.d, cityObj);
              
            },
            error: function (xml, textStatus, errorThrown) {
               // alert(xml.status + "||" + xml.responseText);
            }
        });
    }
}
function BindUniversityPopulateList(data, objControl) {

    PopulateControl(data, objControl);
   
}
function BindUniversityCountryPopulateList(data, objControl) {

    PopulateControl(data, objControl);
   
    
}

function BindUniversityStatePopulateList(data, objControl) {

    PopulateControl(data, objControl);
  
}

function BindUniversityCityPopulateList(data, objControl) {

    PopulateControl(data, objControl);

}

//for usermaster....
function OpenUserMasterPoup(divid, width, linkId, userId, emailId, countryId, courseId, userCategoryId, stateId, cityId, userName, pinCode, password, corespondenceAddr, permanenetAddr, mobileNo, phoneNo, userStatus,zender,dob) {
    var urls;
    urls = document.getElementById(linkId);
    urls.href = "#?w=" + width;
    urls.rel = divid
    OpenModalPopUp(urls);
    document.getElementById("ctl00_ContentPlaceHolderMain_hndUserId").value = userId;
    document.getElementById("ctl00_ContentPlaceHolderMain_hndCourseId").value = courseId;
    document.getElementById("ctl00_ContentPlaceHolderMain_hndCategoryId").value = userCategoryId;
    document.getElementById("ctl00_ContentPlaceHolderMain_hndStateId").value = stateId;
    document.getElementById("ctl00_ContentPlaceHolderMain_hndCityId").value = cityId;
    document.getElementById("ctl00_ContentPlaceHolderMain_hndEmailID").value = emailId;
    document.getElementById("ctl00_ContentPlaceHolderMain_hndCountryId").value = countryId;
    
    document.getElementById("ctl00_ContentPlaceHolderMain_txtPopupUserName").value = userName;
    document.getElementById("ctl00_ContentPlaceHolderMain_txtPopupUserPassword").value = password;
    document.getElementById("ctl00_ContentPlaceHolderMain_txtPopupUserMobileNo").value = mobileNo;
    document.getElementById("ctl00_ContentPlaceHolderMain_txtPopupUserPhoneNo").value = phoneNo;
    document.getElementById("ctl00_ContentPlaceHolderMain_txtPopupCorresPondence").value = corespondenceAddr;
    document.getElementById("ctl00_ContentPlaceHolderMain_txtPopupUserPermanentAddress").value = permanenetAddr;
    document.getElementById("ctl00_ContentPlaceHolderMain_txtPopupUserPinCode").value = pinCode;

    $('#ctl00_ContentPlaceHolderMain_rbtGender').find("input[value=" + zender + "]").attr("checked", "checked");
    document.getElementById("ctl00_ContentPlaceHolderMain_txtDOB").value = dob;
    $("#ctl00_ContentPlaceHolderMain_chkPopupUserStatus").attr('checked', userStatus);
    FillStateName(stateId);
    FillCountryName(countryId);
    FillCategoryName(userCategoryId);
    FillCourseName(courseId);
    FillCityName(cityId);


} 
//function to fill The User State fields
function FillStateName(stateId) {
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/GetAllState",
        async: false,
        data: '{"StateId":"' + stateId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnStatePopulated,
        error: function (xml, textStatus, errorThrown) {

        }

    });
}

function OnStatePopulated(response) {

    PopulateControl(response.d, $("#ctl00_ContentPlaceHolderMain_ddlPopupStateName"));


}


//function to fill The User Country fields
function FillCountryName(countryId) {
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/GetCountryList",
        async: false,
        data: '{"CountryId":"' + countryId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnCountryPopulated,
        error: function (xml, textStatus, errorThrown) {

        }

    });
}

function OnCountryPopulated(response) {

    PopulateControl(response.d, $("#ctl00_ContentPlaceHolderMain_ddlPopupCountryName"));

    $("#ctl00_ContentPlaceHolderMain_ddlPopupCountryName").val(document.getElementById("ctl00_ContentPlaceHolderMain_hndCountryId").value);


}

//function to fill The User Category list fields
function FillCategoryName(categoryId) {
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/GetAllUserCategory",
        async: false,
        data: '{"CategoryId":"' + categoryId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnCategoryPopulated,
        error: function (xml, textStatus, errorThrown) {

        }

    });
}

function OnCategoryPopulated(response) {

    PopulateControl(response.d, $("#ctl00_ContentPlaceHolderMain_ddlPopupUSerCategoryName"));
    $("#ctl00_ContentPlaceHolderMain_ddlPopupUSerCategoryName").val(document.getElementById("ctl00_ContentPlaceHolderMain_hndCategoryId").value);


}

//function to fill The User Category list fields
function FillCourseName(courseId) {
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/GetAllCourseList",
        async: false,
        data: '{"CategoryId":"' + courseId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnCoursePopulated,
        error: function (xml, textStatus, errorThrown) {

        }

    });
}

function OnCoursePopulated(response) {

    PopulateControl(response.d, $("#ctl00_ContentPlaceHolderMain_ddlPopupCourseName"));

    $("#ctl00_ContentPlaceHolderMain_ddlPopupCourseName").val(document.getElementById("ctl00_ContentPlaceHolderMain_hndCourseId").value);


}

//function to fill The User City list fields
function FillCityName(CityId) {
    $.ajax({
        type: "POST",
        url: "../../WebServices/CommonWebServices.asmx/GetAllCityWithoutId",
        async: false,
        data: '{"CityId":"' + CityId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnCityPopulated,
        error: function (xml, textStatus, errorThrown) {

        }

    });
}
function OnCityPopulated(response) {
    PopulateControl(response.d, $("#ctl00_ContentPlaceHolderMain_ddlPopupCityName"));
    $("#ctl00_ContentPlaceHolderMain_ddlPopupCityName").val(document.getElementById("ctl00_ContentPlaceHolderMain_hndCityId").value);
   
}

function CommonAutoComplete(objControl1, objControl2, url, datatQuery) {

    $.ajax({
        type: "POST",
        url: url,
        data: datatQuery,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = msg.d.split(",");
            objControl1.unautocomplete()
            objControl2.unautocomplete()
            objControl1.autocomplete(data); objControl2.autocomplete(data);
        },
        error: function (xml, textStatus, errorThrown) {
            alert(xml.status + "||" + xml.responseText);
        }
    });
  }
  function BindDropDown(objControl, url, methodRef, selectedValue) {
      $.ajax({
          type: "POST",
          url: url,
          data: {},
          async: true,
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
              methodRef(data.d, objControl, selectedValue);
          },
          error: function (xml, textStatus, errorThrown) {
              alert(xml.status + "||" + xml.responseText);
          }
      });
  }

  function CommonWebServicesCall(url,dataQuery,methodObject) {
      $.ajax({
          type: "POST",
          url: url,
          data: dataQuery,
          async: true,
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (msg) {
              methodObject(msg.d);

          },
          error: function (xml, textStatus, errorThrown) {
              alert(xml.status + "||" + xml.responseText);
          }
      });
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

              //Code Ends


          },
          error: function (xml, textStatus, errorThrown) {
              alert(xml.status + "||" + xml.responseText);
          }
      });
  }


  function OnAjaxError(obj, status, error) {
      //var err = eval('(' + obj.responseText + ')');
      alert(obj.responseText);
  }

  function CopyContent(contl1, contl2, contl3, contl4, contl5) {
      contl2.val($(contl1).val());
      contl3.val($(contl1).val());
      contl4.val($(contl1).val());
      contl5.val($(contl1).val());

  }