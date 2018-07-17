<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcGraduateInfo.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcGraduateInfo" %>
<fieldset>
    <legend>Graduation </legend>
    <ul>
        
          <li>
            <label>
                College Name</label>
            <asp:TextBox ID="txtGrdCollegeName" runat="server" ToolTip="College Name"></asp:TextBox><sup>i.e Hindu College, Max 99 chars</sup>
              
            <asp:RequiredFieldValidator ID="rfvGrdCollegeName" CssClass="error" runat="server" ControlToValidate="txtGrdCollegeName"
                Display="Dynamic">Field College name cannot  be blank
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Specialization</label>
            <asp:TextBox ID="txtGrdSpecialization" runat="server" ToolTip="Specialization"></asp:TextBox>
            <sup>i.e PCM (P-Physics,C-Chemistry,M-Maths),PCB(P-Physics,C-Chemistry,B-Biology) etc, Max 99 chars</sup>
            
            <asp:RequiredFieldValidator ID="rfvGrdSpecialization" CssClass="error" runat="server" ControlToValidate="txtGrdSpecialization"
                Display="Dynamic"> Field Specialization cannot  be blank
            </asp:RequiredFieldValidator>
        </li>
      
        <li>
            <label>
                Year of Passing</label>
            <asp:TextBox ID="txtGraduationYOP" runat="server" MaxLength="4"></asp:TextBox>
            <sup>i.e 2010, Numeric</sup>
            
            <asp:RequiredFieldValidator ID="rfvGraduationYOP" CssClass="error" runat="server" ControlToValidate="txtGraduationYOP"
                Display="Dynamic">Field Year of passing cannot  blank 
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revGraduationYOP" runat="server" CssClass="error" ControlToValidate="txtGraduationYOP">Incorrect Field selection,please try again
            </asp:RegularExpressionValidator>
        </li>
        <li>
            <label>
                Aggregate(%/CGPA/DGPA)</label>
            <asp:TextBox ID="txtGrdPer" runat="server" MaxLength="4"></asp:TextBox>
            <sup>i.e 98.3, Numeric</sup>
            
            
        </li>
        <li></li>
       
    </ul>
</fieldset>
