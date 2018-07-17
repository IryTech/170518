<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcHighSchoolInfo.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.UcHighSchoolInfo" %>
<fieldset>
    <legend>Higher Secondary (10th or equivalent) </legend>
    <ul>
        <li>
            <label>
                School Name:</label>
            <asp:TextBox ID="txt10SchoolName" runat="server" ToolTip="10 School Name"></asp:TextBox><sup>i.e Ramjas school, Max 99 chars</sup>
             
            <asp:RequiredFieldValidator ID="rfv10SchoolName" runat="server" ControlToValidate="txt10SchoolName"
                Display="Dynamic" CssClass="lblValidation error">
                Field School Name cannot be blank
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Board:</label>
            <asp:DropDownList ID="ddl10Board" runat="server" Width="260px" title="Select Board">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv10Board" runat="server" InitialValue="Select" Width="98%"
                ControlToValidate="ddl10Board" Display="Dynamic" CssClass="lblValidation error">
                  Select Board
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
               Year of Passing:</label>
            <asp:TextBox ID="txt10YerPass" runat="server"  MaxLength="4"></asp:TextBox><sup>i.e 2010, Numeric"</sup>
           
            <asp:RequiredFieldValidator ID="rfv10YerPass" runat="server" ControlToValidate="txt10YerPass"
                Display="Dynamic" CssClass="lblValidation error">
                Field  year of passing cannot be blank
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev10YerPass" runat="server"
                CssClass="lblValidation error" ControlToValidate="txt10YerPass" > Incorrect  Field selection, please try again
            </asp:RegularExpressionValidator>
        </li>

        <li>
        <label>
      Aggregate % or CGPA:
        </label>
          <asp:TextBox ID="txtTenthCGPA" runat="server"  MaxLength="4"></asp:TextBox><sup>i.e if you know CGPA/DGPA then 9.5 or if you know percentage 98.3, Numeric</sup>
          
            
            <asp:RequiredFieldValidator ID="rfvTenthper" runat="server" ControlToValidate="txtTenthCGPA"
                Display="Dynamic" CssClass="lblValidation error">Field  CGPA/DGPA or percentage cannot be blank
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revtenthPer" runat="server"
                CssClass="lblValidation error" ControlToValidate="txtTenthCGPA" >Incorrect CGPA/DGPA or percentage selection , please try again
            </asp:RegularExpressionValidator>
        </li>
    </ul>
</fieldset>
