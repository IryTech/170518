<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="ManageCollegeEvent.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.ManageCollegeEvent" %>
    <%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:HiddenField ID="hndEventId" runat="server" />
    <div>
        <fieldset>
            <legend>Insert College Event</legend>
            <ul>
                <li>
                  
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </li>
                <li>
                    <label>
                        <%=Resources.label.Course %>
                    </label>
                    <asp:DropDownList runat="server" ID="ddlCourse" ToolTip="Please select course" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvcourseName" ValidationGroup="Event"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="ddlCourse"
                        InitialValue="0"> 
                 Filed Course cannot be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Choose College:
                    </label>
                    <asp:TextBox ID="txtCollegeName" ToolTip="Enter College Name" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvCollege" ValidationGroup="Event"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtCollegeName"> 
                  Field  Choose College cannot be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Event Name:
                    </label>
                    <asp:TextBox runat="server" ID="txtEventName" ToolTip="Please enter Event name" TabIndex="3">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvEventName" ValidationGroup="Event"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventName"> 
                  Field Event Name cannot be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Event Date:
                    </label>
                    <asp:TextBox ID="txtEventDate" runat="server" ToolTip="Enter Event Date"></asp:TextBox>
                    <Ajex:CalendarExtender ID="ajexClndr" runat="server" TargetControlID="txtEventDate"
                        PopupButtonID="txtEventDate" PopupPosition="Right" Format="dd/MM/yyyy">
                    </Ajex:CalendarExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvEventDate" ValidationGroup="Event"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventDate"> 
                            Field Event date cannot be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Event Location:</label>
                    <asp:TextBox ID="txtEventLocation" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEventLocation" runat="server" ValidationGroup="Event"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventLocation">
                    Field Event Location cannot be blank
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Event Description:
                    </label>
                    <asp:TextBox ID="txtEventDesc" runat="server" TextMode="MultiLine" Rows="5" ToolTip="Please enter Event Desc"
                        TabIndex="4">
                      
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvEventDesc" ValidationGroup="Event"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEventDesc"> 
                Field Event Description cannot be blank

                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Event Status:
                    </label>
                    <asp:CheckBox ID="chkEventStatus" Text="" runat="server"  />
                </li>
                <li><label>&nbsp;</label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="Event"
                        OnClick="txtSave_Click" />
                </li>
            </ul>
        </fieldset>
        <div class="grdOuterDiv">
        <h3>Event List</h3> 
            <ul>
                <li>
                    <asp:Repeater ID="rptEventList" runat="server" 
                        onitemcommand="rptEventList_ItemCommand" >
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                       College Name
                                    </th>
                                    <th>
                                        Event Name
                                    </th>
                                    <th>
                                        Event Date
                                    </th>
                                    <th>
                                        Event Status
                                    </th>
                                    <th>
                                        Action
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("SrNo") %>
                                </td>
                                <td>
                                    <%# Eval("AjCollegeBranchName")%>
                                </td>
                                <td>
                                    <%# Eval("AjCollegeEventName")%>
                                </td>
                                <td>
                                    <%# Eval("AjCollegeEventDate")%>
                                </td>
                                   <td>
                                    <%# Eval("AjCollegeEventStatus")%>
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("AjCollegeEventId")%>'
                                        CommandName="Edit" CausesValidation="false" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <aj:custompaging id="ucCustomPaging" runat="server" />
                </li>
            </ul>
        
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/Autocomplete.js"></script>
    <script type="text/javascript" src="../Scripts/CommonFrontScript.js"></script>
    <script type="text/javascript" defer="defer">
        $(document).ready(function () {
            showCollegeDetailsWiseCollegeNameAndCourseId($("#<%=txtCollegeName.ClientID %>"), null, null, null, $("#<%= ddlCourse.ClientID %>").val());
            $("#<%=ddlCourse.ClientID %>").change(function () {
                if ($("#<%= ddlCourse.ClientID %>").val() > 0) {
              
                    ChangeCourseId($("#<%= ddlCourse.ClientID %>").val());
                    showCollegeDetailsWiseCollegeNameAndCourseId($("#<%=txtCollegeName.ClientID %>"), null, null, null, $("#<%= ddlCourse.ClientID %>").val());
                } else {

                    alert("Select course");
                    return false;
                }

            });
        });
    </script>
</asp:Content>
