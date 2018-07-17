<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FckEditorCostomize.ascx.cs" Inherits="IryTech.AdmissionJankari20.Web.UserControl.FckEditorCostomize" %>

<CKEditor:CKEditorControl ID="txtFckEditorCostomize" TabIndex="7"  onfocus="ClearLabel()" runat="server" BasePath="~/fckeditor">
</CKEditor:CKEditorControl>
<asp:RequiredFieldValidator ID="rfvEditor" runat="server" ControlToValidate="txtFckEditorCostomize" Display="Dynamic" SetFocusOnError="True" ></asp:RequiredFieldValidator>

