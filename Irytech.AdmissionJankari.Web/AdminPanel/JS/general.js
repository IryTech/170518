
/* Import Utility....................................*/
var PROGRESS;
/* The below function is using for query string...*/
function getQueryStringValue(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

/* Start: Function for bind dropdownlist..........*/
function bindAllTableNamesNPrimaryColumnToDRP(controlId, functionName, tblName) {
    $.ajax({
        type: "POST",
        url: functionName,
        data: '{"tblName":"' + tblName + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $(controlId).get(0).options.length = 0;

            $(controlId).get(0).options[0] = new Option("--Select a Table--", "0");


            $.each(msg.d, function (index, item) {

                $(controlId).get(0).options[$(controlId).get(0).options.length] = new Option(item.PropTABLENAME, item.PropPK_COLUMNNAME);
                //);
            });

        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });

}




/* Start: Function for bind dropdownlist..........*/
function bindColumnsToDRP(controlId, functionName, data, mappedField, mappedFieldType, mappedFieldSize) {
    //    alert(controlId);
    $.ajax({
        type: "POST",
        url: functionName,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $(controlId).get(0).options.length = 0;

            $(controlId).get(0).options[0] = new Option("--Select a field--", "0");


            $.each(msg.d, function (index, item) {

                $(controlId).get(0).options[$(controlId).get(0).options.length] = new Option(msg.d[index], msg.d[index] + '|' + mappedField + '|' + mappedFieldType + '|' + mappedFieldSize);
                //);
            });

        },
        error: function (xml, textStatus, errorThrown) {
            //  alert(xml.status + "||" + xml.responseText);
        }
    });

}







/* Start: Create Grid Mapping For Columns ..... */
function createMappingGrid(tableName,FileName) {
    $("#MappingGrid").empty();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/GetColumns",
        data: "{'TableName':'" + tableName + "','AutoIncrementedColumnName':'" + $("#drpTable").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {

            $("#MappingGrid").append("<div class='gridHeader rows'><span class='width3Percent'>Sr No.</span><span class='width20Percent'>File Column</span><span class='width25Percent'>Module Field</span><span class='width15Percent'>Data Type</span><span class='width8Percent center'>Is Mapped</span><span class='width15Percent'>CSV Mapped Field</span></div>");

            if (data.d.length > 0) {
                for (var i = 0; i < data.d.length; i++) {

                    $("#MappingGrid").append("<div id='divItemRow" + (i + 1) + "' class='rows'><span class='width8Percent'>" + (i + 1) + "</span><span id='spanDBColumnName" + i + "' class='width20Percent'><select style='width:80px' id='drpDBColumnName" + (i + 1) + "'/> </span><span id='spanColumnName" + i + "' class='width35Percent'>" + data.d[i].PropColumnName + "</span><span id='spanColumnDataType" + i + "' class='width15Percent'>" + data.d[i].PropDataType + "</span><span class='width8Percent center'><img id='imgMapped" + (i + 1) + "' src='../../../Image/CommonImages/cross.png' /></span><span id='spanMappedValue" + (i + 1) + "' class='width15Percent Black'></span></div>");

                    $("#hfTotalCSVFileColumns").val(i + 1);

                    //  alert(data.d[i].PropColumnDTCharLength);
                    bindColumnsToDRP("#drpDBColumnName" + (i + 1), "../../../WebServices/CommonWebServices.asmx/GetCSVColumns", "{'FilePath':'" + FileName + "'}", data.d[i].PropColumnName, data.d[i].PropDataType, data.d[i].PropColumnDTCharLength);

                    $("#drpDBColumnName" + (i + 1)).bind("change", function (e) {

                        var id = e.target.id;
                        var index = id.match(/\d+$/);
                        index = parseInt(index, 10);
                        alert(index);
                        var imageId = "imgMapped" + index;
                        if ($(this).val() != "0") {
                            $("#spanMappedValue" + index).html($('select[id$=' + id + '] :selected').text());

                            if ($("#spanColumnName" + (index - 1)).html() == $("#spanMappedValue" + index).html()) {
                                document.getElementById(imageId).src = "../../../Image/CommonImages/right.png";
                            }

                            else {

                                document.getElementById(imageId).src = "../../../Image/CommonImages/cross.png";
                            }

                        }
                        return ValidateDuplicateColumns();



                    });

                }
            }
            else {
                //alertMsg("#msg", "alert_info", "Oops, No records found !");
            }


        },
        error: function (xml, textStatus, errorThrown) {

            //alert(xml.status + "||" + xml.responseText);
        }
    });
}
/* End: Create Shop Branch Grid..... */








/* Start: Check Duplicate selections........*/
function ValidateDuplicateColumns() {
    var totalRecords = $("#hfTotalCSVFileColumns").val();
    var totalMapped = 0;
    var arrayMappedRecords = new Array();
    for (var i = 1; i <= totalRecords; i++) {
        if ($("#spanMappedValue" + i).html() != "") {
            arrayMappedRecords[i] = $("#spanMappedValue" + i).html();
        }
        totalMapped = i;
         
    }
    return removeDupes(arrayMappedRecords);
}
/* End: Check Duplicate selections........*/

/* Start: The Below Function highlights those mapped fields which have selected more then once..... */
function removeDupes(arr) {
    var returnStatus = true;
    var nonDupes = [];
    var totalRecords = $("#hfTotalCSVFileColumns").val();
    $("#MappedMsg").removeClass("msg");
    $("#MappedMsg").html(" ");

    arr.forEach(function (value) {
        if (nonDupes.indexOf(value) == -1) {
            nonDupes.push(value);
            for (var i = 1; i <= totalRecords; i++) {

                if ($("#spanMappedValue" + i).html() == value) {
                    $("#spanMappedValue" + i).removeClass("Red");
                    $("#spanMappedValue" + i).addClass("Black");
                }
            }

        }
        else {
            for (var i = 1; i <= totalRecords; i++) {

                if ($("#spanMappedValue" + i).html() == value) {
                    $("#spanMappedValue" + i).removeClass("Black");
                    $("#spanMappedValue" + i).addClass("Red");

                }
            }

            $("#MappedMsg").addClass("msg");
            $("#MappedMsg").html("You have mapped some fields more then once !");
            returnStatus = false;
        }
    });
    return returnStatus;
}
/* End: The Below Function highlights those mapped fields which have selected more then once..... */



/* Start: Get Mapped Field.................*/
function ImportMappedTable() {
    if (ValidateDuplicateColumns()) {
        var Columns = '';
        var totalRecords = $("#hfTotalCSVFileColumns").val();
        var FileName = getQueryStringValue('file');
        var varFinalQuery = '';
        for (var i = 1; i <= totalRecords; i++) {
            var drpSelectedValue = $("#drpDBColumnName" + i).val();
            if (drpSelectedValue != '0') {
                Columns += drpSelectedValue + ",";
            }
        }
        Columns = Columns.substring(0, Columns.length - 1);
        var url = window.location.href;

        var urlSplit = url.split("?");
           var url1 = urlSplit[1].split("&");
           urlSplit1 = url1[0].split("=");

           if (urlSplit1[1] == "AjCollegeBranchMaster") {
               ImportFile($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns);

           }
           else if (urlSplit1[1] == "AjCollegeBranchCourseMaster") {
               ImportCourse($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns);
           }
           else if (urlSplit1[1] == "AjCollegeBranchCourseStream") {
               ImportCourseStream($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns);
           }
           else if (urlSplit1[1] == "AjCollegeCourseExamMaster") {
               ImportCourseExam($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns)
           }
           else if (urlSplit1[1] == "AjCollegeFacilities") {
         
               ImportCourseFacality($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns)
           } else if (urlSplit1[1] == "AjCollegeHighlights") {
               
               ImportCourseHighLights($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns)
           }
           else if (urlSplit1[1] == "AjCollegeRank") {
                 ImportCourseRankSource($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns)
               
           }
             else if (urlSplit1[1] == "AjCollegeHostelMaster") {
                 ImportHostel($("#drpTable option:selected").text(), $("#drpTable").val(), FileName, Columns)

             }

        return true;
    }
    else
        return false;
}
/* End: Get Mapped Field.................*/



/* Start: Import File to Selected Table..................*/

function ImportFile(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCollegeBranchBasicInfo",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
function ImportCourse(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCourse",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
            
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
function ImportCourseStream(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCourseStream",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
function ImportCourseExam(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCourseExam",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
function ImportCourseFacality(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCourseFacality",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
function ImportCourseHighLights(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCourseHighLights",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
function ImportCourseRankSource(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCourseRankSource",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
function ImportHostel(_strTableName, _strPrimaryKey, FileName, Columns) {
    $("#loadingdiv").show();
    $.ajax({
        type: "POST",
        url: "../../../WebServices/CommonWebServices.asmx/ImportCourseHostel",
        data: "{'_strTableName':'" + _strTableName + "','_strPrimaryKey':'" + _strPrimaryKey + "','fileName':'" + FileName + "','Columns':'" + Columns + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (data) {
            $("#loadingdiv").hide();
            var sucessCount = data.d;
            location.href = '/AdminPanel/College/CollegeImport/CompletedExcelUtitlity.aspx?t=' + sucessCount[0].TotalNoRows + '&f=' + sucessCount[1].TotalFailureCount + '&s=' + sucessCount[2].TotalSucessCount;
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);

        }
    });
}
/* End: Import File to Selected Table..................*/







