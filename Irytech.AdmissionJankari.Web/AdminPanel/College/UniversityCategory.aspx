<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="UniversityCategory.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.UniversityCategory" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="grdOuterDiv">
        <asp:Label ID="lblSuccess" runat="server" CssClass="success hide">
        </asp:Label>
        <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
        </asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
        </asp:Label>
        <ul class="addPage_utility">
            <li class="fright" style="width: 208px !important;">
                <div class="navbar-inner">
                    <a href="#" id='sndAddUniversityType' class="insertIco" onclick="OpenPoup('divUniversityCategoryInsert','650px','sndAddUniversityType');return false;">
                        Add University Category</a>
                    <div class="clear">
                    </div>
                </div>
            </li>
            <li class="fright" style="width: 72px !important;">
                <asp:ImageButton ID="btnUpload" runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"
                    ValidationGroup="GrUpload" TabIndex="4" OnClick="btnUpload_Click" />
                <asp:ImageButton ID="btnViewExcelFormat" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png"
                    runat="server" ToolTip="Preview Excel" TabIndex="5" OnClick="BtnSeeExcelFormatClick" />
            </li>
        </ul>
        <fieldset style="display: none;">
            <legend>University Category Master</legend>
            <ul class="options-bar">
                <li class="opt-barlist">
                    <label>
                        Upload File :</label>
                    <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="5" />
                    <asp:RequiredFieldValidator ID="rfvExcelUpload" Display="Dynamic" runat="Server"
                        ControlToValidate="fileUploadExcel" ValidationGroup="ExcelUpload" />
                </li>
                <li class="width12perc fleft">
                    <asp:Button runat="server" CssClass="uploadbtn" Text="Upload Excel" /></li>
                <li class="width24perc fleft">
                    <label style="width: 80px !important;">
                        See Excel:</label>
                    <asp:Button runat="server" CssClass="downloadbtn" Text="Excel Format" />
                    <asp:Label runat="server" Text="" ID="lblRecordsInserted"></asp:Label>
                </li>
                <li class="width25perc fleft">
                    <div class="navbar-inner">
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <legend>University Category Master</legend>
                    <asp:Repeater ID="rptUniversityCategory" runat="server">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        UniversityCategoryName
                                    </th>
                                    <th>
                                        Action
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='<%# Eval("UniversityCategoryId")%>'>
                                <td>
                                    <%# Eval("SrNo")%>
                                </td>
                                <td>
                                    <span id="spnUniversityType">
                                        <%# Eval("UniversityCategoryName")%>
                                    </span>
                                </td>
                                <td>
                                    <img src="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon"
                                        id="edit" class="editIconmargin" width="12px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
                </fieldset>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divUniversityCategoryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add University Category</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                        University Category
                    </label>
                    <asp:TextBox ID="txtUniversityCategoryName" runat="server" TabIndex="1" ToolTip="Select Stream Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUniversityCategoryName" SetFocusOnError="true"
                        runat="server" Display="Dynamic" ValidationGroup="course" ControlToValidate="txtUniversityCategoryName"> </asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnUniversityCategoryName" runat="server" Text="Save" ValidationGroup="course"
                        CausesValidation="true" TabIndex="3" ToolTip="Please Submit" OnClick="BtntCourseCategoryClick" />
                    <input id="btnClear" type="button" value="Clear" title="Please Clear" tabindex="3"
                        onclick="ClearFields()" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script type="text/javascript">

        function ClearFields() {
            $('<%=txtUniversityCategoryName.ClientID %>').val('');
        }
        $(document).ready(function () {
            // Use live so when adding new records the events will
            // automatically be bounde
            $("[id*='edit']").live('click', OnEdit);

        });
        function OnEdit() {
            // Get the row this button is within
            var tr = $(this).closest("tr");
            // Get the first and last name controls in this row
            var instituteType = tr.find("span[id='spnUniversityType']");


            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label


            instituteType.before("<input id='spnInstituteTypeEdit' type='text' value='" + instituteType.text().trim() + "'/>").hide();


            // Hide the existing buttons and add a save button in there place
            tr.find("[id*='edit']").before("<img id='save' class='editIconmargin' src='/AdminPanel/Images/CommonImages/base_floppydisk_32.png'title='Save' alt='Save' width='14px' />")
         .hide();

            tr.find("[id*='save']").one('click', OnSave);
            c
        }
        function OnSave() {
            // Get the row this button is within
            var tr = $(this).closest("tr");

            var firstName = tr.find("[id='spnInstituteTypeEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spnUniversityType']").text(firstName.val()).show();


            // Remove the input elements
            firstName.remove();


            // Show the buttons again and remove the save
            tr.find("[id*='edit']").show();
            tr.find("[id*='save']").remove();

            // update the contact on the server
            UpdateContact(tr.attr("id"), firstName.val())
        }
        function UpdateContact(id, firstName) {

            var data = '{'
            + "\"universityCategoryId\":" + id + ","
            + "\"universityCategoryType\":\"" + firstName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "UniversityCategory.aspx/UpdateUniversityCategoryDetails",
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                    $("#<%=lblSuccess.ClientID%>").addClass("success");
                    $("#<%=lblSuccess.ClientID%>").removeClass("hide");
                    $("#<%=lblSuccess.ClientID%>").show();
                    $("#<%=lblSuccess.ClientID%>").text(response.d);
                },
                error: OnAjaxError
            });
        }
    </script>
</asp:Content>
