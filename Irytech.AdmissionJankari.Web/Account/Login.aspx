<%@ Page Title="Log In" Language="C#"  AutoEventWireup="true"  CodeBehind="Login.aspx.cs" Inherits="IryTech.AdmissionJankariVer20.UI.Account.Login" %>
<%@ Register Src="~/UserControl/Login.ascx" TagPrefix="ADMJ" TagName="Login" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
   <div  style="width:460px; margin:0 auto;">
    <ul>
        <li>
             <ADMJ:Login ID="ucLogin" runat="server" />
        </li>
     
    </ul>
    </div>

 
</asp:Content>
