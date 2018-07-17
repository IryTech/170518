<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/AdminPanel/Controls/Admin.Master" CodeBehind="State.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.State" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="ucCustomPaging" TagPrefix="Aj" %>
<asp:Content ID="ContentCountry" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlState" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnStateId"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="HdnZoneId"></asp:HiddenField>
            <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorMsg" runat="server" CssClass="error" Visible="false"></asp:Label>
            <ul class="addPage_utility">
                <li class="fright" style="width: 105px !important;">
                    <div class="navbar-inner">
                        <a href="#" id='sndAddState' class="insertIco" onclick="OpenPoup('divCourseCategoryInsert','650px','sndAddState');return false;">
                            Add Sate</a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
                <li class="fright" style="width: 72px !important;">
                    <asp:ImageButton ID="btnUpload" runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"
                        ValidationGroup="GrUpload" TabIndex="4" OnClick="btnUpload_Click" />
                    <asp:ImageButton ID="btnPreview" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png"
                        runat="server" ToolTip="Preview Excel" TabIndex="5" OnClick="btnPreview_Click" />
                </li>
            </ul>
            <fieldset style="display: none;">
                <legend>State Master </legend>
                <ul class="options-bar">
                    <li class="opt-barlist">
                        <label>
                            Upload File:
                        </label>
                        <asp:FileUpload ID="fileUploadExcel" onfocus="ClearLabel()" runat="server" TabIndex="5" /></label>
                        <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" Display="Dynamic"
                            ControlToValidate="fileUploadExcel" ValidationGroup="ExcelUpload" />
                    </li>
                    <li class="width12perc fleft">
                        <asp:Button runat="server" Text="Upload Excel" CssClass="uploadbtn" CausesValidation="true"
                            TabIndex="6" OnClick="btnUpload_Click" /></li>
                    <li class="width24perc fleft">
                        <label style="width: 80px !important;">
                            See Excel:</label>
                        <asp:Button runat="server" Text="Preview Excel" CssClass="downloadbtn" TabIndex="7"
                            OnClick="btnPreview_Click" />
                    </li>
                    <li class="width25perc fleft">
                        <div class="navbar-inner">
                            <div class="clear">
                            </div>
                        </div>
                    </li>
                </ul>
            </fieldset>
            <fieldset>
                <legend>Search State </legend>
                <ul>
                    <li>
                        <label>
                        </label>
                        <%--<label>State Name:</label>--%>
                        <asp:DropDownList ID="ddlSearchStateName" onfocus="ClearLabel()" AutoPostBack="true"
                            runat="server" OnSelectedIndexChanged="ddlSearchStateName_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%-- <label>Zone Name:</label>--%>
                        <asp:DropDownList ID="ddlSearchZoneName" onfocus="ClearLabel()" AutoPostBack="true"
                            runat="server" OnSelectedIndexChanged="ddlSearchZoneName_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                </ul>
            </fieldset>
            <fieldset>
                <legend>State List</legend>
                <asp:Label ID="lblwarning" runat="server" CssClass="warning" Visible="false"></asp:Label>
                <asp:Repeater ID="rptState" runat="server" OnItemCommand="rptState_ItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    S.No
                                </th>
                                <th>
                                    Country Name
                                </th>
                                <th>
                                    Zone Name
                                </th>
                                <th>
                                    State Name
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
                                <%# Eval("CountryName")%>
                            </td>
                            <td>
                                <%# Eval("ZoneName")%>
                            </td>
                            <td>
                                <%# Eval("StateName")%>
                            </td>
                            <td>
                                <asp:LinkButton ID="BtnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("StateId")%>'
                                    CommandName="Edit" CausesValidation="false" OnClientClick="return FocusLabel();" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <Aj:ucCustomPaging ID="ucCustomPaging" runat="server" />
            </fieldset>
            <div id="divCourseCategoryInsert" class="popup_block width43perc">
                <fieldset id="basicInfo">
                    <legend>Add State</legend>
                    <ul class="pouplist">
                        <li style="width: 99% !important;">
                            <label>
                                Country Name:</label>
                            <asp:DropDownList ID="ddlCountryName" Width="180px" onfocus="ClearLabel()" runat="server"
                                ValidationGroup="State">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvConutryName" InitialValue="0" runat="server" SetFocusOnError="true"
                                ControlToValidate="ddlCountryName" ValidationGroup="State" TabIndex="1"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Zone Name:</label>
                            <asp:DropDownList ID="ddlZoneName" runat="server" onfocus="ClearLabel()" Width="180px"
                                TabIndex="2" ValidationGroup="State">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvZoneName" runat="server" ControlToValidate="ddlZoneName"
                                SetFocusOnError="true" ValidationGroup="State"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                State Name:</label>
                            <asp:TextBox ID="txtStateName" runat="server" TabIndex="3" ValidationGroup="State"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStateNameEnter" runat="server" ControlToValidate="txtStateName"
                                SetFocusOnError="true" ValidationGroup="State"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnState" runat="server" Text="Add" TabIndex="4" OnClick="btnState_Click"
                                CausesValidation="true" ValidationGroup="State" />
                            <input id="btnReset" type="button" value="Reset" onclick="ClearAllFields();" title="Please Reset" />
                        </li>
                    </ul>
                </fieldset>
            </div>
            <script type="text/javascript">
                function ClearAllFields() {
                    document.getElementById('<%=ddlCountryName.ClientID %>').selectedIndex = 0;
                    document.getElementById('<%=ddlZoneName.ClientID %>').selectedIndex = 0;
                    document.getElementById('<%=txtStateName.ClientID %>').value = '';

                    window.scrollTo(0, 0);
                }
            </script>
            <script language="javascript" type="text/javascript">
                function ClearLabel() {
                    document.getElementById('<%=lblMsg.ClientID %>').style.display = "none";
                    document.getElementById('<%=lblErrorMsg.ClientID %>').style.display = "none";
                    document.getElementById('<%=lblwarning.ClientID %>').style.display = "none";
                }
 
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" alt="Loading_Image" title="Loading Image" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadImage").colorbox({ width: "550px", inline: true, href: "#uploadImagePanel" });
        });

        function closeOverlay() {
            $.colorbox.close();
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
        function ValidateStatus() {
            if (confirm("Are you sure you want to update the status?"))
                return true;
            else
                return false;
        }
        function colorboxDialogSubmitClicked(validationGroup, panelId) {

            if (typeof Page_ClientValidate !== 'undefined') {
                if (!Page_ClientValidate(validationGroup)) {
                    return true;
                }
            }
            $.colorbox.close();
            $("form").append($("#" + panelId));
            return true;
        }
    </script>
</asp:Content>
