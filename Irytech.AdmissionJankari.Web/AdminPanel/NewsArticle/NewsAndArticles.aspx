<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="NewsAndArticles.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.NewsAndArticles" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="ctnNewsAndArticles" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel runat="server" ID="updPnlNews">
        <ContentTemplate>
            <ul class="addPage_utility">
                <li class="fright" style="width: 162px !important;">
                    <div class="navbar-inner" style="margin-right: 1%;">
                        <a href="AddNewsAndArticles.aspx" class="insertIco">Add News Master</a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
            </asp:Label>
            <fieldset>
                <legend>Search News</legend>
                <ul>
                    <li class="list75width">
                        <label>
                            Subject
                        </label>
                        <asp:TextBox runat="server" ID="txtNewsName" CssClass="autocomplete" Width="63%" placeholder="Please Enter News Subject" TabIndex="1" ToolTip="Please Enter Notice">
                        </asp:TextBox>
                        <asp:Button runat="server" Text="Search" CssClass="searchbtn" ID="btnSearch" ToolTip="Please Submit Search" OnClick="BtnSearchClick" />
                    </li>
                </ul>
            </fieldset>
            
                <asp:Label runat="server" Text="" ID="lblEditStatus" Visible="False"></asp:Label>
                <asp:Repeater ID="rptNewsAndArticles" runat="server">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>
                                <th>
                                    S.No
                                </th>
                                <th>
                                    Subject
                                </th>
                                <th>
                                    Url
                                </th>
                                <th>
                                    Status
                                </th>
                                <th>
                                    Image
                                </th>
                                <th>
                                    Title
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
                                <%# Eval("NewsSubject")%>
                            </td>
                            <td>
                                <%# Eval("NewsUrl") %>
                            </td>
                            <td>
                                <%# Eval("NewsStatus") %>
                            </td>
                            <td>
                                <img src='<%# String.Format("{0}{1}","/image.axd?News=",string.IsNullOrEmpty(Eval("NewsImage").ToString()) ?"NoImage.jpg":Eval("NewsImage")) %>' width="50px" height="50" alt='<%# Eval("NewsSubject")%>' title='<%# Eval("NewsSubject")%>' />
                            </td>
                            <td>
                                <%# Eval("NewsTitle") %>
                            </td>
                            <td>
                                <a href="AddNewsAndArticles.aspx?NewsId=<%# Eval("NewsId")%>" title="Edit">Edit</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                </asp:Repeater>
                <AJ:CustomPaging ID="ucCustomPaging" runat="server" />

            <div id="fade">
            </div>
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" title="Loading" alt="Loading" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <link href="../../Styles/autoCompliteCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script src="../JS/commonscripts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var newsUrl = "../../WebServices/CommonWebServices.asmx/GetNewsDetails";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtNewsName.ClientID %>"), newsUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtNewsName.ClientID %>"), newsUrl);
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            $("#fade").show();
            $("#divImage").show();
        }
        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            $("#fade").hide();
            $("#divImage").hide();
        }

    </script>
</asp:Content>
