<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStudentCounsellingPersonelInfo.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcStudentCounsellingPersonelInfo" %>
    <%@ Register Src="~/UserControl/UcDOB.ascx" TagName="DOB" TagPrefix="AJ"%>
<fieldset>
<legend>Your personal details</legend>
     <ul>
        <li>
            <label>
               <%=Resources.label.Name %></label>
            <asp:TextBox ID="txtCandidateName" runat="server"  ToolTip="Enter your name"></asp:TextBox>
            <sup>Candidate Name, Max 99 chars</sup>
        
            <asp:RequiredFieldValidator ID="rfvCandidateName" runat="server" CssClass="error" ControlToValidate="txtCandidateName"
                Display="Dynamic">
                           Field Name cannot be blank
              </asp:RequiredFieldValidator>
          
        </li>
        <li>
            <label>
               <%=Resources.label.FName%></label>
            <asp:TextBox ID="txtUserFatherName" runat="server"  ToolTip="Enter your Father name"></asp:TextBox>
            <sup>Candidate Father Name, Max 99 chars</sup>
        
            <asp:RequiredFieldValidator ID="rfvUserFatherName" runat="server" CssClass="error" ControlToValidate="txtUserFatherName"
                Display="Dynamic">
                           Field Father Name cannot be blank
              </asp:RequiredFieldValidator>
          
        </li>
        <li>
            <label>
               <%=Resources.label.Email %></label>
            <asp:TextBox ID="txtEmailId" runat="server" ToolTip="Email Id"></asp:TextBox>
             
             <sup>Candidate Email, Max 99 chars</sup>
            <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" CssClass="error" ControlToValidate="txtEmailId"
                Display="Dynamic">
                               Field Email cannot be blank
                 </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmailId" runat="server" CssClass="error" ControlToValidate="txtEmailId"
                Display="Dynamic">
                           Incorrect Email format, please try again

            </asp:RegularExpressionValidator>
            
        </li>
        <li>
            <label>
                 <%=Resources.label.Mobile %></label>
            <asp:TextBox ID="txtContactNo" runat="server" ToolTip="Mobile No" MaxLength="11"></asp:TextBox>
           
            <a href="javascript:;"  onclick="ShowControl()">add more</a>
            <sup>Candidate Mobile, Max 10 chars, Numeric without 0 or 91-</sup>
            
            <asp:RequiredFieldValidator ID="rfvContactNo" runat="server" CssClass="error" ControlToValidate="txtContactNo"
                Display="Dynamic">
                                Field Mobile Number cannot be blank

            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactNo" CssClass="error" runat="server" ControlToValidate="txtContactNo">
                                Provide 10 digit mobile number
                </asp:RegularExpressionValidator>
           
        </li>
        <li id="alternameNo" class="hide">
            <label>
                Alternate <%=Resources.label.Mobile %></label>
            <asp:TextBox ID="txtAlternateNo" runat="server" ToolTip="Alternate No" MaxLength="11"></asp:TextBox>
            <a href="javascript:;" onclick="HideControl()">hide </a>
           
            <asp:RegularExpressionValidator ID="revAlterNameMobileNo" CssClass="error" runat="server"
                ControlToValidate="txtAlternateNo">
                                        Provide 10 digit mobile number
            </asp:RegularExpressionValidator>
         
           
        </li>
         <li >
         <label>
            <%=Resources.label.Sex%></label>
           <asp:DropDownList ID="ddlGender" runat="server">
           <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Female" Value="F" ></asp:ListItem>
            </asp:DropDownList>
            <sup>Select your gender</sup>
          </li>

        <li >
         <label>
            <%=Resources.label.DOB%></label>
            <AJ:DOB ID="UcDob" runat="server" /> 
         

        </li>
       
    </ul>
</fieldset>
 <script type="text/javascript" defer="defer">
     function ShowControl() {
      
         if ($('#alternameNo').is(':visible')) {
             
         } else {
             $("#alternameNo").slideToggle();

             $("#alternameNo").removeClass("hide");
             
         }

     }
     function HideControl() {
         if ($('#alternameNo').is(':visible')) {
             $("#alternameNo").slideToggle();
        
         }

     }


     

</script>

