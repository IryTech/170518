<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.User.User" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="cphUser" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updUser" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnUser"></asp:HiddenField>
            <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
            </asp:Label>
            <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
            </asp:Label>
            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
            </asp:Label>

           <ul class="addPage_utility">
        <li class="fright" style="width: 153px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddUserType' class="insertIco" onclick="OpenPoup('divUserTypeInsert','650px','sndAddUserType');return false;">Add User Type</a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>


            <fieldset>
               <legend>
                    <asp:Label runat="server" Text="" ID="lblEditStatus" Visible="false"></asp:Label></legend>
                <asp:Repeater ID="rptUser" runat="server" OnItemCommand="RptUserItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    S.NO
                                </th>
                                <th>
                                    User
                                </th>
                                <th>
                                    Url
                                </th>
                                <th>
                                    Status
                                </th>
                                <th>
                                    Create User
                                </th>
                                <th>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("SrNo")%>
                            </td>
                            <td>
                                <%# Eval("UserCategoryName")%>
                            </td>
                            <td>
                                <%# Eval("UserCategoryDashboard")%>
                            </td>
                            <td>
                                <%# Eval("UserCategoryStatus")%>
                            </td>
                            <td>
                                <%# Eval("CanCreateUser")%>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("UserCategoryId")%>' CommandName="Edit" CausesValidation="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
            </fieldset>
            <div id="divUserTypeInsert" class="popup_block width43perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblInsertUpdate" runat="server" Text="Insert"></asp:Label></legend>
                    <ul class="pouplist">
                        <li style="width: 99% !important;">
                            <label>
                                User Name</label>
                            <asp:TextBox runat="server" ID="txtUserName" ToolTip="Please Enter User Type" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvUserName" ControlToValidate="txtUserName" SetFocusOnError="True" ValidationGroup="User" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                DashBoardUrl</label>
                            <asp:TextBox runat="server" ID="txtDashBoardUrl" ToolTip="Please Enter Dashboard Url" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvDashBoardUrl" ControlToValidate="txtDashBoardUrl" SetFocusOnError="True" ValidationGroup="User" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Create User</label>
                            <asp:RadioButtonList runat="server" CssClass="RadioButtonList" ID="rbtCreateUser" ToolTip="User can Careate user or not" TabIndex="3" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                <asp:ListItem Value="1">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Display</label>
                            <asp:CheckBox runat="server" ID="chkStatus" TabIndex="4" ToolTip="Please Check Status"></asp:CheckBox>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button runat="server" Text="Save" ID="btnSave" ToolTip="Please Enter To Submit" TabIndex="5" ValidationGroup="User" OnClick="BtnSaveClick" />
                            <input id="btnReset" type="button" value="Clear" title="Please Clear" tabindex="5" onclick="ClearFields()" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ClearFields() {

            $("#<%=txtUserName.ClientID %>").val('');
            $("#<%=txtDashBoardUrl.ClientID %>").val('');
        }
        
    </script>
</asp:Content>
