<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollegeRegisteration.aspx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.Account.CollegeRegisteration"  %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="boxPlane bgblue" style="width: 75%; margin: 0 auto; box-shadow: 0px 4px 8px 2px #1f4b7a;">
                <h3>
                    College Registration</h3>
                <hr class="hrline" />
                <fieldset class="ajloginclgimg">
                <div class="adminjan"><img src="/image.axd?Common=Ajregister.jpg" /></div>
                    <div id="lblInfo" runat="server" visible="false">
                    </div>
                    <div class="cform">
                     <p class="p1" style="font-family: 'Paprika', cursive;">Registration Form</p>
                     <hr class="hrline1"/>
                    <ol class="style">
                    <p>
                                College Basic Information</p><hr class="hrline"/>
                        <li class="fleft">
                         
                        <strong>College Name:</strong>
                            <asp:TextBox ID="txtCollegeName" onkeyup="CheckCollegeRegisteration(this);return false;"
                                 runat="server" TabIndex="1"
                                CssClass="masterTooltip" ToolTip="Enter college name" onmouseleave="CheckCollegeRegisteration(this);return false;" placeholder="Enter college name"></asp:TextBox>
                            <a href="#" id="sndCheck" style="float: left; margin-top: 3px; font-size: 11px; margin-left: 162px;"
                                class="rightImglink" onclick="CheckCollegeRegisteration(this);return false;"
                                tabindex="2" title="Check college registeration">Check College</a> <span class="errormsgSpan1">
                                    <asp:RequiredFieldValidator runat="server" ID="rfvCollegeName" ValidationGroup="collegeregister"
                                        Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtCollegeName"> 
                                    </asp:RequiredFieldValidator></span> </li>
                        <li class="fleft"><strong style="margin-right:5px;">Choose Course:</strong>
                           <asp:DropDownList ID="ddlCollegeCourse" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <span class="errormsgSpan1">
                                <asp:CustomValidator ID="cusValidation" SetFocusOnError="True" Display="Dynamic" ValidationGroup="collegeregister"
                                    CssClass="error" runat="server" ErrorMessage="Please select atleast one course" ClientValidationFunction="ValidateCourse"></asp:CustomValidator>
                                </span> </li>
                        <li class="fleft"><strong>College Mobile:</strong>
                            <asp:TextBox ID="txtCollegePhone" runat="server" TabIndex="3" CssClass="masterTooltip"
                                ToolTip="Enter your 10 digit mobile number" placeholder="Enter your 10 digit mobile number"></asp:TextBox>
                            <span class="errormsgSpan1">
                                <asp:RequiredFieldValidator runat="server" ID="rfvConatctMobile" ValidationGroup="collegeregister"
                                    Display="Dynamic" CssClass="error" ControlToValidate="txtMobile">Field Mobile cannot be blank
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revContactMobile" Display="Dynamic"
                                    ValidationGroup="collegeregister" SetFocusOnError="True" CssClass="error" ControlToValidate="txtCollegePhone">  

                                </asp:RegularExpressionValidator></span> </li>
                        <li class="fleft"><strong>College State:</strong>
                            <asp:DropDownList runat="server" ID="ddlState" TabIndex="4" ClientIDMode="Static" CssClass="masterTooltip"
                                ToolTip="Select state">
                            </asp:DropDownList>
                            <span class="errormsgSpan1">
                                <asp:RequiredFieldValidator runat="server" ID="rfvState" ValidationGroup="collegeregister"
                                    Display="Dynamic" InitialValue="0" SetFocusOnError="True" CssClass="error" ControlToValidate="ddlState"> 
                                </asp:RequiredFieldValidator></span> </li>
                        <li class="fleft"><strong>College City:</strong><%--<hr class="hrline" />--%>
                            <asp:DropDownList runat="server" ID="ddlCity" TabIndex="5" ClientIDMode="Static" CssClass="masterTooltip"
                               ToolTip="Select city">
                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="errormsgSpan1">
                                <asp:RequiredFieldValidator runat="server" ID="rfvCity" ValidationGroup="collegeregister"
                                    Display="Dynamic" InitialValue="0" SetFocusOnError="True" CssClass="error" ControlToValidate="ddlCity"> 
                                </asp:RequiredFieldValidator></span> </li>
                        <p>
                                Contact Information</p><hr class="hrline"/>
                        <li class="fleft">
                        <strong>Name of Contact Person:</strong><%--<hr class="hrline" />--%>
                            <asp:TextBox ID="txtCollegeContactPersonName" runat="server" CssClass="masterTooltip"
                                TabIndex="6" ToolTip="Enter college contact person name" placeholder="Enter college contact person name"></asp:TextBox>
                            <span class="errormsgSpan1">
                                <asp:RequiredFieldValidator runat="server" ID="rfvCollegePersonName" ValidationGroup="collegeregister"
                                    Display="Dynamic" SetFocusOnError="True" CssClass="error" ControlToValidate="txtCollegeContactPersonName"> 
            
                                </asp:RequiredFieldValidator></span> </li>
                        <li class="fleft"><strong>Contact Person Designation</strong>
                            <asp:TextBox ID="txtContactDesignation" runat="server" CssClass="masterTooltip" TabIndex="7"
                                ToolTip="Enter contact person designation" placeholder="Enter contact person designation"></asp:TextBox>
                            <span class="errormsgSpan1">
                                <asp:RequiredFieldValidator runat="server" SetFocusOnError="True" ID="rfvDesignation" ValidationGroup="collegeregister"
                                    Display="Dynamic" CssClass="error" ControlToValidate="txtMobile">Field Designation cannot be blank
                                </asp:RequiredFieldValidator></span> </li>
                        <li class="fleft"><strong>Contact Person
                            <%=Resources.label.Mobile%></strong><%--<hr class="hrline" />--%>
                            <asp:TextBox ID="txtMobile" runat="server" TabIndex="8" CssClass="masterTooltip"
                                ToolTip="Enter your 10 digit mobile number" placeholder="Enter your 10 digit mobile number"></asp:TextBox>
                            <span class="errormsgSpan1">
                                <asp:RequiredFieldValidator runat="server" SetFocusOnError="True" ID="rfvMobile" ValidationGroup="collegeregister"
                                    Display="Dynamic" CssClass="error" ControlToValidate="txtMobile">Field Mobile cannot be blank
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server"  SetFocusOnError="True" ID="revMobile" Display="Dynamic" ValidationGroup="collegeregister"
                                    CssClass="error" ControlToValidate="txtMobile">  

                                </asp:RegularExpressionValidator></span> </li>
                        <li class="fleft"><strong>Contact Person
                            <%=Resources.label.Email%></strong><%--<hr class="hrline" />--%>
                            <asp:TextBox ID="txtEmailId" runat="server" TabIndex="11" CssClass="masterTooltip"
                                ToolTip="Enter your email id" placeholder="Enter your email id"></asp:TextBox>
                            <span class="errormsgSpan1">
                                <asp:RequiredFieldValidator runat="server" ID="rfvEmailId" SetFocusOnError="True" CssClass="error" ValidationGroup="collegeregister"
                                    Display="Dynamic" ControlToValidate="txtEmailId"> 
          
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revEmailId" Display="Dynamic"
                                    ValidationGroup="collegeregister" SetFocusOnError="True" CssClass="error" ControlToValidate="txtEmailId"> 

                                </asp:RegularExpressionValidator></span>
                         <span class="errormsgSpan1">
                            <asp:Label ID="spnEmailError" runat="server" Visible="false"></asp:Label>
                            </span>
                        </li>
                        <li class="fleft">
                            <asp:CheckBox runat="server" ID="chkTerms" TabIndex="12" ClientIDMode="Static" CssClass="CheckBoxList"></asp:CheckBox>I
                            agree <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Terms-and-Conditions").ToLower()%>'
                                target="_blank">T&amp;C</a> and <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+("Privacy-Policy").ToLower()%>'
                                    target="_blank">Privacy Policy</a><br />
                            <span class="errormsgSpan1">
                                <asp:CustomValidator ID="cvEventsValidator" Display="Dynamic" ValidationGroup="collegeregister"
                                    CssClass="error" runat="server" SetFocusOnError="True" ErrorMessage="Please accept terms and conditions to continue" ClientValidationFunction="ValidateCheckBoxList"></asp:CustomValidator>
                            </span></li>
                        <li class="fleft lftbor" style="text-align:center;">
                            <asp:Button runat="server" Text="Click to finish Registration process" ID="btnRegister"
                                Style="padding: 6px 35px;" TabIndex="13" ValidationGroup="collegeregister" class="button"
                                ToolTip="Please Submit To Register"  OnClick="BtnRegisterClick" />
                        </li>
                       
                    </ol></div>
                </fieldset>
            </div>
            <div id="divImage" style="display: none">
                <label style="color: red; font-size: 16px">
                    Processing...</label>
                <img src="/image.axd?Common=LoadingImage.gif" alt='Processing' />
            </div>
            <div class="loading fademessage" id="divReportMessage">
            </div>
            <asp:HiddenField runat="server" ID="hdnCollegeSuccess" />
            <asp:HiddenField ID="hndCollegeCourse" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hndCityId" runat="server" ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <link href="../Styles/jquery.multiselect.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/UI.li.css" />
    <script src="../Js/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Js/jquery.multiselect.js.js" type="text/javascript"></script>
   
    <script type="text/javascript">

        $(document).ready(function () {

            var url = "/WebServices/CommonWebServices.asmx/GetCollegeDetails";
            BindDropDownCommon($("#<%=txtCollegeName.ClientID %>"), url);
            if ($("#<%=txtCollegeName.ClientID %>").val == '') {
                $("#<%=txtCollegeName.ClientID %>").focus();
            }
            $("#ddlState").change(function () {
                var stateId = this.value;
                BindCityByState($("#ddlCity"), stateId);
            });
            $("#ddlCity").change(function () {
                
                $("#hndCityId").val(this.value);
            });
            $("#ddlCollegeCourse").multiselect({
                selectedList: 4 // 0-based index
            });

        });
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                var url = "/WebServices/CommonWebServices.asmx/GetCollegeDetails";
                BindDropDownCommon($("#<%=txtCollegeName.ClientID %>"), url);
                if ($("#<%=txtCollegeName.ClientID %>").val == '') {
                    $("#<%=txtCollegeName.ClientID %>").focus();
                }
                $("#ddlState").change(function () {
                    var stateId = this.value;
                    BindCityByState($("#ddlCity"), stateId);
                });

                $("#ddlCollegeCourse").multiselect({
                    selectedList: 4 // 0-based index
                });
                $("#ddlCity").change(function () {

                    $("#hndCityId").val(this.value);
                });
            }
        }
       
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible

            $("#divImage").show();
        }
        // Called when async postback ends
        function prm_EndRequest(sender, args) {

            $("#divImage").hide();
        }


        function CollegeFailedMessage(result, message) {
            if (result > 0) {
                $("#divReportMessage").html("");
                $("#divReportMessage").css('background-color', '#fff18b');
                $("#divReportMessage").append(message);
                $("#divReportMessage").show();
                $("#divReportMessage").fadeOut(10000);
            } else {
                $("#divReportMessage").html("");
                $("#divReportMessage").css(message);
                $("#divReportMessage").append(message);
                $("#divReportMessage").show();
                $("#divReportMessage").fadeOut(10000);
            }
        }

        function CheckCollegeRegisteration(control) {

             if ($("#<%=txtCollegeName.ClientID %>").val().length > 1) {
                $(".addCollege").remove();
                $(".login").remove();
                $(control).after('');
                $(control).after("<span class='progress'><img src='/image.axd?Common=LoadingImage.gif' alt='Loading'/></span>");
                $.ajax({
                    type: "POST",
                    url: "/WebServices/CommonWebServices.asmx/CheckCollegeRegisteration",
                    data: '{"collegeName":"' + $("#<%=txtCollegeName.ClientID %>").val() + '"}',
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $(".progress").remove();
                        var status = response.d;
                        var imgTick;
                        if (status == "0") {
                            imgTick = "<img  src='/image.axd?Common=redRight.png' alt='Right Image' />";
                          
                            $(control).after("<span class='login errormsgSpan1 error'>" + imgTick + " You can continue registeration</span>");
                        }
                        else if (status == "1") {
                            imgTick = "<img  src='/image.axd?Common=cross1.png' alt='Right Image' width='15px' />";
                        
                            $(control).after("<span class='addCollege errormsgSpan1 error'>" + imgTick + " The college is registered with us. Please <a href='/account/college-login'> login. </a> or <a href='/contact-us'> contact us.</a></span>");
                        }
                    },
                    error: function (xml, textStatus, errorThrown) {
                        $(".progress").remove();
                        alert(xml.status + "||" + xml.responseText);
                    }
                });
            } else {
                event.preventDefault();
                $("#<%=txtCollegeName.ClientID %>").focus();
                return false;
            }
        }

    </script>
    <script language="javascript" type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
           args.IsValid = false;
            if (document.getElementById("chkTerms").checked)
                 args.IsValid = true;
        }
        function ValidateCourse(sender, args) {
            $("#hndCollegeCourse").val($("#ddlCollegeCourse").multiselect("getChecked").map(function () {
                return this.value;
            }).get());
            args.IsValid = false;
            if ($("#hndCollegeCourse").val()!='') {
                 $("#hndCollegeCourse").val($("#ddlCollegeCourse").multiselect("getChecked").map(function () {
                     return this.value;
                }).get());
                args.IsValid = true;
            }
        }
        function HideMessageStatus() {
            $("[id$='lblInfo']").fadeOut('slow', function () { });
        }
        
    </script>
  
    <style>
    
    </style>
</asp:Content>
