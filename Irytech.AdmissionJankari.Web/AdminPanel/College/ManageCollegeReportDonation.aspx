<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ManageCollegeReportDonation.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.College.ManageCollegeReportDonation" %>
    <%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updatelist" runat="server">
<ContentTemplate>
<div>

    <fieldset>
            <legend>Search</legend>
            <ul>
                <li>
                    <asp:Label ID="lblMsg" runat="server" Visible="false" ></asp:Label>
                </li>
                <li>
                    <label>
                        <%=Resources.label.Course %>
                    </label>
                    <asp:DropDownList runat="server" ID="ddlCourse" ToolTip="Please select course" 
                        TabIndex="1" AutoPostBack="true" onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                   
                </li>
          
            </ul>
        </fieldset>
<div class="grdOuterDiv">
        <h3>Event List</h3> 
            <ul>
                <li>
                    <asp:Repeater ID="rptCollegeList" runat="server" 
                        onitemcommand="rptCollegeList_ItemCommand" onitemdatabound="rptCollegeList_ItemDataBound" 
                      >
                        <HeaderTemplate>
                            <table class="grdView">
                                <tr>
                                    <th>
                                        S.No
                                    </th>
                                    <th>
                                       College Name
                                    </th>
                                    <th>
                                       CourseName
                                    </th>
                                   
                                    <th>
                                       Update Status
                                    </th>
                                  
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("SrNo") %>
                                </td>
                                <td>
                                    <%# Eval("AjCollegeBranchName")%>
                                    <asp:Literal ID="hndCollegeBranchCourseId" Visible="false" runat="server" Text='<%# Eval("AjCollegeBranchCourseId")%>'></asp:Literal>
                           <asp:Repeater ID="rptContactDetails" OnItemCommand="rptContactDetails_ItemCommand" runat="server"  >
                            <HeaderTemplate>
                                <table class="grdView">
                                    <tr>
                                        <th>
                                          Student Name
                                        </th>
                                        <th>
                                            Email Id
                                        </th>
                                        <th>
                                           Mobile
                                        </th>
                                        <th>
                                          Student Query
                                        </th>
                                       
                                       
                                        <th>
                                         Action
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                             <ItemTemplate>
                        <tr>
                           <td>
                               
                              <%# Eval("AjUserFullName")%>   
                            </td>
                            <td>
                              <%# Eval("AjUserEmail")%> 
                                   
                            </td>
                            <td>
                             <%# Eval("AjUserMobile")%> 
                            </td>
                            <td>
                             <%# Eval("AjUserStory")%>
                            </td>
                            
                             <td>
                              <asp:LinkButton ID="lnkUpdateStatus" runat="server" Text='<%#  Eval("AjReportStatus")%>' CommandArgument='<%# Eval("AjReportDonationId")%>'
                                    CommandName="Edit" CausesValidation="false" />
                            </td>
                          
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table></FooterTemplate>
                        </asp:Repeater>
                                </td>
                                <td>
                                    <%# Eval("AjCourseName")%>
                                </td>
                               
                                <td>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text= '<%#  Eval("AjReportStatus")%>' CommandArgument='<%# Eval("AjCollegeBranchCourseId")%>'
                                        CommandName="Edit" CausesValidation="false" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                    <aj:custompaging id="ucCustomPaging" runat="server" />
                </li>
            </ul>
        
        </div>
        </div>
        </ContentTemplate>
   </asp:UpdatePanel>
   
</asp:Content>
