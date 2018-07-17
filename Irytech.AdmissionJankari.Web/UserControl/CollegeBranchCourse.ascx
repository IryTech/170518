<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeBranchCourse.ascx.cs"  Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeBranchCourse" %>
<script src="/AdminPanel/JS/CollegeBranch.js" type="text/javascript"></script>
<input type="hidden" id="hdnArray"/><input type="hidden" id="collegeId" value="0"/><input type="hidden" id="hdnExamIndex" />
<div id="divOuter">
    <div id="divBranchCourse">
        <asp:HiddenField runat="server" ID="hdnObjectArray">
        </asp:HiddenField>
        <fieldset>
            <legend>Course</legend>
            <ul>
                <li>
                    <label>
                        Course</label>
                    <select  id="ddlCourse0" onchange="showStreamPop(0)" tabindex="1" title="Please Select Course"></select>
                    <input type="hidden" id="hdnStreamDynamic0" />
                             
                </li>
                <li>
                    <label>
                        University</label>
                    <select id="ddlUniversity0"  tabindex="2" title="Please Select University"></select>
                    <input type="hidden" id="hdnUniversityName"/>
                             
                </li>
         <li>
                    <label>
                        Title</label>
                    <input id="txtCourseTitle0" type="text" tabindex="3" title="Please Enter Course Meta Tag" />
                </li>
                <li>
                    <label>
                        Meta Tag</label>
                    <input id="txtCourseMetaTag0" type="text" tabindex="3" title="Please Enter Course Meta Tag" />
                </li><li>
                         <label>
                             Url</label>
                         <input id="txtCourseUrl0" type="text" tabindex="4" title="Please Enter Course Url" />            
                     </li>
                <li>
                    <label>
                        Meta Desc</label>
            
                    <input id="txtCourseMetaDesc0" type="text" tabindex="5" title="Please Enter Course Meta Tag" />        
               
                </li>
                <li >
                    <label>Establishment</label>
                    <input id="txtCourseEst0" type="text" tabindex="6" title="Please Enter College Popular Name" />
                </li>
                <li>
                    <label>Status</label>
                    <input type="checkbox" id="chkCourseStatus0" tabindex="7" title="Please Check Status" />       
                </li>
                <li >
                    <label>Description</label>
                    <textarea ID="txtCourseDesc0" tabindex="8" title="Please Enter College Popular Name"></textarea>
                </li>
            </ul>
        </fieldset></div>
      
  <div id="divCollegeFacility">

<fieldset>
    <legend>Facality</legend>
    <ul>
        <li>
            <label>
                Facality</label>
                <input type="text" id="txtCollegeFacality0"  tabindex="1" title="Please Enter College Facality"/>
                          
        </li>
        <li>
            <label>
               Status</label>
           <input type="checkbox" id="chkCollegeCourseFacality0" title="Please Select Status" tabindex="2"/>       
        </li>  <li>
          <label>
                Description</label>
               
             <input  type="text" id="txtCollegeFacalityDescription0"  tabindex="3" title="Please Enter College Facality"/>                 
        </li>
       
      </ul>
</fieldset></div>
<div id="divCollegeBranchHighLights">

<fieldset>
    <legend>HighLights</legend>
    <ul>
        <li>
            <label>
                HighLights</label> <input type="text" id="txtCollegeHighLights0"  tabindex="1" title="Please Enter College HighLights"/>
                            
        </li>
        <li>
            <label>
               Status</label>
           <input type="checkbox" id="chkCollegeHighLightStatus0" title="Please Select Status" tabindex="2"/>      
        </li>
       
      </ul>
</fieldset></div>
   <div id="divCollegeBranchRankSource">
<fieldset>
    <legend>Rank Source</legend>
    <ul>
        <li>
            <label>
                Rank Source</label>
          
           <select id="ddlCollegeRankSource0" tabindex="1" title="Please Select College Rank Source" ></select>    
                <input type="hidden" id="hdnRankSource0"  />                
        </li>
        <li>
            <label>
               Source Year</label>
                <input  type="text" id="txtCollegeRankSourceYear0"  tabindex="2" title="Please Enter Source Year"/>   
                            
        </li>
        <li>
            <label>
          Rank Overall</label>
          
           <input  type="text" id="txtRankOverall0"  tabindex="3" title="Please Enter Rank OverAll"/>                         
        </li>

                <li>
            <label>Status</label>
                  <input type="checkbox" id="chkCollegeBranchRankSourceStatus0" title="Please Select Status" tabindex="4"/>          
        </li>
          </ul>
</fieldset></div>
<div id="divCollegeHostel">

<fieldset>
    <legend>Hostel</legend>
    <ul>
        <li>
            <label>
                Hostel Category</label> 
         <select id="ddlCollegeHostelCategory0" tabindex="1" title="Please Select Hostel Category" ></select>    
                <input type="hidden" id="hdnHostelCategory0"  />                
        </li>
       
         <li>
          <label>
                Location</label>
                 <input  type="text" id="txtCollegeHostelLocation0"  tabindex="2" title="Please Enter College Location"/>   
                             
        </li>
        <li>
          <label>
                Internet</label>
                <input type="radio" name="InterNet0" id="rbtHostelInternetYes0" value="0" checked title="Please Select Internet"/><label>Yes
                </label>
          <input type="radio" name="InterNet0" id="rbtHostelInternetNo0" value="1"  title="Please Select Internet"/>No
                          
        </li>
        <li>
          <label>
                Laundry</label>
                 <input type="radio" name="Loundary0" id="rbtLoundaryYes0" value="0" checked title="Please Select Yes"/><label>Yes
                </label>
          <input type="radio" name="Loundary0" id="rbtLoundaryNo0" value="1"  title="Please Select No"/>No
          
                          
        </li>
         <li>
          <label>
                AC</label>
                 <input type="radio" name="AC0" id="rbtACYes0" value="0" checked title="Please Select Yes"/><label>Yes
                </label> 
          <input type="radio" name="AC0" id="rbtACNo0" value="1"  title="Please Select No"/>No
           
                          
        </li>
         <li>
          <label>
                Power Backup</label>
                
           <input type="radio" name="Power0" id="rbtPowerYes0" value="0" checked title="Please Select Yes"/><label>Yes
                </label>
          <input type="radio" name="Power0" id="rbtPowerNo0" value="1"  title="Please Select No"/>No            
        </li>
        <li>
          <label>
              Charge</label>
                  <input  type="text" id="txtHostelCharge0"  tabindex="7" title="lease Enter Charge"/>   
        </li>
         <li>
            <label>
               Status</label>
             <input type="checkbox" id="chkHostelStatus0" title="Please Select Status" tabindex="4"/>        
        </li>
       
      </ul>
</fieldset></div>
   
    <asp:HiddenField runat="server" ID="hdnCourseIndex" Value="1">
    </asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnStreamIndex" Value="1">
    </asp:HiddenField>
    <input type="hidden" id="hdnCourseValue" value="1">
    <input type="hidden" id="hdnStreamMgt" >
  <div id="courseContainer">
    </div>
    <div id="streamContainer"   >
    </div> <div id="fade" class="fade"></div> <div id="courseExamContainer">
                      </div><div id="examPopUpContainer" style='display:none;width: 520px' class='popup_block'>
                      <a href='#' style='float:right;color:red;' id='examClose' class='close' onclick='closeExamPopUp();return false;'>X</a><fieldset><legend>Exam</legend><ul><li><label> Exam</label><select id="ddlCollegeExam" tabindex='1' title='Please select Exam'></select>  </li><li><label>Status</label><input type='checkbox' id="examStatus" tabindex='2' title='Please Check'/></li><li><label></label><input id='btnCourseStreamDone' class='close' type='button' onclick='SetExamValues()' value='Next'/></li></ul></fieldset></div>
                           
                            <div id="divCourseBranchFacailty">
                      </div>
                      <div id="divCourseHighLights">
                      </div> <div id="divCourseRankSourceDyn">
                      </div><div id="divHostelCource">
                      </div> 
</div><fieldset><ul><li> <input  style="float: right" type="button"  onclick="insertCourse();return false;" value="Add"><div style="float:right">
                      <input  style="float: right" type="button"  onclick="AddFields();return false;" value="AddMore"></input></div></input> <span id="progress" style="display: none">
                          <img src="../Images/CommonImages/loading.gif" />
                          Please Wait
                      </span</li></ul></fieldset>
                      <style>.fade
                          {
                              z-index:9999999;
                          }</style>
<script src="../AdminPanel/JS/CollegeBranch.js" type="text/javascript"></script>
<script type="text/javascript">
    var courseUrl = "../../WebServices/CommonWebServices.asmx/GetAllCourseList";
    var universityUrl = "../../WebServices/CommonWebServices.asmx/GetUniversityList";
    BindDropDown($("#ddlCourse0"), courseUrl);
    BindDropDown($("#ddlUniversity0"), universityUrl);
    var courseListUrl = "../../WebServices/CommonWebServices.asmx/GetAllCourseList";
    BindDropDown($("#ddlCollegeExamCourse0"), courseListUrl);
    var rankSourceUrl = "../../WebServices/CommonWebServices.asmx/GetRankSourcelist";
    BindDropDown($("#ddlCollegeRankSource0"), rankSourceUrl);
    var hostelUrl = "../../WebServices/CommonWebServices.asmx/GetCollegeHostel";
    BindDropDown($("#ddlCollegeHostelCategory0"), hostelUrl);
    var collegeInterNet = $("table.rbtInternet input:radio");
    var collegeAc = $("table.rbtAC input:radio");
    var collegeLoundary = $("table.rbtLoundary input:radio");
    var courseStream = $("#ddlCourse0");
    var university = $("#ddlUniversity0");
    var courseMetaTag = $("#txtCourseMetaTag0");
    var courseUrl = $("#txtCourseUrl0");
    var courseMetaDesc = $("#txtCourseMetaDesc0");
    var courseEst = $("#txtCourseEst0");
    var courseDescription = $("#txtCourseDesc0"); var chkCollegeFacalityStatus = false;
    var chkCollegeHostelStatus = false;
    var chkCollegeRankSourceStatus = false;
    var chkCollegeHighLightStatus = false;
    var $radInterNetChecked;
    var $radAcChecked;
    var $radLoundaryChecked;
    collegeInterNet.click(function () {
        radInterNetChecked = $(':radio:checked');
        $("#hdnInternet").val(radInterNetChecked.val());
    });
    collegeAc.click(function () {
        radAcChecked = $(':radio:checked');
        $("#hdnAc").val(radAcChecked.val());
    });
    collegeLoundary.click(function () {
        radLoundaryChecked = $(':radio:checked');
        $("#hdnLoundary").val(radLoundaryChecked.val());
    });
    function validateCourse() {
        var courseLength = $("#hdnCourseValue").val();
        for (var i = 0; i < courseLength; i++) {

            if ($("#ddlCourse" + i).val() <= 0) {
                $("#ddlCourse" + i).focus();
                alert("Please Select Course");
                return false;
            }
            //               }else if ($("#ddlUniversity" + i).val()<=0) {
            //                     $("#ddlUniversity" + i).focus();
            //                   alert("Please Select University");
            //                   return false;
            //               }  
            else if (!numericNo.test($("#txtCourseEst" + i).val().trim())) {
                $("#txtCourseEst" + i).focus();
                alert('Please Enter Numeric Course Establishment'); return false;
            }
            else if (!numericNo.test($("#txtCollegeRankSourceYear" + i).val().trim())) {
                $("#txtCollegeRankSourceYear" + i).focus();
                alert('Please Enter Rank Source Year In Numeric'); return false;
            }
            else if ($("#ddlCollegeHostelCategory" + i).val() <= 0) {
                $("#ddlCollegeHostelCategory" + i).focus();
                alert('Please Enter Hostel Category'); return false;
            }
            else if (!numericNo.test($("#txtHostelCharge" + i).val().trim())) {
                $("#txtHostelCharge" + i).focus();
                alert('Please Enter Numeric Hostel Charge '); return false;
            }

        }
        return true;
    }
    function insertCourse() {
        alert($("#collegeId").val());
        if ($("#collegeId").val() > 0) {
         
            if (validateCourse()) {
                insertCourseDetails($("#collegeId").val());
                return true;
            }
        }
        else {
            alert("Please Submit College Module First");
            return false;
        }
    }
</script>