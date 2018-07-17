<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testimonial.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Testimonial" %>

<%@ Register Src="~/UserControl/FckEditorCostomize.ascx" TagName="Testimonial" TagPrefix="Aj" %>
<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
<asp:UpdatePanel ID="updateDonation" runat="server">
    <ContentTemplate>    
        <div class="boxPlane mainBG">
            <h2>
                Testimonial
            </h2>
            <hr class="hrline" />
            <fieldset class="boxBody" >
                <h3 style="padding-left:5px;">Your Details</h3>
                <hr class="hrline" />
                <ul >
                    <li class="fleft">
                        <span style="text-transform: none !important; display:block; line-height:15px;" id="spnErrMsg" visible="false" runat="server" class="success">
                            
                        </span>
                    </li>
                    <li >
                        <label>
                            <%=Resources.label.Name %></label> 
                        <asp:TextBox ID="txtUserName" runat="server" Style="width:350px!important;height: 30px!important" ToolTip="Enter your name" placeholder="Enter your name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" CssClass="error" ControlToValidate="txtUserName" Display="Dynamic" ValidationGroup="Exam">
                          Field Name cannot be blank
                        </asp:RequiredFieldValidator>
                    </li>
                    <li >
                         <label>
                            <%=Resources.label.Email %></label>
                        <asp:TextBox ID="txtUserEmailId" runat="server" Style="width:350px!important;height: 30px!important" placeholder="Enter your Email Id" ToolTip="Enter your Email Id"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserEmailId" runat="server" CssClass="error" ControlToValidate="txtUserEmailId" Display="Dynamic" ValidationGroup="Exam">
                         Field Email cannot be blank
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUserEmailId" runat="server" CssClass="error" ControlToValidate="txtUserEmailId" Display="Dynamic" ValidationGroup="Exam">
                           Incorrect Email format, please try again
                        </asp:RegularExpressionValidator>
                    </li>
                    <li >
                         <label>
                            <%=Resources.label.Mobile%></label>
                        <asp:TextBox ID="txtUserMobile" runat="server" Style="width:350px!important;height: 30px!important" placeholder="Enter your 10 digit mobile" ToolTip="Enter your mobile"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserMobile" runat="server" CssClass="error" ControlToValidate="txtUserMobile" Display="Dynamic" ValidationGroup="Exam">
                           Field Mobile Number cannot be blank
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revMobile" runat="server" CssClass="error" Display="Dynamic" ControlToValidate="txtUserMobile" ValidationGroup="Exam">
                              Provide 10 digit mobile number
                        </asp:RegularExpressionValidator>
                    </li>
                    <li >
                         <label>
                            <%=Resources.label.Testimonial%></label>  <br/>                      
                    <asp:Label ID="RfvFck" runat="server" CssClass="error" style="margin-left:300px" Visible="false"></asp:Label>
                    <Aj:Testimonial ID="txtTesimonial" runat="server" />
                    
                    </li>

                    <li ><label></label>
                    <asp:Button ID="btnSave" runat="server" CssClass="button " Text="Save Testimonial" CausesValidation="true" validationgroup="Exam" OnClick="BtnSaveClick" />
                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="false" OnClick="BtnClearClick" validationgroup="Exam" />
                    
                </li>
                </ul>
</fieldset>
<div class="displayBlock clearBoth" ></div>
</div></ContentTemplate></asp:UpdatePanel>
</asp:content>
