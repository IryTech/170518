<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ChangePassword" %>
<asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success"></asp:Label>
              <asp:Label ID="lblInfo" runat="server" Text="" Visible="false" CssClass="info"></asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error"></asp:Label>
 <fieldset>
            <legend>Change Password</legend>
            <ul>
      <li>
                    <label>
               c</label>
       <asp:TextBox ID="txtEmail" runat="server" TabIndex="1" ToolTip="Please Enter Email"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" ValidationGroup="register" Display="Dynamic" SetFocusOnError="True" >
          </asp:RequiredFieldValidator>       
          <asp:RegularExpressionValidator runat="server" ID="revEmail" Display="Dynamic" ValidationGroup="register" ControlToValidate="txtEmail" SetFocusOnError="True">
                     </asp:RegularExpressionValidator>                
                </li>
            <li>
                    <label>
                       <%= Resources.label.OldPassword%></label>
                    <asp:TextBox ID="txtOldPassword" runat="server" TabIndex="2" ToolTip="Please Enter  Old Password" TextMode="Password"></asp:TextBox>
                          <asp:RequiredFieldValidator runat="server" ID="rfvOldPassword" ControlToValidate="txtOldPassword" ValidationGroup="register" Display="Dynamic" SetFocusOnError="True">
          </asp:RequiredFieldValidator>                       
                </li>
                 <li>
                    <label>
                      <%= Resources.label.NewPassword%></label>
                      <asp:TextBox ID="txtNewPassword" runat="server" TabIndex="3" ToolTip="Please Enter Mobile Number" TextMode="Password"></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rfvNewPassword" ValidationGroup="register" Display="Dynamic" ControlToValidate="txtNewPassword" SetFocusOnError="True">
          </asp:RequiredFieldValidator>
                      
                </li>
                <li>
                    <label>
                       <%= Resources.label.CnfPassword%></label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TabIndex="4" ToolTip="Please Enter EmailId" TextMode="Password"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server" ID="rfvConfirmPassword" ValidationGroup="register" Display="Dynamic" ControlToValidate="txtConfirmPassword" SetFocusOnError="True">
          </asp:RequiredFieldValidator>  
                    <asp:CompareValidator ID="confrmPassord" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword">
                    </asp:CompareValidator>
                </li>
                 
             <li>
                    <asp:Button runat="server" Text="Change Password" ID="btnChangePassword" TabIndex="5" 
                        ToolTip="Please Submit To Register" onclick="BtnChangePasswordClick"  />     
                     </li>
                </ul>
                </fieldset>