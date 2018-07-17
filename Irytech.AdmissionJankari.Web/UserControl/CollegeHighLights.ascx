<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CollegeHighLights.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.CollegeHighLights" %>
<div id="divCollegeBranchHighLights">

<fieldset>
    <legend>HighLights</legend>
    <ul>
        <li>
            <label>
                HighLights</label>
            <asp:TextBox runat="server" ID="txtCollegeHighLights" TabIndex="1" ToolTip="Please Enter College HighLights">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvCollgeHighLights" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="txtCollegeHighLights">
            </asp:RequiredFieldValidator>                   
        </li>
        <li>
            <label>
               Status</label>
            <asp:CheckBox runat="server" ID="chkCollegeHighLightStatus" TabIndex="2" ToolTip="Please Check Status">
            </asp:CheckBox>          
        </li>
       
      </ul>
</fieldset></div>