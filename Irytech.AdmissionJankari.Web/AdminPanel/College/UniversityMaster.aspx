<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="UniversityMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.UniversityMaster" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UpdUniversityMaster" runat="server">
        <ContentTemplate>
            <div id="fade">
            </div>
            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" alt="Loading" title="Loading" />
            </div>
            <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
            </asp:Label>
            <ul class="addPage_utility">
                <li class="fright" style="width: 142px !important;">
                    <div class="navbar-inner">
                        <a href="AddUniversityMaster.aspx" class="insertIco">Add University </a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>
            <fieldset>
                <legend>University Master</legend>
                <ul class="options-bar">
                    <li class="list75width ">
                        <label>
                            University Name
                        </label>
                        <asp:TextBox ID="txtUniversityListByName" runat="server" CssClass="autocomplete" Width="63%" ToolTip="PLease Enter University Name"></asp:TextBox>
                    </li>
                    <li>
                        <label>
                            University Category</label>
                        <asp:DropDownList runat="server" ID="ddlUniversityCategory" ToolTip="Please Select University Category" AutoPostBack="true" OnSelectedIndexChanged="ddlUniversityCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </li>
                    <li>
                        <label>
                        </label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="BtnSearchClick" ToolTip="Please Search" /></li>
                </ul>
             
            <asp:Label runat="server" Text="Edit" ID="lblEditStatus" Visible="False"></asp:Label> 




            <asp:Repeater ID="rptUniversityMaster" runat="server">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                University Name
                            </th>
                            <th>
                                University Url
                            </th>
                            <th>
                                University Title
                            </th>
                            <th>
                                University Category
                            </th>
                            <th>
                                Phone No
                            </th>
                            <th>
                                Image
                            </th>
                            <th>
                                Action
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("SrNo")%>
                        </td>
                        <td>
                            <%# Eval("UniversityName")%>
                        </td>
                        <td>
                            <%# Eval("UniversityUrl")%>
                        </td>
                        <td>
                            <%# Eval("UniversityTitle")%>
                        </td>
                        <td>
                            <%# Eval("UniversityCategoryName")%>
                        </td>
                        <td>
                            <%# Eval("UniversityMobile")%>
                        </td>
                        <td>
                            <img id="Exam" title='<%# Eval("UniversityName")%>' alt='<%# Eval("UniversityName")%>' height="50px;" width="50px;" src='<%# String.Format("{0}{1}","/image.axd?College=",Eval("UniversityLogo")==null ?"NoImage.jpg":Eval("UniversityLogo")) %>' />
                        </td>
                        <td>
                            <a href="AddUniversityMaster.aspx?UniversityId=<%# Eval("UniversityId")%>" title="Edit University">Edit</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../JS/jquery-1.5.2.min.js" type="text/javascript"></script>
    <link href="../../Styles/autoCompliteCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Autocomplete.js" type="text/javascript"></script>
    <script src="../JS/commonscripts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var universityUrl = "../../WebServices/CommonWebServices.asmx/GetUniversityMaster";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtUniversityListByName.ClientID %>"), universityUrl);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtUniversityListByName.ClientID %>"), universityUrl);
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
