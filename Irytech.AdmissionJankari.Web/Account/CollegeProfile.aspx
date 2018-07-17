<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeProfile.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.Account.CollegeProfile" MasterPageFile="~/themes/Blue/Site.Master" %>

<%@ Register TagPrefix="AJ" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<%@ Register TagName="Testimonial" TagPrefix="Aj" Src="~/UserControl/FckEditorCostomize.ascx" %>
<%@ Register TagName="AddProduct" TagPrefix="Aj" Src="~/UserControl/AddProduct.ascx" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="popup_block" style="width: 350px !important" id="divUserImage">
             <input type="file" id="fulCollegeImage"    >
               <input type="button" id="btnUploadCollegeImage" onclick="UploadCollegeImage()"  value="Upload College Logo" class="button" />
               <br />
                <br />
               <label  id="message" class="success hide"></label>
               
            </div>
            <div id="tabBasic" style="overflow: hidden; border: 1px solid #e1e1e1; border-radius: 5px;">
                <ul class="tabs" id="ulBasics" style="background-color: transparent; height: 41px;">
                    <li onclick="SetOuterTab('tabs1',this,'ulBasics')" class="active" id="litabs1" style="width: 90px!important;
                        height: 38px!important; display: inline-block; text-align: center; font-size: 14px;">
                        <a href="javascript:void(0)" class="cursor">College Details</a> </li>
                    <li onclick="SetOuterTab('tabs2',this,'ulBasics')" id="litabs2" style="width: 90px!important;
                        height: 38px!important; display: inline-block; text-align: center; font-size: 14px;">
                        <a href="javascript:void(0)" class="cursor">Your Query</a> </li>
                    <li onclick="SetOuterTab('tabs3',this,'ulBasics')" id="litabs3" style="width: 120px!important;
                        height: 38px!important; display: inline-block; text-align: center; font-size: 14px;">
                        <a href="javascript:void(0)" class="cursor">Your Visitors</a> </li>
                    <li id="liTab" onclick="SetOuterTab('tabAdvertise1',this,'ulBasics');GetProductCount();"
                        runat="server" visible="False" style="width: 150px!important; height: 38px!important;
                        display: inline-block; text-align: center; font-size: 14px;"><a class="cursor">Advertise
                            With Us</a> </li>
                    <li onclick="SetOuterTab('divYourAdvertise',this,'ulBasics')" clientidmode="Static"
                        id="liAdvertiseList" runat="server" visible="False" style="width: 150px!important;
                        height: 38px!important; display: inline-block; font-size: 14px;"><a href="javascript:void(0)"
                            onclick="GetPaymentProductList()" class="cursor">Your Advertisement</a>
                    </li>
                    <li clientidmode="Static" onclick="SetOuterTab('divBanner',this,'ulBasics')" id="liBanner"
                        runat="server" visible="False"><a href="javascript:void(0)" class="cursor">Banner</a>
                    </li>
                </ul>
                <div class="clearBoth">
                </div>
                <asp:HiddenField ID="hdnCollegeId" runat="server" ClientIDMode="Static" />
                <p class="g-toolTip" style="padding: 15px 0px 0px 10px;">
                    <strong id="colgeName"></strong><span id="colgeName1"></span>
                </p>
                <hr class="hrline" />
                <div class="tab_container">
                    <div class="tab_content fleft" id="tabs1">
                        <div id="tabss1" style="min-width: 1100px; overflow: hidden;">
                            <ul class="tabs" id="ulTopRanked">
                                <li onclick="SetInnerTab('tab1',this,'ulTopRanked')" class="active" id="litab1"><a
                                    href="javascript:void(0)" class="cursor">College Basic Details</a> </li>
                                <li onclick="SetInnerTab('tab2',this,'ulTopRanked')" id="litab2"><a href="javascript:void(0)"
                                    class="cursor">College Courses</a> </li>
                                <li onclick="SetInnerTab('tab3',this,'ulTopRanked')" id="litab3"><a href="javascript:void(0)"
                                    class="cursor">College Streams</a> </li>
                                <li onclick="SetInnerTab('tab4',this,'ulTopRanked')" id="litab4"><a href="javascript:void(0)"
                                    class="cursor">College Exams</a> </li>
                                <li onclick="SetInnerTab('tab5',this,'ulTopRanked')" id="litab5"><a href="javascript:void(0)"
                                    class="cursor">College Hostel</a> </li>
                                <li onclick="SetInnerTab('tab6',this,'ulTopRanked')" id="litab6"><a href="javascript:void(0)"
                                    class="cursor">College Rank</a> </li>
                                <li onclick="SetInnerTab('tab7',this,'ulTopRanked')" id="litab7"><a href="javascript:void(0)"
                                    class="cursor">College Highlights</a> </li>
                                <li onclick="SetInnerTab('tab8',this,'ulTopRanked')" id="litab8"><a href="javascript:void(0)"
                                    class="cursor">College Placement</a> </li>
                                <li onclick="SetInnerTab('tab9',this,'ulTopRanked')" id="litab9"><a href="javascript:void(0)"
                                    class="cursor">College Events</a> </li>
                                <li onclick="SetInnerTab('tab10',this,'ulTopRanked')" id="litab10"><a href="javascript:void(0)"
                                    class="cursor">College Notices</a> </li>
                                <li onclick="SetInnerTab('tab11',this,'ulTopRanked')" id="litab11"><a href="javascript:void(0)"
                                    class="cursor">College Testimonial</a> </li>
                            </ul>
                        </div>
                        <asp:HiddenField ID="hdnImageFile" runat="server" />
                        <asp:HiddenField ID="hdnCOuntryId" runat="server" />
                        <div class="tab_container clgProfDiv">
                            <div class="tab_content1 fleft" id="tab1">
                                <fieldset id="basicInfo" class="field width42Percent fleft clgBg" style="min-height: 300px;
                                    border: 0px solid;">
                                    <legend>College Basic Info</legend>
                                    <ul>
                                        <li>
                                            <asp:Label runat="server" ID="lblBasicDetailsMsg" CssClass="clgsubmitsms" ClientIDMode="Static"
                                                Style="display: block;" Visible="False"></asp:Label></li>
                                        <li>
                                            <label>
                                                &nbsp;</label>
                                            <asp:Image runat="server" ID="imgCollege" Height="80px" Width="80px" ClientIDMode="Static"
                                                CssClass="fleft clgProImg"></asp:Image>
                                            <br />
                                            <a href="#" id="sndImage" onclick="OpenPoup('divUserImage',440,'sndImage');return false;"
                                                title="Change college image">Change Image</a> </li>
                                        <li>
                                            <label>
                                                College Name</label>
                                            <strong>
                                                <asp:Label runat="server" ID="lblCollegeName"></asp:Label>
                                            </strong></li>
                                        <li>
                                            <label>
                                                Management</label>
                                            <asp:DropDownList runat="server" ID="ddlCollegeMgt" ClientIDMode="Static" TabIndex="2"
                                                ToolTip="Select college management">
                                            </asp:DropDownList>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="rfvCollegeMgt" ClientIDMode="Static" runat="server" />
                                                <span id="spnCollegeManagementError" class="error hide"></span></span></li>
                                        <li>
                                            <label>
                                                Establishment</label>
                                            <asp:TextBox ID="txtCollegeEst" ClientIDMode="Static" runat="server" TabIndex="3"
                                                ToolTip="Enter college establishment"></asp:TextBox>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="rfvCollegeEst" runat="server" ClientIDMode="Static" />
                                                <span id="spnCollegeest" class="error hide"></span>
                                                <asp:HiddenField ID="revCollegeEst" runat="server" ClientIDMode="Static" />
                                            </span></li>
                                        <li>
                                            <label>
                                                Popular Name</label>
                                            <asp:TextBox ID="txtCollegePopularName" runat="server" TabIndex="4" ToolTip="Enter college popular name"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:HiddenField runat="server" ID="countryId" Value="0"></asp:HiddenField>
                                            <label>
                                                State</label>
                                            <asp:DropDownList runat="server" ID="ddlState" ClientIDMode="Static" TabIndex="5"
                                                ToolTip="Select state">
                                            </asp:DropDownList>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="rfvState" runat="server" ClientIDMode="Static" />
                                                <span id="spnCollegeBasicState" class="error hide"></span></span></li>
                                        <li>
                                            <label>
                                                City</label>
                                            <asp:DropDownList runat="server" ID="ddlCollegeCity" ClientIDMode="Static" TabIndex="6"
                                                ToolTip="Select city">
                                            </asp:DropDownList>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="rfvCity" runat="server" ClientIDMode="Static" />
                                                <span id="spnCollegeBasicCity" class="error hide"></span></span></li>
                                        <li>
                                            <label>
                                                Website</label>
                                            <asp:TextBox ID="txtCollegeWebsite" runat="server" TabIndex="7" ToolTip="Enter college website"></asp:TextBox>
                                        </li>
                                        <li>
                                            <label>
                                                Description</label>
                                            <asp:TextBox ID="txtCollegeDesc" runat="server" TabIndex="8" TextMode="MultiLine"
                                                Style="max-width: 265px; height: 100px;" ToolTip="Enter college description"></asp:TextBox>
                                        </li>
                                    </ul>
                                </fieldset>
                                <fieldset id="basicInfoContact" class="field width42Percent fleft clgBg" style="min-height: 445px;
                                    margin-left: -4px; border: 0px solid;">
                                    <legend>Contact Details</legend>
                                    <ul>
                                        <li>
                                            <label>
                                                Email</label>
                                            <asp:TextBox ID="txtEmailId" runat="server" TabIndex="9" ClientIDMode="Static" ToolTip="Enter your email id"></asp:TextBox>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="rfvEmailId" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="revEmailId" runat="server" ClientIDMode="Static" />
                                                <span id="spnCollegeBasicEmailId" class="error hide"></span></span></li>
                                        <li>
                                            <label>
                                                Mobile</label>
                                            <asp:TextBox ID="txtCollegeMobile" runat="server" ClientIDMode="Static" TabIndex="10"
                                                ToolTip="Enter your 10 digit mobile number"></asp:TextBox>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="rfvMobile" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="revMobile" runat="server" ClientIDMode="Static" />
                                                <span id="spnCollegeBasicMobile" class="error hide"></span></span></li>
                                        <li>
                                            <label>
                                                Pincode
                                            </label>
                                            <asp:TextBox ID="txtPinCode" runat="server" TabIndex="11" ClientIDMode="Static" ToolTip="Enter pincode"></asp:TextBox>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="revCollegePinCode" runat="server" ClientIDMode="Static" />
                                                <span id="spnCollegeBasicPincode" class="error hide"></span></span></li>
                                        <li>
                                            <label>
                                                Fax</label>
                                            <asp:TextBox ID="txtCollegeFax" ClientIDMode="Static" runat="server" TabIndex="12"
                                                ToolTip="Enter college fax"></asp:TextBox>
                                            <span class="errormsgSpan">
                                                <asp:HiddenField ID="revFax" runat="server" ClientIDMode="Static" />
                                                <span id="spnCollegeBasicFax" class="error hide"></span></span></li>
                                        <li>
                                            <label>
                                                Address</label>
                                            <asp:TextBox ID="txtAddress" runat="server" TabIndex="13" ToolTip="Enter college address"
                                                Style="max-width: 265px; height: 120px;" TextMode="MultiLine"></asp:TextBox>
                                        </li>
                                        <li>
                                            <label>
                                                &nbsp;</label>
                                            <asp:Button ID="Button1" runat="server" CssClass="button" ValidationGroup="collegeUpdate"
                                                Text="Update College" TabIndex="14" OnClientClick="return ValidiateCollegeBasicProfile()"
                                                OnClick="BtnSubmitCollegeBasicInfoClick" ToolTip="Click to update college details" />
                                        </li>
                                    </ul>
                                </fieldset>
                                <ul>
                                    <li>
                                        <div id="divImage1" style="display: none" >
                                            <label style="color: red; font-size: 16px">
                                                Processing...</label>
                                            <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab_content1" id="tab2">
                                <div id="divCourseInsert" class="field width42Percent fleft">
                                    <asp:HiddenField runat="server" ID="hdnCollegeCourseId" ClientIDMode="Static" />
                                    <fieldset class="clgBg">
                                        <legend>College Courses</legend>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblCourseMsg" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="False"></asp:Label></li>
                                            <li>
                                                <label>
                                                    <strong>
                                                        <%=Resources.label.Course %></strong></label>
                                                <asp:DropDownList ID="ddlCourseInsert" runat="server" ClientIDMode="Static" TabIndex="1"
                                                    ToolTip="Select course">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator CssClass="error" runat="server" ID="rfvCOurseInsertMain"
                                                        Display="Dynamic" ValidationGroup="CourseInsert" ControlToValidate="ddlCourseInsert"
                                                        InitialValue="0">Select course</asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    University:</label>
                                                <asp:DropDownList ID="ddlUniversityInsert" ClientIDMode="Static" runat="server" TabIndex="2"
                                                    ToolTip="Select university">
                                                </asp:DropDownList>
                                            </li>
                                            <li>
                                                <label>
                                                    Establishment:</label>
                                                <asp:TextBox ID="txtEstInsert" runat="server" ClientIDMode="Static" ToolTip="Enter course establishment"
                                                    TabIndex="3" />
                                                <span class="errormsgSpan">
                                                    <asp:RegularExpressionValidator runat="server" ID="revCourseEst" Display="Dynamic"
                                                        CssClass="error" ControlToValidate="txtEstInsert" SetFocusOnError="True" ValidationGroup="CourseInsert"></asp:RegularExpressionValidator>
                                                    <asp:RangeValidator ID="rgCourseEst" runat="server" Display="Dynamic" CssClass="error"
                                                        SetFocusOnError="True" ValidationGroup="CourseInsert" ControlToValidate="txtEstInsert">Enter Establishment less than current year</asp:RangeValidator>
                                                </span></li>
                                            <li>
                                                <label>
                                                    Has Hostel:</label>
                                                <asp:CheckBox ID="chkHasHostelInsert" ClientIDMode="Static" ToolTip="Check hostel status"
                                                    runat="server" TabIndex="4" />
                                                <p>
                                                    Is hostel available for this course
                                                </p>
                                            </li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkCourseStatus" ToolTip="Check course status" ClientIDMode="Static"
                                                    runat="server" TabIndex="5" />
                                            </li>
                                            <li>
                                                <label>
                                                </label>
                                                <asp:Button ID="btnCourseInsert" runat="server" ClientIDMode="Static" Text="Insert"
                                                    TabIndex="6" ToolTip="Click to finish process" ValidationGroup="CourseInsert"
                                                    CssClass="button" OnClick="BtnCourseInsertClick" />
                                                <div id="divCourseImage" style="display: none">
                                                    <label style="color: red; font-size: 16px">
                                                        Processing...</label>
                                                    <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                                </div>
                                            </li>
                                        </ul>
                                    </fieldset>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptCourse" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course
                                                    </td>
                                                    <td>
                                                        Edit
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#(ucCustomPaging.PageSize*ucCustomPaging.CurrentPageIndex)+Container.ItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <%# Eval("CourseName")%>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="GetCollegeCourseDeatils(<%# Eval("CollegeBranchCourseId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                                </div>
                            </div>
                            <div class="tab_content1" id="tab3">
                                <div id="div2" class="field width42Percent fleft">
                                    <asp:HiddenField runat="server" ID="hdnCollegeCourseStreamId" ClientIDMode="Static" />
                                    <fieldset class="clgBg">
                                        <legend>College Streams</legend>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblCourseStreamMsg" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="False"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    <strong>
                                                        <%=Resources.label.Course %></strong></label>
                                                <asp:DropDownList ID="ddlCourseStream" ClientIDMode="Static" runat="server" TabIndex="1"
                                                    title="Select course">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvCoursestreamInsert"
                                                        Display="Dynamic" ValidationGroup="StreamInsert" ControlToValidate="ddlCourseStream"
                                                        InitialValue="0">Select course</asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <asp:HiddenField ID="hndStreamId" runat="server" ClientIDMode="Static" />
                                                <label>
                                                    Stream:</label>
                                                <asp:DropDownList ID="ddlStream" ClientIDMode="Static" runat="server" TabIndex="2"
                                                    title="Select stream">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvStreamInsert"
                                                        Display="Dynamic" ValidationGroup="StreamInsert" ControlToValidate="ddlStream"
                                                        InitialValue="0">Select stream</asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Study Mode:</label>
                                                <asp:DropDownList ID="ddlStreammode" ClientIDMode="Static" runat="server" TabIndex="3"
                                                    title="Select study mode">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvStreamMode" Display="Dynamic"
                                                        ValidationGroup="StreamInsert" ControlToValidate="ddlStreammode" InitialValue="0">Select study mode</asp:RequiredFieldValidator>
                                                </span></li>
                                            <li>
                                                <label>
                                                    Duration:</label>
                                                <asp:TextBox ID="txtStreamDurationInsert" ClientIDMode="Static" runat="server" TabIndex="4"
                                                    ToolTip="Enter stream duration"></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>
                                                    Stream Fee:</label>
                                                <asp:TextBox ID="txtStreamFeesInsert" ClientIDMode="Static" runat="server" TabIndex="5"
                                                    ToolTip="Enter stream fee"></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>
                                                    Eligibility:</label>
                                                <asp:TextBox ID="txtStreamEligibiltyINsert" ClientIDMode="Static" runat="server"
                                                    TabIndex="6" Style="max-width: 263px;" ToolTip="Enter stream eligibility" TextMode="MultiLine"></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>
                                                    Stream Seats:
                                                </label>
                                                <asp:TextBox ID="txtStreamSeatInsert" ClientIDMode="Static" runat="server" TabIndex="7"
                                                    ToolTip="Enter stream Seats"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RegularExpressionValidator runat="server" ID="revStreamSeat" Display="Dynamic"
                                                        ValidationGroup="StreamInsert" SetFocusOnError="True" CssClass="error" ControlToValidate="txtStreamSeatInsert">  Field Stream Seat in digit
                                                    </asp:RegularExpressionValidator>
                                                </span></li>
                                            <li>
                                                <label>
                                                    Quota Seats:</label>
                                                <asp:TextBox ID="txtStreamQuotaSeatInsert" ClientIDMode="Static" runat="server" TabIndex="8"
                                                    ToolTip="Enter stream quota Seats"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RegularExpressionValidator runat="server" ID="revQuotaSeat" Display="Dynamic"
                                                        ValidationGroup="StreamInsert" SetFocusOnError="True" CssClass="error" ControlToValidate="txtStreamQuotaSeatInsert">  Field Quota Seat in digit
                                                    </asp:RegularExpressionValidator>
                                                </span></li>
                                            <li>
                                                <label>
                                                    Lateral Seats:</label>
                                                <asp:TextBox ID="txtLateralSeatInsert" runat="server" ClientIDMode="Static" TabIndex="9"
                                                    ToolTip="Enter stream lateral Seats"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RegularExpressionValidator runat="server" ID="revLateralSeat" Display="Dynamic"
                                                        ValidationGroup="StreamInsert" SetFocusOnError="True" CssClass="error" ControlToValidate="txtLateralSeatInsert">  Field Lateral Seat in digit
                                                    </asp:RegularExpressionValidator>
                                                </span></li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkStreamStatus" ClientIDMode="Static" ToolTip="Check stream status"
                                                    runat="server" TabIndex="10" />
                                            </li>
                                            <li>
                                                <label>
                                                </label>
                                                <asp:Button ID="btnStreamInsert" ValidationGroup="StreamInsert" ClientIDMode="Static"
                                                    CssClass="button" TabIndex="11" runat="server" Text="Insert" ToolTip="Click to finish process"
                                                    OnClick="BtnStreamInsertClick" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divStreamImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptCourseStream" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course Name
                                                    </td>
                                                    <td>
                                                        Stream Name
                                                    </td>
                                                    <td>
                                                        Stream Seats
                                                    </td>
                                                    <td>
                                                        Fee
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#(CustomPaging2.PageSize * CustomPaging2.CurrentPageIndex) + Container.ItemIndex + 1%>
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
                                                    <a href="#" onclick="GetCollegeCourseStreamDeatils(<%# Eval("CollegeBranchCourseStreamId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    <Aj:CustomPaging ID="CustomPaging2" runat="server" />
                                </div>
                            </div>
                            <div class="tab_content1" id="tab4">
                                <asp:HiddenField ID="hdnExamId" runat="server" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField ID="hdnCourseExamId" runat="server" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField ID="hndSelectedExam" runat="server" ClientIDMode="Static" />
                                <div class="field width42Percent fleft">
                                    <fieldset class="clgBg">
                                        <legend>College Exams</legend>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblExamMsg" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="False"></asp:Label></li>
                                            <li>
                                                <label>
                                                    <strong>
                                                        <%=Resources.label.Course %></strong></label>
                                                <asp:DropDownList ID="ddlCoursesExam" ClientIDMode="Static" runat="server" TabIndex="1"
                                                    title="Select course">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvExamInsert" CssClass="error" Display="Dynamic"
                                                        ValidationGroup="examinsert" ControlToValidate="ddlCoursesExam" InitialValue="0">
                 Select course</asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Exam:</label>
                                                <asp:DropDownList ID="ddlExam" runat="server" ClientIDMode="Static" TabIndex="2"
                                                    title="Select exam">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="efvExam" Display="Dynamic"
                                                        ValidationGroup="examinsert" ControlToValidate="ddlExam" InitialValue="0">
                     Select exam</asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkExamStatus" ClientIDMode="Static" ToolTip="Check exam status"
                                                    runat="server" TabIndex="3" />
                                            </li>
                                            <li>
                                                <label>
                                                </label>
                                                <asp:Button ID="btnExam" runat="server" ClientIDMode="Static" Text="Insert" TabIndex="4"
                                                    CssClass="button" ToolTip="Click to finish process" OnClick="BtnExamClick" ValidationGroup="examinsert" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divExamImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptExam" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course Name
                                                    </td>
                                                    <td>
                                                        Exam Name
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
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
                                                    <a href="#" onclick="GetCollegeCourseExamDeatils(<%# Eval("CollegeBranchCourseExamId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="tab_content1" id="tab5">
                                <div class="field width42Percent fleft">
                                    <fieldset class="clgBg">
                                        <legend>Hostel</legend>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblHostelMsg" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="False"></asp:Label></li>
                                            <li>
                                                <label>
                                                    <strong>
                                                        <%=Resources.label.Course %></strong></label>
                                                <asp:DropDownList ID="ddlCoursesHostel" ClientIDMode="Static" runat="server" TabIndex="1"
                                                    title="Select course">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvHostelCOurse"
                                                        Display="Dynamic" ValidationGroup="hostelinsert" ControlToValidate="ddlCoursesHostel"
                                                        InitialValue="0">Select course</asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Hostel Category:</label>
                                                <asp:DropDownList ID="ddlHostelMasterInsert" ClientIDMode="Static" runat="server"
                                                    TabIndex="2" title="Select hostel category">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvHostelCategory"
                                                        Display="Dynamic" ValidationGroup="hostelinsert" ControlToValidate="ddlHostelMasterInsert"
                                                        InitialValue="0">Select hostel category</asp:RequiredFieldValidator></span> 
                                            </li>
                                            <li>
                                                <label>
                                                    Location:
                                                </label>
                                                <asp:TextBox ID="txtHostelLocationInsert" ClientIDMode="Static" runat="server" TabIndex="3"
                                                    ToolTip="Enter location"></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>
                                                    Charge:</label>
                                                <asp:TextBox ID="txtHostelChargeInsert" ClientIDMode="Static" runat="server" TabIndex="4"
                                                    ToolTip="Enter hostel charge"></asp:TextBox>
                                            </li>
                                            <li>
                                                <label>
                                                    AC:</label>
                                                <asp:RadioButtonList runat="server" ClientIDMode="Static" RepeatDirection="Horizontal"
                                                    TabIndex="5" ID="rbtAcInsert" Width="220px" class="tbl forRadio" ToolTip="Select ac">
                                                    <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </li>
                                            <li>
                                                <label>
                                                    Laundry:</label>
                                                <asp:RadioButtonList runat="server" ClientIDMode="Static" RepeatDirection="Horizontal"
                                                    ID="rbtLoundaryInsert" Width="220px" class="tbl forRadio" TabIndex="6" ToolTip="Select loundary">
                                                    <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </li>
                                            <li>
                                                <label>
                                                    PowerBackUp:</label>
                                                <asp:RadioButtonList runat="server" ClientIDMode="Static" RepeatDirection="Horizontal"
                                                    ID="rbtPowerInsert" Width="220px" class="tbl forRadio" TabIndex="7" ToolTip="Select power backup">
                                                    <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </li>
                                            <li>
                                                <label>
                                                    InterNet:</label>
                                                <asp:RadioButtonList runat="server" ClientIDMode="Static" RepeatDirection="Horizontal"
                                                    ID="rbtInternetInsert" Width="220px" class="tbl forRadio" TabIndex="8" ToolTip="Select internet">
                                                    <asp:ListItem Text="Yes" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkHostelStatus" ClientIDMode="Static" ToolTip="Check hostel status"
                                                    runat="server" TabIndex="9" />
                                            </li>
                                            <li>
                                                <label>
                                                </label>
                                                <asp:Button ID="btnHostelInsert" ClientIDMode="Static" runat="server" CssClass="button"
                                                    Text="Insert" TabIndex="10" ToolTip="Click in finish process" ValidationGroup="hostelinsert"
                                                    OnClick="BtnHostelClick" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divHostelImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnHostelId" ClientIDMode="Static" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnHostelCourseId" ClientIDMode="Static" runat="server" Value="0" />
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptHostel" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course Name
                                                    </td>
                                                    <td>
                                                        Has Ac
                                                    </td>
                                                    <td>
                                                        Has Laundry
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
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
                                                    <%# Eval("IsCollegeBranchCourseHostelHasAC")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IsCollegeBranchCourseHostelHasLoundry")%>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="GetCollegeCourseHostelDeatils(<%# Eval("CollegeBranchCourseHostelId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="tab_content1" id="tab6">
                                <asp:HiddenField ID="hdnRankId" ClientIDMode="Static" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnCourseRankId" ClientIDMode="Static" runat="server" Value="0" />
                                <div class="field width42Percent fleft">
                                    <fieldset class="clgBg">
                                        <legend>College Rank</legend>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblRankMsg" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="False"></asp:Label></li>
                                            <li>
                                                <label>
                                                    <strong>
                                                        <%=Resources.label.Course %></strong></label>
                                                <asp:DropDownList ID="ddlCoursesRank" runat="server" ClientIDMode="Static" TabIndex="1"
                                                    title="Select course">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvRank" Display="Dynamic"
                                                        ValidationGroup="rankinsert" ControlToValidate="ddlCoursesRank" InitialValue="0">Select course</asp:RequiredFieldValidator></span>
                                            </li>
                                            <li>
                                                <label>
                                                    Rank Source:</label>
                                                <asp:DropDownList ID="ddlRankSourceInsert" runat="server" ClientIDMode="Static" ToolTip="Select rank source"
                                                    TabIndex="2">
                                                </asp:DropDownList>
                                                <a href="javascript:void(0)" id="sndRank" style="line-height: 12px;" onclick="$('#spnRank').removeClass('hide');$(this).hide();$('<%=txtRankSource.ClientID %>').focus();">
                                                    Add Rank Source</a> <span id="spnRank" class="hide">
                                                        <asp:TextBox ID="txtRankSource" runat="server" Width="24%" Style="margin-left: 115px"
                                                            ToolTip="Enter Rank source" TabIndex="1" placeholder="Enter rank source"></asp:TextBox>
                                                        <asp:Button ID="btnRankSourceInsert" runat="server" Style="margin-top: 2px;" CssClass="ultimateLink fleft"
                                                            ToolTip="Click to insert rank source" TabIndex="2" ValidationGroup="rankSource"
                                                            Text="Insert Rank Source" OnClick="BtnRankSourceInsertClick"></asp:Button>
                                                        <a href="javascript:void(0)" onclick="$('#spnRank').addClass('hide');$('#sndRank').show();">
                                                            Hide</a> </span><span class="errormsgSpan">
                                                                <asp:RequiredFieldValidator runat="server" ID="revRankSource" Display="Dynamic" ValidationGroup="rankSource"
                                                                    SetFocusOnError="True" CssClass="error" ControlToValidate="txtRankSource">Field rank source connot be blank</asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator runat="server" ID="rfvSource" Display="Dynamic" ValidationGroup="rankSource"
                                                                    SetFocusOnError="True" CssClass="error" InitialValue="0" ControlToValidate="ddlRankSourceInsert">Select rank source </asp:RequiredFieldValidator>
                                                            </span></li>
                                            <li>
                                                <label>
                                                    Rank Year:</label>
                                                <asp:TextBox ID="txtRanKYearInsert" runat="server" ClientIDMode="Static" ToolTip="Enter rank year"
                                                    TabIndex="3"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RegularExpressionValidator runat="server" ID="revRanRankYear" Display="Dynamic"
                                                        ValidationGroup="rankinsert" SetFocusOnError="True" CssClass="error" ControlToValidate="txtRanKYearInsert">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rgRankYear" runat="server" ValidationGroup="rankinsert"
                                                        Display="Dynamic" CssClass="error" SetFocusOnError="True" ControlToValidate="txtRanKYearInsert">Enter Rank year less than current year </asp:RequiredFieldValidator>
                                                </span></li>
                                            <li>
                                                <label>
                                                    Rank Overall:</label>
                                                <asp:TextBox ID="txtRankOverallInsert" runat="server" ClientIDMode="Static" ToolTip="Enter rank overall"
                                                    TabIndex="4"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator ID="rfvOverall" runat="server" ValidationGroup="rankinsert"
                                                        Display="Dynamic" CssClass="error" SetFocusOnError="True" ControlToValidate="txtRankOverallInsert">Field overall rank can not be blank</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="revRanRankOverAll" Display="Dynamic"
                                                        ValidationGroup="rankinsert" SetFocusOnError="True" CssClass="error" ControlToValidate="txtRankOverallInsert">
                                                    </asp:RegularExpressionValidator>
                                                </span></li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkRankStatus" ToolTip="Check rank status" ClientIDMode="Static"
                                                    runat="server" TabIndex="5" />
                                            </li>
                                            <li>
                                                <label>
                                                </label>
                                                <asp:Button ID="btnRankOverAllInsert" runat="server" ClientIDMode="Static" Text="Insert"
                                                    CssClass="button" TabIndex="6" ValidationGroup="rankinsert" ToolTip="Click here to finish process"
                                                    OnClick="BtnRankOverAllInsertClick" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divRankImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptRankSource" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course Name
                                                    </td>
                                                    <td>
                                                        Year
                                                    </td>
                                                    <td>
                                                        Rank OverAll
                                                    </td>
                                                    <td>
                                                    </td>
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
                                                    <a href="#" onclick="GetCollegeCourseRankDeatils(<%# Eval("CollegeRankId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="tab_content1" id="tab7">
                                <asp:HiddenField ID="hdnHighLights" ClientIDMode="Static" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnCourseHighLightsId" ClientIDMode="Static" runat="server"
                                    Value="0" />
                                <div class="field width42Percent fleft">
                                    <fieldset class="clgBg">
                                        <legend>College Highlights</legend>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblHighLightsMsg" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="False"></asp:Label></li>
                                            <li>
                                                <label>
                                                    <strong>
                                                        <%=Resources.label.Course %></strong></label>
                                                <asp:DropDownList ID="ddlCoursesHigh" runat="server" TabIndex="1" ClientIDMode="Static"
                                                    title="Select course">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvHighLightsCourse"
                                                        Display="Dynamic" ValidationGroup="highLightsinsert" ControlToValidate="ddlCoursesHigh"
                                                        InitialValue="0">Select course</asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Highlights:
                                                </label>
                                                <div style="width: 75%; float: left;">
                                                    <Aj:Testimonial runat="server" class="test" ID="txtCourseHighLightsInsert" Width="370px" />
                                                </div>
                                                <span class="errormsgSpan error"><span class="error" runat="server" id="spnHighError">
                                                </span></span></li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkHighlightsStatus" ClientIDMode="Static" ToolTip="Check highlights status"
                                                    runat="server" TabIndex="3" />
                                            </li>
                                            <li>
                                                <label>
                                                </label>
                                                <asp:Button ID="btnHighLightsInsert" ClientIDMode="Static" runat="server" Text="Insert"
                                                    CssClass="button" ValidationGroup="highLightsinsert" TabIndex="4" ToolTip="Click to finish process"
                                                    OnClick="BtnHighLightsInsertClick" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divHighImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptHighLights" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course Name
                                                    </td>
                                                    <td>
                                                        HighLights
                                                    </td>
                                                    <td>
                                                        Action
                                                    </td>
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
                                                    <a href="#" onclick="GetCollegeCourseHighLightsDeatils(<%# Eval("CollegeBranchCourseHighlightId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="tab_content1" id="tab8">
                                <asp:HiddenField ID="hdnPlacementCourseiD" ClientIDMode="Static" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnPlacementId" runat="server" ClientIDMode="Static" Value="0" />
                                <div class="field width42Percent fleft">
                                    <fieldset class="clgBg">
                                        <legend>College Placement</legend>
                                        <ul>
                                            <li>
                                                <asp:Label runat="server" ID="lblPlacementMsg" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="False"></asp:Label></li>
                                            <li>
                                                <label>
                                                    <strong>
                                                        <%=Resources.label.Course %></strong></label>
                                                <asp:DropDownList ID="ddlCoursesPlacement" runat="server" ClientIDMode="Static" TabIndex="1"
                                                    title="Select course">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvPlacement" Display="Dynamic"
                                                        ValidationGroup="placement" ControlToValidate="ddlCoursesPlacement" InitialValue="0">Select course</asp:RequiredFieldValidator></span>
                                            </li>
                                            <li>
                                                <label>
                                                    Company Name:</label>
                                                <asp:TextBox ID="txtCompanyName" runat="server" ClientIDMode="Static" ToolTip="Enter company name"
                                                    TabIndex="2"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvCompanyName" Display="Dynamic"
                                                        ValidationGroup="placement" ControlToValidate="txtCompanyName">Enter company name</asp:RequiredFieldValidator></span>
                                            </li>
                                            <li>
                                                <label>
                                                    Year:</label>
                                                <asp:TextBox ID="txtCompanyNameyear" runat="server" ClientIDMode="Static" ToolTip="Enter hired year"
                                                    TabIndex="3"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvCompanyHiringYear"
                                                        Display="Dynamic" ValidationGroup="placement" ControlToValidate="txtCompanyNameyear">Enter year of hiring</asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rgYear" runat="server" CssClass="error" ValidationGroup="placement"
                                                        ControlToValidate="txtCompanyNameyear">Enter year less than current year</asp:RangeValidator>
                                                </span></li>
                                            <li>
                                                <label style="line-height: 15px;">
                                                    No. of student hired:</label>
                                                <asp:TextBox ID="txtStudentHired" runat="server" ClientIDMode="Static" ToolTip="Enter no student hired"
                                                    TabIndex="4"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvStudentHired" CssClass="error"
                                                        Display="Dynamic" ValidationGroup="placement" ControlToValidate="txtStudentHired">Enter no of student hired</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ID="revStudentHired" Display="Dynamic"
                                                        ValidationGroup="placement" SetFocusOnError="True" CssClass="error" ControlToValidate="txtStudentHired">Enter no of student hired in digit
                                                    </asp:RegularExpressionValidator>
                                                </span></li>
                                            <li>
                                                <label style="line-height: 15px;">
                                                    Average salary offered:</label>
                                                <asp:TextBox ID="txtStudentSalary" runat="server" ClientIDMode="Static" ToolTip="Enter average salary offered by company"
                                                    TabIndex="5"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" CssClass="error" ID="rfvAvgSalaryHired"
                                                        Display="Dynamic" ValidationGroup="placement" ControlToValidate="txtStudentSalary">Enter average salary offered</asp:RequiredFieldValidator></span>
                                            </li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkPlacement" ClientIDMode="Static" ToolTip="Check placement status"
                                                    runat="server" TabIndex="6" />
                                            </li>
                                            <li>
                                                <label>
                                                </label>
                                                <asp:Button ID="btnPlacementInsert" ClientIDMode="Static" runat="server" Text="Insert"
                                                    CssClass="button" ValidationGroup="placement" TabIndex="7" ToolTip="Click to finish process"
                                                    OnClick="BtnPlacementInsertClick" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divPlacementImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptPlacemnet" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course Name
                                                    </td>
                                                    <td>
                                                        Company Name
                                                    </td>
                                                    <td>
                                                        Average salary offered
                                                    </td>
                                                    <td>
                                                        No of student hired
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
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
                                                    <%# Eval("CollegeBranchCoursePlacementCompanyName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CollegeBranchCoursePlacementAvgSalaryOffered")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CollegeBranchCoursePlacementNoOfStudentHired")%>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="GetCollegeCoursePlacementDeatils(<%# Eval("CollegeBranchCoursePlacementId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="tab_content1" id="tab9">
                                <div class="field width42Percent fleft">
                                    <fieldset class="clgBg">
                                        <legend>Insert College Event</legend>
                                        <ul>
                                            <li>
                                                <asp:Label ID="lblMsg" runat="server" CssClass="clgsubmitsms" Style="display: block;"
                                                    Visible="false"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    <%=Resources.label.Course %>
                                                </label>
                                                <asp:DropDownList runat="server" ID="ddlCourseEvent" ClientIDMode="Static" ToolTip="Select course"
                                                    TabIndex="1">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvcourseName" ValidationGroup="Event"
                                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="ddlCourseEvent"
                                                        InitialValue="0">
                 Filed Course cannot be blank

                                                    </asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Event Name:
                                                </label>
                                                <asp:TextBox runat="server" ID="txtEventName" ClientIDMode="Static" ToolTip="Please enter event name"
                                                    TabIndex="2">
                                                </asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvEventName" ValidationGroup="Event"
                                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventName">
                  Field Event Name cannot be blank

                                                    </asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Event Date:
                                                </label>
                                                <asp:TextBox ID="txtEventDate" runat="server" ClientIDMode="Static" ToolTip="Enter event date"
                                                    TabIndex="3"></asp:TextBox>(DD/MM/YYYY) <span class="errormsgSpan">
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvEventDate" ValidationGroup="Event"
                                                            Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventDate">
                            Field Event date cannot be blank

                                                        </asp:RequiredFieldValidator>
                                                        
                                                        <asp:RegularExpressionValidator ID="RExpEventDate" runat="server" ValidationGroup="Event"
                                                            Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventDate" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" >Enter correct date (DD/MM/YYYY)</asp:RegularExpressionValidator>
                                                        </span> </li>
                                            <li>
                                                <label>
                                                    Event Location:</label>
                                                <asp:TextBox ID="txtEventLocation" runat="server" ClientIDMode="Static" TabIndex="4"
                                                    ToolTip="Enter event location"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator ID="rfvEventLocation" runat="server" ValidationGroup="Event"
                                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventLocation">
                    Field Event Location cannot be blank
                                                    </asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label style="line-height: 12px;">
                                                    Event Description:
                                                </label>
                                                <asp:TextBox ID="txtEventDesc" runat="server" ClientIDMode="Static" TextMode="MultiLine"
                                                    Rows="5" Style="max-width: 264px;" ToolTip="Enter event description" TabIndex="5">

                                                </asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvEventDesc" ValidationGroup="Event"
                                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventDesc">
                Field Event Description cannot be blank

                                                    </asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Publish:</label>
                                                <asp:CheckBox ID="chkEvent" ToolTip="Check event status" ClientIDMode="Static" runat="server"
                                                    TabIndex="6" />
                                            </li>
                                            <li>
                                                <label>
                                                    &nbsp;</label>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" ClientIDMode="Static" CssClass="button"
                                                    ValidationGroup="Event" TabIndex="6" ToolTip="Click to finish process" OnClick="TxtSaveClick" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divEventImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:HiddenField ID="hndEventId" ClientIDMode="Static" runat="server" />
                                    <asp:Repeater ID="rptEventList" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No
                                                    </td>
                                                    <td>
                                                        Course
                                                    </td>
                                                    <td>
                                                        Event Name
                                                    </td>
                                                    <td>
                                                        Event Date
                                                    </td>
                                                    <td>
                                                        Event Status
                                                    </td>
                                                    <td>
                                                        Action
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#(CustompagingEvent.PageSize * CustompagingEvent.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <%# Eval("AjCourseName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("AjCollegeEventName")%>
                                                </td>
                                                <td>
                                                    <%#Convert.ToDateTime(Eval("AjCollegeEventDate")).ToString("dd/MM/yyyy")%>
                                                </td>
                                                <td>
                                                    <%# Eval("AjCollegeEventStatus")%>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="GetCollegeEventDeatils(<%# Eval("AjCollegeEventId")%>); return false;">
                                                        Edit </a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    <Aj:custompaging id="CustompagingEvent" runat="server" />
                                </div>
                            </div>
                            <div class="tab_content1" id="tab10">
                                <asp:HiddenField runat="server" ID="hdnNoticeId" ClientIDMode="Static" />
                                <div class="field width42Percent fleft">
                                    <fieldset class="clgBg">
                                        <asp:Label ID="lblNoticeMsg" runat="server" CssClass="clgsubmitsms" Style="width:200px;"
                                            Visible="false"></asp:Label>
                                        <legend>Insert College Notices</legend>
                                        <ul>
                                            <li>
                                                <asp:Label ID="lblMsgNotice" CssClass="clgsubmitsms" runat="server" Visible="false"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Category:
                                                </label>
                                                <asp:DropDownList runat="server" ID="ddlNoticeCategory" ClientIDMode="Static" ToolTip="Enter notice category"
                                                    TabIndex="1">
                                                </asp:DropDownList>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvNoticeCategory" ValidationGroup="notice"
                                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" InitialValue="0" ControlToValidate="ddlNoticeCategory">
                                                  Select Notice Category

                                                    </asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Subject:
                                                </label>
                                                <asp:TextBox runat="server" ID="txtNoticeSubject" ClientIDMode="Static" ToolTip="Enter notice subject"
                                                    TabIndex="2">
                                                </asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvNoticeSubject" ValidationGroup="notice"
                                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtNoticeSubject">
                                                             Field notice subject cannot be blank

                                                    </asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label style="line-height: 12px;">
                                                    Short Description:
                                                </label>
                                                <asp:TextBox ID="txtNoticeShortDesc" runat="server" ClientIDMode="Static" TextMode="MultiLine"
                                                    Rows="4" Style="min-width: 264px;" ToolTip="Enter notice short description" TabIndex="3"></asp:TextBox>
                                                <span class="errormsgSpan">
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvNoticeShortDesc" ValidationGroup="notice"
                                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtNoticeShortDesc">
                                                     Field notice short description cannot be blank

                                                    </asp:RequiredFieldValidator></span> </li>
                                            <li>
                                                <label>
                                                    Full Description:
                                                </label>
                                                <div style="width: 75%; float: left;">
                                                    <Aj:Testimonial runat="server" ID="txtCollegeNotice" Width="380px" />
                                                </div>
                                            </li>
                                            <span class="errormsgSpan"><span class="error" runat="server" id="errNotice"></span>
                                            </span>
                                            <li>
                                                <li>
                                                    <label>
                                                        Image:</label>
                                                    <asp:Image runat="server" ID="imgNoticeImage" Width="60px" Height="60" ClientIDMode="Static"
                                                        CssClass="fleft hide" />
                                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                                </li>
                                                <li>
                                                    <label>
                                                        Publish:</label>
                                                    <asp:CheckBox ID="chkNotice" ToolTip="Check notice status" ClientIDMode="Static"
                                                        runat="server" TabIndex="6" />
                                                </li>
                                                <li>
                                                    <label>
                                                        &nbsp;</label>
                                                    <asp:Button ID="btnSaveNotice" runat="server" Text="Insert" ClientIDMode="Static"
                                                        ToolTip="Click here to process notice" CssClass="button" ValidationGroup="notice"
                                                        TabIndex="7" OnClick="BtnSaveNoticeClick" />
                                                </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divNoticeImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="field width50Percent fleft border clgTable">
                                    <asp:Repeater ID="rptNoticeDetails" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.NO
                                                    </td>
                                                    <td>
                                                        Subject
                                                    </td>
                                                    <td>
                                                        Image
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#(noticePaging.PageSize * noticePaging.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <%# Eval("NoticeSubject")%>
                                                </td>
                                                <td>
                                                    <img src='<%# String.Format("{0}{1}","/image.axd?Notice=",string.IsNullOrEmpty(Eval("NoticeImage").ToString()) ?"NoImage.jpg":Eval("NoticeImage")) %>'
                                                        width="60px" height="60" alt='<%# Eval("NoticeSubject")%>' title='<%# Eval("NoticeSubject")%>' />
                                                </td>
                                                <td>
                                                    <a href="#" onclick="GetCollegeNoticeDeatils(<%# Eval("NoticeId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    <Aj:CustomPaging ID="noticePaging" runat="server" />
                                </div>
                            </div>
                            <div class="tab_content1" id="tab11">
                                <asp:HiddenField runat="server" ID="hdnTestimonial" ClientIDMode="Static" />
                                <div>
                                    <fieldset class="clgBg">
                                        <legend>College Testimonial</legend>
                                        <ul>
                                            <li>
                                                <asp:Label ID="lblMsgTestimonial"  CssClass="clgsubmitsms" Style="display: block;" runat="server" Visible="false"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Testimonial:
                                                </label>
                                                <div style="width: 75%; float: left;">
                                                    <Aj:Testimonial runat="server" ID="txtTestimonial" />
                                                </div>
                                            </li>
                                            <li>
                                                <label>
                                                    Testimonial Status:</label>
                                                <asp:CheckBox ID="chkTestimonialStatus" ClientIDMode="Static" ToolTip="Check notice status"
                                                    runat="server" TabIndex="6" />
                                            </li>
                                            <li>
                                                <label>
                                                    &nbsp;</label>
                                                <asp:Button ID="btnSaveTestimonial" runat="server" Text="Save" ClientIDMode="Static"
                                                    CssClass="button" ValidationGroup="testmonial" TabIndex="2" OnClick="BtnSaveTestimonialClick" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <div id="divTestiImage" style="display: none">
                                        <label style="color: red; font-size: 16px">
                                            Processing...</label>
                                        <img src="/image.axd?Common=LoadingImage.gif" alt='loading' />
                                    </div>
                                </div>
                                <div class="clgview1" id="grdTestomonial" runat="server">
                                    <asp:Repeater ID="rptTestimonial" runat="server">
                                        <HeaderTemplate>
                                            <table class="grdView" style="background-color: #fff; font-family: @Meiryo UI;">
                                                <tr class="clgBg">
                                                    <td>
                                                        S.No.
                                                    </td>
                                                    <td>
                                                        Testimonial
                                                    </td>
                                                    <td>
                                                        Action
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#(TestimonialPager.PageSize * TestimonialPager.CurrentPageIndex) + Container.ItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <%# Eval("AjTestimonial").ToString()%>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="GetCollegeTestomonialDeatils(<%# Eval("AjTestimonialId")%>); return false;">
                                                        Edit</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                    <Aj:CustomPaging ID="TestimonialPager" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab_container">
                    <div class="tab_content" id="tabs2">
                        <div id="Div1">
                            <ul class="tabs" id="ulQuery">
                                <li rel="Query2" id="liQuery2" class="active" onclick="GetCollLastQuery()"><a href="javascript:void(0)"
                                    class="cursor" title="Get Last Query">Latest Query</a> </li>
                                <li rel="Query3" id="liQuery3" onclick="GetUnAnsweredQuery(1)"><a href="javascript:void(0)"
                                    class="cursor" title="Get UnAnswered Query">Unanswered Query</a> </li>
                                <li rel="Query4" id="liQuery4" onclick="GetAnsweredQuery(1)"><a href="javascript:void(0)"
                                    class="cursor" title="Get Answered Query">Answered Query</a> </li>
                                <li rel="Query1" id="liQuery1" onclick="GetCollegeQuery(1)" title="Get All Query Posted ">
                                    <a href="javascript:void(0)" class="cursor">All Query</a> </li>
                            </ul>
                            <div class="tab_container clgProfDiv">
                                <asp:HiddenField runat="server" ID="hdnTabQuery" ClientIDMode="Static" Value="Query2" />
                                <ol style="margin-top: 10px;">
                                    <li><strong>Choose Course:</strong>
                                        <asp:DropDownList runat="server" ID="ddlCourseForQuery" ClientIDMode="Static" Width="300px"
                                            ToolTip="Select course to filter query">
                                        </asp:DropDownList>
                                    </li>
                                </ol>
                                <div class="tab_content2" id="Query2">
                                </div>
                                <div class="tab_content2" id="Query3">
                                </div>
                                <div class="tab_content2" id="Query4">
                                </div>
                                <div class="tab_content2" id="Query1">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="popup_block" id="divReply">
                    <h2>
                        Query Reply</h2>
                    <input type="hidden" value="0" id="hdnReply" />
                    <input type="hidden" value="0" id="hdnQueryId" />
                    <input type="hidden" value="0" id="hdnQuestion" />
                    <fieldset class="field clgBg">
                        <ul>
                            <li>
                                <label>
                                    Reply:</label>
                                <asp:TextBox runat="server" ID="txtReply" ClientIDMode="Static" TextMode="MultiLine"
                                    Style="max-width: 400px; width: 400px; height: 100px;" TabIndex="1" placeholder="Enter reply text"
                                    ToolTip="Enter query reply text">
                                </asp:TextBox>
                            </li>
                            <li>
                                <label>
                                </label>
                                <input type="button" id="btnReply" class="button" value="Click here to reply" title="click to reply"
                                    tabindex="2" />
                            </li>
                        </ul>
                    </fieldset>
                </div>
                <div class="tab_container">
                    <div class="tab_content" id="tabs3">
                        <fieldset class="fleft clgBg">
                            <ul>
                                <li>
                                    <h3>
                                        Total Visitors on AdmissionJankari.com :
                                        <asp:Label runat="server" ID="lblTotalVisitors">

                                        </asp:Label></h3>
                                </li>
                                <li>
                                    <label>
                                        <%=Resources.label.Course %>
                                    </label>
                                    <asp:DropDownList runat="server" ID="ddlCourseForVisitors" AutoPostBack="True" ToolTip="Select course to see visitors"
                                        OnSelectedIndexChanged="DdlCoursVisitorsSelectedIndexChanged">
                                    </asp:DropDownList>
                                    <div id="divImageVisitors" style="display: none">
                                        <img src="/image.axd?Common=LoadingImage.gif" />
                                    </div>
                                </li>
                                <li>
                                    <label style="padding-top: 0px;">
                                        Total College Visitors:</label>
                                    <strong>
                                        <asp:Label runat="server" ID="lblCollegeVisitors">
                                        </asp:Label></strong> </li>
                                <li>
                                    <asp:Chart ID="rankChart" ImageStorageMode="UseImageLocation" runat="server" SuppressExceptions="True"
                                        ImageLocation="~/Image/tempImage/ChartPic_#SEQ(1000,30)" Palette="None" Width="600px"
                                        Height="400px" BorderlineColor="White">
                                        <Series>
                                            <asp:Series Name="rankSeries" ChartType="Column" Font="Arial, 12pt" CustomProperties="DrawingStyle=Cylinder"
                                                IsValueShownAsLabel="True" BackSecondaryColor="Transparent" Color="teal" XAxisType="Primary">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea2" IsSameFontSizeForAllAxes="true" BackColor="#eff2f7">
                                                <Area3DStyle Enable3D="true" LightStyle="Realistic"></Area3DStyle>
                                                <AxisX Title="Source Name" TitleForeColor="#415983" TitleFont="Arial, 12pt, style=Bold">
                                                    <MajorGrid Enabled="false" />
                                                </AxisX>
                                                <AxisY Interval="350000" Title="Overall Rank" TitleForeColor="Maroon" TitleFont="Arial, 12pt, style=Bold">
                                                    <MajorGrid Enabled="false" />
                                                </AxisY>
                                                <InnerPlotPosition Auto="true" />
                                            </asp:ChartArea>
                                        </ChartAreas>
                                        <Titles>
                                            <asp:Title BackColor="Transparent" Font="Microsoft Sans Serif, 16pt, style=Bold"
                                                ForeColor="ForestGreen" Name="Title1">
                                            </asp:Title>
                                        </Titles>
                                    </asp:Chart>
                                </li>
                            </ul>
                        </fieldset>
                    </div>
                </div>
                <div class="tab_container" id="tabAdvertise" clientidmode="Static" runat="server" visible="False">
                  
                    <div class="tab_content" id="tabAdvertise1">
                        <Aj:AddProduct ID="addProduct" runat="server" />
                    </div>
                </div>
                <div class="tab_container" id="tabBanner" runat="server" visible="false">
                    <div class="tab_content" id="divBanner">
                        <div class="field width42Percent fleft">
                            <asp:Label runat="server" ID="lblBannerResult" Visible="False"></asp:Label>
                            <fieldset class="clgBg">
                                <legend>Banner</legend>
                                <ul>
                                    <li>
                                        <label>
                                            <%=Resources.label.Course %>
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlBannerCourse" ClientIDMode="Static" ToolTip="Please select course"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="Banner"
                                            Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="ddlBannerCourse"
                                            InitialValue="0">
                                         Course can not be blank

                                        </asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <label>
                                            Banner ToolTip:
                                        </label>
                                        <asp:TextBox runat="server" ID="txtToolTip" ToolTip="Please enter tooltip" TabIndex="4"
                                            ClientIDMode="Static">

                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvToolTip" ValidationGroup="Banner"
                                            Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtToolTip">
                                            Banner ToolTip can not be blank

                                        </asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <label>
                                            Ads URL:
                                        </label>
                                        <asp:TextBox runat="server" ID="txtUrl" ToolTip="Please enter url" TabIndex="4" ClientIDMode="Static">

                                        </asp:TextBox>
                                        <img src="/image.axd?Common=Information.jpg" alt="information" title="Provide complete url, else leave blank" />
                                    </li>
                                    <li style="width: 550px!important">
                                        <label>
                                            Banner image</label>
                                        <span style="width: 250px; display: inline-block;">
                                            <input type="file" id="UploadButton" />
                                        </span><span>
                                            <img runat="server" id="bannerImage" width="100" height="100" clientidmode="Static" />
                                        </span>
                                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnBanner" />
                                    </li>
                                    <li>
                                        <label>
                                        </label>
                                        <asp:Button ID="btnBannerSubmit" OnClick="BtnBannerSubmitClick" runat="server" Text="Save"
                                            ValidationGroup="Banner" />
                                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnBannerId" />
                                    </li>
                                </ul>
                            </fieldset>
                        </div>
                        <div class="field width50Percent fleft border clgTable">
                            <asp:Repeater ID="rptBannerList" runat="server">
                                <HeaderTemplate>
                                    <table class="grdView">
                                        <tr class="clgBg">
                                            <td>
                                                Banner Position
                                            </td>
                                            <td>
                                                Course
                                            </td>
                                            <td>
                                                TooTip
                                            </td>
                                            <td>
                                                Image
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("AjBannerPosition")%>
                                        </td>
                                        <td>
                                            <%# Eval("AjCourseName")%>
                                        </td>
                                        <td>
                                            <%# Eval("AjBannerToolTip")%>
                                        </td>
                                        <td>
                                            <img src='<%# String.Format("{0}{1}","/image.axd?Banner=",string.IsNullOrEmpty(Eval("AjBannerImage").ToString()) ?"NoImage.jpg":Eval("AjBannerImage")) %>'
                                                width="60px" height="60" alt='<%# Eval("AjBannerPosition")%>' title='<%# Eval("AjBannerPosition")%>' />
                                        </td>
                                        <td>
                                            <a href="#" onclick="GetBannerDetails(<%# Eval("AjBannerId")%>); return false;">Edit</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="tab_container" id="divAdvertiseContainer" runat="server" visible="false">
                    <div class="tab_content" id="divYourAdvertise">
                        <div style="display: none" id="progress">
                            <img src="/image.axd?Common=loading.gif" alt="loading" />
                        </div>
                        <div id="divProductList" style="display: none">
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hdnNoticeImage" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdnTabs" ClientIDMode="Static" Value="tab1" />
            <asp:HiddenField runat="server" ID="hdnMainTabs" ClientIDMode="Static" Value="tabs1" />
            <asp:HiddenField runat="server" ID="hdnCollegeName" ClientIDMode="Static" />
            <a href="#" id="sndLink"></a>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveNotice" />
        </Triggers>
    </asp:UpdatePanel>
    <script src="/Js/JCollegeProfile.js" type="text/javascript"></script>
    <%--<script src="/Js/ajaxupload.js" type="text/javascript"></script>--%>
</asp:Content>
