<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="CourseCategory.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Course.CourseCategory" %>

<%@ Register Src="../UserControl/ExcelSuccessCount.ascx" TagName="ExcelSuccessCount" TagPrefix="AJ" %>
<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="grdOuterDiv">
        <div id="fade">
        </div>
        <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
        <div id="divImage" class="loading">
            <img alt="Please wait" src="/image.axd?Common=Loading.gif" />
        </div>
        <asp:Label ID="lblSuccess" runat="server" CssClass="success hide">
        </asp:Label>
        <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
        </asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
        </asp:Label>
        
         <ul class="addPage_utility">
        <li class="fright" style="width: 195px !important;">
            <div class="navbar-inner">
               <a href="#" id='sndAddCourseType' class="insertIco" onclick="OpenPoup('divCourseCategoryInsert','650px','sndAddCourseType');return false;">Add Course Category</a>
                <div class="clear">
                </div>
            </div>
        </li>
        <li class="fright" style="width: 72px !important;">
            <asp:ImageButton ID="btnUpload"  runat="server" ToolTip="Upload Excel" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png"  ValidationGroup="GrUpload"
                            TabIndex="4" OnClick="BtnUploadClick" /> 
            <asp:ImageButton ID="btnSeeExcelFormat" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" runat="server" ToolTip="Preview Excel"  TabIndex="5" OnClick="BtnSeeExcelFormatClick" />
        </li>
    </ul>
        
        
        
        
        
        
        
        <fieldset style="display:none;">
            <legend>Course Category Master</legend>
            <ul class="options-bar" >
                <li class="opt-barlist">
                    <label>
                        Upload File :</label>
                    <asp:FileUpload ID="fulUploadExcel" runat="server" TabIndex="5" />
                    <asp:RequiredFieldValidator ID="rfvUpload" runat="server" ControlToValidate="fulUploadExcel" Display="Dynamic" SetFocusOnError="True" ValidationGroup="GrUpload">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revExcelUpload" runat="server" SetFocusOnError="True" ControlToValidate="fulUploadExcel" Display="Dynamic" ValidationGroup="GrUpload">
                    </asp:RegularExpressionValidator>
                </li>
                <li class="width12perc fleft">
                   </li>
                <li class="width24perc fleft">
                    <label style="width: 80px !important;">
                        See Excel:</label>
                    
                    <AJ:ExcelSuccessCount ID="exclSuccessCount" runat="server" />
                </li>
                <li class="width25perc fleft">
                    <div class="navbar-inner">
                        
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul></fieldset>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset>
                    <legend>Course Category Master</legend>
                    <asp:Repeater ID="rptCourseCategoryData" runat="server">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        Course Category Name
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='<%# Eval("CourseCategoryId")%>'>
                                <td>
                                    <%# Eval("SrNo") %>
                                </td>
                                <td>
                                    <span id="spnCoursecategory">
                                        <%# Eval("CourseCategoryName")%></span>
                                </td>
                                <td>
                                    <span id="spnCourseCategorySataus">
                                        <%# Eval("CourseCategoryStatus")%></span>
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
        
    </div>
    <div id="divCourseCategoryInsert" class="popup_block width43perc">
        <fieldset id="basicInfo">
            <legend>Add Course Category</legend>
            <ul class="pouplist">
                <li style="width: 99% !important;">
                    <label>
                        Course Type</label>
                    <asp:TextBox ID="txtCourseCategory" runat="server" TabIndex="1" ToolTip="Please Enter Course Type">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCourse" SetFocusOnError="true" runat="server" Display="Dynamic" ValidationGroup="course" ControlToValidate="txtCourseCategory">
                       
                    </asp:RequiredFieldValidator>
                </li>
                <li style="width: 99% !important;">
                    <label>
                        Display</label>
                    <asp:CheckBox ID="chkCategoryStatus" runat="server" TabIndex="2" ToolTip="Select Status" />
                </li>
                <li style="width: 99% !important;">
                    <label>
                        &nbsp;</label>
                    <asp:Button ID="btntCourseCategory" runat="server" Text="Save" ValidationGroup="course" CausesValidation="true" TabIndex="3" ToolTip="Please Submit" OnClick="BtntCourseCategoryClick" />
                    <input id="btnReset" type="button" value="Clear" onclick="ClearFields()" />
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
            var rankSourceName = tr.find("span[id='spnCoursecategory']");
            var rankSourceStatus = tr.find("span[id='spnCourseCategorySataus']");

            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label
            if (rankSourceStatus.text().trim().toLowerCase() == 'true') {
                rankSourceStatus.before("<input type='checkbox' id='spnCourseCategorySatausEdit'  checked='checked'/>").hide();
            }
            else {
                rankSourceStatus.before("<input type='checkbox' id='spnCourseCategorySatausEdit'  />").hide();
            }

            rankSourceName.before("<input id='spnCoursecategoryEdit' type='text' value='" + rankSourceName.text().trim() + "'/>").hide();





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

            var firstName = tr.find("[id='spnCoursecategoryEdit']");
            var lastName = tr.find("[id='spnCourseCategorySatausEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spnCoursecategory']").text(firstName.val()).show();
            tr.find("span[id='spnCourseCategorySataus']").text(lastName.is(':checked')).show();

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
            + "\"courseCategoryId\":" + id + ","
            + "\"courseCategoryName\":\"" + firstName + "\","
            + "\"courseCategoryStatus\":\"" + lastName + "\""
            + '}';
            $.ajax({
                type: "POST",
                url: "CourseCategory.aspx/UpdateCourseCategoryStatus",
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

        function ClearFields() {
            $("#<%=txtCourseCategory.ClientID %>").val('');
            $("#<%=btntCourseCategory.ClientID %>").attr('value', 'Save');
            $("#<%=chkCategoryStatus.ClientID %>").attr('checked', false);
            window.scrollTo(0, 0);
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
