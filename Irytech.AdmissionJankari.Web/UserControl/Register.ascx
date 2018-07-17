<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.Register" %>
<asp:HiddenField runat="server" ID="hdnUserCategory"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnUserId"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnCollege"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnPassword"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdnCourseId"></asp:HiddenField>

<div class="boxPlane bgblue" style="width:75%; margin:0 auto; box-shadow:0px 4px 8px 2px #1f4b7a;">
<h3>Register</h3>
<hr class="hrline" />
      <div id="lblInfo" runat="server"  Visible="false" style="margin-bottom: 10px;"></div>
<fieldset class="forStudentAJregister">

    <ol class="fleft" style="margin-left:55%; min-height:530px; "> 
    <li class="fleft lftbor">
        
   </li>
    <li class="fleft lftbor"><span style="font-weight:bold; color:Red; font-size:12px; margin-left:-5px;">Please fill in the following details to register.</span></li>  
    <li class="fleft">
            <strong class="dspblock">
             <%=Resources.label.RegistationCate%></strong><hr class="hrline" /> 
            <asp:DropDownList runat="server" ID="ddlUserCategory" CssClass="masterTooltip"  TabIndex="1" Width="58%" ToolTip="Select the user category" 
                onchange="checkUserCategory()">
            </asp:DropDownList>
            <span class="errormsgSpan1"> <asp:RequiredFieldValidator runat="server" ID="rfvUserCategory" ValidationGroup="register"
                Display="Dynamic" InitialValue="0" SetFocusOnError="True" CssClass="error" ControlToValidate="ddlUserCategory"> 
                Select user category

            </asp:RequiredFieldValidator> </span></li>
             <li class="fleft">
            <strong class="dspblock">
              <%=Resources.label.Name%></strong><hr class="hrline" />  
            <asp:TextBox ID="txtUserName" runat="server" TabIndex="2" CssClass="masterTooltip" ToolTip="Enter your name" placeholder="Enter your name"></asp:TextBox>
            <span class="errormsgSpan1"> <asp:RequiredFieldValidator runat="server" ID="rfvUserName" ValidationGroup="register"
                Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtUserName"> 
                  Field Name cannot be blank

            </asp:RequiredFieldValidator></span>
        </li>
        <li class="fleft">
           <strong class="dspblock">
               <%=Resources.label.Mobile%></strong> <hr class="hrline" /> 
            <asp:TextBox ID="txtMobile" runat="server" TabIndex="3" ToolTip="Enter your 10 digit mobile number" CssClass="masterTooltip" placeholder="Enter your 10 digit mobile number"></asp:TextBox>
           <span class="errormsgSpan1">  <asp:RequiredFieldValidator runat="server" ID="rfvMobile" ValidationGroup="register"
                Display="Dynamic" CssClass="error" ControlToValidate="txtMobile">
                Field Mobile Number cannot be blank

                </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationExpression="[7-9][0-9]{9}$" runat="server" ID="revMobile" Display="Dynamic" ValidationGroup="register"
                SetFocusOnError="True" CssClass="error" ControlToValidate="txtMobile">  
                Provide 10 digit mobile number

            </asp:RegularExpressionValidator> </span></li> <li class="fleft">
            <strong class="dspblock">
                <%=Resources.label.Email%></strong><hr class="hrline" />  
            <asp:TextBox ID="txtEmailId" runat="server" TabIndex="4" ToolTip="Enter your email id" CssClass="masterTooltip" placeholder="Enter your email id" ></asp:TextBox>
            <span class="errormsgSpan1"> <asp:RequiredFieldValidator runat="server" ID="rfvEmailId" CssClass="error" ValidationGroup="register"
                Display="Dynamic" ControlToValidate="txtEmailId"> 
                Field Email cannot be blank

            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationExpression="^([a-zA-Z0-9_\-\.']+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" ID="revEmailId" Display="Dynamic"
                ValidationGroup="register" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEmailId"> 
                 Incorrect Email format, please try again

            </asp:RegularExpressionValidator></span> </li> <li class="fleft">
            <strong class="dspblock">
              <%=Resources.label.Course%></strong><hr class="hrline" />  
            <asp:DropDownList runat="server" ID="ddlCourse" TabIndex="5" CssClass="masterTooltip"  Width="58%" ToolTip="Select Course">
            </asp:DropDownList>
            <span class="errormsgSpan1"> <asp:RequiredFieldValidator runat="server" ID="rfvCourse" ValidationGroup="register"
                Display="Dynamic" InitialValue="0" SetFocusOnError="True" CssClass="error" ControlToValidate="ddlCourse"> 
               Select the course you are interested in


            </asp:RequiredFieldValidator></span>
         
             </li>
             <li class="fleft lftbor" style="font-size:11px;color:Gray; "><input type="checkbox" checked="checked" />I agree T&amp;C and Privacy Policy</li>
             <li class="fleft lftbor"><asp:Button runat="server"  Text="Click to finish Registration process" ID="btnRegister" TabIndex="7" ValidationGroup="register" style="padding:6px 35px;" class="button"
                ToolTip="Please Submit To Register" OnClick="BtnRegisterClick" /></li>
             </ol>
      
       
</fieldset>  
</div>  

<div id="popCity" class="popup_block">
    <fieldset>
        <ul>
            <li>
                <label>
                    <%=Resources.label.City%></label>
                <asp:DropDownList runat="server" ID="ddlCity" CssClass="masterTooltip" ToolTip="Please Select City">
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ID="rfvCity" Display="Dynamic"
                    InitialValue="0" ControlToValidate="ddlCity" SetFocusOnError="True" ValidationGroup="regsiterCity">
                </asp:RequiredFieldValidator></li>
            <li>
                <label>&nbsp;
                </label>
                <asp:Button runat="server" Text="Register" CssClass="masterTooltip" ValidationGroup="regsiterCity" ID="btnCityRegister"
                    ToolTip="Please Submit to insert City" TabIndex="2" OnClick="BtnCityRegisterClick" />
            </li>
        </ul>
    </fieldset>
</div>
<div id="popCollege" class="popup_block">
    <fieldset>
        <ul>
            <li>
                <label>
                    College</label>
                <input id="txtCollege" title="Please Enter College" class="masterTooltip" type="text" tabindex="1" onchange="setCollege()" />
            </li>
            <li>
                <label>
                </label>
                <asp:Button ID="btnCollegeRegister"  CssClass="masterTooltip" OnClientClick=" return ValidateCollege()" OnClick="BtnCollegeRegisterClick"
                    runat="server" TabIndex="8" Text="Register" title="Please Submit to insert City"
                    ValidationGroup="regsiterCity" />
            </li>
        </ul>
    </fieldset>
</div>

<script type="text/javascript">

    function checkUserCategory() {
        var userCategory = $("#<%=ddlUserCategory.ClientID %> option:selected").text();
          
        if (userCategory == "College") {
            var myVal = document.getElementById('<%=rfvCourse.ClientID %>');
            showCollege();
            window.ValidatorEnable(myVal, false);
            $("#liCourse").hide();
        } else {
            $("#liCourse").show();
        }
    }
        
    
    function setCollege() {
        if ($("#txtCollege").val() != "") {
            $("#<%=hdnCollege.ClientID %>").val();
        }
    }

    function ValidateCollege() {
        if ($("#txtCollege").val() == "") {
            alert("Please Select College");
            return false;
        }
        else {
            return true;
        }
    }
    function HideMessageStatus(lblInfo) {
        $(lblInfo).fadeOut('slow', function () { });
        $(lblInfo).focus();
    }
</script>
<style>
    .CommentSuccess
    {
        padding: 15px;
        margin-bottom: 20px;
        border: 1px solid transparent;
        border-radius: 7px;
        color: #005e00 !important;
        background-color: #dbf7d8 !important;
        border-color: rgb(235, 204, 209);
        margin-top: 15px;
    }
    
    .CommentSuccess a
    {
        color: rgb(174, 19, 16);
    }
    .CommentError
    {
        padding: 15px;
        margin-bottom: 20px;
        border: 1px solid transparent;
        border-radius: 7px;
        color: #bf0000 !important;
        background-color: #fef9c5 !important;
        border: 1px solid #a89d18;
        margin-top: 15px;
    }
    
    .CommentError a
    {
        color: rgb(174, 19, 16);
    }
    
</style>
