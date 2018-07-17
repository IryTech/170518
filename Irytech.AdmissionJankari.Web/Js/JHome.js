//#region TOPRANKED COLLEGE
/*......................................START OF TOP PRIVATE COLLEGE JS SECTION.............................*/
       var courseName = $('[id$=hdnTopCourseName]').val();
       var pageSizeTopRanked = $('[id$=hdnTopColleges]').val();
       var courseIDTopRanked;
       var pageIndexTopRanked = 1;
       var pageTopRanked;
       function BindTabs(data) {
           var topStatus = true;
           if (data.d.length > 0) {

               $.each(data.d, function(i, entity) {

                   if ($('[id$=hdnTopCourse]').val() == entity.CourseId) {
                       topStatus = false;
                       $('#tab ul').append('<li class="active"  id="li' + entity.CourseId + '"  rel=' + entity.CourseId + ' onclick="BindCollegeByCourse(' + entity.CourseId + ',' + entity.CourseId + ',1);return false;"><a class="image_tooltip" title=' + entity.CourseName + ' href="javascript:void(0)" >' + entity.CourseName + '</a></li>');
                       BindCollegeByCourse(entity.CourseId, (entity.CourseId), 1);

                       $('.tab_container').append('<div id="tab' + entity.CourseId + '" class="tab_content "></div>');
                   } else {
                       $('#tab ul').append('<li   id="li' + entity.CourseId + '"  rel=' + entity.CourseId + ' onclick="BindCollegeByCourse(' + (entity.CourseId) + ',' + ((entity.CourseId)) + ',1);return false;"><a class="image_tooltip" title=' + entity.CourseName + ' href="javascript:void(0)">' + entity.CourseName + '</a></li>');
                       $('.tab_container').append('<div id="tab' + entity.CourseId + '" class="tab_content"></div>');
                   }
               });
               if (topStatus) {
                   var cousreId = $('[id$=hdnTopCourseInAppSetting]').val().trim();
                   var liId = "#li"+cousreId;
                   $(liId).addClass("active");
                   BindCollegeByCourse($('[id$=hdnTopCourseInAppSetting]').val(), $('[id$=hdnTopCourseInAppSetting]').val(), 1);

               }

               $(".tab_content").hide();
               $(".tab_content:first").show();

               $("#ulTopRanked li").bind("click", function () {

                   $("#noRecords").hide();
                   $("#ulTopRanked li").removeClass("active");
                   $(this).addClass("active");
                   $(".tab_content").hide();
                   var activeTabTop = $(this).attr("rel");
                   $("#" + activeTabTop).fadeIn();
                   ChangeCourseId(activeTabTop);
                   var courseName = RemoveChahracterfromCorse($(this).find("a").html());
                   window.BindBanner(courseName, activeTabTop);
                   show(courseName.toLocaleLowerCase(), activeTabTop);
                   //course = courseName.toLocaleLowerCase();
                   ChangeUrl(courseName.toLocaleLowerCase());                       /*...to change url of links at master page...*/
                   window.BindCollegePrivateByCourse(activeTabTop, activeTabTop, 1);
                   $("#noRecordsPrivate").hide();
                   var lidids = "#ulPrivate li#lii" + activeTabTop;
                   $("#ulPrivate li").removeClass("active");
                   $(lidids).addClass("active");
                   $(".tab_contentPrivate").hide();
                   var activeTab = $(lidids).attr("rel");
                   $("#" + activeTab).fadeIn();

               });
           }
       }


       $(".TopPage .page").bind("click", function () {
           pageIndexTopRanked = parseInt($(this).attr('page'));
           BindCollegeByCourse(courseIDTopRanked, pageTopRanked, pageIndexTopRanked);
           $("#ulTopRanked").focus();
       });
       function BindCollegeByCourse(courseId, id, index) {
          
           $("#topPrivatePaging").html("");

           courseIDTopRanked = courseId;
           pageTopRanked = id;
           $("#totalRecords").text("");
           $("#loading").show();
           $.ajax({
               type: "POST",
               url: "WebServices/CommonWebServices.asmx/GetTopRankedColleges",
               data: '{"pageNumber":"' + (index - 1) + '","pageSize":" ' + window.pageSize + '","courseId":"' + courseId + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               cache: true,
               async: true,
               success: function (msg) {
                   var total = msg.d.TotalRecords;
                  $("#loading").hide();
                   if (total > 0) {
                       $("#noRecords").hide();
                       $(".TopPage").show(); $("#topPrivatePaging").html("");
                       BindCollgeDataByCourseList(msg, id, total);
                   }
                   else {
                       $(".TopPage").hide();
                       $("#noRecords").show();

                   }

               },
               error: function (xml, textStatus, errorThrown) {
                   //alert(xml.status + "||" + xml.responseText);
               }
           });
       }



       function BindCollgeDataByCourseList(response, id, total) {
           $('#tab' + id).html('');
        
          var imageDataTop = "";
             $.each(response.d.MessageList, function (i, client) {

                 imageDataTop = "";
                 if (client.CollegeBranchLogo == null || client.CollegeBranchLogo == "") {
                     imageDataTop = "<img class='image_tooltip' alt='" + client.CollegeBranchName + "' src='/image.axd?College=NoImage.jpg' height='80px' width='70px' />";
                 }
                 else {
                     imageDataTop = "<img alt='" + client.CollegeBranchName + "' src='/image.axd?College=" + client.CollegeBranchLogo + "' height='80px' width='70px' />";
                 }
                 if (client.CollegeAssociationId ==$('[id$=hdnTopAssociation]').val()) {
                     $('#tab' + id).append("<div class='TabStyle'><ul><li class='image Imgarrow marginRight'><a class='image_tooltip'  href='" + client.CollegeUrl + "'   rel='canonical' title='" + client.CollegeBranchName + "'>" + imageDataTop + "</a></li><li class='width68Percent'><strong><a style='font-size:13px; text-decoration:underline;' class='image_tooltip' href='" + client.CollegeUrl + "' rel='canonical' title='" + client.CollegeBranchName + "'>" + client.CollegeBranchName + "</a></strong><strong style=' color:#494949; font-size:11px !important;'> Est : " + client.CollegeBranchEst + "  | " + client.CollegeManagementType + "</strong><strong><a class='image_tooltip'  href='" + client.UniversityUrl + "' title='" + client.UniversityName + "'>" + client.UniversityName + "</a></strong><strong > <span>Rank :</span> <span id='lblRankSource" + id + i + "'></span><span style='margin-left:15px;'> By - </span> <span id='lblRankSourceNameForTop" + id + i + "'></span></strong></li></ul><div class='clearBoth'></div></div>");

                 }
                 else {

                     $('#tab' + id).append("<div class='TabStyle'><ul><li class='image Imgarrow marginRight'><a class='image_tooltip'  href='" + client.CollegeUrl + "'   rel='canonical' title='" + client.CollegeBranchName + "'>" + imageDataTop + "</a></li><li class='width68Percent'><strong><a style='font-size:13px; text-decoration:underline;' class='image_tooltip' href='" + client.CollegeUrl + "' rel='canonical' title='" + client.CollegeBranchName + "'>" + client.CollegeBranchName + "</a></strong><strong style='color:#494949; font-size:11px !important;'> Est : " + client.CollegeBranchEst + "  | " + client.CollegeManagementType + "</strong><strong><a  class='image_tooltip' href='" + client.UniversityUrl + "' title='" + client.UniversityName + "'>" + client.UniversityName + "</a></strong><strong ><span>Rank :</span> <span id='lblRankSource" + id + i + "'></span><span style='margin-left:15px;'> By - </span> <span id='lblRankSourceNameForTop" + id + i + "'></span></strong></li></ul><div class='clearBoth'></div></div>");

                 }
                 $(".image_tooltip").tipTip({ maxWidth: "auto", delay: 50 });
                 GetRankSourceByCollegeBranchCourseId(client.CollegeIdBranchId, i, id, 'lblRankSource', 'lblRankSourceNameForTop');

             });


           $("#loading").hide();
           $('#tab' + id).show();
           $('#tabPrivate' + id).show();
           $(".TopPage").AdmissionJankari_Pager({
               ActiveCssClass: "current",
               PagerCssClass: "pager",
               PageIndex: pageIndexTopRanked,
               PageSize: pageSizeTopRanked,
               RecordCount: total
           });
       }


       function GetRankSourceByCollegeBranchCourseId(collegeId, id, index,rank,rankSource) {
           rank = '#' + rank;
           rankSource = '#' + rankSource;
           $.ajax({
               type: "POST",
               url: "/WebServices/CommonWebServices.asmx/GetRankSourceByCollegeBranchCourseId",
               data: '{"collegeId":"' + collegeId + '"}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               async: true,
               cache: false,
               success: function (msg) {
                   var overAllRank = msg.d[0].CollegeOverAllRank;
                   if (overAllRank == "" || overAllRank == null) {
                       $(rank + index + id).text("N/A");
                       $(rankSource + index + id).text(msg.d[0].RankSourceName);
                   }
                   else {
                       
                       $(rank + index + id).text(overAllRank);
                       $(rankSource + index + id).text(msg.d[0].RankSourceName);
                      
                   }

               }, error: function (xml) {
                  // alert(xml.status + "||" + xml.responseText);
               }
           });
       }
       function ChangeUrl(courseName) {
           $("#sndCollSrch").attr('href', ("/course/" + courseName).toLowerCase());
           $("#sndFooterCllgSrch").attr('href', ("/course/" + courseName).toLowerCase());
           $("#sndHeaderDirectAdmission").attr('href', ("/" + courseName + "/Get-Direct-Admission").toLowerCase());
           $("#sndDirectAdmission").attr('href', ("/" + courseName + "/Get-Direct-Admission").toLowerCase());
           $("#sndFooterDirectAdmission").attr('href', ("/" + courseName + "/Get-Direct-Admission").toLowerCase());

           $("#sndExamLink").attr('href', ("/Exams/" + courseName).toLowerCase());

           $("#sndFooterExam").attr('href', ("/Exams/" + courseName).toLowerCase());

          $("#sndCollCompare").attr('href', ("/" + courseName + "/Compare-Colleges/").toLowerCase());
           $("#libookseat").attr('href', ("/bookseat/" + courseName).toLowerCase());
           $("#sndCourseCompare").attr('href', ("/" + courseName + "/Compare-Streams/").toLowerCase());
       }
       /*......................................END OF TOP RANKED COLLEGE SECTION................................*/
       //#endregion TOPRANKED COLLEGE
       


       //#region BEST PRIVATE JQUERY SECTION.......................................
       //...............................BEST PRIVATE JQUERY SECTION..................'
     var courseName = $('[id$=hdnCourseNameAtPrivate]').val();
    var pageSize =$('[id$=hdnPrivateCollege]').val();
    var courseID;
    var pageIndex = 1;
    var page;
  
    $.ajax({
        type: "POST",
        url: "WebServices/CommonWebServices.asmx/GetCourseSourceHome",
        data: "{}",
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            window.BindTabs(response);
            BindTabsPrivate(response);
          
        },
        error: function (xml, textStatus, errorThrown) {
            //alert(xml.status + "||" + xml.responseText);
        }
    });



    function BindTabsPrivate(data) {
        var status = true;
        if (data.d.length > 0) {

            $.each(data.d, function (i, entity) {

                courseList.push(entity.CourseId);
                if ($('[id$=hdnPrivateCourse]').val()== entity.CourseId) {
                    status = false;
                    $('#privateTabs ul').append('<li id="lii' + entity.CourseId + '" class="active" rel=' + entity.CourseId + ' onclick="BindCollegePrivateByCourse(' + (entity.CourseId) + ',' + (entity.CourseId) + ',1)"><a title=' + entity.CourseName + ' class="image_tooltip"  href="javascript:void(0)" >' + entity.CourseName + '</a></li>');
                    $('#tabPrivateContainer').append('<div id="tabPrivate' + entity.CourseId + '" class="tab_contentPrivate"></div>');
                    BindCollegePrivateByCourse(entity.CourseId, (entity.CourseId), 1);


                }
                else {

                    $('#privateTabs ul').append('<li id="lii' + entity.CourseId + '"  rel=' + entity.CourseId + ' onclick="BindCollegePrivateByCourse(' + entity.CourseId + ',' + entity.CourseId + ',1)"><a title=' + entity.CourseName + '  href="javascript:void(0)"  class="image_tooltip" >' + entity.CourseName + '</a></li>');
                    $('#tabPrivateContainer').append('<div id="tabPrivate' + entity.CourseId + '" class="tab_contentPrivate"></div>');
                }

            });
           
            if (status) {
            var courseId=$('[id$=hdnAppSettingCourseId]').val();
            $("#lii"+courseId).addClass("active");
            BindCollegePrivateByCourse($('[id$=hdnAppSettingCourseId]').val(), $('[id$=hdnAppSettingCourseId]').val(), 1);

            }
            $(".tab_contentPrivate").hide();
            $(".tab_contentPrivate:first").show();

            $("#ulPrivate li").click(function () {

                $("#noRecordsPrivate").hide();
                $("#ulPrivate li").removeClass("active");
                $(this).addClass("active");
                $(".tab_contentPrivate").hide();
                var activeTabPrivate = $(this).attr("rel");
                var id = $(this).attr("id");
                $("#" + activeTabPrivate).fadeIn();
                ChangeCourseId(activeTabPrivate);
                var url = RemoveIlegalCharecterFromCourse($(this).find("a").html());
                window.BindBanner(url, activeTabPrivate);
                show(url.toLocaleLowerCase(), activeTabPrivate);
                window.ChangeUrl(url.toLocaleLowerCase());                  /*...to change url of links at master page...*/
                window.BindCollegeByCourse(activeTabPrivate, activeTabPrivate, 1);
                $("#noRecords").hide();
                $("#ulTopRanked li").removeClass("active");
                var lididPrivate = "#ulTopRanked li#li" + activeTabPrivate;
                $(lididPrivate).addClass("active");
                $(".tab_content").hide();
                var activeTab = $(lididPrivate).attr("rel");
                $("#" + activeTab).fadeIn();


            });
        }
    }

    $(".Pager .page").bind("click", function () {
        pageIndex = parseInt($(this).attr('page'));
        BindCollegePrivateByCourse(courseID, page, pageIndex);
        $("#ulPrivate").focus();
    });
    function BindCollegePrivateByCourse(courseId, id, index) {

        $("#privatePaging").html("");

        courseID = courseId;
        page = id;
        var data = '{"pageNumber":"' + (index - 1) + '","pageSize":" ' + pageSize + '","courseId":"' + courseId + '"}'
        $("#totalRecordsPrivate").text("");
        $("#Privateloading").show();
        $.ajax({
            type: "POST",
            url: "WebServices/CommonWebServices.asmx/GetPrivateColleges",
            data: data,
            cache: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#Privateloading").hide();
                var privatetotal = msg.d.TotalRecords;
                if (privatetotal > 0) {
                    $("#noRecordsPrivate").hide();
                    $(".Pager").show();
                    $("#privatePaging").html("");
                    BindPrivateCollgeDataByCourseList(msg, id, privatetotal);

                } else {

                    $(".Pager").hide();
                    $("#Privateloading").hide();
                    $("#noRecordsPrivate").show();

                }

            },
            error: function (xml, textStatus, errorThrown) {
                alert(xml.status + "||" + xml.responseText);
            }
        });
    }

    function BindPrivateCollgeDataByCourseList(response, id, privatetotal) {
        $('#tabPrivate'+id).html('');
     var imageData;
          var onlineStatus;
         $.each(response.d.MessageList, function (i, client) {


              imageData = "";
              if (client.CollegeBranchLogo == null || client.CollegeBranchLogo == "") {
                  imageData = "<img  src='/image.axd?College=NoImage.jpg' alt='" + client.CollegeBranchName + "' height='80px' width='70px'/>";
              }
              else {
                  imageData = "<img  src='/image.axd?College=" + client.CollegeBranchLogo + "' alt='" + client.CollegeBranchName + "' height='80px' width='70px'/>";
              }
              onlineStatus = "";
              if (client.CollegeBranchCourseOnlineStatus) {
                  courseName = RemoveIlegalCharecterFromCourse(client.CourseName);
                  onlineStatus = "<a rel='canonical' href='/" + courseName.toLowerCase() + "/get-direct-admission' id='participate'  class='rightImglink borderbtn image_tooltip' title='Apply Now For " + client.CollegeBranchName + "'>Apply Now</a>";
              }
              if (client.CollegeAssociationId == $('[id$=hdnAssociation]').val()) {

                  $('#tabPrivate' + id).append("<div class='TabStyle1' itemscope itemprop='http://schema.org/EducationalOrganization'><ul><li  class='image Imgarrow marginRight'><a class='image_tooltip' itemprop='url' href='" + client.CollegeUrl + "'  rel='canonical' title='" + client.CollegeBranchName + "'>" + imageData + "</a></li><li class='width68Percent'><strong itemprop='college-Name'><a class='image_tooltip' style='font-size:13px; text-decoration:underline; itemprop='url' href='" + client.CollegeUrl + "' rel='canonical' title='" + client.CollegeBranchName + "'>" + client.CollegeBranchName + "</a></strong><strong style='color:#494949; font-size:11px !important;'itemprop='establishment'> Est : " + client.CollegeBranchEst + " | " + client.CollegeManagementType + "</strong><label class='fright'>" + onlineStatus + "</label><strong itemprop='university-Name'><a class='image_tooltip'  href='" + client.UniversityUrl + "' title='" + client.UniversityName + "'>" + client.UniversityName + "</a></strong><strong ><span itemprop='rank'> Rank :</span> <span id='lblPrivateRankSource" + id + i + "'></span><span itemprop='Publisher' style='margin-left:15px;'>By - </span> <span id='lblRankSourceNameForPrivate" + id + i + "'></span></strong></li></ul><div class='clearBoth'></div></div>");

              }
              else {

                  $('#tabPrivate' + id).append("<div class='TabStyle' itemscope itemprop='http://schema.org/EducationalOrganization'><ul><li class='image Imgarrow marginRight'><a class='image_tooltip' itemprop='url' href='" + client.CollegeUrl + "' rel='canonical' title='" + client.CollegeBranchName + "'>" + imageData + "</a></li><li class='width68Percent'><strong itemprop='college-Name'><a class='image_tooltip' style='font-size:13px; text-decoration:underline; itemprop='url' href='" + client.CollegeUrl + "' rel='canonical' title='" + client.CollegeBranchName + "'>" + client.CollegeBranchName + "</a></strong><strong style='color:#494949; font-size:11px !important;' itemprop='establishment'> Est : " + client.CollegeBranchEst + " | " + client.CollegeManagementType + "</strong><label class='fright'>" + onlineStatus + "</label><strong itemprop='university-Name'><a class='image_tooltip'  href='" + client.UniversityUrl + "' title='" + client.UniversityName + "'>" + client.UniversityName + "</a></strong><strong><span itemprop='rank'> Rank :</span> <span id='lblPrivateRankSource" + id + i + "'></span><span itemprop='Publisher' style='margin-left:15px;'>By - </span> <span id='lblRankSourceNameForPrivate" + id + i + "'></span></strong></li></ul><div class='clearBoth'></div></div>");

              }
              $(".image_tooltip").tipTip({ maxWidth: "auto", delay: 50 });
              window.GetRankSourceByCollegeBranchCourseId(client.CollegeIdBranchId, i, id, 'lblPrivateRankSource', 'lblRankSourceNameForPrivate');


          });

        $("#Privateloading").hide();
        
        $('#tabPrivate' + id).show();
     
        $(".Pager").AdmissionJankari_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: pageIndex,
            PageSize: pageSize,
            RecordCount: privatetotal
        });

    }

    function AddClassMasterToolTip() {
        $("a").addClass("masterTooltip");
        $("input").addClass("masterTooltip");
        $("select").addClass("masterTooltip");
        $("textarea").addClass("masterTooltip");
        $("img").addClass("masterTooltip");
    }
    //#endregion BEST PRIVATE JQUERY SECTION.......................................