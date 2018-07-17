<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ErrorMessage.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Message.ErrorMessage" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlErrorMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
      
 
               <asp:Label ID="lblHeader" runat="server"></asp:Label>
            
            <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
            <asp:Label ID="lblwarningMsg" CssClass="warning" runat="server" Visible="false"></asp:Label>
        


            <fieldset id="updateErrorMessage" visible="false" runat="server">
                <legend>
                    <asp:Label ID="lblInsertUpdate" runat="server" Text="Update Error Message"></asp:Label>  </legend>
                <ul>
                    <li>
                        <label>Error Message Id:</label>
                   
                        <asp:TextBox ID="txtErrorMessageId" runat="server" ReadOnly="true"  CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="40%" TabIndex="2" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvErrorMessageId" runat="server" ControlToValidate="txtErrorMessageId" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>           
                    </li>
                    <li>
                        <label>Error Message:</label>
                     
                        <asp:TextBox ID="txtErrorMessageValue" runat="server"  style="max-width: 100%;" Width="60%" TextMode="MultiLine"  TabIndex="2" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvErrorMessage" runat="server" ControlToValidate="txtErrorMessageValue" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>&nbsp;</label>
                        <asp:Button ID="btnUpdateErrorMessage" ToolTip="Click to Update" runat="server" Text="Update" 
                              CausesValidation="true" 
                            ValidationGroup="Setting" TabIndex="3" 
                            onclick="UpdateErrorMessage"/>
                        <input id="btnReset" type="button" value="Reset" onclick="ClearFields()" title="Please Reset" />
                    </li>
                </ul>
            </fieldset>
            
            <fieldset>
                <legend>Application Error List</legend>
                <asp:Label ID="lblRecordMsg" runat="server" Visible="false" CssClass="warning"></asp:Label>
                <asp:Repeater ID="rptErrorMessage" runat="server" 
                    onitemcommand="rptErrorMessageItemCommand">
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                              <th> SrNo </th>
                                <th>Error Message Id</th>
                                <th>Error Message</th>
                                 <th>Action</th>
                             </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>
                               <td>  <%# Eval("SrNo") %> </td>
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
            </fieldset>
           <script type="text/javascript">
              
            </script>
            
       
            
     </ContentTemplate>
     
</asp:UpdatePanel>
<div>
<script type="text/javascript">
    function ClearFields() {
        $("#<%=txtErrorMessageValue.ClientID %>").val('');
        window.scrollTo(0, 0);
    }
    </script>
</div>
</asp:Content>
