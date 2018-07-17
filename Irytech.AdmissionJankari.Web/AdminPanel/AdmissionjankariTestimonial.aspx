<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master"
    AutoEventWireup="true" CodeBehind="AdmissionjankariTestimonial.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.AdmissionjankariTestimonial" %>

<%@ Register TagName="Testimonial" TagPrefix="Aj" Src="~/UserControl/FckEditorCostomize.ascx" %>
<%@ Register TagPrefix="AJ" TagName="CustomPaging" Src="~/UserControl/CustomPaging.ascx" %>
<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUpload" TagPrefix="Aj" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
      <asp:Label runat="server" Text="" ID="lblResult" Visible="false"></asp:Label>
            <div class="grdOuterDiv">
                <asp:HiddenField ID="hdnTestimonialId" runat="server" />
                 <fieldset>
                  
                    <legend>College Testomonial Master</legend>
                 <ul class="addPage_utility">
                    <li class="fright" style="width: 215px !important;">
                        <div class="navbar-inner">
                            <a href="#" id='sndAddCollegeTestomonial' class="insertIco" onclick="OpenPoup('divUniversityCategoryInsert','650px','sndAddCollegeTestomonial');return false;">Add College Placement </a>
                            <div class="clear">
                            </div>
                        </div>
                    </li>
                </ul>
                </fieldset>
            
            
     <%--  <asp:UpdatePanel runat="server">
       <ContentTemplate>--%>
    <asp:Repeater ID="rptTestimonial" runat="server" OnItemCommand="RptTestimonialItemCommand">
        <HeaderTemplate>
            <table class="grdView">
                <tr>
                    <th>
                        S.NO
                    </th>
                    <th>
                        PersonName
                    </th>
                    <th>
                        Designation
                    </th>
                    <th>
                        Status
                    </th>
                    <th>
                        Testimonial
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("SrNo") %>
                </td>
                <td>
                    <%# Eval("AjTestimonialPersonName")%>
                </td>
                <td>
                    <%# Eval("AjTestimonialPersonDesignation")%>
                </td>
                <td>
                    <%# Eval("AjTestimonialStatus")%>
                </td>
                <td>
                    <%# Eval("AjTestimonialText")%>
                </td>
                <td>
                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("AjTestimonialId")%>'
                        CommandName="Edit" OnClientClick="return FocusLabel();" CausesValidation="false" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
      
    </asp:Repeater>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <AJ:CustomPaging ID="ucCustomPaging" runat="server" />
     <div id="divUniversityCategoryInsert" class="popup_block width60perc">
                <fieldset id="basicInfo">
                    <legend>
                        <legend>Insert College Testomonial</legend></legend>
                    <ul class="pouplist clear">
                        <li style="width: 99% !important; height:39px !important;">
                           <label>
                                Person Name:</label>
                            <asp:TextBox runat="server" ID="txtPersonName" CssClass="autocomplete" style="background:none !important; table-layout:5px !important;" Width="63%" ToolTip="Enter Person Name" TabIndex="1"> </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPersonName" runat="server" ControlToValidate="txtPersonName" ValidationGroup="vldgTestomonial" CssClass="error1">Field Person Name can not be blank</asp:RequiredFieldValidator>
                        </li>
                        <li id="filterList" runat="server" style="width: 99% !important;  height:39px !important;">
                             <label>
                                Person Designation:</label>
                            <asp:TextBox runat="server" ID="txtPersondesig" CssClass="autocomplete" style="background:none !important; table-layout:5px !important;" Width="63%" ToolTip="Enter Person designation"
                                TabIndex="2"> </asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvOesindegination" runat="server" ValidationGroup="vldgTestomonial" ControlToValidate="txtPersondesig" CssClass="error1">Field Person Designation can not be blank</asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important;">
                          <label>
                                Person Image:</label>
                            
                            <AJ:FileUpload ID="FileUpload1"  runat="server" />
                            <asp:Image runat="server" ID="imgCollege" Height="50px" Width="50px">
                            </asp:Image>
                            <asp:HiddenField ID="hdnImageFile" runat="server" />
                        </li>
                        <li style="width: 99% !important;">
                             <label>
                                Testimonial Priority</label>
                            <asp:TextBox runat="server" ID="txtPriority" ToolTip="Enter Testimonial priority"
                                TabIndex="3"> </asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvPeriorty" runat="server" ValidationGroup="txtPriority" ControlToValidate="txtPersondesig" CssClass="error1">Field  Testimonial Priority can not be blank</asp:RequiredFieldValidator>
                        </li>
                        <li style="width: 99% !important; height:180px !important;">
                            <label>
                                Testimonial Text:</label><span class="fleft" style="margin:3px 5px;">
                            <AJ:Testimonial runat="server" id="txtTestimonial" />
                            (Please enter less than 300 characters for more visibilty)</span>
                        </li>
                        <li style="width: 99% !important;">
                           <label>
                                Publish:</label>
                            <asp:CheckBox ID="chkTestimonialStatus" runat="server" ToolTip="Check Status" TabIndex="5" />
                        </li>
                       
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                           <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="txtPriority" CausesValidation="true" ToolTip="Click to finish process"
                                TabIndex="6" OnClick="btnSave_Click" />
                           
                        </li>
                    </ul>
                </fieldset>
            </div>
</asp:Content>
