<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    CodeBehind="Zone.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Zone" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="ContentCountry" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        <ul class="addPage_utility">
        <li class="fright" style="width: 110px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddZone' class="insertIco" onclick="OpenPoup('divZoneInsert','650px','sndAddZone');return false;">
                                Add Zone</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload" runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" 
                            TabIndex="4" OnClick="btnUpload_Click" />
            <asp:ImageButton ID="btnPreview" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" runat="server" ToolTip="Preview Excel"  TabIndex="5" OnClick="btnPreview_Click" />
        </li>
    </ul>








            <asp:Label ID="lblMsg" CssClass="success hide" runat="server"></asp:Label>
            <asp:Label ID="lblErrorMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
            <fieldset style="display:none;">
                <legend>Zone Master</legend>
                <ul class="options-bar">
                    <li class="opt-barlist">
                        <label>
                            Upload File:
                        </label>
                        <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="3" />
                        <asp:RequiredFieldValidator ID="rfvUploadExcel" runat="server" ControlToValidate="fileUploadExcel" Display="Dynamic"
                            ValidationGroup="GrUpload"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadExcel" runat="server" Display="Dynamic" ControlToValidate="fileUploadExcel"
                            ValidationGroup="GrUpload"></asp:RegularExpressionValidator>
                    </li>
                     <li class="width12perc fleft">
                        </li>
                     <li class="width24perc fleft">
                        <label style="width: 80px !important;">
                            See Excel:</label>
                        
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
            <legend>Zone List</legend>
                <asp:Repeater ID="rptZone" runat="server">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr id='<%# Eval("ZoneId")%>'>
                                <th>
                                    S.No
                                </th>
                                <th>
                                    Zone Name
                                </th>
                                <th>
                                    Action
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id='<%# Eval("ZoneId")%>'>
                            <td>
                                <%# Eval("SrNo")%>
                            </td>
                            <td>
                                <span id="zoneName">
                                    <%# Eval("ZoneName")%></span>
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
    <div id="divZoneInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add Zone</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                        Zone Name:</label>
                    <asp:TextBox ID="txtZoneName" runat="server" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvZoneName" runat="server" ControlToValidate="txtZoneName"
                        ValidationGroup="Zone"></asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnZone" runat="server" Text="Add" TabIndex="2" CausesValidation="true"
                        ValidationGroup="Zone" OnClick="btnZone_Click" />
                    <input id="btnReset" type="button" value="Reset" title="Please Reset" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script type="text/javascript">
        function ClearAllFields() {
            document.getElementById('ctl00_ContentPlaceHolderMain_txtZoneName').value = '';
            window.scrollTo(0, 0);
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
            var rankSourceName = tr.find("span[id='zoneName']");


            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label


            rankSourceName.before("<input id='zoneNameEdit' type='text' value='" + rankSourceName.text().trim() + "'/>").hide();





            // Hide the existing buttons and add a save button in there place
            tr.find("[id*='edit']").before("<img id='save' class='editIconmargin' src='/AdminPanel/Images/CommonImages/base_floppydisk_32.png'title='Save' alt='Save' width='14px' />")
         .hide();

            tr.find("[id*='save']").one('click', OnSave);
            $("#<%=lblMsg.ClientID%>").removeClass("success");
            $("#<%=lblMsg.ClientID%>").removeClass("show");
            $("#<%=lblMsg.ClientID%>").addClass("hide");
        }

        function OnSave() {
            // Get the row this button is within
            var tr = $(this).closest("tr");

            var firstName = tr.find("[id='zoneNameEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='zoneName']").text(firstName.val()).show();


            // Remove the input elements
            firstName.remove();


            // Show the buttons again and remove the save
            tr.find("[id*='edit']").show();
            tr.find("[id*='save']").remove();

            // update the contact on the server
            UpdateContact(tr.attr("id"), firstName.val())
        }

        function UpdateContact(id, zoneName) {

            alert(id);
            alert(zoneName);
            var data = '{'
            + "\"zoneId\":" + id + ","
            + "\"zoneName\":\"" + zoneName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "Zone.aspx/UpdateZoneList",
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: function (response) {

                    $("#<%=lblMsg.ClientID%>").addClass("success");
                    $("#<%=lblMsg.ClientID%>").removeClass("hide");
                    $("#<%=lblMsg.ClientID%>").text(response.d);
                },
                error: OnAjaxError
            });
        }
    </script>
</asp:Content>
