<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="CollegeList.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeList" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<%@ Register TagPrefix="AJ" TagName="FileUpload" Src="~/UserControl/AjaxFileUpload.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnCountryInsert" Value="0"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="hdnStateInsert" Value="0"></asp:HiddenField>
            <asp:HiddenField runat="server" Value="0" ID="hdnCityInsert"></asp:HiddenField>
            <div id="fade">
            </div>
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" alt="Loading" title="Loading" />
            </div>
            <asp:HiddenField ID="hdnQuery" runat="server" />
            <asp:HiddenField ID="hdnDbQuery" runat="server" />
            <div class="grdOuterDiv">
                <asp:Label ID="lblUpdate" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSuccess" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblInfo" runat="server" Visible="false"></asp:Label>
                
                
                
                 <ul class="addPage_utility">
                    <li class="fright" style="width: 170px !important;">
                        <div class="navbar-inner">
                            <asp:Label ID="lblResult" runat="server" Visible="false"></asp:Label>
                                <a href="#" id='sndCollegeInsert' class="insertIco" onclick="OpenPoup('divCollegeInsert','650px','sndCollegeInsert');return false;">Insert New College</a>
                                
                            <div class="clear">
                            </div>
                        </div>
                    </li>
                </ul>
                
                
                
                
                
                
                
                
                <fieldset>
                    <legend>Search</legend>
                    <ul class="options-bar">
                        <li>
                            <label>
                                College:</label>
                            <asp:TextBox runat="server" CssClass="autocomplete" TabIndex="1" ID="txtCollegeName" Width="63%"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="searchbtn " TabIndex="2" OnClick="BtnSearchClick" ValidationGroup="search"></asp:Button>
                        </li>
                         
                    </ul>
                
                    <ul>
                        <li><label></label>
                           
                           
                                
                                <asp:DropDownList ID="ddlCourseList" runat="server" TabIndex="3" style="min-width:185px !important; width:185px !important"  ToolTip="Please Select Course" AutoPostBack="True" OnSelectedIndexChanged="ddlCourseList_SelectedIndexChanged">
                                </asp:DropDownList>
                            
                                <asp:DropDownList ID="ddlState" runat="server" TabIndex="4" style="min-width:185px !important; width:185px !important" ToolTip="Please Select State" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                             
                               <asp:DropDownList ID="ddlCity" runat="server" ToolTip="Please Select City" style="min-width:185px !important; width:185px !important" TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                    </asp:DropDownList>
                             
                            
                           
                                <asp:DropDownList ID="rbtSponser" runat="server" TabIndex="6" ToolTip="Please Select College Type" style="min-width:160px !important; width:160px !important" AutoPostBack="True" OnSelectedIndexChanged="rbtSponser_SelectedIndexChanged">
                                   <asp:ListItem Value="0" Selected="True" Text="Un Sponsered"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Sponsered"></asp:ListItem>
                                    </asp:DropDownList>
                             </li>
                    </ul>
                </fieldset>
                <asp:Repeater ID="rptCollegeList" runat="server" OnItemCommand="RptCollegeListItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    CollegeName
                                </th>
                                <th>
                                    Course
                                </th>
                                <th>
                                    BranchPopularName
                                </th>
                                <th>
                                    Management
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
                                <a href='UpdateCollegeDetails.aspx?CollegeBranchId=<%# Eval("CollegeIdBranchId")%>'>
                                    <%# Eval("CollegeBranchName")%></a>
                            </td>
                            <td>
                                <%# Eval("CourseName")%>
                            </td>
                            <td>
                                <a href='UpdateCollegeDetails.aspx?CollegeBranchId=<%# Eval("CollegeIdBranchId")%>'>
                                    <%# Eval("CollegePopulaorName")%></a>
                            </td>
                            <td>
                                <%# Eval("CollegeManagementType")%>
                            </td>
                            <td>
                                <%# Eval("CollegeBranchStatus")%>
                            </td>
                            
                            <td>
                                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("CollegeIdBranchId")%>' CommandName="Edit" CausesValidation="false"><img src="../Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" class="editIconmargin" width="12px" /></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <asp:Panel runat="server" ID="pnlPager" CssClass="pagination">
                </asp:Panel>
            </div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="Progress" class="loading">
                        <img src="/image.axd?Common=Loading.gif" alt="Loading" title="Loading" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

     
            <asp:HiddenField ID="hdnAssociation" runat="server" />
            <asp:HiddenField ID="hdnBranchCourseid" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>


    <div id="divCollegeInsert" class="popup_block width60perc">
        <fieldset id="basicInfo">
            <legend>College Branch Basic Info</legend>
            <ul class="pouplist">
                <li>
                    <label>
                        Institute Type</label>
                    <asp:DropDownList runat="server" ID="ddlInstituteType" TabIndex="1" ToolTip="Please Select Institute">
                    </asp:DropDownList>
                    </li>
                    <li>
                    <label>
                        Institute Group</label>
                    <asp:DropDownList runat="server" ID="ddlCollegeGroup" TabIndex="2" ToolTip="Please Select Institute">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        College Name:</label>
                    <asp:TextBox ID="txtCollegeBranch" runat="server" TabIndex="3" ToolTip="Please Enter CollegeBranch"></asp:TextBox>
                    </li><li>
                    <label>
                        Establishment</label>
                    <asp:TextBox ID="txtCollegeEst" runat="server" TabIndex="4" ToolTip="Please Enter College Establishment"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Popular Name</label>
                    <asp:TextBox ID="txtCollegePopularName" runat="server" TabIndex="5" ToolTip="Please Enter College Popular Name"></asp:TextBox>
                    </li>
                <li>
                    <label>
                        Management</label>
                    <asp:DropDownList runat="server" ID="ddlManagement" TabIndex="6" ToolTip="Please select management">
                    </asp:DropDownList>
                </li>
                
                    <li><label>
                        WebSite</label>
                    <asp:TextBox ID="txtCollegeWebsite" runat="server" TabIndex="7" ToolTip="Please Enter College WebSite"></asp:TextBox>
                </li>
                
                
                <li>
                    <label>
                        Logo</label>
                    <asp:FileUpload ID="FileUpload3" TabIndex="8" runat="server" />
                    <asp:HiddenField ID="hdnImageFile" runat="server" />
                   </li>
                   
                <li style="height:64px !important;">
                    <label>
                        Description</label>
                    <asp:TextBox ID="txtCollegeDesc" runat="server" TabIndex="9" TextMode="MultiLine" ToolTip="Please Enter College Establishment"></asp:TextBox>
                   </li>
                   <li>
                    <label>
                        Display</label>
                    <asp:CheckBox runat="server" ID="chkCollegeStatus" ToolTip="Please Check Status" TabIndex="10" CssClass="chkCollege"></asp:CheckBox>
                </li>
                   <li>
                    <label>
                        EmailId</label>
                    <asp:TextBox ID="txtEmailId" runat="server" TabIndex="11" ToolTip="Please Enter EmailId"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Phone</label>
                    <asp:TextBox ID="txtCollegeMobile" runat="server" TabIndex="12" ToolTip="Please Enter MobileNo"></asp:TextBox>
                   </li>
                   <li>
                    <label>
                        Fax</label>
                    <asp:TextBox ID="txtCollegeFax" runat="server" TabIndex="13" ToolTip="Please Enter Fax"></asp:TextBox>
                    </li>
                   
                   <li>
                    <label>
                        Country</label>
                    <select id="ddlUniversityCountryName" tabindex="14" style="width:180px !important;">
                        <option value="0" selected="selected">Select Country</option>
                    </select>
                    </li><li>
                    <label>
                        State</label>
                    <select id="ddlUniversityStateName" disabled="disabled" tabindex="15"  style="width:180px !important;">
                        <option value="0" selected="selected">Select State</option>
                    </select>
                </li>
                <li>
                    <label>
                        City</label>
                    <select id="ddlUniversityCityName" disabled="disabled" tabindex="16"  style="width:180px !important;">
                        <option value="0" selected="selected">Select City</option>
                    </select>
                </li>
                <li>
                    <label>
                        PinCode
                    </label>
                    <asp:TextBox ID="txtPinCode" runat="server" TabIndex="17" ToolTip="Please Enter PinCode"></asp:TextBox>
                </li>
                 <li style="height:64px !important;">
                    <label>
                        Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" TabIndex="18" ToolTip="Please Enter Adress" TextMode="MultiLine"></asp:TextBox>
                </li>
                   
                
                <li><label>
                        Go To Next:</label>
                    
                    <input type="button" tabindex="19" onclick="javascript:if(validate()){$('#basicInfo').hide();$('#divCourse').show();}" class="navbutton" value="Insert Course" />
                </li>
            </ul>
        </fieldset>
        <fieldset id="divCourse" style="display: none">
            <input type="button" onclick="javascript:$('#basicInfo').show();$('#divCourse').hide();" class="navbutton" style="float:right !important;" value="Back" />
            <legend>Course</legend>
            <ul class="pouplist">
                <li style="width:100% !important;">
                    <label>
                        Course</label>
                    <asp:DropDownList ID="ddlCourse" runat="server" TabIndex="1" title="Please Select Course">
                    </asp:DropDownList>
                </li>
                <li style="width:100% !important;">
                    <label>
                        University</label>
                    <asp:DropDownList ID="ddlUniversity" runat="server" TabIndex="2" title="Please Select University">
                    </asp:DropDownList>
                </li>
                
                <li>
                    <label>
                        Title</label>
                    <input id="txtCourseTitle" runat="server" type="text" tabindex="3" title="Please Enter Course Meta Tag" />
                </li>
                <li>
                    <label>
                        Meta Tag</label>
                    <input id="txtCourseMetaTag" runat="server" type="text" tabindex="4" title="Please Enter Course Meta Tag" />
                </li>
                 <li>
                    <label>
                        Meta Desc</label>
                    <input id="txtCourseMetaDesc" runat="server" type="text" tabindex="5" title="Please Enter Course Meta Tag" />
                </li>
                <li>
                    <label>
                        Url</label>
                    <input id="txtCourseUrl" runat="server" type="text" tabindex="6" title="Please Enter Course Url" />
                </li>
               
                <li style="height:62px !important;">
                    <label>
                        Description</label>
                    <textarea id="txtCourseDesc" tabindex="7" runat="server" title="Please Enter College Popular Name"></textarea>
                </li>
                <li>
                    <label>
                        Establishment</label>
                    <input id="txtCourseEst" type="text" runat="server" tabindex="8" title="Please Enter College Popular Name" />
                </li>
                
                <li>
                    <label>
                        Has Hostel</label>
                    <asp:CheckBox ID="chkHasHostel" TabIndex="9" ToolTip="Please Select Hostel" runat="server" />
                </li>
                <li>
                    <label>
                        Sponser</label>
                    <asp:CheckBox ID="chkSponserStatus" runat="server" TabIndex="10" ToolTip="Please Checked" />
                </li>
                <li>
                    <label>
                        Status</label>
                    <asp:CheckBox ID="chkCollegeCourse" runat="server" TabIndex="11" ToolTip="Please Checked" />
                </li>
                
                <li><label>&nbsp;</label>
                    <asp:Button ID="btnSubmitCollegeBasicInfo" runat="server" Text="Submit" TabIndex="12" OnClientClick="javascript:return validateCurse()" ToolTip="Please Submit" OnClick="BtnSubmitCollegeBasicInfoClick" /></li>
            </ul>
        </fieldset>
    </div>


    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        var countryUrl = "../../WebServices/CommonWebServices.asmx/GetCountryList";

        BindDropDownForData($("#ddlUniversityCountryName"), countryUrl);
        var url = "../../WebServices/CommonWebServices.asmx/GetCollegeDetails";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeName.ClientID %>"), url);
       

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $(".grdView tr:even").css("background-color", "#f4f4f8");
                $(".grdView tr:odd").css("background-color", "#ffffff");
                BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeName.ClientID %>"), url);
                BindDropDownForData($("#ddlUniversityCountryName"), countryUrl);
                close();

            }
        }

        function close() {

            $("#fade").hide();
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


        $("#ddlUniversityCountryName").change(function () {
            if ($("#ddlUniversityCountryName").val() > 0) {
                $("#<%=hdnCountryInsert.ClientID %>").val($("#ddlUniversityCountryName").val());
            }
            State($("#ddlUniversityStateName"), $("#ddlUniversityCountryName"));
        });
        $("#ddlUniversityStateName").change(function () {
            if ($("#ddlUniversityStateName").val() > 0) {
                $("#<%=hdnStateInsert.ClientID %>").val($("#ddlUniversityStateName").val());
            }
            City($("#ddlUniversityStateName"), $("#ddlUniversityCityName"));
        });
        $("#ddlUniversityCityName").change(function () {
            if ($("#ddlUniversityCityName").val() > 0) {
                $("#<%=hdnCityInsert.ClientID %>").val($("#ddlUniversityCityName").val());
            }

        });

        function validate() {
            if ($("#<%=txtCollegeBranch.ClientID %>").val().length == 0) {
                alert("Please enter college name"); return false;
            } else { return true; }

        }
        function validateCurse() {
            if ($("#<%=ddlCourse.ClientID %>").val() <= 0) {
                alert("Please select  course");
                return false;
            } else {
                return true;
            }

        }
        $(".grdView tr:even").css("background-color", "#f4f4f8");
        $(". grdView tr:odd").css("background-color", "#ffffff");
    </script>
</asp:Content>
