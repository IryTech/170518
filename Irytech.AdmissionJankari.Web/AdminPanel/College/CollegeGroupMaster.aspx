<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CollegeGroupMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeGroupMaster" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnCollegeGroupMaster"></asp:HiddenField>
            <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
            </asp:Label>
            <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
            </asp:Label>
            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
            </asp:Label>

            <ul class="addPage_utility">

    <li class="fright" style="width: 186px !important;">
        <div class="navbar-inner">
            <a href="#" id='sndAddCollegeGroup' class="insertIco" onclick="OpenPoup('divRankSourceInsert','650px','sndAddCollegeGroup');return false;">
                                Insert College Group</a>
            <div class="clear">
            </div>
        </div>
    </li>
    <li class="fright" style="width: 72px !important;">
        <asp:ImageButton ID="btnUpload" runat="server"  ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" title="Upload Excel"
                            TabIndex="7" OnClick="btnUpload_Click" />
                        <asp:Label runat="server" Text="" ID="lblRecordsInserted"></asp:Label>
        <asp:ImageButton ID="btnSeeExcelFormat" runat="server"  ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" title="Preview Excel"
                            TabIndex="7" OnClick="btnSeeExcelFormat_Click" />
    </li>

</ul>

            <fieldset style="display:none;">
                <legend>College Group Master</legend>
                <ul class="options-bar">
                    <li class="opt-barlist">
                        <label>
                            Upload File:
                        </label>
                        <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="6" />
                        <asp:RequiredFieldValidator ID="rfvExcelUpload" Display="Dynamic" runat="Server"
                            ControlToValidate="fileUploadExcel" ValidationGroup="ExcelUpload" />
                    </li>
                     
                </ul>
            </fieldset>
            <fieldset>
                <legend>Search College Group
</legend>
                <ul class="options-bar">
                    <li>
                        <label>
                            College Group
                        </label>
                        <asp:TextBox ID="txtCollegeGroupNameSearch" placeholder="Enter College Group" TabIndex="1" runat="server" CssClass="autocomplete"
                            Width="63%" ToolTip="Please Enter College Group"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" TabIndex="2" Text="Search" CssClass="searchbtn"
                            OnClick="btnSearch_Click" ToolTip="Please Submit" /></li>
                </ul>
           
                <asp:Repeater ID="rptCollegeGroupMaster" runat="server" OnItemCommand="rptCollegeGroupMaster_ItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    S.No
                                </th>
                                <th>
                                    Group Name
                                </th>
                                <th>
                                    Image
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
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <%# Eval("CollegeGroupName")%>
                            </td>
                            <td>
                                <img id="Logo" height="50" width="50" alt='<%# Eval("CollegeGroupLogo")%>' src='<%# String.Format("{0}{1}","/image.axd?CollegeGroup=",string.IsNullOrEmpty(Eval("CollegeGroupLogo").ToString()) ?"NoCarImage.PNG":Eval("CollegeGroupLogo")) %>' />
                            </td>
                            <td>
                                <%# Eval("CollegeGropuStatus")%>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/AdminPanel/Images/CommonImages/editIcon.png"
                                    CausesValidation="false" Width="14px" CommandName="Edit" CommandArgument='<%# Eval("CollegeGroupId")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
            </fieldset>
            <div id="divRankSourceInsert" class="popup_block width43perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblHeader" runat="server"></asp:Label>
                    </legend>
                    <ul>
                        <li>
                            <label>
                                College Group Name</label>
                            <asp:TextBox runat="server" ID="txtCollegeGroupName" TabIndex="1" ToolTip="Please Enter College Group Name">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCollegeGroupName" SetFocusOnError="true" runat="server"
                                Display="Dynamic" ValidationGroup="College" CssClass="error1" ControlToValidate="txtCollegeGroupName">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                Display</label>
                            <asp:CheckBox runat="server" ID="chkStatus" TabIndex="2" ToolTip="Please Check Status">
                            </asp:CheckBox>
                        </li>
                        <li>
                            <label>
                                College Group Logo</label>
                            <asp:HiddenField runat="server" ID="hdnFileName" />
                            <asp:FileUpload ID="flpImgUpload" runat="server" TabIndex="3" ToolTip="Please Enter University Logo" />
                            <asp:RequiredFieldValidator ID="rfvImageUpload" runat="Server" ControlToValidate="flpImgUpload"
                                ValidationGroup="CollegeUpload" />
                            <asp:Image runat="server" ID="iumgUniversity" Width="50px" CssClass="uploadImage"
                                Height="50px" Visible="False"></asp:Image>
                        </li>
                        <li>
                            <label>
                                &nbsp;</label>
                            <asp:Button runat="server" Text="Save" ID="btnSave" TabIndex="4" ValidationGroup="College"
                                ToolTip="Please Submit" OnClick="btnSave_Click" />
                            <asp:Button runat="server" Text="Cancel" TabIndex="5" ID="btnCancel" ToolTip="Please Cancel" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="fade">
    </div>
    <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" alt="Loading_Image" title="Loading Image" />
    </div>
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        var collegeGroupUrl = "../../WebServices/CommonWebServices.asmx/GetCollegeGroupDetails";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeGroupNameSearch.ClientID %>"), collegeGroupUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtCollegeGroupNameSearch.ClientID %>"), collegeGroupUrl);
            }
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
</asp:Content>
