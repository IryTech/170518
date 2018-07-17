<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcChangePassword.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcChangePassword" %>

<fieldset>
    <legend>Change Password</legend>
    <ul>
        <asp:Label ID="lblMsg" runat="server" Text="" Visible="false" ></asp:Label>
               
        <li>
            <label>
               <%=Resources.label.Email%></label>
            <asp:TextBox ID="txtEmailId" runat="server" TabIndex="1" ToolTip="Please Enter EmailId"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvEmailId" ValidationGroup="ChangePassword"
                Display="Dynamic" ControlToValidate="txtEmailId">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="revEmailId" Display="Dynamic"
              ValidationGroup="ChangePassword" SetFocusOnError="True" ControlToValidate="txtEmailId">
            </asp:RegularExpressionValidator>
        </li>

        <li>
            <label>
                  <%=Resources.label.OldPassword%></label>
            <asp:TextBox ID="txtOldPassword" runat="server" TabIndex="2" TextMode="Password" ToolTip="Please Enter Old Password"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvOldPassword" ValidationGroup="ChangePassword"
                Display="Dynamic" ControlToValidate="txtOldPassword">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="revOldPassword" Display="Dynamic"
               ValidationGroup="ChangePassword" SetFocusOnError="True" ControlToValidate="txtOldPassword">
            </asp:RegularExpressionValidator>
        </li>
         <li>
            <label>
                  <%=Resources.label.NewPassword%></label>
            <asp:TextBox ID="txtNewPassword" runat="server" TabIndex="3" TextMode="Password" ToolTip="Please Enter New Password"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvNewPassword" ValidationGroup="ChangePassword"
                Display="Dynamic" ControlToValidate="txtNewPassword">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="revNewPassword" Display="Dynamic"
                ValidationGroup="ChangePassword" SetFocusOnError="True" ControlToValidate="txtNewPassword">
            </asp:RegularExpressionValidator>
        </li>
        
        <li>
            <label>&nbsp;
            </label>
            <asp:Button runat="server" Text="Change Password" ID="btnChnagePassword" TabIndex="4"
                ToolTip="Please Submit To Change Password" OnClick="BtnChangePasswordClick" />
        </li>
    </ul>
</fieldset>