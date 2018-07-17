<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ValidationMessage.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Message.ValidationMessage" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlValidationMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
               <asp:Label ID="lblHeader" runat="server"></asp:Label>
            
            <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
            <asp:Label ID="lblwarningMsg" CssClass="warning" runat="server" Visible="false"></asp:Label>
            
            <fieldset id="updateValidationMessage" visible="false" runat="server">
                <legend>
                    <asp:Label ID="lblInsertUpdate" runat="server" Text="Update Validation Message"></asp:Label>  </legend>
                <ul>
                    <li>
                        <label>Validation Id:</label>
                   
                        <asp:TextBox ID="txtValidationId" runat="server" ReadOnly="true" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="40%"  TabIndex="1" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvValidationId" runat="server" ControlToValidate="txtValidationId" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>           
                    </li>
                    <li>
                        <label>Error Message:</label>
                     
                        <asp:TextBox ID="txtValidationMessage" runat="server" style="max-width: 100%;" Width="60%" TextMode="MultiLine"  TabIndex="2" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvValidationMessage" runat="server" ControlToValidate="txtValidationMessage" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>&nbsp;</label>
                        <asp:Button ID="btnUpdateErrorMessage" ToolTip="Click to Update" runat="server" Text="Update" 
                              CausesValidation="true" 
                            ValidationGroup="Setting" TabIndex="3" 
                            onclick="UpdateValidationMessage"/>
                        <input id="btnReset" type="button" value="Reset" onclick="ClearFields()" title="Please Reset" />
                    </li>
                </ul>
            </fieldset>
            
            <fieldset>
                <legend>Application Validation List</legend>
                <asp:Label ID="lblRecordMsg" runat="server" Visible="false" CssClass="warning"></asp:Label>
                <asp:Repeater ID="rptValidationMessage" runat="server" 
                    onitemcommand="rptValidationMessageItemCommand">
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                                <th>S.No</th>
                                <th>Validation Id</th>
                                <th>Validation Message</th>
                                 <th>Action</th>
                             </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>

                           
                                <td><%# Eval("SrNo") %></td>
                                <td><%# Eval("MessageID")%></td>
                               <td><%# Eval("description")%></td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" ToolTip='<%# String.Format("Edit {0}",  DataBinder.Eval(Container.DataItem, "description"))%>' runat="server" CssClass="roundedFormat Link_Btn" OnClientClick="return FocusLabel();" Text="Edit" CommandArgument='<%# Eval("MessageID")%>' CommandName="Edit" CausesValidation="false" />
                                 </td>
                             </tr>
                         </itemtemplate> 
                         <FooterTemplate>
                        </table></FooterTemplate>
                     </asp:Repeater>
                     

                     <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
               <%--<AJ:CustomPaging ID="ucCustomPaging"  runat="server" />--%>
            </fieldset>
           <script type="text/javascript">
              
            </script>
            
       
            
     </ContentTemplate>
     
</asp:UpdatePanel>
<div>
<script type="text/javascript">
    function ClearFields() {
        $("#<%=txtValidationMessage.ClientID %>").val('');
        window.scrollTo(0, 0);
    }
    </script>
</div>
</asp:Content>
