<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCollegeBranchRankSource.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ucCollegeBranchRankSource" %>
<script src="../AdminPanel/JS/CollegeBranch.js" type="text/javascript"></script>
<div id="divCollegeBranchRankSource">
<fieldset>
    <legend>Rank Source</legend>
    <ul>
        <li>
            <label>
                Rank Source</label>
            <asp:DropDownList runat="server" ID="ddlCollegeRankSource" TabIndex="1" ToolTip="Please Select College Rank Source" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeRankSource" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="ddlCollegeRankSource">
            </asp:RequiredFieldValidator>                   
        </li>
        <li>
            <label>
               Source Year</label>
            <asp:TextBox ID="txtCollegeRankSourceYear" runat="server" TabIndex="3" ToolTip="Please Enter Source Year"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvSourceYear" ValidationGroup="College" Display="Dynamic" InitialValue="0" SetFocusOnError="True" ControlToValidate="txtCollegeRankSourceYear">
            </asp:RequiredFieldValidator>                   
        </li>
        <li>
            <label>
          Rank Overall</label>
            <asp:TextBox ID="txtRankOverall" runat="server" TabIndex="3" ToolTip="Please Enter Rank OverAll"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvRanlOverAll" ValidationGroup="College" Display="Dynamic" ControlToValidate="txtRankOverall" SetFocusOnError="True">
            </asp:RequiredFieldValidator>                         
        </li>

                <li>
            <label>Status</label>
                    <asp:CheckBox runat="server" ID="chkCollegeBranchRankSourceStatus" TabIndex="7" ToolTip="Please Check Status">
                    </asp:CheckBox>         
        </li>
          </ul>
</fieldset></div>
<script type="text/javascript">
    var rankSourceUrl = "../../WebServices/CommonWebServices.asmx/GetRankSourcelist";
    BindDropDown($("#<%=ddlCollegeRankSource.ClientID %>"), rankSourceUrl);
  
</script>