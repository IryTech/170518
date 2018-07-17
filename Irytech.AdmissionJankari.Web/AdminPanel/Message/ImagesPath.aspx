<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ImagesPath.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Message.ImagesPath" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="UdtpnlFile" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         
               <asp:Label ID="lblHeader" runat="server"></asp:Label>
             
            <asp:Label ID="lblMsg" runat="server" CssClass="success" Visible="false"></asp:Label>
            <asp:Label ID="lblwarningMsg" CssClass="warning" runat="server" Visible="false"></asp:Label>
           
            <fieldset id="updateFilePathMessage" visible="false" runat="server">
                <legend>
                    <asp:Label ID="lblInsertUpdate" runat="server" Text="File Path List"></asp:Label>  </legend>
                <ul>
                    <li>
                        <label>Error Message Id:</label>
                   
                        <asp:TextBox ID="txtFilePathId" runat="server" ReadOnly="true" CssClass="autocomplete" style="background:none !important; text-indent:5px !important;" Width="40%"  TabIndex="1" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFilePathId" runat="server" ControlToValidate="txtFilePathId" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>           
                    </li>
                    <li>
                        <label>Error Message:</label>
                     
                        <asp:TextBox ID="txtFilePath" runat="server" style="max-width: 100%;" Width="60%" TextMode="MultiLine"  TabIndex="2" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFilePath" runat="server" ControlToValidate="txtFilePath" SetFocusOnError="true" ValidationGroup="Setting"></asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>&nbsp;</label>
                        <asp:Button ID="btnUpdateErrorMessage" ToolTip="Click to Update" runat="server" Text="Update" 
                              CausesValidation="true" 
                            ValidationGroup="Setting" TabIndex="3" 
                            onclick="UpdateFilePath"/>
                        <input id="btnReset" type="button" value="Reset" onclick="ClearFields()" title="Please Reset" />
                    </li>
                </ul>
            </fieldset>
            
            <fieldset>
                <legend>Application Image Path</legend>
                <asp:Label ID="lblRecordMsg" runat="server" Visible="false" CssClass="warning"></asp:Label>
                <asp:Repeater ID="rptFilePath" runat="server" 
                    onitemcommand="rptFilePathItemCommand">
                       <headertemplate>
                          <table class="grdView"> 
                            <tr>
                                <th>S.No</th>
                                <th>Image Id</th>
                                <th>Image Path</th>
                                 <th>Action</th>
                             </tr>
      
                        </headertemplate>
                        <itemtemplate>
                           <tr>
                                <td><%# Eval("SrNo") %> </td>
                                <td><%# Eval("PathId")%></td>
                               <td><%# Eval("lgpath")%></td>
                                <td>
                                    <asp:LinkButton ID="BtnEdit" ToolTip='<%# String.Format("Edit {0}",  DataBinder.Eval(Container.DataItem, "lgpath"))%>' runat="server" CssClass="roundedFormat Link_Btn" OnClientClick="return FocusLabel();" Text="Edit" CommandArgument='<%# Eval("PathId")%>' CommandName="Edit" CausesValidation="false" />
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
        $("#<%=txtFilePath.ClientID %>").val('');
        window.scrollTo(0, 0);
    }
    </script>
</div>
</asp:Content>
