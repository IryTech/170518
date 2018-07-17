<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeHostel.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeHostel" %>
<script src="../AdminPanel/JS/CollegeBranch.js" type="text/javascript"></script>
<div id="divCollegeHostel">

<fieldset>
    <legend>Hostel</legend>
    <ul>
        <li>
            <label>
                Hostel Category</label>
            <asp:DropDownList runat="server" ID="ddlCollegeHostelCategory" TabIndex="1" ToolTip="Please Select Hostel Category">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeHostelCategory" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="ddlCollegeHostelCategory">
            </asp:RequiredFieldValidator>                   
        </li>
       
         <li>
          <label>
                Location</label>
                
            <asp:TextBox runat="server" ID="txtCollegeHostelLocation" TabIndex="2" ToolTip="Please Enter College Location">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvHostelLocation" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="txtCollegeHostelLocation">
            </asp:RequiredFieldValidator>                   
        </li>
        <li>
          <label>
                Internet</label>
                
            <asp:RadioButtonList runat="server" TabIndex="3" ID="rbtHostelInternet" CssClass="rbtInternet"
                ToolTip="Please Select Internet" RepeatDirection="Horizontal" Width="250px" >
                <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                <asp:ListItem Value="1">NO</asp:ListItem>
            </asp:RadioButtonList>
                          
        </li>
        <li>
          <label>
                Laundry</label>
                
            <asp:RadioButtonList runat="server" ID="rbtHostelLoundary" TabIndex="4" CssClass="rbtLoundary"
                ToolTip="Please Select Loundary" RepeatDirection="Horizontal" Width="250px"> <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                <asp:ListItem Value="1">NO</asp:ListItem>
            </asp:RadioButtonList>
                          
        </li>
         <li>
          <label>
                AC</label>
                
            <asp:RadioButtonList runat="server" ID="rbtHostelAc" TabIndex="5" CssClass="rbtAC"
                 ToolTip="Please Select Hostel Ac" RepeatDirection="Horizontal" Width="250px" > <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                <asp:ListItem Value="1">NO</asp:ListItem>
            </asp:RadioButtonList>
                          
        </li>
         <li>
          <label>
                Power Backup</label>
                
            <asp:TextBox runat="server" ID="txtHostelPower" TabIndex="6" ToolTip="Please Enter PowerBackup">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvPowerBackUp" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="txtHostelPower">
            </asp:RequiredFieldValidator>                   
        </li>
        <li>
          <label>
              Charge</label>
                
            <asp:TextBox runat="server" ID="txtHostelCharge" TabIndex="7" ToolTip="Please Enter Charge">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvHostelCharge" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="txtHostelCharge">
            </asp:RequiredFieldValidator>                   
        </li>
         <li>
            <label>
               Status</label>
            <asp:CheckBox runat="server" ID="chkHostelStatus" TabIndex="8" ToolTip="Please Check Status">
            </asp:CheckBox>          
        </li>
       
      </ul>
</fieldset></div>
<script type="text/javascript">
    var hostelUrl = "../../WebServices/CommonWebServices.asmx/GetCollegeHostel";
    BindDropDown($("#<%=ddlCollegeHostelCategory.ClientID %>"), hostelUrl);
 </script>
