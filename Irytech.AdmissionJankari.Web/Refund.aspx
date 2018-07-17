<%@ Page Title="Refund" Language="C#" AutoEventWireup="true" CodeBehind="Refund.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.Refund" %>


<asp:content id="BodyContent" runat="server" contentplaceholderid="cphBody">
    <div class="three_fourth fleft last">
       
        
        <div class="box1 bgblue">
            <h3 class="streamCompareH3">Ask For Refund </h3>

            <hr class="hrline" />
	        <asp:UpdatePanel ID="panel" runat="server">
                <ContentTemplate>

                     <span>
                        <asp:Label runat="server" id="lblMsg" title="Message" ></asp:Label>
                           
                    </span> 
                    <div class="box">
                        <ol class="marginleft style">
                            <li>
                                <label>Name</label>
                                <asp:TextBox runat="server" id="txtRefundName" placeholder="Enter your name" title="Enter your name" width="256px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter your Name" ControlToValidate="txtRefundName" Display="Dynamic" SetFocusOnError="True" Font-Names="verdana" Font-Size="X-Small" ForeColor="Red" ValidationGroup="Refund"></asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>Email ID</label>
                                <asp:TextBox runat="server" id="txtRefundEmailId" placeholder="Enter your Email ID" title="Enter your Email ID" width="256px" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter your Email ID" ValidationGroup="Refund" ControlToValidate="txtRefundEmailId" Display="Dynamic" SetFocusOnError="True" Font-Names="verdana" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator id="validateEmail" runat="server" ValidationGroup="Refund" ErrorMessage="Please enter a valid Email ID" ControlToValidate="txtRefundEmailId" Display="Dynamic" SetFocusOnError="True" Font-Names="verdana" Font-Size="X-Small" ForeColor="Red"></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <label>Form Number</label>
                                <asp:TextBox runat="server" id="txtRefundForm" placeholder="Enter your form number" title="Enter your form number" width="256px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="Refund" ErrorMessage="Please enter your form number" ControlToValidate="txtRefundForm" Display="Dynamic" SetFocusOnError="True" Font-Names="verdana" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>
                       
                            </li>
                            <li>
                                <label></label>
                                <asp:Button runat="server" id="btnSubmit" Text="Ask For Refund" CssClass="button" OnClick="btnSubmit_Click" ValidationGroup="Refund"></asp:Button>
                                <asp:Button runat="server" id="btnCancel" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"></asp:Button>
                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>

                                            <img src="/image.axd?Common=LoadingImage.gif"  />
                                       
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </li>
                           
                               
                           

                        </ol>
                    </div>   
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>