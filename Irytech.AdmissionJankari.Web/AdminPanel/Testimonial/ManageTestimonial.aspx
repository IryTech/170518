<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/Controls/Admin.Master" AutoEventWireup="true" CodeBehind="ManageTestimonial.aspx.cs" Inherits="IryTech.AdmissionJankari20.Web.AdminPanel.Testimonial.ManageTestimonial" %>
<%@ Register Src="~/UserControl/FckEditorCostomize.ascx" TagName="Testimonial" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="Aj" %>
<%@ Register Src="~/UserControl/AjaxFileUpload.ascx" TagName="FileUploader" TagPrefix="Aj" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <asp:UpdatePanel ID="updTestimonials" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblSeccessMsg" CssClass="success" runat="server" Width="100%" Visible="false"></asp:Label>
            <asp:Label ID="lblErorrMsg" CssClass="error" runat="server" Visible="false"></asp:Label>
           

            <ul class="addPage_utility">
        <li class="fright" style="width: 153px !important;">
            <div class="navbar-inner">
                <a href="#" id='sndAddTestomonual' class="insertIco" onclick="OpenPoup('divStudentTestomonialInsert','650px','sndAddTestomonual');return false;">
                                    Insert Page Title</a>
                <div class="clear">
                </div>
            </div>
        </li>
        </ul>

              <div class="grdOuterDiv">

               <fieldset>
                    <legend>Student Testimonial Master</legend>
                    <ul class="options-bar">
                    <li class="list75width">
                    <label>
                                User Name:
                            </label>
                            <asp:TextBox ID="txtSearch" CssClass="autocomplete" Style="width: 63%;" runat="server"></asp:TextBox>
                        
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="searchbtn" Text="Search" />

                     
                    </ul>
                    <asp:HiddenField ID="hndCollegeTopHirer" runat="server" />
                
                         <ul>
                        
                        <li>
                            <asp:Repeater ID="rptTestimonilas" runat="server" OnItemCommand="rptTestimonilas_ItemCommand">
                                <HeaderTemplate>
                                    <table class="grdView">
                                        <tr>
                                            <th>
                                                S.No
                                            </th>
                                            <th>
                                                Name
                                            </th>
                                            <th>
                                                Testimonial
                                            </th>
                                            <th>
                                                TestimonialStatus
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
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%# Eval("Testimonials")%>
                                        </td>
                                        <td>
                                            <%# Eval("TestimonilaStatus")%>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hndCollegeBranchCourseID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserID") %>' />
                                            <asp:LinkButton ID="BtnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("TestimonialID")%>'
                                                CommandName="Edit" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <Aj:CustomPaging ID="ucCustomPaging" runat="server" />
                        </li>
                    </ul>
                </fieldset>
            </div>
               <div id="divStudentTestomonialInsert" class="popup_block width60perc">
                <fieldset id="basicInfo">
                    <legend>Add Student Testomonial</legend>
                    <ul class="pouplist">
                        <li style="width: 99% !important;">
                             <label>
                                User Name:
                            </label>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtName" SetFocusOnError="true" Display="Static"
                            ErrorMessage="*" ValidationGroup="Testomonial"></asp:RequiredFieldValidator>

                        </li>
                        <li style="width: 99% !important;">
                           <label>
                                User Image :
                            </label>
                            <asp:HiddenField ID="hdnImageFile" runat="server" />                        
                         <Aj:FileUploader ID="FIleUploder" runat="server" style="border:1px solid green;" uploadToDirectory="../../Image/UserImage/" />
                        </li>
                        <li style="width: 99% !important; height:200px !important;">
                             <label>
                                Testimonials:
                            </label><span class="fleft" style="margin:3px 5px;">
                            <Aj:Testimonial ID="txtTesimonial" runat="server" />
                            </span>
                        </li>
                        <li style="width: 99% !important;">
                           <label>
                                Display:
                            </label>
                            <asp:CheckBox ID="chkStatus" runat="server" />
                        </li>
                        <li style="width: 99% !important;">
                            <label>
                                &nbsp;</label>
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Testomonial" Text="Submit" OnClick="btnSubmit_Click"  />
                            <asp:Button ID="btnClear" runat="server" Text="Reset" OnClick="btnClear_Click"  />
                        </li>
                    </ul>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divImage" class="loading">
        <img src="/image.axd?Common=Loading.gif" alt="Loading_Image" title="Loading Image" />
    </div>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            $("#fade").show();
            $("#divImage").show();
        }
        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            $("#fade").hide();
            $("#divImage").hide();
        }
        function ValidateStatus() {
            if (confirm("Are you sure you want to update the status?"))
                return true;
            else
                return false;
        }

    </script>
</asp:Content>
