<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SponseredColleges.ascx.cs"
    Inherits="IryTech.AdmissionJankari20.Web.UserControl.SponseredColleges" %>
<%@ Import Namespace="IryTech.AdmissionJankari.BL" %>
<div runat="server" id="divSponseredCollege" visible="False">
    <div class="aboutStudentHeader marginBottom">
        <h2 class="ClientHeader" runat="server" id="headerSponseredColleges" visible="False">
            Our Key Clients
        </h2>
    </div>
    <asp:Repeater ID="dtlSponseredColleges" runat="server">
        <HeaderTemplate>
            <ul class="ulClients">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <img title="<%# Eval("AjCollegeBranchName") %>" src='<%# String.Format("{0}{1}","/image.axd?College=",string.IsNullOrEmpty(Eval("AjCollegeBranchLogo").ToString()) ?"NoImage.jpg":Eval("AjCollegeBranchLogo")) %>'
                    alt='<%# Eval("AjCollegeBranchName") %>' />
                <div class="collegeName">
                    <%# Eval("AjCollegeBranchName") %></div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul></FooterTemplate>
    </asp:Repeater>
</div>
<style>
    div.collegeName
    {
        position: relative;
        overflow: hidden;
        background-color: aliceblue;
        font-size: 1.0em;
        text-align: center;
        height: 52px;
        width: 100%;
        padding: 2px;
    }
    .aboutStudentHeader
    {
        width: 950px;
        height: auto;
        padding: 7px 5px;
        border-bottom: 2px solid rgb(157, 207, 228);
        overflow: auto;
    }
    
    .ClientHeader
    {
        font-size: 1.3em !important;
        font-weight: bold !important;
        color: rgb(50, 50, 51) !important;
        line-height: normal !important;
    }
    ul.ulClients
    {
        list-style: none;
        width: 100%;
    }
    ul.ulClients li
    {
        width: 190px;
        display: inline-block;
        padding: 2px;
        margin: 15px;
        border-collapse: separate;
        border-bottom: 1px solid rgb(222, 222, 222);
        border-right: 1px solid rgb(222, 222, 222);
        border-left: 1px solid rgb(222, 222, 222);
        border-width: 1px;
        border-bottom-left-radius: 3px;
        border-bottom-right-radius: 3px;
        box-shadow: 0px 0px 10px 1px rgba(0, 0, 0, 0.05);
        height: auto;
    }
    ul.ulClients li img
    {
        width: 170px;
        height: 170px !important;
        padding-top: 8px;
        padding-left: 8px;
        z-index: 99990;
    }
</style>
