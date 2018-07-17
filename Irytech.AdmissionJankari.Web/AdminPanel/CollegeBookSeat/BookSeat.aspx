<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    EnableEventValidation="false" AutoEventWireup="true" CodeBehind="BookSeat.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.CollegeBookSeat.BookSeat" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<%@ Register Src="~/UserControl/FckEditorCostomize.ascx" TagName="FckEditor" TagPrefix="Aj" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ul class="addPage_utility">
                <li class="fright" style="width:127px !important;">
                    <div class="navbar-inner">
                        <a href="#" id='sndAddCollegeinBookSeat' class="insertIco" onclick="openpopupAds();return false;">
                            Add College </a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>
            <fieldset>
                <legend>Search College</legend>
                <ul class="options-bar">
                    <li>
                        <label>
                            College:</label>
                        <asp:TextBox ID="txtCollegeSearch" CssClass="autocomplete" placeholder="Enter College Name"
                            Width="63%" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Course:</label>
                        <asp:DropDownList ID="ddlCourseSearch" runat="server" ToolTip="Please Select Course"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourseSearch_SelectedIndexChanged">
                        </asp:DropDownList>
                    
                    </li>
                    <li>
                        <label>
                        </label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="BtnSearchClick" />
                    </li>
                </ul>
                <asp:Label runat="server" ID="lblResult" Visible="False"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnEligibity" />
                <asp:HiddenField runat="server" ID="hdnBookSeatId" />
                <asp:Repeater ID="rptBookSeat" runat="server" OnItemCommand="RptBookSeatItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    College
                                </th>
                                <th>
                                    Course
                                </th>
                                <th>
                                   Start Date
                                </th>
                                 <th>
                                   End Date
                                </th>
                                <th>
                                    Book Seat Status
                                </th>
                                <th>
                                    Booking Amount
                                </th>
                                <th>
                                    Action
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%-- <td>
                            <%# Eval("SrNo") %>
                        </td>--%>
                            <td>
                                <%# Eval("CollegeBasicInfo.CollegeBranchName")%>
                            </td>
                            <td>
                                <%# Eval("CourseMaster.CourseName")%>
                            </td>
                          
                            <td>
                                <%# !string.IsNullOrEmpty(Convert.ToString(Eval("BookSeatStartDate")))?Convert.ToDateTime(Eval("BookSeatStartDate")).ToString("dd/MM/yyyy"):"" %>
                            </td>
                            <td>
                                <%# !string.IsNullOrEmpty(Convert.ToString(Eval("BookSeatEndDate"))) ? Convert.ToDateTime(Eval("BookSeatEndDate")).ToString("dd/MM/yyyy") : ""%>
                            </td>
                            <td>
                                <%# Eval("BookSeatStatus")%>
                            </td>
                            <td>
                                <%# Eval("BookSeatAmount")%>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("BookSeatId")%>'
                                    CommandName="Edit" CausesValidation="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <Aj:CustomPaging ID="ucCollegeList" runat="server" />
                <asp:HiddenField runat="server" ID='hdnCourseId' Value="0"></asp:HiddenField>
            </fieldset>
            <div id="fade">
            </div>
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" alt="loading_Image" title="Loading Image" />
            </div>
            <div id="divUniversityCategoryInsert" class="popup_block width62perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblBookSeat" runat="server"></asp:Label></legend>
                    <ul>
                        <li>
                            <label>
                                Course</label>
                            <asp:DropDownList ID="ddlCourseList" 
                                Width="200px" TabIndex="2" runat="server" ToolTip="Please Select Course">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCourse" InitialValue="0" ErrorMessage="Select course"
                                ControlToValidate="ddlCourseList" Display="Dynamic" SetFocusOnError="True" CssClass="error1"
                                ValidationGroup="BookSeat"></asp:RequiredFieldValidator>
                        </li>
                        <li >
                            <label>
                                College</label>
                            <asp:TextBox ID="txtCollege"  TabIndex="3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeName" ErrorMessage="Enter college "
                                ControlToValidate="txtCollege" Display="Dynamic" SetFocusOnError="True" CssClass="error1"
                                ValidationGroup="BookSeat"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 48% !important; float: left;">
                            <label>
                                Start Date</label>
                            <asp:TextBox ID="txtStartDate" Style="min-width:142px !important;" TabIndex="3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvStartDate" ErrorMessage="Start Date can not be blank"
                                ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True" CssClass="error1"
                                ValidationGroup="BookSeat"></asp:RequiredFieldValidator>
                        </li>
                          <li style="width: 48% !important; float: left;">
                            <label>
                                End Date</label>
                            <asp:TextBox ID="txtEndDate" Style="min-width: 142px !important;" TabIndex="3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEndDate" ErrorMessage="End Date can not be blank"
                                ControlToValidate="txtEndDate" Display="Dynamic" SetFocusOnError="True" CssClass="error1"
                                ValidationGroup="BookSeat"></asp:RequiredFieldValidator>
                        </li>
                        <li >
                            <label>
                             Booking Amount
                            </label>
                            <asp:TextBox ID="txtPayment"  TabIndex="4" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeTitle" ErrorMessage="Enter payment"
                                ControlToValidate="txtPayment" Display="Dynamic" SetFocusOnError="True" CssClass="error1"
                                ValidationGroup="BookSeat">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ID="rgPayment" ErrorMessage="Enter payment in digit"
                                ValidationExpression="^\d+$" ControlToValidate="txtPayment" Display="Dynamic"
                                SetFocusOnError="True" CssClass="error1" ValidationGroup="BookSeat">
                            </asp:RegularExpressionValidator>
                        </li>
                        <li id="liEligibity">
                            <label runat="server" id="lblEligibilty" style="font-size: 11px !important;">
                                Eligibilty Percentage For</label>
                            <asp:TextBox ID="txtEligibity" runat="server" Style="min-width: 225px !important;"
                                TabIndex="5" ValidationGroup="BookSeat" Enabled="False" MaxLength="2">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEligibity" ErrorMessage="Enter eligibilty percentage"
                                ControlToValidate="txtEligibity" Display="Dynamic" SetFocusOnError="True" CssClass="error1"
                                ValidationGroup="BookSeat">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ID="rgEligibity" ValidationExpression="^\d+$"
                                ErrorMessage="Enter eligibilty percentage in digit" ControlToValidate="txtEligibity"
                                Display="Dynamic" SetFocusOnError="True" CssClass="error1" ValidationGroup="BookSeat">
                            </asp:RegularExpressionValidator>
                        </li>
                        <li>
                            <label>
                             Display:
                            </label>
                            <asp:CheckBox ID="chkStatus" TabIndex="6" runat="server" />
                        </li>
                       
                        <li style=" height: 181px !important;">
                            <label>
                                Instruction :</label><span class="fleft">
                                    <Aj:FckEditor ID="fckInstruction" runat="server" TabIndex="8" />
                                </span></li>
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnSubmit" runat="server" TabIndex="9" Text="Insert" CausesValidation="True"
                                ValidationGroup="BookSeat" OnClick="BtnSubmitCollegeBasicInfoClick" />

                            <input type="button" value="Clear" onclick="ClearControl()" />
                            <asp:Label ID="lblSeccessMsg" runat="server" Visible="false"></asp:Label>
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <link href="../StyleSheets/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../JS/jquery.ui.core.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txtStartDate.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
            $("#<%=txtEndDate.ClientID %>").datepicker({
                changeMonth: true,
                changeYear: true
            });
        });
        var url = "/WebServices/CommonWebServices.asmx/GetCollegeDetails";
        BindDropDownCommon($("#<%=txtCollegeSearch.ClientID %>"), url);
        $("#<%=ddlCourseList.ClientID %>").change(function () {
            var courseId = $("#<%=ddlCourseList.ClientID %>").val();
            if (courseId > 0) {
                BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollege.ClientID %>"), courseId, "../../WebServices/CommonWebServices.asmx/BindCollegesByCourse");
                $("#<%=hdnCourseId.ClientID %>").val(courseId);
                getEligibiltyCriteria(courseId);
                HideProgress();

            } else {
                $("#<%=txtEligibity.ClientID %>").text(" ");
                $("#<%=hdnCourseId.ClientID %>").val(0);
                $("#<%=lblEligibilty.ClientID %>").html("Eligiblty Percentage");
                $("#<%=txtEligibity.ClientID %>").attr("disabled", true);
            }

        });

        function openpopupAds() {
            OpenPoup('divUniversityCategoryInsert', '750px', 'sndAddCollegeinBookSeat')
            ClearControl();
           
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommon($("#<%=txtCollegeSearch.ClientID %>"), url);
                $("#<%=ddlCourseList.ClientID %>").change(function () {
                    var courseId = $("#<%=ddlCourseList.ClientID %>").val();
                    if (courseId > 0) {
                        BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollege.ClientID %>"), courseId, "../../WebServices/CommonWebServices.asmx/BindCollegesByCourse");
                        $("#<%=hdnCourseId.ClientID %>").val(courseId);
                        getEligibiltyCriteria(courseId);
                        HideProgress();
                    } else {
                        $("#<%=hdnCourseId.ClientID %>").val(0);
                        $("#<%=lblEligibilty.ClientID %>").html("Eligiblty Percentage For");
                        $("#<%=txtEligibity.ClientID %>").attr("disabled", true);
                    }

                });
                $(function () {
                    $("#<%=txtStartDate.ClientID %>").datepicker({
                        changeMonth: true,
                        changeYear: true
                    });
                    $("#<%=txtEndDate.ClientID %>").datepicker({
                        changeMonth: true,
                        changeYear: true
                    });
                });
            }
        }


        function getEligibiltyCriteria(courseId) {

            var courseQuery = '{"course":"' + courseId + '"}';
            var webserviceUrl = '../../WebServices/CommonWebServices.asmx/GetCourseEligibiltyByCourse';
            CommonWebServicesCall(webserviceUrl, courseQuery, courseEligibity);
        }

        function courseEligibity(response) {

            if (response.length > 0) {
                $.each(response, function (index) {

                    $("#<%=hdnEligibity.ClientID %>").val(response[index].CourseEligibityName);
                    $("#<%=lblEligibilty.ClientID %>").html("Eligiblty Percentage For " + response[index].CourseEligibityName);
                    $("#<%=txtEligibity.ClientID %>").attr("disabled", false);
                });
            } else {
                $("#<%=lblEligibilty.ClientID %>").html("Eligiblty Percentage For ");
                $("#<%=txtEligibity.ClientID %>").attr("disabled", true);
            }
        }

        function ShowProgress() {
            $("#progress").show();
        }

        function HideProgress() {
            $("#progress").hide();
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


        function ClearControl() {

            $("#<%=ddlCourseList.ClientID %>" + "option:first-child").attr("selected", "selected");
            $("#<%=txtCollege.ClientID %>").val('');
            $("#<%=lblBookSeat.ClientID %>").text('Add College');
            $("#<%=btnSubmit.ClientID %>").text('Add');
            $("#<%=txtCollege.ClientID %>").removeAttr('disabled');
            $("#<%=ddlCourseList.ClientID %>").removeAttr('disabled');
            $("#<%=txtPayment.ClientID %>").val('');
            $("#<%=txtPayment.ClientID %>").val('');
        }

    </script>
</asp:Content>
