<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportAbuse.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.ReportAbuse" %>
<div class="login">
<asp:UpdatePanel runat="server">
    <ContentTemplate>

         <h2>Report as abuse</h2>
    <h6>All reports are strictly confidential. What best describes this?</h6>
    <br/>
    <ol>
        <li>
            <asp:DropDownList runat="server" ID="ddlReportAbuseList" TabIndex="1" ToolTip="Select abuse type">
            </asp:DropDownList>
        </li>
          <li>
            <asp:TextBox runat="server" ID="txtReportAbuseContent" TabIndex="2" ToolTip="Enter abuse content to report" TextMode="MultiLine">
            </asp:TextBox>
        </li>
          <li>
            <asp:Button runat="server" ID="btnAbuseReport" TabIndex="3" CssClass="button"
                  Text="REPORT AS ABUSE" ToolTip="Click to finish abuse report" 
                  OnClientClick="return CheckAbuseFileds()" onclick="btnAbuseReport_Click">
            </asp:Button>
       
        </li>
    </ol>

    </ContentTemplate>
   </asp:UpdatePanel></div>
   <div id="fade"></div>
<script type="text/javascript">
    
    function CheckAbuseFileds() {
        if ($("#<%=ddlReportAbuseList.ClientID %>").val() <= 0) {
            alert("Select Abuse type");
            return false;
        } else if ($("#<%=txtReportAbuseContent.ClientID %>").val() == "" || $("#<%=txtReportAbuseContent.ClientID %>").val().length == 0) {
            alert("Enter Abuse content");
            return false;
        } else {
            return true;
        }
    }
    function Message(result, message) {
        $("#divReportMessage").html("");
        $("#divReportAbuse").hide();

        $("#divReportMessage").append(message);
        $("#divReportMessage").show();
        $("#divReportMessage").fadeOut(10000);
    }
</script>