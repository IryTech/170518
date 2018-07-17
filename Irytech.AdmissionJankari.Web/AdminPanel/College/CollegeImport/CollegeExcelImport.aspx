<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="CollegeExcelImport.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeImport.CollegeExcelImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="grdOuterDiv">
        <h4 style="margin-bottom: 10px; border-bottom: 0px; color: #333;">
            College Data Import Utility</h4>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College Basic Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="CollegeBasicInfoUploader" name="CollegeBasicInfoUploader" type="file" />
                    <input type="button" id="uploads" value="Upload" onclick="return ajaxFileUpload('AjCollegeBranchMaster','CollegeBasicInfoUploader');" />
                    <div id="loading" class="loading">
                        <img src="/image.axd?Common=Loading.gif" />
                    </div>
                </li>
            </ul>
        </fieldset>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College Course Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="CollegeCourseInfoUploader" type="file" name="CollegeCourseInfoUploader" />
                    <input type="button" id="Button1" value="Upload" onclick="return ajaxFileUpload('AjCollegeBranchCourseMaster','CollegeCourseInfoUploader');" />
                </li>
            </ul>
        </fieldset>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College Stream Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="CollegeStreamInfoUploader" type="file" name="CollegeStreamInfoUploader" />
                    <input type="button" id="btnUploadCollegeStreamInfo" value="Upload" onclick="return ajaxFileUpload('AjCollegeBranchCourseStream','CollegeStreamInfoUploader');" />
                </li>
            </ul>
        </fieldset>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College Exam Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="fulCollegeExamInfo" type="file" name="fulCollegeExamInfo" />
                    <input type="button" id="btnUploadCollegeExamInfo" value="Upload" onclick="return ajaxFileUpload('AjCollegeCourseExamMaster','fulCollegeExamInfo');" />
                </li>
            </ul>
        </fieldset>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College Facality Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="fulCollegeFacalityInfo" type="file" name="fulCollegeFacalityInfo" />
                    <input type="button" id="btnFacality" value="Upload" onclick="return ajaxFileUpload('AjCollegeFacilities','fulCollegeFacalityInfo');" />
                </li>
            </ul>
        </fieldset>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College HighLights Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="fulCollegeHighLightsInfo" type="file" name="fulCollegeHighLightsInfo" />
                    <input type="button" id="Button2" value="Upload" onclick="return ajaxFileUpload('AjCollegeHighlights','fulCollegeHighLightsInfo');" />
                </li>
            </ul>
        </fieldset>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College Rank Source Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="fulCollegeRankSourcesInfo" type="file" name="fulCollegeRankSourcesInfo" />
                    <input type="button" id="Button3" value="Upload" onclick="return ajaxFileUpload('AjCollegeRank','fulCollegeRankSourcesInfo');" />
                </li>
            </ul>
        </fieldset>
        <fieldset class="width45perc fleft clearNon ">
            <legend>Upload College Hostel Info</legend>
            <ul class="options-bar">
                <li>
                    <label style="width: 130px !important;">
                        Choose File :</label>
                    <input id="fulCollegeHostel" type="file" name="fulCollegeHostel" />
                    <input type="button" id="Button4" value="Upload" onclick="return ajaxFileUpload('AjCollegeHostelMaster','fulCollegeHostel');" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script type="text/javascript" src="../../JS/jquery-1.5.2.min.js"></script>
    <script src="../../JS/AjexFileUploader.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ajaxFileUpload(table, fileName) {
            $("#loading")
                 .ajaxStart(function () {
                     $(this).show();
                 })
                 .ajaxComplete(function () {
                     $(this).hide();
                 });

            $.ajaxFileUpload(
                 {
                     url: 'ExcelImport.axd?tblName=' + table,
                     secureuri: false,
                     fileElementId: fileName,
                     dataType: 'json',
                     data: { name: 'logan', id: 'id' },
                     success: function (data, status) {
                         if (typeof (data.error) != 'undefined') {
                             if (data.error != '') {
                                 alert(data.error);
                             } else {
                                 alert(data.msg);
                             }
                         }
                     },
                     error: function (data, status, e) {
                         alert(e);
                     }
                 }
             )

            return false;

        }

    </script>
</asp:Content>
