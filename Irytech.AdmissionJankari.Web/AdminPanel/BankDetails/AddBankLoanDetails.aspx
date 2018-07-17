<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddBankLoanDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.BankDetails.AddBankLoanDetails" %>

<%@ Register Src="../../UserControl/FckEditorCostomize.ascx" TagName="FckEditorCostomize" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <ul class="addPage_utility">
        <li class="fright" style="width: 126px !important;">
            <div class="navbar-inner">
                <a href="../BankDetails/ManageBankDetails.aspx" class="viewIco">Bank Master </a>
                <div class="clear">
                </div>
            </div>
        </li>
    </ul>
    <div class="grdOuterDiv">
       
        <fieldset id="bankBasicDetails">
            <legend>
                <asp:Label ID="lblInsertUpdate" runat="server" Text="Add Bank Master"></asp:Label></legend>
            <ul>
                <li class="width48perc fleft">
                    <label>
                        Bank Name</label>
                    <asp:TextBox ID="txtBankName" runat="server" TabIndex="1" ToolTip="Please Enter Bank Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBankName" SetFocusOnError="true" runat="Server" ControlToValidate="txtBankName"  CssClass="forerror" ValidationGroup="Bank" Display="Dynamic" ErrorMessage="PLease Enter Bank Name" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Contact Person
                    </label>
                    <asp:TextBox ID="txtContactPerson" runat="server" TabIndex="6" ToolTip="Please Enter Person Name"></asp:TextBox>
                </li>
                <li class="width48perc fleft">
                    <label>
                        Bank Short Name</label>
                    <asp:TextBox ID="txtBankShortName" runat="server" TabIndex="2" ToolTip="Please Enter Bank  Short Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvShortName" runat="Server" CssClass="forerror" SetFocusOnError="true" ControlToValidate="txtBankShortName" ValidationGroup="Bank" Display="Dynamic" ErrorMessage="PLease Enter Short Name" />
                </li>
                <li class="width48perc fleft">
                    <label>
                        Contact Person Email Id</label>
                    <asp:TextBox ID="txtContactEmailId" runat="server" TabIndex="7" ToolTip="Please Enter EmailId"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvEmailId" ValidationGroup="Bank" CssClass="forerror" Display="Dynamic" ControlToValidate="txtContactEmailId">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revEmailId" Display="Dynamic" CssClass="forerror" ValidationGroup="Bank" SetFocusOnError="True" ControlToValidate="txtContactEmailId">
                    </asp:RegularExpressionValidator>
                </li>
               
                 <li class="width48perc fleft">
                    <label>
                        Bank Url</label>
                    <asp:TextBox ID="txtBankUrl" runat="server" TabIndex="3" ToolTip="Please Enter Bank Url"></asp:TextBox>
                </li>
                <li class="width48perc fleft">
                    <label>
                        Contact Person Mobile</label>
                    <asp:TextBox ID="txtContactMobile" runat="server" TabIndex="8" ToolTip="Please Enter Mobile Number"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvMobile" ValidationGroup="Bank" CssClass="forerror" Display="Dynamic" ControlToValidate="txtContactMobile">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="revMobile" Display="Dynamic" CssClass="forerror" ValidationGroup="Bank" SetFocusOnError="True" ControlToValidate="txtContactMobile">
                    </asp:RegularExpressionValidator>
                </li>
                 <li class="width48perc fleft">
                    <label>
                        Bank Phone</label>
                    <asp:TextBox ID="txtBankPhone" runat="server" TabIndex="4" ToolTip="Please Enter Bank Phone Number"></asp:TextBox>
                </li>
                <li class="width48perc fleft">
                    <label>
                        Image</label><asp:HiddenField runat="server" ID="hdnFileName" />
                    <asp:FileUpload runat="server" ID="flpImgUpload" ToolTip="Please Upload Images" TabIndex="9"></asp:FileUpload>
                    <asp:RequiredFieldValidator ID="rfvImageUpload" runat="Server" CssClass="forerror" ControlToValidate="flpImgUpload" ValidationGroup="CollegeUpload" />
                    <asp:Button runat="server" ID="imgUpload" ToolTip="Please Upload Images" Text="Upload" TabIndex="10"></asp:Button>
                    <asp:Image runat="server" ID="imgBank" Width="100px" Height="100px" Visible="False"></asp:Image>
                    </li>
                <li class="width48perc fleft">
                    <label>
                        Bank Address</label>
                    <asp:TextBox ID="txtBankAddress" runat="server" TabIndex="5" ToolTip="Please Enter Bank Address" TextMode="Multiline"></asp:TextBox>
                </li>
                
                 
                 
                 <li class="width48perc fleft">
                    <label>
                        Designation</label>
                    <asp:TextBox ID="txtContactDesignation" runat="server" TabIndex="11" ToolTip="Please Enter Designation"></asp:TextBox>
                </li>
                
                    <li>
                        <label>
                            Bank Description</label><span class="fleft" style="margin:3px 5px;">
                        <AJ:FckEditorCostomize ID="fckBankDesc" runat="server" TabIndex="12" /></span>
                    </li>
            </ul>
        </fieldset>
        
        <fieldset id="Loan" runat="server">
            <legend><label runat="server" id="loanCriteria" style="font-size:16px !important;" visible="false">
            </label>
                <asp:Label ID="lblInsertUpdateCriteria" runat="server" Text=""></asp:Label></legend>
            <ul class="bankmanage">
                <li>
                    <label>
                        Eligibilty</label>
                    <asp:TextBox ID="txtEligibilty" runat="server" TabIndex="11" ToolTip="Please Enter Eligibilty"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Category</label>
                    <asp:DropDownList ID="ddlCategory1" runat="server" TabIndex="13" Width="120px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" SetFocusOnError="true" ID="rfvCategory" CssClass="forerror" InitialValue="0" ValidationGroup="Bank" Display="Dynamic" ControlToValidate="ddlCategory1" ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlCategory2" runat="server" TabIndex="14" Width="120px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCategory3" runat="server" TabIndex="15" Width="120px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlCategory4" runat="server" TabIndex="16" Width="120px">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Loan Range</label>
                    <asp:DropDownList ID="ddlLoanRange1" runat="server" TabIndex="17" Width="120px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvLoan" SetFocusOnError="true" InitialValue="0" CssClass="forerror" ValidationGroup="Bank" Display="Dynamic" ControlToValidate="ddlLoanRange1" ErrorMessage="*">
                    </asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlLoanRange2" runat="server" TabIndex="18" Width="120px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlLoanRange3" runat="server" TabIndex="19" Width="120px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlLoanRange4" runat="server" Width="120px" TabIndex="20">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Rate of Interest</label>
                    <asp:TextBox ID="txtRateOfInterest" runat="server" TabIndex="21" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRateOfInterest1" runat="server" TabIndex="22" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRateOfInterest2" runat="server" TabIndex="23" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRateOfInterest3" runat="server" TabIndex="24" Width="110px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Repayment Duration</label>
                    <asp:TextBox ID="txtRepaymentDuration" runat="server" TabIndex="25" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRepaymentDuration1" runat="server" TabIndex="26" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRepaymentDuration2" runat="server" TabIndex="27" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRepaymentDuration3" runat="server" TabIndex="28" Width="110px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Security</label>
                    <asp:TextBox ID="txtSecurity" runat="server"  TabIndex="29"  Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtSecurity1" runat="server" TabIndex="30" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtSecurity2" runat="server" TabIndex="31" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtSecurity3" runat="server" TabIndex="32" Width="110px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Margin</label>
                    <asp:TextBox ID="txtMargin" runat="server"  TabIndex="33" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtMargin1" runat="server" TabIndex="34" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtMargin2" runat="server" TabIndex="35" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtMargin3" runat="server" TabIndex="36" Width="110px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Processing Fee</label>
                    <asp:TextBox ID="txtProcessingFee" runat="server"  TabIndex="37" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtProcessingFee1" runat="server" TabIndex="38" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtProcessingFee2" runat="server" TabIndex="39" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtProcessingFee3" runat="server" TabIndex="40" Width="110px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Processing Time</label>
                    <asp:TextBox ID="txtProcessingTime" runat="server"  TabIndex="41" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtProcessingTime1" runat="server" TabIndex="42" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtProcessingTime2" runat="server" TabIndex="43" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtProcessingTime3" runat="server" TabIndex="44" Width="110px"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Remark</label>
                    <asp:TextBox ID="txtRemark" runat="server"  TabIndex="45" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRemark1" runat="server" TabIndex="46" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRemark2" runat="server" TabIndex="47" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtRemark3" runat="server" TabIndex="48" Width="110px"></asp:TextBox>
                </li>
            </ul>
        </fieldset>
        <fieldset style="background:none !important;">
            <label>
            </label>
            <asp:Button ID="btnAdd"  ValidationGroup="Bank" runat="server" Text="Save" CausesValidation="true"   OnClick="btnAdd_Click" />
        </fieldset>
        
            <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
            </asp:Label>
            <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
            </asp:Label>
            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
            </asp:Label> 
        <fieldset id="fldLoan" runat="server" visible="false">
            <legend>UpdateLoan</legend>
            <asp:Repeater ID="rptLoanDetails" runat="server" OnItemCommand="rptLoanDetails_ItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                Eligibilty
                            </th>
                            <th>
                                RepaymentDuration
                            </th>
                            <th>
                                ProcessingFees
                            </th>
                            <th>
                                RateOfInterest
                            </th>
                            <th>
                                ProcessingTime
                            </th>
                            <th>
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Container.ItemIndex+1 %>
                        </td>
                        <td>
                            <%# Eval("Eligibilty")%>
                        </td>
                        <td>
                            <%# Eval("RepaymentDuration")%>
                        </td>
                        <td>
                            <%# Eval("ProcessingFees")%>
                        </td>
                        <td>
                            <%# Eval("RateOfInterest")%>
                        </td>
                        <td>
                            <%# Eval("ProcessingTime")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("LoanId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </fieldset>
    </div>



    <div class="popup_block" style="display: none; width: 550px" id="loanPOpUp">
        <fieldset id="fldLoanUpdate" runat="server">
            <legend>UpdateLoan</legend>
            <ul>
                <li>
                    <label>
                        Eligibilty</label>
                    <asp:TextBox ID="txtEligibiltyUp" runat="server" TabIndex="1" ToolTip="Please Enter Eligibilty"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Category</label>
                    <asp:DropDownList ID="ddlLoanCategory" runat="server" Width="159px" TabIndex="2">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Loan Range</label>
                    <asp:DropDownList ID="ddlLoanRange" runat="server" Width="159px" TabIndex="3">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Rate of Interest</label>
                    <asp:TextBox ID="txtRoi" runat="server" TabIndex="4"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Repayment Duration</label>
                    <asp:TextBox ID="txtRepayentUP" runat="server" TabIndex="5"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Security</label>
                    <asp:TextBox ID="txtSecurityUp" runat="server" TabIndex="6"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Margin</label>
                    <asp:TextBox ID="txtMarginUp" runat="server" TabIndex="7"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Processing Fee</label>
                    <asp:TextBox ID="txtFeesUp" runat="server" TabIndex="8"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Processing Time</label>
                    <asp:TextBox ID="txtPTimeUp" runat="server" TabIndex="9"></asp:TextBox>
                </li>
                <li>
                    <label>
                        Remark</label>
                    <asp:TextBox ID="txtRemarkUp" runat="server" TabIndex="10"></asp:TextBox>
                </li>
                <li>
                    <label>
                        <asp:Button ID="btnLoanUpdate" BackColor="#35628c" TabIndex="11" OnClick="btnLoanUpdate_Click" runat="server" Text="Update" CausesValidation="true" ForeColor="White" Style="left: 450px; position: absolute; height: 25px; width: 57px" />
                    </label>
                </li>
            </ul>
            <asp:HiddenField ID="hdnLoanId" runat="server" />
        </fieldset>
    </div>
    <script type="text/javascript">
        var url = location.href.indexOf("?");
        if (url > 0) {
            var myVal = document.getElementById("<%=rfvCategory.ClientID %>");
            var myVal1 = document.getElementById("<%=rfvLoan.ClientID %>");
            window.ValidatorEnable(myVal1, false); window.ValidatorEnable(myVal, false);
        }

        function openLoanPop(divid) {
            var popMargTop = ($('#' + divid).height() + 80) / 2;
            var popMargLeft = ($('#' + divid).width() + 80) / 2;
            $('#' + divid).css({
                'margin-top': -popMargTop,
                'margin-left': -popMargLeft
            });
            $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
            $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
            $("#" + divid).show();
        }
    </script>
</asp:Content>
