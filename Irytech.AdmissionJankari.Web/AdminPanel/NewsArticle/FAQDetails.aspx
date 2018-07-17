<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="FAQDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.FAQDetails" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:HiddenField runat="server" ID="hdnCollegeGroupMaster"></asp:HiddenField>
    <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
    </asp:Label>
    <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
    </asp:Label>
    <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
    </asp:Label>
    <ul class="addPage_utility">
        <li class="fright" style="width: 104px !important;">
            <div class="navbar-inner">
                <a class="insertIco" href="AddFAQDetails.aspx">Add FAQ </a>
                <div class="clear">
                </div>
            </div>
        </li>
    </ul>
    <fieldset>
        <legend>Search FAQs</legend>
        <ul>
            <li>
                <label>
                    FAQ</label>
                <asp:TextBox ID="txtFAQName" CssClass="autocomplete" Width="63%" placeholder="Please Enter FAQs" runat="server"></asp:TextBox></li>
            <li>
                <label>
                    FAQ Category</label>
                <asp:DropDownList ID="ddlFAQCategory" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DdlFaqCategorySelectedIndexChanged">
                </asp:DropDownList>
            </li>
            <li>
                <label>
                </label>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="BtnSearchClick" /></li>
        </ul>
    </fieldset>
    <asp:Label runat="server" Text="" ID="lblEditStatus" Visible="False"></asp:Label>
    <asp:Repeater ID="rptFAQDeatils" runat="server">
        <HeaderTemplate>
            <table class="grdView">
                <tr>
                    <th>
                        S.No
                    </th>
                    <th>
                        FAQ Question
                    </th>
                    <th>
                        FAQ Answer
                    </th>
                    <th>
                        Status
                    </th>
                    <th>
                    Action
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("SrNo") %>
                </td>
                <td>
                    <%# Eval("FAQDetailsQuestion")%>
                </td>
                <td>
                    <%# Eval("FAQDetailsAnswer")%>
                </td>
                <td>
                    <%# Eval("FAQDetailsStatus")%>
                </td>
                <td>
                    <a href='AddFAQDetails.aspx?FAQDetailsId=<%# Eval("FAQDetailsId")%>' title="Edit Faq">Edit</a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </asp:Repeater>
    <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
    </fieldset>
    <link href="../../Styles/autoCompliteCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script src="../../Scripts/CommonScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        var noticeUrl = "../../WebServices/CommonWebServices.asmx/GetFaqDetails";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtFAQName.ClientID %>"), noticeUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtFAQName.ClientID %>"), noticeUrl);
            }
        }

         

    </script>
</asp:Content>
