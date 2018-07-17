<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcDownloadApplicationfotm.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcDownloadApplicationfotm" %>
<div class="pdf">
<a href="#" id="lnkDownloadApplication" onclick="OpenPoup('divDownloadApplicationform', '450', 'lnkDownloadApplication');return false;" title="Download Application Form">
<span>Download</span><span>Application Form</span></a>
    </div>
    <%--<asp:UpdatePanel ID="updateDownload" runat="server">
    <ContentTemplate>--%>
<div id='divDownloadApplicationform' class="popup_block box1">
<h3 class="streamCompareH3">Please provide the following details to download application form</h3>
<span>
       <asp:Label ID="lblMsg" runat="server" CssClass="sucess" Visible="false"></asp:Label>
 </span>
<div class="box">

    <ol class="marginleft style">
        <li>
      <strong><%=Resources.label.Course%></strong><asp:DropDownList ID="ddlCourse" runat="server" ToolTip="Select course"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rfvCourse" runat="server" CssClass="error" ControlToValidate="ddlCourse"
                Display="Dynamic" InitialValue="0" ValidationGroup="download">
                            Select course.
            </asp:RequiredFieldValidator>
        </li>
        <li>
          <strong> <%=Resources.label.Name%>*</strong><asp:TextBox ID="txtName" runat="server" title="Enter your name"
			  placeholder="Enter your name"></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvName" runat="server" ValidationGroup="download" CssClass="error" ControlToValidate="txtName"
                Display="Dynamic">
                            Field Name cannot be blank
		  </asp:RequiredFieldValidator>
        </li>
        
        <li>
           <strong> <%=Resources.label.Email%>*</strong>  
			
			<asp:TextBox ID="txtEmailId" runat="server"  placeholder="Enter your email id" title="Enter your email id"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ValidationGroup="download" CssClass="error" ControlToValidate="txtEmailId"
                Display="Dynamic">
                               Field Email cannot be blank
			</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmailId" runat="server" ValidationGroup="download" CssClass="error" ControlToValidate="txtEmailId"
                Display="Dynamic">
                            Incorrect Email format, please try again
			</asp:RegularExpressionValidator>
         </li>
         <li >
            <strong> <%=Resources.label.Mobile%>*</strong><asp:TextBox ID="txtMobile" runat="server" title="Enter your 10 digit mobile number"
				placeholder="Enter your 10 digit mobile number"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rfvContactNo" runat="server" CssClass="error" ValidationGroup="download" ControlToValidate="txtMobile"
                Display="Dynamic">
                                 Field Mobile Number cannot be blank
			 </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactNo" CssClass="error" runat="server" ValidationGroup="download" ControlToValidate="txtMobile">
                                 Provide 10 digit mobile number
			</asp:RegularExpressionValidator>
        </li>
       </ol>
            <footer>
            <asp:Button id="btnDownload" runat="server" CssClass="button" 
                    Text="Download Application" ValidationGroup="download" 
                    onclick="btnDownload_Click" title="Click to download application form"/>
            <asp:Button id="btnClear" Text="Cancel"  runat="server" CssClass="button" 
                    onclick="btnClear_Click"  />
           
            
           
          </footer>
  
    </div>
    </div>
   <%-- </ContentTemplate>
  </asp:UpdatePanel>--%>