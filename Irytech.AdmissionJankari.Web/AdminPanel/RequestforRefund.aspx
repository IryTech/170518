<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="RequestforRefund.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.RequestforRefund" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
 <asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>

    <div class="grdOuterDiv">
        <h4>Manage Refund Request:</h4>
     <br /><div>
        <fieldset>
            <legend>
                Refund Request
            </legend>
            <asp:HiddenField ID="hdnCourseId" runat="server" />
            <asp:HiddenField ID="hdnEmailId" runat="server" />
            <asp:HiddenField ID="hdnFormNo" runat="server" />
            <ul>

                    <li>
                            <label>
                                 Select Course:
                            </label>
                            <asp:DropDownList ID="ddlCourse" TabIndex="1" 
                                runat="server" >
                            </asp:DropDownList>
                    </li>
                    <li>
                        <label>Email ID:</label>
                        <asp:TextBox ID="txtFilterbyEmail" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="validateEmail" ValidationGroup="search" runat="server" ErrorMessage="Please enter a valid Email" ControlToValidate="txtFilterbyEmail" Display="Dynamic" SetFocusOnError="True" Font-Names="verdana" Font-Size="X-Small" ForeColor="Red"></asp:RegularExpressionValidator>
                    </li>
                    <li>
                        <label>Form Number:</label>
                        <asp:TextBox ID="txtFilterbyFormNo" runat="server"></asp:TextBox>

                    </li>
                    <li>
                        <label> </label>
                            <asp:Button ID="btnSearch" Text="Search" ValidationGroup="search" runat="server" onclick="btnSearch_Click" />
                            <asp:Button ID="btnReset" Text="Reset" runat="server" onclick="btnReset_Click" />
                    </li>
            </ul>
           
            <asp:Label ID="lblMsg" runat="server" ></asp:Label>
               
            <asp:Repeater ID="rptRefundRequest" runat="server"> 
                <HeaderTemplate>
                    <table class="grdView">
                    <tr>
                        <th>S.No.</th>
                                
                        <th>Email Id</th>
                        <th>Form No.</th>
                        <th>Status</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Srno") %></td>
                        <td>
                            <%# Eval("AjUserEmail") %>
                            <asp:HiddenField ID="hndCollegeBranchCourseID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "AjRefundID") %>' />    
                        </td>
                        <td><%# Eval("AjStudentFormNumber") %></td>
                        <td><%# Eval("AjRefundStatus") %></td>
                                
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
               
            <AJ:CustomPaging ID="UcCustomPaging" runat="server" />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>

                    <img src="/image.axd?Common=LoadingImage.gif"  />
                                       
                </ProgressTemplate>
            </asp:UpdateProgress>
        </fieldset>
      </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
