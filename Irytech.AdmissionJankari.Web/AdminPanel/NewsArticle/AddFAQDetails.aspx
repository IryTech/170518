<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AddFAQDetails.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.NewsArticle.AddFAQDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
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
        <asp:ImageButton ID="btnUpload" ImageUrl="~/AdminPanel/Images/CommonImages/uploadExcel.png" title="Upload Excel" runat="server"  ValidationGroup="GrUpload"
                        TabIndex="7" onclick="BtnUploadClick"  />
        <asp:ImageButton ID="btnSeeExcelFormat" runat="server" ImageUrl="~/AdminPanel/Images/CommonImages/downloadExcel2.png" title="Download Format"  
                        TabIndex="7" onclick="BtnSeeExcelFormatClick" />
                        <asp:Label runat="server" Text="" ID="lblRecordsInserted"></asp:Label>
    </li>

</ul>
   
   
   
   
   
     
        
        <fieldset>
            <legend><asp:Label ID="lblHeader" runat="server"></asp:Label>
                <asp:Label ID="lblInsertUpdate" runat="server" Text=""></asp:Label></legend>
            <ul>               
                <li>
                    <label>
                       FAQ Category</label>
                    <asp:DropDownList ID="ddlFAQCategoryId" runat="server" TabIndex="1" ToolTip="Please Select FAQ Category" onfocus="ClearLabel()">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFAQCategoryId" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="FAQ" ControlToValidate="ddlFAQCategoryId">
                    </asp:RequiredFieldValidator>
                </li>
               <li>
                    <label>
                        FAQ</label>
                    <asp:TextBox runat="server" ID="txtFAQQuestion" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="40%" placeholder="Enter FAQ Name" TabIndex="2" ToolTip="Please Enter FAQ Name">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFAQName" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="FAQ" ControlToValidate="txtFAQQuestion">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Answer</label>
                    <asp:TextBox runat="server" ID="txtFAQDetailsAnswer" style="max-width: 100%;" Width="60%" TextMode="MultiLine" TabIndex="3" ToolTip="Please Enter FAQ Details Answer">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFAQDetailsAnswer" SetFocusOnError="true" runat="server"
                        Display="Dynamic" ValidationGroup="FAQ" ControlToValidate="txtFAQDetailsAnswer">
                    </asp:RequiredFieldValidator>
                </li>
                <li>
                    <label>
                        Display</label>
                    <asp:CheckBox runat="server" ID="chkFAQDetailsStatus" TabIndex="4" ToolTip="Please FAQ Details Status">
                    </asp:CheckBox>
                </li>
                <li>   <label></label>                
                    <asp:Button ID="BtnSubmit" runat="server" Text="Save" abIndex="5" ValidationGroup="FAQ"
                        ToolTip="Please Submit" onclick="BtnSubmitClick" />
                    <input id="btncancel" type="button" value="Cancel" onclick="ClearAllFields();" />

                </li>
            </ul>
        </fieldset>
        <fieldset style="display:none">
            <legend>Upload an excel sheet of FAQ Details</legend>
            <ul>
                <li>
                    <label>
                        Upload File:
                    </label>
                    <asp:FileUpload ID="fileUploadExcel" runat="server" TabIndex="6" />
                    <asp:RequiredFieldValidator ID="rfvfileUploadExcel" runat="server" ControlToValidate="fileUploadExcel" SetFocusOnError="true"
                        ValidationGroup="GrUpload"></asp:RequiredFieldValidator>                    
                </li>


                



            </ul>
        </fieldset>
    
        <script type="text/javascript">
            function ClearAllFields() {
                $("#<%=ddlFAQCategoryId.ClientID %>").val(0);
                $("#<%=txtFAQQuestion.ClientID %>").val('');
                $("#<%=txtFAQDetailsAnswer.ClientID %>").val('');
                $("#<%=chkFAQDetailsStatus.ClientID %>").removeAttr('checked');
                $("#<%=BtnSubmit.ClientID %>").val('Save');
                $("#<%=lblInsertUpdate.ClientID %>").text('Insert');
                $("#<%=lblHeader.ClientID %>").text('Add FAQ Details');
            }
            </script>
             <script language="javascript" type="text/javascript">
                 function ClearLabel() {
                     document.getElementById('<%=lblSuccess.ClientID %>').style.display = "none";
                     document.getElementById('<%=lblError.ClientID %>').style.display = "none";
                 }
            </script>
</asp:Content>
