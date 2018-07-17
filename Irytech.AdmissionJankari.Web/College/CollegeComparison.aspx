<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeComparison.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.College.CollegeComparison" MasterPageFile="~/themes/Blue/Site.Master" %>

<%@ Register Src="~/UserControl/UcRegistration.ascx" TagPrefix="ADMJ" TagName="Registration" %>
<%@ Register Src="~/UserControl/CollegeAvailableCourse.ascx" TagPrefix="AJ" TagName="CollegeBasicCourse" %>
<%@ Register Src="~/UserControl/CollegeAvailabelCourseExam.ascx" TagPrefix="AJ" TagName="CollegeCourseExam" %>
<%@ Register Src="~/UserControl/CollegeAvailableHostel.ascx" TagPrefix="AJ" TagName="CollegeCourseHostel" %>
<%@ Register Src="~/UserControl/CollegeAvailableRank.ascx" TagPrefix="AJ" TagName="CollegeAvailableRank" %>
<%@ Register Src="~/UserControl/RightBanner.ascx" TagPrefix="AJ" TagName="RightBanner" %>
<%@ Register Src="~/UserControl/CollegeAvailablePlaceMent.ascx" TagPrefix="AJ" TagName="CollegeAvailablePlaceMent" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hndCourseId" runat="server" />
            <asp:HiddenField ID="hdnCoursePopularName" runat="server" />
            <asp:HiddenField ID="hdnCollegeBranchName1" runat="server" />
            <asp:HiddenField ID="hdnCollegeBranchCourseid1" runat="server" />
            <asp:HiddenField ID="hdnCollegeBranchCourseid2" runat="server" />
            <asp:HiddenField ID="hdnCityId1" runat="server" />
            <asp:HiddenField ID="hdnCityId2" runat="server" />
            <asp:HiddenField ID="hdnCollegeBranchName2" runat="server" />
            <div class="five_sixth fleft last">
                <h1>
                    College Comparison</h1>
                    <hr class="hrline" />
                <p>
                    Choose at least two Colleges to Compare of your choice to see how they compare on Courses, Seats, Fee, Rank and placement. You have a choice of comparing new Colleges in India.
                </p>
                <div class="box1 marginTop1">
                    <h3 class="streamCompareH3">
                        Search colleges to compare
                        <label id="lblShowCollege" style="font-size: 15px; color: Maroon;">
                        </label>
                    </h3>
                    <hr class="hrline" />
                    <fieldset class="boxBody">
                        <ul class="vertical width100Percent">
                            <li style="font-size: 12px; width:120px; padding-left:35px;"><strong id="lblCourse" style="font-size:15px; font-weight:600;">Change course </strong></li>
                            <li class="width75Percent">
                                <select id="ddlCourse" title="Select Course">
                                </select>
                                <label id="lblCollegeError" class=" hide error" title="Please Select College">
                                </label>
                            </li>
                        </ul>

                        <div class="campareDiv">
                            <span style=" float:left; border-right:1px dotted gray; padding-right:20px; width:30%; padding-bottom:10px;">
                            <strong>First College</strong>
                            
                            <asp:TextBox ID="txtFirstCollegeName" Wrap="false" Width="80%" runat="server"></asp:TextBox>
                            </span>

                            <span style="float:left; width:32%; padding-left:50px; padding-bottom:10px;">
                            <strong>Second College</strong>
                            
                            <asp:TextBox ID="txtSecondCollegeName"  Wrap="false" Width="80%" runat="server"></asp:TextBox>
                            </span>
                            <div class="clearBoth"></div>
                        </div>
                        <center class="clearBoth width70Percent" style="border-top:1px dotted gray; padding-top:10px; margin-left:10px;">
                            <asp:Button ID="btnCompare" runat="server" Text="Compare" class="button" OnClientClick="return CheckCollegeField()" ToolTip="Compare" ValidationGroup="strmCompare" OnClick="CompareCollege" />
                            <input type="button" value="Clear" title="Clear" class="button" onclick="ClearControl()" />
                        </center>
                        <div id="divImage" style="display: none">
                            <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" />
                        </div>
                    </fieldset>
                </div>
                <div class="box1" id="DivCollegeDetails" visible="false" runat="server">
                    <center>
                        <div class="width80Percent" style="margin: 0px auto;">
                            <ul class="vertical collegeCompare">
                                <li style="width:230px; display: block;">
                                    <asp:Label ID="lblCollegeName1" runat="server"></asp:Label></li>
                                <li style="width: 100px; display: block;">
                                    <img src="/image.axd?Common=vs1.png" alt="VS" /></li>
                                <li style="width: 230px; display: block;">
                                    <asp:Label ID="lblCollegeName2" runat="server"></asp:Label></li>
                            </ul>
                        </div>
                    </center>
                    <hr class="hrline" />
                    <div class="box">
                        <ul class="clgCompare">
                            <li><strong>&nbsp;</strong> <span><a id="sndImgCollg1" runat="server">
                                <asp:Image ID="imgFirstCollege" runat="server" Height="100" Width="100" hspace="5" />
                            </a></span><span><a id="sndImgCollg2" runat="server">
                                <asp:Image ID="imgSeccondCollege" runat="server" Height="100" Width="100" hspace="5" />
                            </a></span></li>
                            <li><strong>Course name</strong>
                                <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                                <asp:Label ID="lbl2CourseName" runat="server"></asp:Label></li>
                            <li><strong>Location</strong>
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                <asp:Label ID="lbl2Location" runat="server"></asp:Label></li>
                            <li><strong>Management</strong>
                                <asp:Label ID="lblManagement" runat="server"></asp:Label>
                                <asp:Label ID="lbl2Managemnt" runat="server"></asp:Label></li>
                            <li><strong>Establishment</strong>
                                <asp:Label ID="lblYOS" runat="server"></asp:Label>
                                <asp:Label ID="lbl2YOS" runat="server"></asp:Label></li>
                            <li><strong>University</strong> <span>
                                <asp:HyperLink runat="server" ID="lblUniversity">
                         
                                </asp:HyperLink></span><span>
                                    <asp:HyperLink runat="server" ID="lbl2University">
                         
                                    </asp:HyperLink></span>
                                <li><strong>Exam</strong> <span><a href="#" id="lnkFirstExam" class="smsImglink" title="Exam" onclick='OpenPoup("divFirstExam","600","lnkFirstExam");return false;'>View Details</a></span> <span><a href="#" id="lnkSeccondExam" class="smsImglink" title="Exam" onclick='OpenPoup("divSeecondExam","600","lnkSeccondExam");return false;'>View Details</a></span> </li>
                                <li><strong>Available Courses/Fees/Seats</strong> <span><a href="#" id="firstCollegeCourse" title="Stream List" class="smsImglink" onclick='OpenPoup("divFirstCollegeAviliableCourse","900","firstCollegeCourse");return false;'>View Details</a></span> <span><a href="#" id="SeccondCollegeCourse" title="Stream List" class="smsImglink" onclick='OpenPoup("divSeccondCollegeAviliableCourse","900","SeccondCollegeCourse");return false;'>View Details</a></span> </li>
                                <li><strong>Hostel</strong> <span><a href="#" id="lnkFirstCollegeHostel" title="Hostel" class="smsImglink" onclick='OpenPoup("divFirstHostel","900","lnkFirstCollegeHostel");return false;'>View Details</a></span> <span><a href="#" id="lnkSeccondCollegeHostel" title="Hostel" class="smsImglink" onclick='OpenPoup("divSeccondHostel","900","lnkSeccondCollegeHostel");return false;'>View Details</a></span> </li>
                                <li><strong>Placement</strong> <span id="spnPlacement1" runat="server"><a href="#" class="smsImglink" id="sendPlaced1" onclick='OpenPoup("divPlaced1","550","sendPlaced1");return false;' title="PlaceMent">View Details</a> </span><span id="spnPlacement2" runat="server"><a href="#" class="smsImglink" id="sendPlaced2" onclick='OpenPoup("divPlaced2","550","sendPlaced2");return false;' title="Placement">View Details</a> </span></li>
                                <li><strong>Rank</strong> <span id="spnRank1" runat="server"><a href="#" class="smsImglink" id="sndRank1" onclick='OpenPoup("divCollRank1","550","sndRank1");return false;' title="Rank">View Details</a> </span><span id="spanRank2" runat="server"><a href="#" class="smsImglink" id="sndRank2" onclick='OpenPoup("divCollRank2","550","sndRank2");return false;' title="Rank">View Details</a> </span></li>
                                <li><strong>I want a call</strong> <span id="spnCall1AfterLogin1"><a href="#" class="smsImglink" id="sendCall1" onclick='openLoginPop();return false;' title="Show interest">Login / Register. Its Free!</a> </span><span id="spnBtnInterested1" style="display: none"><a href="#">
                                    <img src="/image.axd?Common=Interstedicon.png " id="btnInterested" onclick='CollegeQuery($("#<%= hdnCollegeBranchName1.ClientID %>"),$("#<%= hdnCityId1.ClientID %>"),$("#<%= hdnCollegeBranchCourseid1.ClientID %>"));return false;' />
                                </a></span><span id="spnCall1AfterLogin"><a href="#" class="smsImglink" id="sendCall2" onclick='openLoginPop();return false;' title="Show interest">Login / Register. Its Free!</a> </span><span id="spnBtnInterested2" style="display: none"><a href="#">
                                    <img src="/image.axd?Common=Interstedicon.png " id="btnInterested1" onclick='CollegeQuery($("#<%= hdnCollegeBranchName2.ClientID %>"),$("#<%= hdnCityId2.ClientID %>"),$("#<%= hdnCollegeBranchCourseid2.ClientID %>"));return false;' />
                                </a></span>
                                    <div id="divImageCall1" style="display: none">
                                        <img src="/image.axd?Common=LoadingImage.gif" alt="Loading" />
                                    </div>
                                </li>
                                <li><strong>Replace with</strong> <span style="height: 20px;">
                                    <asp:TextBox ID="txtFirstCollegeReplace" Width="55%" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnFirstSearchReplace" CssClass="button" runat="server" Text="Replace" OnClientClick='return CheckReplaceCollege1()' ToolTip="Search" OnClick="btnFirstSearchReplace_Click" />
                                </span><span style="height: 20px;">
                                    <asp:TextBox ID="txtSecondCollegeReplace" Width="55%" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSecondSearchReplace" CssClass="button" runat="server" Text="Replace" ToolTip="Search" OnClientClick=' return CheckReplaceCollege()' OnClick="btnSecondSearchReplace_Click" />
                                </span></li>
                        </ul>
                    </div>
                </div>
            </div>
             <div class="one_sixth last fright">
                                    <AJ:RightBanner ID="ucRightBanner" runat="server" />
                                    </div>
            <div>
                <ul id="divFirstCollege" runat="server" visible="false">
                </ul>
                <ul id="divSecondCollege" runat="server" visible="false">
                    <li>
                        <asp:Label ID="lblColledgeCompersion" runat="server" Visible="false"></asp:Label></li></ul>
            </div>
            <div id="divRegister" class="popup_block">
                <div id="divPoupup" class="box">
                    <span>
                        <label id="lblerrMsg" class="hide">
                        </label>
                    </span>
                    
                    <fieldset id="fldLogin" style="border: 1px solid #fff;">
                     <h3>Login | <a href="#" onclick="showLoginAndRegister();return false;">Register here</a></h3>
                        <hr class="hrline" />
                       
                        <ul>
                            <li><strong class="liststrong">Email:</strong>
                                <input type="text" id="txtEmail" title="Enter EmailId" />
                               <label id="lblEmailError" class="error lblwidth" title="Please Enter Email Id">
                                </label>
                            </li>
                          
                            <li><strong class="liststrong">Password: </strong>
                                <input type="password"  id="txtPassword" title="Enter your Password" />    
                                 <label id="lblpasswordError" class="error lblwidth" title="Please Enter Your Password">
                                </label>                           
                            </li>
                           
                             </ul>
                            <footer style="padding-left:140px; clear:both;">
                                <span style="font-size:12px; color:Gray;"> <input type="checkbox" checked="checked" />
                                Remember</span><br />
                                <input type="button" title="Click to Login" class="button" onclick="Login()" id="btnLogin" value="Login" />
                                <input type="button" title="Clear" onclick="ClearLoginControl()" class="button" id="btnClear" value="Clear" />
                            </footer>
                       
                        
                    </fieldset>
                    <fieldset class="hide" id="fldRegister" style="border: 1px solid #fff;">
                        <h3 class="streamCompareH3">
                            New User Signup-It's free | <a href="#fldLogin" onclick="showLoginAndRegist();return false;">Login here</a></h3>
                        <hr class="hrline" />
                        <ul>
                            <li><strong class="liststrong">You are:</strong>
                                <select id="ddlUserType" title="Select UserType">
                                </select>
                                <label id="lblUserTypeError" class="error lblwidth" title="Please select UserType">
                                </label>
                            </li>
                            <li><strong class="liststrong">Name:</strong>
                                <input type="text" id="txtName" title="Enter Name" />
                                <label id="lblNameError" class="error lblwidth" title="Please enter name">
                                </label>
                            </li>
                            <li><strong class="liststrong">Mobile:</strong>
                                <input type="text" id="txtMobileNumber" title="Enter Mobile" />
                                <label id="lblNumberError" class="error lblwidth" title="Please Enter Mobile">
                                </label>
                            </li>
                            <li><strong class="liststrong">Email:</strong><input type="text" id="txtEmailId" title="Enter EmailId" />
                                <label id="lblEmailIdError" class="error lblwidth" title="Please Enter Email Id">
                                </label>
                            </li>
                        </ul>
                        <footer style="padding-left:140px; clear:both;">
            
           <span style="font-size:12px; color:Gray;"><input type="checkbox" checked="checked" /> I agree <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Terms-and-Conditions"%>' target="_blank">T&amp;C</a> and
             <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Privacy-Policy"%>' target="_blank">Privacy Policy</a></span> <br /> 
            <input type="button" title="Submit the for Registrrtion" class="button" onclick="ResisterUser()" id="butSubmit" value="Register" />
           <input type="button" title="Clear Field " id="butClear" onclick="ClearRegistationControl()" value="Clear" />
                   
        </footer>                        
                    </fieldset>
                </div>
            </div>
            <div id="divFirstCollegeAviliableCourse" class="popup_block">
                <AJ:CollegeBasicCourse ID="FirstCollegeAviliableCourse" runat="server"></AJ:CollegeBasicCourse>
            </div>
            <div id="divSeccondCollegeAviliableCourse" class="popup_block">
                <AJ:CollegeBasicCourse ID="SeccondCollegeAviliableCourse" runat="server"></AJ:CollegeBasicCourse>
            </div>
            <div id="divFirstExam" class="popup_block">
                <AJ:CollegeCourseExam ID="FirstCollegeExam" runat="server"></AJ:CollegeCourseExam>
            </div>
            <div id="divSeecondExam" class="popup_block">
                <AJ:CollegeCourseExam ID="SeccondCollegeExam" runat="server"></AJ:CollegeCourseExam>
            </div>
            <div id="divFirstHostel" class="popup_block">
                <AJ:CollegeCourseHostel ID="FirstCollegeHostel" runat="server"></AJ:CollegeCourseHostel>
            </div>
            <div id="divSeccondHostel" class="popup_block">
                <AJ:CollegeCourseHostel ID="SeccondCollegeHostel" runat="server"></AJ:CollegeCourseHostel>
            </div>
            <div id="divCollRank1" class="popup_block">
                <AJ:CollegeAvailableRank ID="CollegeAvailableRank1" runat="server"></AJ:CollegeAvailableRank>
            </div>
            <div id="divCollRank2" class="popup_block">
                <AJ:CollegeAvailableRank ID="CollegeAvailableRank2" runat="server"></AJ:CollegeAvailableRank>
            </div>
            <div id="divPlaced1" class="popup_block">
                <AJ:CollegeAvailablePlaceMent ID="CollegeAvailablePlaceMent1" runat="server"></AJ:CollegeAvailablePlaceMent>
            </div>
            <div id="divPlaced2" class="popup_block">
                <AJ:CollegeAvailablePlaceMent ID="CollegeAvailablePlaceMent2" runat="server"></AJ:CollegeAvailablePlaceMent>
            </div>
            <input type="hidden" id="hdnScolll" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" defer="defer">      
        var reEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}$/;
        function pageLoad(sender, args) {
            
            if (args.get_isPartialLoad()) {
                BindFrontCourseList($("#ddlCourse"), $("#<%= hndCourseId.ClientID %>").val());
                showCollegeDetailsWiseCollegeNameAndCourseId($("#<%=txtFirstCollegeName.ClientID %>"), $("#<%=txtSecondCollegeName.ClientID %>"), $("#<%=txtFirstCollegeReplace.ClientID %>"), $("#<%=txtSecondCollegeReplace.ClientID %>"), $("#<%= hndCourseId.ClientID %>").val());
                checkLogin();
                $("#ddlCourse").change(function () {
                    if ($("#ddlCourse").val() > 0) {
                        ChangeCourseId($("#ddlCourse").val());

                        location.href = ("/" + RemoveIlegalCharecterFromCourse($("#ddlCourse option:selected").text()) + "/compare-colleges/").toLocaleLowerCase();
                    } else {
                        $("#ddlCourse").val($("#<%= hndCourseId.ClientID %>").val());
                        alert("Select course");
                        return false;
                    }

                });
                BindFrontUserType($("#ddlUserType"));
            }
        }
        $(document).ready(function () {
            checkLogin();

            BindFrontUserType($("#ddlUserType"));

            BindFrontCourseList($("#ddlCourse"), $("#<%= hndCourseId.ClientID %>").val());
            showCollegeDetailsWiseCollegeNameAndCourseId($("#<%=txtFirstCollegeName.ClientID %>"), $("#<%=txtSecondCollegeName.ClientID %>"), $("#<%=txtFirstCollegeReplace.ClientID %>"), $("#<%=txtSecondCollegeReplace.ClientID %>"), $("#<%= hndCourseId.ClientID %>").val());
            $("#ddlCourse").change(function () {
                if ($("#ddlCourse").val() > 0) {
                    ChangeCourseId($("#ddlCourse").val());

                    location.href = ("/" + RemoveIlegalCharecterFromCourse($("#ddlCourse option:selected").text()) + "/compare-colleges/").toLocaleLowerCase();
                } else {
                    $("#ddlCourse").val($("#<%= hndCourseId.ClientID %>").val());
                    alert("Select course");
                    return false;
                }

            });


        });



        function ClearControl() {
            $('#<%=txtFirstCollegeName.ClientID %>').val('');
            $('#<%=txtSecondCollegeName.ClientID %>').val('');
            $('#<%=txtFirstCollegeReplace.ClientID %>').val('');
            $('#<%=txtSecondCollegeReplace.ClientID %>').val('');


        }

        var usernameCheckerTimer;


        function checkLogin() {
            if ('<%= Session["UID"] %>' != '') {
                $("#spnCall1AfterLogin1").hide();
                $("#spnCall1AfterLogin").hide();
                $("#spnBtnInterested1").show();
                $("#spnBtnInterested2").show();

            }
        }
        function OpenPoupLogins(Divid, Width, linkId) {
            var urls;
            if ('<%= Session["UID"] %>' == '') {

                OpenPoup(Divid, Width, linkId);

            }

        }


        function Login() {
            var isErrorRegis = false;

            if (!ValidateLoginForm()) {

                isErrorRegis = true;
            }
            if (isErrorRegis) {

                return false;

            } else {
                ValidateLogin();
            }

        }


        function ValidateLoginForm() {
            var isErrorRegis = false;
            var emailId = $("#txtEmail");
            var password = $("#txtPassword");
            if (emailId.val() == "") {
                $("#lblEmailError").removeClass("hide");
                $("#lblEmailError").text("Error:Email Required!");
                emailId.focus();
                isErrorRegis = true;
            }
            else if (!reEmail.test(emailId.val())) {
                $("#lblEmailError").removeClass("hide");
                $("#lblEmailError").text("Error:Check Email Format");
                isErrorRegis = true;
            } else {
                $("#lblEmailError").text('');
            }
            if (password.val() == "") {
                $("#lblpasswordError").removeClass("hide");
                $("#lblpasswordError").text("Error:Password Required!");
                emailId.focus();
                isErrorRegis = true;
            } else {
                $("#lblpasswordError").text('');
            }

            return !isErrorRegis;

        }

        function ValidateLogin() {
            var json = "{'emailId':'" + escape($("[id$='txtEmail']").val()) + "','password':'" + escape($("[id$='txtPassword']").val()) + "'}";
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/ValidateLogin",
                data: json,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var spliResponse = response.d.split("-status-");
                    if (spliResponse[1] == "True") {

                        if (spliResponse[0] =='Sorry,Your account is not active') {
                            $("#spnCall1AfterLogin1").hide();
                            $("#spnCall1AfterLogin").hide();
                            $("#spnBtnInterested1").show();
                            $("#spnBtnInterested2").show();

                            $("#divRegister").hide(); $("#fade").hide();
                            ClearLoginControl();
                        }
                        else {

                            $("#lblerrMsg").css("display", "block");
                            $("#lblerrMsg").addClass("error");
                            $("#lblerrMsg").html(spliResponse[0])
                        }
                    } else {
                        ClearLoginControl();
                        $("#divRegister").hide(); $("#fade").hide();
                        alert("Sorry,you are register as college.");
                        return false;
                    }
                }, error: function (response) {
                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("error");
                    $("#lblerrMsg").html(response.d)
                    ClearLoginControl();
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
        function ResisterUser() {

            var isErrorRegis = false;

            if (!ValidateRegistationForm()) {

                isErrorRegis = true;
            }
            if (isErrorRegis) {

                return false;

            } else {
                Register();
            }
        }

        function Register() {
            $("#lblerrMsg").html('');
            var dataQuery = '{"mobileNo":"' + $("#txtMobileNumber").val() + '","emailId":"' + $("#txtEmailId").val() + '","name":"' + $("#txtName").val() + '","userType":"' + $("#ddlUserType").val() + '","courseId":"' + $("#<%= hndCourseId.ClientID %>").val() + '"}';

            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/UserRegister",
                data: dataQuery,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
            
                    if (response.d> 0) {
                        $("#spnCall1AfterLogin1").hide();
                        $("#spnCall1AfterLogin").hide();
                        $("#spnBtnInterested1").show();
                        $("#spnBtnInterested2").show();

                        $("#divRegister").hide(); $("#fade").hide();

                    } else {

                        $("#lblerrMsg").css("display", "block");
                        $("#lblerrMsg").addClass("error");
                        $("#lblerrMsg").html("Dear " + $("#txtName").val() + " you are already registered.<a href='#' onclick='openLoginPop();return false;'>Please Login</a>");
                    }
                }, error: function (response) {
                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("error");
                    $("#lblerrMsg").html(response.d)
                    ClearLoginControl();
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }
        function ValidateRegistationForm() {
            var emailId = $("#txtEmailId");
            var mono = $("#txtMobileNumber");
            var name = $("#txtName"); var userType = $("#ddlUserType");

            var mobileNo = /^[1-9][0-9]{9}$/;
            var isErrorRegis = false;
            if (userType.val() <= 0) {
                $("#lblUserTypeError").removeClass("hide");
                $("#lblUserTypeError").html("Error:User Required!");

                userType.focus();

                isErrorRegis = true;
            }
            else {
                $("#lblUserTypeError").text('');
            }

            if (name.val() == "") {
                $("#lblNameError").removeClass("hide");
                $("#lblNameError").text("Error:Name Required!");

                name.focus();

                isErrorRegis = true;
            }
            else {
                $("#lblNameError").text('');
            }


            if (mono.val() == "") {
                $("#lblNumberError").removeClass("hide");
                $("#lblNumberError").text("Error:Mobile Number Required!");
                mono.focus();
                isErrorRegis = true;
            }
            else if (mono.val().length < 10 || mono.val().length > 10) {
                $("#lblNumberError").removeClass("hide");
                $("#lblNumberError").text("Error:Only 10 digit Numeric");
                isErrorRegis = true;
            }
            else if (!mobileNo.test(mono.val().trim())) {
                $("#lblNumberError").removeClass("hide");
                $("#lblNumberError").text("Error:Numeric starting with 7 or 8 or 9");
                isErrorRegis = true;
            }


            else {
                $("#lblNumberError").text('');
            }

            if (emailId.val() == "") {
                $("#lblEmailIdError").removeClass("hide");
                $("#lblEmailIdError").text("Error:Email Required!");
                emailId.focus();
                isErrorRegis = true;
            }
            else if (!reEmail.test(emailId.val())) {
                $("#lblEmailIdError").removeClass("hide");
                $("#lblEmailIdError").text("Error:Check Email Format");
                isErrorRegis = true;
            } else {
                $("#lblEmailIdError").text('');
            }


            return !isErrorRegis;

        }
        function ClearRegistationControl() {
            $("#ddlUserType").val(0);
            $("#txtEmailId").val('');
            $("#txtMobileNumber").val('');
            $("#txtName").val('');
            $("#lblNameError").addClass("hide");
            $("#lblUserTypeError").addClass("hide");
            $("#lblEmailIdError").addClass("hide");
            $("#lblNumberError").addClass("hide");
        }

        function ClearLoginControl() {
            $("#txtEmail").val('')
            $("#txtPassword").val('')
            $("#lblEmailError").addClass("hide");
            $("#lblpasswordError").addClass("hide");

        }

        function hidePoup() {


            document.getElementById("divExamQuery").style["display"] = 'none';


        }

        function showLoginAndRegister() {
            ClearRegistationControl();
            $('#fldLogin').hide();
            $("#lblerrMsg").html('');
            $('#fldRegister').show();
            $('#fldRegister').removeClass('hide');

            return false;
        }
        function showLoginAndRegist() {
            ClearLoginControl();
            $('#fldLogin').show();
            $('#fldRegister').hide();
            $('#fldLogin').removeClass('hide');
            return false;
        }

        function CollegeQuery(collegeNmae, cityName, branchCourseId) {

            var dataQuery = '{"collegeBranchName":"' + collegeNmae.val() + '","cityName":"' + cityName.val() + '","branchCourseId":"' + branchCourseId.val() + '","courseId":"' + $("#<%=hndCourseId.ClientID %>").val() + '"}';
            $("#divImageCall1").show();
            $.ajax({
                type: "POST",
                url: "/WebServices/CommonWebServices.asmx/InterestedQuickQuery",
                data: dataQuery,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#divImageCall1").hide();
                    
                    if (response.d[0] == "True") {
                        $("#divImageCall1").hide();

                        alert("Thank you for showing interest.Our team will revert you soon");
                    } else {
                        alert("Sorry,you are register as college.");
                        return false;
                    }

                },
                error: function (response) {
                    $("#lblerrMsg").css("display", "block");
                    $("#lblerrMsg").addClass("error");
                    $("#lblerrMsg").html(response.d[1])
                    ClearLoginControl();
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible

            $("#divImage").show();

        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {

            $("#divImage").hide();

        }
        function openLoginPop() {
            $('#fldLogin').show();
            $("#lblerrMsg").html('');
            $('#fldRegister').hide();
            OpenPoupLogins("divRegister", "650", "sendCall1"); return false;
        }

        function CheckCollegeField() {
            if ($("#<%=txtFirstCollegeName.ClientID %>").val() == "") {
                alert("Please Enter College ");
                return false;
            }
            else if ($("#<%=txtSecondCollegeName.ClientID %>").val() == "") {
                alert("Please Enter  College ");
                return false;
            }
            else if ($("#<%=txtSecondCollegeName.ClientID %>").val().trim() == $("#<%=txtFirstCollegeName.ClientID %>").val().trim()) {
                alert("Please different college name");
                return false;
            }
            else {
                return true;
            }

        }

        function CheckReplaceCollege() {

            if ($("#<%=txtSecondCollegeReplace.ClientID %>").val() == "") {
                alert("Please Enter  College ");
                return false;
            }
            else if ($("#<%=txtSecondCollegeReplace.ClientID %>").val().trim() == $("#<%=txtFirstCollegeName.ClientID %>").val().trim()) {
                alert("Please enter different collge");
                return false;
            }
            else if ($("#<%=txtSecondCollegeReplace.ClientID %>").val().trim() == $("#<%=txtSecondCollegeName.ClientID %>").val().trim()) {
                alert("Please enter different collge");
                return false;
            }
            else {
                return true;
            }

        }
        function CheckReplaceCollege1() {
            if ($("#<%=txtFirstCollegeReplace.ClientID %>").val() == "") {
                alert("Please Enter College ");
                return false;
            }
            else if ($("#<%=txtFirstCollegeReplace.ClientID %>").val().trim() == $("#<%=txtFirstCollegeName.ClientID %>").val().trim()) {
                alert("Please enter different collge");
                return false;
            }
            else if ($("#<%=txtFirstCollegeReplace.ClientID %>").val().trim() == $("#<%=txtSecondCollegeName.ClientID %>").val().trim()) {
                alert("Please enter different collge");
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>
