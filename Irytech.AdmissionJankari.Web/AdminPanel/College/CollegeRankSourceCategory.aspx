<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="CollegeRankSourceCategory.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.CollegeRankSourceCategory" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnCollegeRankSource"></asp:HiddenField>
            <asp:Label ID="lblSuccess" runat="server" CssClass="success hide" Visible="true" >
            </asp:Label>
            <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
            </asp:Label>
            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
            </asp:Label>

            <ul class="addPage_utility">
        <li class="fright" style="width: 165px !important;">
            <div class="navbar-inner">
              <a href="#" id='sndAddCollegeRankCategory' class="insertIco" onclick="OpenPoup('divRankSourceInsert','650px','sndAddCollegeRankCategory');return false;">Add Rank Source</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
           <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ToolTip="Upload Excel" ValidationGroup="GrUpload" />
             <asp:ImageButton ID="btnSeeExcelFormat" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" ToolTip="Download Excel" runat="server" />
        </li>
    </ul>





            <fieldset style="display:none;">
                <legend>Rank Source</legend>
                <ul class="options-bar">
                    <li class="opt-barlist">
                        <label>
                            Upload File :</label>
                        <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="5" />
                        <asp:RequiredFieldValidator ID="rfvUpload" runat="server" ControlToValidate="fulUploadExcel" Display="Dynamic" SetFocusOnError="True" ValidationGroup="GrUpload">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="fulUploadExcel" ValidationGroup="GrUpload">
                        </asp:RegularExpressionValidator>
                    </li>
                    <li class="width12perc fleft">
                        
                   
                        
                    </li>
                     
                </ul>
            </fieldset>
            <fieldset>
                <legend>
                   Course Category</legend>
                <asp:Repeater ID="rptCollegeRankSource" runat="server">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    S.No
                                </th>
                                <th>
                                    Rank Source
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
                        <tr id='<%# Eval("CollegeRankSourceId") %>'>
                            <td>
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td>
                                <span id="rankSourceName">
                                    <%# Eval("CollegeRankSourceName")%></span>
                            </td>
                            <td>
                                <span id="rankSourceStatus">
                                    <%# Eval("CollegeRankSourceStatus")%></span>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divRankSourceInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Rank Source Master Details</legend>
            <ul>
                <li>
                    <label>
                        College Rank</label>
                    <asp:TextBox ID="txtCollegeRank" runat="server" TabIndex="1" ToolTip="Please Enter College Rank">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCollegeRank" SetFocusOnError="true" runat="server" CssClass="error1" Display="Dynamic" ValidationGroup="RANK" ControlToValidate="txtCollegeRank">Field rank can not be blank
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Display</label>
                    <asp:CheckBox ID="chkCollegeRankStatus" runat="server" TabIndex="2" ToolTip="Select Status" />
                </li>
                <li>
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btnCollegeRank" runat="server" Text="Save" TabIndex="3" ValidationGroup="RANK" OnClick="BtnCollegeRankClick" />
                    <input id="btnReset" type="button" value="Clear" onclick="ClearFields()" />
                </li>
            </ul>
        </fieldset>
    </div>
    <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" />
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
            var rankSourceName = tr.find("span[id='rankSourceName']");
            var rankSourceStatus = tr.find("span[id='rankSourceStatus']");

            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label
            if (rankSourceStatus.text().trim().toLowerCase() == 'true') {
                rankSourceStatus.before("<input type='checkbox' id='rankSourceStatusEdit'  checked='checked'/>").hide();
            }
            else {
                rankSourceStatus.before("<input type='checkbox' id='rankSourceStatusEdit'  />").hide();
            }

            rankSourceName.before("<input id='rankSourceNameEdit' type='text' value='" + rankSourceName.text().trim() + "'/>").hide();





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

            var firstName = tr.find("[id='rankSourceNameEdit']");
            var lastName = tr.find("[id='rankSourceStatusEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='rankSourceName']").text(firstName.val()).show();
            tr.find("span[id='rankSourceStatus']").text(lastName.is(':checked')).show();

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
            + "\"rankSourceId\":" + id + ","
            + "\"rankSource\":\"" + firstName + "\","
            + "\"rankSourceStatus\":\"" + lastName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "CollegeRankSourceCategory.aspx/UpdateRankSource",
                data: data,
                contentType: "application/json",
                dataType: "json",
                success: function (response) {
                              
                    document.getElementById('<%=lblSuccess.ClientID %>').style.display = "block";
                    $("#<%=lblSuccess.ClientID%>").addClass("success");
                    $("#<%=lblSuccess.ClientID%>").removeClass("hide");
                    $("#<%=lblSuccess.ClientID%>").show();
                    $("#<%=lblSuccess.ClientID%>").text(response.d);
                },
                error: OnAjaxError
            });
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

        function ClearFields() {
            $("#<%=txtCollegeRank.ClientID %>").val('');
            $("#<%=btnCollegeRank.ClientID %>").attr('value', 'Save');
            $("#<%=chkCollegeRankStatus.ClientID %>").attr('checked', false);
            window.scrollTo(0, 0);
        } </script>
    <script language="javascript" type="text/javascript">
        function ClearLabel() {
            document.getElementById('<%=lblSuccess.ClientID %>').style.display = "none";
            document.getElementById('<%=lblError.ClientID %>').style.display = "none";
        }</script>
</asp:Content>
