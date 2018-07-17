<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="FactorMaster.aspx.cs" Inherits="Irytech.AdmissionJankari.Web.AdminPanel.Form_Package.FactorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updSpeechDetails" runat="server">
        <ContentTemplate>
            <div id="fade">
            </div>
            <asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
            <div id="divImage" class="loading">
                <img src="/image.axd?Common=Loading.gif" />
            </div>

            <ul class="addPage_utility">
                <li class="fright" style="width: 213px !important;">
                    <div class="navbar-inner">
                        <a href="#" id='sndAddCollegePresedentSpeech' class="insertIco" onclick="OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegePresedentSpeech');return false;">Add Form Factor </a>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ul>

            <fieldset>
                <legend>Form Factor</legend>

                <asp:Label ID="lblErrorMessage" runat="server" CssClass="err_msg" Visible="false"></asp:Label>
                <asp:Repeater ID="rptPresidentDetails" runat="server" OnItemCommand="rptPresidentDetails_ItemCommand">
                    <HeaderTemplate>
                        <table class="grdView">
                            <tr>

                                <th>Factor Name
                                </th>
                                <th>Factor Remark
                                </th>
                                <th>Is Chargeable 
                                </th>
                                <th>Is Visible 
                                </th>

                                <th>Action
                                </th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%# Eval("FactorName")%>
                            </td>
                            <td>
                                <%# Eval("FactorRemark")%>
                            </td>
                            <td>
                                <%# Eval("IsChargeable")%>
                            </td>
                            <td>
                                <%# Eval("IsVisible")%>
                            </td>

                            <td>
                                <asp:LinkButton ID="BtnEdit" runat="server" CssClass="roundedFormat Link_Btn" Text="Edit"
                                    CommandArgument='<%# Eval("FactorID")%>' CommandName="Edit" CausesValidation="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id="Progress" class="pop">
                            <img src="/image.axd?Common=LoadingImage.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </fieldset>
            <asp:HiddenField ID="hndFactorId" runat="server" />

            <div id="divUniversityCategoryInsert" class="popup_block width60perc">
                <fieldset id="basicInfo">
                    <legend>
                        <asp:Label ID="lblCollegePlacement" runat="server"></asp:Label></legend>
                    <ul class="pouplist">
                        <li style="width: 99% !important;">
                            <label>
                                Factor Name:</label>
                            <asp:TextBox ID="txtFactorName" runat="server" ToolTip="Enter Factor Name" Style="min-width: 207px !important;" TabIndex="1" ValidationGroup="President"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCollegeName" ControlToValidate="txtFactorName" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="President">Enter factor name</asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Factor Remark :</label>
                            <asp:TextBox ID="txtFactorRemark" runat="server" TextMode="MultiLine" Rows="6" Columns="9" ToolTip="Enter Factor Remark" Style="min-width: 207px !important;" TabIndex="2" ValidationGroup="President"></asp:TextBox>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Is Chargeable:
                            </label>
                            <asp:CheckBox ID="chkChargeable" runat="server" ToolTip="Is Factor is Chargeable" TabIndex="3" />

                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                Is Visible:
                            </label>
                            <asp:CheckBox ID="chkVisible" runat="server" ToolTip="Is Factor is Chargeable" TabIndex="4" />

                        </li>

                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnSubmit" runat="server" TabIndex="5" CausesValidation="true" OnClick="btnSubmit_Click" Text="Insert"
                                ValidationGroup="President"  />
                            <asp:Button ID="btnClear" TabIndex="6" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
      <asp:PostBackTrigger ControlID="rptPresidentDetails" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>
