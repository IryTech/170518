<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AppsError.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Setting.AppsError" %>

<%@ Register src="~/UserControl/CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
 <asp:UpdatePanel ID="UdtpnlFile" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
             
                
            <fieldset>
                <legend>Application Server Error List</legend>
                <asp:Label ID="lblRecordMsg" runat="server" Visible="false" CssClass="warning"></asp:Label>
                <asp:Repeater ID="rptAppsError" runat="server" 
                    onitemcommand="rptAppsError_ItemCommand" >
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                                <th>S.No</th>
                                <th>Source</th>
                                <th>Message</th>
                                 <th>Action</th>
                             </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>
                                 <td><%# Eval("SrNo")%> </td>
                                <td><%# Eval("Source")%></td>
                               <td><%# Eval("Message")%></td>
                               
                                <td>
                                    <asp:LinkButton ID="BtnDelete" OnClientClick="return confirm('Do you want to delete this record.')"  ToolTip='<%# String.Format("Delete {0}",  DataBinder.Eval(Container.DataItem, "Message"))%>' runat="server" CssClass="roundedFormat Link_Btn"  Text="Delete" CommandArgument='<%# Eval("ApplicationIncidentId")%>' CommandName="Delete" CausesValidation="false" />
                                 </td>

                             </tr>
                         </itemtemplate> 
                         <FooterTemplate>
                        </table></FooterTemplate>
                     </asp:Repeater>
                 <AJ:CustomPaging ID="ucCustomPaging"  runat="server" />
            </fieldset>
           
            
       
            
     </ContentTemplate>
     
</asp:UpdatePanel>

</asp:Content>
