<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CoursePaymentMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Course.CoursePaymentMaster" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <ul class="addPage_utility">
        <li class="fright" style="width: 198px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddFormPrice' class="insertIco" onclick="OpenPoup('divCourseCategoryInsert','650px','sndAddFormPrice');return false;">
                                    Add Online Form Price</a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>   
            
            
            
            
            
            
            
                <label id="lblMsg" runat="server" class="success hide">
                </label>
                <fieldset>
                    <legend>Online Form Price Master</legend>
                    
                    <asp:Repeater ID="rptCoursePaymentAmount" runat="server">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                     <th>
                                        Course Name
                                    </th>
                                    <th>
                                        OnlineAmount
                                    </th>
                                    <th>
                                        OfflineAmount
                                    </th>
                                    <th>
                                        Action
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id='<%# Eval("PaymentCourseId")%>'>
                                <td>
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td id='<%# Eval("CourseId")%>'>
                                     <%# Eval("CourseName")%>
                                </td>
                                
                                <td>
                               <span id="spnOnlineAmount"> <%# Eval("OnlinePaymentAmount")%></span>
                                </td>
                                <td>
                                 <spn id="spnOfflineAmount"> <%# Eval("OfflinePaymentAmount")%></spn>
                                </td>
                                <td>
                                 <img src="/AdminPanel/Images/CommonImages/editIcon.png" title="Edit" alt="Edit-Icon" id="edit" class="editIconmargin" width="12px" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <aj:CustomPaging ID="ucCustomPaging" runat="server" />
                </fieldset>
                <div id="divCourseCategoryInsert" class="popup_block width43perc">
                    <fieldset id="basicInfo">
                        <legend>Add Course Eligibilty</legend>
                        <ul class="pouplist">
                            <li style="width: 99% !important;">
                                <label>
                                    Select Course Category</label>
                                <asp:DropDownList ID="ddlCourseCategory" runat="server" TabIndex="1" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCourseCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCoursePaymentCategory" SetFocusOnError="true"
                                    runat="server" Display="Dynamic" ValidationGroup="course" ControlToValidate="ddlCourseCategory"
                                    InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li style="width: 99% !important;">
                                <label>
                                    Course</label>
                                <asp:DropDownList ID="ddlCourse" runat="server" TabIndex="2" AutoPostBack="false">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlCourse" SetFocusOnError="true" runat="server"
                                    Display="Dynamic" ValidationGroup="course" ControlToValidate="ddlCourse" InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li style="width: 99% !important;">
                                <label>
                                    Online  Amount</label>
                                <asp:TextBox ID="txtOnlinePaymentAmount" TabIndex="3" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOnlinePaymentAmount" SetFocusOnError="true" runat="server"
                                    Display="Dynamic" ValidationGroup="course" ControlToValidate="txtOnlinePaymentAmount">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li style="width: 99% !important;">
                                <label>
                                    Offline  Amount</label>
                                <asp:TextBox ID="txtOfflinePaymentAmount" TabIndex="4" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOfflinePaymentAmount" SetFocusOnError="true" runat="server"
                                    Display="Dynamic" ValidationGroup="course" ControlToValidate="txtOfflinePaymentAmount">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li style="width: 99% !important;">
                                <label>
                                    &nbsp;</label>
                                <asp:Button ID="btntCoursePaymentAmount" TabIndex="5" runat="server" Text="Save"
                                    ValidationGroup="course" CausesValidation="true" ToolTip="Please Submit" OnClick="btntCoursePaymentAmount_Click" />
                                <input id="btnReset" type="button" onclick="ClearFields()" value="Clear" />
                            </li>
                        </ul>
                    </fieldset>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ClearFields() {
            $("#<%=ddlCourseCategory.ClientID %>").val(0);
            $("#<%=ddlCourse.ClientID %>").val(0);
            $("#<%=txtOnlinePaymentAmount.ClientID %>").val('');
            $("#<%=txtOfflinePaymentAmount.ClientID %>").val('');
            $("#<%=btntCoursePaymentAmount.ClientID %>").attr('value', 'Save');
            document.getElementById('<%=lblMsg.ClientID %>').style.display = "none";
            window.scrollTo(0, 0);
        }
        $(document).ready(function () {
            // Use live so when adding new records the events will
            // automatically be bounde
            if ($('<%=lblMsg.ClientID %>').text() == '') {
                $('<%=lblMsg.ClientID %>').hide();
            }
            $("[id*='edit']").live('click', OnEdit);

        });
        function OnEdit() {
            // Get the row this button is within
            var tr = $(this).closest("tr");
            // Get the first and last name controls in this row
            var onlineAmount = tr.find("span[id='spnOnlineAmount']");
            var offLineAmount = tr.find("span[id='spnOfflineAmount']");

            // Insert an input element before the labels
            // and set the value to the label text
            // Then hide the label
            alert(offLineAmount.text());
            onlineAmount.before("<input id='spnOnlineAmountEdit' type='text' value='" + onlineAmount.text().trim() + "'/>").hide();
            offLineAmount.before("<input id='spnOfflineAmountEdit' type='text' value='" + offLineAmount.text().trim() + "'/>").hide();

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

              var onlineAmount = tr.find("span[id='spnOnlineAmountEdit']");
              var offLineAmount = tr.find("span[id='spnOfflineAmountEdit']");


            // Set the text of the labels from the input elements and show them
            tr.find("span[id='spnOnlineAmount']").text(onlineAmount.val()).show();
            tr.find("span[id='OfflinePaymentAmount']").text(offLineAmount.val()).show();

            // Remove the input elements
            onlineAmount.remove();
            offLineAmount.remove();

            // Show the buttons again and remove the save
            tr.find("[id*='edit']").show();
            tr.find("[id*='save']").remove();

            // Get The course Id
            var course = tr.next("td");
            alert(course.attr("id"));
            // update the contact on the server
            UpdateContact(tr.attr("id"), onlineAmount.val(), offLineAmount.val())
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

                    $("#<%=lblMsg.ClientID%>").addClass("success");
                    $("#<%=lblMsg.ClientID%>").removeClass("hide");
                    $("#<%=lblMsg.ClientID%>").text(response.d);
                },
                error: OnAjaxError
            });
        }
    </script>
   
</asp:Content>
