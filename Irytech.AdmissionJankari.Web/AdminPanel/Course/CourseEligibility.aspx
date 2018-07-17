<%@ Page Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="True" CodeBehind="CourseEligibility.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Course.CourseEligibility" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="grdOuterDiv">
        <div id="fade">
        </div>
        <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
        <div id="divImage" class="loading">
            <img alt="Please wait" src="/image.axd?Common=Loading.gif" /></div>
        <asp:Label ID="lblSuccess" runat="server" Text="" CssClass="success hide"></asp:Label>
        <asp:Label ID="lblInfo" runat="server" Text="" Visible="false" CssClass="info"></asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error"></asp:Label>
         <ul class="addPage_utility">
        <li class="fright" style="width: 195px !important;">
            <div class="navbar-inner">
               <a href="#" id='sndAddCourseEligibilty' class="insertIco" onclick="OpenPoup('divCourseCategoryInsert','650px','sndAddCourseEligibilty');return false;">Add Course Eligibilty</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload"  runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"  ValidationGroup="GrUpload"
                            TabIndex="4" OnClick="BtnUploadClick" /><asp:Label runat="server" Text="" ID="lblInsert"></asp:Label>
            <asp:ImageButton ID="btnSeeExcelFormat" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" runat="server" ToolTip="Preview Excel"  TabIndex="5" OnClick="BtnSeeExcelFormatClick" />
        </li>
    </ul>
        
        
        
        
        
        <fieldset style="display:none;">
            <legend>Course Eligibilty Master</legend>
            <ul class="options-bar">
                <li class="opt-barlist">
                    <label>
                        Upload File :</label>
                    <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="5" />
                    <asp:RequiredFieldValidator ID="rfvUpload" runat="Server" ControlToValidate="fulUploadExcel" Display="Dynamic" ValidationGroup="ExcelUpload" />
                    <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" Display="Dynamic" ControlToValidate="fulUploadExcel" ValidationGroup="GrUpload"></asp:RegularExpressionValidator>
                </li>
               
            </ul>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <legend>Course Eligibilty Master</legend>
                    <asp:Repeater ID="rptCourseEligibility" runat="server">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr id='<%# Eval("CourseEligibilityId")%>'>
                                    <th>
                                        S.NO
                                    </th>
                                    <th>
                                        Course Eligibilty
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id=' <%# Eval("CourseEligibilityId")%>'>
                                <td>
                                    <%#Eval("SrNo") %>
                                </td>
                                <td>
                                    <span id="spncourseEligibiltyName">
                                        <%# Eval("CourseEligibiltyName")%></span>
                                </td>
                                <td>
                                    <span id="spnCourseEligibiltyStatus">
                                        <%# Eval("CourseEligibilityStatus")%></span>
                                </td>
                                <td>
                                    <img src="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" id="edit" class="editIconmargin" width="12px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <AJ:CustomPaging ID="usCustomPaging" runat="server" />
                </fieldset>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divCourseCategoryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add Course Eligibilty</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                        Course Eligibilty</label>
                    <asp:TextBox ID="txtCourseEligibilty" onfocus="ClearLabel()" runat="server" TabIndex="1" ToolTip="Please Enter Eligibilty Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCourseEligibilty" SetFocusOnError="true" runat="server" Display="Dynamic" ValidationGroup="course" ControlToValidate="txtCourseEligibilty"></asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        Display
                    </label>
                    <asp:CheckBox ID="chkStatus" runat="server" TabIndex="2" ToolTip="Select Status" />
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnCourseEligibility" runat="server" Text="Save" ValidationGroup="course" TabIndex="3" ToolTip="Please Submit" CausesValidation="true" OnClick="BtnCourseEligibilityClick" />
                    <input id="btnClear" type="button" value="Clear " tabindex="4" onclick="ClearFields()" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script type="text/javascript">
        function ClearFields() {
            $("#<%=txtCourseEligibilty.ClientID %>").val('');
            $("#<%=btnCourseEligibility.ClientID %>").attr('value', 'Save');
            $("#<%=chkStatus.ClientID %>").attr('checked', false);
            window.scrollTo(0, 0);
        }


        $(document).ready(function () {
            // Use live so when adding new records the events will
            // automatically be bounde
            if ($('<%=lblSuccess.ClientID %>').text() == '') {
                $('<%=lblSuccess.ClientID %>').hide();
            }
            $("[id*='edit']").live('click', OnEdit);

        });
        function OnEdit() {
            // Get the row this button is within
            var tr = $(this).closest("tr");
            // Get the first and last name controls in this row
            var rankSourceName = tr.find("span[id='spncourseEligibiltyName']");
            var rankSourceStatus = tr.find("span[id='spnCourseEligibiltyStatus']");

            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label
            if (rankSourceStatus.text().trim().toLowerCase() == 'true') {
                rankSourceStatus.before("<input type='checkbox' id='spnCourseEligibiltyStatusEdit'  checked='checked'/>").hide();
            }
            else {
                rankSourceStatus.before("<input type='checkbox' id='spnCourseEligibiltyStatusEdit'  />").hide();
            }

            rankSourceName.before("<input id='spncourseEligibiltyNameEdit' type='text' value='" + rankSourceName.text().trim() + "'/>").hide();





            // Hide the existing buttons and add a save button in there place
            tr.find("[id*='edit']").before("<img id='save' class='editIconmargin' src='/AdminPanel/Images/CommonImages/base_floppydisk_32.png'title='Save' alt='Save' width='14px' />")
         .hide();

            tr.find("[id*='save']").one('click', OnSave);
            $("#<%=lblSuccess.ClientID%>").removeClass("success");
            $("#<%=lblSuccess.ClientID%>").removeClass("show");
            $("#<%=lblSuccess.ClientID%>").addClass("hide");
        }

        function OnSave() {
            // Get the row this button is within
            var tr = $(this).closest("tr");

            var firstName = tr.find("[id='spncourseEligibiltyNameEdit']");
            var lastName = tr.find("[id='spnCourseEligibiltyStatusEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spncourseEligibiltyName']").text(firstName.val()).show();
            tr.find("span[id='spnCourseEligibiltyStatus']").text(lastName.is(':checked')).show();

            // Remove the input elements
            firstName.remove();
            lastName.remove();

            // Show the buttons again and remove the save
            tr.find("[id*='edit']").show();
            tr.find("[id*='save']").remove();

            // update the contact on the server
            UpdateContact(tr.attr("id"), firstName.val(), lastName.is(':checked'))
        }

        function UpdateContact(id, firstName, lastName) {

            var data = '{'
            + "\"courseEligibiltyId\":" + id + ","
            + "\"courseEligibiltyName\":\"" + firstName + "\","
            + "\"courseEligibiltyStatus\":\"" + lastName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "CourseEligibility.aspx/UpdateCourseEligibilty",
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: function (response) {

                    $("#<%=lblSuccess.ClientID%>").addClass("success");
                    $("#<%=lblSuccess.ClientID%>").removeClass("hide");
                    $("#<%=lblSuccess.ClientID%>").text(response.d);
                },
                error: OnAjaxError
            });
        }
    </script>
</asp:Content>
