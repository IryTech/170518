<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcIntertestedCollegeList.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcIntertestedCollegeList" %>
 <div id='grdCollegeList' style="display:none;" class='grdView'>
 </div>
 <input type="hidden" id="hdnTotalCount"  value="0"/>
 <script type="" defer="defer">

     $(document).ready(function () {
        BindCollegeList();
     });


     function BindCollegeList() {

         $.ajax({
             type: "POST",
             contentType: "application/json; charset=utf-8",
             url: "/WebServices/CommonWebServices.asmx/GetIntertestedForConsulling",
             data: "{}",
             async: true,
             success: function (response) {

                 var xmlDoc = $.parseXML(response.d);

                 var xml = $(xmlDoc);
                 var rowIndex = 0;
                 var collegeList = xml.find("Table");

                 $("[id*=grdCollegeList]").html("");
                 $("[id*=grdCollegeList]").html("<div> <ul class='vertical paddingTopBot' style='border-bottom:1px solid #e1e1e1;'><li class='width10Percent'><strong>Sr No</strong></li><li class='width70Percent'><strong>College Name</strong></li><li class='width5Percent'><strong>Action</strong></li><div class='clearBoth'></div></ul></div>");
                 $.each(collegeList, function (i) {

                     rowIndex = ++i;
                     $("[id*=grdCollegeList]").append("<div id='divCollegeList" + rowIndex + "'  class='rows' onclick='clicked(this," + rowIndex + ");'><ul class='vertical clgCompare'><li class='width10Percent'>" + rowIndex + "</li><li class='width70Percent'>" + $(this).find("AjCollegeBranchName").text() + "</li><li class='width5Percent'><a href='#' onclick='DeleteIntertestedCollege(" + $(this).find("AjStudentInterestedCollegeCounsellingId").text() + ")'><img src='/image.axd?Common=blueClose.png' /></a></li><div class='clearBoth'></div></ul></div>");

                 });
                 if (rowIndex > 0) {
                     $("[id*=hdnTotalCount]").val(rowIndex);
                     $("[id*=spanCartTotalCount]").text(rowIndex);

                     $("[id*=grdCollegeList]").css("display", "block"); 
                     showHideControls("divWishlistInformation", "show");
                 } else {
                     $("[id*=hdnTotalCount]").val(0);
                     
                     $("[id*=spanCartTotalCount]").text(0);
                     $("[id*=grdCollegeList]").css("display", "none"); showHideControls("divWishlistInformation", "hide");
                 }

             },
             failure: function (response) {
                 // alert(response.d);
             },
             error: function (xml, textStatus, errorThrown) {
                 //alert(xml.status + "||" + xml.responseText);
             }
         });

     }


     function DeleteIntertestedCollege(insrtedCollegeBranchCourseId) {
         $.ajax({
             type: "POST",
             url: "/WebServices/CommonWebServices.asmx/DeleteIntertestedForConsulling",
             data: '{interestedCollegeId:"' + insrtedCollegeBranchCourseId + '"}',
             async: false,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (response) {
                 alert(response.d);
                 BindCollegeList();
             },
             error: function (xml, textStatus, errorThrown) {
                 //alert(xml.status + "||" + xml.responseText);
             }
         });
     }

 </script>