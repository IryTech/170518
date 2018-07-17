<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcDiplomaInfo.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcDiplomaInfo" %>
<fieldset>
    <legend>For Lateral entry/Diploma holders: </legend>
    <ul>
        <li>
            <label>
                College Name:</label>
            <asp:TextBox ID="txtDipCollegeName" runat="server"  ToolTip="College Name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDipCollegeName"  CssClass="error" runat="server" ControlToValidate="txtDipCollegeName"
                Display="Dynamic">Field College name cannot  be blank
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Course:</label>
            <asp:TextBox ID="txtDipCourse" runat="server"  ToolTip="Course"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDipCourse" CssClass="error" runat="server" ControlToValidate="txtDipCourse"
                Display="Dynamic">Field Course  cannot  be blank
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Total % of Marks:</label>
           <asp:TextBox ID="txtDipMarks" runat="server"  ToolTip="Marks" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDipMarks" runat="server" CssClass="error" ControlToValidate="txtDipMarks"
                Display="Dynamic" >Field Percentage marks cannot  be blank
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDipMarks" CssClass="error" runat="server" 
                ControlToValidate="txtDipMarks">Incorrect Field selection, please try again

            </asp:RegularExpressionValidator>
          
        </li>
       
        <li>
            <label>
                CGPA/DGPA:
            </label>
            <asp:TextBox ID="txtDipCGPA" runat="server" MaxLength="3"></asp:TextBox>
        </li>
        <li>
            <label>
               Year of Passing :
            </label>
            <asp:TextBox ID="txtDipYOP" runat="server"  ToolTip="Year" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDipYOP" runat="server" ControlToValidate="txtDipYOP"
                      Display="Dynamic" CssClass="lblValidation error">
                  Field Year of passing cannot  be blank
             </asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="revDipYOP" runat="server" CssClass="lblValidation error"
                     ControlToValidate="txtDipYOP">Incorrect Field selection, please try again
               </asp:RegularExpressionValidator>
        </li>
    </ul>
</fieldset>
