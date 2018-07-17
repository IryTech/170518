<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ManageBankDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.BankDetails.ManageBankDetails" %>

<%@ Register Src="../../UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="AJ" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<ul class="addPage_utility">
        <li class="fright" style="width: 160px !important;">
            <div class="navbar-inner">
                <a href="AddBankLoanDetails.aspx" class="insertIco">Add Bank Master </a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>
    <fieldset>
        <legend>Manage Bank Details</legend>
        <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
        </asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
        </asp:Label>
        <asp:Repeater ID="rptBankDetails" runat="server" OnItemCommand="rptBankDetails_ItemCommand" OnItemDataBound="rptBankDetails_ItemDataBound">
            <HeaderTemplate>
                <table class="grdView">
                    <tr>
                        <th>
                            ViewLoan
                        </th>
                        <th>
                            S.No
                        </th>
                        <th>
                            BankName
                        </th>
                        <th>
                            Url
                        </th>
                        <th>
                            Phone
                        </th>
                        <th>
                            ContactPerson
                        </th>
                        <th>
                            ViewLoan
                        </th>
                        <th>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <a href="JavaScript:divexpandcollapse('div<%# Eval("BankId") %>');">
                            <img id="imgdiv<%# Eval("BankId") %>" width="9px" border="0" src="../Images/CommonImages/plus.gif" alt="+" />
                        </a>
                        <div id="div<%# Eval("BankId") %>" style="width: 550px" class="popup_block">
                            <br />
                            <a href="#" class="close">
                                <img src="/Image/CommonImages/closebox.png" class="btn_close" title="Close Window" alt="Close" /></a>
                            <h4>
                                Loan Details</h4>
                            <asp:Label runat="server" ID='lblBankResult' Visible="False"></asp:Label>
                            <asp:Repeater ID="rptLoanDetails" runat="server">
                                <HeaderTemplate>
                                    <table class="grdView">
                                        <tr>
                                            <th>
                                                S.No
                                            </th>
                                            <th>
                                                Eligibilty
                                            </th>
                                            <th>
                                                RepaymentDuration
                                            </th>
                                            <th>
                                                ProcessingFees
                                            </th>
                                            <th>
                                                RateOfInterest
                                            </th>
                                            <th>
                                                ProcessingTime
                                            </th>
                                            <th>
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Container.ItemIndex+1 %>
                                        </td>
                                        <td>
                                            <%# Eval("Eligibilty")%>
                                        </td>
                                        <td>
                                            <%# Eval("RepaymentDuration")%>
                                        </td>
                                        <td>
                                            <%# Eval("ProcessingFees")%>
                                        </td>
                                        <td>
                                            <%# Eval("RateOfInterest")%>
                                        </td>
                                        <td>
                                            <%# Eval("ProcessingTime")%>
                                        </td>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                    <td>
                        <%#Container.ItemIndex+1 %>
                    </td>
                    <td>
                        <%# Eval("BankName")%>
                        <asp:HiddenField runat="server" ID="hdnBankId" Value='<%# Eval("BankId")%>'></asp:HiddenField>
                    </td>
                    <td>
                        <%# Eval("BankUrl")%>
                    </td>
                    <td>
                        <%# Eval("BankPhoneNo")%>
                    </td>
                    <td>
                        <%# Eval("BankContactPerson")%>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("BankId")%>' CommandName="Edit" CausesValidation="false" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
    </fieldset>
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            $("div").addClass('display', 'none');
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../Images/CommonImages/minus.gif";
                $(".close").show();
            }
            else {
                div.style.display = "none";
                img.src = "../Images/CommonImages/plus.gif";
            }
        }
        $('a.close, #fade').click(function () { //When clicking on the close or fade layer...
            $('#fade , .popup_block').fadeOut(function () {

            }); //fade them both out

            return false;
        });

    </script>
</asp:Content>
