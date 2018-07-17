<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="FAQCategoryMaster.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.FAQCategoryMaster" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:HiddenField ID="hdnFAQCategoryMaster" runat="server" />
    
    
         
           <asp:Label ID="lblHeader" style="display:none;" runat="server"></asp:Label> 
            <asp:Label ID="lblInform" runat="server" Visible="false" Text="Label"></asp:Label>
           <asp:Label ID="lblSuccess" runat="server" Text="" Visible="false" CssClass="success"></asp:Label>
              <asp:Label ID="lblInfo" runat="server" Text="" Visible="false" CssClass="info"></asp:Label>
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="error"></asp:Label>   
        
        
        <ul class="addPage_utility">

    <li class="fright" style="width: 123px !important;">
        <div class="navbar-inner">
            <a class="viewIco" href="FAQDetails.aspx">FAQ Master </a>
            <div class="clear"></div>
        </div>
    </li>
    <li class="fright" style="width: 72px !important;">
        <asp:ImageButton ID="btnUpload" runat="server"  ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" title="Upload Excel"  OnClientClick="colorboxDialogSubmitClicked('ExcelUpload', 'uploadImagePanel'); FocusLabel();" 
                        TabIndex="7" onclick="btnUpload_Click"  />

                    <asp:ImageButton ID="btnSeeExcelFormat" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" title="Download Format" 
                        TabIndex="7" onclick="btnSeeExcelFormat_Click"  />
                        <asp:Label runat="server" Text="" ID="lblRecordsInserted"></asp:Label>
    </li>

</ul>  
        
        
        <fieldset>
            <legend>
                <asp:Label ID="lblInsert" runat="server" Text="Add FAQ Category Master"></asp:Label></legend>
            <ul>
                <li>
                    <label>
                        FAQ Category Name</label>
                    <asp:TextBox runat="server" ID="txtFAQCategoryName" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="40%" onfocus="ClearLabel()" TabIndex="1" ToolTip="Please Enter FAQ Category Name">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFAQCategoryName" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="FAQ" ControlToValidate="txtFAQCategoryName">
                    </asp:RequiredFieldValidator>
                </li>
                 <li>
                    <label>
                        Display</label>
                    <asp:CheckBox runat="server" ID="chkFAQDetailsStatus" TabIndex="2" ToolTip="Please FAQ Details Status">
                    </asp:CheckBox>
                </li>
                <li><label></label>
                    <asp:Button runat="server" Text="Save" ID="btnSave" TabIndex="4" ValidationGroup="FAQ"
                        ToolTip="Please Submit" onclick="btnSave_Click"  />
                    <input id="btnReset" type="button" value="Clear" tabindex="5" onclick="ClearFields()"  />

                </li>
            </ul>
     
       </fieldset>
       <fieldset style="background:none;">
                
                <div >
                    <asp:Repeater ID="rptCategoryMaster" runat="server" 
                        onitemcommand="rptCategoryMaster_ItemCommand">
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                        FAQ Category Name
                                    </th>
                                    <th>
                                        Status
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("SrNo") %>
                                </td>
                                <td>
                                    <%# Eval("FAQCategoryName")%>
                                </td>
                                <td>
                                    <%# Eval("FAQCategoryStatus")%>
                                </td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" runat="server" CssClass="roundedFormat Link_Btn" Text="Edit"
                                        CommandArgument='<%# Eval("FAQCategoryId")%>' CommandName="Edit" CausesValidation="false"  />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>

                    <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                </div>
            </fieldset>
             <fieldset style="display:none;">
            <legend>Upload an excel sheet of FAQ Category Master</legend>
            <ul>
                <li>
                    <label>
                        Upload File:
                    </label>
                    <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="6" />
                    <asp:RequiredFieldValidator ID="rfvExcelUpload" runat="Server" ControlToValidate="fileUploadExcel"
                                ValidationGroup="ExcelUpload" />
                    
                </li>
                
            </ul>

        </fieldset>
    
    <script type="text/javascript">
        function ClearFields() {
            $("#<%=txtFAQCategoryName.ClientID %>").val('');
            $("#<%=btnSave.ClientID %>").val('Save');
            $("#<%=chkFAQDetailsStatus.ClientID %>").removeAttr('checked');
            $("#<%=lblInsert.ClientID %>").text('Insert');
            $("#<%=lblHeader.ClientID %>").text('Add FAQ Category Master');
        }
    </script>
    <script language="javascript" type="text/javascript">
        function ClearLabel() {
            document.getElementById('<%=lblSuccess.ClientID %>').style.display = "none";
            document.getElementById('<%=lblError.ClientID %>').style.display = "none";
        }
            </script>
             <script type="text/javascript">
                 $(document).ready(function () {
                     $("#uploadImage").colorbox({ width: "550px", inline: true, href: "#uploadImagePanel" });
                 });

                 function closeOverlay() {
                     $.colorbox.close();
                 }

                 function colorboxDialogSubmitClicked(validationGroup, panelId) {

                     if (typeof Page_ClientValidate !== 'undefined') {
                         if (!Page_ClientValidate(validationGroup)) {
                             return true;
                         }
                     }
                     $.colorbox.close();
                     $("form").append($("#" + panelId));
                     return true;
                 }
                 function focusOnField() {
                     $("#<%=txtFAQCategoryName.ClientID %>").focus();
                 }
                </script>
</asp:Content>
