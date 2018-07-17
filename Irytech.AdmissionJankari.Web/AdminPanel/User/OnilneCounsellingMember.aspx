<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="OnilneCounsellingMember.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.User.OnilneCounsellingMember" %>

<%@ Register TagPrefix="AJ" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
            <asp:HiddenField runat="server" ID="hdnCourseId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnForm"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnUserName"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnCourseName"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnEmail"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnUserId"></asp:HiddenField>
            <div id="divAccademic" class="popup_block">
                <div class="tabbed_area" id="tabAccademicStudent">
                    <h3 style="border-bottom: 1px dashed #e1e1e1;">
                        Student Accademic Details</h3>
                    <ul class="tabs" id="ulAccademic">
                    </ul>
                    <div class="clear">
                    </div>
                </div>
                <div class="tab_container " id="tabPrivateContainerAccademic">
                    <span id="Privateloading" style="display: none">
                        <img src="/image.axd?Common=LoadingImage.gif" />
                    </span>
                </div>
            </div>
            <ul class="addPage_utility">
        <li class="fright">
           <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png"
                            ToolTip="Download Excel" TabIndex="5" />  
        </li>
        </ul>
            <fieldset>
                <legend>Lead Search </legend>
                <ul class="options-bar">
                    <li>
                        <label class="searchlabel">
                            <%=Resources.label.Email %>
                            &
                            <%=Resources.label.Mobile %></label>
                        <asp:TextBox ID="txtUserEmailId" runat="server" TabIndex="1" placeholder="Email Search"
                            Width="40%" CssClass="autocomplete" ValidationGroup="UserMaster"></asp:TextBox>
                        <asp:TextBox ID="txtUserMobileNo" runat="server" placeholder="Mobile Search" TabIndex="2"
                            Width="20%" CssClass="autocomplete" ValidationGroup="UserMaster"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="searchbtn" Text="Search" TabIndex="3"
                            OnClick="btnSearch_Click" />
                    </li>
                </ul>
            
                <ul>
                    <li>
                        <label>
                            <%=Resources.label.Course %></label>
                        <span class="filterPart2">
                            <asp:DropDownList ID="ddlCourseName" runat="server" TabIndex="4" OnSelectedIndexChanged="DdlCourseNameSelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </span><span class="filterPart2">
                            <asp:DropDownList ID="ddlCityName" runat="server" TabIndex="5" OnSelectedIndexChanged="DdlCityNameSelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </span><span class="filterPart2">
                            <asp:DropDownList ID="rbtPaymentMode" runat="server" TabIndex="6" OnSelectedIndexChanged="RbtCourseAdmissionEligibiltySelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="true" Text="Paid" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="false" Text="UnPaid"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                       </li></ul>
            </fieldset>
            <div>
                <asp:Repeater ID="rptUserMaster" runat="server">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    S.No
                                </th>
                                <th>
                                    Email
                                </th>
                                <th>
                                    Mobile
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Course
                                </th>
                                <th>
                                    City
                                </th>
                                <th>
                                    Payment Status
                                </th>
                                <th>
                                    Accademic
                                </th>
                                <th>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("SrNo") %>
                            </td>
                            <td>
                                <%# Eval("UserEmailid")%>
                            </td>
                            <td>
                                <%# Eval("MobileNo")%>
                            </td>
                            <td>
                                <%# Eval("UserFullName")%>
                            </td>
                            <td>
                                <%# Eval("CourseName")%>
                            </td>
                            <td>
                                <%# Eval("CityName")%>
                            </td>
                            <td>
                                <%# Eval("StudentPaymentStatus")%>
                            </td>
                            <td>
                                <a id="lnkUser" style="cursor: pointer; text-decoration: underline" onclick="ShowAccademicUserDetails('<%# Eval("UserId")%>','paid');return false;">
                                    See Accademic Details </a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
            </div>
            <div>
                <asp:Repeater ID="rptUnpaid" runat="server">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    EmailId
                                </th>
                                <th>
                                    Mobile
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Course
                                </th>
                                <th>
                                    City
                                </th>
                                <th>
                                    Status
                                </th>
                                <th>
                                    Accademic
                                </th>
                                <th>
                                    Action
                                </th>
                                <th>
                                     Payment Status
                                </th>
                                <th>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("UserEmailid")%>
                            </td>
                            <td>
                                <%# Eval("MobileNo")%>
                            </td>
                            <td>
                                <%# Eval("UserFullName")%>
                            </td>
                            <td>
                                <%# Eval("CourseName")%>
                            </td>
                            <td>
                                <%# Eval("CityName")%>
                            </td>
                            <td>
                                <%# Eval("StudentPaymentStatus")%>
                            </td>
                            <td>
                                <a id="sndAccademic" style="cursor: pointer; text-decoration: underline" onclick="ShowAccademicUserDetails('<%# Eval("UserId")%>','unpaid');return false;">
                                    See Accademic Details </a>
                                <td>
                                    <a id="A1" style="cursor: pointer; text-decoration: underline" onclick="SendPaymentLink('<%# Eval("UserId")%>','<%# Eval("CourseId")%>','<%# Eval("UserEmailid")%>','<%# Eval("UserFullName")%>','<%# Eval("CourseName")%>','<%# Eval("ApplicationFormNumber")%>');return false;">
                                        Send Payment Link </a>
                                </td>
                                <td>
                                    <a id="snPaymnetStatus" style="cursor: pointer; text-decoration: underline" onclick="SendPaymentStatus('<%# Eval("UserId")%>','<%# Eval("CourseId")%>','<%# Eval("UserEmailid")%>','<%# Eval("UserFullName")%>','<%# Eval("CourseName")%>','<%# Eval("ApplicationFormNumber")%>');return false;">
                                        Update Payment Status </a>
                                </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <AJ:CustomPaging ID="CustomPaging1" runat="server" />
            </div>
            <div id="divTransaction" class="popup_block">
                <fieldset>
                    <legend>Update Transaction Status </legend>
                    <ul>
                        <li>
                            <label>
                                Transaction Mode</label>
                            <asp:DropDownList runat="server" ID="ddlTransaction">
                                <asp:ListItem Value="By Cheque" Text="By Cheque"></asp:ListItem>
                                <asp:ListItem Value="By Cash" Text="By Cash"></asp:ListItem>
                                <asp:ListItem Value="Online" Text="Online"></asp:ListItem>
                                <asp:ListItem Value=" By Demand Draft" Text="By Demand Draft"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <label>
                                Bank</label>
                            <asp:TextBox runat="server" ID="txtBank">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="bank" Display="Dynamic" ControlToValidate="txtBank"
                                ErrorMessage="Field bank can't be blank" ValidationGroup="InsertTransaction"
                                SetFocusOnError="True" CssClass="error1"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                Payment</label>
                            <asp:TextBox runat="server" ID="txtPayment">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="payment" Display="Dynamic" ControlToValidate="txtPayment"
                                ErrorMessage="Field payment can't be blank" ValidationGroup="InsertTransaction"
                                SetFocusOnError="True" CssClass="error1"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                Display</label>
                            <asp:CheckBox ID="chkPaymentStatus" runat="server" TabIndex="1"></asp:CheckBox>
                        </li>
                        <li>
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" TabIndex="2"
                                OnClick="btnUpdateStatus_Click" ValidationGroup="InsertTransaction" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="fade">
    </div>
    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
    <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" />
    </div>
    <script src="../JS/commonscripts.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/Autocomplete.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Styles/autoCompliteCSS.css" />
    <script language="javascript" type="text/javascript">

        function close() {

            $("#fade").hide();
        }
        var mobileListUrl = "../../WebServices/CommonWebServices.asmx/GetMobileNoList";
        var emailListUrl = "../../WebServices/CommonWebServices.asmx/GetUserEmailIDList";
        var bankUrl = "../../WebServices/CommonWebServices.asmx/GetBankName";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtBank.ClientID %>"), bankUrl);
        BindDropDownCommonForAdminAutoComplete($("#<%=txtUserMobileNo.ClientID %>"), mobileListUrl);
        BindDropDownCommonForAdminAutoComplete($("#<%=txtUserEmailId.ClientID %>"), emailListUrl);
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtBank.ClientID %>"), bankUrl);

                BindDropDownCommonForAdminAutoComplete($("#<%=txtUserMobileNo.ClientID %>"), mobileListUrl);
                BindDropDownCommonForAdminAutoComplete($("#<%=txtUserEmailId.ClientID %>"), emailListUrl);
                close();
            }
        }
        function SendPaymentLink(userId, courseId, emailId, userName, courseName, formNumber) {
            var json = "{'courseId':'" + courseId + "','userId':'" + userId + "','emailId':'" + emailId + "','userName':'" + userName + "','formNumber':'" + formNumber + "'}";

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/SendPaymentLink",
                data: json,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert("Payment link has been sent at " + emailId);
                },
                error: function (xml, textStatus, errorThrown) {
                    // alert(xml.status + "||" + xml.responseText);
                }
            });

        }

        function SendPaymentStatus(userId, courseId, emailId, userName, courseName, formNumber) {

            $("#<%=hdnUserId.ClientID %>").val(userId);
            $("#<%=hdnCourseName.ClientID %>").val(courseName);
            $("#<%=hdnCourseId.ClientID %>").val(courseId);
            $("#<%=hdnForm.ClientID %>").val(formNumber);
            $("#<%=hdnEmail.ClientID %>").val(emailId);
            OpenPoup('divTransaction', '650', 'snPaymnetStatus');
        }

        function ShowAccademicUserDetails(userId, stat) {

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetAccademicOfStudent",
                data: '{"userId":"' + userId + '"}',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindTabsAccademic(response, userId, stat);
                },
                error: function (xml, textStatus, errorThrown) {
                    // alert(xml.status + "||" + xml.responseText);
                }
            });

        }
        function BindTabsAccademic(data, userId, stat) {

            $('#tabPrivateContainerAccademic').html(""); $('#tabAccademicStudent ul').html('');
            if (data.d.length > 0) {
                if (data.d == 10) {
                    $('#tabAccademicStudent ul').append($('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails(' + userId + ');return false;">10Th</a></li>'));

                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails(userId);
                }
                else if (data.d == 11) {

                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails(' + userId + ');return false;">10Th</a></li><li  rel="tabAccademicDiploma"><a href="#tabsAccademic-Diploma " onclick="GetDiplomaDetails(' + userId + ');return false"  >Diploma</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademicDiploma" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails(userId);
                }
                else if (data.d == 12) {
                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails(' + userId + ');return false;">10Th</a></li><li  rel="tabAccademic12"><a href="#tabsAccademic-12" onclick="GetStudentInterMediateSchoolDetails(' + userId + ');return false;" >12Th</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademic12" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails(userId);
                }
                else if (data.d == 13) {
                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10"  onclick="GetStudentHighSchoolDetails(' + userId + ');return false;" >10Th</a></li><li  rel="tabAccademic12"><a href="#tabsAccademic-12" onclick="GetStudentInterMediateSchoolDetails(' + userId + ');return false;">12Th</a></li><li  rel="tabAccademicDiploma"><a href="#tabsAccademic-Diploma" onclick="GetDiplomaDetails(' + userId + ');return false">Diploma</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademic12" class="tab_contentAccademic"></div><div id="tabAccademicDiploma" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails(userId);
                }
                else if (data.d == 15) {
                    $('#tabAccademicStudent ul').append('<li class="active" rel="tabAccademic10"><a href="#tabsAccademic-10" onclick="GetStudentHighSchoolDetails(' + userId + ');return false;" >10Th</a></li><li  rel="tabAccademic12"><a href="#tabsAccademic-12" onclick="GetStudentInterMediateSchoolDetails(' + userId + ');return false;">12Th</a></li><li  rel="tabAccademicGraduation"><a href="#tabsAccademic-Graduation" onclick="GetGraduationDetails(' + userId + ');return false;" >Graduation</a></li>');
                    $('#tabPrivateContainerAccademic').append('<div id="tabAccademic10" class="tab_contentAccademic"></div><div id="tabAccademic12" class="tab_contentAccademic"></div><div id="tabAccademicGraduation" class="tab_contentAccademic"></div>');
                    GetStudentHighSchoolDetails(userId);
                }
                $(".tab_contentAccademic").hide();
                $(".tab_contentAccademic:first").show();

                $("#ulAccademic li").click(function () {

                    $("#ulAccademic li").removeClass("active");
                    $(this).addClass("active");
                    $(".tab_contentAccademic").hide();
                    var activeTab = $(this).attr("rel");
                    $("#" + activeTab).fadeIn();
                });
            }
            stat == "paid" ? (OpenPoup('divAccademic', '650', 'lnkUser')) : (OpenPoup('divAccademic', '650', 'sndAccademic'));

        }
        function GetStudentHighSchoolDetails(userId) {

            $("#Privateloading").show();
            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetStudentHighSchlDetails",
                data: '{"userId":"' + userId + '"}',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    BindHighSchoolDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    // alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        function BindHighSchoolDetails(data) {

            var finalData = "";
            if (data.d.length > 0) {
                finalData = "<fieldset ><legend>10th Standard</legend><ul><li><label>Board Name:</label><strong class='tabStrong'>" + data.d[0].BoardName + "</strong></li><li><label>School Name:</label><strong class='tabStrong'>" + data.d[0].HigherSecondarySchoolName + "</strong></li><li><label>CGPA:</label><strong class='tabStrong'>" + data.d[0].HigherSecondarySchoolScoreCGPA + "</strong></li><li><label>Passing Year:</label><strong class='tabStrong'>" + data.d[0].HigherSecondarySchoolPassingYear + "</strong></li><ul></fieldset>";
                $("#tabAccademic10").html('');

                $("#tabAccademic10").append(finalData);
            } else {
                $("#tabAccademic10").html('Sorry no data found.');
            }
        }
        function GetStudentInterMediateSchoolDetails(userId) {

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetStudentInterMediateDetails",
                data: '{"userId":"' + userId + '"}',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindInetrMediateSchoolDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        function BindInetrMediateSchoolDetails(data) {

            var finalData = "";
            if (data.d.length > 0) {
                finalData += "<fieldset ><legend>12th Standard</legend><ul><li><label>Board Name:</label><strong class='tabStrong'>" + data.d[0].BoardName + "</strong></li><li><label>School Name:</label><strong class='tabStrong'>" + data.d[0].SeniorSecondarySchoolName + "</strong></li><li><label>CGPA:</label><strong class='tabStrong'>" + data.d[0].SeniorSecondarySchoolScoreCgpa + "</strong></li><li><label>Passing Year:</label><strong class='tabStrong'>" + data.d[0].SeniorSecondarySchoolPassingYear + "</strong></li><li><label>Subject Combination:</label><strong class='tabStrong'>" + data.d[0].SeniorSecondarySchoolSubjectCombination + "</strong></li><li><label>Subject Marks:</label><strong class='tabStrong'>" + data.d[0].SeniorSecondarySchoolSubjectMarks + "</strong></li><li><label>Percent:</label><strong class='tabStrong'>" + data.d[0].SeniorSecondarySchoolSubjectPercent + "</strong></li><ul></fieldset>";
                $("#tabAccademic12").html('');
                $("#tabAccademic12").append(finalData);
            } else { $("#tabAccademic10").html('Sorry no data found.'); }

        }
        function GetDiplomaDetails(userId) {

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetStudentDiplomaDetails",
                data: '{"userId":"' + userId + '"}',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindDiplomaFullDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        function BindDiplomaFullDetails(data) {

            var finalData = "";
            if (data.d.length > 0) {
                finalData += "<fieldset style='display:550px'><legend>Diploma Standard</legend><ul><li><label>School Name:</label>" + data.d[0].StudentDicCollegeName + "</li><li><label>Course:</label>" + data.d[0].StudentDicCourse + "</li><li><label>Percent:</label>" + data.d[0].StudentDicPercentage + "</li><li><label>CGPA:</label>" + data.d[0].StudentDicCGPA + "</li><li><label>Passing Year:</label>" + data.d[0].StudentDicYOP + "</li><li><a href='#' id='sndDiploma' onclick='EditDiplomaStandard(550);return false;'>Edit</a></li><ul></fieldset>";
                $("#tabAccademicDiploma").html('');
                $("#tabAccademicDiploma").append(finalData);
            } else { $("#tabAccademic10").html('Sorry no data found.'); }


        }
        function GetGraduationDetails(userId) {

            $.ajax({
                type: "POST",
                url: "../../WebServices/CommonWebServices.asmx/GetStudentGraduationDetails",
                data: '{"userId":"' + userId + '"}',
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    BindGraduationDetails(response);
                },
                error: function (xml, textStatus, errorThrown) {
                    //alert(xml.status + "||" + xml.responseText);
                }
            });
        }

        function BindGraduationDetails(data) {


            var finalData = "";
            if (data.d.length > 0) {
                finalData += "<fieldset style='display:550px'><legend>Graduation Standard</legend><ul><li><label>College:</label>" + data.d[0].StudentGrdCollegeName + "</li><li><label>Specialization:</label>" + data.d[0].StudentGrdSpecialization + "</li><li><label>Percent:</label>" + data.d[0].StudentGrdPer + "</li><li><label>CGPA:</label>" + data.d[0].StudentGrdCGPA + "</li><li><label>Passing Year:</label>" + data.d[0].StudentGrdYOP + "</li><li><a href='#' id='sndGrad' onclick='EditGradStandard(550);return false;'>Edit</a></li><ul></fieldset>";
                $("#tabAccademicGraduation").html('');
                $("#tabAccademicGraduation").append(finalData);
            } else { $("#tabAccademic10").html('Sorry no data found.'); }



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

        $("#<%=txtUserEmailId.ClientID %>").keypress(function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                $("#<%=btnSearch.ClientID %>").click();
                return false;
            }

        });
        $("#<%=txtUserMobileNo.ClientID %>").keypress(function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                $("#<%=btnSearch.ClientID %>").click();
                return false;
            }

        }); 
    </script>
</asp:Content>
