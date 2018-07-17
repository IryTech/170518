<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Account.UserProfile" %>

<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="~/UserControl/StudentExamAppeared.ascx" TagName="ExamAppeared" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/StudentCollegePreffered.ascx" TagName="StudentCollegePreffered" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/StudentCoursePreffered.ascx" TagName="StudentCoursePreffered" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CourseStreamPreffered.ascx" TagName="CourseStreamRelated" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CollegeCityPreference.ascx" TagName="CityPrefered" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/StudentQuery.ascx" TagName="StudentQuery" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/StudentAccademicInfo.ascx" TagName="StudentAccademicInfo" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/UcUserFinalList.ascx" TagName="UserFinalList" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/UcStudentPaymentOption.ascx" TagName="StudentPaymentOption" TagPrefix="Aj" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
    <input type="hidden" id="hdnFinalCourse" value="0" />
     <div id="fade"></div>
     <div id="divImage" class="loading" >
<img src="/image.axd?Common=Loading.gif"  />
 
 </div>  
    <div class="box1">
    <input id="hdnUserCourseId" type="hidden" />
    <asp:UpdatePanel ID="updateCollegeList" runat="server">
        <ContentTemplate>

        <div id="tabs1" class="tabbed_area">
            <ul class="tabs" id="ulTopRanked">
                <li rel="tab1" class="active"><a href="javascript:void(0)" class="cursor">My Profile</a></li>
                <li rel="tab2" id="liCounsulling" ><a href="javascript:void(0)" class="cursor">Online Counseling</a></li>
                <li rel="QueryTab" onclick="GetStudentQuery(1)"><a href="javascript:void(0)" class="cursor">Query</a></li>
            </ul>
        </div>

        <div class="tab_container ">
            <div class="tab_content" id="tab1">
                <h3 class="italic">
                    Welcome :
                    <%=Common.GetStringProperCase(Session["LoginUserName"].ToString())%></h3>
                <div class="boxPlane" style="padding:0px; margin-bottom:8px; padding-bottom:8px;">
                    <div id="Div1" class="tabbed_area">
                        <ul class="tabs" id="ulPrivate">
                            <li rel="tab3" class="active"><a href="javascript:void(0)">Personal Info</a></li>
                            <li rel="tab4" onclick="GetAccedemicInfo();return false;"><a href="javascript:void(0)" >Academic
                                Info</a></li>
                            <li rel="tab5" onclick=" GetExamAppearedForStudent();return false;"><a href="javascript:void(0)" >
                                Exam Appeared</a></li>
                            <li rel="tab6" onclick="GetStudentCollegePreference();return false;"><a href="javascript:void(0)" >
                                College Preferences</a></li>
                            <li rel="tab7" onclick="GetLoginDetails();return false;"><a href="javascript:void(0)" >
                                Course Preferences</a></li>
                            <li rel="tab8" onclick="GetCourseStreamPreferedByStudent();return false;"><a href="javascript:void(0)" >
                                Stream Preferences</a></li>
                            <li rel="tab9" onclick="GetCityPreferenceByStudent();return false;"><a href="javascript:void(0)" >
                                City Preferences</a></li>
                            <li rel="ChGPwd"><a href="javascript:void(0)" onclick="clearpwdfield()">Change Password</a></li>
                        </ul>
                    </div>
                    <input type="hidden" id="hdnCountry" />
                    <input type="hidden" id="hdnState" />
                    <input type="hidden" id="hdnCity" />
                    <div class="tab_container">
                        <div class="tab_content1" id="tab3">
                        <div class="prohead"><strong class="RedrightImglink">Personal Info</strong></div>
                             <ul class="horizontal marginTop fleft width20Percent">
                             <li id="divFile">
                             <center>
                                     <img id="userImage" width="180px" height="235px" alt="User_Pics" /><br />

                                        <strong><a href="#" class="aColor"  id="sndImage" onclick="OpenPoup('divUserImage',440,'sndImage');return false;">Change Image</a></strong></center>
                                        
                                       <div class="popup_block" id="divUserImage">
                                     
                                                 <Ajex:AsyncFileUpload ID="AsyncFileUpload1" runat="server"  UploadingBackColor="#CCFFFF"
                                                 OnUploadedComplete="ProcessUpload" OnClientUploadComplete="showUploadConfirmation" ThrobberID="spanUploading"  />
                                                <span id="spanUploading" runat="server">
                                                    
                                                      <img src='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot%>image.axd?Common=LoadingImage.gif' alt="Loading"
                                                             />
                                                </span>

                                            <br />
                                                 
                                         </div>
                                   
                              </li>
                            </ul>
                            <ul class="horizontal marginTop fleft width70Percent">
                             
                                <li>
                                    <label class="strongDetails">
                               <%=Resources.label.Name %></label>
                                    <label id="lblFullName" class="show Profiledis ">
                                    </label>
                                    <input type="text" id="txtFullName" title="Please Enter Your Name To Change" maxlength="45" class="hide width20Percent" />
                                    <a href="#" id="linkFullNameEdit" onclick="openFullName('#txtFullName','#lblFullName','#linkFullNameEdit','#linkFullNameUpdate','#lnkNameCancel');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="linkFullNameUpdate" onclick="Update('#txtFullName','#lblFullName','#linkFullNameEdit','#linkFullNameUpdate','#lnkNameCancel','AjUserFullName');return false;" class="hide profileanchor">Update</a> 
                                    <a href="#" id="lnkNameCancel" onclick="Cancel('#txtFullName','#lblFullName','#linkFullNameEdit','#linkFullNameUpdate','#lnkNameCancel');return false;" class="hide profileanchor">Cancel</a> </li>
                                <li>
                                    <label class="strongDetails">
                                        Gender:</label>
                                    <label id="lblGender" class="show Profiledis ">
                                    </label>
                                    <asp:DropDownList runat="server" ID="rbtGender" class="hide width20Percent ">
                                    </asp:DropDownList>
                                    <a href="#" id="lnkGenderEdit" onclick="openGender('gender','#lnkGenderEdit','#lnkUpdateGender','#lnkCancelGender');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="lnkUpdateGender" onclick="UpdateGender('gender','#lnkGenderEdit','#lnkUpdateGender','#lnkCancelGender','AjUserGender');return false;" class="hide profileanchor">Update</a>
                                    <a href="#" id="lnkCancelGender" onclick="CancelGender('gender','#lnkGenderEdit','#lnkUpdateGender','#lnkCancelGender');return false;" class="hide profileanchor">Cancel</a> </li>
                                <li>
                                    <label class="strongDetails">
                                        <%=Resources.label.Email %></label>
                                    <label id="lblEmailId" class="show Profiledis">
                                    </label>
                                </li>
                                <li>
                                    <label class="strongDetails">
                                        <%=Resources.label.Mobile %></label>
                                    <label id="lblMobileNumber" class="show Profiledis">
                                    </label>
                                    <input type="text" id="txtMobileNumber" title="Please Enter Your Number To Change" class="hide width20Percent" />
                                    <a href="#" id="lnkEditMoNo" onclick="openFullName('#txtMobileNumber','#lblMobileNumber','#lnkEditMoNo','#lnkUpdateMoNo','#lnkCancelMoNo');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="lnkUpdateMoNo" onclick="Update('#txtMobileNumber','#lblMobileNumber','#lnkEditMoNo','#lnkUpdateMoNo','#lnkCancelMoNo','AjUserMobile');return false;" class="hide profileanchor">Update</a> 
                                    <a href="#" id="lnkCancelMoNo" onclick="Cancel('#txtMobileNumber','#lblMobileNumber','#lnkEditMoNo','#lnkUpdateMoNo','#lnkCancelMoNo');return false;" class="hide profileanchor">Cancel</a> </li>
                                <li>
                                    <label class="strongDetails">
                                       <%=Resources.label.DOB %></label>
                                    <label id="lblDob" class="show Profiledis">
                                    </label>

                                    <asp:TextBox ID="txtDOB" runat="server" title="Please Enter Your Date of Birth To Change" CssClass="hide width20Percent" />
                                    <a href="#" id="lnkDob" onclick="openDobName('#<%=txtDOB.ClientID %>','#lblDob','#lnkDob','#lnkDobUpdate','#lnkDobCancel');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="lnkDobUpdate" onclick="Update('#<%=txtDOB.ClientID %>','#lblDob','#lnkDob','#lnkDobUpdate','#lnkDobCancel','AjUserDOB');return false;" class="hide profileanchor">Update</a> 
                                    <a href="#" id="lnkDobCancel" onclick="Cancel('#<%=txtDOB.ClientID %>','#lblDob','#lnkDob','#lnkDobUpdate','#lnkDobCancel');return false;" class="hide profileanchor">Cancel</a> 
                                     <span class="hide" id="spnDob">  <asp:Image ID="imgCal" runat="server" Height="10px" Width="10px" ImageUrl="/image.axd?Common=Calendar-icon.png" />
                                 </span>
                                    <Ajex:CalendarExtender BehaviorID="calendar1" ID="txtDOB_CalendarExtender" PopupPosition="Right" Format="dd/MM/yyyy" runat="server" CssClass="CalendarCSS" TargetControlID="txtDOB" PopupButtonID="imgCal">
                                    </Ajex:CalendarExtender>
                                    </li>
                                   
                                <li>
                                    <label class="strongDetails">
                                        Phone:</label>
                                    <label id="lblPhoneNumber" class="show Profiledis">
                                    </label>
                                    <input type="text" id="txtPhoneNumber" title="Please Enter Your Number To Change" class="hide width20Percent" />
                                    <a href="#" id="lnkPhoneNumberEdit" onclick="openFullName('#txtPhoneNumber','#lblPhoneNumber','#lnkPhoneNumberEdit','#lnkUpdatePhone','#lnkCancelPhone');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="lnkUpdatePhone" onclick="Update('#txtPhoneNumber','#lblPhoneNumber','#lnkPhoneNumberEdit','#lnkUpdatePhone','#lnkCancelPhone','AjUserPhoneNo');return false;" class="hide profileanchor">Update</a> 
                                    <a href="#" id="lnkCancelPhone" onclick="Cancel('#txtPhoneNumber','#lblPhoneNumber','#lnkPhoneNumberEdit','#lnkUpdatePhone','#lnkCancelPhone');return false;" class="hide profileanchor">Cancel</a> </li>
                                <li>
                                    <label class="strongDetails">
                                        Pin Code:</label>
                                    <label id="lblPinCode" class="show Profiledis">
                                    </label>
                                    <input type="text" id="txtPinCode" title="Please Enter Your Number To Change" class="hide width20Percent" />
                                    <a href="#" id="lnkPincodeEdit" onclick="openFullName('#txtPinCode','#lblPinCode','#lnkPincodeEdit','#lnkUpdatePincode','#lnkPincodeCancel');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="lnkUpdatePincode" onclick="Update('#txtPinCode','#lblPinCode','#lnkPincodeEdit','#lnkUpdatePincode','#lnkPincodeCancel','AjUserPincode');return false;" class="hide profileanchor">Update</a> 
                                    <a href="#" id="lnkPincodeCancel" onclick="Cancel('#txtPinCode','#lblPinCode','#lnkPincodeEdit','#lnkUpdatePincode','#lnkPincodeCancel');return false;" class="hide profileanchor">Cancel</a> </li>
                                <li>
                                    <label class="strongDetails">
                                        Mailing Address:</label>
                                    <label  id="lblMailingAddress" class="show Profiledis" >
                                    </label>
                                    <textarea type="text" id="txtMailingAddress" maxlength="150" rows="4" cols="5" title="Please Enter Your Number To Change" class="hide width20Percent"></textarea>
                                    <a href="#" id="lnkMailingAddressEdit" onclick="openFullName('#txtMailingAddress','#lblMailingAddress','#lnkMailingAddressEdit','#lnkUpdateMailingAddress','#lnkCancelMailingAddress');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="lnkUpdateMailingAddress" onclick="Update('#txtMailingAddress','#lblMailingAddress','#lnkMailingAddressEdit','#lnkUpdateMailingAddress','#lnkCancelMailingAddress','AjUserMailingAddress');return false;" class="hide profileanchor">Update</a> 
                                    <a href="#" id="lnkCancelMailingAddress" onclick="Cancel('#txtMailingAddress','#lblMailingAddress','#lnkMailingAddressEdit','#lnkUpdateMailingAddress','#lnkCancelMailingAddress');return false;" class="hide profileanchor">Cancel</a> </li>
                                <li>
                                    <label class="strongDetails">
                                        Permanent Address:</label>
                                    <label id="lblPermanentAddress" class="show Profiledis" >
                                    </label>
                                    <textarea type="text" id="txtPermanentAddress" maxlength="150" rows="4" cols="5" title="Please Enter Your Number To Change" class="hide width20Percent"></textarea>

                                    <a href="#" id="lnkPrmEdit" onclick="openFullName('#txtPermanentAddress','#lblPermanentAddress','#lnkPrmEdit','#lnkPrmUpdate','#lnkCnclPrm');return false;" class="show profileanchor">Edit</a>
                                     <a href="#" id="lnkPrmUpdate" onclick="Update('#txtPermanentAddress','#lblPermanentAddress','#lnkPrmEdit','#lnkPrmUpdate','#lnkCnclPrm','AjUserPermanentAddress');return false;" class="hide profileanchor">Update</a> 
                                     <a href="#" id="lnkCnclPrm" onclick="Cancel('#txtPermanentAddress','#lblPermanentAddress','#lnkPrmEdit','#lnkPrmUpdate','#lnkCnclPrm');return false;" class="hide profileanchor">Cancel</a> </li>
                                <li>
                                    <label class="strongDetails">
                                        Country:</label>
                                    <label id="lblCountry" class="show Profiledis">
                                    </label>
                                    <select id="slctCountry"  onchange="GetState()" class="hide width20Percent">
                                    </select>
                                </li>
                                <li>
                                    <label class="strongDetails">
                                        State:</label>
                                    <label id="lblState" class="show Profiledis">
                                    </label>
                                    <select id="slctState" onchange="GetCity()" class="hide width20Percent">
                                    </select>
                                </li>
                                <li>
                                    <label class="strongDetails">
                                        City:</label>
                                    <label id="lblCity" class="show Profiledis">
                                    </label>
                                    <select id="slctCity" class="hide width20Percent">
                                    </select>
                                    <a href="#" id="lnkPrmCityEdit" onclick="openCityEdit('#slctCity','#lnkPrmCityEdit','#lnkPrmCityUpdate','#lnkCityCancel');return false;" class="show profileanchor">Edit</a> 
                                    <a href="#" id="lnkPrmCityUpdate" onclick="UpdateCity('#slctCity','#lnkPrmCityEdit','#lnkPrmCityUpdate','#lnkCityCancel');return false;" class="hide profileanchor">Update</a> 
                                    <a href="#" id="lnkCityCancel" onclick="CancelCity('#slctCity','#lnkPrmCityEdit','#lnkPrmCityUpdate','#lnkCityCancel');return false;" class="hide profileanchor">Cancel</a> </li>
                            
                          
                            
                            </ul>
                        </div>

                        <div class="tab_content1" id="tab4">
                            <Aj:StudentAccademicInfo runat="server" ID="StudentAccademicInfo" />
                        </div>
                        <div class="tab_content1" id="tab5">
                            <Aj:ExamAppeared runat="server" ID="AjExamAppeared" />
                        </div>
                        <div class="tab_content1" id="tab6">
                            <Aj:StudentCollegePreffered runat="server" ID="AJStudentCollegePreffered" />
                        </div>
                        <div class="tab_content1" id="tab7">
                            <Aj:StudentCoursePreffered runat="server" ID="AJStudentCoursePreffered" />
                        </div>
                        <div class="tab_content1" id="tab8">
                            <Aj:CourseStreamRelated runat="server" ID="CourseStreamRelatedPrefer" />
                        </div>
                        <div class="tab_content1" id="tab9">
                            <Aj:CityPrefered runat="server" ID="ucCityPrefered" />
                        </div>
                        <div class="tab_content1" id="ChGPwd">
                            <span class="spnSuccess "></span>
                            <fieldset>
                                <legend>Change Password</legend>
                                <ul>
                                    <li>
                                        <label>
                                            <%=Resources.label.Email%></label>
                                        <input type="text" tabindex="1" id="txtEmailIdPwd"  disabled="disabled" /> </li>
                                        
                                        <li>
                                            <label>
                                                <%=Resources.label.NewPassword%></label>
                                            <input type="password" tabindex="3" id="txtNewPassword" title="Enter new password" placeholder="Enter new password" />
                                        </li>
                                        
                                        <li>
                                            <label>
                                                <%=Resources.label.CnfPassword%>:</label>
                                            <input type="password" tabindex="4" id="txtConfirmPassword" title="Enter confirm password" onchange="CheckPassWord()" placeholder="Enter confirm password" />
                                        </li>
                                        <li>
                                            <label>
                                            </label>
                                            <input type="button" id="btnChangePwd" tabindex='5' value="Update" onclick="ChangePassword()" title="Click to finish to change password" />
                                        </li>
                                </ul>
                            </fieldset>
                        </div>
                    </div>
                    <span style="display: none" id="progress">
                        <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" /> 
                    </span>
                <div class="clearBoth"></div>
                </div>
            </div>
            
             <%--<div class="prohead"><strong class="RedrightImglink">Counsulling</strong></div>--%>
             <div class=" tab_container">
             <div class="tab_content" id="tab2">
                 
             <div id="spnucUserFinalList" class="hide">
                <Aj:UserFinalList ID="ucUserFinalList" runat="server" /></div>

             <div id="spnStartCounsulling" class="hide">
                 <a id="btnStartCounsulling" href="/counselling/ChooseCollege.aspx"
                class="greenbutton fleft" title="Please Start Counsulllling">Start counseling</a> 
                </div>

             <div id="spnCounsullingCollege" class="hide">             
                    <Aj:StudentPaymentOption runat="server" ID="ucStudentPaymentOption" />
                </div>

            <div id="spnCounsellingNoData" class="hide"  >
                <a id="sndDirectAdmission" href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+IryTech.AdmissionJankari.Components.Utils.RemoveIllegealFromCourse((new Common().CourseName))+"/get-direct-admission"%>'
                class="greenbutton fleft" title="Please Start Counseling">Apply Counseling</a> 

            </div>
                </div>
                <div class="clearBoth"></div>
                </div>
        </div>
        <div class="tab_container">
            <div class="tab_content" id="QueryTab">
                
                <div class="prohead"><strong class="RedrightImglink">
                    Your Query's</strong></div>
                 <div class="boxPlane" style="padding:0px; margin-bottom:8px; padding-bottom:8px;">  
                <div class="tabbed_area">

                    <ul class="tabs" id="ulQuery">
                        <li rel="tabAllQuery" class="active"  onclick=" GetStudentQuery(1);return false;"><a href=" javascript:void(0)"
                            >All Query</a></li>
                        <li rel="LastQuery"  onclick="GetLastQuery();return false;"><a href="javascript:void(0)">Last</a></li>
                        <li rel="answered" onclick="GetAnsweredQuery(1);return false;"><a href="javascript:void(0)" >Answered</a></li>
                        <li rel="unanswered" onclick="GetUnAnsweredQuery(1);return false;"><a href="javascript:void(0)">
                            Unanswered</a></li>
                    </ul>
                </div>
                <div class="tab_container">
                    <span style="display: none" id="progress">
                        <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" />
                    </span>
                    <div class="tab_content2" id="tabAllQuery">
                        <Aj:StudentQuery runat="server" ID="StudentQuery" />
                    </div>
                    <div class="tab_content2" id="LastQuery">
                        <div id="lastQuery">
                            <label id="nolastQuery" class="hide">
                            </label>
                        </div>
                    </div>
                    <div class="tab_content2" id="answered">
                        <div id="answeredQuery">
                            <label id="lblansweredQuery" class="hide">
                            </label>
                        </div>
                         <div id="AnsweredPager" class="AnsweredPager pagination">
                        </div>
                    </div>
                    <div class="tab_content2" id="unanswered">
                        <div id="unAnsweredQuery">
                            <label id="lblunAnsweredQuery" class="hide">
                            </label>
                        </div>
                        <div id="unAnsweredPager" class="unAnsweredPager pagination">
                        </div>
                    </div>
                </div>
                <div class="clearBoth"></div>
                </div> 
            </div>
        </div>
        <div class="clearBoth">
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    
    <script type="text/javascript">
        GetTransactionDetails();
        function GetTransactionDetails() {
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/GetStudentTransationDetails",
                data: "{}",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    GetCounsullingStatus(response);

                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });

        }
        function GetCounsullingStatus(data) {

            if (data.d.length > 0) {
                $("#spnCounsellingNoData").addClass("hide");
                $("#liCounsulling").removeClass("hide");

                if (data.d[0].StudentPaymentStatus == true) {

                    if (data.d[0].CollegeBranchCourseId == "" || data.d[0].CollegeBranchCourseId == null) {

                        $("#spnStartCounsulling").removeClass("hide");
                    } else {

                        $("#spnucUserFinalList").removeClass("hide");

                    }

                } else if (data.d[0].StudentPaymentStatus == false) {
                    if (data.d[0].PaymentAmount == "1100") {
                        $("#spnCounsullingCollege").removeClass("hide");
                    } else if (data.d[0].PaymentAmount == "26100") {
                        if (data.d[0].CollegeBranchCourseId == "" || data.d[0].CollegeBranchCourseId == null) {

                            $("#spnStartCounsulling").removeClass("hide");
                        } else {
                            $("#spnucUserFinalList").removeClass("hide");
                        }
                    }

                }
            } else {
                $("#spnCounsellingNoData").removeClass("hide");
            } 
        }
  
    </script>
   
    <script src="/Js/Profile.js" type="text/javascript"></script>
    <style>
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.50;
        }
        .pnl
        {
            background: gray;
            padding: 10px;
            border: 2px solid #ddd;
            float: left;
            font-size: 1.2em;
            color: red;
            position: fixed;
            top: 50%;
            left: 50%;
            z-index: 99999;
            box-shadow: 0px 0px 20px #999; /* CSS3 */
            -moz-box-shadow: 0px 0px 20px #999; /* Firefox */
            -webkit-box-shadow: 0px 0px 20px #999; /* Safari, Chrome */
            border-radius: 3px 3px 3px 3px;
            -moz-border-radius: 3px; /* Firefox */
            -webkit-border-radius: 3px; /* Safari, Chrome */
        }
        .close
        {
            display: block;
            background: url(Images/close.png) no-repeat 0px 0px;
            left: -12px;
            width: 26px;
            text-indent: -1000em;
            position: absolute;
            top: -12px;
            height: 26px;
        }
    </style>
<script type = "text/javascript">

    function Success() {
        document.getElementById("lblMessage").innerHTML = "File Uploaded";

    }

    function Error() {
        document.getElementById("lblMessage").innerHTML = "Upload failed.";
    }

    function showUploadConfirmation() {
        $("#divUserImage").hide();
        $("#fade").hide();
        var uploadText = document.getElementById('<%=AsyncFileUpload1.ClientID %>').getElementsByTagName("input");
        for (var i = 0; i < uploadText.length; i++) {
            if (uploadText[i].type == 'file') {
                uploadText[i].value = '';
            }
        }
        GetLoginDetails();
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
        

</script>
</asp:content>
