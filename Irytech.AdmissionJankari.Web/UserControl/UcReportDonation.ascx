<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcReportDonation.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcReportDonation" %>
<asp:HiddenField ID="hndCourseId" runat="server" />
<asp:UpdatePanel ID="updateDonation" runat="server">
    <ContentTemplate>
        <div class="boxPlane mainBG fleft">
            <h2>
                Report Donation
            </h2>
            <hr class="hrline" />
            <fieldset class="fleft">
                <h3 style="padding-left:5px;">Your Details</h3>
                <hr class="hrline" />
                <ol class="style fleft">
                    <li class="fleft"><span style="text-transform: none !important; display:block; line-height:15px;" id="spnErrMsg" visible="false" runat="server" class="success"></span></li>
                    <li class="fleft">
                        <strong>
                            <%=Resources.label.Name %></strong> 
                        <asp:TextBox ID="txtUserName" runat="server" Width="90%" ToolTip="Enter Your name" ValidationGroup="vldgReportdonation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" CssClass="error1" ValidationGroup="vldgReportdonation" ControlToValidate="txtUserName" Display="Dynamic">
                          Field Name cannot be blank
                        </asp:RequiredFieldValidator>
                    </li>
                    <li class="fleft">
                         <strong>
                            <%=Resources.label.Email %></strong>
                        <asp:TextBox ID="txtUserEmailId" runat="server" Width="90%" ToolTip="Enter your Email Id" ValidationGroup="vldgReportdonation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserEmailId" runat="server" CssClass="error1" ValidationGroup="vldgReportdonation" ControlToValidate="txtUserEmailId" Display="Dynamic">
                         Field Email cannot be blank
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUserEmailId" runat="server" ValidationGroup="vldgReportdonation" CssClass="error1" ControlToValidate="txtUserEmailId" Display="Dynamic">
                           Incorrect Email format, please try again
                        </asp:RegularExpressionValidator>
                    </li>
                    <li class="fleft">
                         <strong>
                            <%=Resources.label.Mobile%></strong>
                        <asp:TextBox ID="txtUserMobile" runat="server" Width="90%" ToolTip="Enter your mobile" ValidationGroup="vldgReportdonation"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserMobile" runat="server" CssClass="error1" ValidationGroup="vldgReportdonation" ControlToValidate="txtUserMobile" Display="Dynamic">
                           Field Mobile Number cannot be blank
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revMobile" runat="server" CssClass="error1" ValidationGroup="vldgReportdonation" Display="Dynamic" ControlToValidate="txtUserMobile">
                              Provide 10 digit mobile number
                        </asp:RegularExpressionValidator>
                    </li>
                    <li class="fleft" style="font-size: 12px; color: Gray;">
                        <input type="checkbox" checked="checked" />
                        Don`t disclose my identity </li>
                        </ol>
              
                <h3 style="padding-left:5px;">Details of Accused</h3>
                <hr class="hrline" />
                <ol class="style fleft">
                    <li class="fleft">
                         <strong>
                            Accused Name:</strong>
                        <asp:TextBox ID="txtaccusedName" Width="90%" runat="server" ToolTip="Enter accused Name"></asp:TextBox>
                    </li>
                     <li class="fleft">
                         <strong>
                            Accused Mobile No: </strong>
                        <asp:TextBox ID="txtAccusedMobileNo" runat="server" Width="90%" ToolTip="Enter accused Mobile number if you know"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revAccusedMobileNo" ValidationGroup="vldgReportdonation" Display="Dynamic" runat="server" CssClass="error1" ControlToValidate="txtAccusedMobileNo">
                                Provide 10 digit mobile number
                        </asp:RegularExpressionValidator>
                    </li>
                    <li class="fleft">
                         <strong>
                            Accused Email Id:</strong>
                        <asp:TextBox ID="txtAccusedEmailId" runat="server" Width="90%" ToolTip="Enter accused Email Id if you know"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revAccusedEmaidId" ValidationGroup="vldgReportdonation" runat="server" CssClass="error1" ControlToValidate="txtAccusedEmailId" Display="Dynamic">
                            Incorrect Email format, please try again
                        </asp:RegularExpressionValidator>
                    </li>
                     <li class="fleft">
                         <strong>
                            For Which course:</strong>
                        <asp:DropDownList ID="ddlCourse" runat="server" Width="92%" ToolTip="Choose Course for which you had applied ">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" CssClass="error1" ValidationGroup="vldgReportdonation" ControlToValidate="ddlCourse" InitialValue="0" Display="Dynamic">
                          Select Course your had applied for
                        </asp:RequiredFieldValidator>
                    </li>
                     <li class="fleft">
                         <strong>
                            For Which College applied:</strong>
                        <asp:TextBox ID="txtCollegeName" runat="server" Width="90%" ToolTip="Enter the college Name for which you had applied"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCollegeName" runat="server" ValidationGroup="vldgReportdonation" CssClass="error1" ControlToValidate="txtCollegeName" Display="Dynamic">
                           Enter college name for which your had applied for
                        </asp:RequiredFieldValidator>
                    </li>
                    </ol>
               
                <h3 style="padding-left:5px;">Describe Incident</h3>
                <hr class="hrline" />
                 <ol class="style fleft">
                     <li class="fleft">
                         <strong>
                            Describe Incident</strong><span class="fleft">
                                <asp:TextBox ID="txtIncident" runat="server" CssClass="textarea" TextMode="MultiLine" Rows="4" Columns="50" Width="90%" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvIncident" runat="server" ValidationGroup="vldgReportdonation" CssClass="error1" ControlToValidate="txtIncident" Display="Dynamic">
                           Enter at least 200 characters
                                </asp:RequiredFieldValidator></span> </li>
               
                 <li class="fleft">
                   <asp:CheckBox ID="chkTandC" Checked="true" runat="server" />
                             
                        I agree <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Terms-and-Conditions"%>' target="_blank">T&amp;C</a> and <a href='<%=IryTech.AdmissionJankari.Components.Utils.ApplicationRelativeWebRoot+"Privacy-Policy"%>' target="_blank">Privacy Policy</a> 
                          <asp:CustomValidator ID="cmpTandC" runat="server" CssClass="error1" ValidationGroup="vldgReportdonation" ErrorMessage="Please Select T&C and Privacy Policy"
                             ClientValidationFunction="ClientValidationForCheckBox"></asp:CustomValidator>
                </li>
                <li class="fleft">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Report Donation" ValidationGroup="vldgReportdonation" CausesValidation="true" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="false" OnClick="btnClear_Click" />
                    <label id="lblMsg" title="Message">
                    </label>
                </li>
            </ol>
             
            </fieldset>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<script type="text/javascript" defer="defer">

    var url = "/WebServices/CommonWebServices.asmx/GetCollegeDetails";
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            BindCollege();
            $("#<%=ddlCourse.ClientID %>").change(function () {

                ChangeCourseId($("#<%=ddlCourse.ClientID %>").val());

                $("#<%=hndCourseId.ClientID %>").val($("#<%=ddlCourse.ClientID %>").val())
                BindCollege();
            });
        }
    }
    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {

            BindCollege();
        }
    }
    $(document).ready(function () {
        BindCollege();
        $("#<%=ddlCourse.ClientID %>").change(function () {

            ChangeCourseId($("#<%=ddlCourse.ClientID %>").val());
            $("#<%=hndCourseId.ClientID %>").val($("#<%=ddlCourse.ClientID %>").val())

            BindCollege();
        });
    });

    function BindCollege() {
        $.ajax({
            type: "POST",
            url: "/WebServices/CommonWebServices.asmx/GetCollegeByCourseSearch",
            async: true,
            data: '{courseId:"' + $("#<%=hndCourseId.ClientID %>").val() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {

                data = msg.d.split(",");

                $("#<%=txtCollegeName.ClientID %>").autocomplete(data);


            }
        });

    }

   
    function ClientValidationForCheckBox(sender, args) {
        var CheckBox1 = document.getElementById("<%=chkTandC.ClientID %>")
        if (CheckBox1.checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }
</script>
