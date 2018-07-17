<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="CreatePrmotionalPage.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.CreatePrmotionalPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <fieldset>
      <legend>Create Promotional Page   </legend>
        <ul>
            <li>
                <label>
                   Promotional Type :</label>
                    <asp:DropDownList ID="ddlPrmotionType" runat="server" >
                    <asp:ListItem Enabled="true" Text="Select" Value="0" ></asp:ListItem>
                    <asp:ListItem Enabled="true" Text="College" Value="1" ></asp:ListItem>
                    <asp:ListItem Enabled="true" Text="Exam" Value="2" ></asp:ListItem>
                    <asp:ListItem Enabled="true" Text="Bank" Value="3" ></asp:ListItem>

                    </asp:DropDownList>
            </li>
            <li id="liCourse" >
                <label>
                  Course:</label>
                <asp:DropDownList ID="ddlCourse" runat="server"></asp:DropDownList>
            </li>
            <li>
                <label id="lblShowName">
                     :</label>
               <asp:TextBox ID="txtPrmotionType" runat="server"></asp:TextBox>
            </li>
            <li>
                <label>
                    Promotional Content</label>
              
            </li>
            <li>
                <label>
                    Testimonial Text:</label>
                <aj:testimonial runat="server" id="txtTestimonial" />
                (Please enter less than 300 characters for more visibilty) </li>
            <li>
                <label>
                    Status:</label>
                <asp:CheckBox ID="chkTestimonialStatus" runat="server" ToolTip="Check Status" TabIndex="5" />
            </li>
            <li>
                <label>
                </label>
                <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="Click to finish process"
                    CssClass="button" TabIndex="6" OnClick="btnSave_Click" />
            </li>
        </ul>
    </fieldset>
    <
</asp:Content>
