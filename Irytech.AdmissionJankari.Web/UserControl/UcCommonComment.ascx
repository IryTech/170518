<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCommonComment.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcCommonComment" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<div class="box1" runat="server" id="divUserComment" visible="False">
    <h3>
        <asp:Label runat="server" ID="lblCount"></asp:Label>&nbsp;Comments</h3>
    <hr class="hrline" />
    <asp:Repeater ID="rptComment" runat="server">
        <ItemTemplate>
            <div class="boxPlane">
                <ul class="vertical">
                    <li class="width15Percent">
                        <img id="collegeImage" title='<%# Eval("AjUserFullName")%>' alt='<%# Eval("AjUserFullName")%>'
                            height="70px;" width="70px;" src='<%# String.Format("{0}{1}","/image.axd?User=",string.IsNullOrEmpty(Convert.ToString(Eval("AjUserImage"))) ?"NoImage.jpg":Eval("AjUserImage")) %>' /></li>
                    <li class="width75Percent">
                        <ul class="vertical marginleft">
                            <li class="width95Percent"><strong class="fright" style="color: Gray; font-size: 11px;
                                text-decoration: underline; padding-bottom: 8px;">Posted On:
                                <%# Eval("CreatedOn") %></strong></li>
                            <li class="width95Percent">
                                <p style="font-size: 12px;">
                                    <%# Eval("Comment") %></p>
                            </li>
                            <li class="width95Percent " style="padding-top: 5px;"><strong class="fright">
                                <%# Eval("AjUserFullName")%>
                            </strong></li>
                        </ul>
                    </li>
                </ul>
                <span class="dispBlock clearBoth"></span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <AJ:CustomPaging ID="ucCustomCommentPaging" runat="server" />
    <span class="dispBlock clearBoth"></span>
</div>
<div id="divComment" class="box1">
    <asp:HiddenField runat="server" ID="hdnCourseComment" Value="0"></asp:HiddenField>
    <h3>
        Comments
        <div runat="server" ID="lblCommenttResult"  Visible="False"></div></h3>
    <hr class="hrline" />
    <div class="box">
        <label style="width: 60px; padding-top: 10px; float: left;" for="<%=txtComment.ClientID %>">
            Comment</label>
        <asp:TextBox ID="txtComment" runat="server" TabIndex="1" ToolTip="Enter your comment"
            Rows="4" Width="70%" TextMode="MultiLine" placeholder="Enter your comment"></asp:TextBox><span
                class="charLeft" id="countCharacter">450 characters left</span> <span class="marginleftper">
                    <asp:Button runat="server" Text="Post Comment" ValidationGroup="comment1" TabIndex="2"
                        CssClass="button" OnClientClick="return checkLogin()" ID="btnComment" OnClick="BtnCommentClick" />
                </span>
    </div>
    <div id="fb-root">
    </div>
    <fb:comments id="lnkFaceBook" num_posts="2" width="500"></fb:comments>
</div>
<div id="divCommentRegister" class="popup_block">
    <fieldset class="boxBody ">
        <legend>Register</legend>
        <ul style="margin-left: 0px;">
            <li>
                <label for="<%=txtUserNameComment.ClientID %>">
                    <%=Resources.label.Name%></label>
                <asp:TextBox ID="txtUserNameComment" runat="server" TabIndex="3" ToolTip="Enter your name"
                    placeholder="Enter your name"></asp:TextBox>
                <span class="errormsgSpan2">
                    <asp:RequiredFieldValidator runat="server" ID="rfvUserCommentName" ValidationGroup="comment"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtUserNameComment"> 
                  Field Name cannot be blank

                    </asp:RequiredFieldValidator></span> </li>
            <li>
                <label for="<%=txtMobileComment.ClientID %>">
                    <%=Resources.label.Mobile%></label>
                <asp:TextBox ID="txtMobileComment" runat="server" TabIndex="4" ToolTip="Enter your 10 digit mobile number"
                    placeholder="Enter your 10 digit mobile number"></asp:TextBox>
                <span class="errormsgSpan2">
                    <asp:RequiredFieldValidator runat="server" ID="rfvMobileComment" ValidationGroup="comment"
                        Display="Dynamic" CssClass="error" ControlToValidate="txtMobileComment">
                Field Mobile Number cannot be blank

                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationExpression="[1-9][0-9]{9}$" runat="server"
                        ID="revMobileComment" Display="Dynamic" ValidationGroup="comment" SetFocusOnError="True"
                        CssClass="error" ControlToValidate="txtMobileComment">  
                Provide 10 digit mobile number

                    </asp:RegularExpressionValidator>
                </span></li>
            <li>
                <label for="<%=txtEmailIdComment.ClientID %>">
                    <%=Resources.label.Email%></label>
                <asp:TextBox ID="txtEmailIdComment" runat="server" TabIndex="5" ToolTip="Enter your email id"
                    placeholder="Enter your email id"></asp:TextBox>
                <span class="errormsgSpan2">
                    <asp:RequiredFieldValidator runat="server" ID="rfvEmailIdComment" CssClass="error"
                        ValidationGroup="comment" SetFocusOnError="True" Display="Dynamic" ControlToValidate="txtEmailIdComment"> 
                Field Email cannot be blank

                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationExpression="^([a-zA-Z0-9_\-\.']+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                        runat="server" ID="revEmailId" Display="Dynamic" ValidationGroup="comment" SetFocusOnError="True"
                        CssClass="error" ControlToValidate="txtEmailIdComment"> 
                 Incorrect Email format, please try again

                    </asp:RegularExpressionValidator>
                </span></li>
            <li id="liCommentCourse" class="hide">
                <label>
                    <%=Resources.label.Course%></label>
                <asp:DropDownList runat="server" ID="ddlCommentCourse" TabIndex="6" ToolTip="Select course" />
                <span class="errormsgSpan2">
                    <asp:RequiredFieldValidator runat="server" ID="rfvCourseComment" ValidationGroup="comment"
                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="ddlCommentCourse"
                        InitialValue="0"> 
                  Select course
                    </asp:RequiredFieldValidator></span> </li>
            <li>
                <label>
                    &nbsp;</label>
                <asp:Button runat="server" Text="Post Comment" ValidationGroup="comment" CssClass="button"
                    TabIndex="7" ID="btnAddComment" OnClick="BtnAddCommentClick" />
            </li>
        </ul>
    </fieldset>
    <a href="#" id="lnkPopId" style="display: none;"></a>
</div>
<div id="fade">
</div>
<script type="text/javascript">
    $(document).ready(function() {
      
         if (jQuery("#<%=hdnCourseComment.ClientID %>").val() <= 0) {
             jQuery("#liCommentCourse").removeClass("hide");
         } 
         else {
                var myVal = document.getElementById('<%=rfvCourseComment.ClientID %>');
                 window.ValidatorEnable(myVal, false);
         }
    });
    
   
    function CountLeft(field, max) {
        if (field.val().length > max)
            field.val(field.val().substring(0, max));
        else
            jQuery(".charLeft").html((max - field.val().length) + " characters left");
    }

    jQuery("#<%=txtComment.ClientID %>").keyup(
        function (event) {
            CountLeft(jQuery(this), 450); // you can increase or derease the number depend on your need.
        }
    );

    function checkLogin() {
        var status = <%=new SecurePage().IsLoggedInUSer %>; //to check user login..............
        if ($("#<%=txtComment.ClientID %>").val().length != 0) {
            if (status == false) {
                ClearCommentFields();
                OpenPoup('divCommentRegister', '600', 'lnkPopId');
                $("#<%=txtUserNameComment.ClientID %>").focus();
                return false;
            }
        } else {
            $("#<%=txtComment.ClientID %>").focus();

            alert("Please enter comment");
            return false;
        }
    }

    function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                if (jQuery("#<%=hdnCourseComment.ClientID %>").val() <= 0) 
                {
                    jQuery("#liCommentCourse").removeClass("hide");
                }
                close();
                 $("#<%=ddlCommentCourse.ClientID %>").change(function() {
                    jQuery("#<%=hdnCourseComment.ClientID %>").val($("#<%=ddlCommentCourse.ClientID %>").val());
                });
           
            }
        }
     function close() {
         $("#divCommentRegister").hide();
         $("#fade").hide();
     }
     

     $("#<%=ddlCommentCourse.ClientID %>").change(function() {
       jQuery("#<%=hdnCourseComment.ClientID %>").val($("#<%=ddlCommentCourse.ClientID %>").val());
     });
       function ClearCommentFields() {
           $("#<%=txtUserNameComment.ClientID %>").val(''); $("#<%=txtMobileComment.ClientID %>").val(''); $("#<%=txtEmailIdComment.ClientID %>").val('');
       }
</script>
<script type="text/javascript">

    var AppID = '145890325492400'; //My FB App ID
    $(document).ready(function () {
        $("#lnkFaceBook").attr("href", document.URL)
       
    });

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=145890325492400"; //My FB App ID
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk'));


    
    function HideMessageStatus() {
        $("[id$='lblCommenttResult']").fadeOut('slow', function () { });
    }
</script>
<style>.CommentSuccess {
    padding: 15px;
    margin-bottom: 20px;
    border: 1px solid transparent;
    border-radius: 7px;
   	color:#005e00 !important;
	background-color: #dbf7d8 !important;
    border-color: rgb(235, 204, 209);
    margin-top: 15px;
}

.CommentSuccess a {
    color: rgb(174, 19, 16);
}
.CommentError {
    padding: 15px;
    margin-bottom: 20px;
    border: 1px solid transparent;
    border-radius: 7px;
    color:#bf0000 !important;
	background-color: #fef9c5 !important;
	border:1px solid #a89d18;
    margin-top: 15px;
}

.CommentError a {
    color: rgb(174, 19, 16);
}
</style>
