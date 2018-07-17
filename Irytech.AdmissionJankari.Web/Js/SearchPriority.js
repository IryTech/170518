        SearchPriorityObject = {
            $: function(id) {

                return document.getElementById(id);
            },
            i18n: {
                FieldValue: "",
            },
            // Adds event to window.onload without overwriting currently assigned onload functions.
            addLoadEvent: function(func) {

                var oldonload = window.onload;
                if (typeof window.onload != 'function') {
                    window.onload = func;
                } else {
                    window.onload = function() {
                        oldonload();
                        func();
                    };
                }
            },
            FieldValue: {
                SearchPriorityCount: null,
                CourseName: null,
                CollegeId: null
            },
            CourseChange: function(control) {

                ChangeCourseId($(control).val());

                location.href = ("/Course/" + RemoveChahracterfromCorse($(control).find("option:selected").text())).toLowerCase();
            },
            SaveHelpLineNo: function(helpLineNumber) {
                $("#divHelpLine").html(" ");
                $("#divHelpLine").append('<div class="login"><fieldset class="boxBody" style="background-image:url(/image.axd?Common=help_line.png); background-repeat:no-repeat; background-position:right bottom; height:180px;"><h2 style="width:400px; padding-bottom:10px; display:block; border-bottom:2px solid #e1e1e1;">Admissionjankari.com</h2><h3 class="streamCompareH3">Help Line Number</h3><h3 style="font-weight:bold; font-size:1em; color:Maroon;" id="HelpLine"></h3></fieldset></div>');

                $("#HelpLine").html(helpLineNumber);
                OpenPoup("divHelpLine", "450", "sndShowHelpline");
            },
            OpenQuickQueryPoup: function(divid, width, linkId, collegeBranchId, courseId, cityId, branchCourseId, collegeName) {

                SearchPriorityObject.FieldValue.CollegeId.value = collegeBranchId;
                $("#hdnCollege").val(collegeBranchId);
                $("#hdnCourse").val(courseId);
                $("#hdnCityId").val(cityId);
                $("#hndcollegeName").val(collegeName);
                $("#hdnBranchCourseId").val(branchCourseId);
                OpenPoup(divid, width, linkId);
            }
        };
// add this to global if it doesn't exist yet
    if (typeof ($) == 'undefined')
    window.$ = SearchPriorityObject.$;

    SearchPriorityObject.addLoadEvent(RegisterSearchValue);

function SearchPriorityListCollege(courseId, stateId, cityId, examId, mgtId) {
   
      var query = '{"courseId":"' + courseId + '","stateId":"' + stateId + '","cityId":"' + cityId + '","examId":"' + examId + '","mgtId":"' + mgtId + '"}';

      $.ajax({
          type: "POST",
          url: "/WebServices/CommonWebServices.asmx/SearchPriorityListingCollege",
          data: query,
          async: true,
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function(response) {
              BindSearchListingCollege(response.d);
          },
          error: function(xml, textStatus, errorThrown) {
              alert(xml.status + "||" + xml.responseText);
          }
      });
  }

  function BindSearchListingCollege(data) {

      $("#searchPriorityListCollege").html("");

      if (data.length > 0) {
          $("#searchPriorityListCollege").show();

      } else {
          $("#searchPriorityListCollege").hide();

      }
      var ulSearchList = "<a class='buttons prev' href='#'></a><div class='viewport'><ul class='overview'>";
      var ulSearchList1 = "<ul class='featured_list clearfix'>";
      var liSearchLIst = "";
      var liSearchLIst1 = "";
      var imageData;
      var imageData1;
      $.each(data, function(i, client) {
          imageData = "";
          if (client.CollegeBasicInfo.CollegeBranchLogo == null || client.CollegeBasicInfo.CollegeBranchLogo == "") {
              imageData = "<img  src='/image.axd?College=NoImage.jpg' alt='" + client.CollegeBasicInfo.CollegeBranchName + "' height='100px' width='100px'/>";
              imageData1 = "<img class='fleft' style='margin:5px;'  src='/image.axd?College=NoImage.jpg' alt='" + client.CollegeBasicInfo.CollegeBranchName + "' height='30' width='30'/>";
          } else {
              imageData = "<img  src='/image.axd?College=" + client.CollegeBasicInfo.CollegeBranchLogo + "' alt='" + client.CollegeBasicInfo.CollegeBranchName + "' height='100px' width='100px'/>";
              imageData1 = "<img class='fleft' style='margin:5px;' src='/image.axd?College=" + client.CollegeBasicInfo.CollegeBranchLogo + "' alt='" + client.CollegeBasicInfo.CollegeBranchName + "' height='30' width='30'/>";
          }

          liSearchLIst += "<li><a class='image_tooltip' target='_blank'  href='" + client.CollegeBannerList.BannerUrl + "'  rel='canonical' title='" + client.CollegeBasicInfo.CollegeBranchName + "'>" + imageData + "</a><strong itemprop='college-Name'><a  class='image_tooltip'  href='" + client.CollegeBannerList.BannerUrl + "' rel='canonical' title='" + client.CollegeBasicInfo.CollegeBranchName + "' target='_blank'>" + client.CollegeBasicInfo.CollegeBranchName + "</a></strong></li>";
          if (i <= 5)
              liSearchLIst1 += "<li><div class='featured_top clearfix'><a  class='image_tooltip' target='_blank'  href='" + client.CollegeBannerList.BannerUrl + "'  rel='canonical' title='" + client.CollegeBasicInfo.CollegeBranchName + "'>" + imageData + "</a><div class='tbl_scroll'><h3><a class='image_tooltip' taget='_blank'  href='" + client.CollegeBannerList.BannerUrl + "' rel='canonical' title='" + client.CollegeBasicInfo.CollegeBranchName + "'>" + client.CollegeBasicInfo.CollegeBranchName + "</a><h3></div></div></li>";
      });
  
      if (liSearchLIst.length != 0) {
          var ulList1 = ulSearchList + liSearchLIst + "</ul></div><a class='buttons next' href='#'></a>";
          var ulList2 = ulSearchList1 + liSearchLIst1 + "</ul>";
          $("[id*=searchPriorityListCollege]").append(ulList1);
          $("[id*=div_scroll]").append(ulList2);
          $('#searchPriorityListCollege').tinycarousel({ display: 2, interval: true });
         $(".image_tooltip").tipTip({ maxWidth: "auto", delay: 50 });
      }
  }

  function checkAllChecked(chkId) {
      var checked = $(".itmTemplateInnerDiv span input:checkbox:checked").length;
      if (checked > 0) {
          $("#compareButton").addClass("visible");

          if (checked > 2) {
              alert("Please choose only 2 colleges to compare");
              $('input:checkbox[id=' + chkId + ']').attr('checked', false);
              $("#compareButton").addClass("hidden");
              $("#compareButton").removeClass("visible");
          }
      }
  }

  $(document).ready(function() {

      $(window).scroll(function() {
          var scrollTop = 350;
          if ($(window).scrollTop() >= scrollTop) {
              $('#div_scroll').css({
                  display: 'block',
                  position: 'fixed',
                  top: '0'
              });
          }
          if ($(window).scrollTop() < scrollTop) {
              $('#div_scroll').removeAttr('style');
          }
      });

  });
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            $('#searchPriorityListCollege').tinycarousel({ display: 2, interval: true });
        }
    }
    var id = "";

    function compareSelected() {
        var qsa = "";
        var l = 0;

        $("#divCollegeList .comapreCheckbox").find("input:checkbox:checked").each(function(k) {
            if (qsa == "")
                qsa += "CollegeId" + (k + 1) + "=" + $(this).val();
            else
                qsa += "&CollegeId" + (k + 1) + "=" + $(this).val();

            l++;

        });


        if (qsa != "") {

            if (l >= 2) {

                document.location.href = ("/" + SearchPriorityObject.FieldValue.CourseName.value + "/Compare-Colleges?" + qsa).toLowerCase();

            } else {
                alert("Please select minimum two Colleges to compare");
                return false;
            }
        } else {
            alert("Pleases choose at least two Colleges to compare.\nClick on the Matching Versions link to see the Colleges available for comparison.");
        }
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            $("#fade").show();
            $("#divImage").show();
        }
        // Called when async postback ends

        function prm_EndRequest(sender, args) {
            $("#fade").hide();
            $("#divImage").hide();
        }
     var countdata = 0;
         function addToCompare(collegeName, collegeId, imageName) {
             imageName = imageName != "" ? imageName : "NoImage.jpg";
            var checks = document.getElementById("chk_Compare" + collegeId);
             countdata = $('#countdata').val();
             var  contentCount = $('#hdnContentCount').val();
             if (checks.checked == true) {
                 if (parseInt(countdata) < 2) {
                     countdata = parseInt(countdata) + 1;
                     
                      var strHtml = "";
                     if (parseInt(contentCount) == 0) {
                         strHtml += '<a href="javascript:void(0)" class="closeIco_College" onclick=CloseCompareBox();></a>';
                     }
                    
                     strHtml += '<div class="FL cmbox POSR MR10" style="width:145px;" id=compareDiv' + collegeId + '>';
                     strHtml += '<a class="closeIco_College"  href="javascript:void(0);" onClick=removeThisVariantShowMsg(' + countdata + ',' + collegeId + ')></a>';
                     strHtml += '<img src="/image.axd?College=' + imageName + '" alt="' + collegeName + '" width="110px" height="110px"/>';
                     strHtml += '<p class="gry12" style="text-align:left;height:35px;overflow:hidden;">' + collegeName + '</p></div>';
                      if (parseInt(contentCount) ==0) {
                      
                         strHtml += '<div class="FL cmbox POSR" id="selectCollege">';
                         strHtml += '<a href="javascript:void(0)" class="closeIco_College" onclick=CloseCompareBox();></a> ';
                         strHtml += '<img src="/image.axd?College=NoImage.jpg" width="100" height="100">';
                         strHtml += '<p class="gry12">Select college to compare</p>';
                         strHtml += '</div><div class="FL cmboxlast">';
                         strHtml += '<p style="padding-top:15px;">You have selected following college to compare</p>';
                         strHtml += '<div style="padding-top:20px;">';
                         strHtml += '<input type="button" class="comparebtn_newCollge" title="Compare,college you have selected" onclick="compareSelected();return false;"/>';
                         strHtml += '</div><div class="clearBoth"></div>';

                     }
                   contentCount = parseInt(contentCount) + 1;
                     if (countdata == 2) 
                         jQuery("#selectCollege").hide();
                     else jQuery("#selectCollege").show();
                     jQuery("#compareDiv").prepend(strHtml);
                      jQuery("#compareDiv").show();
                     $('#countdata').val(countdata);
                      $('#hdnContentCount').val(contentCount);
                 } else if (countdata > 1) {
                     document.getElementById('chk_Compare' + collegeId).checked = false;
                     alert('You have already selected 2 item');

                 }
             } else {
                 removeThisVariantShowMsg(countdata, collegeId);
             }

         }

         function removeThisVariantShowMsg(countdata1, variantid) {
       
             var countdata = $('#countdata').val();
             if (countdata > 0) {
                 countdata = (countdata - 1);
                 $("#chk_Compare" + variantid).removeAttr('checked');
                 $("#compareDiv" + variantid).remove();
                 $('#countdata').val(countdata)
                 if (countdata == 0) {
                     CloseCompareBox();
                 } else {
                     jQuery("#selectCollege").show();
                 }
             }
         }

         function CloseCompareBox() {
             $('#countdata').val(0);
             $('input[name=compare]').attr('checked', false);
             $('#hdnContentCount').val(0);
             $('#compareDiv').html("");
             $('#compareDiv').hide();
         }
        

        