<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="HostelMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.HostelMaster" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="cphHostelMaster" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="grdOuterDiv">
                 <asp:Label ID="lblSuccess" CssClass="success hide"  runat="server"></asp:Label>
                <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
                </asp:Label>
                
                <ul class="addPage_utility">
        <li class="fright" style="width: 165px !important;">
            <div class="navbar-inner">
              <a href="#" id='sndAddHostelType' class="insertIco" onclick="OpenPoup('divRankSourceInsert','650px','sndAddHostelType');return false;">Add Rank Source</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload"  runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"  ValidationGroup="GrUpload"
                            TabIndex="4" OnClick="BtnUploadClick1"  /> 
            <asp:ImageButton ID="btnSeeExcelFormat" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" runat="server" ToolTip="Preview Excel"  TabIndex="5" OnClick="BtnUploadClick" />
        </li>
    </ul>
                
                
                
                
                <fieldset style="display:none;">
                    <legend>Hostel Type Master</legend>
                    <ul class="options-bar">
                        <li class="opt-barlist">
                            <label>
                                Upload File :</label>
                            <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="1" />
                            <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" Display="Dynamic" ControlToValidate="fulUploadExcel" ValidationGroup="ExcelUpload" />
                        </li>
                        <li class="width12perc fleft">
                            <asp:Button  runat="server" CssClass="uploadbtn" Text="Upload Excel" TabIndex="2"  /></li>
                        <li class="width24perc fleft">
                            <label style="width: 80px !important;">
                                See Excel:</label>
                            <asp:Button  runat="server" Text="Excel Format" CssClass="downloadbtn"  />
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
                    <legend>
                        <asp:Label runat="server" Text="" ID="lblEditStatus"></asp:Label></legend>
                    <asp:Repeater ID="rptHostel" runat="server">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        Hostel Type
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
                            <tr id='<%# Eval("HostelCategoryId")%>'>
                                <td>
                                    <%# Eval("SrNo") %>
                                </td>
                                <td>
                                    <span id="spnHostelType">
                                        <%# Eval("HostelCategoryType")%></span>
                                </td>
                                <td>
                                    <span id="spnHostelTypeStatus">
                                        <%# Eval("HostelCategoryStatus")%></span>
                                </td>
                                <td>
                                    <img src="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" id="edit" class="editIconmargin" width="12px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divRankSourceInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add Hostel Type</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                        Hostel Type</label>
                    <asp:TextBox ID="txtHostelType" runat="server" TabIndex="1" ToolTip="Please Enter Course Type">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvHostel" SetFocusOnError="true" runat="server" Display="Dynamic" ValidationGroup="hostel" ControlToValidate="txtHostelType">
                       
                    </asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        Display</label>
                    <asp:CheckBox ID="chkStatus" runat="server" TabIndex="2" ToolTip="Select Status" />
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="hostel" CausesValidation="true" TabIndex="3" ToolTip="Please Submit" OnClick="BtnSaveClick" />
                    <input id="btnReset" type="button" value="Clear" onclick="ClearFields()" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            // Use live so when adding new records the events will
            // automatically be bounde
            
            $("[id*='edit']").live('click', OnEdit);

        });

        function ClearFields() {
            $("#<%=txtHostelType.ClientID %>").val('');
            $("#<%=btnSave.ClientID %>").attr('value', 'Save');
            $("#<%=chkStatus.ClientID %>").attr('checked', false);
            window.scrollTo(0, 0);
        }



        function OnEdit() {
            // Get the row this button is within
            var tr = $(this).closest("tr");
            // Get the first and last name controls in this row
            var rankSourceName = tr.find("span[id='spnHostelType']");
            var rankSourceStatus = tr.find("span[id='spnHostelTypeStatus']");

            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label
            if (rankSourceStatus.text().trim().toLowerCase() == 'true') {
                rankSourceStatus.before("<input type='checkbox' id='spnHostelTypeStatusEdit'  checked='checked'/>").hide();
            }
            else {
                rankSourceStatus.before("<input type='checkbox' id='spnHostelTypeStatusEdit'/>").hide();
            }

            rankSourceName.before("<input id='spnHostelTypeEdit' type='text' value='" + rankSourceName.text().trim() + "'/>").hide();





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

            var firstName = tr.find("[id='spnHostelTypeEdit']");
            var lastName = tr.find("[id='spnHostelTypeStatusEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spnHostelType']").text(firstName.val()).show();
            tr.find("span[id='spnHostelTypeStatus']").text(lastName.is(':checked')).show();

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
            + "\"hostelTypeId\":" + id + ","
            + "\"hostelType\":\"" + firstName + "\","
            + "\"hostelTypeStatus\":\"" + lastName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "HostelMaster.aspx/UpdateInstituteType",
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
