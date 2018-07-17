<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="NoticeCategory.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.NoticeCategory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnNoticeCategoryId"></asp:HiddenField>
            <ul class="addPage_utility">
        <li class="fright" style="width: 185px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddNoticeCategory' class="insertIco" onclick="OpenPoup('divCourseCategoryInsert','650px','sndAddNoticeCategory');return false;">Add Notice Category</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton  ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" ID="btnUpload" runat="server" ToolTip="Upload Excel"   OnClick="BtnUploadClick" />
                           
            <asp:ImageButton ID="btnSeeExcelFormat" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" runat="server" ToolTip="Excel Format"  OnClick="BtnSeeExcelFormatClick" />
        </li>
    </ul>




            <div class="grdOuterDiv">
                 <asp:Label ID="lblSuccess" runat="server" Text="" CssClass="success hide">
                </asp:Label>
                <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
                </asp:Label>
                <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
                </asp:Label>
                  <fieldset style="display:none;">
            <legend>Notice Category Master</legend>
            <ul class="options-bar">
                <li class="opt-barlist">
                    <label>
                                Upload File :</label>
                            <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="5" />
                            <asp:RequiredFieldValidator ID="rfvExcelUpload" Display="Dynamic" runat="Server" ControlToValidate="fulUploadExcel"
                                ValidationGroup="ExcelUpload" />
                            <asp:RegularExpressionValidator ID="revExcelUpload" Display="Dynamic" runat="server" ControlToValidate="fulUploadExcel"
                                ValidationGroup="GrUpload">
                            </asp:RegularExpressionValidator>
                </li>
                <li class="width12perc fleft">
                </li>
                
                     
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
                <legend>Notice Category List</legend>
                   
                    <asp:Repeater ID="rptNoticeCategory" runat="server" >
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr id='<%# Eval("NoticecategoryId")%>'>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        Notice Category
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
                            <tr id='<%# Eval("NoticeCategoryId")%>'>
                                <td>
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td>
                              <span id="spnNoticeCategoryName"> <%# Eval("NoticeCategoryName")%></span>
                                </td>
                                <td>
                               
                              <span id="spnNoticeCategoryStatus"> <%# Convert.ToBoolean(Eval("NoticeCategoryStatus"))%></span>
                                </td>
                                <td>
                                    <img src="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" id="edit" class="editIconmargin" width="12px" />
                                </td>
                            </tr>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
               </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
      <div id="divCourseCategoryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add Notice Category</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                                Notice Category</label>
                            <asp:TextBox ID="txtNoticeCategory" runat="server"  TabIndex="1"
                                 ToolTip="Please Enter Notice Category">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNoticeCategory" SetFocusOnError="true" runat="server"
                                Display="Dynamic" ValidationGroup="Notice" ControlToValidate="txtNoticeCategory"
                                ErrorMessage="Please Enter Notice Category">
                            </asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                         <label>
                                Display</label>
                            <asp:CheckBox ID="chkNoticeStatus" runat="server" TabIndex="2" ToolTip="Check Status" />
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                   <asp:Button ID="btnNoticeCategory" runat="server" Text="Save" ValidationGroup="Notice"
                                CausesValidation="True" TabIndex="3" ToolTip="Please Submit" OnClick="BtnNoticeCategoryClick" />
                    <input id="btnClear" type="button" value="Clear " tabindex="4" onclick="ClearFields()" />
                </li>
            </ul>
        </fieldset>
    </div>
    <script language="javascript" type="text/javascript">
        function ClearFields() {
            $("#<%=txtNoticeCategory.ClientID %>").val('');
            $("#<%=chkNoticeStatus.ClientID %>").attr('checked', false);
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
            var rankSourceName = tr.find("span[id='spnNoticeCategoryName']");
            var rankSourceStatus = tr.find("span[id='spnNoticeCategoryStatus']");

            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label
            if (rankSourceStatus.text().trim().toLowerCase() == 'true') {
                rankSourceStatus.before("<input type='checkbox' id='spnNoticeCategoryStatusEdit'  checked='checked'/>").hide();
            }
            else {
                rankSourceStatus.before("<input type='checkbox' id='spnNoticeCategoryStatusEdit'  />").hide();
            }

            rankSourceName.before("<input id='spnNoticeCategoryNameEdit' type='text' value='" + rankSourceName.text().trim() + "'/>").hide();





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

            var firstName = tr.find("[id='spnNoticeCategoryNameEdit']");
            var lastName = tr.find("[id='spnNoticeCategoryStatusEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spnNoticeCategoryName']").text(firstName.val()).show();
            tr.find("span[id='spnNoticeCategoryStatus']").text(lastName.is(':checked')).show();

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
            + "\"noticeCategoryId\":" + id + ","
            + "\"noticeCategoryName\":\"" + firstName + "\","
            + "\"noticeCategoryStatus\":\"" + lastName + "\""
            + '}';
          
            $.ajax({
                type: "POST",
                url: "NoticeCategory.aspx/UpdateNoticeCategory",
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
