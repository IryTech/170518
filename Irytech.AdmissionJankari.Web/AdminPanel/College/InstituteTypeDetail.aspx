<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="InstituteTypeDetail.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.InstituteTypeDetail" %>

<asp:Content ID="cphInstituteDetails" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="grdOuterDiv">
                <asp:Label ID="lblSuccess" runat="server" CssClass="success hide"></asp:Label>
                <asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="info"></asp:Label>
                <asp:Label ID="lblError" runat="server" Visible="false" CssClass="error"></asp:Label>
               
                <ul class="addPage_utility">
        <li class="fright" style="width: 169px !important;">
            <div class="navbar-inner">
               <a href="#" id='sndAddInstituteType' class="insertIco" onclick="OpenPoup('divInstituteTypeInsert','650px','sndAddInstituteType');return false;">Add Institute Type</a>
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
                    <legend>
                        <asp:Label ID="lblHeader" runat="server"></asp:Label>
                    </legend>
                    <ul class="options-bar">
                        <li class="opt-barlist">
                            <label>
                                Upload File :</label>
                            <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="1" />
                            <asp:RequiredFieldValidator ID="rfvExcelUpload" Display="Dynamic" runat="Server" ControlToValidate="fulUploadExcel" ValidationGroup="ExcelUpload" />
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
                    <legend>Institute Type Master</legend>
                    <asp:Repeater ID="rptInstituteType" runat="server">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.NO
                                    </th>
                                    <th>
                                        InstituteTypeName
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='<%# Eval("InstituteTypeId")%>'>
                                <td>
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td>
                                    <span id="spnInstituteType">
                                        <%# Eval("InstituteType")%></span>
                                </td>
                                <td>
                                    <img src="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" id="edit" class="editIconmargin" width="12px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divInstituteTypeInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add Institute Type</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                        Institutte Type</label>
                    <asp:TextBox runat="server" ID="txtInstituteType" ToolTip="Please Enter Institutte Type" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvInstituteType" ControlToValidate="txtInstituteType" SetFocusOnError="True" ValidationGroup="InstituteTYpe" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button runat="server" Text="Save" ID="btnSave" ToolTip="Please Enter To Submit" TabIndex="2" ValidationGroup="InstituteTYpe" OnClick="BtnSaveClick" />
                    <input id="btnReset" type="button" value="Clear" title="Please Clear" tabindex="3" onclick="ClearFields()" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script type="text/javascript">
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
            var instituteType = tr.find("span[id='spnInstituteType']");


            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label


            instituteType.before("<input id='spnInstituteTypeEdit' type='text' value='" + instituteType.text().trim() + "'/>").hide();


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

            var firstName = tr.find("[id='spnInstituteTypeEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spnInstituteType']").text(firstName.val()).show();


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
            + "\"instituteTypeId\":" + id + ","
            + "\"instituteType\":\"" + firstName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "InstituteTypeDetail.aspx/UpdateInstituteType",
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

        function ClearFields() {
            $("#<%=txtInstituteType.ClientID %>").val('');
            window.scrollTo(0, 0);
        } </script>
</asp:Content>
