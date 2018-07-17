<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CollegeAdvetise.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeAdvetise" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updateAdvst" runat="server">
        <ContentTemplate>
            <ul class="addPage_utility">
        <li class="fright" style="width: 173px !important;">
            <div class="navbar-inner">
               <a href="#" id='sndCollegeInsert' class="insertIco" onclick="OpenPoup('divCollegeInsert','650px','sndCollegeInsert');return false;">
                                    Add Advertisement </a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>
            
            
            
            
            <div class="grdOuterDiv">
                <asp:Label ID="lblMsg"  runat="server"></asp:Label>
                <fieldset>
                    <legend>Search Advertisement</legend>
                    <ul class="options-bar">
                        <li>
                            <label >
                                College:</label>
                            <asp:TextBox runat="server" CssClass="autocomplete" placeholder="Enter College Name" TabIndex="1" ID="txtCollegeName"
                                Width="63%"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchbtn " TabIndex="2"
                                CausesValidation="false" OnClick="btnSearch_Click"></asp:Button>
                        </li>
                       
                    </ul>
                
                    <ul>
                        <li><label></label>
                            <asp:DropDownList ID="ddlCourseList" runat="server" TabIndex="3" Width="60%" OnSelectedIndexChanged="ddlCourseList_SelectedIndexChanged"
                                ToolTip="Please Select Course" AutoPostBack="True">
                            </asp:DropDownList>
                        
                            <asp:DropDownList ID="rbtSponser" runat="server" TabIndex="6" OnSelectedIndexChanged="rbtSponser_SelectedIndexChanged"
                                ToolTip="Please Select College Type" AutoPostBack="True">
                            </asp:DropDownList>
                         </li>
                    </ul>
                </fieldset>
                <asp:Repeater ID="rptCollegeList" runat="server" OnItemCommand="rptCollegeList_ItemCommand">
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
                                    Ads Code
                                </th>
                                <th>
                                  Ads Start Date
                                </th>
                                 <th>
                                  Ads End Date
                                </th>
                                 <th>
                                  Ads Status
                                </th>
                                <th>
                                    Action
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("AjCollegeBranchName")%>
                            </td>
                            <td>
                                <%# Eval("AjCourseName")%>
                            </td>
                            <td>
                                <%# Eval("AjCollegeAssociationCategoryName")%>
                            </td>
                             <td>
                              <%# String.Format("{0}", string.IsNullOrEmpty(Convert.ToString(Eval("AjAdsBannerStartDate"))) ? "NA" : Convert.ToDateTime(Eval("AjAdsBannerStartDate")).ToString("dd/MM/yyyy"))%>
                               
                            </td>
                              <td>
                               <%# String.Format("{0}", string.IsNullOrEmpty(Convert.ToString(Eval("AjAdsBannerEndDate"))) ? "NA" : Convert.ToDateTime(Eval("AjAdsBannerEndDate")).ToString("dd/MM/yyyy"))%>
                            </td>
                              <td>
                                <%# Eval("AjBannerStatus")%>
                            </td>
                            <td>
                                <asp:HiddenField ID="hndAdvstType" runat="server" Value='<%# Eval("AjCollegeAssociationCategoryId")%>' />
                                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("AjCollegeBranchCourseId")%>'
                                    CommandName="Edit" CausesValidation="false"><img src="../Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" class="editIconmargin" width="12px" /></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                <div id="divCollegeInsert" class="popup_block width43perc">
                    <fieldset id="basicInfo">
                        <legend>
                            <asp:Label ID="lblInsertUpdate" runat="server" Text="Add Text Ads"></asp:Label>
                        </legend>
                        <ul>
                            <li>
                                <label>
                                    Select Course:</label>
                                <asp:DropDownList ID="ddlAdstCourse" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAssociationCategoryType" SetFocusOnError="true"
                                    runat="server" ControlToValidate="ddlAdstCourse" CssClass="error1" InitialValue="0"
                                    ValidationGroup="CollegeCat">
                               Select Course
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    College Name:</label>
                                <asp:TextBox ID="txtCollegeAdvst" runat="server" ToolTip="Enter the name of college"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCollegeName" SetFocusOnError="true" runat="server"
                                    ControlToValidate="txtCollegeAdvst" CssClass="error1" ValidationGroup="CollegeCat">
                             Field college name can not be blank
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    Ads Code:</label>
                                <asp:DropDownList ID="ddlAdvstType" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAdvstType" SetFocusOnError="true" runat="server"
                                    ControlToValidate="ddlAdvstType" CssClass="error1" InitialValue="0" ValidationGroup="CollegeCat">
                                 Select Ads Code
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    Redirect URL:</label>
                                <asp:TextBox ID="txtRedirectURL" runat="server" ToolTip="Please enter  advertiser url or leave it blank"></asp:TextBox>
                            </li>
                            <li>
                                <label>
                                    Ads Priorty:</label>
                                <asp:TextBox runat="server" ID="txtPriority" ToolTip="Please enter priorty" TabIndex="3">
                                </asp:TextBox>
                                <Ajex:NumericUpDownExtender ID="txtMinutes_NumericUpDownExtender" runat="server"
                                    Enabled="True" Maximum="9" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                                    ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtPriority"
                                    Width="50">
                                </Ajex:NumericUpDownExtender>
                                <asp:RequiredFieldValidator runat="server" ID="rfvPrior" ValidationGroup="CollegeCat"
                                    Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtPriority"> 
                                 Field Ads Priority can not be blank

                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    Ads Start Date:
                                </label>
                                <asp:TextBox runat="server" ID="txtStartDate" ToolTip="Please enter the date from which banner will visible to site"
                                    TabIndex="5">
                                </asp:TextBox>(MM/dd/YYYY)
                                <asp:RequiredFieldValidator runat="server" ID="rfvBannerStartDate" ValidationGroup="CollegeCat"
                                    Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtStartDate"> 
                             Field  Ads Start Date can not be blank
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    Ads End Date:
                                </label>
                                <asp:TextBox runat="server" ID="txtEndDate" ToolTip="Please enter the date from which banner will last visible to site"
                                    TabIndex="6">
                                </asp:TextBox>(MM/dd/YYYY)
                                <asp:RequiredFieldValidator runat="server" ID="rfvAdvstEndDate" ValidationGroup="CollegeCat"
                                    Display="Dynamic" SetFocusOnError="True" CssClass="error1" ControlToValidate="txtEndDate"> 
                             Field Ads End Date can not be blank
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    Publish Ads:
                                </label>
                                <asp:CheckBox ID="chkbannerStatus" runat="server" Checked="true" />
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label>
                                <asp:Button ID="btnCollegeAssociat" runat="server" Text="Save" TabIndex="9" OnClick="btnCollegeAssociat_Click"
                                    CausesValidation="true" ValidationGroup="CollegeCat" />
                                <input id="btnReset" type="button" value="Reset" onclick="ClearAllFields();" title="Please Reset" />
                            </li>
                        </ul>
                    </fieldset>
                </div>
            </div>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" alt="Loading" title="Loading" />
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
        var url = "../../WebServices/CommonWebServices.asmx/BindCollegesByCourse";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeName.ClientID %>"), "../../WebServices/CommonWebServices.asmx/GetSponserCollegeList");
        function ClearAllFields() {

            $("#<%=ddlAdstCourse.ClientID %>" + "option:first-child").attr("selected", "selected");
            $("#<%=txtCollegeAdvst.ClientID %>").val('');
            $("#<%=ddlAdvstType.ClientID %>" + "option:first-child").attr("selected", "selected");
            $("#<%=lblInsertUpdate.ClientID %>").text('Add Text Ads');
            $("#<%=btnCollegeAssociat.ClientID %>").text('Save');
            $("#<%=txtCollegeAdvst.ClientID %>").removeAttr('disabled');
            $("#<%=ddlAdstCourse.ClientID %>").removeAttr('disabled');
            $("#<%=ddlAdvstType.ClientID %>").removeAttr('disabled');
        }

        $("#<%=ddlAdstCourse.ClientID %>").change(function () {
            var courseid = $("#<%=ddlAdstCourse.ClientID %>").val();
            BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollegeAdvst.ClientID %>"), courseid, url);
        })
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                var courseid = $("#<%=ddlAdstCourse.ClientID %>").val();
                BindDropDownCommonForAdminAutoCompletebyCourseID($("#<%=txtCollegeAdvst.ClientID %>"), courseid, url);
                BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeName.ClientID %>"), "../../WebServices/CommonWebServices.asmx/GetSponserCollegeList");

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
        function OpenAdvst() {
           OpenPoup('divCollegeInsert', '650px', 'sndCollegeInsert')
            ClearAllFields();
        }   
    </script>
</asp:Content>
