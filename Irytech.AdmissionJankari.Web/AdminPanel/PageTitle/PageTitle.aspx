<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="PageTitle.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.PageTitle.PageTitle" %>

<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel runat="server" ID="upPnlNotice">
        <ContentTemplate>
         <ul class="addPage_utility">
        <li class="fright" style="width: 153px !important;">
            <div class="navbar-inner">
                 <a href="#" id='sndAddPageTitle' class="insertIco" onclick="OpenPoup('divCourseCategoryInsert','650px','sndAddPageTitle');return false;">
                                    Add Page Title</a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>



            <div class="grdOuterDiv">
                <asp:HiddenField ID="hndPageId" runat="server" />
                <asp:Label ID="lblInform" runat="server" Text="" Visible="false" CssClass="info">
                </asp:Label>
                <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error">
                </asp:Label>
                <fieldset>
                    <legend>Page Title Master</legend>
                    
                    <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success">
                    </asp:Label>
                    
                  
                        <asp:Repeater ID="rptPageTitle" runat="server" OnItemCommand="rptPageTitle_ItemCommand">
                            <HeaderTemplate>
                                <table class="grdView">
                                    <tr>
                                        <th>
                                            S.No
                                        </th>
                                        <th>
                                            PageName
                                        </th>
                                        <th>
                                            PageTitle
                                        </th>
                                        <th>
                                            PageKeyWord
                                        </th>
                                        <th>
                                            Action
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id='<%# Eval("AjPageId")%>'>
                                    <td>
                                        <%# Eval("SrNo")%>
                                    </td>
                                    <td>
                                        <%# Eval("AjPageName")%>
                                    </td>
                                    <td>
                                        <%# Eval("AjPageTitle")%>
                                    </td>
                                    <td>
                                        <%# Eval("AjPageKeyword")%>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="BtnEdit" runat="server" CssClass="roundedFormat Link_Btn" Text="Edit"
                                            CommandArgument='<%# Eval("AjPageId")%>' CommandName="Edit" CausesValidation="false" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table></FooterTemplate>
                        </asp:Repeater>
                        <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                  
                </fieldset>
            </div>
            <div id="divCourseCategoryInsert" class="popup_block width43perc">
                <fieldset id="basicInfo">
                    <legend>Add Page Title</legend>
                    <ul class="pouplist">
                        <li style="width: 99% !important;">
                            <label>
                                PageName:
                            </label>
                            <asp:TextBox ID="txtPageName" TabIndex="1" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPageName" ValidationGroup="Submit" runat="server"
                                ControlToValidate="txtPageName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important; height:50px !important;">
                            <label>
                                PageTitle:
                            </label>
                            <asp:TextBox ID="txtPageTitle" TabIndex="2" runat="server"  style="height:37px !important;" TextMode="MultiLine" 
                               ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPageTitle" ValidationGroup="Submit" runat="server"
                                ControlToValidate="txtPageTitle" Display="Dynamic"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important; height:50px !important;">
                            <label>
                                PageKeyWord:
                            </label>
                            <asp:TextBox ID="txtPagekeyWord" TabIndex="3" runat="server" style="height:37px !important;" TextMode="MultiLine"
                                ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPageKeyWord" ValidationGroup="Submit" runat="server"
                                ControlToValidate="txtPagekeyWord" Display="Dynamic"></asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important; height:50px !important;">
                            <label>
                                PageDescription:
                            </label>
                            <asp:TextBox ID="txtDesc" TabIndex="3" runat="server" style="height:37px !important;" TextMode="MultiLine" 
                                ></asp:TextBox>
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnSubmit" TabIndex="3" ValidationGroup="Submit" CausesValidation="true"
                                runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                            <input id="btnClear" type="button" value="Clear " tabindex="4" onclick="ClearFields()" />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
