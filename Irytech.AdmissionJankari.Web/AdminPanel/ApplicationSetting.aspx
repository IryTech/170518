<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ApplicationSetting.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.ApplicationSetting" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" tagname="CustomPaging" TagPrefix="AJ"  %>
<%--<%@ Register src="../../UserControl/CustomPaging.ascx" tagname="CustomPaging" tagprefix="AJ" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
 <asp:UpdatePanel ID="UdtpnlCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:HiddenField runat="server" ID="hdnApplicationSettingId"></asp:HiddenField>
           
               <asp:Label ID="lblHeader" runat="server"></asp:Label>
          
            <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
            <asp:Label ID="lblwarningMsg" CssClass="warning" runat="server" Visible="false"></asp:Label>
            <fieldset id="updateApplicationSetting" visible="false" runat="server">
                <legend>
                    <asp:Label ID="lblInsertUpdate" runat="server" Text="Update Application Setting"></asp:Label>  </legend>
                <ul>
                    <li>
                        <label>Setting Name:</label>
                   
                        <asp:TextBox ID="txtSettingName" runat="server" ReadOnly="true"  CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="40%" onfocus="ClearLabel()" TabIndex="2" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSettingName" runat="server" ControlToValidate="txtSettingName" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>           
                    </li>
                    <li>
                        <label>Setting Value:</label>
                     
                        <asp:TextBox ID="txtSettingvalue" runat="server" onfocus="ClearLabel()" style="max-width: 100%;" Width="60%" TextMode="MultiLine"  TabIndex="2" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSettingValue" runat="server" ControlToValidate="txtSettingvalue" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>&nbsp;</label>
                        <asp:Button ID="btnUpdateApplicationSetting" ToolTip="Click to Update" runat="server" Text="Update" 
                              CausesValidation="true" 
                            ValidationGroup="Setting" TabIndex="3" 
                            onclick="btnUpdateApplicationSettings"/>
                        <input id="btnReset" type="button" value="Reset" onclick="ClearAllFields();" title="Please Reset" />
                    </li>
                </ul>
            </fieldset>
            
            <fieldset>
                <legend>Application Setting List</legend>
                <asp:Label ID="lblRecordMsg" runat="server" Visible="false" CssClass="warning"></asp:Label>
                <asp:Repeater ID="rptApplicationSetting" runat="server" 
                    onitemcommand="rptApplicationSetting_ItemCommand">
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                                <th>S.No</th>
                                <th>Setting Name</th>
                                <th>Setting Value</th>
                                 <th>Action</th>
                             </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>
                                <td><%# Eval("SrNo")%> </td>
                                <td><%# Eval("ApplicationSettingName")%></td>
                               <td><%# Eval("ApplicationSettingValue")%></td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" ToolTip='<%# String.Format("Edit {0}",  DataBinder.Eval(Container.DataItem, "ApplicationSettingName"))%>' runat="server" CssClass="roundedFormat Link_Btn" OnClientClick="return FocusLabel();" Text="Edit" CommandArgument='<%# Eval("ApplicationSettingId")%>' CommandName="Edit" CausesValidation="false" />
                                 </td>
                             </tr>
                         </itemtemplate> 
                         <FooterTemplate>
                        </table></FooterTemplate>
                     </asp:Repeater>
                    
                <AJ:CustomPaging ID="ucCustomPaging"  runat="server" />
            </fieldset>
           <script type="text/javascript">
               function ClearAllFields() {
                   $("#<%=txtSettingvalue.ClientID%>").val('');
                   $("#<%=txtSettingName.ClientID%>").val('');
                 

               }
            </script>
            
       
            
     </ContentTemplate>
     
</asp:UpdatePanel>
</asp:Content>
