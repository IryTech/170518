<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="UserComment.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.User.UserComment" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset>
                <legend>Search Comment: </legend>
                <ul>
                    <li>
                        <label>
                            Comment Type:</label>
                        <asp:RadioButtonList ID="rbtSearchQuery" CssClass="RadioButtonList" AutoPostBack="true"
                            RepeatDirection="Horizontal" runat="server" OnSelectedIndexChanged="rbtSearchQuery_SelectedIndexChanged">
                            <asp:ListItem Text="College Comment" Value="1" Selected="True">
                            </asp:ListItem>
                            <asp:ListItem Text="Exam  Comment" Value="2"></asp:ListItem>
                            <asp:ListItem Text="News Comment" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Notice Comment" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Loan Comment" Value="5"></asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                </ul>
                <div id="divImage" class="loading" style="display: none">
                    <label>
                        Please wait...</label>
                    <img src="/image.axd?Common=LoadingImage.gif" />
                </div>
            </fieldset>
            <asp:Repeater ID="rptComment" runat="server">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                             <th>
                                Comment For
                            </th>
                            <th>
                                User Name
                            </th>
                            <th>
                                CommentType
                            </th>
                            <th>
                                Query
                            </th>
                            <th>
                                Is Publish
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class=''>
                        <td>
                            <%# Eval("CommentFor")%>
                        </td>
                        <td>
                            <%# Eval("AjUserFullName")%>
                        </td>
                        <td>
                            <%# Eval("AjUserCommentType")%>
                        </td>
                        <td>
                            <%# Eval("Comment")%>
                        </td>
                        <td>
                            <a id="lnkUpdate" href="#" onclick="OpenCommentPoup('<%# Eval("CreatedBy")%>','<%# Eval("AJCommentId")%>','<%# Eval("AjCommentStatus")%>','<%# Eval("AjUserFullName")%>','<%# Eval("AjUserEmail")%>','<%# Eval("CommentFor")%>');return false;">
                                <%#GetModerateCommentClass(Eval("AJCommentId"))%></a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
            </div>
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hdnCommentId" runat="server" />
            <asp:HiddenField ID="hdnUserId" runat="server" />
            <div id="divComment" class="popup_block">
                <fieldset>
                    <legend>Comment Status</legend>
                    <ul>
                        <li>
                            <asp:RadioButtonList ID="rbtCommentStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Publish" Value="true"></asp:ListItem>
                                <asp:ListItem Text="Restric" Value="false"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="rbtCommentStatus"
                                ValidationGroup="vldgCoomentStatus" ErrorMessage="Please slect option">
                            
                            </asp:RequiredFieldValidator>
                        </li>
                        <li></li>
                        <li>
                            <label>
                            </label>
                            <asp:Button runat="server" ID="BtnUpdate" Text="Update" ValidationGroup="vldgCoomentStatus"
                                OnClick="BtnUpdateStatus" />
                            <input type="button" class="close" value="Cancel" />
                        </li>
                    </ul>
                </fieldset>
            </div>
               <asp:HiddenField ID="hdnCommentFor" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnEmail" runat="server" ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <style>
        .disable
        {
            color: gray;
            text-decoration: none;
        }
    </style>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible

            $("#divImage").show();
        }
        // Called when async postback ends
        function prm_EndRequest(sender, args) {

            $("#divImage").hide();
        }

        function OpenCommentPoup(userId, commentId, status, userName,userEmail,commentFor) {

            $("#<%=hdnCommentId.ClientID %>").val(commentId);
            $("#<%=hdnUserId.ClientID %>").val(userId);
            $("#hdnCommentFor").val(commentFor);
            $("#hdnUserName").val(userName);
            $("#hdnEmail").val(userEmail);

            OpenPoup('divComment', '450', 'lnkUpdate');

        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                close();
            }
        }

        function close() {

            $("#fade").hide();
        }
    </script>
</asp:Content>
