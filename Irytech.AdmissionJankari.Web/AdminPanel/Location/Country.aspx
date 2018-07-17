<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    CodeBehind="Country.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Country" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="ContentCountry" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlState" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnCountryMaster" runat="server" />


<ul class="addPage_utility">
        <li class="fright" style="width: 130px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddCountry' class="insertIco" onclick="OpenPoup('divCountryInsert','650px','sndAddCountry');return false;">
                                    Add Country</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload" runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"  ValidationGroup="GrUpload"
                            TabIndex="4" OnClick="btnUpload_Click" />
            <asp:ImageButton ID="btnPreview" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" runat="server" ToolTip="Preview Excel"  TabIndex="5" OnClick="btnPreview_Click" />
        </li>
    </ul>







            <div class="grdOuterDiv">
                <asp:Label ID="lblSuccess" CssClass="success hide" runat="server"></asp:Label>
                <asp:Label ID="lblInform" runat="server" Visible="false" Text="Label"></asp:Label>
                <asp:Label ID="lblError" CssClass="error" runat="server" Visible="false" Text="Label"></asp:Label>
                <fieldset style="display:none;">
                    <legend>Country Master</legend>
                    <ul class="options-bar">
                        <li class="opt-barlist">
                            <label>
                                Upload File:
                            </label>
                            <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="4" />
                            <asp:RequiredFieldValidator ID="rfvExcelUpload" Display="Dynamic" runat="Server" ControlToValidate="fileUploadExcel"
                                ValidationGroup="ExcelUpload" />
                        </li>
                        <li class="width12perc fleft">
                            <asp:Button  runat="server" Text="Upload Excel" CssClass="uploadbtn" TabIndex="12" OnClick="btnUpload_Click" /></li>
                        <li class="width24perc fleft">
                            <label style="width: 80px !important;">
                                See Excel:</label>
                            <asp:Button  runat="server" Text="Preview Excel"  CssClass="downloadbtn" TabIndex="5" OnClick="btnPreview_Click" />
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

                
                <fieldset>
                   <legend> Country List</legend>
                        <asp:Repeater ID="rptCountry" runat="server">
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
                                            Country Code
                                        </th>
                                        <th>
                                            Action
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id='<%# Eval("CountryId")%>'>
                                    <td>
                                        <%# Eval("SrNo")%>
                                    </td>
                                    <td>
                                   <span id="spnCountryName"> <%# Eval("CountryName")%></span>
                                    </td>
                                    <td>
                                     <span id="spnCountryCode"><%# Eval("CountryCode")%></span>
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
                        <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                     
                </fieldset>
                </div>
                 
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divCountryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add Country</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                        Country Name:</label>
                    <asp:TextBox ID="txtCountryName" runat="server" ValidationGroup="Country" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCountryName" Display="Dynamic" runat="server"
                        SetFocusOnError="true" ControlToValidate="txtCountryName" ValidationGroup="Country"></asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        Country Code:</label>
                    <asp:TextBox ID="txtCountryCode" runat="server" ValidationGroup="Country" TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCountryCode" runat="server" ControlToValidate="txtCountryCode"
                        SetFocusOnError="true" Display="Dynamic" ValidationGroup="Country"></asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnCountryMasterSubmit" runat="server" Text="Save" CausesValidation="true"
                        ValidationGroup="Country" TabIndex="3" OnClick="btnCountryMasterSubmit_Click" />
                    <input id="btnReset" type="button" value="Clear" tabindex="4" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script type="text/javascript">
        function ClearFields() {
            $("#<%=txtCountryName.ClientID %>").val('');
            $("#<%=txtCountryCode.ClientID %>").val('');
            $("#<%=btnCountryMasterSubmit.ClientID %>").val('Save');
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
            var rankSourceName = tr.find("span[id='spnCountryName']");
            var rankSourceStatus = tr.find("span[id='spnCountryCode']");

            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label


            rankSourceName.before("<input id='spnCountryNameEdit' type='text' value='" + rankSourceName.text().trim() + "'/>").hide();
            rankSourceStatus.before("<input id='spnCountryCodeEdit' type='text' value='" + rankSourceStatus.text().trim() + "'/>").hide();





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

            var firstName = tr.find("[id='spnCountryNameEdit']");
            var lastName = tr.find("[id='spnCountryCodeEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spnCountryName']").text(firstName.val()).show();
            tr.find("span[id='spnCountryCode']").text(lastName.val()).show();

            // Remove the input elements
            firstName.remove();
            lastName.remove();

            // Show the buttons again and remove the save
            tr.find("[id*='edit']").show();
            tr.find("[id*='save']").remove();

            // update the contact on the server
            UpdateContact(tr.attr("id"), firstName.val(), lastName.val())
        }

        function UpdateContact(id, firstName, lastName) {

            var data = '{'
            + "\"countryId\":" + id + ","
            + "\"countryName\":\"" + firstName + "\","
            + "\"countyCode\":\"" + lastName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "Country.aspx/UpdateCountryMaster",
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
