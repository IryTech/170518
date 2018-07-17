<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.Login" %>
<%@ Register Src="~/UserControl/ForgetPassword.ascx" TagPrefix="ADMJ" TagName="Forgot" %>

<div class="form-1 border" style="width:80%;">
    <h2 class="streamCompareH3 ">
        Log In
    </h2>
    <hr class="hrline" />
    <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error"></asp:Label>
    <p class="field" style="margin-top:6px;">
        <asp:TextBox ID="txtUserName"  runat="server" CssClass="masterTooltip" placeholder="Enter email id with which you have registered " TabIndex="1" ToolTip="Enter email id with which you have registered with us"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" SetFocusOnError="True" ToolTip="Email is required." Display="Dynamic" ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" ValidationGroup="LoginUserValidationGroup" ID="revEmail" Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtUserName">
        </asp:RegularExpressionValidator>
        <i>
            <img src="/image.axd?Common=loginImg.png" alt="userName" width="15px" height="15px" /></i>
    </p>
    <p class="field">
        <asp:TextBox ID="txtPassword" runat="server" CssClass="masterTooltip" TabIndex="1" TextMode="Password" placeholder="Enter password" ToolTip="Enter password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" SetFocusOnError="True" ToolTip="Field Password cannot be blank" Display="Dynamic" ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
        <i>
            <img src="/image.axd?Common=lockImg.png" alt="Password" width="15px" height="15px" /></i>
    </p>
    <p>
        <a class="rightImglink masterTooltip" title="Forgot Password" href="#" id="sndFrogetPwd" onclick="OpenForgetPopUp();return false;">Forgot Password</a></p>
   
    <asp:Button ID="LoginButton" CssClass="submitbtn masterTooltip" ToolTip="Click here to Login" runat="server"  ValidationGroup="LoginUserValidationGroup" OnClick="LoginButtonClick" />
</div> 
<div class="popup_block"  id="divForgot">
        <ADMJ:Forgot ID="ucForgot" runat="server" />
    </div>

    <div id="fade"></div>
<script type="text/javascript">
    function OpenForgetPopUp() {
        $("#msg").html("");
        $("#lblEmailIdError").addClass("hide");
    OpenPoup('divForgot', 650, 'sndFrogetPwd');
}
</script>
