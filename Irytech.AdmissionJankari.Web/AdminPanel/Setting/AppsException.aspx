<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="AppsException.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Setting.AppsException" %>

<%@ Register src="../../UserControl/CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
<asp:UpdatePanel ID="UdtpnlFile" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
       
         <fieldset>
              <legend>Application Error List</legend>
                <asp:Label ID="lblRecordMsg" runat="server" Visible="false" CssClass="warning"></asp:Label>
                <asp:Repeater ID="rptAppsException" runat="server" 
                  onitemcommand="rptAppsException_ItemCommand" >
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                                <th>S.No</th>
                                <th>Exception Source</th>
                                <th>Exception Message</th>
                                <th>Exception Date</th>
                                 <th>Action</th>
                             </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>
                                <td><%# Eval("SrNo")%> </td>
                                <td><%# Eval("ExceptionInfo")%></td>
                               <td><%# Eval("AdditionalInfo")%></td>
                               <td><%# Eval("ExceptionDateTime")%></td>
                                <td>
                                    <asp:LinkButton ID="BtnDelete" OnClientClick="return confirm('Do you want to delete this record.')" ToolTip='<%# String.Format("Delete {0}",  DataBinder.Eval(Container.DataItem, "ExceptionInfo"))%>' runat="server" CssClass="roundedFormat Link_Btn"  Text="Delete" CommandArgument='<%# Eval("ExceptionId")%>' CommandName="Delete" CausesValidation="false" />
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
