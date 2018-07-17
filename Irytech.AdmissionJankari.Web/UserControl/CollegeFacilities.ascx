<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeFacilities.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeFacilities" %>
<div id="divCollegeFacility">

<fieldset>
    <legend>Facality</legend>
    <ul>
        <li>
            <label>
                Facality</label>
            <asp:TextBox runat="server" ID="txtCollegeFacality" TabIndex="1" ToolTip="Please Enter College Facality">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeFacality" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="txtCollegeFacality">
            </asp:RequiredFieldValidator>                   
        </li>
        <li>
            <label>
               Status</label>
            <asp:CheckBox runat="server" ID="chkCollegeHighLightsStatus" TabIndex="2" ToolTip="Please Check Status">
            </asp:CheckBox>          
        </li>  <li>
          <label>
                Description</label>
               
            <asp:TextBox runat="server" ID="txtCollegeFacalityDescription" TabIndex="3" ToolTip="Please Enter College Facality Description" TextMode="MultiLine">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFacalityDesc" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="txtCollegeFacalityDescription">
            </asp:RequiredFieldValidator>                   
        </li>
       
      </ul>
</fieldset></div>