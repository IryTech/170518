<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ManageExamFormMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Exam.ManageExamFormMaster" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlState" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <ul class="addPage_utility">
        <li class="fright">
                        <div class="navbar-inner">
                            <a class="insertIco" href="AddExamFormMaster.aspx">Add Exam Form</a>
                            <div class="clear">
                            </div>
                        </div>
                    </li>
        </ul>
            <fieldset>
                <legend>Search Exam</legend>
                 
                        <ul>
                            <li>
                                <label>
                                    Subject Name:</label>
                                <asp:TextBox ID="txtSubjectName" CssClass="autocomplete" Width="63%" placeholder="Please Enter Subject Name" runat="server" TabIndex="3" ValidationGroup="Exam"></asp:TextBox>
                                <asp:Label ID="lblSeccessMsg" CssClass="warning" runat="server" Visible="false"></asp:Label>
                            </li>
                            <li>
                                <label>
                                    &nbsp;</label>
                                <asp:DropDownList ID="ddlExamName" runat="server" TabIndex="1" ValidationGroup="Exam" AutoPostBack="true" OnSelectedIndexChanged="DdlExamNameSelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlCourseName" runat="server" TabIndex="2" ValidationGroup="Exam">
                                </asp:DropDownList>
                            </li>
                            <li>
                                <label>
                                </label>
                                <asp:Button ID="btnSubmit" runat="server" Text="Search" CausesValidation="true" ValidationGroup="Exam" TabIndex="4" OnClick="BtnSubmitClick" />
                                <input id="btnReset" type="button" value="Reset" onclick="ClearAllFields();" title="Please Reset" />
                            </li>
                        </ul>
                     
            </fieldset>
            <asp:Repeater ID="rptExamFromMaster" runat="server" OnItemCommand="RptExamFromMasterItemCommand">
                <HeaderTemplate>
                    <table class="grdView">
                        <tr>
                            <th>
                                S.No
                            </th>
                            <th>
                                Exam Name
                            </th>
                            <th>
                                Subject
                            </th>
                            <th>
                                From Year
                            </th>
                            <th>
                                Sale start date
                            </th>
                            <th>
                                Submit date
                            </th>
                            <th>
                                Result date
                            </th>
                            <th>
                                Result website
                            </th>
                            <th>
                                Form price
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
                            <%# Eval("ExamName")%>
                        </td>
                        <td>
                            <%# Eval("ExamFormSubject")%>
                        </td>
                        <td>
                            <%# Eval("ExamFormYear")%>
                        </td>
                        <td>
                            <%# Eval("ExamFormSaleStartDate")%>
                        </td>
                        <td>
                            <%# Eval("ExamFormSubmitDate")%>
                        </td>
                        <td>
                            <%# Eval("ExamFormResultDate")%>
                        </td>
                        <td>
                            <%# Eval("ExamFormResultWebsite")%>
                        </td>
                        <td>
                            <%# Eval("ExamFormPrice")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("ExamFormId")%>' CommandName="Edit" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
            <script type="text/javascript">
                function ClearAllFields() {
                    document.getElementById('ctl00_ContentPlaceHolderMain_ddlExamName').selectedIndex = 0;
                    document.getElementById('ctl00_ContentPlaceHolderMain_ddlCourseName').selectedIndex = 0;
                    document.getElementById('ctl00_ContentPlaceHolderMain_txtSubjectName').value = '';
                    window.scrollTo(0, 0);
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/Autocomplete.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Styles/autoCompliteCSS.css" />
    <script type="text/javascript" src="../JS/commonscripts.js"></script>
    <script type="text/javascript">
        var examSubject = "../../WebServices/CommonWebServices.asmx/GetExamSubject";
        BindDropDownCommonForAdminAutoComplete($("#<%=txtSubjectName.ClientID %>"), examSubject);

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                BindDropDownCommonForAdminAutoComplete($("#<%=txtSubjectName.ClientID%>"), examSubject);
            }
        }</script>
</asp:Content>
