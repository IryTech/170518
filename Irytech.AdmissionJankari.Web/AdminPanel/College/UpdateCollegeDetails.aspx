<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="UpdateCollegeDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.UpdateCollegeDetails" %>

<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUpload" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <ul class="addPage_utility">
        <li class="fright" style="width: 146px !important;">
            <div class="navbar-inner" style="margin-right: 1%;">
                <a href="/adminpanel/College/CollegeList.aspx" class="viewIco">College Master</a>
                <div class="clear">
                </div>
            </div>
        </li>
    </ul>
       <asp:Label ID="lblResult" runat="server" Visible="false"></asp:Label>
    <div class="popup_block" id="divUploadImage">
        <label>
            Logo</label>
        <AJ:FileUpload ID="FileUpload1" runat="server" />
        <asp:HiddenField ID="hdnImageFile" runat="server" />
    </div>

    <fieldset>
    <legend>Update College Master</legend>

    <div class="accordion1">
        <h2 class="accord1">
            Basic Details
        </h2>
        <div>
         
            <fieldset id="basicInfo" style="background:none !important;">
               <%-- <legend>College Branch Basic Info</legend>--%>
                <ul style=" float:right; width:100px; text-align:center; ">
                    <li>
                        <asp:Image runat="server" ID="imgCollege" Height="100px" CssClass="fleft" Width="100px"></asp:Image>
                         <a href="#" id='sndImage' class="changeImg" onclick="OpenPoup('divUploadImage','350','sndImage');return false;">Change Image</a>
                        
                    </li>
                </ul>
                <ul class="collegemaster">
                    <li class="width48perc fleft">
                        <label>
                            Institute Type</label>
                        <asp:DropDownList runat="server" ID="ddlInstituteType" TabIndex="1" ToolTip="Please Select Institute">
                        </asp:DropDownList>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            Institute Group</label>
                        <asp:DropDownList runat="server" ID="ddlCollegeGroup" TabIndex="3" ToolTip="Please Select Institute">
                        </asp:DropDownList>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            Institute Name</label>
                        <asp:TextBox ID="txtCollegeBranch" runat="server" TabIndex="4" ToolTip="Please Enter CollegeBranch"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="college" SetFocusOnError="True" runat="server" ValidationGroup="CollegeUpdate" ControlToValidate="txtCollegeBranch" ErrorMessage="Field institute name can't be blank" Display="Dynamic" CssClass="error" />
                    </li>
                   <li class="width48perc fleft">
                        <label>
                            Popular Name</label>
                        <asp:TextBox ID="txtCollegePopularName" runat="server" TabIndex="7" ToolTip="Please Enter College Popular Name"></asp:TextBox>
                    </li>
                   <li class="width48perc fleft">
                        <label>
                            Management</label>
                        <asp:RadioButtonList runat="server" TabIndex="5" CssClass="RadioButtonList5" RepeatDirection="Horizontal" ID="rbtManagement">
                        </asp:RadioButtonList>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            Establishment</label>
                        <asp:TextBox ID="txtCollegeEst" runat="server" TabIndex="6" ToolTip="Please Enter College Establishment"></asp:TextBox>
                    </li>
                    
                    <li class="width48perc fleft clear">
                        <label>
                            Country</label>
                        <asp:DropDownList runat="server" ID="ddlCountry" TabIndex="8" style="width:225px !important;" AutoPostBack="true" ToolTip="Please Select Country" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            State</label>
                        <asp:DropDownList runat="server" ID="ddlState" TabIndex="9" AutoPostBack="true" ToolTip="Please Select State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            City</label>
                        <asp:DropDownList runat="server" ID="ddlCollegeCity" TabIndex="10" ToolTip="Please Select City">
                        </asp:DropDownList>
                    </li>
                   <li class="width48perc fleft">
                        <label>
                            WebSite</label>
                        <asp:TextBox ID="txtCollegeWebsite" runat="server" TabIndex="11" ToolTip="Please Enter College WebSite"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Status</label>
                        <asp:CheckBox runat="server" ID="chkCollegeStatus" ToolTip="Please Check Status" TabIndex="13" CssClass="chkCollege"></asp:CheckBox>
                    </li>
                    <li>
                        <label>
                            Description</label>
                       <span class="fleft" style="margin:3px 5px;">
                        <AJ:FckEditorCostomize ID="fckCourseDecsription" TabIndex="13" runat="server" />
                        </span>
                    </li>
                    <li>
                        <label>
                        </label>
                    </li>
                </ul>
            </fieldset>
            <fieldset id="basicInfoContact">
                <legend>Contact Details</legend>
                <ul>
                    <li class="width48perc fleft">
                        <label>
                            EmailId</label>
                        <asp:TextBox ID="txtEmailId" runat="server" TabIndex="9" ToolTip="Please Enter EmailId"></asp:TextBox>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            Phone</label>
                        <asp:TextBox ID="txtCollegeMobile" runat="server" TabIndex="9" ToolTip="Please Enter MobileNo"></asp:TextBox>
                    </li>
                     <li class="width48perc fleft">
                        <label>
                            PinCode
                        </label>
                        <asp:TextBox ID="txtPinCode" runat="server" TabIndex="10" ToolTip="Please Enter PinCode"></asp:TextBox>
                    </li>
                    <li class="width48perc fleft">
                        <label>
                            Fax</label>
                        <asp:TextBox ID="txtCollegeFax" runat="server" TabIndex="11" ToolTip="Please Enter Fax"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" TabIndex="12" ToolTip="Please Enter Adress" TextMode="MultiLine"></asp:TextBox>
                    </li>
                    <li><label></label>
                        <asp:Button ID="btnSubmitCollegeBasicInfo" runat="server" Text="Update" ValidationGroup="CollegeUpdate" ToolTip="Please Submit" OnClick="BtnSubmitCollegeBasicInfoClick" /></li></ul>
            </fieldset>
        </div>
    </div>
    <div class="accordion1">
        <a href="#" id="sndCourseInsert" onclick="OpenPoup('divCourseInsert','650','sndCourseInsert');return false;" class="anchor">Add Course</a>
        <h2 class="accord1">
             Courses Available
        </h2>
        
        <div>
            <asp:Repeater ID="rptCourse" runat="server" OnItemCommand="rptCourse_ItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                Course
                            </th>
                            <th>
                                Helpline No
                            </th>
                            <th>
                                Sponser Status
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                                Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("SrNo") %>
                        </td>
                        <td>
                            <%# Eval("CourseName")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseHelplineNo")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseSponserStatus")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseStatus")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeBranchCourseId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
        </div>
    </div>
    <div class="accordion1">
        <a href="#" id="sndCourseStreamInsert" onclick="OpenPoup('divCourseStreamInsert','650','sndCourseStreamInsert');return false;" class="anchor">Add Stream</a>
        <h2 class="accord1">
             Streams Available
        </h2>
        <div>
            <asp:Repeater ID="rptCourseStream" runat="server" OnItemCommand="rptCourseStream_ItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                CourseName
                            </th>
                            <th>
                                StreamName
                            </th>
                            <th>
                                StreamSeat
                            </th>
                            <th>
                                Fees
                            </th>
                            <th>
                            Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("SrNo") %>
                        </td>
                        <td>
                            <%# Eval("CourseName")%>
                        </td>
                        <td>
                            <%# Eval("StreamName")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseStreamSeat")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseStreamFees")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeBranchCourseStreamId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <AJ:CustomPaging ID="CustomPaging1" runat="server" />
        </div>
    </div>
    <div class="accordion1">
        <a href="#" id="sndExamInsert" onclick="OpenPoup('divExamInsert','650','sndExamInsert');return false;" class="anchor">Add Qualifying Exam</a>
        <h2 class="accord1">
            Qualifying Exams
        </h2>
        <div>
            <asp:Repeater ID="rptExam" runat="server" OnItemCommand="RptExamItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                CourseName
                            </th>
                            <th>
                                ExamName
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                            Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Container .ItemIndex +1 %>
                        </td>
                        <td>
                            <%# Eval("CourseName")%>
                        </td>
                        <td>
                            <%# Eval("ExamName")%>
                        </td>
                        <td>
                            <%# Eval("CollegeCourseExamStatus")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeBranchCourseExamId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
     <div class="accordion1">
        <a href="#" id="sndhostel" onclick="OpenPoup('divHostelInsert','650','sndhostel');return false;" class="anchor">Add Hostel</a>
        <h2 class="accord1">
            Hostel Availability
        </h2>
        <div>
            <asp:Repeater ID="rptHostel" runat="server" OnItemCommand="rptHostel_ItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                CourseName
                            </th>
                            <th>
                                Hostel Category
                            </th>
                            <th>
                                Has Ac
                            </th>
                            <th>
                                Has Laundry
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                            Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Container .ItemIndex +1 %>
                        </td>
                        <td>
                            <%# Eval("CourseName")%>
                        </td>
                        <td>
                            <%# Eval("HostelCategoryName")%>
                        </td>
                        <td>
                            <%# Eval("IsCollegeBranchCourseHostelHasAC")%>
                        </td>
                        <td>
                            <%# Eval("IsCollegeBranchCourseHostelHasLoundry")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseHostelStatus")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeBranchCourseHostelId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="accordion1">
        <a href="#" id="sndFacauilty" onclick="OpenPoup('divFacalityInsert','650','sndFacauilty');return false;" class="anchor">Add Facility</a>
        <h2 class="accord1">
            Facilities Available
        </h2>
        <div>
            <asp:Repeater ID="rptCourseFacality" runat="server" OnItemCommand="RptCourseFacalityItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                CourseName
                            </th>
                            <th>
                                Facality
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                            Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Container .ItemIndex +1 %>
                        </td>
                        <td>
                            <%# Eval("CourseName")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseFacilitieName")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseFacilitieStatus")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeBranchCourseFacilitieId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="accordion1">
        <a href="#" id="sndRank" onclick="OpenPoup('divRankInsert','650','sndRank');return false;" class="anchor">Add Rank</a>
        <h2 class="accord1">
            Ranking of College<small>(if any)</small>
        </h2>
        <div>
            <asp:Repeater ID="rptRankSource" runat="server" OnItemCommand="RptRankSourceItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                CourseName
                            </th>
                            <th>
                                Year
                            </th>
                            <th>
                                OverAll
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                            Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Container .ItemIndex +1 %>
                        </td>
                        <td>
                            <%# Eval("CourseName")%>
                        </td>
                        <td>
                            <%# Eval("CollegeRankYear")%>
                        </td>
                        <td>
                            <%# Eval("CollegeOverAllRank")%>
                        </td>
                        <td>
                            <%# Eval("CollegeRankStatus")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeRankId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="accordion1">
        <a href="#" id="sndHigh" onclick="OpenPoup('divHighLightsInsert','650','sndHigh');return false;" class="anchor">Add HighLights</a>
        <h2 class="accord1">
            HighLights of College 
        </h2>
        <div>
            <asp:Repeater ID="rptHighLights" runat="server" OnItemCommand="rptHighLights_ItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                CourseName
                            </th>
                            <th>
                                HighLights
                            </th>
                            <th>
                                Status
                            </th>
                            <th>
                            Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Container.ItemIndex +1 %>
                        </td>
                        <td>
                            <%# Eval("CourseName")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseHighlight")%>
                        </td>
                        <td>
                            <%# Eval("CollegeBranchCourseHighlightStatus")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CollegeBranchCourseHighlightId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
   

    </fieldset>

    <div class="popup_block width62perc" id="coursePop">
        <a href="#" class="close">
            <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" />
        </a>
        <asp:HiddenField ID="hdnCollegeCourse" runat="server" Value="0" />
        <fieldset>
            <legend>Update Course</legend>
            <ul>
                <li class="width48perc fleft">
                    <label>
                        Course</label>
                    <asp:DropDownList ID="ddlCourse" runat="server" TabIndex="1" title="Please Select Course">
                    </asp:DropDownList>
                </li>
                 <li class="width48perc fleft">
                    <label>
                        University</label>
                    <asp:DropDownList ID="ddlUniversity" runat="server" style="width:225px !important;" TabIndex="2" title="Please Select University">
                    </asp:DropDownList>
                </li>
                <li class="width48perc fleft">
                    <label>
                        Url</label>
                    <input id="txtCourseUrl" runat="server" type="text" tabindex="4" title="Please Enter Course Url" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Title</label>
                    <input id="txtCourseTitle" runat="server" type="text" tabindex="3" title="Please Enter Course Meta Tag" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Meta Tag</label>
                    <input id="txtCourseMetaTag" runat="server" type="text"  tabindex="3" title="Please Enter Course Meta Tag" />
                </li>
                
                <li class="width48perc fleft">
                    <label>
                        Meta Desc</label>
                    <input id="txtCourseMetaDesc" runat="server" type="text" tabindex="5" title="Please Enter Course Meta Tag" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Establishment</label>
                    <input id="txtCourseEst" type="text" runat="server" tabindex="6" title="Please Enter College Popular Name" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Helpline No</label>
                    <input id="txtHelplineNoEdit" type="text" runat="server" tabindex="7" title="Please Enter Course Helpline No" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Has Hostel</label>
                    <asp:CheckBox ID="chkHasHostel" TabIndex="3" ToolTip="Please Select Hostel" runat="server" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Sponser</label>
                    <asp:CheckBox ID="chkSponserStatus" runat="server" TabIndex="8" ToolTip="Please Checked" />
                </li>
                <li class="width48perc fleft clear">
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkCollegeCourse" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        IsBookSeatVisible</label>
                    <asp:CheckBox ID="chkIsBookSeatVisibleEdit" runat="server" TabIndex="10" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                        Description</label>
                    <textarea id="txtCourseDesc" tabindex="11" runat="server" title="Please Enter College Popular Name"></textarea>
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="CourseUpdate" runat="server" Text="Update" TabIndex="12" ToolTip="Please Select" OnClick="CourseUpdate_Click" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block" style="width:650px !important;"  id="divStream">
        
        <a href="#" class="close">
            <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" />
        </a>
        <asp:HiddenField ID="hdnCourseStream" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCollegeBranchCourseId" runat="server" Value="0" />
        <fieldset>
            <legend>Stream</legend>
            <ul>
                <li>
                    <label>
                        Stream</label>
                    <asp:DropDownList ID="ddlCourseStream" runat="server" TabIndex="1" style="width:225px !important;" title="Please Select Stream">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Stream Mode</label>
                    <asp:RadioButtonList runat="server" CssClass="RadioButtonList" RepeatDirection="Horizontal" ID="rbtCourseStream">
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        Stream Seat
                    </label>
                    <asp:TextBox ID="txtStreamSeat" runat="server" TabIndex="3" ToolTip="Please Enter Stream Seat"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Duration</label>
                    <asp:TextBox ID="txtStreamDuration" runat="server" TabIndex="4" ToolTip="Please Enter Stream Duration"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Stream Fee</label>
                    <asp:TextBox ID="txtStreamFees" runat="server" TabIndex="5" ToolTip="Please Enter Stream Fees"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Eligibility</label>
                    <asp:TextBox ID="txtStreamEligibilty" runat="server" TabIndex="6" ToolTip="Please Enter Stream Eligibilty"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Quota Seat</label>
                    <asp:TextBox ID="txtStreamQuotaSeat" runat="server" TabIndex="7" ToolTip="Please Enter Stream Quota Seat"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Lateral Seat</label>
                    <asp:TextBox ID="txtLateralSeat" runat="server" TabIndex="8" ToolTip="Please Enter Stream Lateral Seat"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkStreamStatus" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                        Description</label>
                    <textarea id="txtStreamDesc" tabindex="8" runat="server" title="Please Enter Description"></textarea>
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="brnStream" runat="server" Text="Update" TabIndex="9" ToolTip="Please Select" OnClick="BtnStreamUpdateClick" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block width40perc" id="divExam">
        <a href="#" class="close">
            <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>
        <asp:HiddenField ID="hdnExamId" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCourseExamId" runat="server" Value="0" />
        <fieldset>
            <legend>Exam</legend>
            <ul>
                <li>
                    <label>
                        Exam</label>
                    <asp:DropDownList ID="ddlExam" runat="server" TabIndex="1" title="Please Select Stream">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkCourseExam" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                        College Exam Elogibilty</label>
                    <asp:TextBox ID="txtCollegeExamEligibity" runat="server" TextMode="MultiLine"  ></asp:TextBox>
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnExam" runat="server" Text="Update" TabIndex="9" ToolTip="Please Select" OnClick="btnExam_Click" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block width40perc" id="divFacality">
        <a href="#" class="close">
            <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>
        <asp:HiddenField ID="hdnFacalityId" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCourseFacalityId" runat="server" Value="0" />
        <fieldset>
            <legend>Facility</legend>
            <ul>
                <li>
                    <label>
                        Facility</label>
                    <asp:TextBox ID="txtCourseFacality" runat="server" ToolTip="Please Enter Facility" TabIndex="1"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Description</label>
                    <asp:TextBox ID="txtFacalityDesc" runat="server" ToolTip="Please Enter Facility" TextMode="MultiLine" TabIndex="2"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkFacalityStatus" runat="server" TabIndex="3" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnCourseFacality" runat="server" Text="Update" TabIndex="4" ToolTip="Please Select" OnClick="btnCourseFacality_Click" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block width40perc" id="divRank">
        <a href="#" class="close">
            <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>
        <asp:HiddenField ID="hdnRankId" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCourseRankId" runat="server" Value="0" />
        <fieldset>
            <legend>Rank</legend>
            <ul>
                <li>
                    <label>
                        Facality</label>
                    <asp:DropDownList ID="ddlRankSource" runat="server" ToolTip="Please Select Rank source" TabIndex="1">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Facality</label>
                    <asp:TextBox ID="txtRanKYear" runat="server" ToolTip="Please Enter Rank year" TabIndex="2"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Description</label>
                    <asp:TextBox ID="txtRankOverall" runat="server" TextMode="MultiLine" ToolTip="Please Enter Rank OverAll" TabIndex="3"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkRankStatus" runat="server" TabIndex="3" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnRankOverAll" runat="server" Text="Update" TabIndex="4" ToolTip="Please Submit" OnClick="btnRankOverAll_Click" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block width40perc" id="divHighLights">
        <a href="#" class="close">
            <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>
        <asp:HiddenField ID="hdnHighLights" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCourseHighLightsId" runat="server" Value="0" />
        <fieldset>
            <legend>HighLights</legend>
            <ul>
                <li>
                    <label>
                        HighLights</label>
                    <asp:TextBox ID="txtCourseHighLights" TextMode="MultiLine" runat="server" ToolTip="Please Enter HighLights" TabIndex="2"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkHighLights" runat="server" TabIndex="3" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnHighLights" runat="server" Text="Update" TabIndex="4" ToolTip="Please Submit" OnClick="btnHighLights_Click" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block width40perc" id="divHostel">
        <a href="#" class="close">
            <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>
        <asp:HiddenField ID="hdnHostelId" runat="server" Value="0" />
        <asp:HiddenField ID="hdnHostelCourseId" runat="server" Value="0" />
        <fieldset>
            <legend>Hostel</legend>
            <ul>
                <li>
                    <label>
                        Stream</label>
                    <asp:DropDownList ID="ddlHostelMaster" runat="server" TabIndex="1" title="Please Select Hostel">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Location
                    </label>
                    <asp:TextBox ID="txtHostelLocation" runat="server" TabIndex="2" ToolTip="Please Enter Location"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Charge</label>
                    <asp:TextBox ID="txtHostelCharge" runat="server" TabIndex="3" ToolTip="Please Enter Charge"></asp:TextBox>
                </li>
                <li>
                    <label>
                        AC</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtAc" CssClass="RadioButtonList" >
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        Laundry</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtLoundary" CssClass="RadioButtonList">
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        PowerBackUp</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtPower" CssClass="RadioButtonList" >
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        InterNet</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtInternet" CssClass="RadioButtonList" >
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkHostel" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnHostel" runat="server" Text="Update" TabIndex="9" ToolTip="Please Select" OnClick="BtnHostelClick" />
                </li>
            </ul>
        </fieldset>
    </div>



    <div class="popup_block width62perc" id="divCourseInsert">
        <fieldset>
            <legend>Insert Course</legend>
            <ul>
               <li class="width48perc fleft">
                    <label>
                        Course</label>
                    <asp:DropDownList ID="ddlCourseInsert" runat="server" TabIndex="1" title="Please Select Course">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvCOurseInsertMain" CssClass="error" Display="Dynamic" ValidationGroup="CourseInsert" ControlToValidate="ddlCourseInsert" InitialValue="0">Select course</asp:RequiredFieldValidator>
                </li>
                <li class="width48perc fleft">
                    <label>
                        University</label>
                    <asp:DropDownList ID="ddlUniversityInsert" style="width:225px !important;" runat="server" TabIndex="2" title="Please Select University">
                    </asp:DropDownList>
                </li>
                <li class="width48perc fleft">
                    <label>
                        Url</label>
                    <input id="txtUrlInsert" runat="server" type="text" tabindex="4" title="Please Enter Course Url" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Title</label>
                    <input id="txtTitleInsert" runat="server" type="text" tabindex="3" title="Please Enter Course Meta Tag" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Meta Tag</label>
                    <input id="txtMetaTagInsert" runat="server" type="text" tabindex="3" title="Please Enter Course Meta Tag" />
                </li>
                
               <li class="width48perc fleft">
                    <label>
                        Meta Desc</label>
                    <input id="txtMetaDescInsert" runat="server" type="text" tabindex="5" title="Please Enter Course Meta Tag" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Establishment</label>
                    <input id="txtEstInsert" type="text" runat="server" tabindex="6" title="Please Enter College Popular Name" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Helpline No</label>
                    <input id="txtHelplineNoInsert" type="text" runat="server" tabindex="6" title="Please Enter Course Helpline No" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Has Hostel</label>
                    <asp:CheckBox ID="chkHasHostelInsert" TabIndex="3" ToolTip="Please Select Hostel" runat="server" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Sponsor</label>
                    <asp:CheckBox ID="chkSponserStatusInsert" runat="server" TabIndex="7" ToolTip="Please Checked" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkCollegeCourseInsert" runat="server" TabIndex="8" ToolTip="Please Checked" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        IsBookSeatVisible</label>
                    <asp:CheckBox ID="chkIsBookSeatVisibleInsert" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                        Description</label>
                    <textarea id="txtDescInsert" tabindex="10" runat="server" title="Please Enter College Popular Name"></textarea>
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnCourseInsert" runat="server" Text="Submit" TabIndex="11" ToolTip="Please Select" ValidationGroup="CourseInsert" OnClick="btnCourseInsert_Click" />
                </li>
            </ul>
        </fieldset>
    </div>

    <div class="popup_block" style="width:650px !important;"  id="divCourseStreamInsert" >
        <fieldset>
            <legend>Stream</legend>
            <ul>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                         <li>
                            <label>
                                Course</label>
                            <asp:DropDownList ID="ddlCourses" runat="server" TabIndex="1" title="Please Select Course" AutoPostBack="True" OnSelectedIndexChanged="DdlCourseStreamSelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvCoursesInsert" Display="Dynamic" ValidationGroup="coursesinsert" ControlToValidate="ddlCourses" InitialValue="0">Select course</asp:RequiredFieldValidator>
                        </li>
                          <li>
                            <label>
                                Stream</label>
                            <asp:DropDownList ID="ddlCourseStreamInsert" style="width:225px !important;" runat="server" TabIndex="1" title="Please Select Stream">
                            </asp:DropDownList>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                 <li>
                    <label>
                        Stream Mode</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtCourseStreamInsert" CssClass="RadioButtonList" >
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        Stream Seat
                    </label>
                    <asp:TextBox ID="txtStreamSeatInsert" runat="server" TabIndex="3" ToolTip="Please Enter Stream Seat"></asp:TextBox>
                </li>
                 <li>
                    <label>
                        Duration</label>
                    <asp:TextBox ID="txtStreamDurationInsert" runat="server" TabIndex="4" ToolTip="Please Enter Stream Duration"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Stream Fee</label>
                    <asp:TextBox ID="txtStreamFeesInsert" runat="server" TabIndex="5" ToolTip="Please Enter Stream Fees"></asp:TextBox>
                </li>
                 <li>
                    <label>
                        Eligibility</label>
                    <asp:TextBox ID="txtStreamEligibiltyINsert" runat="server" TabIndex="6" ToolTip="Please Enter Stream Eligibilty"></asp:TextBox>
                </li>
                 <li>
                    <label>
                        Quota Seat</label>
                    <asp:TextBox ID="txtStreamQuotaSeatInsert" runat="server" TabIndex="7" ToolTip="Please Enter Stream Quota Seat"></asp:TextBox>
                </li>
                 <li>
                    <label>
                        Lateral Seat</label>
                    <asp:TextBox ID="txtLateralSeatInsert" runat="server" TabIndex="8" ToolTip="Please Enter Stream Lateral Seat"></asp:TextBox>
                </li>
                 <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkStreamStatusInsert" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                        Description</label>
                    <textarea id="txtStreamInsertDesc" tabindex="8" runat="server" title="Please Enter Description"></textarea>
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnStreamInsert" ValidationGroup="coursesinsert" runat="server" Text="Add Stream" ToolTip="Please Select" OnClick="BtnStreamInsertClick" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block" style="width: 450px" id="divExamInsert">
        <fieldset>
            <legend>Exam</legend>
            <ul>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <li>
                            <label>
                                Course</label>
                            <asp:DropDownList ID="ddlCoursesExam" AutoPostBack="True" runat="server" TabIndex="1" title="Please Select Course" OnSelectedIndexChanged="DdlCourseExamSelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvExamInsert" Display="Dynamic" ValidationGroup="examinsert" ControlToValidate="ddlCoursesExam" InitialValue="0">Select course</asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                Exam</label>
                            <asp:DropDownList ID="ddlExamInsert" runat="server" TabIndex="1" title="Please Select Stream">
                            </asp:DropDownList>
                        </li>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkCourseExamInsert" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                        College Exam Eligibity</label>
                    <asp:TextBox ID="txtCollegeExamEligibilty" runat="server" TextMode="MultiLine"></asp:TextBox>
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnExamInsert" runat="server" Text="Add Exam" ValidationGroup="examinsert" TabIndex="9" ToolTip="Please Select" OnClick="BtnExamInsertClick" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block" style="display: none; width: 450px" id="divFacalityInsert">
        <fieldset>
            <legend>Facility</legend>
            <ul>
                <li>
                    <label>
                        Course</label>
                    <asp:DropDownList ID="ddlCoursesFacality" runat="server" TabIndex="1" title="Please Select Course">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvFacalityCourse" Display="Dynamic" ValidationGroup="faclityinsert" ControlToValidate="ddlCoursesFacality" InitialValue="0">Select course</asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Facility</label>
                    <asp:TextBox ID="txtCourseFacalityInsert" runat="server" ToolTip="Please Enter Facility" TabIndex="1"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Description</label>
                    <asp:TextBox ID="txtFacalityDescInsert" runat="server" ToolTip="Please Enter Facility" TextMode="MultiLine" TabIndex="2"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkFacalityStatusInsert" runat="server" TabIndex="3" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnCourseFacalityInsert" runat="server" ValidationGroup="faclityinsert" Text="Add Facility" TabIndex="4" ToolTip="Please Select" OnClick="BtnCourseFacalityInsertClick" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block" style="display: none; width: 450px" id="divRankInsert">
        <fieldset>
            <legend>Rank</legend>
            <ul>
                <li>
                    <label>
                        Course</label>
                    <asp:DropDownList ID="ddlCoursesRank" runat="server" TabIndex="1" title="Please Select Course">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvRank" Display="Dynamic" ValidationGroup="rankinsert" ControlToValidate="ddlCoursesRank" InitialValue="0">Select course</asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Tank Sourse</label>
                    <asp:DropDownList ID="ddlRankSourceInsert" runat="server" ToolTip="Please Select Rank source" TabIndex="1">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Year</label>
                    <asp:TextBox ID="txtRanKYearInsert" runat="server" ToolTip="Please Enter Rank year" TabIndex="2"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Rank Overall</label>
                    <asp:TextBox ID="txtRankOverallInsert" runat="server" ToolTip="Please Enter Rank OverAll" TabIndex="3"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkRankStatusInsert" runat="server" TabIndex="3" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnRankOverAllInsert" runat="server" Text="Add Rank" TabIndex="4" ValidationGroup="rankinsert" ToolTip="Please Submit" OnClick="btnRankOverAllInsert_Click" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block" style="display: none; width: 450px" id="divHighLightsInsert">
        <fieldset>
            <legend>HighLights</legend>
            <ul>
                <li>
                    <label>
                        Course</label>
                    <asp:DropDownList ID="ddlCoursesHigh" runat="server" TabIndex="1" title="Please Select Course">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvHighLights" Display="Dynamic" ValidationGroup="highLightsinsert" ControlToValidate="ddlCoursesHigh" InitialValue="0">Select course</asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        HighLights</label>
                    <asp:TextBox ID="txtCourseHighLightsInsert" runat="server" ToolTip="Please Enter HighLights" TabIndex="2"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkHighLightsInsert" runat="server" TabIndex="3" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnHighLightsInsert" runat="server" Text="Add HighLights" ValidationGroup="highLightsinsert" TabIndex="4" ToolTip="Please Submit" OnClick="btnHighLightsInsertClick" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div class="popup_block" style="display: none; width: 500px" id="divHostelInsert">
        <fieldset>
            <legend>Hostel</legend>
            <ul>
                <li>
                    <label>
                        Course</label>
                    <asp:DropDownList ID="ddlCoursesHostel" runat="server" TabIndex="1" title="Please Select Course">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvHostelCOurse" Display="Dynamic" ValidationGroup="hostelinsert" ControlToValidate="ddlCoursesHostel" InitialValue="0">Select course</asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Hostel Category</label>
                    <asp:DropDownList ID="ddlHostelMasterInsert" runat="server" TabIndex="1" title="Please Select Hostel">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Location
                    </label>
                    <asp:TextBox ID="txtHostelLocationInsert" runat="server" TabIndex="2" ToolTip="Please Enter Location"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Charge</label>
                    <asp:TextBox ID="txtHostelChargeInsert" runat="server" TabIndex="3" ToolTip="Please Enter Charge"></asp:TextBox>
                </li>
                <li>
                    <label>
                        AC</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtAcInsert" CssClass="RadioButtonList" >
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        Laundry</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtLoundaryInsert"  CssClass="RadioButtonList" >
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        PowerBackUp</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtPowerInsert"  CssClass="RadioButtonList" >
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        Internet</label>
                    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rbtInternetInsert"  CssClass="RadioButtonList" >
                        <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkHostelInsert" runat="server" TabIndex="9" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                    </label>
                    <asp:Button ID="btnHostelInsert" runat="server" Text="Add Hostel" TabIndex="9" ToolTip="Please Select" ValidationGroup="hostelinsert" OnClick="btnHostelInsertClick" />
                </li>
            </ul>
        </fieldset>
    </div>



    
    <script type="text/javascript" defer="defer">
        $(document).ready(function () {

            $(".accordion1 div:first").addClass("hide");
            $(".accordion1 div:not(:first)").hide();

            $(".accordion1 h2").click(function () {
                $(this).next("div").removeClass("hide");
                $(this).next("div").slideToggle("fast")
		.siblings("div:visible").slideUp("fast");
                $(this).toggleClass("active");
                $(this).siblings("h2").removeClass("active");
            });


        });


        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $(".accordion1 div:first").addClass("hide");
                $(".accordion1 div:not(:first)").hide();

                $(".accordion1 h2").click(function () {
                    $(this).next("div").removeClass("hide");
                    $(this).next("div").slideToggle("fast")
		.siblings("div:visible").slideUp("fast");
                    $(this).toggleClass("active");
                    $(this).siblings("h2").removeClass("active");
                });

            }
        }

    </script>
    <script src="../JS/CollegeBranch.js" type="text/javascript"></script>
    <script type="text/javascript">

        function openpop(divid) {

            var popMargTop = ($('#' + divid).height() + 80) / 2;
            var popMargLeft = ($('#' + divid).width() + 80) / 2;
            $('#' + divid).css({
                           
            });
            $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
            $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
            $("#" + divid).show();
        }
        $('a.close, #fade').click(function () { //When clicking on the close or fade layer...
            $('#fade , .popup_block').fadeOut(function () {

                $('#fade, a.close').remove();
            }); //fade them both out

            return false;
        });
    </script>
    <style type="text/css">
        .error1
        {
            color: red;
            font-style: italic;
            font-size: 11px;
        }
    </style>
</asp:Content>
