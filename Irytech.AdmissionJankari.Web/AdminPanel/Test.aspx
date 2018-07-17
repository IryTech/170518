<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:FileUpload ID="fulUploadCollegeImage" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
        onclick="btnUpload_Click" />
   
</asp:Content>
